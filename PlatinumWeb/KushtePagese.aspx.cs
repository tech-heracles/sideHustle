using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using DbCore.IMBUtils.Messages;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;


namespace PlatinumWeb
{
    public partial class KushtePagese : MyPageBase
    {
        DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
        private string komponente = "KushtePagese.aspx";
        private string guidString;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            //if (CacheLayer.GlobalCacheManager.MySessionCache["LoggedIn"].Equals("No"))
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            //if (CacheLayer.GlobalCacheManager.MySessionCache["KodiNdermarrjes"] == null)
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
            }
            //oPerdorues = (DbCore.DbAdmin.clsPerdorues)(CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"]);
            oPerdorues = DbCore.mySessionObjects.kthePerdorues(Session);

            if (!IsPostBack)
            {
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                if (Request.QueryString["ruaj"] == "ok")
                    pergjigja.Text = MessagesResource.Messages["mesazhRuajtjeMeSukses"];

                konfiguroVleraFillestare();
                konfiguroGride();
                GridUtil.percaktoVisibleColumnsMeWidth(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), grid_KushtePagese, "grid_KushtePagese", komponente);

                AspxWebControlUtils.konfiguroMenuPaTheme(ASPxMenu1);
                AspxWebControlUtils.percaktoTedrejtatPerKeteFaqe(komponente, ASPxMenu1);
                if (Request.QueryString["indexrow"] != null)
                {
                    grid_KushtePagese.FocusedRowIndex = int.Parse(Request.QueryString["indexrow"]);
                }
            }
            guidString = (string)hfState["guidString"];
            konfiguroGride();

        }

        /// <summary>
        /// Mbush griden e kushteve te pageses me listen e kushteve te pageses se ndermarrjes
        /// </summary>
        private void konfiguroVleraFillestare()
        {
            //dbKontab = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();
            //DbCore.DbKontabiliteti.colKushtPageseKoka colKushtePagese = dbKontab.merrGjitheKushtetPageses(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbKontabiliteti.colKushtPageseKoka colKushtePagese = new DbCore.DbKontabiliteti.colKushtPageseKoka(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            grid_KushtePagese.DataSource = colKushtePagese;
            grid_KushtePagese.DataBind();
        }

        /// <summary>
        /// Shton nje kolone checkbox ne gride per te bere selektim
        /// </summary>
        protected void grid_KushtePagese_DataBound(object sender, EventArgs e)
        {
            if (grid_KushtePagese.Columns["#"] == null)
            {
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.Width = 20;
                check.ShowSelectCheckbox = true; check.Width = Unit.Percentage(2);
                check.SetColVisibleIndex(0);
                grid_KushtePagese.Columns.Add(check);
            }
        }

        /// <summary>
        /// Konfiguron griden e kushteve te pageses
        /// </summary>
        private void konfiguroGride()
        {
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            KonfigurimComboGride.shtoAutorizimSipasKushtePagese(grid_KushtePagese, idPerdoruesi, Session, komponente, guidString, "IdAutorizim");
            KonfigurimComboGride.shtoLlojPagese(grid_KushtePagese, rm, ci);

           GridUtil.konfiguroGrideListeEvogelPaTheme(grid_KushtePagese, "IdKoka");
        }

        /// <summary>
        /// Therret funksionet 
        /// <see cref="konfiguroVleraFillestare"/>
        /// <see cref="konfiguroGride"/>
        /// </summary>
        protected void grid_KushtePagese_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            konfiguroVleraFillestare();
            konfiguroGride();
        }

        /// <summary>
        /// Fshin kushtin e pageses qe eshte zgjedhur
        /// </summary>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            List<object> rreshtat = grid_KushtePagese.GetSelectedFieldValues("IdKoka");
            foreach (int id in rreshtat)
            {
                //dbKontab = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();
                //DbCore.DbKontabiliteti.colKushtPageseKoka colKushtePagese = dbKontab.merrKushtPageseSipasID(id);
                DbCore.DbKontabiliteti.clsKushtPageseKoka clsKushtePagese = new DbCore.DbKontabiliteti.clsKushtPageseKoka(id);
                //foreach (DbCore.DbKontabiliteti.clsKushtPageseKoka koka in colKushtePagese)
                //{
                clsKushtePagese.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = clsKushtePagese.fshi();
                //}
            }
            Response.Redirect(komponente);
        }

        /// <summary>
        /// Percakton veprimin qe kryhet nese klikohet nje nga butonat e menuse
        /// </summary>
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            if (e.Item.Name == "Modifiko")
            {
                int indeksi = grid_KushtePagese.FocusedRowIndex;
                string id;

                if (grid_KushtePagese.GetRowValues(indeksi, "IdKoka") != null)
                    id = grid_KushtePagese.GetRowValues(indeksi, "IdKoka").ToString();
                else
                    id = null;

                Response.Redirect("~/Modifiko_KushtPagese.aspx?id=" + id + "&indexrow=" + grid_KushtePagese.FocusedRowIndex);
            }

            else
                if (e.Item.Name == "Shto")
                {
                    Response.Redirect("Shto_KushtPagese.aspx");
                }
        }

    }
}
