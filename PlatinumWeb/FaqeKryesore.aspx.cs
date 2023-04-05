using DbCore.DbAdmin;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Globalization;
using System.Resources;
using System.Web;
using System.Web.Security;
using DbCore;
using System.Web.Configuration;
using CacheLayer;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Fiskalizimi.API;
using DbCore.IMBUtils.Fiskalizimi.Controls;
using DbCore.IMBUtils.Security;
using System.Web.UI;
using System.Threading.Tasks;

namespace PlatinumWeb
{
    public partial class FaqeKryesore : MyPageBase
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            base.Page_PreInit(sender, e);

            //if (!(Request.Browser.Browser == "InternetExplorer" && Request.Browser.Version == "11.0"))
                ASPxMenu1.Theme = "Moderno1";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                var cookie = Request.Cookies["adresa"];
                if (cookie == null)
                {
                    cookie = new HttpCookie("adresa");
                }
                if (!DbCore.mySessionObjects.isLogedIn(Session))
                {
                    cookie.Value = DbCore.IMBUtils.Paths.defaultLoginPath;
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(cookie);
                    DbCore.clsFunksione.logout(Session, true, string.Empty, false);
                    return;
                }
                int idgjuha = mySessionObjects.ktheGjuhe(Session);
                var periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
                if (periudha != null)
                    periudha = new clsPeriudhaKontabel(periudha.IdPeriudha, idgjuha);

                var ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
                hfState.Set("activateSignalR", System.Web.Configuration.WebConfigurationManager.AppSettings["activateSignalR"].ToString());
                hfState.Set("periudha", JsonConvert.SerializeObject(periudha));
                hfState.Set("idGjuha", DbCore.mySessionObjects.ktheGjuhe(Session));
                var user = DbCore.mySessionObjects.kthePerdorues(Session);
                var idPerdoruesi = user.IdPerdorues;
                var ndermarrja = DbCore.mySessionObjects.ktheKodNdermarrje(Session);
                
                var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                string stringKonfigMenuMajtas = DbCore.DbShare.clsKonfigMenu.ktheListKonfigMenu(idPerdoruesi, idNdermarrje);
                hfState.Set("adminUser", false);
                clsPerdorues perdorues = new clsPerdorues(mySessionObjects.ktheIdPerdoruesi(Session));
                foreach (var role in perdorues.OColRolPerdoruesi)
                {
                    clsRoli rol = new clsRoli(role.IdRoli);
                    if (rol.KodRoli == "RA" || rol.KodRoli == "RAS") hfState.Set("adminUser", true);
                }
                hfState.Set("konfigMenuMajtas", String.IsNullOrEmpty(stringKonfigMenuMajtas) ? "" : stringKonfigMenuMajtas);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("ShfaqPerdoruesMenu", user.ShfaqPerdoruesMenu);
                hfState.Set("EmerMbiemerPerdorues", user.EmriPerdorues + ' ' + user.MbiemriPerdorues);
                hfState.Set("organizata", clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar());
                hfState.Set("emailPerdoruesi", user.PerdoruesEmail);
                hfState.Set("Username",user.PerdoruesUsername);
                hfState.Set("shenime",user.Shenime);
                hfState.Set("Kadnderrmarje",ndermarrja);
                var licenca = new clsLicenca();
                licenca.mbushLicencen(IdPerdoruesi);
                hfState.Set("chatAktiv", licenca.ChatAktiv);
                hfState.Set("chatLink", licenca.ChatLink);
                hfState.Set("chatPortHttp", licenca.ChatPortHttp);
                hfState.Set("chatPortHttps", licenca.ChatPortHttps);
                PageAsyncTask t = new PageAsyncTask(showPopUp);
                Page.RegisterAsyncTask(t);
                Page.ExecuteRegisteredAsyncTasks();
                hfState.Set("googleAnalytics", licenca.GoogleAnalytics);
                hfState.Set("googleAnalyticsTrackingId", licenca.GoogleAnalyticsTrackingId);
                if (user.ShfaqPerdoruesMenu)
                {
                    ASPxLabel1.Text = user.EmriPerdorues + ' ' + user.MbiemriPerdorues;
                }
                hfState.Set("idNdermarrje", idNdermarrje);
                var mesazhiPerodruesit = DbCore.clsFunksione.KtheMesazhPerPerdoruesin();
                if (!String.IsNullOrEmpty(mesazhiPerodruesit))
                {
                    popUpAzhornim.ShowOnPageLoad = true;
                    popUpAzhornim.Text = mesazhiPerodruesit;
                }
                var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                //var msgSkadimLicence = DbCore.DbAdmin.clsLicenca.KontrolloSkadiminLicences(idPerdoruesi, rm, ci);
                //if (!msgSkadimLicence.Status)
                //{
                //    if (msgSkadimLicence.PershkrimMesazhi == "Problem ne validimin e licences!")
                //        DbCore.clsFunksione.logout(Session, true, "problemLicenca");
                //    else DbCore.clsFunksione.logout(Session, true, "perfundoiLicenca");
                //}
                //if (!msgSkadimLicence.PershkrimMesazhi.Equals(string.Empty))
                //{
                //    popUpDiteTeMbeturaLicenca.ShowOnPageLoad = true;
                //    lblDiteTeMbeturaTeLicenca.Text = msgSkadimLicence.PershkrimMesazhi;
                //}
                var obj = DbCore.clsFunksione.ktheUrlHelpi(string.Empty);

                var urlHelp = obj.Item1;
                var urlVersion = obj.Item2;

                string urlImazhPerdoruesi = DbCore.DbShare.clsArkiva.ktheImazhPerdoruesi(idPerdoruesi);
                if (String.IsNullOrEmpty(urlImazhPerdoruesi) || !System.IO.File.Exists(Server.MapPath(urlImazhPerdoruesi)))
                    urlImazhPerdoruesi = "images/new/perdorues.png";
                DevExpress.Web.MenuItem ikonaImazhPerdorues = ASPxMenu1.Items.FindByName("ikonaImazhPerdorues");
                ikonaImazhPerdorues.Image.Url = urlImazhPerdoruesi;
                ikonaImazhPerdorues.Image.Width = 25;
                ikonaImazhPerdorues.Image.Height = 25;
                bool kycurMobile = clsPerdorues.kthePerdoruesKycurMobile(idPerdoruesi);
                hfState.Set("kycurMobile", kycurMobile);
                DevExpress.Web.MenuItem grupitHelpMenuLart = ASPxMenu1.Items.FindByName("help");
                DevExpress.Web.NavBarGroup grupiHelp = ASPxNavBar1.Groups.FindByName("help");

                grupitHelpMenuLart.Items[0].NavigateUrl = urlHelp;
                grupiHelp.Items[0].NavigateUrl = urlHelp;

                


                grupitHelpMenuLart.Items[4].NavigateUrl = urlVersion;

                grupitHelpMenuLart.Items[0].NavigateUrl = urlHelp;
                grupiHelp.Items[0].NavigateUrl = urlHelp;
                hfUrlHelp.Set("urlHelp", urlHelp);
                var IdNdermViti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session).ToString();
                var KodViti = DbCore.mySessionObjects.ktheVitiNdermarrjes(Session).ToString();
                var pathBck = "height: 100%; width: 100%;";
           
                backDiv.Attributes.Add("style", pathBck);
                if (user.KontrollPassword)
                {
                    var konfig = new DbCore.DbAdmin.clsKonfigurimeFjalekalimi(idPerdoruesi);
                    if (konfig.NdryshimPasswordiDetyruar || konfig.SkadoPassword)
                    {
                        var passVlefshem = DbCore.clsFunksione.kontrolloPassword(konfig, idPerdoruesi, HttpContext.Current, rm, ci);
                        if (passVlefshem.Status)
                        {
                            enableMenuTeDrejta(idPerdoruesi, rm, ci);
                        }
                        else
                        {
                            Response.Redirect("NdryshimFjalekalimi.aspx");
                            return;
                        }
                    }
                    else
                    {
                        enableMenuTeDrejta(idPerdoruesi, rm, ci);
                    }
                }
                else
                {
                    enableMenuTeDrejta(idPerdoruesi, rm, ci);
                }
                string exCertificateDate =  clsFunksioneFiskalizimi.kontrolloNeseCertifikataKaSkaduar(idNdermarrje);
                if (exCertificateDate != "")
                    hfState.Set("SkadimCertifikate", exCertificateDate);
                else hfState.Set("SkadimCertifikate", "");
                EmrateLabelave(ci, obj.Item3);
                var idTheme = !String.IsNullOrEmpty(Request.QueryString["idTheme"]) ? Convert.ToInt32(Request.QueryString["idTheme"]) : DbCore.DbAdmin.clsThemesAmbjente.ktheIdTheme(idPerdoruesi);
                hfState.Set("idTheme", idTheme);
                var komponente = !String.IsNullOrEmpty(Request.QueryString["ambienti"]) ? buildQueryStringNgaAmbienti(Request.QueryString) : string.Empty;
                if (!String.IsNullOrEmpty(Request.QueryString["ambDef"]) && Request.QueryString["ambDef"] == "Dashboard.aspx")
                    komponente = "Dashboard.aspx";
                if (komponente == string.Empty && (Request.QueryString["vjenNga"] != "CRM" || Request.QueryString["vjenNga"] != "GIS"))
                {
                    //komponente = DbCore.DbAdmin.clsKomponente.merrKomponenteDefaultPerdoruesi(idPerdoruesi);
                    komponente = clsFunksione.ktheKomponenteDefaultPerPerdorues(idPerdoruesi, idNdermarrje, "Default.aspx", KodViti);
                }
                if (komponente == string.Empty)
                {
                    komponente = "Default.aspx";
                }
                var scopeID = Request.Params[ScopeManager.ScopeIdKey];
                komponente= DbCore.clsFunksione.shtoVarToUrl(komponente, "idTheme", idTheme.ToString());
                komponente= DbCore.clsFunksione.shtoVarToUrl(komponente, ScopeManager.ScopeIdKey, scopeID);
                ASPxSplitter1.GetPaneByName("paneKryesor").ContentUrl = komponente;

                cookie.Value = komponente;
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(cookie);

                var komponenteFooter= DbCore.clsFunksione.shtoVarToUrl("FooterPanelInfo.aspx", "idTheme", idTheme.ToString());
                komponenteFooter = clsFunksione.shtoVarToUrl(komponenteFooter, ScopeManager.ScopeIdKey, scopeID);
                ASPxSplitter1.GetPaneByName("Footer").ContentUrl = komponenteFooter;

                var njoftimet = new DbCore.DbShare.colNjoftime(idPerdoruesi, DateTime.Now);
                hfState.Set("njoftimet", JsonConvert.SerializeObject(njoftimet));
                hfState.Set("afishonjoftime", user.ShfaqMesazhePopup);

            }
            if (IsPostBack && !IsCallback)
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
            }
            string connectionStringName = clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar();
            bool jsonWebRequest =  clsFunksione.sendExpireLicenceRequest(WebConfigurationManager.AppSettings["expireLink"], connectionStringName);

        }
        public static string buildQueryStringNgaAmbienti(System.Collections.Specialized.NameValueCollection queryStringCol)
        {
            var removeString = "ambienti" + "=" + queryStringCol["ambienti"];
            var queryString = queryStringCol.ToString();
            var index = queryString.IndexOf(removeString);
            queryString = queryString.Remove(index, removeString.Length);
            queryString = queryString.Replace("&&", "&");
            queryString = queryString.StartsWith("&") ? queryString.Remove(0, 1) : queryString.EndsWith("&") ? queryString.Remove(queryString.Length - 1, 1) : queryString;
            return queryStringCol["ambienti"] + "?" + queryString;
        }
        protected void refreshFooter(object sender, EventArgs e)
        {
            ASPxSplitter1.GetPaneByName("Footer").ContentUrl = "FooterPanelInfo.aspx";
        }

        protected void ButtonOk_RemoteSupport_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/TeamViewer/TeamViewerSetup.exe");
            return;
        }
        private async Task showPopUp()
        {
            bool val = await clsFunksione.merrShenimePerdoruesi(new clsPerdorues(IdPerdoruesi).Shenime);
            googlePopup.Visible = val;
        }
        protected void btnDownloadProgramKase_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Kase/ImbKase.exe");
            return;
        }

        public void TeDrejta()
        {
            try
            {
                var idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                var idviti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);

                var dt = DbCore.DbAdmin.colTeDrejtaRoli.merrTeDrejtaRoliDheRaporteshMeEmraKomponentesh(idPerdoruesi, idNdermarrje, idviti);
                for (var i = 0; i < ASPxMenu1.Items.Count; i++)
                {
                    var countmenu = 0;
                    for (var j = 0; j < ASPxMenu1.Items[i].Items.Count; j++)
                    {
                        var countnenmenu = 0;
                        for (var m = 0; m < ASPxMenu1.Items[i].Items[j].Items.Count; m++)
                        {
                            var countnmenu = 0;
                            for (var p = 0; p < ASPxMenu1.Items[i].Items[j].Items[m].Items.Count; p++)
                            {
                                var countnennenmenu = 0;
                                for (var q = 0; q < ASPxMenu1.Items[i].Items[j].Items[m].Items[p].Items.Count; q++)
                                {
                                    if (ASPxMenu1.Items[i].Items[j].Items[m].Items[p].Items[q].Name != string.Empty)
                                    {
                                        if (ConfigurationManager.AppSettings["ZyreKlient"].ToString() == "true")
                                        {
                                            ASPxMenu1.Items[i].Items[j].Items[m].Items[p].Items[q].Visible = true;
                                            countnennenmenu++;
                                            continue;
                                        }
                                        var dr = dt.Select("KOMPONEMRI= '" + ASPxMenu1.Items[i].Items[j].Items[m].Items[p].Items[q].Name.Split('&')[0] + "'");
                                        if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
                                        {
                                            ASPxMenu1.Items[i].Items[j].Items[m].Items[p].Items[q].Visible = true;
                                            countnennenmenu++;
                                        }
                                        else
                                        {
                                            ASPxMenu1.Items[i].Items[j].Items[m].Items[p].Items[q].Visible = false;
                                        }
                                    }
                                }

                                if (ASPxMenu1.Items[i].Items[j].Items[m].Items[p].Name != string.Empty)
                                {
                                    if (ConfigurationManager.AppSettings["ZyreKlient"].ToString() == "true")
                                    {
                                        ASPxMenu1.Items[i].Items[j].Items[m].Items[p].Visible = true;
                                        countnmenu++;
                                        continue;
                                    }

                                    var dr = dt.Select("KOMPONEMRI= '" + ASPxMenu1.Items[i].Items[j].Items[m].Items[p].Name.Split('&')[0] + "'");
                                    if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
                                    {
                                        ASPxMenu1.Items[i].Items[j].Items[m].Items[p].Visible = true;
                                        countnmenu++;
                                    }
                                    else
                                    {
                                        if (countnennenmenu > 0)
                                        {
                                            ASPxMenu1.Items[i].Items[j].Items[m].Items[p].Visible = true;
                                            countnmenu++;
                                        }
                                        else
                                        {
                                            ASPxMenu1.Items[i].Items[j].Items[m].Items[p].Visible = false;
                                        }
                                    }
                                }
                                else
                                {
                                    if (countnennenmenu > 0)
                                    {
                                        ASPxMenu1.Items[i].Items[j].Items[m].Items[p].Visible = true;
                                        countnmenu++;
                                    }
                                    else
                                    {
                                        ASPxMenu1.Items[i].Items[j].Items[m].Items[p].Visible = false;
                                    }
                                }
                            }
                            if (ASPxMenu1.Items[i].Items[j].Items[m].Name != string.Empty)
                            {
                                if (ConfigurationManager.AppSettings["ZyreKlient"].ToString() == "true")
                                {
                                    ASPxMenu1.Items[i].Items[j].Items[m].Visible = true;
                                    countnenmenu++;
                                    continue;
                                }
                                if (ASPxMenu1.Items[i].Items[j].Items[m].Name == "ImportWK.aspx?lloji=importwk" && !DbCore.mySessionObjects.merrEshteMemeSesioni(Session))
                                {
                                    ASPxMenu1.Items[i].Items[j].Items[m].Visible = false;
                                    continue;
                                }
                                var dr = dt.Select("KOMPONEMRI= '" + ASPxMenu1.Items[i].Items[j].Items[m].Name.Split('&')[0] + "'");
                                if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
                                {
                                    ASPxMenu1.Items[i].Items[j].Items[m].Visible = true;
                                    countnenmenu++;
                                }
                                else
                                {
                                    if (countnmenu > 0)
                                    {
                                        ASPxMenu1.Items[i].Items[j].Items[m].Visible = true;
                                        countnenmenu++;
                                    }
                                    else
                                    {
                                        ASPxMenu1.Items[i].Items[j].Items[m].Visible = false;
                                    }
                                }
                            }
                            else
                            {
                                if (countnmenu > 0)
                                {
                                    ASPxMenu1.Items[i].Items[j].Items[m].Visible = true;
                                    countnenmenu++;
                                }
                                else
                                {
                                    ASPxMenu1.Items[i].Items[j].Items[m].Visible = false;
                                }
                            }
                        }
                        if (ASPxMenu1.Items[i].Items[j].Name != string.Empty)
                        {
                            if (ConfigurationManager.AppSettings["ZyreKlient"].ToString() == "true")
                            {
                                ASPxMenu1.Items[i].Items[j].Visible = true;
                                countmenu++;
                                continue;
                            }

                            var dr = dt.Select("KOMPONEMRI= '" + ASPxMenu1.Items[i].Items[j].Name.Split('&')[0] + "'");
                            if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
                            {
                                ASPxMenu1.Items[i].Items[j].Visible = true;
                                countmenu++;
                            }
                            else
                            {
                                if (countnenmenu > 0)
                                {
                                    ASPxMenu1.Items[i].Items[j].Visible = true;
                                    countmenu++;
                                }
                                else
                                {
                                    ASPxMenu1.Items[i].Items[j].Visible = false;
                                }
                            }
                        }
                        else
                        {
                            if (countnenmenu > 0)
                            {
                                ASPxMenu1.Items[i].Items[j].Visible = true;
                                countmenu++;
                            }
                            else
                            {
                                ASPxMenu1.Items[i].Items[j].Visible = false;
                            }
                        }
                    }
                    if (countmenu == 0)
                    {
                        ASPxMenu1.Items[i].Visible = false;
                    }
                    else
                    {
                        ASPxMenu1.Items[i].Visible = true;
                    }
                }
                for (var k = 0; k < ASPxNavBar1.Groups.Count; k++) //fsheh/shfaq elemenet e navbarit sipas te drejtave
                {
                    var count = 0;
                    DevExpress.Web.NavBarGroup tmpGrup = ASPxNavBar1.Groups[k];

                    if (tmpGrup.ToString() == "Dashboard")
                    {
                        var drMobile = dt.Select("KOMPONEMRI= 'Dashboard.aspx'");
                        if(drMobile[0]["D_AMB"].ToString() == "1")
                            count++;
                    }

                    if (tmpGrup.ToString() == "Mobile")
                    {
                        var drMobile = dt.Select("TEXTMODUL= 'Mobile'");
                        for (var m = 0; m < drMobile.Length; m++)
                        {
                            if (drMobile[m]["KOMPONEMRI"].ToString() != string.Empty)
                            {
                                if (drMobile[m]["D_AMB"].ToString() == "1")
                                {
                                    count++;
                                }
                            }
                        }
                         
                    }
                    for (var l = 0; l < tmpGrup.Items.Count; l++)
                    {
                        if (ConfigurationManager.AppSettings["ZyreKlient"].ToString() == "true")
                        {
                            tmpGrup.Items[l].Visible = true;
                            count++;
                            continue;
                        }
                        if (tmpGrup.Items[l].Name != string.Empty)
                        {
                            var dr = dt.Select("KOMPONEMRI= '" + tmpGrup.Items[l].Name.Split('&')[0] + "'");
                            tmpGrup.Items[l].Visible = true;
                            if (dr.Length > 0 && dr[0]["D_AMB"].ToString() == "1")
                            {
                                tmpGrup.Items[l].Visible = true;
                                count++;
                            }
                            else
                            {
                                tmpGrup.Items[l].Visible = false;
                            }
                        }
                        else
                            System.Diagnostics.Trace.WriteLine("Shume gabim - turp");
                    }

                    tmpGrup.Visible = true;
                    tmpGrup.ClientVisible = count != 0;

                }
                //   shfaqHelp();
                DevExpress.Web.MenuItem ikonaImazhPerdoruesMenuLart = ASPxMenu1.Items.FindByName("ikonaImazhPerdorues");
                ikonaImazhPerdoruesMenuLart.Visible = true;
                ikonaImazhPerdoruesMenuLart.Items.FindByName("LupaPersonalizoPerdorues.aspx").Visible = true;
                ikonaImazhPerdoruesMenuLart.Items.FindByName("mesazhe").Visible = true;
                ikonaImazhPerdoruesMenuLart.Items.FindByName("abonimi").Visible = true;
                ikonaImazhPerdoruesMenuLart.Items.FindByName("dalje").Visible = true;
                //DevExpress.Web.MenuItem grupitHelpMenuLart = ASPxMenu1.Items.FindByName("settings");
                //grupitHelpMenuLart.Visible = true;
                //grupitHelpMenuLart.ClientVisible = true;
                //ikonaImazhPerdoruesMenuLart.Items.FindByName("mesazhe").ClientVisible = true;
                ASPxNavBar1.Groups.FindByName("settings").Visible = true;
                ASPxNavBar1.Groups.FindByName("settings").ClientVisible = true;
                if (ASPxNavBar1.Groups.FindByName("Mobile").ClientVisible)
                { 
                    ASPxNavBar1.Groups.FindByName("Mobile").ClientVisible = !(bool)hfState["kycurMobile"];
                    ASPxNavBar1.Groups.FindByName("Mobile").Visible = !(bool)hfState["kycurMobile"];
                }
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
            }
        }

        //private void shfaqHelp()
        //{
        //    DevExpress.Web.MenuItem grupitHelpMenuLart = ASPxMenu1.Items.FindByName("help");
        //    grupitHelpMenuLart.Visible = true;
        //    grupitHelpMenuLart.Items.FindByName("manuali").Visible = true;
        //    DevExpress.Web.NavBarGroup grupiHelp = ASPxNavBar1.Groups.FindByName("help");
        //    if (bool.Parse(WebConfigurationManager.AppSettings["BuxhetQK"]))
        //    {
        //        grupitHelpMenuLart.Items.FindByName("ProgramKase").Visible = true;
        //        grupitHelpMenuLart.Items.FindByName("ProgramKaseNew").Visible = true;

        //        grupiHelp.Items.FindByName("ProgramKase").Visible = true;
        //        grupiHelp.Items.FindByName("ProgramKaseNew").Visible = true;
        //        grupiHelp.Items.FindByName("RemoteSupport").Visible = true;
        //    }
        //    grupiHelp.Visible = true;
        //    grupiHelp.ClientVisible = true;
        //    ASPxNavBar1.Groups.FindByName("settings").ClientVisible = true;
        //    grupiHelp.Items.FindByName("manuali").Visible = true;

        //}
        private void enableMenuTeDrejta(int idPerdoruesi, ResourceManager rm, CultureInfo ci)
        {
            var idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var KodViti = DbCore.mySessionObjects.ktheVitiNdermarrjes(Session).ToString();
            if (idNdermarrje == 0)
            {
                DbCore.clsFunksione.avancoPerpara(Response, Session, idPerdoruesi, rm, ci, (bool)Application["validInstall"]);
            }
            else
            {
                if (Request.QueryString["vjenNga"] != "CRM")
                {
                    if ((Request.UrlReferrer != null && (HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["vjenNga"] == "CRM"))
                        || (DbCore.mySessionObjects.merrObjectNgaSesioni(Session) != null && DbCore.mySessionObjects.merrObjectNgaSesioni(Session).ToString() == "shfaqdefault"))
                    {
                        int idTheme = clsThemesAmbjente.ktheIdTheme(idPerdoruesi);
                        //string komponente = DbCore.DbAdmin.clsKomponente.merrKomponenteDefaultPerdoruesi(idPerdoruesi);
                        string komponente = clsFunksione.ktheKomponenteDefaultPerPerdorues(idPerdoruesi, idNdermarrje, String.Empty, KodViti);
                        if (komponente != "")
                        {
                            Response.Redirect(DbCore.clsFunksione.shtoVarToUrl(komponente, "idTheme", idTheme.ToString()), false);
                            return;
                        }
                        DbCore.mySessionObjects.ruajObjectNeSesion(Session, "");
                    }
                }

            }

            //DbCore.clsFunksione.vendosPeriudhenKlientSide(Session, hfPeriudhKontabel);
            TeDrejta();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            var ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            enableMenuTeDrejta(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), rm, ci);
        }
        protected void VerifikoEmail_Click(object sender, EventArgs e)
        {
            PasswordHelper.GjenroApiKey("");
        }
        

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci, string versioni)
        {
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            //ASPxHyperLink2.Text = " | " + rm.GetString("labelLogOut", ci);
            ASPxMenu1.Items[0].Items[15].Visible = true;

            ASPxMenu1.Items[0].Text = rm.GetString("MenuItemAdminstrimi", ci);
            ASPxMenu1.Items[0].Items[0].Text = rm.GetString("MenuItemAsistenti", ci);
            ASPxMenu1.Items[0].Items[1].Text = rm.GetString("MenuItemBackupRestore", ci);
            ASPxMenu1.Items[0].Items[1].Items[0].Text = rm.GetString("MenuItemBackup", ci);
            ASPxMenu1.Items[0].Items[1].Items[1].Text = rm.GetString("MenuItemRestore", ci);
            clsPerdorues perdorues = new clsPerdorues(mySessionObjects.ktheIdPerdoruesi(Session));
            foreach (var role in perdorues.OColRolPerdoruesi)
            {
                clsRoli rol = new clsRoli(role.IdRoli);
                if (rol.KodRoli == "RSU" || rol.KodRoli == "RA" || rol.KodRoli == "RAS")
                {
                    ASPxMenu1.Items[0].Items[1].Visible = true;
                    ASPxMenu1.Items[0].Items[1].Items[2].Visible = true;
                    ASPxMenu1.Items[0].Items[1].Items[0].Visible = false;
                    ASPxMenu1.Items[0].Items[1].Items[1].Visible = false;
                    ASPxMenu1.Items[0].Items[16].Visible = true;
                    break;
                }
            }
            ASPxMenu1.Items[0].Items[2].Text = "Dergo Mesazh";
            ASPxMenu1.Items[0].Items[3].Text = rm.GetString("MenuItemMotivet", ci);
            ASPxMenu1.Items[0].Items[4].Text = rm.GetString("MenuItemFjalekalimi", ci);
            ASPxMenu1.Items[0].Items[4].Items[0].Text = rm.GetString("MenuItemPolitikaFjalekalimi", ci);
            ASPxMenu1.Items[0].Items[4].Items[1].Text = rm.GetString("MenuItemNdryshimFjalekalimi", ci);
            ASPxMenu1.Items[0].Items[5].Text = rm.GetString("MenuItemHistorikuEmail", ci);
            ASPxMenu1.Items[0].Items[6].Text = rm.GetString("MenuItemHyrjetDaljetNeProgram", ci);
            ASPxMenu1.Items[0].Items[7].Text = rm.GetString("MenuItemKonfigurimEmail", ci);
            ASPxMenu1.Items[0].Items[8].Text = rm.GetString("MenuItemKonfigurimFtp", ci);
            ASPxMenu1.Items[0].Items[9].Text = rm.GetString("MenuItemLogeSistemi", ci);

            ASPxMenu1.Items[0].Items[10].Text = rm.GetString("MenuItemMbylljePeriudhe", ci);
            ASPxMenu1.Items[0].Items[11].Text = rm.GetString("MenuItemSkemaWorkFlow", ci);
            ASPxMenu1.Items[0].Items[12].Text = rm.GetString("MenuItemStrukturaOrganizimit", ci);
            ASPxMenu1.Items[0].Items[12].Items[0].Text = rm.GetString("MenuItemGrupimNdermarrjesh", ci);
            ASPxMenu1.Items[0].Items[12].Items[1].Text = rm.GetString("MenuItemNdermarrjet", ci);
            ASPxMenu1.Items[0].Items[12].Items[2].Text = rm.GetString("MenuItemDegetAdministrative", ci);
            ASPxMenu1.Items[0].Items[12].Items[3].Text = rm.GetString("MenuItemDepartamentet", ci);
            ASPxMenu1.Items[0].Items[13].Text = rm.GetString("MenuItemTeDrejtat", ci);
            ASPxMenu1.Items[0].Items[13].Items[0].Text = rm.GetString("MenuItemRolet", ci);
            ASPxMenu1.Items[0].Items[13].Items[1].Text = rm.GetString("MenuItemPerdoruesit", ci);
            ASPxMenu1.Items[0].Items[13].Items[2].Text = rm.GetString("MenuItemAutorizimet", ci);
            ASPxMenu1.Items[0].Items[15].Text = rm.GetString("MenuItemWebhooks", ci);
            ASPxMenu1.Items[0].Items[16].Text = rm.GetString("MenuItemInvite", ci);

            ASPxMenu1.Items[1].Text = rm.GetString("MenuItemKonfigurime", ci);
            ASPxMenu1.Items[1].Items[0].Text = rm.GetString("MenuItemAmortizimi", ci);
            ASPxMenu1.Items[1].Items[0].Items[0].Text = rm.GetString("MenuItemStandardeAmortizimi", ci);
            ASPxMenu1.Items[1].Items[0].Items[1].Text = rm.GetString("MenuItemRregullaAmortizimi", ci);

            ASPxMenu1.Items[1].Items[1].Text = rm.GetString("MenuItemCmimet", ci);
            ASPxMenu1.Items[1].Items[1].Items[0].Text = rm.GetString("MenuItemNivelCmimi", ci);
            ASPxMenu1.Items[1].Items[1].Items[1].Text = rm.GetString("MenuItemPercaktimCmimi", ci);
            ASPxMenu1.Items[1].Items[1].Items[1].Items[0].Text = rm.GetString("MenuItemCmimetShitjeve", ci);
            ASPxMenu1.Items[1].Items[1].Items[1].Items[1].Text = rm.GetString("MenuItemCmimetBlerje", ci);
            ASPxMenu1.Items[1].Items[2].Text = rm.GetString("MenuItemDokumenta", ci);
            ASPxMenu1.Items[1].Items[2].Items[0].Text = rm.GetString("MenuItemKategoriDokumenti", ci);
            ASPxMenu1.Items[1].Items[2].Items[1].Text = rm.GetString("MenuItemGrupetDokumentave", ci);
            ASPxMenu1.Items[1].Items[3].Text = rm.GetString("MenuItemEksportImportTeDhenash", ci);
            ASPxMenu1.Items[1].Items[3].Items[0].Text = rm.GetString("MenuItemKonfigurimFormati", ci);
            ASPxMenu1.Items[1].Items[3].Items[0].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[1].Items[3].Items[0].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[1].Items[3].Items[1].Text = rm.GetString("MenuItemEksport", ci);
            ASPxMenu1.Items[1].Items[3].Items[2].Text = rm.GetString("MenuItemImport", ci);
            ASPxMenu1.Items[1].Items[3].Items[3].Text = rm.GetString("MenuItemImportShitjeWinlineCarta", ci);
            ASPxMenu1.Items[1].Items[3].Items[4].Text = rm.GetString("MenuItemImportShitjeTollona", ci);
            ASPxMenu1.Items[1].Items[3].Items[5].Text = rm.GetString("MenuItemImportTollonaLeter", ci);
            ASPxMenu1.Items[1].Items[3].Items[6].Text = rm.GetString("MenuItemImportTollonaElekronike", ci);
            ASPxMenu1.Items[1].Items[3].Items[7].Text = rm.GetString("MenuItemImportShitjeTollonaSpecifik", ci);
            ASPxMenu1.Items[1].Items[3].Items[8].Text = rm.GetString("MenuItemImportFleteKontabel", ci);
            ASPxMenu1.Items[1].Items[3].Items[9].Text = rm.GetString("MenuItemTransferimDalje", ci);
            ASPxMenu1.Items[1].Items[3].Items[10].Text = rm.GetString("MenuItemTransfoNeISKSH", ci);
            ASPxMenu1.Items[1].Items[4].Text = rm.GetString("MenuItemElementePerIntegrim", ci);
            ASPxMenu1.Items[1].Items[5].Text = rm.GetString("MenuItemInstrumenta", ci);
            ASPxMenu1.Items[1].Items[5].Items[0].Text = rm.GetString("MenuItemFormatiNumrave", ci);
            ASPxMenu1.Items[1].Items[5].Items[1].Text = rm.GetString("MenuItemFormatePrintimi", ci);
            ASPxMenu1.Items[1].Items[5].Items[2].Text = rm.GetString("MenuItemFushatShtese", ci);
            ASPxMenu1.Items[1].Items[5].Items[3].Text = rm.GetString("menuItemGjeneroKodbar", ci);
            ASPxMenu1.Items[1].Items[5].Items[4].Text = rm.GetString("MenuItemInfo", ci);
            ASPxMenu1.Items[1].Items[5].Items[5].Text = rm.GetString("MenuItemKonfigurimKasash", ci);
            ASPxMenu1.Items[1].Items[5].Items[6].Text = rm.GetString("MenuItemMonedhat", ci);
            ASPxMenu1.Items[1].Items[5].Items[7].Text = rm.GetString("MenuItemNumratAutomatike", ci);
            ASPxMenu1.Items[1].Items[5].Items[8].Text = rm.GetString("MenuItemQytete", ci);
            ASPxMenu1.Items[1].Items[5].Items[9].Text = rm.GetString("MenuItemTaksat", ci);
            ASPxMenu1.Items[1].Items[5].Items[10].Text = rm.GetString("MenuItemVitet", ci);
            ASPxMenu1.Items[1].Items[5].Items[11].Text = rm.GetString("MenuItemKategoriArkive", ci);
            ASPxMenu1.Items[1].Items[5].Items[12].Text = rm.GetString("MenuItemPostoNeEPaySlip", ci);

            ASPxMenu1.Items[1].Items[6].Text = rm.GetString("MenuItemKategoriShpenzimi", ci);
            ASPxMenu1.Items[1].Items[7].Text = rm.GetString("MenuItemKonfigurimDokumenti", ci);
            ASPxMenu1.Items[1].Items[7].Items[0].Text = rm.GetString("MenuItemKonfigurimCelje", ci);
            ASPxMenu1.Items[1].Items[7].Items[0].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[1].Items[7].Items[0].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[1].Items[7].Items[1].Text = rm.GetString("MenuItemKonfigurimeRegjistrime", ci);
            ASPxMenu1.Items[1].Items[7].Items[1].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[1].Items[7].Items[1].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[1].Items[7].Items[2].Text = rm.GetString("MenuItemKonfigurimeLupa", ci);
            ASPxMenu1.Items[1].Items[7].Items[2].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[1].Items[7].Items[2].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[1].Items[8].Text = rm.GetString("MenuItemQendraKosto", ci);
            ASPxMenu1.Items[1].Items[8].Items[0].Text = rm.GetString("MenuItemLlogariQK", ci);
            ASPxMenu1.Items[1].Items[8].Items[1].Text = rm.GetString("MenuItemKonfigurimeQK", ci);
            ASPxMenu1.Items[1].Items[9].Text = rm.GetString("MenuItemRaporteFinanciare", ci);
            ASPxMenu1.Items[1].Items[9].Items[0].Text = rm.GetString("MenuItemFormatBilanci", ci);
            ASPxMenu1.Items[1].Items[9].Items[1].Text = rm.GetString("MenuItemFormatPASH", ci);
            ASPxMenu1.Items[1].Items[9].Items[2].Text = rm.GetString("MenuItemFormatCashFlow", ci);
            ASPxMenu1.Items[1].Items[9].Items[3].Text = rm.GetString("MenuItemFormatBuxhetore", ci);
            ASPxMenu1.Items[1].Items[9].Items[4].Text = rm.GetString("MenuItemFormatOJF", ci);
            ASPxMenu1.Items[1].Items[9].Items[4].Items[0].Text = rm.GetString("MenuItemFormatBilanciOFJ", ci);
            ASPxMenu1.Items[1].Items[9].Items[4].Items[1].Text = rm.GetString("MenuItemFormatPASHOJF", ci);
            ASPxMenu1.Items[1].Items[9].Items[4].Items[2].Text = rm.GetString("MenuItemFormatCashFlowOJF", ci);

            ASPxMenu1.Items[1].Items[10].Text = rm.GetString("MenuItemSkedulimiPunonjesve", ci);
            ASPxMenu1.Items[1].Items[10].Items[0].Text = rm.GetString("MenuItemKonfigurimListeOrare", ci);
            ASPxMenu1.Items[1].Items[10].Items[1].Text = rm.GetString("MenuItemKalendariFestave", ci);
            ASPxMenu1.Items[1].Items[10].Items[2].Text = rm.GetString("MenuItemLegjendaListOrareve", ci);
            ASPxMenu1.Items[1].Items[10].Items[3].Text = rm.GetString("MenuItemProfesioneDhePozicione", ci);
            ASPxMenu1.Items[1].Items[10].Items[4].Text = rm.GetString("MenuItemVendodhjet", ci);
            ASPxMenu1.Items[1].Items[10].Items[5].Text = rm.GetString("MenuItemLokaleGlobale", ci);
            ASPxMenu1.Items[1].Items[10].Items[6].Text = rm.GetString("MenuItemKodeProfesione", ci);

            ASPxMenu1.Items[1].Items[11].Text = rm.GetString("MenuItemStrukturatLlogarive", ci);
            ASPxMenu1.Items[1].Items[12].Text = rm.GetString("MenuItemKonfigurimUrdherPagesa", ci);
            ASPxMenu1.Items[1].Items[12].Items[0].Text = rm.GetString("MenuItemGrupe", ci);
            ASPxMenu1.Items[1].Items[12].Items[1].Text = rm.GetString("MenuItemKodProgrami", ci);
            ASPxMenu1.Items[1].Items[12].Items[2].Text = rm.GetString("MenuItemKapituj", ci);
            ASPxMenu1.Items[1].Items[13].Text = rm.GetString("MenuItemZbritjeAnalitike", ci);
            ASPxMenu1.Items[1].Items[13].Items[0].Text = rm.GetString("MenuItemNivelZbritje", ci);
            ASPxMenu1.Items[1].Items[13].Items[1].Text = rm.GetString("MenuItemPercaktimZbritje", ci);
            ASPxMenu1.Items[1].Items[14].Text = rm.GetString("MenuItemGISKonfigurime", ci);
            ASPxMenu1.Items[1].Items[14].Items[0].Text = rm.GetString("MenuItemGISKonfiguroWS", ci);


            ASPxMenu1.Items[2].Text = rm.GetString("MenuItemCelje", ci);
            ASPxMenu1.Items[2].Items[0].Text = rm.GetString("MenuItemAgjentetShitjes", ci);
            ASPxMenu1.Items[2].Items[1].Text = rm.GetString("MenuItemArkaBanka", ci);
            ASPxMenu1.Items[2].Items[1].Items[0].Text = rm.GetString("MenuItemCeljaArkave", ci);
            ASPxMenu1.Items[2].Items[1].Items[1].Text = rm.GetString("MenuItemCeljaBankave", ci);
            ASPxMenu1.Items[2].Items[2].Text = rm.GetString("MenuItemArtikujt", ci);
            ASPxMenu1.Items[2].Items[2].Items[0].Text = rm.GetString("MenuItemArtikujt", ci);
            ASPxMenu1.Items[2].Items[2].Items[1].Text = rm.GetString("MenuItemArtikujtAfatgjate", ci);
            ASPxMenu1.Items[2].Items[2].Items[2].Text = rm.GetString("MenuItemAtributeTeArtikujve", ci);
            ASPxMenu1.Items[2].Items[2].Items[2].Items[0].Text = rm.GetString("MenuItemNjesiteMatese", ci);
            ASPxMenu1.Items[2].Items[2].Items[2].Items[1].Text = rm.GetString("MenuItemGrupetArtikujve", ci);
            ASPxMenu1.Items[2].Items[2].Items[2].Items[2].Text = rm.GetString("MenuItemGrupetArtikujveAfatgjate", ci);
            ASPxMenu1.Items[2].Items[2].Items[2].Items[3].Text = rm.GetString("MenuItemDatajime", ci);
            ASPxMenu1.Items[2].Items[2].Items[2].Items[4].Text = rm.GetString("MenuItemSerialeUnike", ci);
            ASPxMenu1.Items[2].Items[2].Items[2].Items[5].Text = rm.GetString("MenuItemKategoriSeriali", ci);
            ASPxMenu1.Items[2].Items[2].Items[2].Items[6].Text = rm.GetString("MenuItemFormatSeriali", ci);
            ASPxMenu1.Items[2].Items[3].Text = rm.GetString("MenuItemAutomjetet", ci);

            ASPxMenu1.Items[2].Items[4].Text = rm.GetString("MenuItem_Buxheti", ci);
            ASPxMenu1.Items[2].Items[4].Items[0].Text = rm.GetString("MenuItem_01_KategoriBuxhetimi", ci);
            ASPxMenu1.Items[2].Items[4].Items[1].Text = rm.GetString("MenuItem_KomponenteBuxheti", ci);
            ASPxMenu1.Items[2].Items[4].Items[2].Text = rm.GetString("MenuItemHedhjaTeDhenave", ci);

            ASPxMenu1.Items[2].Items[5].Text = rm.GetString("MenuItemElementePage", ci);
            ASPxMenu1.Items[2].Items[5].Items[0].Text = rm.GetString("MenuItemStrukturaPage", ci);
            ASPxMenu1.Items[2].Items[5].Items[0].Items[0].Text = rm.GetString("MenuItemKategoriPage", ci);
            ASPxMenu1.Items[2].Items[5].Items[0].Items[1].Text = rm.GetString("MenuItemShtesaPage", ci);
            ASPxMenu1.Items[2].Items[5].Items[0].Items[2].Text = rm.GetString("MenuItemSigurimeSuplementare", ci);
            ASPxMenu1.Items[2].Items[5].Items[1].Text = rm.GetString("MenuItemKomponenteListepagese", ci);
            ASPxMenu1.Items[2].Items[5].Items[2].Text = rm.GetString("MenuItemKomponentePage", ci);
            ASPxMenu1.Items[2].Items[5].Items[3].Text = rm.GetString("MenuItemSigurimet", ci);
            ASPxMenu1.Items[2].Items[5].Items[4].Text = rm.GetString("MenuItemTatimeMbiPagen", ci);
            ASPxMenu1.Items[2].Items[6].Text = rm.GetString("MenuItemElementeProdhimi", ci);
            ASPxMenu1.Items[2].Items[6].Items[1].Text = rm.GetString("MenuItemBurimet", ci);
            ASPxMenu1.Items[2].Items[6].Items[0].Text = rm.GetString("MenuItemAktivitetet", ci);

            ASPxMenu1.Items[2].Items[7].Text = rm.GetString("MenuItemKartaKlienti", ci);
            ASPxMenu1.Items[2].Items[7].Items[0].Text = rm.GetString("MenuItemKarteKlient", ci);
            ASPxMenu1.Items[2].Items[7].Items[1].Text = rm.GetString("MenuItemPolitikeKartaKlienti", ci);
            ASPxMenu1.Items[2].Items[7].Items[2].Text = rm.GetString("MenuItemKartaShperndaDhurata", ci);
            ASPxMenu1.Items[2].Items[8].Text = rm.GetString("MenuItemKlientFurnitor", ci);
            ASPxMenu1.Items[2].Items[8].Items[0].Text = rm.GetString("MenuItemFurnitoret", ci);
            ASPxMenu1.Items[2].Items[8].Items[1].Text = rm.GetString("MenuItemKlientet", ci);
            ASPxMenu1.Items[2].Items[8].Items[2].Text = rm.GetString("MenuItemAtributePerKlientFurnitor", ci);
            ASPxMenu1.Items[2].Items[8].Items[2].Items[0].Text = rm.GetString("MenuItemAfatetEMaturimit", ci);
            ASPxMenu1.Items[2].Items[8].Items[2].Items[1].Text = rm.GetString("MenuItemKategoriteZbritjeve", ci);
            ASPxMenu1.Items[2].Items[8].Items[2].Items[2].Text = rm.GetString("MenuItemLlojeteTransportit", ci);
            ASPxMenu1.Items[2].Items[8].Items[2].Items[3].Text = rm.GetString("MenuItemKushtDergimi", ci);
            ASPxMenu1.Items[2].Items[8].Items[2].Items[4].Text = rm.GetString("MenuItemKushtPagese", ci);
            ASPxMenu1.Items[2].Items[8].Items[2].Items[5].Text = rm.GetString("MenuItemGrupetKlienteve", ci);
            ASPxMenu1.Items[2].Items[8].Items[2].Items[6].Text = rm.GetString("MenuItemGrupetFurnitoreve", ci);

            ASPxMenu1.Items[2].Items[9].Text = rm.GetString("MenuItemLlogarite", ci);
            ASPxMenu1.Items[2].Items[10].Text = rm.GetString("MenuItemLlojDefekti", ci);
            ASPxMenu1.Items[2].Items[11].Text = rm.GetString("MenuItemMakro", ci);
            ASPxMenu1.Items[2].Items[12].Text = rm.GetString("MenuItemNjesiAdministrative", ci);
            ASPxMenu1.Items[2].Items[12].Items[0].Text = rm.GetString("MenuItemPikatShitjeve", ci);
            ASPxMenu1.Items[2].Items[12].Items[1].Text = rm.GetString("MenuItemPikatFurnizimit", ci);
            ASPxMenu1.Items[2].Items[12].Items[2].Text = rm.GetString("MenuItemMagazinat", ci);
            ASPxMenu1.Items[2].Items[12].Items[3].Text = rm.GetString("MenuItemNjesiVartese", ci);
            ASPxMenu1.Items[2].Items[12].Items[4].Text = rm.GetString("MenuItemNjesiProdhimi", ci);
            ASPxMenu1.Items[2].Items[13].Text = rm.GetString("MenuItemPunonjes", ci);

            ASPxMenu1.Items[2].Items[14].Text = rm.GetString("MenuItemKomponenteQendraKosto", ci);
            ASPxMenu1.Items[2].Items[14].Items[0].Text = rm.GetString("MenuItemQendraKostoShto", ci);
            ASPxMenu1.Items[2].Items[14].Items[1].Text = rm.GetString("MenuItemObjektivaKosto", ci);
            ASPxMenu1.Items[2].Items[14].Items[2].Text = rm.GetString("MenuItemSkemaQendraKosto", ci);
            ASPxMenu1.Items[2].Items[15].Text = rm.GetString("MenuItemStatusRiparimi", ci);
            ASPxMenu1.Items[2].Items[16].Text = rm.GetString("MenuItemTransportues", ci);

            


            ASPxMenu1.Items[3].Text = rm.GetString("MenuItemRegjistrime", ci);
            
            ASPxMenu1.Items[3].Items[0].Text = rm.GetString("MenuItemAmortizimi", ci);
            ASPxMenu1.Items[3].Items[0].Items[0].Text = rm.GetString("MenuItemRegjistrimAmortizimi", ci);
            ASPxMenu1.Items[3].Items[0].Items[0].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[0].Items[0].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[0].Items[1].Text = rm.GetString("MenuItemRivleresimAmortizimi", ci);
            ASPxMenu1.Items[3].Items[0].Items[1].Items[0].Text = rm.GetString("MenuItemAmortizimiFillestar", ci);
            ASPxMenu1.Items[3].Items[0].Items[1].Items[0].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[0].Items[1].Items[0].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[0].Items[1].Items[1].Text = rm.GetString("MenuItemRivleresim", ci);
            ASPxMenu1.Items[3].Items[0].Items[1].Items[1].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[0].Items[1].Items[1].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[0].Items[2].Text = rm.GetString("MenuItemRillogaritjeAmortizimi", ci);
            ASPxMenu1.Items[3].Items[1].Text = rm.GetString("MenuItemAprovime", ci);
            ASPxMenu1.Items[3].Items[1].Items[0].Text = rm.GetString("MenuItemAprovimet", ci);
            ASPxMenu1.Items[3].Items[1].Items[1].Text = rm.GetString("MenuItemKerkesePerAprovim", ci);
            
            ASPxMenu1.Items[3].Items[2].Text = rm.GetString("MenuItemFaturatBlerjeve", ci);
            ASPxMenu1.Items[3].Items[2].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[2].Items[1].Text = rm.GetString("MenuItemERe", ci);

            ASPxMenu1.Items[3].Items[2].Items[2].Text = "Fatura Blerje Einvoice";
            ASPxMenu1.Items[3].Items[2].Items[2].Visible = true;


            ASPxMenu1.Items[3].Items[3].Text = rm.GetString("MenuItem_EkzekutimBuxheti", ci);
            ASPxMenu1.Items[3].Items[3].Items[0].Text = rm.GetString("MenuItem_PlanifikimEkzekutimBuxheti", ci);
            ASPxMenu1.Items[3].Items[3].Items[0].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[3].Items[0].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[3].Items[1].Text = rm.GetString("MenuItem_EkzekutimBuxheti", ci);
            ASPxMenu1.Items[3].Items[3].Items[1].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[3].Items[1].Items[1].Text = rm.GetString("MenuItemERe", ci);

            ASPxMenu1.Items[3].Items[4].Text = rm.GetString("MenuItemFleteDoganore", ci);
            ASPxMenu1.Items[3].Items[4].Items[0].Text = rm.GetString("MenuItemImport", ci);
            ASPxMenu1.Items[3].Items[4].Items[0].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[4].Items[0].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[4].Items[1].Text = rm.GetString("MenuItemEksport", ci);
            ASPxMenu1.Items[3].Items[4].Items[1].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[4].Items[1].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[5].Text = rm.GetString("MenuItemFleteKontabel", ci);
            ASPxMenu1.Items[3].Items[5].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[5].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[5].Items[2].Text = rm.GetString("AmbjentKontabilizimi", ci);

            ASPxMenu1.Items[3].Items[6].Text = rm.GetString("MenuItemLidhjaDokumentave", ci);
            ASPxMenu1.Items[3].Items[6].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[6].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[7].Text = rm.GetString("MenuItemListPagesa", ci);
            ASPxMenu1.Items[3].Items[7].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[7].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[8].Text = rm.GetString("filterMagazina", ci);
            ASPxMenu1.Items[3].Items[8].Items[0].Text = rm.GetString("NavBarItemDokumentatHyrjeve", ci);
            ASPxMenu1.Items[3].Items[8].Items[0].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[8].Items[0].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[8].Items[1].Text = rm.GetString("NavBarItemDokumentatDaljeve", ci);
            ASPxMenu1.Items[3].Items[8].Items[1].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[8].Items[1].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[8].Items[2].Text = rm.GetString("MenuItemNdryshimCmimi", ci);
            ASPxMenu1.Items[3].Items[8].Items[3].Text = rm.GetString("NavBarItemInventarizimASH", ci);
            ASPxMenu1.Items[3].Items[8].Items[3].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[8].Items[3].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[8].Items[4].Text = rm.GetString("NavBarItemInventarizimAGJ", ci);
            ASPxMenu1.Items[3].Items[8].Items[4].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[8].Items[4].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[8].Items[5].Text = rm.GetString("MenuItemNdryshimCmimSasi", ci);
            ASPxMenu1.Items[3].Items[8].Items[5].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[8].Items[5].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[8].Items[6].Text = rm.GetString("MenuItemRivleresimInventarit", ci);

            ASPxMenu1.Items[3].Items[9].Text = rm.GetString("MenuItemProdhimi", ci);
            ASPxMenu1.Items[3].Items[9].Items[0].Text = rm.GetString("MenuItemPlanifikimiProdhimit", ci);
            ASPxMenu1.Items[3].Items[9].Items[0].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[9].Items[0].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[9].Items[1].Text = rm.GetString("MenuItemSkedulimProdhimit", ci);
            ASPxMenu1.Items[3].Items[9].Items[1].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[9].Items[1].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[9].Items[2].Text = rm.GetString("MenuItemEkzekutimiProdhimit", ci);
            ASPxMenu1.Items[3].Items[9].Items[2].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[9].Items[2].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[9].Items[3].Text = rm.GetString("MenuItemGjeneroProjektinProdhimit", ci);
            ASPxMenu1.Items[3].Items[10].Text = rm.GetString("MenuItemQendraKosto", ci);
            ASPxMenu1.Items[3].Items[10].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[10].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[11].Text = rm.GetString("MenuItemRecetaOptike", ci);
            ASPxMenu1.Items[3].Items[11].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[11].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[12].Text = rm.GetString("MenuItemRegjistrimRiparimi", ci);
            ASPxMenu1.Items[3].Items[12].Items[0].Text = rm.GetString("MenuItemStatusiCelMeProbleme", ci);
            ASPxMenu1.Items[3].Items[12].Items[1].Text = rm.GetString("MenuItemRiparimiAparateveTePrishura", ci);
            ASPxMenu1.Items[3].Items[13].Text = rm.GetString("MenuItemRezervime", ci);
            ASPxMenu1.Items[3].Items[13].Items[0].Text = rm.GetString("NavBarItemDokumentatHyrjeve", ci);
            ASPxMenu1.Items[3].Items[13].Items[0].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[13].Items[0].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[13].Items[1].Text = rm.GetString("NavBarItemDokumentatDaljeve", ci);
            ASPxMenu1.Items[3].Items[13].Items[1].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[13].Items[1].Items[1].Text = rm.GetString("MenuItemERe", ci);

            ASPxMenu1.Items[3].Items[14].Text = rm.GetString("MenuItem_SigurimBuxheti", ci);
            ASPxMenu1.Items[3].Items[14].Items[0].Text = rm.GetString("MenuItem_03_PlanifikimBuxheti", ci);
            ASPxMenu1.Items[3].Items[14].Items[0].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[14].Items[0].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[14].Items[1].Text = rm.GetString("MenuItem_02_MiratimBuxheti", ci);
            ASPxMenu1.Items[3].Items[14].Items[1].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[14].Items[1].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[14].Items[2].Text = rm.GetString("MenuItem_04_AlokimBuxheti", ci);
            ASPxMenu1.Items[3].Items[14].Items[2].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[14].Items[2].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[14].Items[3].Text = rm.GetString("MenuItem_RialokimBuxheti", ci);
            ASPxMenu1.Items[3].Items[14].Items[3].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[14].Items[3].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[14].Items[4].Text = rm.GetString("MenuItem_PerfitimBuxheti", ci);
            ASPxMenu1.Items[3].Items[14].Items[4].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[14].Items[4].Items[1].Text = rm.GetString("MenuItemERe", ci);

            ASPxMenu1.Items[3].Items[15].Text = rm.GetString("MenuItemFaturatShitjeve", ci);
            ASPxMenu1.Items[3].Items[15].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[15].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[15].Items[2].Text = rm.GetString("MenuItemRaportShitjetEinvoice", ci);
            
            ASPxMenu1.Items[3].Items[15].Items[2].Visible = true;

            ASPxMenu1.Items[3].Items[15].Items[3].Text = rm.GetString("menuItemFaturaShitjeGjeneroFaturePermbledhese", ci);
            ASPxMenu1.Items[3].Items[15].Items[4].Text = rm.GetString("menuItemFaturaShitjeRuajteAutomatikeDokumentave", ci);
            ASPxMenu1.Items[3].Items[15].Items[5].Text = rm.GetString("MenuItemDiscountDevice", ci);
            ASPxMenu1.Items[3].Items[15].Items[5].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[15].Items[5].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[15].Items[6].Text = rm.GetString("MenuItemBazaar", ci);
            ASPxMenu1.Items[3].Items[15].Items[6].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[15].Items[6].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[15].Items[7].Text = rm.GetString("PromocioniPlus", ci);



            ASPxMenu1.Items[3].Items[16].Text = rm.GetString("MenuItemShperndarjaShpenzimeve", ci);
            ASPxMenu1.Items[3].Items[16].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[16].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[17].Text = rm.GetString("MenuItemUrdherPagesa", ci);
            ASPxMenu1.Items[3].Items[17].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[17].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[18].Text = rm.GetString("MenuItemVeprimeArkaBanka", ci);
            ASPxMenu1.Items[3].Items[18].Items[0].Text = rm.GetString("MenuItemVeprimeArka", ci);
            ASPxMenu1.Items[3].Items[18].Items[0].Items[0].Text = rm.GetString("MenuItemArketimet", ci);
            ASPxMenu1.Items[3].Items[18].Items[0].Items[0].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[18].Items[0].Items[0].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[18].Items[0].Items[1].Text = rm.GetString("MenuItemPagesat", ci);
            ASPxMenu1.Items[3].Items[18].Items[0].Items[1].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[18].Items[0].Items[1].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[18].Items[1].Text = rm.GetString("MenuItemVeprimeBanka", ci);
            ASPxMenu1.Items[3].Items[18].Items[1].Items[0].Text = rm.GetString("MenuItemDerdhjetBankare", ci);
            ASPxMenu1.Items[3].Items[18].Items[1].Items[0].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[18].Items[1].Items[0].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[18].Items[1].Items[1].Text = rm.GetString("MenuItemTerheqjetBankare", ci);
            ASPxMenu1.Items[3].Items[18].Items[1].Items[1].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[18].Items[1].Items[1].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[19].Text = rm.GetString("MenuItemVeprimeMeKlientFurnitor", ci);
            ASPxMenu1.Items[3].Items[19].Items[0].Text = rm.GetString("MenuItemVeprimeKlientFurnitor", ci);
            ASPxMenu1.Items[3].Items[19].Items[0].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[19].Items[0].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[19].Items[1].Text = rm.GetString("MenuItemAzhornimeKlientFurnitor", ci);
            ASPxMenu1.Items[3].Items[19].Items[1].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[19].Items[1].Items[1].Text = rm.GetString("MenuItemERe", ci);
            ASPxMenu1.Items[3].Items[19].Items[2].Text = rm.GetString("MenuItemMbylljeKlientFurnitor", ci);
            ASPxMenu1.Items[3].Items[19].Items[2].Items[0].Text = rm.GetString("MenuItemLista", ci);
            ASPxMenu1.Items[3].Items[19].Items[2].Items[1].Text = rm.GetString("MenuItemERe", ci);
            
         
          

            ASPxMenu1.Items[4].Text = rm.GetString("MenuItemRaportet", ci);
            ASPxMenu1.Items[4].Items[0].Text = rm.GetString("MenuItemAmortizimi", ci);
            ASPxMenu1.Items[4].Items[1].Text = rm.GetString("MenuItemRaportArka", ci);
            ASPxMenu1.Items[4].Items[2].Text = rm.GetString("MenuItemRaportBanka", ci);
            ASPxMenu1.Items[4].Items[3].Text = rm.GetString("MenuItemRaportBussinesIntelligence", ci);
            ASPxMenu1.Items[4].Items[4].Text = rm.GetString("MenuItemRaportBlerjet", ci);
            ASPxMenu1.Items[4].Items[5].Text = rm.GetString("MenuItemRaportFaturatEBlrejes", ci);
            ASPxMenu1.Items[4].Items[5].Visible = true; 
                
            ASPxMenu1.Items[4].Items[6].Text = rm.GetString("MenuItemRaportBurimetNjerezore", ci);
            ASPxMenu1.Items[4].Items[7].Text = rm.GetString("MenuItemBuxheti", ci);
            ASPxMenu1.Items[4].Items[8].Text = rm.GetString("MenuItemRaportInventari", ci);
            ASPxMenu1.Items[4].Items[9].Text = rm.GetString("MenuItemRaportKlientetdheFurnitoret", ci);
            ASPxMenu1.Items[4].Items[10].Text = rm.GetString("MenuItemRaportKontabiliteti", ci);
            ASPxMenu1.Items[4].Items[11].Text = rm.GetString("MenuItemRaporteMenaxheriale", ci);
            ASPxMenu1.Items[4].Items[12].Text = rm.GetString("MenuItemRaportProdhimi", ci);
            ASPxMenu1.Items[4].Items[13].Text = rm.GetString("MenuItemRaportCRM", ci);
            ASPxMenu1.Items[4].Items[14].Text = rm.GetString("MenuItemRaportQendratKostos", ci);
            ASPxMenu1.Items[4].Items[15].Text = rm.GetString("MenuItemRaportShitjet", ci);
            ASPxMenu1.Items[4].Items[16].Text = rm.GetString("MenuItemRaportShitjetEinvoice", ci);
            ASPxMenu1.Items[4].Items[16].Visible = true;
            ASPxMenu1.Items[4].Items[17].Text = rm.GetString("MenuItemRaportTollonash", ci);
            ASPxMenu1.Items[4].Items[18].Text = rm.GetString("MenuItemRaportTollonashKastrati", ci);
            ASPxMenu1.Items[4].Items[19].Text = rm.GetString("MenuItemRaportiGjendjaEMagazines", ci);
            ASPxMenu1.Items[4].Items[20].Text = rm.GetString("MenuItemRaportGjendjaEArtikujveMeSeriale", ci);
            ASPxMenu1.Items[4].Items[21].Text = rm.GetString("MenuItemRaportGjendjaArtikujveIMEI", ci);
            ASPxMenu1.Items[4].Items[22].Text = rm.GetString("MenuItemRaportGjendjaArtikujveIMEIEkspozitor", ci);


            ASPxMenu1.Items[5].Text = rm.GetString("MenuItemHelp", ci);
            ASPxMenu1.Items[5].Items[0].Text = rm.GetString("MenuItemManualiPerdoruesit", ci);
            ASPxMenu1.Items[5].Items[1].Text = rm.GetString("MenuItemProgramKase", ci);
            ASPxMenu1.Items[5].Items[2].Text = rm.GetString("MenuItemProgramKaseNew", ci);
            ASPxMenu1.Items[5].Items[3].Text = rm.GetString("MenuItemRemoteSupport", ci);
            ASPxMenu1.Items[5].Items[4].Text = rm.GetString("MenuItemVersioni", ci) + " " + versioni;
            DevExpress.Web.MenuItem ikonaImazhPerdoruesMenuLart = ASPxMenu1.Items.FindByName("ikonaImazhPerdorues");
            ikonaImazhPerdoruesMenuLart.Visible = true;
            var perdoruesEmerItem = ikonaImazhPerdoruesMenuLart.Items.FindByName("LupaPersonalizoPerdorues.aspx");
            perdoruesEmerItem.Visible = true;
            perdoruesEmerItem.Text = DbCore.mySessionObjects.kthePerdorues(Session).PerdoruesUsername;
            var mesazheItem = ikonaImazhPerdoruesMenuLart.Items.FindByName("mesazhe");
            mesazheItem.Visible = true;
            mesazheItem.Text = rm.GetString("labelmesazh", ci);
            var daljeItem = ikonaImazhPerdoruesMenuLart.Items.FindByName("dalje");
            daljeItem.Visible = true;
            daljeItem.Text = rm.GetString("labelLogOut", ci);
            daljeItem.NavigateUrl = $"{DbCore.IMBUtils.Paths.defaultLoginPath}?arsye=logout&google=true";
            //ikonaImazhPerdoruesMenuLart.Items.FindByName("mesazhe").ClientVisible = true;
            var fjalekalimItem = ikonaImazhPerdoruesMenuLart.Items.FindByName("NdryshimFjalekalimi.aspx");
            fjalekalimItem.Text = rm.GetString("labelEmailFjalekalimi", ci);
            var abonimItem = ikonaImazhPerdoruesMenuLart.Items.FindByName("abonimi");
            abonimItem.Visible = true;

            //ASPxNavBar1.Groups[0].Text = rm.GetString("MenuItemAdminstrimi", ci);


            //ASPxNavBar1.Groups[0].Items[0].Text = rm.GetString("MenuItemPerdoruesit", ci);
            //ASPxNavBar1.Groups[0].Items[1].Text = rm.GetString("MenuItemNdryshimFjalekalimi", ci);
            //ASPxNavBar1.Groups[0].Items[2].Text = rm.GetString("MenuItemRolet", ci);






            ASPxNavBar1.Groups.FindByName("settings").Visible = true;

            ASPxNavBar1.Groups[0].Text = rm.GetString("MenuItemAdminstrimi", ci);
            ASPxNavBar1.Groups[0].Items[0].Text = rm.GetString("MenuItemRolet", ci);
            ASPxNavBar1.Groups[0].Items[1].Text = rm.GetString("MenuItemPerdoruesit", ci);
            ASPxNavBar1.Groups[0].Items[2].Text = rm.GetString("MenuItemNdermarrjet", ci);

            ASPxNavBar1.Groups[1].Text = rm.GetString("MenuItemRaportKontabiliteti", ci);
            ASPxNavBar1.Groups[1].Items[0].Text = rm.GetString("MenuItemStrukturatLlogarive", ci);
            ASPxNavBar1.Groups[1].Items[1].Text = rm.GetString("MenuItemLlogarite", ci);
            ASPxNavBar1.Groups[1].Items[2].Text = rm.GetString("NavBarItemAmbjentiKontabilizimit", ci);

            ASPxNavBar1.Groups[2].Text = rm.GetString("MenuItemRaportInventari", ci);
            ASPxNavBar1.Groups[2].Items[0].Text = rm.GetString("MenuItemArtikujt", ci);
            ASPxNavBar1.Groups[2].Items[1].Text = rm.GetString("NavBarItemDokumentatHyrjeve", ci);
            ASPxNavBar1.Groups[2].Items[2].Text = rm.GetString("MenuItemRegjistrimetHyrjeve", ci);
            ASPxNavBar1.Groups[2].Items[3].Text = rm.GetString("NavBarItemDokumentatDaljeve", ci);
            ASPxNavBar1.Groups[2].Items[4].Text = rm.GetString("MenuItemRegjistrimetDaljeve", ci);

            ASPxNavBar1.Groups[3].Text = rm.GetString("NavBarItemBlerjetDheShitjet", ci);
            ASPxNavBar1.Groups[3].Items[0].Text = rm.GetString("MenuItemCmimetShitjeve", ci);
            ASPxNavBar1.Groups[3].Items[1].Text = rm.GetString("MenuItemCmimetBlerje", ci);
            ASPxNavBar1.Groups[3].Items[2].Text = rm.GetString("MenuItemKlientet", ci);
            ASPxNavBar1.Groups[3].Items[3].Text = rm.GetString("MenuItemFurnitoret", ci);
            ASPxNavBar1.Groups[3].Items[4].Text = rm.GetString("MenuItemFaturatBlerjeve", ci);
            ASPxNavBar1.Groups[3].Items[5].Text = rm.GetString("NavBarItemRegjistrimetBlerjeve", ci);
            ASPxNavBar1.Groups[3].Items[6].Text = rm.GetString("MenuItemFaturatShitjeve", ci);
            ASPxNavBar1.Groups[3].Items[7].Text = rm.GetString("NavBarItemRegjistrimetShitjeve", ci);
            ASPxNavBar1.Groups[3].Items[8].Text = rm.GetString("MenuItemDiscountDevice", ci);
            ASPxNavBar1.Groups[3].Items[9].Text = rm.GetString("MenuItemRegjistrimeDiscountDevice", ci);
            ASPxNavBar1.Groups[3].Items[10].Text = rm.GetString("MenuItemBazaar", ci);
            ASPxNavBar1.Groups[3].Items[11].Text = rm.GetString("MenuItemRegjistrimeBazaar", ci);
            ASPxNavBar1.Groups[3].Items[12].Text = "Fatura Blerje Einvoice";
            ASPxNavBar1.Groups[3].Items[12].Visible = true;


            ASPxNavBar1.Groups[4].Text = rm.GetString("MenuItemRaportArkadheBanka", ci);
            ASPxNavBar1.Groups[4].Items[0].Text = rm.GetString("MenuItemDerdhjetBankare", ci);
            ASPxNavBar1.Groups[4].Items[1].Text = rm.GetString("NavBarItemRegjistrimiDerdhjeve", ci);
            ASPxNavBar1.Groups[4].Items[2].Text = rm.GetString("MenuItemTerheqjetBankare", ci);
            ASPxNavBar1.Groups[4].Items[3].Text = rm.GetString("NavBarItemRegjistrimiTerheqjeve", ci);
            ASPxNavBar1.Groups[4].Items[4].Text = rm.GetString("MenuItemArketimet", ci);
            ASPxNavBar1.Groups[4].Items[5].Text = rm.GetString("NavBarItemRegjistrimiArketimeve", ci);
            ASPxNavBar1.Groups[4].Items[6].Text = rm.GetString("MenuItemPagesat", ci);
            ASPxNavBar1.Groups[4].Items[7].Text = rm.GetString("NavBarItemRegjistrimiPagesave", ci);




            ASPxNavBar1.Groups[5].Text = rm.GetString("MenuItemRaportBurimetNjerezore", ci);
            ASPxNavBar1.Groups[5].Items[0].Text = rm.GetString("MenuItemDepartamentet", ci);
            ASPxNavBar1.Groups[5].Items[1].Text = rm.GetString("MenuItemKomponenteListepagese", ci);
            ASPxNavBar1.Groups[5].Items[2].Text = rm.GetString("MenuItemPunonjes", ci);
            ASPxNavBar1.Groups[5].Items[3].Text = rm.GetString("MenuItemListPagesa", ci);
            //urdher pagesa
            ASPxNavBar1.Groups[6].Text = rm.GetString("MenuItemKonfigurimUrdherPagesa", ci);
            ASPxNavBar1.Groups[6].Items[0].Text = rm.GetString("MenuItemGrupe", ci);
            ASPxNavBar1.Groups[6].Items[1].Text = rm.GetString("MenuItemTituj", ci);
            ASPxNavBar1.Groups[6].Items[2].Text = rm.GetString("MenuItemKapituj", ci);
            ASPxNavBar1.Groups[6].Items[3].Text = rm.GetString("NavBarItemRegjUrdherpagesave", ci);
            ASPxNavBar1.Groups[6].Items[4].Text = rm.GetString("NavBarItemUrdherpagesat", ci);



            ASPxNavBar1.Groups[7].Text = rm.GetString("MenuItemRaportProdhimi", ci);
            ASPxNavBar1.Groups[7].Items[0].Text = rm.GetString("MenuItemPlanifikimiProdhimit", ci);
            ASPxNavBar1.Groups[7].Items[1].Text = rm.GetString("NavBarItemPlanifikimRi", ci);
            ASPxNavBar1.Groups[7].Items[2].Text = rm.GetString("MenuItemEkzekutimiProdhimit", ci);
            ASPxNavBar1.Groups[7].Items[3].Text = rm.GetString("NavBarItemEkzekutimRi", ci);
            ASPxNavBar1.Groups[7].Items[4].Text = rm.GetString("MenuItemGjeneroProjektinProdhimit", ci);

            ASPxNavBar1.Groups[8].Text = rm.GetString("MenuItemQendraKosto", ci);
            ASPxNavBar1.Groups[8].Items[0].Text = rm.GetString("MenuItemKonfigurimeQK", ci);
            ASPxNavBar1.Groups[8].Items[1].Text = rm.GetString("MenuItemQendraKostoShto", ci);
            ASPxNavBar1.Groups[8].Items[2].Text = rm.GetString("MenuItemSkemaQendraKosto", ci);
            ASPxNavBar1.Groups[8].Items[3].Text = rm.GetString("MenuItemRegjistrimQK", ci);
            ASPxNavBar1.Groups[8].Items[4].Text = rm.GetString("MenuItemRegjistrimQKRe", ci);



            ASPxNavBar1.Groups[9].Text = rm.GetString("MenuItemAmortizimi", ci);
            ASPxNavBar1.Groups[9].Items[0].Text = rm.GetString("MenuItemArtikujtAfatgjate", ci);
            ASPxNavBar1.Groups[9].Items[1].Text = rm.GetString("MenuItemAmortizimiFillestar", ci);
            ASPxNavBar1.Groups[9].Items[2].Text = rm.GetString("MenuItemAmortizimiFillestarRi", ci);
            ASPxNavBar1.Groups[9].Items[3].Text = rm.GetString("MenuItemRegjistrimAmortizimi", ci);
            ASPxNavBar1.Groups[9].Items[4].Text = rm.GetString("MenuItemRegjistrimAmortizimiRi", ci);




            ASPxNavBar1.Groups[10].Text = rm.GetString("MenuItemAprovimetDok", ci);
            ASPxNavBar1.Groups[10].Items[0].Text = rm.GetString("MenuItemAprovimet", ci);
            ASPxNavBar1.Groups[10].Items[1].Text = rm.GetString("MenuItemKerkesePerAprovim", ci);

            //Promocioni Plus
            ASPxNavBar1.Groups[11].Text = rm.GetString("PromocioniPlus", ci);
            ASPxNavBar1.Groups[11].Items[0].Text = rm.GetString("PromocioniPlus", ci);

            ASPxNavBar1.Groups[12].Text = rm.GetString("MenuItemRaportet", ci);
            ASPxNavBar1.Groups[12].Items[0].Text = rm.GetString("MenuItemRaportKontabiliteti", ci);
            ASPxNavBar1.Groups[12].Items[1].Text = rm.GetString("MenuItemRaportBlerjet", ci);
            ASPxNavBar1.Groups[12].Items[2].Visible = true;
            ASPxNavBar1.Groups[12].Items[3].Text = rm.GetString("MenuItemRaportShitjet", ci);
            ASPxNavBar1.Groups[12].Items[4].Text = rm.GetString("MenuItemRaportInventari", ci);
            ASPxNavBar1.Groups[12].Items[5].Text = rm.GetString("MenuItemRaportKlientetdheFurnitoret", ci);
            ASPxNavBar1.Groups[12].Items[6].Text = rm.GetString("MenuItemRaportArka", ci);
            ASPxNavBar1.Groups[12].Items[7].Text = rm.GetString("MenuItemRaportBanka", ci);
            ASPxNavBar1.Groups[12].Items[8].Text = rm.GetString("MenuItemRaportBurimetNjerezore", ci);
            ASPxNavBar1.Groups[12].Items[9].Text = rm.GetString("MenuItemRaportProdhimi", ci);
            ASPxNavBar1.Groups[12].Items[10].Text = rm.GetString("MenuItemRaportQendratKostos", ci);
            ASPxNavBar1.Groups[12].Items[11].Text = rm.GetString("MenuItemAmortizimi", ci);
            ASPxNavBar1.Groups[12].Items[12].Text = rm.GetString("MenuItemRaportBussinesIntelligence", ci);
            ASPxNavBar1.Groups[12].Items[13].Text = rm.GetString("MenuItemRaportTollonash", ci);
            ASPxNavBar1.Groups[12].Items[14].Text = rm.GetString("MenuItemRaportTollonashKastrati", ci);
            ASPxNavBar1.Groups[12].Items[15].Text = rm.GetString("MenuItemRaportiGjendjaEMagazines", ci);
            ASPxNavBar1.Groups[12].Items[16].Text = rm.GetString("MenuItemRaportGjendjaEArtikujveMeSeriale", ci);
            ASPxNavBar1.Groups[12].Items[17].Text = rm.GetString("MenuItemRaportGjendjaArtikujveIMEI", ci);
            ASPxNavBar1.Groups[12].Items[18].Text = rm.GetString("MenuItemRaportGjendjaArtikujveIMEIEkspozitor", ci);
            ASPxNavBar1.Groups[12].Items[19].Text = rm.GetString("MenuItemRaporteMenaxheriale", ci);
            ASPxNavBar1.Groups[12].Items[20].Text = rm.GetString("MenuItemRaporteCRM", ci);    // Raportet CRM
            ASPxNavBar1.Groups[12].Items[21].Text = rm.GetString("MenuItemRaportBuxheti", ci);






            ASPxNavBar1.Groups[13].Text = rm.GetString("MenuItemGroupBusinessIntelligence", ci); //"Business Intelligence";
            ASPxNavBar1.Groups[13].Items[0].Text = rm.GetString("labelShitje", ci);
            ASPxNavBar1.Groups[13].Items[1].Text = rm.GetString("lblRaportMagazina", ci);
            ASPxNavBar1.Groups[13].Items[2].Text = rm.GetString("labelBlerje", ci);
            //  ASPxNavBar1.Groups[12].Items[3].Text = rm.GetString("MenuItemRaportKontabiliteti");


            ASPxNavBar1.Groups[14].Text = rm.GetString("MenuGroupHarta", ci);
            ASPxNavBar1.Groups[14].Items[0].Text = rm.GetString("MenuItem_0_Harta", ci);  // Hartat e Njesive Administrative
            ASPxNavBar1.Groups[14].Items[1].Text = rm.GetString("MenuItem_1_Harta", ci);   // Hartat e shitjeve sipas magazinave
            ASPxNavBar1.Groups[14].Items[2].Text = rm.GetString("MenuItem_2_Harta", ci);   // Harta e shitjeve sipas klienteve
            ASPxNavBar1.Groups[14].Items[3].Text = rm.GetString("MenuItem_3_Harta", ci);   // Harta e shitjeve sipas furnitoreve
            ASPxNavBar1.Groups[14].Items[4].Text = rm.GetString("MenuItem_4_Harta_Marzhi", ci);   //   Marzhi i shitjes sipas klienteve
            ASPxNavBar1.Groups[14].Items[5].Text = rm.GetString("MenuItem_5_Harta", ci);   //   Harta e shitjeve sipas pikave te shitjes
            ASPxNavBar1.Groups[14].Items[6].Text = rm.GetString("MenuItem_6_Harta", ci);   //  Harta e gjendjes se magazinave
            ASPxNavBar1.Groups[14].Items[7].Text = rm.GetString("MenuItem_7_Harta", ci);    // Harta e amortizimit te aseteve ne perqindje  
            ASPxNavBar1.Groups[14].Items[8].Text = rm.GetString("MenuItem_HartaEKlienteve", ci);    //Harta e klienteve



            ASPxNavBar1.Groups[15].Text = rm.GetString("MenuItemGrupCRM", ci); //"CRM";
            ASPxNavBar1.Groups[15].Items[0].Text = rm.GetString("MenuItemCRM", ci);
            ASPxNavBar1.Groups[15].Items[1].Text = rm.GetString("CRMRoute", ci);
            ASPxNavBar1.Groups[15].Items[2].Text = rm.GetString("CRMFushaAnkete", ci);
            ASPxNavBar1.Groups[15].Items[3].Text = rm.GetString("CRMAnketa", ci);
            ASPxNavBar1.Groups[15].Items[4].Text = rm.GetString("CRMListeAnketa", ci);
            ASPxNavBar1.Groups[15].Items[5].Text = rm.GetString("CRMLidhAnkete", ci);
            ASPxNavBar1.Groups[15].Items[6].Text = rm.GetString("CRMHistoriku", ci);
            ASPxNavBar1.Groups[15].Items[7].Text = rm.GetString("CRMDetyra", ci);
            ASPxNavBar1.Groups[15].Items[8].Text = rm.GetString("CRMRaporte", ci);

            ASPxNavBar1.Groups[16].Text = rm.GetString("MenuItemGroupGIS", ci);  // "GIS"; 
            ASPxNavBar1.Groups[16].Items[0].Text = rm.GetString("MenuItemGIS", ci);


            ASPxNavBar1.Groups[17].Text = rm.GetString("MenuItemGrupAnalizBuxheti", ci);//"Analiza e Buxhetit";
            ASPxNavBar1.Groups[17].Items[0].Text = rm.GetString("MenuItem_0_AnalizBuzheti", ci); //Konfigurimi i zerave per ambjentet e analizes se buxhetit//
            ASPxNavBar1.Groups[17].Items[1].Text = rm.GetString("MenuItem_3_AnalizBuxheti", ci);  //Regjistrimi i buxhetit permbledhes//
            ASPxNavBar1.Groups[17].Items[2].Text = rm.GetString("MenuItem_4_AnalizBuxheti", ci);   //Regjistrimi i parashikimit te shpenzimeve per personelin//
            ASPxNavBar1.Groups[17].Items[3].Text = rm.GetString("MenuItem_5_AnalizBuxheti", ci);   //Regjistrimi i parashikimit te te ardhurave//
            ASPxNavBar1.Groups[17].Items[4].Text = rm.GetString("MenuItem_6_AnalizBuxheti", ci);   //Regjistrimi i shpenzimeve kapitale//
            ASPxNavBar1.Groups[17].Items[5].Text = rm.GetString("MenuItem_7_AnalizBuxheti", ci);    //Regjistrimi i projektbuxhetit per tre vite
            ASPxNavBar1.Groups[17].Items[6].Text = rm.GetString("MenuItem_8_AnalizBuxheti", ci); //Regjistrimi i planifikimit te produkteve  // 
            ASPxNavBar1.Groups[17].Items[7].Text = rm.GetString("MenuItem_9_AnalizBuxheti", ci);  //Regjistrimi i shpenzimeve operative 
            ASPxNavBar1.Groups[17].Items[8].Text = rm.GetString("MenuItem_12_AnalizBuxheti", ci);  //Regjistrimi i pasqyres organike
            ASPxNavBar1.Groups[17].Items[9].Text = rm.GetString("MenuItem_13_AnalizBuxheti", ci); //Regjistrimi i evidences statistikore
            ASPxNavBar1.Groups[17].Items[10].Text = rm.GetString("MenuItem_31_AnalizBuxheti", ci); //Planifikim dhe realizim
            ASPxNavBar1.Groups[17].Items[11].Text = rm.GetString("MenuItem_28_AnalizBuxheti", ci); //Regjistrim i realizimit te prokurimeve publike
            ASPxNavBar1.Groups[17].Items[12].Text = rm.GetString("MenuItem_30_AnalizBuxheti", ci); //Regjistrim i parashikimit te prokurimeve publike
            ASPxNavBar1.Groups[17].Items[13].Text = rm.GetString("MenuItem_14_AnalizBuxheti", ci);  //Raporti per projekt buxhetin permbledhes
            ASPxNavBar1.Groups[17].Items[14].Text = rm.GetString("MenuItem_15_AnalizBuxheti", ci); //Raporti per parashikimin e te ardhurave
            ASPxNavBar1.Groups[17].Items[15].Text = rm.GetString("MenuItem_16_AnalizBuxheti", ci); //Raporti per parashikimin e shpenzimeve per personelin
            ASPxNavBar1.Groups[17].Items[16].Text = rm.GetString("MenuItem_17_AnalizBuxheti", ci);  //Raporti per projekt buxhetin ne zerin e shpenzimeve operative ne vitet pasardhes
            ASPxNavBar1.Groups[17].Items[17].Text = rm.GetString("MenuItem_18_AnalizBuxheti", ci);  //Raporti per projekt buxhetin ne zerin e shpenzimeve operative ne 3 vitet pasardhese
            ASPxNavBar1.Groups[17].Items[18].Text = rm.GetString("MenuItem_22_AnalizBuxheti", ci);   //Raporti per parashikimin e shpenzimeve kapitale
            ASPxNavBar1.Groups[17].Items[19].Text = rm.GetString("MenuItem_20_AnalizBuxheti", ci);  //Raporti per projekt buxhetin 3 vjecar
            ASPxNavBar1.Groups[17].Items[20].Text = rm.GetString("MenuItem_19_AnalizBuxheti", ci);     //Raporti per shpenzimet operative ne baze mujore
            ASPxNavBar1.Groups[17].Items[21].Text = rm.GetString("MenuItem_21_AnalizBuxheti", ci); //Raporti per planifikimin e produkteve te programit
            ASPxNavBar1.Groups[17].Items[22].Text = rm.GetString("MenuItem_23_AnalizBuxheti", ci);  //Raporti per parashikimin e shpenzimeve per vitin pasardhes(Raportuese)
            ASPxNavBar1.Groups[17].Items[23].Text = rm.GetString("MenuItem_24_AnalizBuxheti", ci);            //Raporti permbledhes per shpenzimet operative
            ASPxNavBar1.Groups[17].Items[24].Text = rm.GetString("MenuItem_27_AnalizBuxheti", ci);  // Raporti i evidences statistikore(Raportuese)
            ASPxNavBar1.Groups[17].Items[25].Text = rm.GetString("MenuItem_25_AnalizBuxheti", ci);      //Raporti i inventarit sipas viteve (Raportuese)
            ASPxNavBar1.Groups[17].Items[26].Text = rm.GetString("MenuItem_26_AnalizBuxheti", ci);     //Raporti i inventarit sipas perdoruesve(Raportuese)
            ASPxNavBar1.Groups[17].Items[27].Text = rm.GetString("MenuItem_29_AnalizBuxheti", ci);     //Regjistri i realizimit te prokurimeve publike
            ASPxNavBar1.Groups[17].Items[28].Text = rm.GetString("MenuItem_32_AnalizBuxheti", ci);     //Tabela permbledhese e planifikimeve dhe realizimeve
            
            //Buxheti
            ASPxNavBar1.Groups[18].Text = rm.GetString("MenuItemBuxheti", ci);
            ASPxNavBar1.Groups[18].Items[0].Text = rm.GetString("MenuItem_01_KategoriBuxhetimi", ci);
            ASPxNavBar1.Groups[18].Items[1].Text = rm.GetString("MenuItem_KomponenteBuxheti", ci);
            ASPxNavBar1.Groups[18].Items[2].Text = rm.GetString("MenuItemHedhjaTeDhenave", ci);
            ASPxNavBar1.Groups[18].Items[3].Text = rm.GetString("MenuItem_03_PlanifikimBuxheti", ci);
            ASPxNavBar1.Groups[18].Items[4].Text = rm.GetString("MenuItem_02_MiratimBuxheti", ci);
            ASPxNavBar1.Groups[18].Items[5].Text = rm.GetString("MenuItem_04_AlokimBuxheti", ci);
            ASPxNavBar1.Groups[18].Items[6].Text = rm.GetString("MenuItem_RialokimBuxheti", ci);
            ASPxNavBar1.Groups[18].Items[7].Text = rm.GetString("MenuItem_PerfitimBuxheti", ci);
            ASPxNavBar1.Groups[18].Items[8].Text = rm.GetString("MenuItem_PlanifikimEkzekutimBuxheti", ci);
            ASPxNavBar1.Groups[18].Items[9].Text = rm.GetString("MenuItem_EkzekutimBuxheti", ci);



            ASPxNavBar1.Groups[20].Text = rm.GetString("MenuItemHelp", ci);
            ASPxNavBar1.Groups[20].Items[0].Text = rm.GetString("MenuItemManualiPerdoruesit", ci);
            ASPxNavBar1.Groups[20].Items[1].Text = rm.GetString("MenuItemRemoteSupport", ci);

            ASPxNavBar1.Groups[21].Text = rm.GetString("MobileMenu", ci);
            //Settings
            ASPxNavBar1.Groups[22].Text = rm.GetString("MenuItemSettings", ci);


            hfState.Set("MenuItemMbyll", rm.GetString("MenuItemMbyll", ci));
            hfState.Set("labelRuajNdryshimet", rm.GetString("labelRuajNdryshimet", ci));
            hfState.Set("labelTitulliModal", rm.GetString("labelTitulliModal", ci));
            hfState.Set("labelMesazhModal", rm.GetString("labelMesazhModal", ci));
            hfState.Set("labelAbonimModal", rm.GetString("labelAbonimModal", ci));
            hfState.Set("MenuLlogaritAbonim", rm.GetString("MenuLlogaritAbonim", ci));


            popupUniversal.HeaderText = rm.GetString("txtZgjidhPeriudhenKontabel", ci);


        }
    }
}
