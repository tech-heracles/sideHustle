using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Web.UI;
using AlphaWeb.Core.Logging;
using CacheLayer;
using DbCore;
using DbCore.classes;
using DbCore.DbAdmin;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Fiskalizimi.Controls;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;
using DbCore.Utility;
using DevExpress.Web;

namespace PlatinumWeb.ApplicationUtils.Pages
{
    public class MyPageBase : Page, IMyPage
    {
        public IAlphaWebLogger Logger { get; set; }

        public const string IdentifikuesFaqjeKeyJS = "CurrentPageId";
        public const string ViewStatePageId = "myPageId";

        private bool IsSetPageId = false;
        private ResourceManager _rm;
        protected ResourceManager rm => _rm ?? (_rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources")));
        public CultureInfo ci => MessagesResource.Messages.CurrentCultureInfo;
        protected int IdGjuha => mySessionObjects.ktheGjuhe(Session);
        protected int IdNdermarrja => mySessionObjects.merrIdNdermarrjeSesioni(Session);
        protected int IdViti => mySessionObjects.ktheIdVitNdermarrje(Session);
        protected int IdNdermarrjeVit => mySessionObjects.ktheNdermarrjeVit(Session);
        protected int VitiNdermarrjes => mySessionObjects.ktheVitiNdermarrjes(Session);
        protected int IdPerdoruesi => mySessionObjects.ktheIdPerdoruesi(Session);
        protected string GuidString => Guid.NewGuid().ToString();
        protected bool Meme => mySessionObjects.merrEshteMemeSesioni(Session);
        protected bool terms => mySessionObjects.ktheTerms(Session.SessionID);

        protected ASPxMenu _menu, _menuInfo;
        protected UpdatePanel _pnlMesazhi;
        protected void Page_PreInit(object sender, EventArgs e)
        {
#if DEBUG
            //this is only for DI testing purpose
            Logger.LogInformation($"Processing Request {Request.Url.ToString()}-----------Event Page_PreInit");
#endif
            if (!User.Identity.IsAuthenticated) return; //nese nuk eshte i autentikuar nuk ka nevoj per autorizime
            try
            {
                string absolutePath = Request.Url.AbsolutePath.ToString();
                if (absolutePath.Contains("aspx") && absolutePath != "/Login_Ndermarrje.aspx" && absolutePath != "/FaqeKryesore" && Session != null && absolutePath != "/Raporti.aspx")
                {
                    clsPerdorues user = new clsPerdorues(IdPerdoruesi);
                    clsNdermarrje enterprise = new clsNdermarrje(IdNdermarrja);
                    BigQueryLogMessage message = new BigQueryLogMessage(absolutePath, user.PerdoruesUsername, clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar(), enterprise.NdermarrjeKodi);
                    Logging.Log(message);
                };

            }
            catch
            {
                Console.WriteLine("Log error");
            }
            AplikoTheme();
            //rreshti me poshte duhet ckomentuar ne momentin qe do aplikohet gjuha shqip e devexpress-it
            //Page.UICulture = mySessionObjects.ktheCultureInfo(Session).Name;
            if (Request.Url.AbsolutePath.ContainsAnyIgnoreCase("Prezantohu.aspx") && Request.UrlReferrer == null && !Session.IsNewSession && terms)
                Context.RedirectToLoginNdermarrje();


            if (Request.Url.AbsolutePath.ContainsAnyIgnoreCase("Default.aspx", "Prezantohu.aspx", "Login_Ndermarrje.aspx", "FaqeKryesore.aspx", "FooterPanelInfo.aspx"))
                return;
            //idperdoruesi merr id-ne nga sesioni, nese id eshte 0, aty behet logout, e therrasim ketu per tu siguruar para cdo faqeje qe sesioni ka mbaruar
            var perdoruesi = IdPerdoruesi;
            try
            {
                if (!GlobalCacheManager.EshteScopeIdAktiv())
                    Context.RedirectToLoginNdermarrje();
            }
            catch (Exception ex)
            {
                ImbLogger.LogTrace($"LOG NGA METODA EshteScopeIdAktiv()  Exception -> {ex} , Data - > {ex.Data.Values}");
            }
            if (Request.QueryString["idraporti"] != null) return;
            var idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var idVitNdermarrje = mySessionObjects.ktheIdVitNdermarrje(Session);

            if (Meme && Request.Url.AbsoluteUri.ContainsAnyIgnoreCase("RegjistrimDokumentash.aspx") && Request.Url.AbsoluteUri.ContainsAnyIgnoreCase("shitje_blerje=blerje"))
                return;

            KontrolloTeDrejtaDheAutorizim(idNdermarrje, idVitNdermarrje);
        }

        protected void Page_InitComplete(object sender, EventArgs e)
        {
            RegiterUniquePageId();
        }

        protected void ShtoVleraTePergjithshmeNeHfState()
        {
            var hfState = (ASPxHiddenField)Page.FindControl("hfState");
            hfState.Set("_idGjuha", IdGjuha);
            hfState.Set("_idNdermarrje", IdNdermarrja);
            hfState.Set("_idPerdoruesi", IdPerdoruesi);
            hfState.Set("_idViti", IdViti);
            hfState.Set("_idNdermarrjeVit", IdNdermarrjeVit);
            hfState.Set("_vitiNdermarrjes", VitiNdermarrjes);
            hfState.Set("_meme", Meme);
            hfState["guidString"] = GuidString;

        }

        protected void ShtoMenuControlsDheMsgFrame()
        {
            var myControl = Page.FindControl("menu_msg_Frame");
            if (myControl == null)
                return;

            _menu = (ASPxMenu)myControl.FindControl("ASPxMenu1");
            _menuInfo = (ASPxMenu)myControl.FindControl("MenuInfo");
            _pnlMesazhi = (UpdatePanel)myControl.FindControl("pnlMesazhi");

        }

        protected void KontrolloTeDrejtaDheAutorizim(int idNdermarrje, int idVitNdermarrje)
        {
            var komponentet = GlobalCacheManager.MyAppCache.Get("komponentet", colKomponentet.MerriTeGjitha);
            try
            {
                //mund te ket ndodhur nje error,ose ka skaduar sesioni,ne kete moment kerkesa do behet redirect keshtu qe po e skip-im stepin e autorizimit
                if (Session == null) return;

                var pageAuthorization = new PageAuthorization(Request, IdPerdoruesi, idNdermarrje, idVitNdermarrje, IdGjuha, komponentet);
                if (pageAuthorization.IsPostBack()) return;

                if (!pageAuthorization.KaAutorizim())
                    Response.Redirect("ThankYou.html");


            }
            catch (ThreadAbortException thae)
            {
                //ska nevoj per catch
            }
            catch (Exception ex)
            {
#if !DEBUG
                    DbCore.IMBUtils.Logging.ImbLogger.Error(ex);
#endif
            }
        }

        /// <summary>
        /// aplikon theme-n e zgjedhur  per kete faqe
        /// </summary>
        private void AplikoTheme()
        {
            ASPxWebControl.SetIECompatibilityMode(11);
            if (Request.Url.AbsolutePath.StartsWith("/CRM"))
            {
                Page.Theme = "Moderno";
                return;
            }
            if (!(Request.Url.AbsolutePath.ContainsAnyIgnoreCase("FaqeKryesore.aspx") || Request.Url.AbsolutePath.ContainsAnyIgnoreCase("Raportet.aspx")))
                switch (Request.QueryString["vjenNga"])
                {
                    case "CRM":
                        Page.Theme = "Moderno";
                        return;
                    case "GIS":
                    case "epayslip":
                        Page.Theme = "MetropolisBlue";
                        return;
                }
            var idTheme = Request.QueryString["idTheme"];
            if (!string.IsNullOrEmpty(idTheme))
                clsFunksione.percaktoThemeAmbjenteDheJQueryMeId(Page, Convert.ToInt32(idTheme));
            else
                clsFunksione.percaktoThemeAmbjenteDheJQuery(Page, IdPerdoruesi);
        }
        /// <summary>
        /// merr nje unique id per faqje,e cila gjenerohet kur faqja hapet per heren e pare.
        /// dhe me pas ruhet ne viewState.
        /// </summary>
        /// <returns></returns>
        public string MerrIdentifikuesFaqje()
        {
            if (IsPostBack || IsSetPageId) return ViewState[ViewStatePageId] as string;
            IsSetPageId = true;
            string id = $"{Page.AppRelativeVirtualPath}_{Guid.NewGuid().ToString()}";
            ViewState[ViewStatePageId] = id;
            return id;
        }

        private void RegiterUniquePageId()
        {
            if (Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), IdentifikuesFaqjeKeyJS)) return;
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), IdentifikuesFaqjeKeyJS, $"<script>window['{IdentifikuesFaqjeKeyJS}']='{MerrIdentifikuesFaqje()}'</script>");

            //if (Page.IsClientScriptBlockRegistered(IdentifikuesFaqjeKeyJS)) return;
            //Page.RegisterStartupScript(IdentifikuesFaqjeKeyJS, $"<script>window['{IdentifikuesFaqjeKeyJS}']='{MerrIdentifikuesFaqje()}'</script>");
        }

        public virtual void MbushGrideNgaDb(bool ndryshoFiltrinGrides = false)
        {
        }

    }
}