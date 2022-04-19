using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing;
using DevExpress.Web;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;
using DbCore.DbShare;
using System.Text;
using DbCore.DbAdmin;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Reflection;
using DevExpress.XtraReports.Web;
using System.IO;
using System.Globalization;
using System.Resources;
using DbCore.DbAsete;
using ICSharpCode.SharpZipLib.Zip;
using DbCore;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Logging;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using DbCore.Raporte;
using DocumentFormat.OpenXml.Office2010.Excel;
using PlatinumWeb.ApplicationUtils;

namespace PlatinumWeb.E_PaySlip
{
    public partial class RaportiEPaySlip : MyPageBase
    {
        private string STR_TeGjitha = "";
        private string STR_TeEkzekutuara = "";
        private string STR_TePaekzekutuara = "";
        private string STR_ArtBurimGjitha = " ";
        private string STR_LlojPorosieUPP = " ";
        private string STR_LlojPorosieJOUPP = " ";
        private string STR_Po = " ";
        private string STR_Jo = " ";
        private string Detajim1 = " ";
        private string Detajim2 = " ";
        private string STR_Pjeserisht = " ";
        private string STR_Artikull = " ";
        private string STR_Burim = " ";
        private string STR_GjendjeZero = " ";
        private string STR_GjendjeJoZero = " ";
        private string STR_Teprica = " ";
        private string STR_Mungesa = " ";
        private string STR_Teprice_Mungese = " ";
        private Int32 idRaporti = -1;
        private static string styleNamePrefix = "Style_";
        private static string styleNameDefault = "Default";
        private static string styleNamePostfix = ".repss";
        private Int32 windowWidth;
        //private string textFiltri;
        private string STR_KonvertimiDytePerfunduar = " ";
        private string STR_KonvertimiDyteJoKonvertuar = " ";
        private string STR_KonvertimiDytePjeserisht = " ";
        private string STR_KonvertimiDyteTejkaluar = " ";
        private ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
        private string STR_GjitheDok = "";
        private string STR_DokBrendaAfat = "";
        private string STR_DokJashteAfat = "";
        private string STR_Emertim1 = "";
        private string STR_Emertim2 = "";
        private string dtdoknga, dtdokderi = "";

        private string EmerRealRaporti = string.Empty;


        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (idRaporti < 0)
                idRaporti = DbCore.clsFunksione.ktheIdRaporti(Request);

            clsPeriudhaKontabel periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            if (!DbCore.mySessionObjects.isLogedIn(Session) || periudha == null)
            {
                DbCore.clsFunksione.Logout(Session, true, true, false, DbCore.IMBUtils.Paths.loginPathEpaySlip, "FaqePaautorizuar");
                return;
            }

            pathStyle.Value = Server.MapPath("~/Style Raporte/");
            if (!mySessionObjects.MerrNgaSession<bool>(Session, "EPaySlipPinAuthentification"))
            {
                DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }
            
            dtdoknga = periudha.FillimiPeriudha.ToShortDateString();
            dtdokderi = periudha.MbarimiPeriudha.ToShortDateString();

            // if (!Page.IsCallback)

            windowWidth = DbCore.clsFunksione.merrWindowWidthRequested(Request);
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);

            int idVit = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            int idNderVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            int idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            bool eshteMeme = DbCore.mySessionObjects.merrEshteMemeSesioni(Session);
            clsPeriudhaKontabel periudhaKontabel = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            CultureInfo ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
            bool kaSubRaport = clsRaporti.KaSubRaporte(idRaporti);
            DbCore.DbListPagesat.clsPunonjes perdoruesi = new DbCore.DbListPagesat.clsPunonjes(idPerdoruesi);
            String guidString;
            var raporti = new clsRaporti(idGjuha, idRaporti);
            EmerRealRaporti = raporti.RaportiEmriReal;
            // filtraAvancuar.Visible = false;
            if (!Page.IsPostBack)
            {
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                try
                {
                    DbCore.DbAdmin.clsKomponente oKomponente = new DbCore.DbAdmin.clsKomponente("Raporti.aspx");
                    DbCore.DbAdmin.clsLogu logu = new DbCore.DbAdmin.clsLogu(0, oKomponente.IdKomponente, idNdermarrje, idPerdoruesi, DateTime.Now, "0", idRaporti, DbCore.mySessionObjects.merrRuajLog(Session));
                    ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                    vendosHfMePerkthime(rm, ci);
                    hfgjuha.Add("idGjuha", idGjuha);
                    hfeshtememe.Add("eshteMeme", eshteMeme);
                    //vjen nga URL me faqen paraardhese

                    percaktoTemplateMenu(ASPxMenuToolBar, idVit, idPerdoruesi, idNdermarrje);
                    DbCore.mySessionObjects.ruajMyReportNeSession<XtraReport>(Session, guidString, null);
                    DbCore.mySessionObjects.ruajMyCallbackNeSesion(Session, guidString, false);
                    konfiguroVleraFillestare(idPerdoruesi, idNdermarrje, ci, idGjuha);
                    EmrateLabelave(ci);
                    merrKontrolleFiltra(idRaporti, idNdermarrje, idPerdoruesi, periudhaKontabel);
                    //reportStyle.Value = perdoruesi.StilRaportiFileName; 

                    EnableStilRaporti();
                    ImbReportToolbar1.ReportStyle = "Style_Jeshile.repss";
                    ImbReportToolbar1.idGjuha = idGjuha;
                    ImbReportToolbar1.ZoomFactor = Convert.ToInt32(0);
                    //if(idRaporti==39)
                    //ImbReportToolbar1.InitialPageWidth = 1400;
                    ImbReportToolbar1.SetSelectionZoomFactor();
                    ImbReportToolbar1.reportExportFormat = 1;
                    ImbReportToolbar1.SetSelectionExportFormat();
                    ImbReportToolbar1.reportExportMode = 0;
                    ImbReportToolbar1.SetSelectionExportMode();


                    //clsTeDrejtaRaporte teDrejtaRap = new clsTeDrejtaRaporte();
                    //teDrejtaRap.merrTeDrejtaPerRaportPerPerdorues(idRaporti, idPerdoruesi, idNdermarrje, idVit);
                    hfTeDrejtaRaporti.Set("dGjitheDok", 1);
                    //DbCore.DbAdmin.colRolPerdorues rolPerd = new DbCore.DbAdmin.colRolPerdorues();
                    //rolPerd.mbushRolePerdoruesSipasPerdoruesi(idPerdoruesi);
                    //DbCore.DbAdmin.clsRoli rol = new DbCore.DbAdmin.clsRoli(rolPerd[0].IdRoli);
                    hfPerdorues.Set("Roli", "RSU");
                    hfPerdorues.Set("Perdoruesi", perdoruesi.Emer);


                    if (mbushComboBoxFiltra(idPerdoruesi, idNdermarrje))
                    {
                        int idFiltri = DbCore.clsFunksione.merrIDFiltriPersonalizuar(Request);
                        DevExpress.Web.MenuItem itemButton = ASPxMenuToolBar.Items.FindByName("TemplatedItemFilter");
                        ASPxComboBox cmbFiltra = ((PlatinumWeb.E_PaySlip.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                        ASPxButton btnFshiFilter = ((PlatinumWeb.E_PaySlip.MenuFilter)(itemButton.Template)).FindControl("btnFshi") as ASPxButton;
                        btnFshiFilter.ClientEnabled = true;
                        NgarkoFiltraTePersonalizuar(idFiltri);

                    }
                    else
                    {
                        rregulloFiltraDateDokumenti();
                    }

                    if (!DbCore.clsFunksione.ktheFiltroQueryString(Request))
                    {
                        afisho(idRaporti, idNdermarrje, idVit, idNderVit, periudhaKontabel, idPerdoruesi, ci, kaSubRaport, guidString, idGjuha);
                    }
                    else if (kaSubRaport) //nqs raporti ka subraport, atehere parametrat e subraportit duhen krijuar qe ne fillim kur hapet faqja.
                    {
                        //krijoParametraSqlSubraporti(idNdermarrje, periudhaKontabel, idNderVit, idPerdoruesi);
                        //krijoParametraShfaqSubraporti(idNdermarrje, periudhaKontabel, idPerdoruesi);
                        konfigurimeSubRaporti(idNdermarrje, periudhaKontabel, idNderVit, idPerdoruesi, ci, guidString, idGjuha);
                    }
                }

                catch (DbCore.MyException m)
                {
                    ImbLogger.Error(m);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, m.Message, pnlMesazhi);
                    DbCore.mySessionObjects.shtoMesazhNeSession(Session, new DbCore.clsMesazh(m.Message));
                }
                catch (Exception ex)
                {
                    ImbLogger.Error(ex);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim i panjohur", pnlMesazhi);
                    //DbCore.mySessionObjects.shtoMesazhNeSession(Session, new DbCore.clsMesazh());
                }
            }
            else
            {
                guidString = hfState["guidString"].ToString();
                //if (idRaporti == 39 || idRaporti == 40 || idRaporti == 237 || idRaporti == 238 || idRaporti == 180)
                //    ImbReportToolbar1.showExportPerTatimeButton(idRaporti);
                if (!IsCallback || (IsCallback && Request["__CALLBACKID"].ToString().Contains("ASPxMenuToolBar")))
                    percaktoTemplateMenu(ASPxMenuToolBar, idVit, idPerdoruesi, idNdermarrje);
                if (IsCallback && Request["__CALLBACKID"].ToString().EndsWith(ReportViewer2.ID))
                {
                    try
                    {
                        XtraReport report;
                        switch (ImbReportToolbar1.ArsyeReload)
                        {
                            case "changeStyle":
                                report = GetReport(guidString);
                                if (report != null)
                                {
                                    ReportViewer2.Report = report;
                                    ImbReportToolbar1.reloadStyle(ReportViewer2, pathStyle.Value);
                                    DbCore.mySessionObjects.ruajMyReportNeSession(Session, guidString, report);
                                    Container.Attributes["src"] = "";
                                }
                                return;
                            case "GotoFirstPage":
                                report = GetReport(guidString);
                                if (report != null)
                                {
                                    ReportViewer2.Report = report;
                                }
                                return;
                            case "ChangeZoom":
                            case "SaveToDisk100":
                                //case "SaveToDiskPerTatime": 
                                report = GetReport(guidString);
                                if (report != null)
                                {
                                    ReportViewer2.Report = report;
                                    ImbReportToolbar1.konfigFleteRaporti(report, idPerdoruesi, EmerRealRaporti);
                                    DbCore.mySessionObjects.ruajMyReportNeSession(Session, guidString, report);
                                    Container.Attributes["src"] = "";
                                }
                                string formatEksporti = ImbReportToolbar1.getExportFormatSelectedItem().Text;
                              
                                return;
                            case "zoomFactor":
                                ImbReportToolbar1.SetSelectionZoomFactor(ImbReportToolbar1.scale);
                                break;
                            default:
                                break;
                        }

                        var currentPage = Request.Form["ASPxCallbackPanel1$ReportViewer2"];
                        var currentPageIndex = (string)Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(currentPage.Replace("&quot;", ""))?.currentPageIndex;

                        if (DbCore.mySessionObjects.merrMyCallbackNgaSesioni(Session, guidString) == false && currentPageIndex == "0" && Convert.ToBoolean(hfRilodo.Value))
                        {//kur perdoruesi shtyp ne menu shiko, ose faqe e re                        
                            konfigurimeSubRaporti(idNdermarrje, periudhaKontabel, idNderVit, idPerdoruesi, ci, guidString, idGjuha);
                            afisho(idRaporti, idNdermarrje, idVit, idNderVit, periudhaKontabel, idPerdoruesi, ci, kaSubRaport, guidString, idGjuha);
                        }
                        else
                        {
                            XtraReport r = GetReport(guidString);
                            DataSet ds = (DataSet)r.DataSource;
                            ReportViewer2.Report = r;
                            DbCore.mySessionObjects.ruajMyCallbackNeSesion(Session, guidString, false);


                        }
                    }
                    catch (DbCore.MyException m)
                    {
                        ImbLogger.Error(m);
                        DbCore.mySessionObjects.shtoMesazhNeSession(Session, new DbCore.clsMesazh(m.Message));
                    }
                    catch (Exception ex)
                    {
                        ImbLogger.Error(ex);
                        DbCore.mySessionObjects.shtoMesazhNeSession(Session, new DbCore.clsMesazh());
                    }
                }
                if (IsPostBack && Request["__EVENTTARGET"].ToString().EndsWith(ReportViewer2.ID))
                {
                    try
                    {
                        if (Request["__EVENTARGUMENT"].ToString().Contains("showPrintDialog") || Request["__EVENTARGUMENT"].ToString().Contains("saveToWindow") || Request["__EVENTARGUMENT"].ToString().Contains("saveToDisk"))
                        //(Convert.ToBoolean(hfPrinto.Value) != true)
                        {
                            percaktoTemplateMenu(ASPxMenuToolBar, idVit, idPerdoruesi, idNdermarrje);
                            XtraReport r = new XtraReport();
                            string formatEksporti = ImbReportToolbar1.getExportFormatSelectedItem().Text;

                            r = GetReport(guidString);
                            ImbReportToolbar1.setExportOptions(r, idRaporti);
                            if (r != null)
                            {
                                DbCore.mySessionObjects.ruajMyCallbackNeSesion(Session, guidString, true);
                                this.ReportViewer2.Report = r;
                            }
                        }
                        else
                            afisho(idRaporti, idNdermarrje, idVit, idNderVit, periudhaKontabel, idPerdoruesi, ci, kaSubRaport, guidString, idGjuha);
                    }
                    catch (DbCore.MyException m)
                    {
                        ImbLogger.Error(m);
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, m.Message, pnlMesazhi);
                        DbCore.mySessionObjects.shtoMesazhNeSession(Session, new DbCore.clsMesazh(m.Message));
                    }
                    catch (Exception ex)
                    {
                        ImbLogger.Error(ex);
                        clsMenuInfo.ShtoMesazh(MenuInfo, new DbCore.clsMesazh(), pnlMesazhi);
                        //DbCore.mySessionObjects.shtoMesazhNeSession(Session, new DbCore.clsMesazh());
                    }
                }
                if (IsCallback && Request["__CALLBACKID"] == ASPxCallbackPanel1.ID)
                {
                    var rapDes = new colRaporteDesign(idNdermarrje, idRaporti).MerrDizajnTeZgjedhur();
                    ImbReportToolbar1.mbushComboOrientimi(rapDes);
                    DbCore.mySessionObjects.ruajMyCallbackNeSesion(Session, guidString, true);
                    XtraReport r = GetReport(guidString);
                    if (r != null)
                        this.ReportViewer2.Report = r;
                }
                if (IsCallback && Request["__CALLBACKID"] == ASPxMenuToolBar.ID)
                {
                    percaktoTemplateMenu(ASPxMenuToolBar, idVit, idPerdoruesi, idNdermarrje);
                    XtraReport r = GetReport(guidString);
                    if (r != null)
                    {
                        //CacheLayer.GlobalCacheManager.MySessionCache["MyCallback"] = true;
                        this.ReportViewer2.Report = r;
                        DbCore.mySessionObjects.ruajMyCallbackNeSesion(Session, guidString, true);

                    }
                }

            }
            Container.Attributes["src"] = "";

            navBarFiltrat.Groups[1].ClientVisible = false;
          

            ImbReportToolbar1.hiqButonExport();
        }
        private void hiqNrPersonalPunonjesi()
        {
            navBarFiltrat.Groups[1].FindControl("lblNrPersonal").Visible = false;
            navBarFiltrat.Groups[1].FindControl("cmbVeprim1NP").Visible = false;
            navBarFiltrat.Groups[1].FindControl("txtBtnNrPersonal1").Visible = false;
            navBarFiltrat.Groups[1].FindControl("cmbLidhesaNP").Visible = false;
            navBarFiltrat.Groups[1].FindControl("cmbVeprim2NP").Visible = false;
            navBarFiltrat.Groups[1].FindControl("txtBtnNrPersonal2").Visible = false;
        }
        private void hiqNrSigurimesh()
        {

            navBarFiltrat.Groups[1].FindControl("lblNrSigurimesh").Visible = false;
            navBarFiltrat.Groups[1].FindControl("txtNrSigurimesh").Visible = false;

        }
        private void hiqFilterDraft()
        {

            navBarFiltrat.Groups[1].FindControl("lblDraft").Visible = false;
            navBarFiltrat.Groups[1].FindControl("cbDraft").Visible = false;
        }



        private void vendosHfMePerkthime(ResourceManager rm, CultureInfo ci)
        {
            hfState.Set("msgNukKeniDrejtaPerVeprim", rm.GetString("msgNukKeniDrejtaPerVeprim", ci));
            hfState.Set("msgRaportiFiltratUNgarkuanMeSukses", rm.GetString("msgRaportiFiltratUNgarkuanMeSukses", ci));
            hfState.Set("msgRaportiZgjidhLlojinEDokumentit", rm.GetString("msgRaportiZgjidhLlojinEDokumentit", ci));
            hfState.Set("popupAdministrimiUniversal", rm.GetString("popupAdministrimiUniversal", ci));
            hfState.Set("msgZgjidhniDepartamentin", rm.GetString("msgZgjidhniDepartamentin", ci));
            hfState.Set("msgZgjidhniNenDepartamentin", rm.GetString("msgZgjidhniNenDepartamentin", ci));
            hfState.Set("msgZgjidhPunonjesin", rm.GetString("msgZgjidhPunonjesin", ci));
            hfState.Set("msgZgjidhSerialin", rm.GetString("msgZgjidhSerialin", ci));
            hfState.Set("msgZgjidhArkaBanka", rm.GetString("msgZgjidhArkaBanka", ci));
            hfState.Set("msgZgjidhMagazinen", rm.GetString("msgZgjidhMagazinen", ci));
            hfState.Set("msgRaportiDuhetTeFshiniNdaresinNgaFushaEKodit", rm.GetString("msgRaportiDuhetTeFshiniNdaresinNgaFushaEKodit", ci));
            hfState.Set("msgZgjidhKF", rm.GetString("msgZgjidhKF", ci));
            hfState.Set("msgZgjidhKategorineEMaturimit", rm.GetString("msgZgjidhKategorineEMaturimit", ci));
            hfState.Set("msgZgjidhPerdoruesin", rm.GetString("msgZgjidhPerdoruesin", ci));
            hfState.Set("msgZgjidhAgjentinEShitjes", rm.GetString("msgZgjidhAgjentinEShitjes", ci));
            hfState.Set("msgZgjidhniNivelinECmimit", rm.GetString("msgZgjidhniNivelinECmimit", ci));
            hfState.Set("msgZgjidhKrijuesin", rm.GetString("msgZgjidhKrijuesin", ci));
            hfState.Set("msgZgjidhQendrenEKostos", rm.GetString("msgZgjidhQendrenEKostos", ci));
            hfState.Set("headerPopUpZgjidhFurnitorin", rm.GetString("headerPopUpZgjidhFurnitorin", ci));
            hfState.Set("roundPanelZgjidhArtikullin", rm.GetString("roundPanelZgjidhArtikullin", ci));
            hfState.Set("roundPanelZgjidhNjesiProdhimi", rm.GetString("roundPanelZgjidhNjesiProdhimi", ci));
            hfState.Set("msgZgjidhKodbar", rm.GetString("msgZgjidhKodbar", ci));
            hfState.Set("msgZgjidhDetajimin", rm.GetString("msgZgjidhDetajimin", ci));
            hfState.Set("msgZgjidhPikeShitjeFurnizimi", rm.GetString("msgZgjidhPikeShitjeFurnizimi", ci));
            hfState.Set("msgZgjidhDegeAdministrative", rm.GetString("msgZgjidhDegeAdministrative", ci));
            hfState.Set("msgZgjidhGrupin1TeArtikullit", rm.GetString("msgZgjidhGrupin1TeArtikullit", ci));
            hfState.Set("msgZgjidhGrupin2TeArtikullit", rm.GetString("msgZgjidhGrupin2TeArtikullit", ci));
            hfState.Set("msgZgjidhAutomjetin", rm.GetString("msgZgjidhAutomjetin", ci));
            hfState.Set("headerPopUpTextZgjidhNdermarrjet", rm.GetString("headerPopUpTextZgjidhNdermarrjet", ci));
            hfState.Set("msgZgjidhGrupin1TeKlientit", rm.GetString("msgZgjidhGrupin1TeKlientit", ci));
            hfState.Set("msgZgjidhGrupin2TeKlientit", rm.GetString("msgZgjidhGrupin2TeKlientit", ci));
            hfState.Set("msgZgjidhGrupin1TeFurnitorit", rm.GetString("msgZgjidhGrupin1TeFurnitorit", ci));
            hfState.Set("msgZgjidhGrupin2TeFurnitorit", rm.GetString("msgZgjidhGrupin2TeFurnitorit", ci));
            hfState.Set("msgZgjidhGrupin1TeKlientFurnitorit", rm.GetString("msgZgjidhGrupin1TeKlientFurnitorit", ci));
            hfState.Set("msgZgjidhGrupin2TeKlientFurnitorit", rm.GetString("msgZgjidhGrupin2TeKlientFurnitorit", ci));
            hfState.Set("msgZgjidhGrupin3TeKlientit", rm.GetString("msgZgjidhGrupin3TeKlientit", ci));
            hfState.Set("msgZgjidhGrupin3TeFurnitorit", rm.GetString("msgZgjidhGrupin3TeFurnitorit", ci));
            hfState.Set("msgZgjidhGrupin3TeKlientFurnitorit", rm.GetString("msgZgjidhGrupin3TeKlientFurnitorit", ci));
            hfState.Set("msgLupaLlogariShpejteZgjidhniGrupin", rm.GetString("msgLupaLlogariShpejteZgjidhniGrupin", ci));
            hfState.Set("msgZgjidhBurimin", rm.GetString("msgZgjidhBurimin", ci));
            hfState.Set("msgZgjidhAktivitetin", rm.GetString("msgZgjidhAktivitetin", ci));
        }

        protected void ReportViewer2_Unload(object sender, EventArgs e)
        {
            ((ReportViewer)sender).Report = null;
        }

       

        private int kthekategoridok()
        {
            clsRaporti clsrap = new clsRaporti(0, Convert.ToInt32(Request.QueryString["idraporti"].ToString()));
            int a = 0;
            switch (clsrap.IdModul)
            {
                case 7:
                    a = 5;
                    break;
                case 12:
                    a = 1;
                    break;
                case 13:
                    a = 2;
                    break;
                case 2:
                    a = 3;
                    break;
                case 6:
                    a = 4;
                    break;
                case 16:
                    a = 6;
                    break;
                case 9:
                    a = 12;
                    break;
            }
            return a;
        }

        private void mbushComboNiveli(int idPerdoruesi, int idNdermarrje)
        {
            //DbCore.DbRegjistrim.clsDatabaseRegjistrim dbregj = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            DbCore.DbRegjistrim.colNivelRegjistrimi colnivel = new DbCore.DbRegjistrim.colNivelRegjistrimi();
            if (kthekategoridok() == 5)
                colnivel.mbushGjitheNivelRegjistrimiGjeneruarKont(idNdermarrje);
            else
                colnivel.mbushGjitheNivelRegjistrimiSipasKategoriPaKonvertime(kthekategoridok(), idNdermarrje, idPerdoruesi);
            string kontrollEmri = "cmbNiveli";
            ASPxComboBox cmbNivel = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);

            colnivel.Insert(0, new DbCore.DbRegjistrim.clsNivelRegjistrimi(0, 0, "", "", 0, true, 0, 0, 0, false));
            cmbNivel.DataSource = colnivel;
            cmbNivel.TextField = "KODI";
            cmbNivel.ValueField = "IDNIVEL";
            cmbNivel.DataBind();
        }

        private void mbushComboLikuiduar(CultureInfo ci)
        {
            ASPxComboBox cmbLikuiduar = new ASPxComboBox();
            string kontrollEmri = "cmbLikuiduar";
            cmbLikuiduar = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbLikuiduar.Items.Add(rm.GetString("cmbStatusiItemTeGjitha", ci), 2);
            cmbLikuiduar.Items.Add(rm.GetString("cmbboxRaportiTeLikuiduar", ci), 0);
            cmbLikuiduar.Items.Add(rm.GetString("cmbboxRaportiTePaLikuiduar", ci), 1);
            cmbLikuiduar.SelectedIndex = 0;
            cmbLikuiduar.DataBind();
        }
        private void mbushcomboLlojArtikulli(CultureInfo ci, int idRaport)
        {
            ASPxComboBox cmbLlojArtikulli = new ASPxComboBox();
            string kontrollEmri = "cmbLlojArtikulli";
            cmbLlojArtikulli = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbLlojArtikulli.Items.Add(rm.GetString("cmbboxItemFilterAvancTeGjithe", ci), rm.GetString("cmbboxItemFilterAvancTeGjithe", ci));
            cmbLlojArtikulli.Items.Add(rm.GetString("cmbboxItemFilterAvancAfatshk", ci), rm.GetString("cmbboxItemFilterAvancAfatshk", ci));
            cmbLlojArtikulli.Items.Add(rm.GetString("cmbboxItemFilterAvancAfatgjt", ci), rm.GetString("cmbboxItemFilterAvancAfatgjt", ci));
            if (idRaport == 280)
                cmbLlojArtikulli.SelectedIndex = 1;
            else
                cmbLlojArtikulli.SelectedIndex = 0;
            cmbLlojArtikulli.DataBind();

        }

        private void mbushcomboLlojSubjekti(CultureInfo ci)
        {
            ASPxComboBox cmbLlojSubjekti = new ASPxComboBox();
            string kontrollEmri = "cmbLlojSubjekti";
            cmbLlojSubjekti = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbLlojSubjekti.Items.Add("", 0);
            cmbLlojSubjekti.Items.Add(rm.GetString("comboItemBlerjeShitjeKlient", ci), 1);
            cmbLlojSubjekti.Items.Add(rm.GetString("comboItemBlerjeShitjeFurnitor", ci), 2);
            cmbLlojSubjekti.Items.Add(rm.GetString("labelRaportLlogari", ci), 3);
            cmbLlojSubjekti.SelectedIndex = 0;
            cmbLlojSubjekti.DataBind();
        }


        private void mbushComboStatus(CultureInfo ci)
        {
            STR_TeGjitha = rm.GetString("cmbStatusiItemTeGjitha", ci);
            STR_TeEkzekutuara = rm.GetString("cmbStatusiItemTeEkzekutuara", ci);
            STR_TePaekzekutuara = rm.GetString("cmbStatusiItemTePaEkzekutuara", ci);
            ASPxComboBox cmbStatusi = new ASPxComboBox();
            string kontrollEmri = "cmbStatusi";
            cmbStatusi = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbStatusi.Items.Add(STR_TeGjitha, 2);
            cmbStatusi.Items.Add(STR_TeEkzekutuara, 0);
            cmbStatusi.Items.Add(STR_TePaekzekutuara, 1);
            cmbStatusi.SelectedIndex = 0;
            cmbStatusi.DataBind();
        }

        private void mbushComboVeprimePeriudhe(CultureInfo ci)
        {
            STR_Jo = rm.GetString("cmbboxItemFilterAvancJo", ci);
            STR_Po = rm.GetString("cmbboxItemFilterAvancPo", ci);
            STR_TeGjitha = rm.GetString("cmbboxItemFilterAvancTeGjitha", ci);
            ASPxComboBox cmbVeprimePeriudhe = new ASPxComboBox();
            string kontrollEmri = "cmbVeprimePeriudhe";
            cmbVeprimePeriudhe = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbVeprimePeriudhe.Items.Add(STR_TeGjitha, 0);
            cmbVeprimePeriudhe.Items.Add(STR_Jo, 1);
            cmbVeprimePeriudhe.Items.Add(STR_Po, 2);
            cmbVeprimePeriudhe.SelectedIndex = 0;
            cmbVeprimePeriudhe.DataBind();
        }

        private void mbushComboGjendjeDetyrimi(CultureInfo ci)
        {

            ASPxComboBox cmbGjendjeDetyrime = new ASPxComboBox();
            string kontrollEmri = "cmbGjendjeDetyrime";
            cmbGjendjeDetyrime = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbGjendjeDetyrime.Items.Add(rm.GetString("cmbRaportMegjendje", ci), 0);
            cmbGjendjeDetyrime.Items.Add(rm.GetString("cmbRaportPagjendje", ci), 1);
            cmbGjendjeDetyrime.Items.Add(rm.GetString("cmbboxItemFilterAvancTeGjithe", ci), 2);
            cmbGjendjeDetyrime.SelectedIndex = 0;
            cmbGjendjeDetyrime.DataBind();
        }
        private void mbushComboStatusCRM(CultureInfo ci)
        {

            //STR_Po = rm.GetString("cmbRealizuar", ci);
            //STR_Jo = rm.GetString("cmbJorealizuar", ci);
            STR_TeGjitha = rm.GetString("cmbboxItemFilterAvancTeGjitha", ci);
            ASPxComboBox cmbStatusCRM = new ASPxComboBox();
            string kontrollEmri = "cmbStatusCRM";
            cmbStatusCRM = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbStatusCRM.Items.Add(rm.GetString("cmbRealizuar", ci), 0);
            cmbStatusCRM.Items.Add(rm.GetString("cmbJorealizuar", ci), 1);
            cmbStatusCRM.Items.Add(STR_TeGjitha, 2);
            cmbStatusCRM.SelectedIndex = 0;
            cmbStatusCRM.DataBind();


        }
        private void mbushComboLlojiArtBurim(CultureInfo ci)
        {
            STR_Artikull = rm.GetString("labelRaportiArtikull", ci);
            STR_Burim = rm.GetString("cmbLlojiArtBurimItemBurim", ci);
            ASPxComboBox cmbLlojiArtBurim = new ASPxComboBox();
            string kontrollEmri = "cmbLlojiArtBurim";
            cmbLlojiArtBurim = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbLlojiArtBurim.Items.Add(" ", 0);
            cmbLlojiArtBurim.Items.Add(STR_Artikull, 1);
            cmbLlojiArtBurim.Items.Add(STR_Burim, 2);
            cmbLlojiArtBurim.SelectedIndex = 0;
            cmbLlojiArtBurim.DataBind();
        }


        private void mbushComboShfaqArt(CultureInfo ci, int idRaport)
        {
            ASPxComboBox cmbShfaqArt = new ASPxComboBox();
            string kontrollEmri = "cmbShfaqArt";

            if (idRaport == 20751 || idRaport == 81)
            {
                STR_ArtBurimGjitha = rm.GetString("cmbboxItemFilterAvancTeGjithe", ci);
                STR_Teprica = rm.GetString("labelRaportArtikujTeprica", ci);
                STR_Mungesa = rm.GetString("labelRaportArtikujMungesa", ci);
                STR_Teprice_Mungese = rm.GetString("labelRaportArtikujTeprica_Mungesa", ci);
                cmbShfaqArt = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
                cmbShfaqArt.Items.Add(STR_ArtBurimGjitha, 0);
                cmbShfaqArt.Items.Add(STR_Teprica, 1);
                cmbShfaqArt.Items.Add(STR_Mungesa, 2);
                cmbShfaqArt.Items.Add(STR_Teprice_Mungese, 3);
                cmbShfaqArt.SelectedIndex = 0;
            }
            cmbShfaqArt.DataBind();
        }
        private void mbushComboGjendjeArt(CultureInfo ci, int idRaport)
        {
            ASPxComboBox cmbGjendjeArt = new ASPxComboBox();
            string kontrollEmri = "cmbGjendjeArt";

            if (idRaport == 46 || idRaport == 48 || idRaport == 264)
            {
                STR_ArtBurimGjitha = rm.GetString("cmbboxItemFilterAvancTeGjithe", ci);
                STR_GjendjeJoZero = rm.GetString("filterRaportMeGjendje", ci);
                STR_GjendjeZero = rm.GetString("filterRaportMeGjendje", ci) + " 0";
                cmbGjendjeArt = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
                cmbGjendjeArt.Items.Add(STR_GjendjeJoZero, 0);
                cmbGjendjeArt.Items.Add(STR_GjendjeZero, 1);
                cmbGjendjeArt.Items.Add(STR_ArtBurimGjitha, 2);
                cmbGjendjeArt.SelectedIndex = 0;
            }
            else
            {
                STR_ArtBurimGjitha = rm.GetString("cmbboxItemFilterAvancTeGjithe", ci);
                STR_GjendjeJoZero = rm.GetString("labelRaportArtikujMeGjendje", ci);
                STR_GjendjeZero = rm.GetString("labelRaportArtikujMeGjendjeZero", ci);
                cmbGjendjeArt = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
                cmbGjendjeArt.Items.Add(STR_ArtBurimGjitha, 0);
                cmbGjendjeArt.Items.Add(STR_GjendjeJoZero, 1);
                cmbGjendjeArt.Items.Add(STR_GjendjeZero, 2);
                if (idRaport == 152 || idRaport == 81)
                    cmbGjendjeArt.SelectedIndex = 0;
                else
                    cmbGjendjeArt.SelectedIndex = 1;
            }
            cmbGjendjeArt.DataBind();
        }


        private void mbushComboLlojPorosie(CultureInfo ci, int idRaport)
        {
            STR_LlojPorosieUPP = rm.GetString("cmbboxItemFilterLlojPorosieUPP", ci);
            STR_LlojPorosieJOUPP = rm.GetString("cmbboxItemFilterLlojPorosieJOUPP", ci);

            ASPxComboBox cmbLlojPorosie = new ASPxComboBox();
            string kontrollEmri = "cmbLlojPorosie";
            cmbLlojPorosie = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbLlojPorosie.Items.Add(" ", 0);
            cmbLlojPorosie.Items.Add(STR_LlojPorosieUPP, 1);
            cmbLlojPorosie.Items.Add(STR_LlojPorosieJOUPP, 2);

            cmbLlojPorosie.SelectedIndex = 0;

            cmbLlojPorosie.DataBind();
        }

        private void mbushComboStatusPerfunduar(CultureInfo ci, int idRaport)
        {
            STR_Po = rm.GetString("cmbboxItemFilterAvancPo", ci);
            STR_Jo = rm.GetString("cmbboxItemFilterAvancJo", ci);
            STR_Pjeserisht = rm.GetString("cmbboxItemFilterAvancPjeserisht", ci);
            ASPxComboBox cmbPerfunduar1 = new ASPxComboBox();
            string kontrollEmri1 = "cmbPerfunduar1";
            cmbPerfunduar1 = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri1);
            cmbPerfunduar1.Items.Add("", 0);
            cmbPerfunduar1.Items.Add(STR_Po, 1);
            cmbPerfunduar1.Items.Add(STR_Jo, 2);
            cmbPerfunduar1.Items.Add(STR_Pjeserisht, 3);
            cmbPerfunduar1.SelectedIndex = 0;
            cmbPerfunduar1.DataBind();

            ASPxComboBox cmbPerfunduar2 = new ASPxComboBox();
            string kontrollEmri2 = "cmbPerfunduar2";
            cmbPerfunduar2 = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri2);
            cmbPerfunduar2.Items.Add("", 0);
            cmbPerfunduar2.Items.Add(STR_Po, 1);
            cmbPerfunduar2.Items.Add(STR_Jo, 2);
            cmbPerfunduar2.Items.Add(STR_Pjeserisht, 3);
            cmbPerfunduar2.SelectedIndex = 0;
            cmbPerfunduar2.DataBind();
        }


        private void mbushComboStatusProdhuar(CultureInfo ci, int idRaport)
        {
            STR_Po = rm.GetString("cmbboxItemFilterAvancPo", ci);
            STR_Jo = rm.GetString("cmbboxItemFilterAvancJo", ci);
            STR_Pjeserisht = rm.GetString("cmbboxItemFilterAvancPjeserisht", ci);
            ASPxComboBox cmbProdhuar1 = new ASPxComboBox();
            string kontrollEmri1 = "cmbProdhuar1";
            cmbProdhuar1 = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri1);
            cmbProdhuar1.Items.Add("", 0);
            cmbProdhuar1.Items.Add(STR_Po, 1);
            cmbProdhuar1.Items.Add(STR_Jo, 2);
            cmbProdhuar1.Items.Add(STR_Pjeserisht, 3);
            cmbProdhuar1.SelectedIndex = 0;
            cmbProdhuar1.DataBind();

            ASPxComboBox cmbProdhuar2 = new ASPxComboBox();
            string kontrollEmri2 = "cmbProdhuar2";
            cmbProdhuar2 = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri2);
            cmbProdhuar2.Items.Add("", 0);
            cmbProdhuar2.Items.Add(STR_Po, 1);
            cmbProdhuar2.Items.Add(STR_Jo, 2);
            cmbProdhuar2.Items.Add(STR_Pjeserisht, 3);
            cmbProdhuar2.SelectedIndex = 0;
            cmbProdhuar2.DataBind();
        }


        private void mbushComboStatusFaturuar(CultureInfo ci, int idRaport)
        {
            STR_Po = rm.GetString("cmbboxItemFilterAvancPo", ci);
            STR_Jo = rm.GetString("cmbboxItemFilterAvancJo", ci);
            STR_Pjeserisht = rm.GetString("cmbboxItemFilterAvancPjeserisht", ci);
            ASPxComboBox cmbFaturuar1 = new ASPxComboBox();
            string kontrollEmri1 = "cmbFaturuar1";
            cmbFaturuar1 = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri1);
            cmbFaturuar1.Items.Add("", 0);
            cmbFaturuar1.Items.Add(STR_Po, 1);
            cmbFaturuar1.Items.Add(STR_Jo, 2);
            cmbFaturuar1.Items.Add(STR_Pjeserisht, 3);
            cmbFaturuar1.SelectedIndex = 0;
            cmbFaturuar1.DataBind();

            ASPxComboBox cmbFaturuar2 = new ASPxComboBox();
            string kontrollEmri2 = "cmbFaturuar2";
            cmbFaturuar2 = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri2);
            cmbFaturuar2.Items.Add("", 0);
            cmbFaturuar2.Items.Add(STR_Po, 1);
            cmbFaturuar2.Items.Add(STR_Jo, 2);
            cmbFaturuar2.Items.Add(STR_Pjeserisht, 3);
            cmbFaturuar2.SelectedIndex = 0;
            cmbFaturuar2.DataBind();
        }

        private void mbushComboEmertimLlog(CultureInfo ci, int idgjuha)
        {
            STR_Emertim1 = rm.GetString("cmbboxItemEmertim", ci) + " 1";
            STR_Emertim2 = rm.GetString("cmbboxItemEmertim", ci) + " 2";
            ASPxComboBox cmbEmertimLlog = new ASPxComboBox();
            string kontrollEmri = "cmbEmertimLlog";
            cmbEmertimLlog = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbEmertimLlog.Items.Add(STR_Emertim1, 0);
            cmbEmertimLlog.Items.Add(STR_Emertim2, 1);
            if (idgjuha == 0)
                cmbEmertimLlog.SelectedIndex = 0;
            else cmbEmertimLlog.SelectedIndex = 1;
            cmbEmertimLlog.DataBind();
        }
        private void mbushComboGjendja(CultureInfo ci)
        {
            STR_Emertim1 = rm.GetString("cmbboxItemFilterAvancTeGjithe", ci);
            STR_Emertim2 = rm.GetString("cmbboxItemLlogGjendje", ci);
            ASPxComboBox cmbGjendja = new ASPxComboBox();
            string kontrollEmri = "cmbGjendja";
            cmbGjendja = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbGjendja.Items.Add(STR_Emertim1, 0);
            cmbGjendja.Items.Add(STR_Emertim2, 1);
            cmbGjendja.SelectedIndex = 0;
            cmbGjendja.DataBind();
        }
        private void mbushComboAktiv(CultureInfo ci)
        {
            STR_Po = rm.GetString("cmbboxItemFilterAvancPo", ci);
            STR_Jo = rm.GetString("cmbboxItemFilterAvancJo", ci);
            STR_TeGjitha = rm.GetString("cmbboxItemFilterAvancTeGjitha", ci);
            ASPxComboBox cmbAktiv = new ASPxComboBox();
            string kontrollEmri = "cmbAktiv";
            cmbAktiv = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbAktiv.Items.Add(STR_TeGjitha, 0);
            cmbAktiv.Items.Add(STR_Po, 1);
            cmbAktiv.Items.Add(STR_Jo, 2);
            cmbAktiv.SelectedIndex = 0;
            cmbAktiv.DataBind();
        }
        private void mbushComboPaguar(CultureInfo ci, int idRaport)
        {
            STR_Po = rm.GetString("cmbboxItemFilterAvancPo", ci);
            STR_Jo = rm.GetString("cmbboxItemFilterAvancJo", ci);
            STR_Pjeserisht = rm.GetString("cmbboxItemFilterAvancPjeserisht", ci);
            STR_TeGjitha = rm.GetString("cmbboxItemFilterAvancTeGjitha", ci);
            ASPxComboBox cmbPaguar = new ASPxComboBox();
            string kontrollEmri = "cmbPaguar";
            cmbPaguar = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbPaguar.Items.Add(STR_Po, 0);
            cmbPaguar.Items.Add(STR_Jo, 1);
            cmbPaguar.Items.Add(STR_Pjeserisht, 2);
            cmbPaguar.Items.Add(STR_TeGjitha, 3);
            if (idRaport == 160)
                cmbPaguar.SelectedIndex = 3;
            else
                cmbPaguar.SelectedIndex = 1;
            cmbPaguar.DataBind();
        }

        private void mbushComboLlojDetajim(CultureInfo ci, int idRaport)
        {

            Detajim1 = rm.GetString("labelDetajim1", ci);
            Detajim2 = rm.GetString("labelDetajim2", ci);
            STR_TeGjitha = rm.GetString("cmbboxItemFilterAvancTeGjitha", ci);
            ASPxComboBox cmbLlojDetajim = new ASPxComboBox();
            string kontrollEmri = "cmbLlojDetajim";
            cmbLlojDetajim = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);

            if (idRaport == 101)
            {
                cmbLlojDetajim.Items.Add(Detajim1, 1);
                cmbLlojDetajim.Items.Add(Detajim2, 2);
                cmbLlojDetajim.SelectedIndex = 0;

            }
            else
            {
                cmbLlojDetajim.Items.Add(STR_TeGjitha, 0);
                cmbLlojDetajim.Items.Add(Detajim1, 1);
                cmbLlojDetajim.Items.Add(Detajim2, 2);
                cmbLlojDetajim.SelectedIndex = 0;
            }
            cmbLlojDetajim.DataBind();
        }


        private void mbushComboStatusAfatDok(CultureInfo ci)
        {
            STR_GjitheDok = rm.GetString("cmbboxItemFilterAvancGjitheDok", ci);
            STR_DokBrendaAfat = rm.GetString("cmbboxItemFilterAvancDokBrendaAfat", ci);
            STR_DokJashteAfat = rm.GetString("cmbboxItemFilterAvancDokJashteAfat", ci);
            ASPxComboBox cmbStatusAfatDok = new ASPxComboBox();
            string kontrollEmri = "cmbStatusAfatDok";
            cmbStatusAfatDok = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbStatusAfatDok.Items.Add(STR_GjitheDok, 0);
            cmbStatusAfatDok.Items.Add(STR_DokBrendaAfat, 1);
            cmbStatusAfatDok.Items.Add(STR_DokJashteAfat, 2);
            cmbStatusAfatDok.SelectedIndex = 1;
            cmbStatusAfatDok.DataBind();
        }
        private void mbushComboStatusKonvertimiDyte(CultureInfo ci)
        {
            STR_KonvertimiDyteJoKonvertuar = rm.GetString("cmbStatusKonvertimiDyteJoKonvertuar", ci);
            STR_KonvertimiDytePerfunduar = rm.GetString("cmbStatusKonvertimiDytePerfunduar", ci);
            STR_KonvertimiDytePjeserisht = rm.GetString("cmbStatusKonvertimiDytePjeserisht", ci);
            STR_KonvertimiDyteTejkaluar = rm.GetString("cmbStatusKonvertimiDyteTejkaluar", ci);
            ASPxComboBox cmbStatusKonvertimiDyte = new ASPxComboBox();
            string kontrollEmri = "cmbStatusKonvertimiDyte1";
            cmbStatusKonvertimiDyte = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbStatusKonvertimiDyte.Items.Add(" ", 0);
            cmbStatusKonvertimiDyte.Items.Add(STR_KonvertimiDyteJoKonvertuar, 1);
            cmbStatusKonvertimiDyte.Items.Add(STR_KonvertimiDytePjeserisht, 2);
            cmbStatusKonvertimiDyte.Items.Add(STR_KonvertimiDytePerfunduar, 3);
            cmbStatusKonvertimiDyte.Items.Add(STR_KonvertimiDyteTejkaluar, 4);

            cmbStatusKonvertimiDyte.DataBind();
            ASPxComboBox cmbStatusKonvertimiDyte2 = new ASPxComboBox();
            string kontrollEmri2 = "cmbStatusKonvertimiDyte2";
            cmbStatusKonvertimiDyte2 = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri2);
            cmbStatusKonvertimiDyte2.Items.Add(" ", 0);
            cmbStatusKonvertimiDyte2.Items.Add(STR_KonvertimiDyteJoKonvertuar, 1);
            cmbStatusKonvertimiDyte2.Items.Add(STR_KonvertimiDytePjeserisht, 2);
            cmbStatusKonvertimiDyte2.Items.Add(STR_KonvertimiDytePerfunduar, 3);
            cmbStatusKonvertimiDyte2.Items.Add(STR_KonvertimiDyteTejkaluar, 4);

            cmbStatusKonvertimiDyte2.DataBind();

            if (idRaporti == 82)
            {
                cmbStatusKonvertimiDyte.SelectedIndex = 3;
                cmbStatusKonvertimiDyte2.SelectedIndex = 2;
                ASPxComboBox cmbLidhesaStatusKonvertimDyte = new ASPxComboBox();
                cmbLidhesaStatusKonvertimDyte = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaStatusKonvertimDyte");
                cmbLidhesaStatusKonvertimDyte.SelectedIndex = 2;
            }

            else
            {
                cmbStatusKonvertimiDyte.SelectedIndex = 0;
                cmbStatusKonvertimiDyte2.SelectedIndex = 0;

            }
        }
        private void mbushComboStatusKonvertimi(CultureInfo ci)
        {
            STR_KonvertimiDyteJoKonvertuar = rm.GetString("cmbStatusKonvertimiDyteJoKonvertuar", ci);
            STR_KonvertimiDytePerfunduar = rm.GetString("cmbStatusKonvertimiDytePerfunduar", ci);
            STR_KonvertimiDytePjeserisht = rm.GetString("cmbStatusKonvertimiDytePjeserisht", ci);
            STR_KonvertimiDyteTejkaluar = rm.GetString("cmbStatusKonvertimiDyteTejkaluar", ci);
            ASPxComboBox cmbStatusKonvertimiDyte = new ASPxComboBox();
            string kontrollEmri = "cmbStatusKonvertimiDyte";
            cmbStatusKonvertimiDyte = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbStatusKonvertimiDyte.Items.Add(" ", 0);
            cmbStatusKonvertimiDyte.Items.Add(STR_KonvertimiDyteJoKonvertuar, 1);
            cmbStatusKonvertimiDyte.Items.Add(STR_KonvertimiDytePjeserisht, 2);
            cmbStatusKonvertimiDyte.Items.Add(STR_KonvertimiDytePerfunduar, 3);
            cmbStatusKonvertimiDyte.Items.Add(STR_KonvertimiDyteTejkaluar, 4);

            cmbStatusKonvertimiDyte.DataBind();
        }

        private void mbushComboCikli(CultureInfo ci)
        {
            ASPxComboBox cmbCikli = new ASPxComboBox();
            string kontrollEmri = "cmbCikli";
            cmbCikli = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            if (idRaporti == 203 || idRaporti == 204)
            {
                cmbCikli.Items.Add("30 " + rm.GetString("filterMinuta", ci), 0);
                cmbCikli.Items.Add("60 " + rm.GetString("filterMinuta", ci), 1);
                cmbCikli.Items.Add("120 " + rm.GetString("filterMinuta", ci), 2);
            }
            else
            {
                cmbCikli.Items.Add(rm.GetString("labelRaportDitore", ci), 1);
                cmbCikli.Items.Add(rm.GetString("labelRaportMujore", ci), 0);
            }
            cmbCikli.SelectedIndex = 1;
            cmbCikli.DataBind();
        }

        private void mbushComboShfaqVlerat(CultureInfo ci)
        {
            ASPxComboBox cmbShfaqVlerat = new ASPxComboBox();
            string kontrollEmri = "cmbShfaqVlerat";
            cmbShfaqVlerat = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbShfaqVlerat.Items.Add(rm.GetString("cmbboxRaportiTeDukshme", ci), 0);
            cmbShfaqVlerat.Items.Add(rm.GetString("cmbboxRaportiTePaDukshme", ci), 1);
            cmbShfaqVlerat.SelectedIndex = 1;
            cmbShfaqVlerat.DataBind();
        }

        private void mbushComboTipGrafiku(CultureInfo ci)
        {
            ASPxComboBox cmbTipGrafiku = new ASPxComboBox();
            string kontrollEmri = "cmbTipGrafiku";
            cmbTipGrafiku = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbTipGrafiku.Items.Add(rm.GetString("cmbboxRaportiVija", ci), DevExpress.XtraCharts.ViewType.Line);
            cmbTipGrafiku.Items.Add(rm.GetString("cmbboxRaportiVija", ci) + " 3D", DevExpress.XtraCharts.ViewType.Line3D);
            cmbTipGrafiku.Items.Add(rm.GetString("cmbboxRaportiSiperfaqe", ci), DevExpress.XtraCharts.ViewType.Area);
            cmbTipGrafiku.Items.Add(rm.GetString("cmbboxRaportiSiperfaqe", ci) + " 3D", DevExpress.XtraCharts.ViewType.Area3D);
            cmbTipGrafiku.Items.Add(rm.GetString("cmbboxRaportiKolona", ci), DevExpress.XtraCharts.ViewType.Bar);
            cmbTipGrafiku.Items.Add(rm.GetString("cmbboxRaportiKolona", ci) + " 3D", DevExpress.XtraCharts.ViewType.Bar3D);
            if (idRaporti == 203 || idRaporti == 204)
                cmbTipGrafiku.SelectedIndex = 4;
            else
                cmbTipGrafiku.SelectedIndex = 0;
            cmbTipGrafiku.DataBind();
        }

        /// <summary>
        /// mbush kombon e mujit
        /// </summary>
        private void mbushComboMuajt(ASPxComboBox cmbMuaji, int idGjuha)
        {
            if (idRaporti == 286 || idRaporti == 290 || idRaporti == 291)
            {
                var muaji = Converter.MerrVlereOseDefault<string>(Request.QueryString["Muaji"]);
                ConfigureAspxComboBox.mbushComboMuajt(cmbMuaji, idGjuha, muaji, mySessionObjects.merrPeriudheKontabel(Session));
            }
            else
            {
                if (idRaporti != 73 && idRaporti != 74)
                    cmbMuaji.Items.Add("", 0);
                ConfigureAspxComboBox.mbushComboMuajt(cmbMuaji, idGjuha,"", null);
            }
        }

        /// <summary>
        /// mbush kombon e mujit
        /// </summary>
        private void mbushComboMuajtInterval2Mujore()
        {
            ASPxComboBox cmbMuaji = (ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbMuaji");
            //cmbMuaji.Items.Add("", 0);
            cmbMuaji.Items.Add(DbCore.DbListPagesat.Muajt.Janar.ToString() + " --> " + DbCore.DbListPagesat.Muajt.Shkurt.ToString(), 0);
            cmbMuaji.Items.Add(DbCore.DbListPagesat.Muajt.Shkurt.ToString() + " --> " + DbCore.DbListPagesat.Muajt.Mars.ToString(), 1);
            cmbMuaji.Items.Add(DbCore.DbListPagesat.Muajt.Mars.ToString() + " --> " + DbCore.DbListPagesat.Muajt.Prill.ToString(), 2);
            cmbMuaji.Items.Add(DbCore.DbListPagesat.Muajt.Prill.ToString() + " --> " + DbCore.DbListPagesat.Muajt.Maj.ToString(), 3);
            cmbMuaji.Items.Add(DbCore.DbListPagesat.Muajt.Maj.ToString() + " --> " + DbCore.DbListPagesat.Muajt.Qershor.ToString(), 4);
            cmbMuaji.Items.Add(DbCore.DbListPagesat.Muajt.Qershor.ToString() + " --> " + DbCore.DbListPagesat.Muajt.Korrik.ToString(), 5);
            cmbMuaji.Items.Add(DbCore.DbListPagesat.Muajt.Korrik.ToString() + " --> " + DbCore.DbListPagesat.Muajt.Gusht.ToString(), 6);
            cmbMuaji.Items.Add(DbCore.DbListPagesat.Muajt.Gusht.ToString() + " --> " + DbCore.DbListPagesat.Muajt.Shtator.ToString(), 7);
            cmbMuaji.Items.Add(DbCore.DbListPagesat.Muajt.Shtator.ToString() + " --> " + DbCore.DbListPagesat.Muajt.Tetor.ToString(), 8);
            cmbMuaji.Items.Add(DbCore.DbListPagesat.Muajt.Tetor.ToString() + " --> " + DbCore.DbListPagesat.Muajt.Nentor.ToString(), 9);
            cmbMuaji.Items.Add(DbCore.DbListPagesat.Muajt.Nentor.ToString() + " --> " + DbCore.DbListPagesat.Muajt.Dhjetor.ToString(), 10);
            //cmbMuaji.Items.Add(DbCore.DbListPagesat.Muajt.Dhjetor.ToString() + " --> " + DbCore.DbListPagesat.Muajt.Janar.ToString(), 11);
            cmbMuaji.SelectedIndex = 0;
            cmbMuaji.DataBind();
        }

        private void mbushComboTipGrafikuKrahasues(CultureInfo ci)
        {
            ASPxComboBox cmbTipGrafikuKrahasues = new ASPxComboBox();
            string kontrollEmri = "cmbTipGrafikuKrahasues";
            cmbTipGrafikuKrahasues = (ASPxComboBox)navBarFiltrat.Groups[0].FindControl(kontrollEmri);
            cmbTipGrafikuKrahasues.Items.Add(rm.GetString("filterTeArdhurat", ci), 0);
            cmbTipGrafikuKrahasues.Items.Add(rm.GetString("labelShpenzimet", ci), 1);
            cmbTipGrafikuKrahasues.Items.Add(rm.GetString("filterFitimi", ci), 2);
            cmbTipGrafikuKrahasues.SelectedIndex = 0;
            cmbTipGrafikuKrahasues.DataBind();
        }

        private void mbushComboPasqyra(int idNdermarrje, int idgjuha)
        {
            ASPxComboBox cmbPasqyra = new ASPxComboBox();
            string kontrollEmri = "cmbPasqyra";
            cmbPasqyra = (ASPxComboBox)navBarFiltrat.Groups[0].FindControl(kontrollEmri);

            DbCore.DbKontabiliteti.colPasqyratFinaciare colPas;
            if (idRaporti == 4 || idRaporti == 178 || idRaporti == 230)
                colPas = new DbCore.DbKontabiliteti.colPasqyratFinaciare("Bilanc", idNdermarrje);
            else if (idRaporti == 188 || idRaporti == 202 || idRaporti == 205 || idRaporti == 207 || idRaporti == 208 || idRaporti == 235 || idRaporti == 239 || idRaporti == 240 || idRaporti == 241)

                colPas = new DbCore.DbKontabiliteti.colPasqyratFinaciare("Buxhetor", idNdermarrje, idRaporti);

            else if (idRaporti == 5 || idRaporti == 180 || idRaporti == 229 || idRaporti == 236)
                colPas = new DbCore.DbKontabiliteti.colPasqyratFinaciare("PASH", idNdermarrje);
            else
                colPas = new DbCore.DbKontabiliteti.colPasqyratFinaciare("Cash Flow", idNdermarrje);

            if (idgjuha == 0) cmbPasqyra.SelectedIndex = 0;
            else cmbPasqyra.SelectedIndex = 1;

            cmbPasqyra.ValueField = "IdPasqyresFin";
            cmbPasqyra.TextField = "KodiPasqyresFin";
            cmbPasqyra.DataSource = colPas;
            cmbPasqyra.DataBind();
          
        }

        private bool mbushComboBoxFiltra(int idPerdoruesi, int idNdermarrje)
        {
            colFilterKoka colFiltra = new colFilterKoka(idRaporti, idPerdoruesi, idNdermarrje);
            int idFiltri = DbCore.clsFunksione.merrIDFiltriPersonalizuar(Request);
            bool done = false;
            if (idFiltri != -1 && idFiltri != 0)
            {
                clsFilterKoka filtri = new clsFilterKoka();
                foreach (clsFilterKoka f in colFiltra)
                {
                    if (f.IdKokaFilter == idFiltri)
                    {
                        done = true;
                        filtri = f;
                    }
                }
                if (done)
                {
                    colFiltra.Remove(filtri);
                    colFiltra.Insert(0, filtri);
                    colFiltra.Insert(colFiltra.Count, new clsFilterKoka(""));
                }
                else
                {
                    colFiltra.Insert(0, new clsFilterKoka(""));
                }
            }
            else
            {
                colFiltra.Insert(0, new clsFilterKoka(""));
            }
            var model = new DbCore.MyMenuFilterModel
            {
                ValueField = "IdFiltra",
                TextField = "KokaFilterKodi",
                DataSource = colFiltra
            };
            DbCore.mySessionObjects.RuajNeSession(System.Web.HttpContext.Current.Session, model, $"filtraGride_{1}");
            //DevExpress.Web.MenuItem itemButton = ASPxMenuToolBar.Items.FindByName("TemplatedItemFilter");
            //ASPxComboBox cmbFiltra = ((PlatinumWeb.E_PaySlip.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            //cmbFiltra.SelectedIndex = 0;
            return done;
        }

        private void konfiguroVleraFillestare(int idPerdoruesi, int idNdermarrje, CultureInfo ci, int idGjuha)
        {

            ConfigureAspxComboBox.mbushComboStatusiHr((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbStatusHR"), rm, ci);
            var rapDes = new colRaporteDesign(idNdermarrje, idRaporti).MerrDizajnTeZgjedhur();
            ImbReportToolbar1.mbushComboOrientimi(rapDes);

            mbushComboMonedha(idPerdoruesi, idRaporti);
            mbushComboKonvertoNe(idPerdoruesi, idRaporti);
            mbushComboStandarte(idNdermarrje);

            mbushComboLikuiduar(ci);
            mbushcomboLlojArtikulli(ci, idRaporti);
            mbushcomboLlojSubjekti(ci);
            mbushComboStatus(ci);
            mbushComboLlojiArtBurim(ci);
            mbushComboVeprimePeriudhe(ci);
            mbushComboGjendjeDetyrimi(ci);
            mbushComboStatusCRM(ci);
            mbushComboGjendjeArt(ci, idRaporti);
            mbushComboShfaqArt(ci, idRaporti);
            mbushComboLlojPorosie(ci, idRaporti);
            mbushComboStatusFaturuar(ci, idRaporti);
            mbushComboStatusPerfunduar(ci, idRaporti);
            mbushComboStatusProdhuar(ci, idRaporti);
            mbushComboEmertimLlog(ci, idGjuha);
            mbushComboGjendja(ci);
            mbushComboAktiv(ci);
            mbushComboPaguar(ci, idRaporti);
            mbushComboLlojDetajim(ci, idRaporti);
            mbushComboStatusAfatDok(ci);
            mbushComboCikli(ci);
            mbushComboShfaqVlerat(ci);
            mbushComboPasqyra(idNdermarrje, idGjuha);
            mbushComboNiveli(idPerdoruesi, idNdermarrje);
            mbushComboTipGrafiku(ci);
            mbushComboViteNderm(idNdermarrje, (ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbViti"), true);
            if (idRaporti == 253 || idRaporti == 263)
                mbushComboMuajtInterval2Mujore();
            else
                mbushComboMuajt((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbMuaji"), idGjuha);
            mbushComboTipGrafikuKrahasues(ci);
            mbushComboStatusKonvertimiDyte(ci);
            mbushComboStatusKonvertimi(ci);
            ASPxComboBox combo = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbFormatNumri");
            ConfigureAspxComboBox.mbushComboFormateNumrashNew(combo);
            combo.SelectedIndex = 2;
            ConfigureAspxComboBox.mbushComboStatusMagazine(idNdermarrje, (ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbStatusMagazine"));
            ConfigureAspxComboBox.mbushComboKlasaArtikullit((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbKlasaArtikullit1"));
            ConfigureAspxComboBox.mbushComboKlasaArtikullit((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbKlasaArtikullit2"));
            ConfigureAspxComboBox.KonfiguroComboBoxComboQytete(IdNdermarrja, (ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbQyteti1"));
            ConfigureAspxComboBox.KonfiguroComboBoxComboQytete(IdNdermarrja, (ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbQyteti2"));
            ConfigureAspxComboBox.mbushComboMenyrePagese((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbMenyrePagese1"), true);
            ConfigureAspxComboBox.mbushComboMenyrePagese((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbMenyrePagese2"), true);
            ConfigureAspxComboBox.mbushComboLlojVeprimi((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLlojVeprimi1"), rm, ci, String.Empty);
            ConfigureAspxComboBox.mbushComboLlojVeprimi((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLlojVeprimi2"), rm, ci, String.Empty);

            ConfigureAspxComboBox.mbushComboMenuja((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbMenu1"));
            ConfigureAspxComboBox.mbushComboMenuja((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbMenu2"));
            ConfigureAspxComboBox.KonfiguroComboBoxNjesiKohe((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbNjesiKohe"), 2);
            ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("dteDtMaturimi")).Date = DateTime.Today;
            AspxWebControlUtils.vendosDateEditMask(((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("dteDtMaturimi")));
            if (idRaporti == 268)
            {
                ((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtInterval1")).Text = "0";
                ((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtInterval2")).Text = "90";
                ((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtInterval3")).Text = "180";
                ((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtInterval4")).Text = "270";
                ((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtInterval5")).Text = "365";
            }
            else
            {
                ((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtInterval1")).Text = "0";
                ((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtInterval2")).Text = "30";
                ((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtInterval3")).Text = "60";
                ((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtInterval4")).Text = "90";
                ((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtInterval5")).Text = "120";
                ((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtInterval6")).Text = "150";
            }
            //if(idRaporti==188)
            //    ((ASPxCheckBox)navBarFiltrat.Groups[1].FindControl("cbShfaqLlogP")).Checked = false;
            //else
            //    ((ASPxCheckBox)navBarFiltrat.Groups[1].FindControl("cbShfaqLlogP")).Checked = true;

            //Per raportin e marzhit te shitjes dhe grafikut te marzhit te shitjes vendosim
            //vlerat default te filtrit klasa e artikullit : != sherbim dhe != pastokueshem
            if (idRaporti == 43 || idRaporti == 67 || idRaporti == 190 || idRaporti == 211)
                percaktoVleraDefaultFiltriKlasaArtikullit();

        }

        private void percaktoVleraDefaultFiltriKlasaArtikullit()
        {
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimKlasaArtikullit1")).SelectedIndex = 3;
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbKlasaArtikullit1")).SelectedIndex = 2;
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaKlasaArtikullit")).SelectedIndex = 1;
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimKlasaArtikullit2")).SelectedIndex = 3;
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbKlasaArtikullit2")).SelectedIndex = 3;
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLlojArtikulli")).SelectedIndex = 1;
        }


        private void konfigKontrolleRaporti(int idRaporti)
        {

        }
        /// <summary>
        /// Gjen minimumin ne nje liste te dhene duke filluar nga indexi i dhene
        /// </summary>
        /// <param name="lista">lista duhet te jete me numra</param>
        /// <param name="startIndex">indeksi nga do filloj kerkimi perfshihet vet indexi</param>
        /// <returns>kthen indeksin ku ndodhet minimumi</returns>
        private static int minIndex(ArrayList lista, int startIndex)
        {
            int minIndex = -1;
            int min = Int16.MaxValue;
            if (startIndex >= lista.Count)
                return minIndex;
            for (int i = startIndex; i < lista.Count; i++)
            {
                if (min > int.Parse(lista[i].ToString()))
                {
                    min = int.Parse(lista[i].ToString());
                    minIndex = i;
                }
            }
            return minIndex;
        }

        /// <summary>
        /// Rendit listat nga me i vogli te me i madhi duke eliminuar gropat dhe shumezon me lartesia hap elementet
        /// </summary>
        /// <param name="emerGrupKontrolli"></param>
        /// <param name="grupKontrolliTop"></param>
        /// <param name="lartesiaHap"></param>
        private static void rendit(ArrayList emerGrupKontrolli, ArrayList grupKontrolliTop, int lartesiaHap)
        {
            double lartesiGrup = lartesiaHap * emerGrupKontrolli.Count;
            for (int i = 0; i < emerGrupKontrolli.Count; i++)
            {
                int min = minIndex(grupKontrolliTop, i);
                if (min != -1)
                {
                    string temp = emerGrupKontrolli[min].ToString();
                    emerGrupKontrolli[min] = emerGrupKontrolli[i];
                    emerGrupKontrolli[i] = temp;

                    grupKontrolliTop[min] = grupKontrolliTop[i];
                    grupKontrolliTop[i] = (i * lartesiaHap / lartesiGrup * 100).ToString();
                }
            }
        }

        private void merrKontrolleFiltra(int idRaporti, int idNdermarrje, int idPerdoruesi, clsPeriudhaKontabel periudha)
        {
            colKontrolle kontrolleRaporti = new colKontrolle();
            colKontrolle kontrolleKomponente = new colKontrolle();
            kontrolleKomponente.merrKontrolletKomponentes(new clsKomponente("Raporti.aspx").IdKomponente);
            kontrolleRaporti.merrKontrolletRaporti(idRaporti);
            colGrupKontrolli grupetEKontrolleve = kontrolleRaporti.merrGrupet();
            // string [] emerGrupKontrolli = new string[grupetEKontrolleve.Count];
            //string[] grupKontrolliTop = new string[grupetEKontrolleve.Count];            
            int lartesiFillestare = 0;
            int lartesiHap = 30;
            int[] lartesia = new int[navBarFiltrat.Groups.Count];
            foreach (int k in lartesia)
                lartesia[k] = lartesiFillestare;
            ArrayList emerGrupKontrolliKryesor = new ArrayList();
            ArrayList emerGrupKontrolliAvancuar = new ArrayList();
            ArrayList grupKontrolliKryesorTop = new ArrayList();
            ArrayList grupKontrolliAvancuarTop = new ArrayList();
            ArrayList kontrollet = new ArrayList();
            //clsPeriudhaKontabel periudha = (clsPeriudhaKontabel)CacheLayer.GlobalCacheManager.MySessionCache["oPeriudhaAktuale"];
            //clsPeriudhaKontabel periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            foreach (clsKontroll kontrolli in kontrolleKomponente)
            {
                for (int i = 0; i < navBarFiltrat.Groups.Count; i++)
                {
                    System.Web.UI.Control uiKontroll = navBarFiltrat.Groups[i].FindControl(kontrolli.KodKontrolli);
                    if (uiKontroll == null)
                        continue;
                    if (!kontrolleRaporti.ekziston(kontrolli.IdKontrolli))
                    {
                        uiKontroll.Visible = false;
                        continue;
                    }
                    uiKontroll.Visible = true;
                    kontrollet.Add(kontrolli);
                    clsGrupKontrolli grupi = grupetEKontrolleve.ktheGrup(kontrolli.IdGrupi);
                    ArrayList emerGrupKontrolli;
                    ArrayList grupKontrolliTop;
                    if (i == 0) //grupi kryesor i filtrave
                    {
                        emerGrupKontrolli = emerGrupKontrolliKryesor;
                        grupKontrolliTop = grupKontrolliKryesorTop;
                    }
                    else //grupi i filtrave te avancuar
                    {
                        emerGrupKontrolli = emerGrupKontrolliAvancuar;
                        grupKontrolliTop = grupKontrolliAvancuarTop;
                    }
                    if (!emerGrupKontrolli.Contains(grupi.EmerGrupi))
                    {
                        emerGrupKontrolli.Add(grupi.EmerGrupi);
                        grupKontrolliTop.Add((grupi.Rend).ToString());
                    }
                    if (kontrolli.KodKontrolli.StartsWith("cmbMonedha") || kontrolli.KodKontrolli.StartsWith("cmbKonvertoNe"))
                    {
                        if (idRaporti == 257)
                        {
                            ASPxComboBox cmbMonedha = (ASPxComboBox)uiKontroll;
                            cmbMonedha.Items.Add("Monedhe QK", 0);
                            cmbMonedha.Items.Add("Monedhe baze", 1);
                            cmbMonedha.SelectedIndex = 1;
                            cmbMonedha.DataBind();
                        }
                        else
                            ConfigureAspxComboBox.mbushComboMonedha(idPerdoruesi, idNdermarrje, true, (ASPxComboBox)uiKontroll);
                        continue;
                    }
                    if (kontrolli.KodKontrolli.StartsWith("cmbStandarti"))
                    {
                        ConfigureAspxComboBox.mbushComboStandartAmortizimi((ASPxComboBox)uiKontroll, idNdermarrje);
                        continue;
                    }
                    if (kontrolli.KodKontrolli.StartsWith("cmbStatusMagazine"))
                    {
                        ConfigureAspxComboBox.mbushComboStatusMagazine((ASPxComboBox)uiKontroll, idNdermarrje);
                        continue;
                    }
                    if (idRaporti == 65 || idRaporti == 66 || idRaporti == 229)
                        if (kontrolli.KodKontrolli.StartsWith("radDtDok"))
                        {
                            ((ASPxRadioButtonList)uiKontroll).Value = "VitiUshtrimor";
                        }
                    if (kontrolli.KodKontrolli.Equals("txtNgaDokFshirje"))
                    {
                        if (periudha != null)
                            ((ASPxDateEdit)uiKontroll).Date = periudha.FillimiPeriudha;
                        continue;
                    }
                    if (kontrolli.KodKontrolli.Equals("txtDeriDokFshirje"))
                    {
                        if (periudha != null)
                            ((ASPxDateEdit)uiKontroll).Date = periudha.MbarimiPeriudha;
                        continue;
                    }

                    if (kontrolli.KodKontrolli.Equals("txtNgaDokKonvertuar"))
                    {
                        if (periudha != null)
                            ((ASPxDateEdit)uiKontroll).Date = periudha.FillimiPeriudha;
                        continue;
                    }
                    if (kontrolli.KodKontrolli.Equals("txtDeriDokKonvertuar"))
                    {
                        if (periudha != null)
                            ((ASPxDateEdit)uiKontroll).Date = periudha.MbarimiPeriudha;
                        continue;
                    }

                    if (kontrolli.KodKontrolli.Equals("txtNgaDok"))
                    {
                        if (periudha != null)
                            ((ASPxDateEdit)uiKontroll).Date = periudha.FillimiPeriudha;
                        continue;
                    }

                    if (kontrolli.KodKontrolli.Equals("txtDeriDok"))
                    {
                        if (periudha != null)
                            ((ASPxDateEdit)uiKontroll).Date = periudha.MbarimiPeriudha;
                        continue;
                    }

                    if (kontrolli.KodKontrolli.Equals("txtNgaDtPlanifikimi"))
                    {
                        if (periudha != null)
                            ((ASPxDateEdit)uiKontroll).Date = periudha.FillimiPeriudha;
                        continue;
                    }

                    if (kontrolli.KodKontrolli.StartsWith("txtDeriDtPlanifikimi"))
                    {
                        if (periudha != null)
                            ((ASPxDateEdit)uiKontroll).Date = periudha.MbarimiPeriudha;
                    }

                    if (kontrolli.KodKontrolli.Equals("txtNgaDtProdhimi"))
                    {
                        if (periudha != null)
                            ((ASPxDateEdit)uiKontroll).Date = periudha.FillimiPeriudha;
                        continue;
                    }

                    if (kontrolli.KodKontrolli.StartsWith("txtDeriDtProdhimi"))
                    {
                        if (periudha != null)
                            ((ASPxDateEdit)uiKontroll).Date = periudha.MbarimiPeriudha;
                    }

                    if (idRaporti == 66)
                        if (kontrolli.KodKontrolli.StartsWith("radDtKrahasues"))
                        {
                            ((ASPxRadioButtonList)uiKontroll).Value = "VitiUshtrimor";
                        }

                    if (kontrolli.KodKontrolli.StartsWith("txtNgaDtKrahasues"))
                    {
                        if (periudha != null)
                            ((ASPxDateEdit)uiKontroll).Date = periudha.FillimiPeriudha.AddYears(-1);
                    }

                    if (kontrolli.KodKontrolli.Equals("txtDeriDtKrahasues"))
                    {
                        if (periudha != null)
                            ((ASPxDateEdit)uiKontroll).Date = periudha.MbarimiPeriudha.AddYears(-1);
                        continue;
                    }


                    if (kontrolli.KodKontrolli.Equals("txtNgaRegj"))
                    {
                        DateTime fillimiPeriudha = new DateTime(1900, 01, 01);
                        ((ASPxDateEdit)uiKontroll).Date = fillimiPeriudha;
                        continue;
                    }

                    if (kontrolli.KodKontrolli.Equals("txtDeriRegj"))
                    {
                        DateTime mbarimiPeriudha = DateTime.MaxValue.AddDays(-1);
                        ((ASPxDateEdit)uiKontroll).Date = mbarimiPeriudha;
                        continue;
                    }
                    if (kontrolli.KodKontrolli.Equals("txtNgaDtFillimi"))
                    {
                        DateTime fillimiPeriudha = new DateTime(1900, 01, 01);
                        ((ASPxDateEdit)uiKontroll).Date = fillimiPeriudha;
                        continue;
                    }

                    if (kontrolli.KodKontrolli.Equals("txtDeriDtFillimi"))
                    {
                        DateTime mbarimiPeriudha = DateTime.MaxValue.AddDays(-1);
                        ((ASPxDateEdit)uiKontroll).Date = mbarimiPeriudha;
                        continue;
                    }

                    if (kontrolli.KodKontrolli.Equals("txtNgaDokAfatKohor"))
                    {
                        DateTime fillimiPeriudha = new DateTime(1900, 01, 01);
                        ((ASPxDateEdit)uiKontroll).Date = fillimiPeriudha;
                        continue;
                    }
                    if (kontrolli.KodKontrolli.Equals("txtDeriDokAfatKohor"))
                    {
                        DateTime mbarimiPeriudha = DateTime.MaxValue.AddDays(-1);
                        ((ASPxDateEdit)uiKontroll).Date = mbarimiPeriudha;
                        continue;
                    }

                    if (kontrolli.KodKontrolli.StartsWith("txtNgaPeriudheMaturimi"))
                    {
                        DateTime fillimiPeriudha = new DateTime(1900, 01, 01);
                        if (idRaporti == 209)
                            fillimiPeriudha = DateTime.Today;
                        ((ASPxDateEdit)uiKontroll).Date = fillimiPeriudha;
                        continue;
                    }

                    if (kontrolli.KodKontrolli.StartsWith("txtDeriPeriudheMaturimi"))
                    {
                        DateTime mbarimiPeriudha = DateTime.MaxValue.AddDays(-1);
                        if (idRaporti == 209)
                            mbarimiPeriudha = DateTime.Today;
                        ((ASPxDateEdit)uiKontroll).Date = mbarimiPeriudha;
                        continue;
                    }

                    if (kontrolli.KodKontrolli.StartsWith("txtNgaPeriudheFillimi"))
                    {
                        DateTime fillimiPeriudha = new DateTime(1900, 01, 01);
                        ((ASPxDateEdit)uiKontroll).Date = fillimiPeriudha;
                        continue;
                    }
                    if (kontrolli.KodKontrolli.StartsWith("txtDeriPeriudheFillimi"))
                    {
                        DateTime mbarimiPeriudha = DateTime.MaxValue.AddDays(-1);
                        ((ASPxDateEdit)uiKontroll).Date = mbarimiPeriudha;
                        continue;
                    }


                    if (kontrolli.KodKontrolli.StartsWith("txtNgaPeriudheMbarimi"))
                    {
                        DateTime fillimiPeriudha = new DateTime(1900, 01, 01);
                        ((ASPxDateEdit)uiKontroll).Date = fillimiPeriudha;
                        continue;
                    }

                    if (kontrolli.KodKontrolli.StartsWith("txtDeriPeriudheMbarimi"))
                    {
                        DateTime mbarimiPeriudha = DateTime.MaxValue.AddDays(-1);
                        ((ASPxDateEdit)uiKontroll).Date = mbarimiPeriudha;
                        continue;
                    }
                    if (kontrolli.KodKontrolli.StartsWith("txtNgaDtStatus"))
                    {

                        if (periudha != null)
                            ((ASPxDateEdit)uiKontroll).Date = periudha.FillimiPeriudha;
                        continue;
                    }
                    if (kontrolli.KodKontrolli.StartsWith("txtDeriDtStatus"))
                    {
                        if (periudha != null)
                            ((ASPxDateEdit)uiKontroll).Date = periudha.MbarimiPeriudha;
                        continue;
                    }

                }
            }
            rendit(emerGrupKontrolliKryesor, grupKontrolliKryesorTop, lartesiHap);
            rendit(emerGrupKontrolliAvancuar, grupKontrolliAvancuarTop, lartesiHap);

            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            hfKontrolle.Value = serializusi.Serialize(kontrollet.ToArray());
            hfIdKrye.Value = serializusi.Serialize(emerGrupKontrolliKryesor.ToArray());
            hfTopKrye.Value = serializusi.Serialize(grupKontrolliKryesorTop.ToArray());
            hfIdAvanc.Value = serializusi.Serialize(emerGrupKontrolliAvancuar.ToArray());
            hfTopAvanc.Value = serializusi.Serialize(grupKontrolliAvancuarTop.ToArray());
            #region komentuar
            //            string[] kontrollEmrit0 = { "radDtDok", "txtNgaDok", "txtDeriDok", "lblDtDok", "lblNgaDok", "lblDeriDok" };
            //            for (int i = 0; i < kontrollEmrit0.Length; i++)
            //                if (navBarFiltrat.Groups[0].FindControl(kontrollEmrit0[i]) != null)
            //                    navBarFiltrat.Groups[0].FindControl(kontrollEmrit0[i]).Visible = false;

            //            string[] kontrollEmrit1 ={"radDtRegj","txtNgaRegj","txtDeriRegj","lblDtRegj","lblngaRegj","lblDeriRegj", "txtPershk2","cmbVeprimPershk2","cmbLidhesaPershk","txtPershk1","cmbVeprimPershk1","txtRef2","cmbVeprimRef2","cmbLidhesaRef","txtRef1",
            //                                        "cmbVeprimRef1","txtNrDok2","cmbVeprimDok2","cmbLidhesaDok","txtNrDok1","cmbVeprimDok1","txtBtnNrLlog2","cmbVeprim2","cmbLidhesa",
            //                                        "txtBtnNrLlog1","cmbVeprim1","cmbGjendjaLlog","txtBtnLlogKoresp", "cmbMonedha","txtBtnLlojDok","cmbFormati", "cmbTipi" ,"cmbNengrupi","cmbGrupi",
            //                                        "lblTipi","lblTipiPershk","lblFormali","lblLlogKoresp","lblLlogKorespPershk","lblGjendjaLlog","lblGrupiPershk","lblNengrupi","lblNengrupiPershk",
            //                                        "lblNrDok","lblReferenca","lblPershkrimi","lblLlojDok","lblLlojDokumenti","lblMonedha","lblMonedhaPershk","lblGrupi","lblNrLlog","btnPastro","btnApliko",
            //                                        "lblKlientFurnitori","cmbVeprimiKF1","btneKodKF","cmbLidhesaKF","cmbVeprimiKF2","btneKodKF2","lblDtMaturimi","dteDtMaturimi","lblMonedheKF","cbMonedheKF",
            //                                     "lblIntervalet","txtInterval1","txtInterval2","txtInterval3","txtInterval4","txtInterval5","txtInterval6","lblNiveli","cmbNiveli","lblNiveliPershk",
            //                                     "lblmagazina","txtBtnMagazina","lblkartela","txtBtnKartela","lblperdoruesi","txtBtnPerdoruesi"};
            //            for (int i = 0; i < kontrollEmrit1.Length; i++)
            //                if (navBarFiltrat.Groups[1].FindControl(kontrollEmrit1[i]) != null)
            //                    navBarFiltrat.Groups[1].FindControl(kontrollEmrit1[i]).Visible = false;

            //            string[] kontrollefill ={"lblDtRegj","lblNrLlog","lblNrDok","lblReferenca","lblPershkrimi","lblKlientFurnitori","lblLlojDok","lblmagazina","lblkartela","lblperdoruesi",
            //"lblMonedha","lblGrupi","lblNengrupi","lblTipi","lblNiveli","lblDtMaturimi","lblMonedheKF","lblIntervalet","lblFormali",
            //"lblLlogKoresp","lblGjendjaLlog"};
            //            int topfill = 0;
            //            int[] top = new int[kontrollefill.Length];

            //            //clsFilterPerRaport clsfilrap = new clsFilterPerRaport(idRaporti);
            //            colFilterPerRaport colfilrap = new colFilterPerRaport();

            //          //  colfilrap.merrFilterPerRaport(idRaporti);
            //            if (colfilrap.Count > 0)
            //            {
            //                for (int j = 0; j < colfilrap.Count; j++)
            //                {
            //                    //clsKomponentPerFilter clskompfil = new clsKomponentPerFilter(colfilrap[j].IdFilter);
            //                    colKomponentPerFilter colkompfil = new colKomponentPerFilter();

            //                    colkompfil.merrKomponentPerFilter(colfilrap[j].IdFilter);

            //                    foreach (clsKomponentPerFilter clskompfiler in colkompfil)
            //                    {
            //                        if (navBarFiltrat.Groups[0].FindControl(clskompfiler.KomponenteEmri) != null)
            //                            navBarFiltrat.Groups[0].FindControl(clskompfiler.KomponenteEmri).Visible = true;
            //                        else
            //                        {
            //                            if (navBarFiltrat.Groups[1].FindControl(clskompfiler.KomponenteEmri) != null)
            //                                navBarFiltrat.Groups[1].FindControl(clskompfiler.KomponenteEmri).Visible = true;
            //                            if (clskompfiler.KomponenteEmri.ToLower().StartsWith("cmbmon"))
            //                                funksione.mbushComboMonedha((ASPxComboBox)navBarFiltrat.Groups[1].FindControl(clskompfiler.KomponenteEmri));
            //                        }

            //                        for (int l = 0; l < kontrollefill.Length; l++)
            //                        {
            //                            if (kontrollefill[l] == clskompfiler.KomponenteEmri)
            //                            {
            //                                top[l] = 34 + topfill * 4;
            //                                topfill++;
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //           hftop.Value = string.Join(",", top);
            #endregion
        }


        private void mbushComboMonedha(int idPerdoruesi, int idRaporti)
        {//mbush kombon e monedhes me te dhena nga databasa
            ASPxComboBox cmbMonedha = new ASPxComboBox();
            string kontrollEmri = "cmbMonedha";
            if (idRaporti == 257)
            {
                cmbMonedha.Items.Add("Monedhe QK", 0);
                cmbMonedha.Items.Add("Monedhe baze", 1);
                cmbMonedha.SelectedIndex = 1;
            }
            else
            {
                colMonedhat colMonedha = new colMonedhat();
                //clsPerdorues perdoruesi = DbCore.mySessionObjects.kthePerdorues(Session);
                //colMonedha.mbushGjitheMonedhatAktive(4, perdoruesi.IdPerdoruesi);
                //colMonedha = dbAdmin.merrGjitheMonedhatAktive(4, oPerdorues.IdPerdoruesi);
                colMonedha.mbushGjitheMonedhatAktive(4, idPerdoruesi);
                cmbMonedha = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
                cmbMonedha.DataSource = colMonedha;
                cmbMonedha.TextField = "KodiMonedha";
                cmbMonedha.ValueField = "IdMonedha";
            }
            cmbMonedha.DataBind();
        }

        private void mbushComboKonvertoNe(int idPerdoruesi, int idRaporti)
        {//mbush kombon e monedhes me te dhena nga databasa
            ASPxComboBox cmbKonvertoNe = new ASPxComboBox();
            string kontrollEmri = "cmbKonvertoNe";

            colMonedhat colMonedha = new colMonedhat();

            colMonedha.mbushGjitheMonedhatAktive(4, idPerdoruesi);
            cmbKonvertoNe = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbKonvertoNe.DataSource = colMonedha;
            cmbKonvertoNe.TextField = "KodiMonedha";
            cmbKonvertoNe.ValueField = "IdMonedha";
            cmbKonvertoNe.DataBind();
        }
        private void mbushComboStandarte(int idNdermarje)
        {//mbush kombon e monedhes me te dhena nga databasa
            //clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            colStandarteAmortizimi standarte = new colStandarteAmortizimi();
            //clsPerdorues perdoruesi = DbCore.mySessionObjects.kthePerdorues(Session);
            //colMonedha.mbushGjitheMonedhatAktive(4, perdoruesi.IdPerdoruesi);
            //colMonedha = dbAdmin.merrGjitheMonedhatAktive(4, oPerdorues.IdPerdoruesi);
            standarte.merrStandarteAmortizimiTeNdermarrjes(idNdermarje);
            ASPxComboBox cmbStandarti = new ASPxComboBox();
            string kontrollEmri = "cmbStandarti";
            cmbStandarti = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbStandarti.DataSource = standarte;
            cmbStandarti.TextField = "PERSHKRIMI";
            cmbStandarti.ValueField = "IDSTANDARTI";
            cmbStandarti.SelectedIndex = 0;
            cmbStandarti.DataBind();
        }


        private void mbushGrupDokumenti1(int idgrupi, string emri, int idNdermarrje, int idPerdoruesi)
        {//mbush kombon e monedhes me te dhena nga databasa
            DbCore.DbRegjistrim.colGrupimDokumentiKoka col = new DbCore.DbRegjistrim.colGrupimDokumentiKoka();
            col.Add(new DbCore.DbRegjistrim.clsGrupimDokumentiKoka());
            clsRaporti rap = new clsRaporti(0, idRaporti);
            switch (rap.IdModul)
            {
                case 12:
                    col.merrGrupeSipasKategorise(idgrupi, idNdermarrje, "1", idPerdoruesi);
                    break;
                case 13:
                    col.merrGrupeSipasKategorise(idgrupi, idNdermarrje, "2", idPerdoruesi);
                    break;
                case 16:
                    col.merrGrupeSipasKategorise(idgrupi, idNdermarrje, "6", idPerdoruesi);
                    break;
                case 2:
                    col.merrGrupeSipasKategorise(idgrupi, idNdermarrje, "3", idPerdoruesi);
                    break;
                case 6:
                    col.merrGrupeSipasKategorise(idgrupi, idNdermarrje, "4", idPerdoruesi);
                    break;
                case 18:
                    col.merrGrupeSipasKategorise(idgrupi, idNdermarrje, "44,45,46", idPerdoruesi);
                    break;
            }
            ASPxComboBox cmbGrup1 = new ASPxComboBox();
            string kontrollEmri = emri;
            cmbGrup1 = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);

            cmbGrup1.DataSource = col;
            cmbGrup1.TextField = "Kodi";
            cmbGrup1.ValueField = "IdGrupimKoka";
            cmbGrup1.DataBind();
        }

        private void mbushComboQyteti(int idNdermarrje)
        {
            colQytetet colQyt = new colQytetet();
            colQyt.mbushGjitheQytetetPozitive(idNdermarrje);

            ASPxComboBox cmbQyteti1 = new ASPxComboBox();
            ASPxComboBox cmbQyteti2 = new ASPxComboBox();
            cmbQyteti1 = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbQyteti1");
            cmbQyteti2 = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbQyteti2");
            cmbQyteti1.DataSource = colQyt;
            cmbQyteti2.DataSource = colQyt;
            cmbQyteti1.TextField = "KODIQYTETI";
            cmbQyteti2.TextField = "KODIQYTETI";
            cmbQyteti1.ValueField = "IDQYTETI";
            cmbQyteti2.ValueField = "IDQYTETI";
            cmbQyteti1.DataBind();
            cmbQyteti2.DataBind();
        }

        private void pastroFiltra()
        {


        }

        private void btnPastro_Click(object sender, EventArgs e)
        {
            pastroFiltra();
        }

        private DateTime ktheVlereDateLibrat(int idNdermarrje, int idViti, int idNderViti, clsPeriudhaKontabel periudhaKontabel)
        {
            DateTime dtmbarimi = DateTime.Today;
            switch (((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDok")).SelectedItem.Value.ToString())
            {
                case "Aktuale":
                    if (periudhaKontabel != null)
                        dtmbarimi = periudhaKontabel.MbarimiPeriudha;
                    break;
                case "VitiUshtrimor":
                    if (idNderViti != 0)
                    {
                        dtmbarimi = new clsNdermarrjeViti(idNderViti).NdermarrjeVitiFund;

                    }
                    break;
                case "Periudha":
                    dtmbarimi = ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDok")).Date;
                    break;
                case "GjitheVitet":
                    {
                        clsNdermarrjeViti ndermviti = new clsNdermarrjeViti();
                        ndermviti.mbushNdermVitFillimFundPerGjitheVitetSipasNdermarjes(idNdermarrje);
                        dtmbarimi = ndermviti.NdermarrjeVitiFund;
                    }
                    break;

                default:
                    break;

            }
            return dtmbarimi;
        }
        private string ktheVlerenEKontrollit(clsKontroll kontrolli, string tipiKontrollit)
        {
            string kodKontrolli = kontrolli.KodKontrolli;
            for (int g = 0; g < navBarFiltrat.Groups.Count; g++) //per momentin nuk po e marr parasysh daten
            {
                System.Web.UI.Control uiKontroll = navBarFiltrat.Groups[g].FindControl(kodKontrolli);
                if (uiKontroll != null)
                {
                    switch (tipiKontrollit)
                    {
                        #region ASPxLabel
                        case "ASPxLabel": //nuk behet gje me label-at
                            continue;
                        #endregion

                        #region ASPxTextBox
                        case "ASPxTextBox":
                            ASPxTextBox textBoxi = (ASPxTextBox)uiKontroll;
                            return textBoxi.Text.Trim();
                        #endregion

                        #region ASPxComboBox
                        case "ASPxComboBox":
                            ASPxComboBox combo = (ASPxComboBox)uiKontroll;
                            return combo.Text.Trim();
                        #endregion

                        #region ASPxDateEdit
                        case "ASPxDateEdit":
                            ASPxDateEdit dateEdit = (ASPxDateEdit)uiKontroll;
                            return dateEdit.Text.Trim();
                        #endregion

                        #region ASPxButtonEdit
                        case "ASPxButtonEdit":
                            ASPxButtonEdit buttonEdit = (ASPxButtonEdit)uiKontroll;
                            return buttonEdit.Text.Trim();
                        #endregion

                        #region RadioButtonList
                        case "RadioButtonList":
                            ASPxRadioButtonList radioKontrolli = ((ASPxRadioButtonList)uiKontroll);
                            return radioKontrolli.SelectedItem.Value.ToString();
                        #endregion

                        #region CheckBox
                        case "ASPxCheckBox":
                            ASPxCheckBox check = (ASPxCheckBox)uiKontroll;
                            return check.Checked.ToString().ToLower();
                        #endregion
                        default:
                            return "";
                    }
                }
            }
            return "";
        }

        protected void btnAfishoRaport_Click(object sender, EventArgs e)
        {
        }

        private string ktheVlereParametri(clsParameter parametri, int idNdermarrje, clsPeriudhaKontabel periudhaKontabel, int idNderviti, int idPerdorues, int idGjuha, colKontrolle oColKontrolleRaporti)
        {
            try
            {
                string vlera = "";
                switch (parametri.Emri.ToLower())
                {
                    case "idraport":
                        return vlera = idRaporti.ToString();
                    case "idndermarje":
                        return vlera = idNdermarrje.ToString();// DbCore.mySessionObjects.ktheNdermarrjeVit(Session).ToString();
                    case "salt":
                        return vlera = System.Web.Configuration.WebConfigurationManager.AppSettings["salt"];
                    case "idnderviti":
                        return vlera = idNderviti.ToString();
                    case "idperdoruesi":
                        return vlera = idPerdorues.ToString();
                    case "shikogjithedokumentat":
                        return vlera = hfTeDrejtaRaporti.Get("dGjitheDok").ToString();
                    case "idpasqyrafinaciarekoka":
                        return vlera = ((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbPasqyra")).Value.ToString();
                    case "filterkodifikimartp":
                        object tmpValue1 = ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("GrupimPareButtonEdit1")).Value;
                        return vlera = tmpValue1 == null ? "" : tmpValue1.ToString();
                    case "filterkodifikimartd":
                        object tmpValue2 = ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("GrupimDyteButtonEdit1")).Value;
                        return vlera = tmpValue2 == null ? "" : tmpValue2.ToString();                    
                    case "txtinterval1":
                        int interval1 = 0;
                        int.TryParse(((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtInterval1")).Text, out interval1);
                        return vlera = interval1.ToString();
                    case "txtinterval2":
                        int interval2 = 0;
                        int.TryParse(((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtInterval2")).Text, out interval2);
                        return vlera = interval2.ToString();
                    case "txtinterval3":
                        int interval3 = 0;
                        int.TryParse(((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtInterval3")).Text, out interval3);
                        return vlera = interval3.ToString();
                    case "txtinterval4":
                        int interval4 = 0;
                        int.TryParse(((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtInterval4")).Text, out interval4);
                        return vlera = interval4.ToString();
                    case "txtinterval5":
                        int interval5 = 0;
                        int.TryParse(((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtInterval5")).Text, out interval5);
                        return vlera = interval5.ToString();
                    case "txtinterval6":
                        int interval6 = 0;
                        int.TryParse(((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtInterval6")).Text, out interval5);
                        return vlera = interval6.ToString();
                    case "filterpikeshitjefurnizmi":
                        if (idRaporti == 256)
                            return ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("pikeShFButtonEdit1")).Text;
                        break;
                    case "filterfurnitor":
                        if (idRaporti == 256)
                            return ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("btneKodKF1")).Text;
                        break;
                    case "filterdegeadministrative":
                        if (idRaporti == 256)
                            return ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("degeAdminButtonEdit1")).Text;
                        break;
                    default:
                        break;
                }
                List<clsKontroll> oColKontrolle = oColKontrolleRaporti.FindAll(x => x.IdGrupi == parametri.IdGrupKontroll);
                //oColKontrolle.merrKontrollet(parametri.IdParametri);
                CultureInfo ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
                string veprimiPara1 = "";
                string veprimiPara2 = "";
                string vlere1 = "";
                string vlere2 = "";
                string lidhes = "";
                //colTipeKontrolli tipet = new colTipeKontrolli();
                //tipet.mbushTipet();
                string kolonaFilter = parametri.KolonaDb;
                foreach (clsKontroll kontrolli in oColKontrolle)
                {
                    string kodKontrolli = kontrolli.KodKontrolli;
                    string tipiKontrollit = Enum.GetName(typeof(DbCore.DbShare.TipeKontrolli), kontrolli.IdTipiKontrollit); //tipet.merrTipin(kontrolli.IdTipiKontrollit).TipiKontrollit;
                    for (int g = 0; g < navBarFiltrat.Groups.Count; g++) //per momentin nuk po e marr parasysh daten
                    {
                        System.Web.UI.Control uiKontroll = navBarFiltrat.Groups[g].FindControl(kodKontrolli);
                        if (uiKontroll != null)
                        {
                            switch (tipiKontrollit)
                            {
                                #region ASPxLabel
                                case "ASPxLabel": //nuk behet gje me label-at
                                    continue;
                                #endregion

                                #region ASPxTextBox
                                case "ASPxTextBox":
                                    ASPxTextBox textBoxi = (ASPxTextBox)uiKontroll;
                                    if (kodKontrolli.Equals("txtNrSigurimesh") || kodKontrolli.Equals("txtArsyeja") || kodKontrolli.Equals("txtKujtIAdresohet") || kodKontrolli.Equals("txtSAPID") || kodKontrolli.Equals("txtDrejtuar"))
                                    {
                                        return textBoxi.Text;
                                    }
                                    if (textBoxi.Text.Trim() == "")
                                        continue;
                                    if (kodKontrolli.Contains("1"))
                                    {
                                        vlere1 = textBoxi.Text;
                                        continue;
                                    }
                                    if (kodKontrolli.Contains("2"))
                                    {
                                        vlere2 = textBoxi.Text;
                                        continue;
                                    }
                                    if (kodKontrolli.ToLower().StartsWith("txtpershkrimfature"))
                                    {
                                        vlera += String.Format("{0} like '%{1}%' ", kolonaFilter, textBoxi.Text);
                                        continue;
                                    }
                                    if (kodKontrolli.ToLower().StartsWith("llogdebi_textbox"))
                                    {
                                        vlera += String.Format("{0} like '{1}%' ", kolonaFilter, textBoxi.Text + "/");
                                        continue;
                                    }
                                    if (kodKontrolli.ToLower().StartsWith("llogkredi_textbox"))
                                    {
                                        vlera += String.Format("{0} like '{1}%' ", kolonaFilter, textBoxi.Text + "/");
                                    }
                                    if (kodKontrolli.ToLower().StartsWith("dega_textbox"))
                                    {
                                        //string s = kolonaFilter.Substring(kolonaFilter.IndexOf('-') + 1, kolonaFilter.Length);
                                        vlera += String.Format("{0} like '%{1}' ", kolonaFilter, "-" + textBoxi.Text);
                                    }
                                    if (kodKontrolli.ToLower().StartsWith("txtemmbshoqeri"))
                                    {
                                        vlera += String.Format("{0} like '%{1}%' ", kolonaFilter, textBoxi.Text);
                                        continue;
                                    }
                                    if (kodKontrolli.ToLower().StartsWith("txtnrtel"))
                                    {
                                        //string s = kolonaFilter.Substring(kolonaFilter.IndexOf('-') + 1, kolonaFilter.Length);
                                        vlera += String.Format("{0} like '%{1}%' ", kolonaFilter, textBoxi.Text);
                                        continue;
                                    }
                                    if (kodKontrolli.ToLower().StartsWith("txtcustomernumber"))
                                    {
                                        //string s = kolonaFilter.Substring(kolonaFilter.IndexOf('-') + 1, kolonaFilter.Length);
                                        vlera += String.Format("{0} like '%{1}%' ", kolonaFilter, textBoxi.Text);
                                    }
                                    if (kodKontrolli.ToLower().StartsWith("txtvlereshitjep"))
                                    {
                                        vlera += String.Format("{0} > {1} ", kolonaFilter, textBoxi.Text);
                                        //vlera + " AND "
                                        //veprimiPara1 = "2";
                                    }
                                    if (kodKontrolli.ToLower().StartsWith("txtvlereshitjed"))
                                    {
                                        if (vlera == "")
                                            vlera += String.Format(" {0} < {1} ", kolonaFilter, textBoxi.Text);
                                        else
                                            //string s = kolonaFilter.Substring(kolonaFilter.IndexOf('-') + 1, kolonaFilter.Length);
                                            //vlera += String.Format("{0} like '%{1}%' ", kolonaFilter, textBoxi.Text);
                                            vlera += String.Format("{2} {0} < {1} ", kolonaFilter, textBoxi.Text, " AND ");
                                        //veprimiPara2 = "1";
                                    }

                                    if (kodKontrolli.Equals("txtNrDok1"))
                                    {
                                        if (idRaporti == 256)
                                            return textBoxi.Text;
                                    }
                                    continue;

                                #endregion

                                #region ASPxComboBox
                                case "ASPxComboBox":
                                    ASPxComboBox combo = (ASPxComboBox)uiKontroll;
                                    if (combo.Text.Trim() == "")
                                        continue;
                                    if (kodKontrolli.ToLower().StartsWith("cmblidhesa") && !kodKontrolli.Equals("cmbLidhesaGrupimPKF") && !kodKontrolli.Equals("cmbLidhesaGrupimDKF") && !kodKontrolli.Equals("cmbLidhesaGrupimTKF")) // && !kodKontrolli.ToString().ToLower().StartsWith("cmbLidhesaPershk")
                                    {
                                        lidhes = combo.Value.ToString();
                                        continue;
                                    }
                                    if (kodKontrolli.ToLower().StartsWith("cmblidhesa") && (parametri.Emri.ToLower().Equals("filtergrupimpkf") || parametri.Emri.ToLower().Equals("filtergrupimdkf") || parametri.Emri.ToLower().Equals("filtergrupimtkf"))) // && !kodKontrolli.ToString().ToLower().StartsWith("cmbLidhesaPershk")
                                    {
                                        lidhes = combo.Value.ToString();
                                        continue;
                                    }
                                    if (kodKontrolli.StartsWith("cmbGrupP") || kodKontrolli.ToString().StartsWith("cmbGrupD") || kodKontrolli.ToString().StartsWith("cmbGrupT"))
                                    {
                                        if (kodKontrolli.Contains("1"))
                                        {
                                            if (idRaporti == 256)
                                                return combo.Text;
                                            vlere1 = combo.Text;
                                            vlera = "";
                                            continue;
                                        }
                                        if (kodKontrolli.Contains("2"))
                                        {
                                            vlere2 = combo.Text;
                                            vlera = "";
                                            continue;
                                        }
                                    }
                                    if (kodKontrolli == "cmbNjesiKohe" || kodKontrolli == "cmbBuxhetet")
                                    {
                                        return combo.Value.ToString();
                                    }
                                    if (kodKontrolli == "cmbMonedha")
                                    {
                                        if (idRaporti == 257)
                                            return combo.Value.ToString();
                                    }

                                    if (kodKontrolli == "cmbKonvertoNe")
                                    {

                                        return combo.Value.ToString();
                                    }
                                    if (kodKontrolli == "cmbMonedha")
                                    {
                                        if (idRaporti == 257)
                                            return combo.Value.ToString();
                                    }
                                    if (parametri.Emri.Equals("filterMuaji"))
                                    {
                                        if (idRaporti == 232 || idRaporti == 247)
                                        {
                                            vlera = String.Format("{0} = ('{1}')", kolonaFilter, combo.Text);
                                            continue;
                                        }
                                        else if (idRaporti == 253 || idRaporti == 263 || idRaporti == 291)
                                            return combo.Value.ToString();
                                    }

                                    //if (parametri.Emri.ToLower().Equals("filternumerdokumenti"))
                                    //{
                                    //    if (idRaporti == 256)
                                    //        return combo.Text;
                                    //}
                                    if (kodKontrolli.ToLower().StartsWith("cmbvep"))
                                    {
                                        if (kodKontrolli.Contains("1") && (parametri.Emri.ToLower().Equals("filtergrupimpkf") || parametri.Emri.ToLower().Equals("filtergrupimdkf") || parametri.Emri.ToLower().Equals("filtergrupimtkf")))
                                        {

                                            veprimiPara1 = combo.Value.ToString();
                                            continue;
                                        }
                                        if (kodKontrolli.Contains("1") && !kodKontrolli.Equals("cmbVeprimi1GrupPKF") && !kodKontrolli.Equals("cmbVeprimi1GrupDKF") && !kodKontrolli.Equals("cmbVeprimi1GrupTKF"))
                                        {

                                            veprimiPara1 = combo.Value.ToString();
                                            continue;
                                        }
                                        if (kodKontrolli.Contains("2") && !kodKontrolli.Equals("cmbVeprimi2GrupPKF") && !kodKontrolli.Equals("cmbVeprimi2GrupDKF") && !kodKontrolli.Equals("cmbVeprimi2GrupTKF"))
                                        {

                                            veprimiPara2 = combo.Value.ToString();
                                            continue;
                                        }

                                        if (kodKontrolli.Contains("2") && (parametri.Emri.ToLower().Equals("filtergrupimpkf") || parametri.Emri.ToLower().Equals("filtergrupimdkf") || parametri.Emri.ToLower().Equals("filtergrupimtkf")))
                                        {

                                            veprimiPara2 = combo.Value.ToString();
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        if (kodKontrolli.Contains("1"))
                                        {
                                            if (kodKontrolli.ToLower().Contains("faturuar") || kodKontrolli.ToLower().Contains("prodhuar") || kodKontrolli.ToLower().Contains("perfunduar"))
                                            {
                                                vlere1 = combo.Text.ToString();
                                            }
                                            else
                                                vlere1 = combo.Value.ToString();
                                            continue;
                                        }
                                        if (kodKontrolli.Contains("2"))
                                        {
                                            if (kodKontrolli.ToLower().Contains("faturuar") || kodKontrolli.ToLower().Contains("prodhuar") || kodKontrolli.ToLower().Contains("perfunduar"))
                                            {
                                                vlere2 = combo.Text.ToString();
                                            }
                                            else
                                                vlere2 = combo.Value.ToString();
                                            continue;
                                        }
                                    }
                                    if (kodKontrolli == "cmbLikuiduar")
                                    {
                                        if (combo.Value.ToString() == "0")
                                            vlera = "=0 ";
                                        else if (combo.Value.ToString() == "1")
                                            vlera = "<>0 ";
                                        else vlera = "is not null ";
                                        return vlera;

                                    }
                                    if (kodKontrolli == "cmbStatusi")
                                    {
                                        if (combo.Value.ToString() == "2")
                                            vlera = ">=0 ";
                                        else if (combo.Value.ToString() == "0")
                                            vlera = ">0 ";
                                        else vlera = "=0 ";
                                        return vlera;

                                    }
                                    if (parametri.Emri.Equals("filterGrupKFP2"))
                                    {
                                        vlera = ((ASPxComboBox)navBarFiltrat.Groups[g].FindControl("cmbLidhesaGrupimPKF")).Text;
                                        if (vlera == "ose") vlera = "or";
                                        else if (vlera == "dhe") vlera = "and";
                                        return vlera;

                                    }
                                    if (parametri.Emri.Equals("filterGrupKFD2"))
                                    {
                                        vlera = ((ASPxComboBox)navBarFiltrat.Groups[g].FindControl("cmbLidhesaGrupimDKF")).Text;
                                        if (vlera == "ose") vlera = "or";
                                        else if (vlera == "dhe") vlera = "and";
                                        return vlera;

                                    }
                                    if (parametri.Emri.Equals("filterGrupKFT2"))
                                    {
                                        vlera = ((ASPxComboBox)navBarFiltrat.Groups[g].FindControl("cmbLidhesaGrupimTKF")).Text;
                                        if (vlera == "ose") vlera = "or";
                                        else if (vlera == "dhe") vlera = "and";
                                        return vlera;

                                    }

                                    switch (kodKontrolli)
                                    {
                                        case "cmbLlojiArtBurim":
                                            switch (idGjuha)
                                            {
                                                case 0:
                                                    if (combo.Text.ToString() == rm.GetString("cmbboxItemFilterAvancTeGjithe", ci))
                                                        vlera = String.Format("{0}  {1}", kolonaFilter, "is not null ");
                                                    else
                                                        if (combo.Text.ToString() == "Artikulli")
                                                        vlera = String.Format("{0} = '{1}'", kolonaFilter, "1");
                                                    else
                                                        vlera = String.Format("{0} = '{1}'", kolonaFilter, "2");
                                                    return vlera + " AND ";
                                                case 1:
                                                    if (combo.Text.ToString() == "All")
                                                        vlera = String.Format("{0}  {1}", kolonaFilter, "is not null ");
                                                    else
                                                        if (combo.Text.ToString() == "Item")
                                                        vlera = String.Format("{0} = '{1}'", kolonaFilter, "1");
                                                    else
                                                        vlera = String.Format("{0} = '{1}'", kolonaFilter, "2");
                                                    return vlera + " AND ";
                                                default:
                                                    throw new DbCore.MyException("Unknown Lang");
                                            }
                                        case "cmbCikli":
                                            if (combo.Value.ToString() == "0")
                                                vlera = "0";
                                            else if (combo.Value.ToString() == "1")
                                                vlera = "1";
                                            else vlera = combo.Value.ToString();
                                            return vlera;
                                        case "cmbNjesia":
                                            vlera = combo.Value.ToString();
                                            return vlera;
                                        case "cmbStandarti":
                                            if (idRaporti == 216 || idRaporti == 244)
                                                vlera = combo.Text;
                                            else
                                                vlera = String.Format("{0} = {1}", kolonaFilter, combo.Value) + " AND ";
                                            return vlera;
                                        case "cmbStatusMagazine":
                                            vlera = String.Format("{0} = '{1}'", kolonaFilter, combo.Value.ToString());
                                            return vlera + " AND ";
                                        case "cmbGjendjeArt":
                                            vlera = combo.Value.ToString();
                                            return vlera;
                                        case "cmbShfaqArt":
                                            vlera = combo.Value.ToString();
                                            return vlera;
                                        case "cmbLlojPorosie":
                                            vlera = combo.Value.ToString();
                                            return vlera;
                                        case "cmbArtCmim":
                                            vlera = combo.Value.ToString();
                                            return vlera;
                                        case "cmbLlojDetajim":
                                            vlera = combo.Value.ToString();
                                            return vlera;
                                        case "cmbMeDetajim":
                                            vlera = combo.Value.ToString();
                                            return vlera;
                                        case "cmbDetajim":
                                            vlera = combo.Value.ToString();
                                            return vlera;
                                        case "cmbVeprimePeriudhe":
                                            vlera = combo.Value.ToString();
                                            return vlera;
                                        case "cmbGjendjeDetyrime":
                                            vlera = combo.Value.ToString();
                                            return vlera;
                                        case "cmbStatusCRM":
                                            vlera = combo.Value.ToString();
                                            return vlera;
                                        case "cmbStatusHR":
                                            vlera = combo.Value.ToString();
                                            return vlera;
                                        //case "cmbProdhuar":
                                        //    vlera = combo.Value.ToString();
                                        //    return vlera;
                                        //case "cmbFaturuar":
                                        //    vlera = combo.Value.ToString();
                                        //    return vlera;
                                        case "cmbEmertimLlog":
                                            vlera = combo.Value.ToString();
                                            return vlera;
                                        case "cmbGjendja":
                                            vlera = combo.Value.ToString();
                                            return vlera;
                                        case "cmbAktiv":
                                            vlera = combo.Value.ToString();
                                            return vlera;
                                        case "cmbStatusAfatDok":
                                            vlera = combo.Value.ToString();
                                            return vlera;
                                        case "cmbStatusKonvertimiDyte":
                                            vlera = combo.Value.ToString();
                                            return vlera;


                                        case "cmbLlojArtikulli":
                                            if (combo.Value.ToString() == rm.GetString("cmbboxItemFilterAvancTeGjithe", ci))
                                                vlera = String.Format("{0}  {1}", kolonaFilter, "is not null ");
                                            else
                                                if (combo.Value.ToString() == rm.GetString("cmbboxItemFilterAvancAfatgjt", ci))
                                                vlera = String.Format("{0} = '{1}'", kolonaFilter, "true");
                                            else
                                                vlera = String.Format("{0} = '{1}'", kolonaFilter, "false");
                                            return vlera + " AND ";

                                        case "cmbLlojSubjekti":
                                            vlera = combo.Value.ToString();
                                            return vlera;
                                        case "cmbPaguar":
                                            vlera = combo.Value.ToString();
                                            return vlera;
                                        case "cmbViti":
                                            vlera = combo.Text;
                                            return vlera;
                                        //case "cmbLlojVeprimi1":
                                        //case "cmbLlojVeprimi2":
                                        //    vlera = combo.Value.ToString();
                                        //    return vlera;
                                        default:
                                            vlera = String.Format("{0} = {1}", kolonaFilter, combo.Value);
                                            if (kodKontrolli.Contains("1"))
                                            {
                                                vlere1 = combo.Value.ToString();
                                                vlera = "";
                                                continue;
                                            }
                                            if (kodKontrolli.Contains("2"))
                                            {
                                                vlere2 = combo.Value.ToString();
                                                vlera = "";
                                                continue;
                                            }
                                            continue;
                                    }

                                #endregion

                                #region ASPxDateEdit
                                case "ASPxDateEdit":
                                    ASPxDateEdit dateEdit = (ASPxDateEdit)uiKontroll;
                                    if (dateEdit.Text.Trim() == "")
                                        continue;
                                    if (kodKontrolli == "dteDtMaturimi")
                                    {
                                        vlera = dateEdit.Text.Trim();
                                        continue;
                                    }
                                    continue;
                                #endregion

                                #region ASPxButtonEdit
                                case "ASPxButtonEdit":
                                    ASPxButtonEdit buttonEdit = (ASPxButtonEdit)uiKontroll;

                                    if (kodKontrolli.Equals("btnGlobalBand") || kodKontrolli.Equals("btnLocalBand"))
                                    {
                                        return buttonEdit.Text;
                                    }

                                    if (kodKontrolli.Equals("btnQenderKosto2") || kodKontrolli.Equals("btnQenderKosto1"))
                                    {
                                        return buttonEdit.Text;
                                    }

                                    if (kodKontrolli.Equals("btnJobTitle"))
                                    {
                                        return buttonEdit.Text.Split(';')[0];
                                    }
                                    if (buttonEdit.Text.Trim() == "")
                                    {
                                        if ((kodKontrolli == "btneGrupPKF1") || (kodKontrolli == "btneGrupDKF1") || (kodKontrolli == "btneGrupTKF1"))
                                            return vlera = "";

                                        else
                                        if (kodKontrolli.Equals("txtBtnNrPersonal1"))
                                            vlere1 = ktheNrPersonalPunonjesLoguar(DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                                        else
                                            continue;
                                    }
                                    if (kodKontrolli.Equals("degeAdminButtonEdit1"))
                                    {
                                        if (idRaporti == 256)
                                            return buttonEdit.Text;
                                    }
                                    if (kodKontrolli.Equals("pikeShFButtonEdit1"))
                                    {
                                        if (idRaporti == 256)
                                            return buttonEdit.Text;
                                    }
                                    if (kodKontrolli.Equals("btneKodKF1"))
                                    {
                                        if (idRaporti == 256)
                                            return buttonEdit.Text;
                                    }
                                    string parametriEmri = parametri.Emri.ToLower();
                                    if (kodKontrolli.Contains("1") && (parametriEmri.Equals("filtergrupimpkf") || parametriEmri.Equals("filtergrupimdkf") || parametriEmri.Equals("filtergrupimtkf") || parametriEmri.Equals("filterDepartamentiPunonjesit") || parametriEmri.Equals("filterNenDepartamentiPunonjesit") || parametriEmri.Equals("filterKodDep") || parametriEmri.Equals("filterKodNenDep")))
                                    {
                                        vlere1 = buttonEdit.Value.ToString();
                                        continue;
                                    }
                                    if (kodKontrolli.Contains("2") && !kodKontrolli.Equals("btneGrupPKF2") && !kodKontrolli.Equals("btneGrupDKF2") && !kodKontrolli.Equals("btneGrupTKF2"))
                                    {
                                        vlere2 = buttonEdit.Value.ToString();
                                        continue;
                                    }
                                    if (kodKontrolli.Contains("2") && (parametriEmri.Equals("filtergrupimpkf") || parametriEmri.Equals("filtergrupimdkf") || parametriEmri.Equals("filtergrupimtkf") || parametriEmri.Equals("filterDepartamentiPunonjesit") || parametriEmri.Equals("filterNenDepartamentiPunonjesit") || parametriEmri.Equals("filterKodDep") || parametriEmri.Equals("filterKodNenDep")))
                                    {
                                        vlere2 = buttonEdit.Value.ToString();
                                        continue;
                                    }

                                    if (parametri.Emri.Equals("filterGrupKFP1"))
                                    {

                                        return vlera = ((ASPxButtonEdit)navBarFiltrat.Groups[g].FindControl("btneGrupPKF1")).Text;
                                    }

                                    if (parametri.Emri.Equals("filterGrupKFP3"))
                                    {
                                        return vlera = ((ASPxButtonEdit)navBarFiltrat.Groups[g].FindControl("btneGrupPKF2")).Text;
                                    }
                                    if (parametri.Emri.Equals("filterGrupKFD1"))
                                    {

                                        return vlera = ((ASPxButtonEdit)navBarFiltrat.Groups[g].FindControl("btneGrupDKF1")).Text;
                                    }

                                    if (parametri.Emri.Equals("filterGrupKFD3"))
                                    {
                                        return vlera = ((ASPxButtonEdit)navBarFiltrat.Groups[g].FindControl("btneGrupDKF2")).Text;
                                    }
                                    if (parametri.Emri.Equals("filterGrupKFT1"))
                                    {

                                        return vlera = ((ASPxButtonEdit)navBarFiltrat.Groups[g].FindControl("btneGrupTKF1")).Text;
                                    }

                                    if (parametri.Emri.Equals("filterGrupKFT3"))
                                    {
                                        return vlera = ((ASPxButtonEdit)navBarFiltrat.Groups[g].FindControl("btneGrupTKF2")).Text;
                                    }

                                    if (kodKontrolli == "txtBtnMagazina" || kodKontrolli == "txtBtnKartela" || kodKontrolli == "txtBtnLlojDok" || kodKontrolli == "btnePerdorues" || kodKontrolli == "txtBtnKrijuesi" || kodKontrolli == "btnAgjentShitje" || kodKontrolli == "btnNivelCmimi" || kodKontrolli == "btneAuto" || kodKontrolli == "btneKompania" || kodKontrolli == "btnSeriali" || kodKontrolli == "btnBurimi" || kodKontrolli == "btnAktiviteti" || kodKontrolli == "")
                                    {
                                        if (idRaporti == 257)
                                            return vlera = buttonEdit.Text;
                                        vlera = buttonEdit.Value.ToString();

                                        if (idRaporti != 178 && idRaporti != 179 && idRaporti != 180)
                                            vlera = String.Format("{0} = ('{1}') ", kolonaFilter, vlera);
                                        continue;
                                    }
                                    if (kodKontrolli.Contains("1") && !kodKontrolli.Equals("btneGrupPKF1") && !kodKontrolli.Equals("btneGrupDKF1") && !kodKontrolli.Equals("btneGrupTKF1"))
                                    {
                                        if (kodKontrolli.Equals("txtBtnNrPersonal1"))
                                            vlere1 = ktheNrPersonalPunonjesLoguar(DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                                        else
                                            vlere1 = buttonEdit.Value.ToString();
                                        continue;
                                    }
                                    continue;
                                #endregion

                                #region ASPxCheckBox
                                case "ASPxCheckBox":
                                    ASPxCheckBox check = (ASPxCheckBox)uiKontroll;
                                    if (kodKontrolli == "cbMbylljeViti")
                                    {
                                        if (check.Checked)
                                            vlera = "1";
                                        else
                                            vlera = "0";
                                        return vlera;
                                    }
                                    if (kodKontrolli == "cbShfaqLlogP")
                                    {
                                        if (check.Checked)
                                            vlera = rm.GetString("cmbFilterPo", ci);
                                        else
                                            vlera = rm.GetString("cmbFilterJo", ci);
                                        return vlera;
                                    }
                                    if (kodKontrolli.ToString() == "cbMonedheKF" || kodKontrolli.ToString() == "checkAfishoPagen")
                                    {
                                        if (check.Checked)
                                            vlera = rm.GetString("cmbFilterPo", ci);
                                        else
                                            vlera = rm.GetString("cmbFilterJo", ci);
                                        return vlera;

                                    }
                                    if (kodKontrolli == "cbShfaqArtGjendjeZero")
                                    {
                                        if (check.Checked)
                                            vlera = rm.GetString("cmbFilterPo", ci);
                                        else
                                            vlera = rm.GetString("cmbFilterJo", ci);
                                        return vlera;
                                    }
                                    if (kodKontrolli == "cbDraft")
                                    {
                                        if (check.Checked)
                                            vlera = rm.GetString("cmbFilterPo", ci);
                                        else
                                            vlera = rm.GetString("cmbFilterJo", ci);
                                        return vlera;
                                    }
                                    break;
                                #endregion

                                #region ASPxRadioButtonList
                                case "ASPxRadioButtonList":
                                    ASPxRadioButtonList radioKontrolli = ((ASPxRadioButtonList)uiKontroll);
                                    if (radioKontrolli.SelectedItem.Value.ToString() == "")
                                        continue;

                                    if (radioKontrolli.ID.StartsWith("radDtDok") || radioKontrolli.ID.StartsWith("radDtRegj") || radioKontrolli.ID.StartsWith("radDtDokAfatKohor") || radioKontrolli.ID.StartsWith("radPeriudheMaturimi") || radioKontrolli.ID.StartsWith("radPeriudheFillimi") || radioKontrolli.ID.StartsWith("radPeriudheMbarimi") || radioKontrolli.ID.StartsWith("radDtPlanifikimi") || radioKontrolli.ID.StartsWith("radDtProdhimi") || radioKontrolli.ID.StartsWith("radDtKrijimiAqtSerial") || radioKontrolli.ID.StartsWith("radDtPorosie") || radioKontrolli.ID.StartsWith("radDtDtFillimi") || radioKontrolli.ID.StartsWith("radDtMbarimi"))
                                    {
                                        if (radioKontrolli.SelectedItem.Value.ToString() == "Aktuale")
                                        {
                                            //if (CacheLayer.GlobalCacheManager.MySessionCache["oPeriudhaAktuale"] != null)
                                            if (periudhaKontabel != null)
                                            {
                                                //clsPeriudhaKontabel periudhaKontabel = ((clsPeriudhaKontabel)CacheLayer.GlobalCacheManager.MySessionCache["oPeriudhaAktuale"]);
                                                //clsPeriudhaKontabel periudhaKontabel = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = periudhaKontabel.FillimiPeriudha.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = periudhaKontabel.MbarimiPeriudha.ToShortDateString();
                                                }


                                                if (parametri.Emri.ToLower().Equals("filterdtkrijimiseriali"))
                                                {
                                                    vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, periudhaKontabel.FillimiPeriudha.ToShortDateString(), periudhaKontabel.MbarimiPeriudha.ToShortDateString());
                                                    continue;
                                                }

                                                if (parametri.Emri.ToLower().Equals("filterdtdokkrahasues"))
                                                {
                                                    vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, periudhaKontabel.FillimiPeriudha.AddYears(-1).ToShortDateString(), periudhaKontabel.MbarimiPeriudha.AddYears(-1).ToShortDateString());
                                                    continue;
                                                }
                                                if (parametri.Emri.ToLower().Equals("filterdtdokplanifikimi") || parametri.Emri.ToLower().Equals("filterdtdokprodhimi"))
                                                {
                                                    vlera = String.Format("( ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) ) or {0} IS NULL )", kolonaFilter, periudhaKontabel.FillimiPeriudha.ToShortDateString(), periudhaKontabel.MbarimiPeriudha.ToShortDateString());
                                                    continue;
                                                }
                                                if (parametri.Emri.ToLower().StartsWith("filterdt"))
                                                {
                                                    vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, periudhaKontabel.FillimiPeriudha.ToShortDateString(), periudhaKontabel.MbarimiPeriudha.ToShortDateString());
                                                    continue;
                                                }
                                            }
                                            continue;
                                        }
                                        if (radioKontrolli.SelectedItem.Value.ToString() == "VitiUshtrimor")
                                        {
                                            //if (DbCore.mySessionObjects.ktheNdermarrjeVit(Session) != null)
                                            if (idNderviti != -1)
                                            {
                                                clsNdermarrjeViti ndermviti = new clsNdermarrjeViti(idNderviti);
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ndermviti.NdermarrjeVitiFillim.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ndermviti.NdermarrjeVitiFund.ToShortDateString();
                                                }

                                                if (parametri.Emri.ToLower().Equals("filterdtkrijimiseriali"))
                                                {
                                                    vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ndermviti.NdermarrjeVitiFillim.ToShortDateString(), ndermviti.NdermarrjeVitiFund.ToShortDateString());
                                                    continue;
                                                }
                                                if (parametri.Emri.ToLower().Equals("filterdtdokkrahasues"))
                                                {
                                                    vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) ) ", kolonaFilter, ndermviti.NdermarrjeVitiFillim.AddYears(-1).ToShortDateString(), ndermviti.NdermarrjeVitiFund.AddYears(-1).ToShortDateString());
                                                    continue;
                                                }
                                                if (parametri.Emri.ToLower().Equals("filterdtdokplanifikimi") || parametri.Emri.ToLower().Equals("filterdtdokprodhimi"))
                                                {
                                                    vlera = String.Format(" ( ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) ) or {0} IS NULL ) ", kolonaFilter, ndermviti.NdermarrjeVitiFillim.ToShortDateString(), ndermviti.NdermarrjeVitiFund.ToShortDateString());
                                                    continue;
                                                }
                                                if (parametri.Emri.ToLower().StartsWith("filterdt"))
                                                {
                                                    vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ndermviti.NdermarrjeVitiFillim.ToShortDateString(), ndermviti.NdermarrjeVitiFund.ToShortDateString());
                                                    continue;
                                                }
                                            }
                                            continue;
                                        }
                                        if (radioKontrolli.SelectedItem.Value.ToString() == "GjitheVitet")
                                        {
                                            if (parametri.Emri.Contains("1"))
                                            {
                                                return vlera = Convert.ToDateTime("1900-01-01").ToShortDateString();
                                            }
                                            if (parametri.Emri.Contains("2"))
                                            {
                                                return vlera = DateTime.MaxValue.ToShortDateString();
                                            }
                                            if (parametri.Emri.ToLower().Equals("filterdtdokkrahasues"))
                                            {
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) ) ", kolonaFilter, Convert.ToDateTime("1900-01-01").ToShortDateString(), DateTime.MaxValue.ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().Equals("filterdtdokplanifikimi") || parametri.Emri.ToLower().Equals("filterdtdokprodhimi"))
                                            {
                                                vlera = String.Format(" ( ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) ) or {0} IS NULL ) ", kolonaFilter, Convert.ToDateTime("1900-01-01").ToShortDateString(), DateTime.MaxValue.ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().StartsWith("filterdt"))
                                            {
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, Convert.ToDateTime("1900-01-01").ToShortDateString(), DateTime.MaxValue.ToShortDateString());
                                                continue;
                                            }
                                        }
                                        if (radioKontrolli.SelectedItem.Value.ToString() == "Periudha")
                                        {
                                            if (parametri.Emri.ToLower().Equals("filterdtkrijimiseriali"))
                                            {
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDtNgaKrijimiAqtSerial")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDtDeriKrijimiAqtSerial")).Date.ToShortDateString());
                                                continue;
                                            }

                                            if (parametri.Emri.ToLower().Equals("filterdtdokkryesor"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokKryesor")).Date.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokKryesor")).Date.ToShortDateString();
                                                }
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokKryesor")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokKryesor")).Date.ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().Equals("filterdtamortizimi"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokAmortizim")).Date.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokAmortizim")).Date.ToShortDateString();
                                                }
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokAmortizim")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokAmortizim")).Date.ToShortDateString());
                                                continue;
                                            }

                                            if (parametri.Emri.ToLower().Equals("filterdtreg"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokReg")).Date.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokReg")).Date.ToShortDateString();
                                                }
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokReg")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokReg")).Date.ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().Equals("filterdtporosie"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDtPorosie")).Date.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDtPorosie")).Date.ToShortDateString();
                                                }
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDtPorosie")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDtPorosie")).Date.ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().Equals("filterdtdoklidhes"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokLidhes")).Date.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokLidhes")).Date.ToShortDateString();
                                                }
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokLidhes")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokLidhes")).Date.ToShortDateString());
                                                continue;
                                            }

                                            if (parametri.Emri.ToLower().Equals("filterdtdokkrahasues"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDtKrahasues")).Date.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDtKrahasues")).Date.ToShortDateString();
                                                }
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDtKrahasues")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDtKrahasues")).Date.ToShortDateString());
                                                continue;
                                            }

                                            if (parametri.Emri.ToLower().Equals("filterdturdherpagese"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokUrdherPagese")).Date.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokUrdherPagese")).Date.ToShortDateString();
                                                }
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokUrdherPagese")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokUrdherPagese")).Date.ToShortDateString());
                                                continue;
                                            }

                                            if (parametri.Emri.ToLower().Equals("filterdtaprovimit"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokAprovimit")).Date.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokAprovimit")).Date.ToShortDateString();
                                                }
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokAprovimit")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokAprovimit")).Date.ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().StartsWith("filterdtdokkonvertuar"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokKonvertuar")).Date.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokKonvertuar")).Date.ToShortDateString();
                                                }
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokKonvertuar")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokKonvertuar")).Date.ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().StartsWith("filterdtdokplanifikimi"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDtPlanifikimi")).Date.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDtPlanifikimi")).Date.ToShortDateString();
                                                }
                                                vlera = String.Format(" ( ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) ) OR {0} IS NULL ) ", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDtPlanifikimi")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDtPlanifikimi")).Date.ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().StartsWith("filterdtdokprodhimi"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDtProdhimi")).Date.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDtProdhimi")).Date.ToShortDateString();
                                                }
                                                vlera = String.Format(" ( ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) ) OR {0} IS NULL ) ", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDtProdhimi")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDtProdhimi")).Date.ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().StartsWith("filterdtdokqenderkosto"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokQenderKosto")).Date.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokQenderKosto")).Date.ToShortDateString();
                                                }
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokQenderKosto")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokQenderKosto")).Date.ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().StartsWith("filterdtdokfshirje"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokFshirje")).Date.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokFshirje")).Date.ToShortDateString();
                                                }
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=DATEADD(SS, -1, (DATEADD(DAY, 1, convert(datetime,'{2}',103)))))", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokFshirje")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokFshirje")).Date.ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().StartsWith("filterdtdokafatkohor"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokAfatKohor")).Date.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokAfatKohor")).Date.ToShortDateString();
                                                }
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokAfatKohor")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokAfatKohor")).Date.ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().StartsWith("filterdtdok"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    if (idRaporti == 98)
                                                        return vlera = Convert.ToDateTime("1900-01-01").ToShortDateString();
                                                    else
                                                        return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDok")).Date.ToShortDateString();//rezi
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDok")).Date.ToShortDateString();
                                                }
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDok")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDok")).Date.ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().StartsWith("filterdtfillimi"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDtFillimi")).Date.ToShortDateString();//rezi
                                                if (parametri.Emri.Contains("2"))
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDtFillimi")).Date.ToShortDateString();
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDtFillimi")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDtFillimi")).Date.ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().StartsWith("filterdtregj"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaRegj")).Date.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriRegj")).Date.ToShortDateString();
                                                }
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaRegj")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriRegj")).Date.ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().StartsWith("filterdtfillimi"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDtFillimi")).Date.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDtFillimi")).Date.ToShortDateString();
                                                }
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDtFillimi")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDtFillimi")).Date.ToShortDateString());
                                                continue;
                                            }

                                            if (parametri.Emri.ToLower().StartsWith("filterdtmbarimi"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDtMbarimi")).Date.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDtMbarimi")).Date.ToShortDateString();
                                                }
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter,
                                                    ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDtMbarimi")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDtMbarimi")).Date.ToShortDateString());
                                                continue;
                                            }

                                            if (parametri.Emri.ToLower().StartsWith("filterdtperiudhematurimi"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaPeriudheMaturimi")).Date.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriPeriudheMaturimi")).Date.ToShortDateString();
                                                }
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaPeriudheMaturimi")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriPeriudheMaturimi")).Date.ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().StartsWith("filterdtperiudhefillimi"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaPeriudheFillimi")).Date.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriPeriudheFillimi")).Date.ToShortDateString();
                                                }
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaPeriudheFillimi")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriPeriudheFillimi")).Date.ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().StartsWith("filterdtperiudhembarimi"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaPeriudheMbarimi")).Date.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriPeriudheMbarimi")).Date.ToShortDateString();
                                                }
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaPeriudheMbarimi")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriPeriudheMbarimi")).Date.ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().StartsWith("filterdtstatusmagazine"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDtStatus")).Date.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDtStatus")).Date.ToShortDateString();
                                                }
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDtStatus")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDtStatus")).Date.ToShortDateString());
                                                continue;
                                            }
                                            //if (parametri.Emri.Equals("filterdtdok")&& idRaporti == 98)
                                            //{  }
                                        }
                                        continue;
                                    }
                                    continue;
                                #endregion
                                default:
                                    continue;
                            }
                        }
                    }
                }
                if ((idRaporti == 232 || idRaporti == 247) && parametri.Emri == "filterKategoriShpenzimi")
                    return vlere1;
                if (vlere1 != "")
                    if (parametri.Emri.ToLower().Contains("1"))
                        vlera = vlere1;
                    else
                        switch (veprimiPara1)
                        {
                            case "0":
                                vlera += String.Format("{0} = ('{1}') ", kolonaFilter, vlere1);
                                break;
                            case "1":
                                vlera += String.Format("{0} < ('{1}') ", kolonaFilter, vlere1);
                                break;
                            case "2":
                                vlera += String.Format("{0} > ('{1}') ", kolonaFilter, vlere1);
                                break;
                            case "3":
                                vlera += String.Format("{0} <> ('{1}') ", kolonaFilter, vlere1);
                                break;
                            case "4":
                                vlera += String.Format("{0} like '{1}%' ", kolonaFilter, vlere1);
                                break;
                            case "5":
                                vlera += String.Format("{0} like '%{1}' ", kolonaFilter, vlere1);
                                break;
                            case "6":
                                vlera += String.Format("{0} like '%{1}%' ", kolonaFilter, vlere1);
                                break;
                            case "7":
                                vlere1 = String.Format("'{0}'", vlere1.Replace(",", "','"));
                                vlera += String.Format("{0} in ({1}) ", kolonaFilter, vlere1);
                                break;
                            default:
                                break;
                        }
                if (vlere2 != "" && lidhes != "")
                {
                    if (vlere1 == "")
                        throw new DbCore.MyException("Vlera e pare e parametrit nuk duhet te jete bosh!");
                    vlera += " " + lidhes + " ";
                    switch (veprimiPara2)
                    {
                        case "0":
                            vlera += String.Format("{0} = ('{1}') ", kolonaFilter, vlere2);
                            break;
                        case "1":
                            vlera += String.Format("{0} < ('{1}') ", kolonaFilter, vlere2);
                            break;
                        case "2":
                            vlera += String.Format("{0} > ('{1}') ", kolonaFilter, vlere2);
                            break;
                        case "3":
                            vlera += String.Format("{0} <> ('{1}') ", kolonaFilter, vlere2);
                            break;
                        case "4":
                            vlera += String.Format("{0} like '{1}%' ", kolonaFilter, vlere2);
                            break;
                        case "5":
                            vlera += String.Format("{0} like '%{1}' ", kolonaFilter, vlere2);
                            break;
                        case "6":
                            vlera += String.Format("{0} like '%{1}%' ", kolonaFilter, vlere2);
                            break;
                        case "7":
                            vlere2 = String.Format("'{0}'", vlere2.Replace(",", "','"));
                            vlera += String.Format("{0} in ({1}) ", kolonaFilter, vlere2);
                            break;
                        default:
                            break;
                    }
                }
                if (vlera.Trim() != "")
                {
                    if ((!vlera.StartsWith("KODIAGJENTSHITJE")) && (!vlera.StartsWith("NRLLOGARI") && (!parametri.Emri.ToLower().StartsWith("filterqk")) && (idRaporti != 178 && idRaporti != 179 && idRaporti != 180)))
                        vlera = String.Format("( {0} ) AND ", vlera);


                }
                else
                    vlera = vlera.Trim();
                return vlera.Replace("+@salt+", System.Web.Configuration.WebConfigurationManager.AppSettings["salt"]);

            }
            catch (Exception)
            {
                throw new DbCore.MyException("Ndodhi nje Gabim gjate marrjes se vleres se parametrit: " + parametri.Emri);
            }
        }


        private string ktheNrPersonalPunonjesLoguar(int idpunonjes)
        {
            DbCore.DbListPagesat.clsPunonjes punonjes = new DbCore.DbListPagesat.clsPunonjes(idpunonjes);
            return punonjes.NrPersonal;
        }

        /// <summary>
        /// nderton parametrin e sql-se dhe e kontrollon per vlera te pa lejuara
        /// </summary>
        /// <param name="parametri"></param>
        /// <returns></returns>
        private string ktheVlerenEParametrit(clsParameter parametri, int idNdermarrje, clsPeriudhaKontabel periudhaKontabel, int idNderviti, int idPerdorues, int idGjuha, colKontrolle oColKontrolle)
        {
            string vlera = ktheVlereParametri(parametri, idNdermarrje, periudhaKontabel, idNderviti, idPerdorues, idGjuha, oColKontrolle);
            // if (parametri.Emri=="filterNrPersonalPunonjesi")
            DbCore.clsMesazh result = eshteParameterILejuar(vlera);
            if (!result.Status)
                throw new DbCore.MyException(result.PershkrimMesazhi);
            return vlera;
        }
        /// <summary>
        /// kontrollon per vlera te pa lejuara vleren e parametrit te sqlse
        /// </summary>
        /// <param name="vlera"></param>
        /// <returns>true nese eshte ok, false nese ka vlera te pa lejuara</returns>
        private DbCore.clsMesazh eshteParameterILejuar(string vlera)
        {
            if (vlera.Contains(';'))
                return new DbCore.clsMesazh(false, "Filtrat kane vlera te pa lejuara");
            return new DbCore.clsMesazh(true, "Parametri eshte brenda rregullave");
        }
        /// <summary>
        /// kthen stylesheet file name
        /// </summary>
        /// <param name="styleName">emri i stylit</param>
        /// <returns>stylesheet file name</returns>
        public static string ktheStyleSheet(string styleName)
        {
            return RaportiEPaySlip.styleNamePrefix + styleName + RaportiEPaySlip.styleNamePostfix;
        }

        public static string ndertoPathStyleSheet(string path, string styleName)
        {
            return path + styleName;
        }

        public static bool makeStyleDefault(string pathStyle, string styleName)
        {
            try
            {
                XRControlStyleSheet styleSheet = new XRControlStyleSheet();
                styleSheet.FileName = pathStyle + RaportiEPaySlip.ktheStyleSheet(styleName);
                styleSheet.SaveToFile(pathStyle + RaportiEPaySlip.styleNamePrefix + RaportiEPaySlip.styleNameDefault + RaportiEPaySlip.styleNamePostfix);
                return true;
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Ruan stilin e raportit ne nivel perdoruesi.
        /// </summary>
        /// <param name="styleName"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static bool ruajStilRaporti(string styleName, System.Web.SessionState.HttpSessionState session, string zoomFactor, int exportFormat, int exportMode)
        {
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(session);
            // DbCore.DbAdmin.clsStilRaporti stili = new clsStilRaporti(perdoruesi.IdGjuha, styleName);
            //DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            //perdoruesi.IdStilRaporti = stili.IdStili;
            //perdoruesi.IdStatusDok = 1;
            float zoom = float.Parse(zoomFactor);
            if (zoom != 100f && zoom != 115f && zoom != 130f && zoom != 140f && zoom != 160f && zoom != 185f)
                zoom = 0;
            //perdoruesi.ZoomFactor = zoom;
            DbCore.clsMesazh mesazh = clsPerdorues.ruajStilRaporti(idPerdoruesi, styleName, zoom, exportFormat, exportMode);
            //mesazh = perdoruesi.modifiko();
            clsPerdorues perdoruesi = new clsPerdorues(idPerdoruesi);
            //perdoruesi = perdoruesi.merrUserNgaLogin()[0];
            //clsFunksione clsHash = new DbCore.clsFunksione();
            //CacheLayer.GlobalCacheManager.MySessionCache["oClsPerdoruesi"] = perdoruesi;
            DbCore.mySessionObjects.ruajPerdoruesNeSesion(session, perdoruesi);
            return mesazh.Status;
        }

        protected void ASPxCallbackPanel1_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
        {
            //AlphaWebReports.raporteUtil.HapRaportDetails(source, e, GetReport(hfState["guidString"].ToString()), ReportViewer2);
        }

        private XtraReport GetReport(String guidString)
        {
            //return CacheLayer.GlobalCacheManager.MySessionCache["MyReport"] as XtraReport;            
            return DbCore.mySessionObjects.merrMyReportNgaSessioni<XtraReport>(Session, guidString);
        }

        protected void afisho(int idRaporti, int idNdermarrje, int idViti, int idNderViti, clsPeriudhaKontabel periudhaKontabel, int idPerdorues, CultureInfo ci, bool kaSubRaport, String guidString, int idGjuha)
        {
            clsRaporti oRap = new clsRaporti(0, idRaporti);

            clsSP oSp = new clsSP(oRap.IdSp);
            SqlParameter[] sqlParam = krijoParametratSql(oSp, idNdermarrje, periudhaKontabel, idNderViti, idPerdorues, idGjuha);
            //String[,] sqlParamShfaqRaport = krijoParametratShfaqRaporti(oSp, idNdermarrje, periudhaKontabel, idNderViti, ci);
            colParameter sqlParamShfaqRaport = krijoParametratShfaqRaporti(oSp, idNdermarrje, periudhaKontabel, idNderViti, ci);
            if (kaSubRaport) //konfigurimeSubRaporti duhet thirrur ketu vetem nqs raporti i hapur ka subraport.
            {
              
                konfigurimeSubRaporti(idNdermarrje, periudhaKontabel, idNderViti, idPerdorues, ci, guidString, idGjuha);
            }

         
            clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasKod("NKM", idNdermarrje);
            DateTime dtmbarimi = DateTime.Today;
            bool azhornim = false;

            if (((ASPxCheckBox)navBarFiltrat.Groups[1].FindControl("cbAzhornim")).Checked && idRaporti != 173 && idRaporti != 174)
            {

                azhornim = true;
                switch (((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDok")).SelectedItem.Value.ToString())
                {
                    case "Aktuale":
                        if (periudhaKontabel != null)
                            dtmbarimi = periudhaKontabel.MbarimiPeriudha;
                        break;
                    case "VitiUshtrimor":
                        if (idNderViti != 0)
                        {


                            dtmbarimi = new clsNdermarrjeViti(idNderViti).NdermarrjeVitiFund;
                        }
                        break;
                    case "Periudha":
                        dtmbarimi = ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDok")).Date;
                        break;
                    case "GjitheVitet":
                        {
                            clsNdermarrjeViti ndermviti = new clsNdermarrjeViti();
                            ndermviti.mbushNdermVitFillimFundPerGjitheVitetSipasNdermarjes(idNdermarrje);
                            dtmbarimi = ndermviti.NdermarrjeVitiFund;
                        }
                        break;
                    default:
                        break;

                }

             
            }
           
            XtraReport report = ReportFunctions.krijoObjektRaporti("", 0, idPerdorues, idViti, idRaporti, idNdermarrje, sqlParamShfaqRaport, 0, ImbReportToolbar1.orientimi, guidString, Request.QueryString[CacheLayer.ScopeManager.ScopeIdKey]);
            int idPeriudha = clsPeriudhaKontabel.ktheIdPeriudheSipasDatesDheNdermarrjes(dtmbarimi, idNdermarrje);// new 
            report.StyleSheet.LoadFromFile(Raporti.ndertoPathStyleSheet(pathStyle.Value, ImbReportToolbar1.ReportStyle));
          
            afisho(idRaporti, report, oSp, sqlParam, idPerdorues, azhornim, idNdermarrje, idNderViti, dtmbarimi, konf.IdKonfigAmbjente, idPeriudha, rm, ci, guidString);
           
        }

     
        /// <summary>
        /// Ngarkon filtrin e personalizuar te zgjedhur te faqja e raporteve sipas modulit
        /// </summary>
        /// <param name="idFiltri">Id e filtrit te personalizuar</param>
        protected void NgarkoFiltraTePersonalizuar(int idFiltri)
        {
            String[][] result = new String[2][];
            result = DbCore.clsFunksione.ktheVleraFiltri(Convert.ToString(idFiltri));
            String[] filertrupikontrolle = result[0];
            String[] filtertrupivlera = result[1];
            //colTipeKontrolli tipet = new colTipeKontrolli();
            //tipet.mbushTipet();

            int filertrupikontrolleLength = filertrupikontrolle.Length;
            //if (filertrupikontrolleLength > 0)
            //    tipet.mbushTipet();
            for (int i = 0; i < filertrupikontrolleLength; i++)
            {
                string kodKontrolli = filertrupikontrolle[i];
                clsKontroll kontrolli = clsKontroll.merrKontrollSipasKoditKomponentes(kodKontrolli, 649);
                string tipiKontrollit = Enum.GetName(typeof(DbCore.DbShare.TipeKontrolli), kontrolli.IdTipiKontrollit); //tipet.merrTipin(kontrolli.IdTipiKontrollit).TipiKontrollit;
                for (int g = 0; g < navBarFiltrat.Groups.Count; g++) //per momentin nuk po e marr parasysh daten
                {
                    System.Web.UI.Control uiKontroll = navBarFiltrat.Groups[g].FindControl(kodKontrolli);
                    if (uiKontroll != null)
                    {
                        switch (tipiKontrollit)
                        {
                            #region ASPxLabel
                            case "ASPxLabel": //nuk behet gje me label-at
                                continue;
                            #endregion

                            #region ASPxTextBox
                            case "ASPxTextBox":
                                ASPxTextBox textBoxi = (ASPxTextBox)uiKontroll;
                                textBoxi.Text = filtertrupivlera[i];
                                continue;
                            #endregion

                            #region ASPxRadioButtonList
                            case "ASPxRadioButtonList":
                                ASPxRadioButtonList radioKontroll = (ASPxRadioButtonList)uiKontroll;
                                int n = radioKontroll.Items.Count;
                                for (int j = 0; j < n; j++)
                                {
                                    if (radioKontroll.Items[j].Value.ToString() == filtertrupivlera[i])
                                    {
                                        radioKontroll.SelectedIndex = j;
                                    }
                                }
                                continue;
                            #endregion

                            #region ASPxComboBox
                            case "ASPxComboBox":
                                ASPxComboBox combo = (ASPxComboBox)uiKontroll;
                                combo.Text = filtertrupivlera[i];
                                continue;
                            #endregion

                            #region ASPxButtonEdit
                            case "ASPxButtonEdit":
                                ASPxButtonEdit button = (ASPxButtonEdit)uiKontroll;
                                button.Text = filtertrupivlera[i];
                                continue;
                            #endregion

                            #region ASPxDateEdit
                            case "ASPxDateEdit":
                                ASPxDateEdit date = (ASPxDateEdit)uiKontroll;
                                date.Text = filtertrupivlera[i];
                                continue;
                            #endregion
                            default:
                                continue;
                        }
                    }
                }

            }

        }

        /// <summary>
        /// Kthen nje Dictionary qe perban collection-et e parametrave te perbashket te raportit me secilin nga subraportet e tij.
        /// </summary>
        /// <returns></returns>
        private Dictionary<int, colParameter> ktheParametratPerbashketSubRaportet()
        {
            var subRaportet = new colRaporti(0, idRaporti);
            Dictionary<int, colParameter> paramReturn = new Dictionary<int, colParameter>();
            if (subRaportet.Count != 0)
            {
                colParameter[] parametratPerbashket = new colParameter[subRaportet.Count];
                for (int i = 0; i < subRaportet.Count; i++)
                {
                    colParameter paramPerbashketR1R2 = colParameter.merrParametraPebashketRaportesh(idRaporti, subRaportet[i].IdRaporti);
                    parametratPerbashket[i] = paramPerbashketR1R2;
                    //Ruan ne dictionary id e subraportit dhe collection-in e parametrave te perbashket
                    paramReturn.Add(subRaportet[i].IdRaporti, parametratPerbashket[i]);
                }
            }
            return paramReturn;
        }

        private void konfigurimeSubRaporti(int idNdermarrje, clsPeriudhaKontabel periudhaKontabel, int idNderviti, int idPerdorues, CultureInfo ci, String guidString, int idGjuha)
        {
            //Marrim listen e parametrave te perbashket per secilin subraport
            Dictionary<int, colParameter> paramPerbashketSubRaport = ktheParametratPerbashketSubRaportet();
            if (paramPerbashketSubRaport.Count != 0) //nese ka subraporte
            {
                foreach (KeyValuePair<int, colParameter> kv in paramPerbashketSubRaport)
                {
                    Dictionary<int, string> vleraParamSubRaport = new Dictionary<int, string>();
                    clsRaporti subRaport = new clsRaporti(0, kv.Key);

                    //pjesa 1
                    colParameter paramSubRaport = new colParameter(subRaport.IdSp);
                    colKontrolle oColKontrolle = new colKontrolle();
                    oColKontrolle.merrKontrolletRaportiSipasIdSp(subRaport.IdSp);
                    foreach (clsParameter param in paramSubRaport)
                    {
                        if (kv.Value.Any(p => p.IdParametri == param.IdParametri))
                        {
                            //Si id raporti do ruhet id e subraportit
                            if (param.Emri.Equals("IdRaport"))
                                vleraParamSubRaport.Add(param.IdParametri, Convert.ToString(subRaport.IdRaporti));
                            else //Per te gjithe parametrat e tjere te perbashket, vendoset vlera e parameterit e njejte me ate te superaportit
                                vleraParamSubRaport.Add(param.IdParametri, ktheVlerenEParametrit(param, idNdermarrje, periudhaKontabel, idNderviti, idPerdorues, idGjuha, oColKontrolle));
                        }
                        else //Per parametrat e vecante te subraportit  vendoset nje string bosh
                            vleraParamSubRaport.Add(param.IdParametri, "");
                    }
                    //Vlerat e parametrave te secilit raport ruhen ne nje session(me id e subraporit) per tu aksesuar nga faqja RaportiShpejte.Aspx
                    DbCore.mySessionObjects.ruajParametratSubRaportitNeSesion(Session, vleraParamSubRaport, kv.Key, guidString);

                    //pjesa 2
                    colParameter paramSubRaport2 = new colParameter(subRaport.IdSp, idRaporti);
                    for (int i = 0, j = 0; i < paramSubRaport2.Count; i++, j++)
                    {
                        if (paramSubRaport2[i].Emri.ToLower() == "idraport" || paramSubRaport2[i].Emri.ToLower() == "idperdoruesi" || (paramSubRaport2[i].Emri.Contains("1") && !paramSubRaport2[i].Emri.ToString().Equals("txtInterval1")) || (paramSubRaport2[i].Emri.Contains("2") && !paramSubRaport2[i].Emri.ToString().Equals("txtInterval2")) || paramSubRaport2[i].Emri == "filterDtDokShitje" || paramSubRaport2[i].Emri == "filterDtRegjShitje" || paramSubRaport2[i].Emri == "filterDtMaturimi" || paramSubRaport2[i].Emri.ToString().Equals("intervalet"))
                        {
                            j--;
                            continue;
                        }
                        paramSubRaport2[i].Vlera = ktheVlerenEParametritShfaqRaport(paramSubRaport2[i], idNdermarrje, periudhaKontabel, idNderviti, ci, oColKontrolle);
                    }
                    DbCore.mySessionObjects.ruajParametratShfaqSubRaportitNeSesion(Session, paramSubRaport2, kv.Key, guidString);
                }
            }
        }


        private SqlParameter[] krijoParametratSql(clsSP oSp, int idNdermarrje, clsPeriudhaKontabel periudhaKontabel, int idNderviti, int idPerdorues, int idGjuha)
        {
            colParameter parametraSp = new colParameter(oSp.IdSp);
            colKontrolle oColKontrolle = new colKontrolle();
            oColKontrolle.merrKontrolletRaportiSipasIdSp(oSp.IdSp);
            //colSpTrupi oColSpTrupi = new colSpTrupi(oSp.IdSp);
            SqlParameter[] sqlparam;
            if (parametraSp.Count() > 0)
            {

                sqlparam = new SqlParameter[parametraSp.Count];
                for (int i = 0; i < parametraSp.Count; i++)
                {
                    //vlera do i jepet oSp.OColSpTrupi[i].Vlera qe mban vleren qe do marri parametri qe do i kalohet SP-se
                    string vlera = "";

                    //************************************************************************************//
                    //behet kontrolli nqs nuk eshte plotesuar filtri atehere do kalohet si bosh, pa vlere 

                    vlera = ktheVlerenEParametrit(parametraSp[i], idNdermarrje, periudhaKontabel, idNderviti, idPerdorues, idGjuha, oColKontrolle);
                    sqlparam[i] = new SqlParameter();
                    sqlparam[i].ParameterName = parametraSp[i].Emri;
                    sqlparam[i].Value = vlera;
                }
            }
            else
                sqlparam = new SqlParameter[0];
            return sqlparam;
        }

        private colParameter krijoParametratShfaqRaporti(clsSP oSp, int idNdermarrje, clsPeriudhaKontabel periudhaKontabel, int idNderviti, CultureInfo ci)
        {
            //int parametra = 0;
            colParameter parametraSp = new colParameter(oSp.IdSp, idRaporti);
            colKontrolle oColKontrolle = new colKontrolle();
            oColKontrolle.merrKontrolletRaporti(idRaporti);
            for (int i = 0, j = 0; i < parametraSp.Count; i++, j++)
            {
                //string vlera = "";

                string parametraSpEmri = parametraSp[i].Emri;
                if (parametraSpEmri.ToLower() == "idraport" || parametraSpEmri.ToLower() == "idperdoruesi" || (parametraSpEmri.Contains("1") && !parametraSpEmri.ToString().Equals("txtInterval1")) || (parametraSpEmri.Contains("2") && !parametraSpEmri.ToString().Equals("txtInterval2")) || parametraSpEmri == "filterDtDokShitje" || parametraSpEmri == "filterDtRegjShitje" || parametraSpEmri == "filterDtMaturimi" || parametraSpEmri.ToString().Equals("intervalet"))
                {
                    j--;
                    continue;
                }
                parametraSp[i].Vlera = ktheVlerenEParametritShfaqRaport(parametraSp[i], idNdermarrje, periudhaKontabel, idNderviti, ci, oColKontrolle);              
            }
           
            return parametraSp;
        }

        private string ktheVlerenEParametritShfaqRaport(clsParameter parametri, int idNdermarrje, clsPeriudhaKontabel periudhaKontabel, int idNderviti, CultureInfo ci, colKontrolle oColKontrolleRaporti)
        {
            string vlera = "";
            string emriParam = parametri.Emri.ToLower();
            //Llogariten vlerat e parametrave te pavarur nga filtrat e raportit.

            if (emriParam.Equals("idndermarje"))
            {
                vlera = clsNdermarrje.merrPershkrimNdermarrje(idNdermarrje);
                return vlera;
            }
            if (emriParam.Equals("idndermvit"))
            {
                vlera = new clsNdermarrje(idNdermarrje).NdermarrjeKodi;
                return vlera;
            }
            if (emriParam.Equals("windowwidth"))
            {
                vlera = Convert.ToString(1.14 * DbCore.clsFunksione.merrWindowWidthRequested(Request));
                return vlera;
            }
            if (emriParam.Equals("shikoGjitheDokumentat"))
            {
                vlera = hfTeDrejtaRaporti.Get("dGjitheDok").ToString();
                return vlera;
            }
            if (emriParam.Equals("filterkodifikimartp"))
            {
                object tmpValue = ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("GrupimPareButtonEdit1")).Value;
                return vlera = tmpValue == null ? "" : tmpValue.ToString();
            }

            if (emriParam.Equals("filterkodifikimartd"))
            {
                object tmpValue = ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("GrupimDyteButtonEdit1")).Value;
                return vlera = tmpValue == null ? "" : tmpValue.ToString();
            }
            if (emriParam.Equals("vitiaktual"))
            {
                //if (CacheLayer.GlobalCacheManager.MySessionCache["oPeriudhaAktuale"] != null)
                if (periudhaKontabel != null)
                {
                    //clsPeriudhaKontabel periudhaKontabel = ((clsPeriudhaKontabel)CacheLayer.GlobalCacheManager.MySessionCache["oPeriudhaAktuale"]);
                    //clsPeriudhaKontabel periudhaKontabel = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
                    vlera = periudhaKontabel.FillimiPeriudha.Year.ToString();
                }
                return vlera;
            }
            if (emriParam.Equals("vitiparaardhes"))
            {
                //if (CacheLayer.GlobalCacheManager.MySessionCache["oPeriudhaAktuale"] != null)
                if (periudhaKontabel != null)
                {
                    //clsPeriudhaKontabel periudhaKontabel = ((clsPeriudhaKontabel)CacheLayer.GlobalCacheManager.MySessionCache["oPeriudhaAktuale"]);
                    //clsPeriudhaKontabel periudhaKontabel = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
                    vlera = periudhaKontabel.FillimiPeriudha.AddYears(-1).Year.ToString();
                }
                return vlera;
            }
            if (emriParam.Equals("monedhendermarje"))
            {
                vlera = DbCore.DbAdmin.clsMonedha.ktheMonedhenENdermarrjes(idNdermarrje);
                return vlera;
            }


            if (emriParam.StartsWith("txtinterval"))
            {
                vlera = ktheVlerenEKontrollit(clsKontroll.merrKontrollSipasKoditKomponentes(emriParam, 649), "ASPxTextBox");
                if (idRaporti != 268)
                {
                    if (vlera == "")
                        vlera = "9999999999999";
                }
                return vlera;
            }

            //Llogariten vlerat e parametrave qe formohen nga filtrat e raporteve
            List<clsKontroll> oColKontrolle = oColKontrolleRaporti.FindAll(x => x.IdGrupi == parametri.IdGrupKontroll);
            //oColKontrolle.merrKontrollet(parametri.IdParametri);

            string veprimiPara1 = "";
            string veprimiPara2 = "";
            string vlere1 = "";
            string vlere2 = "";
            string lidhes = "";
            //colTipeKontrolli tipet = new colTipeKontrolli();
            //tipet.mbushTipet();
            string kolonaFilter = parametri.KolonaDb;
            foreach (clsKontroll kontrolli in oColKontrolle)
            {
                string kodKontrolli = kontrolli.KodKontrolli;
                string tipiKontrollit = Enum.GetName(typeof(DbCore.DbShare.TipeKontrolli), kontrolli.IdTipiKontrollit); //tipet.merrTipin(kontrolli.IdTipiKontrollit).TipiKontrollit;
                for (int g = 0; g < navBarFiltrat.Groups.Count; g++) //per momentin nuk po e marr parasysh daten
                {
                    System.Web.UI.Control uiKontroll = navBarFiltrat.Groups[g].FindControl(kodKontrolli);
                    if (uiKontroll != null)
                    {
                        switch (tipiKontrollit)
                        {
                            #region ASPxTextBox
                            case "ASPxTextBox":
                                ASPxTextBox textBoxi = (ASPxTextBox)uiKontroll;
                                if (textBoxi.Text.Trim() == "")
                                    continue;
                                if (kodKontrolli.Contains("1"))
                                {
                                    vlere1 = textBoxi.Text;
                                    continue;
                                }
                                if (kodKontrolli.Contains("2"))
                                {
                                    vlere2 = textBoxi.Text;
                                    continue;
                                }
                                if (kodKontrolli.ToString().ToLower().StartsWith("txtpershkrimfature"))
                                {
                                    vlera += textBoxi.Text;
                                    continue;

                                }
                                if (kodKontrolli.ToString().ToLower().StartsWith("llogdebi_textbox"))
                                {
                                    vlera += textBoxi.Text;
                                }

                                if (kodKontrolli.ToString().ToLower().StartsWith("dega_textbox"))
                                {


                                    vlera += textBoxi.Text;
                                }

                                if (kodKontrolli.ToString().ToLower().StartsWith("txtemmbshoqeri"))
                                {

                                    vlera += textBoxi.Text;
                                    continue;
                                }
                                if (kodKontrolli.ToString().ToLower().StartsWith("txtnrtel"))
                                {

                                    vlera += textBoxi.Text;
                                    continue;
                                }

                                if (kodKontrolli.ToString().ToLower().StartsWith("txtcustomernumber"))
                                {

                                    vlera += textBoxi.Text;
                                    continue;
                                }
                                if (kodKontrolli.ToString().ToLower().StartsWith("txtvlereshitjep"))
                                {
                                    //vlera += String.Format("{0} > {1} ", kolonaFilter, textBoxi.Text);
                                    vlera += textBoxi.Text;
                                }
                                if (kodKontrolli.ToString().ToLower().StartsWith("txtvlereshitjed"))
                                {
                                    vlera += textBoxi.Text;
                                    //vlera += String.Format("{2} {0} < {1} ", kolonaFilter, textBoxi.Text, " AND ");
                                }
                                continue;
                            #endregion

                            #region ASPxComboBox
                            case "ASPxComboBox":
                                ASPxComboBox combo = (ASPxComboBox)uiKontroll;
                                if (combo.Text.Trim() == "")
                                    continue;
                                if (kodKontrolli.ToString().ToLower().StartsWith("cmblidhesa")) // && !kodKontrolli.ToString().ToLower().StartsWith("cmbLidhesaPershk")
                                {
                                    lidhes = combo.Value.ToString();
                                    if (lidhes == "and")
                                        lidhes = "dhe";
                                    if (lidhes == "or")
                                        lidhes = "ose";
                                    continue;
                                }
                                if (kodKontrolli.ToString().ToLower().StartsWith("cmbvep"))
                                {
                                    if (kodKontrolli.ToString().Contains("1"))
                                    {
                                        veprimiPara1 = combo.Value.ToString();
                                        continue;
                                    }
                                    if (kodKontrolli.ToString().Contains("2"))
                                    {
                                        veprimiPara2 = combo.Value.ToString();
                                        continue;
                                    }
                                }
                                if (kodKontrolli.ToString().StartsWith("cmbFormatNumri"))
                                    return vlera = Convert.ToString(combo.SelectedIndex);

                                vlera = String.Format("{0}", combo.Value);


                                if (kodKontrolli.ToString().ToLower().StartsWith("cmbarkabanka"))
                                {
                                    //if (combo.Value.ToString() == "0")
                                    //    combo.Value = "Arka";
                                    //if (combo.Value.ToString() == "1")
                                    //    combo.Value = "Banka";

                                    vlera = String.Format("{0}", combo.Text);
                                }
                                if (kodKontrolli.ToString().ToLower().StartsWith("cmbniveli"))
                                {
                                    //if (combo.SelectedIndex == 1)
                                    //    combo.Value = "Hyrje";
                                    //if (combo.SelectedIndex == 2)
                                    //    combo.Value = "Dalje";
                                    vlera = String.Format("{0}", combo.Text);
                                }
                                if (kodKontrolli.ToString().ToLower().StartsWith("cmbstandarti"))
                                {
                                    //if (combo.SelectedIndex == 1)
                                    //    combo.Value = "Hyrje";
                                    //if (combo.SelectedIndex == 2)
                                    //    combo.Value = "Dalje";
                                    vlera = String.Format("{0}", combo.Text);
                                }
                                if (kodKontrolli.ToString().ToLower().StartsWith("cmbstatusmagazine"))
                                {
                                    vlera = String.Format("{0}", combo.Text);
                                }
                                if (kodKontrolli.ToString().ToLower().StartsWith("cmbNjesiKohe"))
                                {
                                    vlera = String.Format("{0}", combo.Value);
                                }
                                if (kodKontrolli.ToString().ToLower().StartsWith("cmbmonedha") || kodKontrolli.ToString().ToLower().StartsWith("cmbkonvertone"))
                                {
                                    if (idRaporti == 257)
                                    {
                                        vlera = String.Format("{0}", combo.Value);
                                        continue;
                                    }
                                    vlera = String.Format("{0}", combo.Text);
                                }
                                if (kodKontrolli.ToString().ToLower().StartsWith("cmbgrup"))
                                {
                                    if (kodKontrolli.Contains("1"))
                                    {
                                        vlere1 = String.Format("{0}", combo.Text); vlera = "";
                                        continue;
                                    }
                                    if (kodKontrolli.Contains("2"))
                                    {
                                        vlere2 = String.Format("{0}", combo.Text); vlera = "";
                                        continue;
                                    }

                                }
                                if (kodKontrolli.ToString().ToLower().StartsWith("cmbqyteti1") || kodKontrolli.ToString().ToLower().StartsWith("cmbklasaartikullit1") || kodKontrolli.ToString().ToLower().StartsWith("cmbstatuskonvertimidyte1") || kodKontrolli.ToString().ToLower().StartsWith("cmbmenyrepagese1"))
                                {
                                    vlere1 = combo.Text;
                                    vlera = "";
                                }
                                if (kodKontrolli.ToString().ToLower().StartsWith("cmbqyteti2") || kodKontrolli.ToString().ToLower().StartsWith("cmbklasaartikullit2") || kodKontrolli.ToString().ToLower().StartsWith("cmbstatuskonvertimidyte2") || kodKontrolli.ToString().ToLower().StartsWith("cmbmenyrepagese2"))
                                {
                                    vlere2 = combo.Text;
                                    vlera = "";
                                }

                                continue;
                            #endregion

                            #region ASPxCheckBox

                            case "ASPxCheckBox":
                                ASPxCheckBox checkBoxi = (ASPxCheckBox)uiKontroll;
                                if (kodKontrolli.ToString().ToLower().StartsWith("cbmonedhekf") || kodKontrolli.ToString().ToLower().StartsWith("cbdetajuar"))
                                {
                                    if (!IsPostBack)
                                        return "True";
                                    else
                                        return checkBoxi.Checked.ToString();
                                }

                                if (kodKontrolli.ToString() == "cbMbylljeViti")
                                {
                                    if (checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterPo", ci);
                                    else if (!checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterJo", ci);

                                    return vlera;

                                }
                                if (kodKontrolli.ToString() == "cbShfaqArtGjendjeZero")
                                {
                                    if (checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterPo", ci);
                                    else if (!checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterJo", ci);

                                    return vlera;

                                }
                                if (kodKontrolli.ToString() == "cbDraft")
                                {
                                    if (checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterPo", ci);
                                    else if (!checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterJo", ci);

                                    return vlera;

                                }

                                if (kodKontrolli.ToString() == "cbShfaqLlogP")
                                {
                                    if (checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterPo", ci);
                                    else if (!checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterJo", ci);

                                    return vlera;

                                }
                                if (kodKontrolli.ToString() == "cbMonedheKF" || kodKontrolli.ToString() == "checkAfishoPagen")
                                {
                                    if (checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterPo", ci);
                                    else if (!checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterJo", ci);

                                    return vlera;

                                }
                                if (kodKontrolli.ToString().ToLower().StartsWith("cbazhornim"))
                                {
                                    if (checkBoxi.Checked)
                                        return rm.GetString("cmbFilterPo", ci);
                                    return rm.GetString("cmbFilterJo", ci);
                                }
                                if (kodKontrolli.ToString().ToLower().StartsWith("checkllogarisintetike"))
                                {
                                    if (checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterPo", ci);
                                    else
                                        vlera = rm.GetString("cmbFilterJo", ci);
                                    return vlera;
                                }


                                continue;
                            #endregion

                            #region ASPxDateEdit
                            case "ASPxDateEdit":
                                ASPxDateEdit dateEdit = (ASPxDateEdit)uiKontroll;
                                if (dateEdit.Text.Trim() == "")
                                    continue;
                                if (kodKontrolli == "dteDtMaturimi")
                                {
                                    vlera = dateEdit.Text.Trim();
                                    continue;
                                }
                                continue;
                            #endregion

                            #region ASPxButtonEdit
                            case "ASPxButtonEdit":
                                ASPxButtonEdit buttonEdit = (ASPxButtonEdit)uiKontroll;
                                if (buttonEdit.Text.Trim() == "")
                                    continue;
                                string[] arr = buttonEdit.Text.Split(',');
                                string pershkrimet = "";
                                for (int v = 0; v < arr.Length; v++)
                                {
                                    if (kodKontrolli.ToString().ToLower().StartsWith("btnarkabankaemra") || kodKontrolli.ToString().ToLower().StartsWith("cmbarkabanka"))
                                    {
                                        DbCore.DbArkaBanka.clsBanka b = new DbCore.DbArkaBanka.clsBanka();
                                        b.mbushBankeSipasKodit(arr[v], idNdermarrje);
                                        pershkrimet += b.EmerBanka + ",";
                                    }
                                    else
                                        if (kodKontrolli.ToString().ToLower().StartsWith("btnekodkfkryesore") || kodKontrolli.ToString().ToLower().StartsWith("btnekodkf") || kodKontrolli.ToString().ToLower().StartsWith("btnefurnart"))
                                    {
                                        DbCore.DbKontabiliteti.clsKlientFurnitor b = new DbCore.DbKontabiliteti.clsKlientFurnitor();
                                        b.mbushKlientFurnitorSipasKodit(arr[v], idNdermarrje);
                                        pershkrimet += b.EmertimiKF + ",";
                                    }
                                    else
                                            if (kodKontrolli.ToString().ToLower().StartsWith("txtbtnmagazina") || kodKontrolli.ToString().ToLower().StartsWith("txtbtnmagazinades"))
                                    {
                                        DbCore.DbRegjistrim.clsNjesiAdministrative b = new DbCore.DbRegjistrim.clsNjesiAdministrative(arr[v], idNdermarrje);

                                        pershkrimet += b.Pershkrimi + ",";
                                    }
                                    else
                                                if (kodKontrolli.ToString().ToLower().StartsWith("txtbtnkartela"))
                                    {
                                        DbCore.DbInventari.clsArtikulli b = new DbCore.DbInventari.clsArtikulli();
                                        b.mbushArtikull(arr[v], idNdermarrje);
                                        pershkrimet += b.PershkrimArtikulli + ",";
                                    }
                                    else
                                                    if (kodKontrolli.ToString().ToLower().StartsWith("pikeshfbuttonedit"))
                                    {
                                        DbCore.DbRegjistrim.clsPikeShitjeFurnizimi b = new DbCore.DbRegjistrim.clsPikeShitjeFurnizimi(arr[v], idNdermarrje);

                                        pershkrimet += b.Pershkrimi + ",";
                                    }
                                    else
                                                        if (kodKontrolli.ToString().ToLower().StartsWith("degeadminbuttonedit"))
                                    {
                                        DbCore.DbRegjistrim.clsDegeAdministrative b = new DbCore.DbRegjistrim.clsDegeAdministrative(arr[v], idNdermarrje);

                                        pershkrimet += b.Pershkrimi + ",";
                                    }
                                    else
                                                            if (kodKontrolli.ToString().ToLower().StartsWith("btneqenderkosto"))
                                    {
                                        DbCore.DbQendraKosto.clsQendraKosto b = new DbCore.DbQendraKosto.clsQendraKosto(arr[v], idNdermarrje);

                                        pershkrimet += b.Pershkrimi + ",";
                                    }
                                    else pershkrimet = buttonEdit.Value.ToString();
                                    if (pershkrimet.Length > 200 && v < arr.Length - 1)
                                    {
                                        pershkrimet += "...";
                                        break;
                                    }

                                }
                                if (pershkrimet.Substring(pershkrimet.Length - 1, 1) == ",")
                                    pershkrimet = pershkrimet.Substring(0, pershkrimet.Length - 1);
                                if (kodKontrolli.Contains("1"))
                                {
                                    vlere1 = String.Format("{0}", pershkrimet);


                                    continue;
                                }
                                if (kodKontrolli.Contains("2"))
                                {

                                    vlere2 = String.Format("{0}", pershkrimet);


                                    continue;
                                }
                                if (kodKontrolli == "txtBtnMagazina" || kodKontrolli == "txtBtnKartela" || kodKontrolli == "txtBtnLlojDok" || kodKontrolli == "btnePerdorues" || kodKontrolli == "txtBtnKrijuesi" || kodKontrolli == "btnAgjentShitje" || kodKontrolli == "btnNivelCmimi" || kodKontrolli == "btneQenderKosto" || kodKontrolli == "btneAuto" || kodKontrolli == "btneKompania" || kodKontrolli == "btnSeriali1" || kodKontrolli == "btnBurimi" || kodKontrolli == "btnAktiviteti")
                                {
                                    // vlera = buttonEdit.Value.ToString();
                                    vlera = String.Format("{0}", pershkrimet);
                                    continue;
                                }
                                continue;
                            #endregion

                            #region ASPxRadioButtonList
                            case "ASPxRadioButtonList":
                                ASPxRadioButtonList radioKontrolli = ((ASPxRadioButtonList)uiKontroll);
                                if (radioKontrolli.SelectedItem.Value.ToString() == "")
                                    continue;

                                if (radioKontrolli.ID.StartsWith("radDtDok") || radioKontrolli.ID.StartsWith("radDtRegj") || radioKontrolli.ID.StartsWith("radDtDtFillimi") || radioKontrolli.ID.StartsWith("radDtDokAfatKohor") || radioKontrolli.ID.StartsWith("radPeriudhe") || radioKontrolli.ID.StartsWith("radDtPlanifikimi") || radioKontrolli.ID.StartsWith("radDtProdhimi") || radioKontrolli.ID.StartsWith("radDtMbarimi"))
                                {
                                    if (radioKontrolli.SelectedItem.Value.ToString() == "Aktuale")
                                    {
                                        //if (CacheLayer.GlobalCacheManager.MySessionCache["oPeriudhaAktuale"] != null)
                                        if (periudhaKontabel != null)
                                        {
                                            //clsPeriudhaKontabel periudhaKontabel = ((clsPeriudhaKontabel)CacheLayer.GlobalCacheManager.MySessionCache["oPeriudhaAktuale"]);
                                            //clsPeriudhaKontabel periudhaKontabel = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
                                            if (parametri.Emri.ToLower().Equals("filterdtkrijimiseriali"))
                                            {
                                                vlera = String.Format("{0} - {1}", periudhaKontabel.FillimiPeriudha.ToShortDateString(), periudhaKontabel.MbarimiPeriudha.ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().Equals("filterdtdokkrahasues"))
                                            {
                                                vlera = String.Format("{0} - {1}", periudhaKontabel.FillimiPeriudha.AddYears(-1).ToShortDateString(), periudhaKontabel.MbarimiPeriudha.AddYears(-1).ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().StartsWith("filterdt"))
                                            {
                                                vlera = String.Format("{0} - {1}", periudhaKontabel.FillimiPeriudha.ToShortDateString(), periudhaKontabel.MbarimiPeriudha.ToShortDateString());
                                                continue;
                                            }
                                        }
                                        continue;
                                    }
                                    if (radioKontrolli.SelectedItem.Value.ToString() == "VitiUshtrimor")
                                    {
                                        //if (DbCore.mySessionObjects.ktheNdermarrjeVit(Session) != null)
                                        if (idNderviti != -1)
                                        {
                                            //ndermviti.mbushNdermarrjeViti(int.Parse(DbCore.mySessionObjects.ktheNdermarrjeVit(Session).ToString()));
                                            clsNdermarrjeViti ndermviti = new clsNdermarrjeViti(idNderviti);
                                            //ndermviti.mbushNdermarrjeViti(idNderviti);
                                            if (parametri.Emri.ToLower().Equals("filterdtdokkrahasues"))
                                            {
                                                vlera = String.Format("{0} - {1}", ndermviti.NdermarrjeVitiFillim.AddYears(-1).ToShortDateString(), ndermviti.NdermarrjeVitiFund.AddYears(-1).ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().Equals("filterdtkrijimiseriali"))
                                            {
                                                vlera = String.Format("{0} - {1}", ndermviti.NdermarrjeVitiFillim.ToShortDateString(), ndermviti.NdermarrjeVitiFund.ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().StartsWith("filterdt"))
                                            {
                                                vlera = String.Format("{0} - {1}", ndermviti.NdermarrjeVitiFillim.ToShortDateString(), ndermviti.NdermarrjeVitiFund.ToShortDateString());
                                                continue;
                                            }
                                        }
                                        continue;
                                    }
                                    if (radioKontrolli.SelectedItem.Value.ToString() == "GjitheVitet")
                                    {
                                        if (parametri.Emri.ToLower().Equals("filterdtdokkrahasues"))
                                        {
                                            vlera = String.Format("{0} - {1}", Convert.ToDateTime("1900-01-01").ToShortDateString(), DateTime.MaxValue.ToShortDateString());
                                            continue;
                                        }
                                        if (parametri.Emri.ToLower().StartsWith("filterdt"))
                                        {
                                            vlera = String.Format("{0} - {1}", Convert.ToDateTime("1900-01-01").ToShortDateString(), DateTime.MaxValue.ToShortDateString());
                                            continue;
                                        }
                                        continue;
                                    }
                                    if (radioKontrolli.SelectedItem.Value.ToString() == "Periudha")
                                    {
                                        if (parametri.Emri.ToLower().Equals("filterdtdokkryesor"))
                                        {
                                            vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokKryesor")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokKryesor")).Date.ToShortDateString());
                                            continue;
                                        }
                                        if (parametri.Emri.ToLower().Equals("filterdtamortizimi"))
                                        {
                                            vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokAmortizim")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokAmortizim")).Date.ToShortDateString());
                                            continue;
                                        }
                                        if (parametri.Emri.ToLower().Equals("filterdtkrijimiseriali"))
                                        {
                                            vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDtNgaKrijimiAqtSerial")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDtDeriKrijimiAqtSerial")).Date.ToShortDateString());
                                            continue;
                                        }

                                        if (parametri.Emri.ToLower().Equals("filterdtreg"))
                                        {
                                            vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokReg")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokReg")).Date.ToShortDateString());
                                            continue;
                                        }
                                        if (parametri.Emri.ToLower().Equals("filterdtdoklidhes"))
                                        {
                                            vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokLidhes")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokLidhes")).Date.ToShortDateString());
                                            continue;
                                        }
                                        if (parametri.Emri.ToLower().Equals("filterdtdokkonvertuar"))
                                        {
                                            vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokKonvertuar")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokKonvertuar")).Date.ToShortDateString());
                                            continue;
                                        }
                                        if (parametri.Emri.ToLower().Equals("filterdtdokkrahasues"))
                                        {
                                            vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDtKrahasues")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDtKrahasues")).Date.ToShortDateString());
                                            continue;
                                        }
                                        if (parametri.Emri.ToLower().Equals("filterdtdokplanifikimi"))
                                        {
                                            vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDtPlanifikimi")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDtPlanifikimi")).Date.ToShortDateString());
                                            continue;
                                        }
                                        if (parametri.Emri.ToLower().Equals("filterdtdokprodhimi"))
                                        {
                                            vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDtProdhimi")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDtProdhimi")).Date.ToShortDateString());
                                            continue;
                                        }

                                        if (parametri.Emri.ToLower().Equals("filterdtdokafatkohor"))
                                        {
                                            vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokAfatKohor")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokAfatKohor")).Date.ToShortDateString());
                                            continue;
                                        }


                                        if (parametri.Emri.ToLower().StartsWith("filterdtdok"))
                                        {
                                            if (parametri.Emri.ToLower().StartsWith("filterdtdokqenderkosto"))
                                            {
                                                vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokQenderKosto")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokQenderKosto")).Date.ToShortDateString());
                                                continue;
                                            }
                                            else if (idRaporti == 98)
                                                vlera = String.Format("{0} - {1}", Convert.ToDateTime("1900-01-01").ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDok")).Date.ToShortDateString());
                                            else
                                                vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDok")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDok")).Date.ToShortDateString());
                                            continue;
                                        }

                                        if (parametri.Emri.ToLower().StartsWith("filterdtregj"))
                                        {
                                            vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaRegj")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriRegj")).Date.ToShortDateString());
                                            continue;
                                        }

                                        if (parametri.Emri.ToLower().StartsWith("filterdtfillimi"))
                                        {
                                            vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDtFillimi")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDtFillimi")).Date.ToShortDateString());
                                            continue;
                                        }
                                        if (parametri.Emri.ToLower().StartsWith("filterdtmbarimi"))
                                        {
                                            vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDtMbarimi")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDtMbarimi")).Date.ToShortDateString());
                                            continue;
                                        }
                                        if (parametri.Emri.ToLower().StartsWith("filterdturdherpagese"))
                                        {
                                            vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokUrdherPagese")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokUrdherPagese")).Date.ToShortDateString());
                                            continue;
                                        }
                                        if (parametri.Emri.ToLower().StartsWith("filterdtaprovimit"))
                                        {
                                            vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokAprovimit")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokAprovimit")).Date.ToShortDateString());
                                            continue;
                                        }
                                        if (parametri.Emri.ToLower().StartsWith("filterdtperiudhematurimi"))
                                        {
                                            vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaPeriudheMaturimi")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriPeriudheMaturimi")).Date.ToShortDateString());
                                            continue;
                                        }
                                        if (parametri.Emri.ToLower().StartsWith("filterdtperiudhefillimi"))
                                        {
                                            vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaPeriudheFillimi")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriPeriudheFillimi")).Date.ToShortDateString());
                                            continue;
                                        }
                                        if (parametri.Emri.ToLower().StartsWith("filterdtperiudhembarimi"))
                                        {
                                            vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaPeriudheMbarimi")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriPeriudheMbarimi")).Date.ToShortDateString());
                                            continue;
                                        }
                                        if (parametri.Emri.ToLower().StartsWith("filterDtStatusMagazine"))
                                        {
                                            vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDtStatus")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDtStatus")).Date.ToShortDateString());
                                            continue;
                                        }
                                    }
                                    continue;
                                }
                                continue;
                            #endregion
                            default:
                                continue;
                        }
                    }
                }
            }
            if (vlere1 != "")
                switch (veprimiPara1)
                {
                    case "0":
                        vlera += String.Format("{0}", vlere1);
                        break;
                    case "1":
                        vlera += String.Format("< {0}", vlere1);
                        break;
                    case "2":
                        vlera += String.Format("> {0}", vlere1);
                        break;
                    case "3":
                        vlera += String.Format(rm.GetString("cmbboxRaportiTeNdryshmeNga", ci) + " {0}", vlere1);
                        break;
                    case "4":
                        vlera += String.Format(rm.GetString("cmbboxItemFilterKryeFillon", ci) + " {0}", vlere1);
                        break;
                    case "5":
                        vlera += String.Format(rm.GetString("cmbboxItemFilterKryeMbaron", ci) + " {0}", vlere1);
                        break;
                    case "6":
                        vlera += String.Format(rm.GetString("cmbboxItemFilterKryePermban", ci) + " {0}", vlere1);
                        break;
                    case "7":
                        vlera += String.Format(rm.GetString("cmbboxItemFilterKryeBenPjeseNe", ci) + " {0}", vlere1);
                        break;
                    default:
                        break;
                }
            if (vlere2 != "" && lidhes != "")
            {
                vlera += " " + lidhes + " ";
                switch (veprimiPara2)
                {
                    case "0":
                        vlera += String.Format("{0}", vlere2);
                        break;
                    case "1":
                        vlera += String.Format("< {0}", vlere2);
                        break;
                    case "2":
                        vlera += String.Format("> {0}", vlere2);
                        break;
                    case "3":
                        vlera += String.Format(rm.GetString("cmbboxRaportiTeNdryshmeNga", ci) + " {0}", vlere2);
                        break;
                    case "4":
                        vlera += String.Format(rm.GetString("cmbboxItemFilterKryeFillon", ci) + " {0}", vlere2);
                        break;
                    case "5":
                        vlera += String.Format(rm.GetString("cmbboxItemFilterKryeMbaron", ci) + " {0}", vlere2);
                        break;
                    case "6":
                        vlera += String.Format(rm.GetString("cmbboxItemFilterKryePermban", ci) + " {0}", vlere2);
                        break;
                    case "7":
                        vlera += String.Format(rm.GetString("cmbboxItemFilterKryeBenPjeseNe", ci) + " {0}", vlere2);
                        break;
                    default:
                        break;
                }
            }
            return vlera;
        }


        private void mbushComboViteNderm(int idNdermarje, ASPxComboBox cmbViti, bool selectItem)
        {
            DataTable dt = DbCore.DbAdmin.colVitet.merrVitetNdermarjeDT(idNdermarje);
            if (!selectItem)
                dt.Rows.InsertAt(dt.NewRow(), 0);
            cmbViti.DataSource = dt;
            cmbViti.TextField = "KodiViti";
            cmbViti.ValueField = "IdViti";
            cmbViti.DataBind();
            if (selectItem)
                cmbViti.SelectedItem = cmbViti.Items.FindByText(Convert.ToString(DbCore.mySessionObjects.ktheVitiNdermarrjes(Session)));
        }

        /// <summary>
        /// Afishon raportin mbasi krijon duke i vene datasetin, duke konfiguruar fleten e raportit
        /// </summary>
        /// <param name="idRap">id-ja raportit</param>
        /// <param name="ruajStyle">te ruhet style apo jo</param>
        protected void afisho(int idRaporti, XtraReport report, clsSP oSp, SqlParameter[] sqlParam, int idPerdorues, bool azhornim, int idNdermarje, int idnderviti, DateTime dtmbarimi, int idkonfig, int idperiudha, ResourceManager rm, CultureInfo ci, String guidString)
        {
            if (idRaporti >= 0)
            {
                string spemri = "";
                if (idRaporti == 180)
                {
                    object[] param = new object[5];

                    param[0] = azhornim;
                    param[1] = dtmbarimi;
                    param[2] = idkonfig;
                    param[3] = idperiudha;
                    SqlParameter[] sqlParam1 = (SqlParameter[])sqlParam.Clone();
                    param[4] = sqlParam1;
                    DbCore.mySessionObjects.ruajParametratERaportit(Session, param, guidString);
                }

                switch (idRaporti)
                {
                    // e pasylip
                    case 291:
                        spemri = "PRC_Rap_KartelaPages_EPaySlip";
                        break;
                    case 285:
                        spemri = "PRC_Rap_MemoAnnualBonus_EPaySlip";
                        break;
                    case 281:
                        spemri = "PRC_Rap_MemoAnnualDeclaration_EPaySlip";
                        break;


                }

                ReportFunctions.konfigDataSetRaporti(report, spemri, idPerdorues, azhornim, idNdermarje, idnderviti, dtmbarimi, idkonfig, idperiudha, sqlParam);
                ImbReportToolbar1.konfigFleteRaporti(report, idPerdorues, EmerRealRaporti);
                //CacheLayer.GlobalCacheManager.MySessionCache["MyReport"] = report; //KEVI 

                DbCore.mySessionObjects.ruajMyReportNeSession(Session, guidString, report);


                //   DevExpress.XtraReports.UI.Band band = report.Bands.GetBandByType(typeof(DevExpress.XtraReports.UI.ReportHeaderBand));
                DataSet ds = (DataSet)report.DataSource;
               
                ReportViewer2.Report = report;

                // }
                //band.Visible = true;  
                //   ReportViewer2.DataBind();
            }
        }

        protected void btnRuaj_Click(object sender, EventArgs e)
        {

        }
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            if (e.Item.Name == "Shiko")
            {
                afisho(idRaporti, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheNdermarrjeVit(Session), DbCore.mySessionObjects.merrPeriudheKontabel(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), new CultureInfo("sq-AL"), clsRaporti.KaSubRaporte(idRaporti), hfState["guidString"].ToString(), Convert.ToInt32(hfState["idGjuha"]));
         
            }

        
        }



        private colFilterKoka ktheFiltraRaporti()
        {
            return null;
        }


        protected void Apliko_ASPxButton_Click(object sender, EventArgs e)
        {

            //kap item qe ka template ne menune e kesaj faqeje
            DevExpress.Web.MenuItem itemButton = ASPxMenuToolBar.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.E_PaySlip.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            colFilterTrupi filtraTrupi = new colFilterTrupi(Convert.ToInt32(cmbFiltra.Value));
        }


        public void ASPxButtonFshiFilterOk_Click(object sender, EventArgs e)
        {
            DevExpress.Web.MenuItem itemButton = ASPxMenuToolBar.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.E_PaySlip.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            int idFilter = Convert.ToInt32(cmbFiltra.Value);
            clsFilterKoka filtri = new clsFilterKoka(idFilter);
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            filtri.IdPerdoruesi = idPerdoruesi;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            mesazh = filtri.fshistatus();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            cmbFiltra.Text = "";
            mbushComboBoxFiltra(idPerdoruesi, idNdermarrje);
            percaktoTemplateMenu(ASPxMenuToolBar, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), idPerdoruesi, idNdermarrje);

            if (mesazh.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            //cmbFiltra.Items.Remove(new ListEditItem(Convert.ToString(cmbFiltra.Value)));
            cmbFiltra.DataBind();

            //((UpdatePanel)navBarFiltrat.Groups[0].FindControl("updfiltrat")).Update();
        }


        protected void ASPxMenuToolBar_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenuToolBar, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

        }
        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {

            percaktoTemplateMenu(mySessionObjects.ktheGjuhe(Session), aSPxMenu1, MenuInfo, Ruaj_ASPxButton_Click, ASPxButtonFshiFilterOk_Click, null, null, DbCore.mySessionObjects.merrEshteMemeSesioni(Session), idNdermarrje, this);

        }


        public static void percaktoTemplateMenu(int idgjuha, ASPxMenu aSPxMenu1, ASPxMenu MenuInfo, EventHandler Ruaj_ASPxButton_Click, EventHandler FshiFilter_ASPxButton_Click, EventHandler btnPo_Click, EventHandler btnJo_Click, bool meme, int idNdermarrje, Page page)
        {
            DbCore.DbShare.colMenuItem menu = new DbCore.DbShare.colMenuItem(idgjuha);
            menu.merrMenuItemSipasKomponentes(idgjuha, 649);
            //per momentin po e fshijme nga kodi sepse nuk kemi kohe ta testojme kalimin ne komponente te re

            menu.RemoveAll(x => x.Name.EqualsAnyIgnoreCase("Gjenero", "HapPopup", "ItemFrame", "ItemFilter", "Pastro"));
            foreach (var item in menu)
            {
                clsToolbarConfig.ShtoMenuItem(page.Theme, aSPxMenu1, item, false);
                //var itemMenu = aSPxMenu1.Items.FindByName(item.Name);
                //itemMenu.Visible = true;
            }

            if (btnJo_Click != null)
                clsMenuInfo.ShtoMenuItemInfo(page, MenuInfo, btnPo_Click, btnJo_Click);
            else clsMenuInfo.ShtoMenuItemInfo(page, MenuInfo);
        }


        /// <summary>
        /// dhurate nga evi :D
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {

        }
        protected void btnruajfiltrin_Click(object sender, EventArgs e)
        {

            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            clsFilterKoka oFilterKoka = new clsFilterKoka(txtkodifiltrit.Text, txtpershkrimifiltrit.Text, idPerdoruesi, idNdermarrje,
                idRaporti, ASPxCheckBoxLocalRaport.Checked, ASPxCheckBoxLocalPerdorues.Checked, ASPxCheckBoxLocalNdermarrje.Checked, 1);
            //krijohet obj i kokes se filtrit

            colFilterTrupi kolFilterTrupi = new colFilterTrupi();
            colKontrolle oColKontrolle = new colKontrolle();
            oColKontrolle.merrKontrolletRaporti(idRaporti);

            foreach (clsKontroll kontroll in oColKontrolle)
            {
                string vlera = ktheVlerenEKontrollit(kontroll, Enum.GetName(typeof(DbCore.DbShare.TipeKontrolli), kontroll.IdTipiKontrollit));// tipet.merrTipin(kontroll.IdTipiKontrollit).TipiKontrollit);
                if (vlera != "")
                {
                    kolFilterTrupi.Add(new clsFilterTrupi(kontroll.IdKontrolli, vlera));
                    int idKontrollKryesore;
                    switch (kontroll.IdKontrolli)
                    {
                        case 1071:
                            idKontrollKryesore = 1362;
                            break;
                        case 1072:
                            idKontrollKryesore = 1363;
                            break;
                        case 1073:
                            idKontrollKryesore = 1364;
                            break;
                        case 1074:
                            idKontrollKryesore = 1365;
                            break;
                        case 1075:
                            idKontrollKryesore = 1366;
                            break;
                        case 1362:
                            idKontrollKryesore = 1071;
                            break;
                        case 1363:
                            idKontrollKryesore = 1072;
                            break;
                        case 1364:
                            idKontrollKryesore = 1073;
                            break;
                        case 1365:
                            idKontrollKryesore = 1074;
                            break;
                        case 1366:
                            idKontrollKryesore = 1075;
                            break;
                        default:
                            idKontrollKryesore = -1;
                            break;
                    }

                    if (idKontrollKryesore != -1)
                    {
                        kolFilterTrupi.Add(new clsFilterTrupi(idKontrollKryesore, vlera));
                    }
                }
            }
            #region komentuar
            //colfilrap.merrFilterPerRaport(idRaporti);

            //for (int i = 0; i < colfilrap.Count; i++)
            //{                
            //    colKomponentPerFilter colkompfil = new colKomponentPerFilter();
            //    colkompfil.merrKomponentPerFilter(colfilrap[i].IdFilter);
            //    foreach (clsKomponentPerFilter clskompfiler in colkompfil)
            //    {
            //        if (navBarFiltrat.Groups[0].FindControl(clskompfiler.KomponenteEmri) != null)
            //        {
            //            if (clskompfiler.KomponenteEmri.StartsWith("rad"))
            //            {
            //                new clsFilterTrupi(clskompfiler.KomponenteEmri, ((RadioButtonList)navBarFiltrat.Groups[0].FindControl(clskompfiler.KomponenteEmri)).SelectedItem.ToString());
            //            }
            //            else if (clskompfiler.KomponenteEmri.ToLower().StartsWith("txt") && ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl(clskompfiler.KomponenteEmri)).ClientVisible == true)
            //            {
            //                KrijoObjektFiltri(clskompfiler.KomponenteEmri, ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl(clskompfiler.KomponenteEmri)).Text.Trim());
            //            }
            //        }
            //        else if (navBarFiltrat.Groups[1].FindControl(clskompfiler.KomponenteEmri) != null)
            //        {
            //            if (clskompfiler.KomponenteEmri.StartsWith("rad"))
            //            {
            //                KrijoObjektFiltri(clskompfiler.KomponenteEmri, ((RadioButtonList)navBarFiltrat.Groups[1].FindControl(clskompfiler.KomponenteEmri)).SelectedItem.ToString());
            //            }
            //            else if (clskompfiler.KomponenteEmri.ToLower().StartsWith("txtbtn") && ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl(clskompfiler.KomponenteEmri)).ClientVisible == true)
            //            {
            //                KrijoObjektFiltri(clskompfiler.KomponenteEmri, ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl(clskompfiler.KomponenteEmri)).Text.Trim());
            //            }
            //            else if (clskompfiler.KomponenteEmri.StartsWith("txt"))
            //            {
            //                if (navBarFiltrat.Groups[1].FindControl(clskompfiler.KomponenteEmri).GetType().Name == "ASPxDateEdit")
            //                    KrijoObjektFiltri(clskompfiler.KomponenteEmri, ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl(clskompfiler.KomponenteEmri)).Text.Trim());
            //                else
            //                    KrijoObjektFiltri(clskompfiler.KomponenteEmri, ((ASPxTextBox)navBarFiltrat.Groups[1].FindControl(clskompfiler.KomponenteEmri)).Text.Trim());
            //            }
            //            else if (clskompfiler.KomponenteEmri.StartsWith("cmb"))
            //            {
            //                KrijoObjektFiltri(clskompfiler.KomponenteEmri, ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl(clskompfiler.KomponenteEmri)).Text.Trim());
            //            }
            //        }
            //    }
            //}
            #endregion

            //oFilterKoka.OColFilterTrupi = colFilterTrupi;
            DbCore.clsMesazh mesazhi = oFilterKoka.krijoFilter(kolFilterTrupi);
            mbushComboBoxFiltra(idPerdoruesi, idNdermarrje);
            percaktoTemplateMenu(ASPxMenuToolBar, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), idPerdoruesi, idNdermarrje);

            if (mesazhi.Status)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazhi.PershkrimMesazhi, pnlMesazhi);
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhi.PershkrimMesazhi, pnlMesazhi);

        }

        protected void navBarFiltrat_ItemClick(object source, DevExpress.Web.NavBarItemEventArgs e)
        {

        }

        private void rregulloFiltraDateDokumenti()
        {
            if (idRaporti == 66 || idRaporti == 65)
                return;
            if (!String.IsNullOrEmpty(Request.QueryString["radButon"]))
            {
                int dtDokPeriudhaZgjedhur = Convert.ToInt32(Request.QueryString["radButon"]);
                ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDok")).SelectedIndex = dtDokPeriudhaZgjedhur;
                ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDokUrdherPagese")).SelectedIndex = dtDokPeriudhaZgjedhur;
                ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDokAprovimit")).SelectedIndex = dtDokPeriudhaZgjedhur;
                ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDokQenderKosto")).SelectedIndex = dtDokPeriudhaZgjedhur;
                ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDokKonvertuar")).SelectedIndex = dtDokPeriudhaZgjedhur;
                ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDtFillimi")).SelectedIndex = dtDokPeriudhaZgjedhur;

            }
            if (!String.IsNullOrEmpty(dtdoknga))
            {
                string dataNga = Convert.ToString(dtdoknga);
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDokQenderKosto")).Text = dataNga;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDokQenderKosto")).EnableClientSideAPI = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDok")).Text = dataNga;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDok")).EnableClientSideAPI = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDokUrdherPagese")).Text = dataNga;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDokUrdherPagese")).EnableClientSideAPI = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDokAprovimit")).Text = dataNga;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDokAprovimit")).EnableClientSideAPI = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDokKonvertuar")).Text = dataNga;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDokKonvertuar")).EnableClientSideAPI = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtNgaDtFillimi")).Text = dataNga;
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtNgaDtFillimi")).EnableClientSideAPI = true;
            }
            if (!String.IsNullOrEmpty(dtdokderi))
            {
                string dataDeri = Convert.ToString(dtdokderi);
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDokQenderKosto")).Text = dataDeri;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDokQenderKosto")).Enabled = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDok")).Text = dataDeri;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDok")).Enabled = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDokUrdherPagese")).Text = dataDeri;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDokUrdherPagese")).Enabled = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDokAprovimit")).Text = dataDeri;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDokAprovimit")).Enabled = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDokKonvertuar")).Text = dataDeri;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDokKonvertuar")).Enabled = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtDeriDtFillimi")).Text = dataDeri;
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtDeriDtFillimi")).Enabled = true;
            }
        }

        protected void ReportViewer2_CacheReportDocument(object sender, CacheReportDocumentEventArgs e)
        {
            e.Key = Guid.NewGuid().ToString();
            CacheLayer.GlobalCacheManager.MySessionCache[e.Key] = e.SaveDocumentToMemoryStream();
        }

        protected void ReportViewer2_RestoreReportDocumentFromCache(object sender, RestoreReportDocumentFromCacheEventArgs e)
        {

            Stream stream = CacheLayer.GlobalCacheManager.MySessionCache[e.Key] as Stream;
            if (stream != null)
                e.RestoreDocumentFromStream(stream);
            //     ReportViewer2.Report = GetReport();
        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ASPxLabelFshiFilter.Text = rm.GetString("LabelFshiFilter", ci);
            // grupi i filtrave kryesorev
            navBarFiltrat.Groups[0].Text = rm.GetString("navbarGroupFiltraKryesore", ci);
            //  Name = rm.GetString("navbarGroupFiltraKryesore", ci);
            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblKlientFurnitoriKryesore")).Text = rm.GetString("labelFilterKryeAvancKlientFurnitor", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbVeprimiKFkryesore1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbVeprimiKFkryesore1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbVeprimiKFkryesore1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbVeprimiKFkryesore1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbLidhesaKFkryesore")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbLidhesaKFkryesore")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbVeprimiKFkryesore2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbVeprimiKFkryesore2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbVeprimiKFkryesore2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbVeprimiKFkryesore2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            if (idRaporti == 276)
                ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblDtDok")).Text = rm.GetString("labelRaportData", ci);
            else
                ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblDtDok")).Text = rm.GetString("labelFilterKryesorDtDokumenti", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDok")).Items[0].Text = rm.GetString("RadioButtonListEditItemAktuale", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDok")).Items[1].Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDok")).Items[2].Text = rm.GetString("RadioButtonListEditItemVitiUshtrimor", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDok")).Items[3].Text = rm.GetString("RadioButtonListEditItemGjitheVitet", ci);


            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblBuxhetet")).Text = rm.GetString("buxhetiTab", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbBuxhetet")).Items[0].Text = rm.GetString("buxhetiTab", ci) + " 1";
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbBuxhetet")).Items[1].Text = rm.GetString("buxhetiTab", ci) + " 2";

            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblNgaDok")).Text = rm.GetString("labelRaportiNga", ci);
            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblDeriDok")).Text = rm.GetString("labelRaportDeri", ci);

            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblDtDokKonvertuar")).Text = rm.GetString("labelFilterKryesorDtDokumenti", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDokKonvertuar")).Items[0].Text = rm.GetString("RadioButtonListEditItemAktuale", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDokKonvertuar")).Items[1].Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDokKonvertuar")).Items[2].Text = rm.GetString("RadioButtonListEditItemVitiUshtrimor", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDokKonvertuar")).Items[3].Text = rm.GetString("RadioButtonListEditItemGjitheVitet", ci);
            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblNgaDtDokKonvertuar")).Text = rm.GetString("labelRaportiNga", ci);
            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblDeriDtDokKonvertuar")).Text = rm.GetString("labelRaportDeri", ci);


            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblDtDokKrahasues")).Text = rm.GetString("labelFilterKryesorDtDokumentiKrahasues", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDokKrahasues")).Items[0].Text = rm.GetString("RadioButtonListEditItemAktuale", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDokKrahasues")).Items[1].Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDokKrahasues")).Items[2].Text = rm.GetString("RadioButtonListEditItemVitiUshtrimor", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDokKrahasues")).Items[3].Text = rm.GetString("RadioButtonListEditItemGjitheVitet", ci);
            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblNgaDokKrahasues")).Text = rm.GetString("labelRaportiNga", ci);
            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblDeriDokKrahasues")).Text = rm.GetString("labelRaportDeri", ci);

            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblTipGrafikuKrahasues")).Text = rm.GetString("labelFilterKryesorTipGrafikuKrahasues", ci);
            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblMuaji")).Text = rm.GetString("labelFilterKryesorMuaji", ci);
            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblPasqyra")).Text = rm.GetString("labelFilterKryesorRaporti", ci);
            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblDtDokUrdherPagese")).Text = rm.GetString("labelFilterKryesorDtUrdherPag", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDokUrdherPagese")).Items[0].Text = rm.GetString("RadioButtonListEditItemAktuale", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDokUrdherPagese")).Items[1].Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDokUrdherPagese")).Items[2].Text = rm.GetString("RadioButtonListEditItemVitiUshtrimor", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDokUrdherPagese")).Items[3].Text = rm.GetString("RadioButtonListEditItemGjitheVitet", ci);
            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblNgaDtDokUrdherPagese")).Text = rm.GetString("labelRaportiNga", ci);
            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblDeriDtDokUrdherPagese")).Text = rm.GetString("labelRaportDeri", ci);


            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblDtDokAprovimit")).Text = rm.GetString("filterDtAprovimit", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDokAprovimit")).Items[0].Text = rm.GetString("RadioButtonListEditItemAktuale", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDokAprovimit")).Items[1].Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDokAprovimit")).Items[2].Text = rm.GetString("RadioButtonListEditItemVitiUshtrimor", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDokAprovimit")).Items[3].Text = rm.GetString("RadioButtonListEditItemGjitheVitet", ci);
            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblNgaDtDokAprovimit")).Text = rm.GetString("labelRaportiNga", ci);
            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblDeriDtDokAprovimit")).Text = rm.GetString("labelRaportDeri", ci);

            // grupi i filtrave te avancuar
            navBarFiltrat.Groups[1].Text = rm.GetString("navbarGroupfiltraAvancuar", ci);
            // navBarFiltrat.Groups[1].Name = rm.GetString("navbarGroupfiltraAvancuar", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNrLlog")).Text = rm.GetString("labelFilterAvancuarNrLlogarie", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesa")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesa")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNrLlogariEkonomike")).Text = rm.GetString("labelFilterAvancuarLlogEkonomike", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrLlogariEkonomike1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrLlogariEkonomike1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrLlogariEkonomike1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrLlogariEkonomike1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaNrLlogariEkonomike")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaNrLlogariEkonomike")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrLlogariEkonomike2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrLlogariEkonomike2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrLlogariEkonomike2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrLlogariEkonomike2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);


            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNrNenLlogariEkonomike")).Text = rm.GetString("labelFilterAvancuarNenllogEkonomike", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrNenLlogariEkonomike1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrNenLlogariEkonomike1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrNenLlogariEkonomike1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrNenLlogariEkonomike1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaNrNenLlogariEkonomike")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaNrNenLlogariEkonomike")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrNenLlogariEkonomike2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrNenLlogariEkonomike2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrNenLlogariEkonomike2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrNenLlogariEkonomike2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNrPersonal")).Text = rm.GetString("labelFilterAvancuarNrPunonjesi", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim1NP")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim1NP")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim1NP")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim1NP")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaNP")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaNP")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim2NP")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim2NP")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim2NP")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim2NP")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDepartamenti")).Text = rm.GetString("labelFilterAvancuarDepartamenti", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim1Dep")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim1Dep")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim1Dep")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim1Dep")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaDep")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaDep")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim2Dep")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim2Dep")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim2Dep")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim2Dep")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblJobTitle")).Text = rm.GetString("lblJobTitle", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblStatusHR")).Text = rm.GetString("labelFilterAvancuarStatusi", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNrSigurimesh")).Text = rm.GetString("lblNumerSigurimesh", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblQenderKosto1")).Text = rm.GetString("lblQenderKosto1", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblQenderKosto2")).Text = rm.GetString("lblQenderKosto2", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGlobalBand")).Text = rm.GetString("lblGlobalBand", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblLocalBand")).Text = rm.GetString("lblLocalBand", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblLlojVeprimi")).Text = rm.GetString("lblLlojVeprimi", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblSAPID")).Text = rm.GetString("lblSAPID", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblArsyeja")).Text = rm.GetString("lblfilterArsyeja", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblKujtIAdresohet")).Text = rm.GetString("lblfilterKujtIAdresohet", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDrejtuar")).Text = rm.GetString("lblDrejtuar", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblAfishoPagen")).Text = rm.GetString("lblfilterAfishoPagen", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNendep")).Text = rm.GetString("labelFilterAvancuarNendepartamenti", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim1Nendep")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim1Nendep")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim1Nendep")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim1Nendep")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaNendep")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaNendep")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim2Nendep")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim2Nendep")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim2Nendep")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim2Nendep")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDepartamentiPunonjesit")).Text = rm.GetString("filterKodDepPunonjesit", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim1DepPunonjesit")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim1DepPunonjesit")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim1DepPunonjesit")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim1DepPunonjesit")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaDepPunonjesit")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaDepPunonjesit")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim2DepPunonjesit")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim2DepPunonjesit")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim2DepPunonjesit")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim2DepPunonjesit")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNendepPunonjesit")).Text = rm.GetString("filterKodNenDepPunonjesit", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim1NendepPunonjesit")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim1NendepPunonjesit")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim1NendepPunonjesit")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim1NendepPunonjesit")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaNendepPunonjesit")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaNendepPunonjesit")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim2NendepPunonjesit")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim2NendepPunonjesit")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim2NendepPunonjesit")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim2NendepPunonjesit")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDtRegj")).Text = rm.GetString("labelFilterAvancuarDtRegjistrimi", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtRegj")).Items[0].Text = rm.GetString("RadioButtonListEditItemAktuale", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtRegj")).Items[1].Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtRegj")).Items[2].Text = rm.GetString("RadioButtonListEditItemVitiUshtrimor", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblngaRegj")).Text = rm.GetString("labelRaportiNga", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDeriRegj")).Text = rm.GetString("labelRaportDeri", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDtFillimi")).Text = rm.GetString("lblDtFillimi", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDtFillimi")).Items[0].Text = rm.GetString("RadioButtonListEditItemAktuale", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDtFillimi")).Items[1].Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDtFillimi")).Items[2].Text = rm.GetString("RadioButtonListEditItemVitiUshtrimor", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblngaDtFillimi")).Text = rm.GetString("labelRaportiNga", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDeriDtFillimi")).Text = rm.GetString("labelRaportDeri", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDtMbarimi")).Text = rm.GetString("labelData_e_mbarimit", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtMbarimi")).Items[0].Text = rm.GetString("RadioButtonListEditItemAktuale", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtMbarimi")).Items[1].Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtMbarimi")).Items[2].Text = rm.GetString("RadioButtonListEditItemVitiUshtrimor", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblngaDtMbarimi")).Text = rm.GetString("labelRaportiNga", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDeriDtMbarimi")).Text = rm.GetString("labelRaportDeri", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDtDokKryesor")).Text = rm.GetString("labelFilterAvancuarDtDokKryesor", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDokKryesor")).Items[0].Text = rm.GetString("RadioButtonListEditItemAktuale", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDokKryesor")).Items[1].Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDokKryesor")).Items[2].Text = rm.GetString("RadioButtonListEditItemVitiUshtrimor", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDokKryesor")).Items[3].Text = rm.GetString("RadioButtonListEditItemGjitheVitet", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNgaDokKryesor")).Text = rm.GetString("labelRaportiNga", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDeriDokKryesor")).Text = rm.GetString("labelRaportDeri", ci);


            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDtDokLidhes")).Text = rm.GetString("labelFilterAvancuarDtDokLidhes", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDokLidhes")).Items[0].Text = rm.GetString("RadioButtonListEditItemAktuale", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDokLidhes")).Items[1].Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDokLidhes")).Items[2].Text = rm.GetString("RadioButtonListEditItemVitiUshtrimor", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDokLidhes")).Items[3].Text = rm.GetString("RadioButtonListEditItemGjitheVitet", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNgaDokLidhes")).Text = rm.GetString("labelRaportiNga", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDeriDokLidhes")).Text = rm.GetString("labelRaportDeri", ci);

            //per raportin Produktet sipas porosive i vendosim pershkrim tjeter
            if (idRaporti == 118) ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNrDok")).Text = rm.GetString("lblRaportNumerPorosie", ci);
            else

                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNrDok")).Text = rm.GetString("labelFilterAvancuarNrDok", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNrProjektProdhimi")).Text = rm.GetString("filterNrProjektProdhimi", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNrDokRezervime")).Text = rm.GetString("labelFilterAvancuarNrDok", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimDok1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimDok1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimDok1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimDok1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaDok")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaDok")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimDok2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimDok2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimDok2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimDok2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            //per raportin Produktet sipas porosive i vendosim pershkrim tjeter
            if (idRaporti == 118) ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNrDokKonvertuar")).Text = rm.GetString("lblRaportNumerProdhimi", ci);

            else

                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNrDokKonvertuar")).Text = rm.GetString("labelFilterAvancuarNrDok", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrDokKonvertuar1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrDokKonvertuar1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrDokKonvertuar1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrDokKonvertuar1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaNrDokKonvertuar")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaNrDokKonvertuar")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrDokKonvertuar2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrDokKonvertuar2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrDokKonvertuar2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrDokKonvertuar2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblKodArtikullBurim")).Text = rm.GetString("filterReceptura", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNrDokKryesor")).Text = rm.GetString("labelFilterAvancuarNrDokKryesor", ci);


            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimDok1Kryesor")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimDok1Kryesor")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimDok1Kryesor")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimDok1Kryesor")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaDokKryesor")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaDokKryesor")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimDok2Kryesor")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimDok2Kryesor")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimDok2Kryesor")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimDok2Kryesor")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNrDokLidhes")).Text = rm.GetString("labelFilterAvancuarNrDokLidhes", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimDok1Lidhes")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimDok1Lidhes")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimDok1Lidhes")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimDok1Lidhes")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaDokLidhes")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaDokLidhes")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimDok2Lidhes")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimDok2Lidhes")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimDok2Lidhes")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimDok2Lidhes")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNrSerial")).Text = rm.GetString("labelFilterAvancuarNrSerial", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrSerialUrdherPagese1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrSerialUrdherPagese1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrSerialUrdherPagese1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrSerialUrdherPagese1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaNrSerialUrdherPagese")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaNrSerialUrdherPagese")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrSerialUrdherPagese2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrSerialUrdherPagese2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrSerialUrdherPagese2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrSerialUrdherPagese2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNumerProjekti")).Text = rm.GetString("lblNumerProjekti", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNumerProjekti1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNumerProjekti1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNumerProjekti1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNumerProjekti1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesNumerProjekti")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesNumerProjekti")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNumerProjekti2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNumerProjekti2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNumerProjekti2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNumerProjekti2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblReferenca")).Text = rm.GetString("labelFilterAvancuarNrReference", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimRef1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimRef1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimRef1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimRef1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaRef")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaRef")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimRef2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimRef2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimRef2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimRef2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblPershkrimi")).Text = rm.GetString("labelFilterAvancuarPershkVeprimi", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimPershk1")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimPershk1")).Items[2].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaPershk")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaPershk")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimPershk2")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimPershk2")).Items[2].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);


            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblKlientFurnitori")).Text = rm.GetString("labelFilterKryeAvancKlientFurnitor", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKF1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKF1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKF1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKF1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaKF")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaKF")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKF2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKF2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKF2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKF2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);


            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblFurnArt")).Text = rm.GetString("labelFilterAvancuarFurnArt", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiFurnArt1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiFurnArt1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiFurnArt1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiFurnArt1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaFurnArt")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaFurnArt")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiFurnArt2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiFurnArt2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiFurnArt2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiFurnArt2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblLlojDok")).Text = rm.GetString("labelFilterAvancuarLlojDok", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiLlojDok1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiLlojDok1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiLlojDok1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiLlojDok1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaLlojDok")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaLlojDok")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiLlojDok2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiLlojDok2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiLlojDok2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiLlojDok2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);


            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblLlojDokLidhes")).Text = rm.GetString("labelFilterAvancuarLlojDokLidhes", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiLlojDok1Lidhes")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiLlojDok1Lidhes")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiLlojDok1Lidhes")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiLlojDok1Lidhes")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaLlojDokLidhes")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaLlojDokLidhes")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiLlojDok2Lidhes")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiLlojDok2Lidhes")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiLlojDok2Lidhes")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiLlojDok2Lidhes")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblmagazina")).Text = rm.GetString("labelFilterAvancuarMagazina", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblmagazinades")).Text = rm.GetString("labelFilterAvancuarMagazinaDes", ci);


            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblkartela")).Text = rm.GetString("labelFilterAvancuarKodArt", ci);

            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKartela1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKartela1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKartela1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKartela1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaKartela")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaKartela")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKartela2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKartela2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKartela2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKartela2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblBurimi")).Text = rm.GetString("burimiTab", ci);

            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKartela1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKartela1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKartela1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKartela1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaKartela")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaKartela")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKartela2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKartela2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKartela2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKartela2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblAktiviteti")).Text = rm.GetString("labelRaportAktiviteti", ci);

            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKartela1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKartela1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKartela1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKartela1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaKartela")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaKartela")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKartela2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKartela2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKartela2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKartela2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblKatShpenzimi")).Text = rm.GetString("lblKategoriShpenzimi", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKatShpenzimi1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKatShpenzimi1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKatShpenzimi1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKatShpenzimi1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKatShpenzimi1")).Items[7].Text = rm.GetString("cmbboxItemFilterKryeBenPjeseNe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaKatShpenzimi")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaKatShpenzimi")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKatShpenzimi2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKatShpenzimi2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKatShpenzimi2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKatShpenzimi2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKatShpenzimi2")).Items[7].Text = rm.GetString("cmbboxItemFilterKryeBenPjeseNe", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblpershkArt")).Text = rm.GetString("filterRaportPershkrimiArtikullit", ci);

            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiPershkArt1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiPershkArt1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiPershkArt1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiPershkArt1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaPershkArt")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaPershkArt")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiPershkArt2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiPershkArt2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiPershkArt2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiPershkArt2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            //((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblShenimeShitjeje")).Text = rm.GetString("filterRaportShenime2Shitje", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiShenimeShitjeje1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiShenimeShitjeje1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiShenimeShitjeje1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiShenimeShitjeje1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaShenime2Shitje")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaShenime2Shitje")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiShenime2Shitje2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiShenime2Shitje2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiShenime2Shitje2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiShenime2Shitje2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblPershkrimDetajimi")).Text = rm.GetString("filterPershkrimDetajimi", ci);

            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimPershkrimDetajimi1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimPershkrimDetajimi1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimPershkrimDetajimi1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimPershkrimDetajimi1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaPershkrimDetajimi")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaPershkrimDetajimi")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimPershkrimDetajimi2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimPershkrimDetajimi2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimPershkrimDetajimi2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimPershkrimDetajimi2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("grupimPLabel")).Text = rm.GetString("labelFilterAvancuarGrupimP", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("grupimPareLabel")).Text = rm.GetString("labelFilterAvancuarGrupimP", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("grupimDyteLabel")).Text = rm.GetString("labelFilterAvancuarGrupimD", ci);

            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1GrupimiP")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1GrupimiP")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1GrupimiP")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1GrupimiP")).Items[4].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaGrupimP")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaGrupimP")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2GrupimiP")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2GrupimiP")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2GrupimiP")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2GrupimiP")).Items[4].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("grupimDLabel")).Text = rm.GetString("labelFilterAvancuarGrupimD", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1GrupimiD")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1GrupimiD")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1GrupimiD")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1GrupimiD")).Items[4].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaGrupimD")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaGrupimD")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2GrupimiD")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2GrupimiD")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2GrupimiD")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2GrupimiD")).Items[4].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGrupPKF")).Text = rm.GetString("labelFilterAvancuarGrupim1KF", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1GrupPKF")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1GrupPKF")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1GrupPKF")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1GrupPKF")).Items[4].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaGrupimPKF")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaGrupimPKF")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2GrupPKF")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2GrupPKF")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2GrupPKF")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2GrupPKF")).Items[4].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);


            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGrupDKF")).Text = rm.GetString("labelFilterAvancuarGrupim2KF", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1GrupDKF")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1GrupDKF")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1GrupDKF")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1GrupDKF")).Items[4].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaGrupimDKF")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaGrupimDKF")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2GrupDKF")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2GrupDKF")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2GrupDKF")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2GrupDKF")).Items[4].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGrupTKF")).Text = rm.GetString("labelFilterAvancuarGrupim3KF", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1GrupTKF")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1GrupTKF")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1GrupTKF")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1GrupTKF")).Items[4].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaGrupimTKF")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaGrupimTKF")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2GrupTKF")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2GrupTKF")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2GrupTKF")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2GrupTKF")).Items[4].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblperdoruesi")).Text = rm.GetString("labelFilterAvancuarPerdorues", ci);





            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblMonedha")).Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGrupi")).Text = rm.GetString("labelFilterAvancuarGrupi", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNengrupi")).Text = rm.GetString("labelFilterAvancuarNengrupi", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblTipi")).Text = rm.GetString("labelFilterAvancuarTipi", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNiveli")).Text = rm.GetString("labelFilterAvancuarNengrupi", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDtMaturimi")).Text = rm.GetString("labelFilterAvancuarDtMaturimi", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblIntervalet")).Text = rm.GetString("labelFilterAvancuarIntervalet", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblFormati")).Text = rm.GetString("labelFilterAvancuarFormati", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblLlogKoresp")).Text = rm.GetString("labelFilterAvancuarLlogKoresp", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblMonedheKF")).Text = rm.GetString("labelFilterAvancuarMonedheKF", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGjendjaLlog")).Text = rm.GetString("labelFilterAvancuarGjendjaLlog", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbGjendjaLlog")).Items[1].Text = rm.GetString("cmbboxItemFilterAvancDebitore", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbGjendjaLlog")).Items[2].Text = rm.GetString("cmbboxItemFilterAvancKreditore", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblVeprimePeriudhe")).Text = rm.GetString("lblVeprimePeriudhe", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblStatusCRM")).Text = rm.GetString("labelFilterAvancuarStatusi", ci);
            //((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimePeriudhe")).Items[2].Text = rm.GetString("cmbboxItemFilterAvancJo", ci);
            //((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimePeriudhe")).Items[1].Text = rm.GetString("cmbboxItemFilterAvancPo", ci);
            //((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimePeriudhe")).Items[3].Text = rm.GetString("cmbboxItemFilterAvancTeGjitha", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGjendjeDetyrime")).Text = rm.GetString("labelRaportGjendje", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblqyteti")).Text = rm.GetString("labelFilterAvancuarQyteti", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimQyteti1")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimQyteti1")).Items[2].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaQyteti")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaQyteti")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimQyteti2")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimQyteti2")).Items[2].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblMenyrePagese")).Text = rm.GetString("labelRaportMenyrePagese", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblArkaBanka")).Text = rm.GetString("labelFilterAvancuarArkaBanka", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbArkaBanka")).Items[1].Text = rm.GetString("cmbboxItemFilterAvancArka", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbArkaBanka")).Items[2].Text = rm.GetString("cmbboxItemFilterAvancBanka", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("pikeShFLabel")).Text = rm.GetString("labelFilterAvancuarPikeShF", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiPikeShF1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiPikeShF1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiPikeShF1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiPikeShF1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaPikeShF")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaPikeShF")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiPikeShF2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiPikeShF2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiPikeShF2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiPikeShF2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);



            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDegeAdministrative")).Text = rm.GetString("labelFilterAvancuarDegeAdministrative", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiDegeAdmin1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiDegeAdmin1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiDegeAdmin1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiDegeAdmin1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaDegeAdmin")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaDegeAdmin")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiDegeAdmin2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiDegeAdmin2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiDegeAdmin2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiDegeAdmin2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);


            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNjesia")).Text = rm.GetString("labelFilterAvancuarNjesia", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbNjesia")).Items[1].Text = rm.GetString("cmbboxItemFilterAvancNjesiaP", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbNjesia")).Items[2].Text = rm.GetString("cmbboxItemFilterAvancNjesiaD", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("cmimiLabel")).Text = rm.GetString("labelFilterAvancuarCmimi", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbCmimeArtikulli")).Items[0].Text = rm.GetString("cmbboxItemFilterAvancCmimiP", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbCmimeArtikulli")).Items[1].Text = rm.GetString("cmbboxItemFilterAvancCmimiD", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblArtCmim")).Text = rm.GetString("labelFilterArtCmim", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbArtCmim")).Items[0].Text = rm.GetString("cmbboxItemFilterAvancTeGjithe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbArtCmim")).Items[1].Text = rm.GetString("cmbItemFilterArtCmim", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblLlojDetajim")).Text = rm.GetString("filterLlojiDetajimit", ci);
      

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDetajim")).Text = rm.GetString("lblRaportDetajime", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbDetajim")).Items[1].Text = rm.GetString("cmbboxItemFilterAvancTeGjithe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbDetajim")).Items[0].Text = rm.GetString("cmbboxItemArtikujDetajime", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblKlasaArtikullit")).Text = rm.GetString("labelFilterAvancuarKlasaArtikullit", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimKlasaArtikullit1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimKlasaArtikullit1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimKlasaArtikullit1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimKlasaArtikullit1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaKlasaArtikullit")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaKlasaArtikullit")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimKlasaArtikullit2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimKlasaArtikullit2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimKlasaArtikullit2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimKlasaArtikullit2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);


            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblLlojArtikulli")).Text = rm.GetString("filterLlojArtikulli", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLlojArtikulli")).Items[0].Text = rm.GetString("cmbboxItemFilterAvancTeGjithe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLlojArtikulli")).Items[1].Text = rm.GetString("cmbboxItemFilterAvancAfatshk", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLlojArtikulli")).Items[2].Text = rm.GetString("cmbboxItemFilterAvancAfatgjt", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblLlojSubjekti")).Text = rm.GetString("filterLlojSubjekti", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLlojSubjekti")).Items[1].Text = rm.GetString("comboItemBlerjeShitjeKlient", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLlojSubjekti")).Items[2].Text = rm.GetString("comboItemBlerjeShitjeFurnitor", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLlojSubjekti")).Items[3].Text = rm.GetString("labelRaportLlogari", ci);
            if (idRaporti == 75)
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblArkaBankaEmra")).Text = rm.GetString("cmbboxItemFilterAvancBanka", ci);
            else
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblArkaBankaEmra")).Text = rm.GetString("labelFilterAvancuarNjesiAB", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimABEmra1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimABEmra1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimABEmra1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimABEmra1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaABEmra")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaABEmra")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiABEmra2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiABEmra2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiABEmra2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiABEmra2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);


            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblLikuiduar")).Text = rm.GetString("labelFilterAvancuarDok", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblAzhornim")).Text = rm.GetString("filterRaportAzhornim", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblCikli")).Text = rm.GetString("labelFilterAvancuarCikli", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblShfaqVlerat")).Text = rm.GetString("labelFilterAvancuarVlerat", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblTipGrafiku")).Text = rm.GetString("labelFilterAvancuarLlojGrafiku", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblMbylljeViti")).Text = rm.GetString("labelFilterAvancuarMbylljeViti", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblLlogariSintetike")).Text = rm.GetString("filterLlogariSintetike", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("kodbariLabel")).Text = rm.GetString("labelFilterAvancuarKodbari", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKodbari1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKodbari1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKodbari1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKodbari1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaKodbari")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaKodbari")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKodbari2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKodbari2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKodbari2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKodbari2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);



            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblFormatNumri")).Text = rm.GetString("MenuItemFormatiNumrave", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblStatusi")).Text = rm.GetString("labelFilterAvancuarStatusi", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblLlojiArtBurim")).Text = rm.GetString("labelFilterAvancuarLlojArtBur", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGrup1")).Text = rm.GetString("filterGrupimDokP", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupP1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupP1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupP1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupP1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaGrup1")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaGrup1")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupP2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupP2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupP2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupP2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);


            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGrup2")).Text = rm.GetString("filterGrupimDokD", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupD1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupD1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupD1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupD1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaGrup1")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaGrup1")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupD2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupD2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupD2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupD2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGrup3")).Text = rm.GetString("filterGrupimDokT", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupT1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupT1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupT1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupT1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaGrup3")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaGrup3")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupT2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupT2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupT2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupT2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGjendjeArt")).Text = rm.GetString("labelFilterAvancuarGjendja", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDetajim1")).Text = rm.GetString("filterDetajimP", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiDetajimP1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiDetajimP1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiDetajimP1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiDetajimP1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaDetajim1")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaDetajim1")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiDetajimP2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiDetajimP2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiDetajimP2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiDetajimP2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDetajim2")).Text = rm.GetString("filterDetajimD", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiDetajimD1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiDetajimD1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiDetajimD1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiDetajimD1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaDetajim2")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaDetajim2")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiDetajimD2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiDetajimD2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiDetajimD2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiDetajimD2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblKatMaturimi")).Text = rm.GetString("labelRaportKategoriMaturimi", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKatMaturimi1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKatMaturimi1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKatMaturimi1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKatMaturimi1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaKatMaturimi")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaKatMaturimi")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKatMaturimi2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKatMaturimi2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKatMaturimi2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKatMaturimi2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblPerdorues")).Text = rm.GetString("filterRaportPerdoruesi", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblFilterKrijuesi")).Text = rm.GetString("filterRaportKrijuesi", ci);

            lblMsgbox.Text = rm.GetString("labelRaportMsgboxFshiRaport", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblPeriudheMaturimi")).Text = rm.GetString("labelFilterAvancuarDtMaturimi", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radPeriudheMaturimi")).Items[0].Text = rm.GetString("RadioButtonListEditItemAktuale", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radPeriudheMaturimi")).Items[1].Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radPeriudheMaturimi")).Items[2].Text = rm.GetString("RadioButtonListEditItemVitiUshtrimor", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNgaPeriudheMaturimi")).Text = rm.GetString("labelRaportiNga", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDeriPeriudheMaturimi")).Text = rm.GetString("labelRaportDeri", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblPershkrimFature")).Text = rm.GetString("filterRaportPershkrimFature", ci) + ":";
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDega")).Text = rm.GetString("filterDega", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblLlogDebiProcredit")).Text = rm.GetString("filterllogaridebiprocredit", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblLlogKrediProcredit")).Text = rm.GetString("filterllogarikrediprocredit", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblAgjentShitje")).Text = rm.GetString("filterRaportiAgjentetShitjes", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblEmMbShoqeri")).Text = rm.GetString("filterRaportiShoqeria", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNrTel")).Text = rm.GetString("filterRaportiNrTel", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblPaguar")).Text = rm.GetString("filterPaguar", ci);
            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblViti")).Text = rm.GetString("lblViti", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblEmertimLlog")).Text = rm.GetString("filterEmertimLlog", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGjendja")).Text = rm.GetString("filterGjendja", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lbShfaqLlogP")).Text = rm.GetString("filterLlogariPacaktuar", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblVlereShitje")).Text = rm.GetString("labelRaportVlera", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDeriVlereSh")).Text = rm.GetString("filterRaportiDeri", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblStatusKonvertimiDyte")).Text = rm.GetString("filterStatusKonvertimi", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblKompania")).Text = rm.GetString("filterKompania", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNivelCmimi")).Text = rm.GetString("filterRaportiNiveletCmimit", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDtProdhimi")).Text = rm.GetString("filterDtDokProdhimi", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDtPlanifikimi")).Text = rm.GetString("filterDtDokPlanifikimi", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDetajuar")).Text = rm.GetString("filterRaportiDetajuar", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblAuto")).Text = rm.GetString("filterRaportiAutomjeti", ci);

            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiAuto1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiAuto1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiAuto1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiAuto1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaAuto")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaAuto")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiAuto2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiAuto2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiAuto2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiAuto2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDtDokAfatKohor")).Text = rm.GetString("filterDtDokAfatKohor", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDtRegj")).Text = rm.GetString("labelFilterAvancuarDtRegjistrimi", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDokAfatKohor")).Items[0].Text = rm.GetString("RadioButtonListEditItemAktuale", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDokAfatKohor")).Items[1].Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDokAfatKohor")).Items[2].Text = rm.GetString("RadioButtonListEditItemVitiUshtrimor", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDokAfatKohor")).Items[3].Text = rm.GetString("RadioButtonListEditItemGjitheVitet", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNgaDokAfatKohor")).Text = rm.GetString("labelRaportiNga", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDeriDokAfatKohor")).Text = rm.GetString("labelRaportDeri", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblLlojPorosie")).Text = rm.GetString("filterLlojPorosie", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblPerfunduar")).Text = rm.GetString("filterStatusPerfunduar", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiPerf1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiPerf1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiPerf1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiPerf1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaPerfunduar")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaPerfunduar")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiPerf2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiPerf2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiPerf2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiPerf2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblProdhuar")).Text = rm.GetString("filterStatusProdhuar", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiProdh1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiProdh1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiProdh1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiProdh1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaProdhuar")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaProdhuar")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiProdh2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiProdh2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiProdh2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiProdh2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);


            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblFaturuar")).Text = rm.GetString("filterStatusFaturuar", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiFat1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiFat1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiFat1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiFat1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaFaturuar")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaFaturuar")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiFat2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiFat2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiFat2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiFat2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);


            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblSeriali")).Text = rm.GetString("filterSeriali", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiSeriali1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiSeriali1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiSeriali1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiSeriali1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaSeriali")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaSeriali")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiSeriali2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiSeriali2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiSeriali2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiSeriali2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblStandarti")).Text = rm.GetString("filterRaportiStandarti", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblStatusMagazine")).Text = rm.GetString("filterRaportiStatusMagazine", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDtAmortizimi")).Text = rm.GetString("filterRaportiDtAmortizimi", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNrDokQenderKosto")).Text = rm.GetString("labelFilterAvancuarNrDok", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDokAmortizimi")).Items[0].Text = rm.GetString("RadioButtonListEditItemAktuale", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDokAmortizimi")).Items[1].Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDokAmortizimi")).Items[2].Text = rm.GetString("RadioButtonListEditItemVitiUshtrimor", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDokAmortizimi")).Items[3].Text = rm.GetString("RadioButtonListEditItemGjitheVitet", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDtNgaAmortizimi")).Text = rm.GetString("labelRaportiNga", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDtDeriAmortizimi")).Text = rm.GetString("labelRaportDeri", ci);


            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblAdreseKlienti")).Text = rm.GetString("filterAdreseKlienti", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAdrKlient1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAdrKlient1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAdrKlient1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAdrKlient1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaAdrKl")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaAdrKl")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAdrKlient2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAdrKlient2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAdrKlient2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAdrKlient2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblAdresaFaturimit")).Text = rm.GetString("lblFilterAdreseFaturimi", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAdrFaturimit1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAdrFaturimit1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAdrFaturimit1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAdrFaturimit1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaAdrFaturimit")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaAdrFaturimit")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAdrFaturimit2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAdrFaturimit2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAdrFaturimit2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAdrFaturimit2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblLlogParapag")).Text = rm.GetString("filterRaportLlogParapagimi", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimLlogP1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimLlogP1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimLlogP1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimLlogP1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaLLogP")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaLLogP")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimLlogP2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimLlogP2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimLlogP2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimLlogP2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblLlogShp")).Text = rm.GetString("filterRaportLlogShpenzimi", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimLLogShp1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimLLogShp1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimLLogShp1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimLLogShp1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaLLogShp")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaLLogShp")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimLLogShp2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimLLogShp2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimLLogShp2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimLLogShp2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblQenderKosto")).Text = rm.GetString("cmbVleraQendraKosto", ci);
            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblDtDokQenderKosto")).Text = rm.GetString("labelFilterKryesorDtDokumenti", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDokQenderKosto")).Items[0].Text = rm.GetString("RadioButtonListEditItemAktuale", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDokQenderKosto")).Items[1].Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDokQenderKosto")).Items[2].Text = rm.GetString("RadioButtonListEditItemVitiUshtrimor", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDokQenderKosto")).Items[3].Text = rm.GetString("RadioButtonListEditItemGjitheVitet", ci);
            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblNgaDokQenderKosto")).Text = rm.GetString("labelRaportiNga", ci);
            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblDeriDokQenderKosto")).Text = rm.GetString("labelRaportDeri", ci);

            //----------------------------------------------------------------------------
            //shtohen ne hidden filed perkthimet per filtrat e avancuar te raportet e klient-furnitoreve
            hfgjuha.Add("labelGrupFurnitori3", rm.GetString("labelGrupFurnitori3", ci));
            hfgjuha.Add("labelGrupFurnitori2", rm.GetString("labelGrupFurnitori2", ci));
            hfgjuha.Add("labelGrupFurnitori1", rm.GetString("labelGrupFurnitori1", ci));
            hfgjuha.Add("labelGrupKlienti3", rm.GetString("labelGrupKlienti3", ci));
            hfgjuha.Add("labelGrupKlenti2", rm.GetString("labelGrupKlenti2", ci));
            hfgjuha.Add("labelGrupKlienti1", rm.GetString("labelGrupKlienti1", ci));
            hfgjuha.Add("labelGrupKlientFurnitor3", rm.GetString("labelGrupKlientFurnitor3", ci));
            hfgjuha.Add("labelGrupKlientFurnitor2", rm.GetString("labelGrupKlientFurnitor2", ci));
            hfgjuha.Add("labelGrupKlientFurnitor1", rm.GetString("labelGrupKlientFurnitor1", ci));

        }

        /// <summary>
        /// vendos combon e stilit ne raport enabled ose jo ne varesi te id se raportit qe do shfaqet
        /// </summary>
        private void EnableStilRaporti()
        {

            switch (idRaporti)
            {
                case 39:
                case 61:
                case 64:
                case 117:
                case 40:
                case 62:
                case 63:
                case 72:
                case 73:
                case 74:
                case 76:
                    ImbReportToolbar1.EnableStil = false;
                    break;

                default:
                    ImbReportToolbar1.EnableStil = true;
                    break;
            }
        }
    }
}