using DbCore.DbAdmin;
using DbCore.DbAsete;
using DbCore.DbShare;
using DevExpress.Web;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using DbCore.DbInventari;
using DbCore;
using DbCore.Raporte;
using DbCore.DbListPagesat;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils.Pages;
using CacheLayer;
using DbCore.IMBUtils.Messages;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using System.Threading;
using DbCore.IMBUtils;
using PlatinumWeb.ApplicationUtils;
using Web.Framework.WebUtils.Pages;
using DevExpress.XtraPrinting.Caching;
using System.Text;
using LiquidEngine.Tools;
using Newtonsoft.Json;
using DbCore.IMBUtils.Fiskalizimi.Controls;

namespace PlatinumWeb
{
    public partial class Raporti : MyReportPageBase
    {
        private string STR_OnConsigment = " ",
        STR_SalesOnCredit = " ",
        STR_Gjithe = " ",
        STR_TeGjitha = "",
        STR_TeEkzekutuara = "",
        STR_TePaekzekutuara = "",
        STR_ArtBurimGjitha = " ",
        STR_KliTeGjithe = " ",
        STR_LlojPorosieUPP = " ",
        STR_LlojPorosieJOUPP = " ",
        STR_LlojKrahasimKostoUPP = " ",
        STR_LlojKrahasimKostoJOUPP = " ",
        STR_LlojCmimiMeTVSH = " ",
        STR_LlojCmimiPaTVSH = " ",
        STR_LlojStatusRezervimiUPP = " ",
        STR_LlojStatusRezervimiJOUPP = " ",
        STR_Po = " ",
        STR_Jo = " ",
        Detajim1 = " ",
        Detajim2 = " ",
        STR_Pjeserisht = " ",
        STR_Artikull = " ",
        STR_Burim = " ",
        STR_GjendjeZero = " ",
        STR_GjendjeJoZero = " ",
        STR_Teprica = " ",
        STR_Mungesa = " ",
        STR_Teprice_Mungese = "",
        STR_MeVeprime = " ",
        STR_GjitheDok = " ",
        STR_DokBrendaAfat = " ",
        STR_DokJashteAfat = " ",
        STR_Ruajtur = " ",
        STR_Draft = " ",
        STR_KonvertimiDytePerfunduar = " ",
        STR_KonvertimiDyteJoKonvertuar = " ",
        STR_KonvertimiDytePjeserisht = " ",
        STR_KonvertimiDyteTejkaluar = " ",
        STR_Emertim1 = " ",
        STR_Emertim2 = " ",
        STR_Emertim3 = " ",
        STR_Emertim4 = " ",
        STR_Emertim5 = " ";

        private string RaportiEmerReal = String.Empty;
        private Int32 idRaportiModul = -1;
        private static string styleNamePrefix = "Style_";
        private static string styleNameDefault = "Default";
        private static string styleNamePostfix = ".repss";
        private string guidString;

        protected void Page_Load(object sender, EventArgs e)
        {
            HfState = hfState;
            if (!Page.IsPostBack)
            {
                try
                {
                    if (IdRaporti < 0 && Convert.ToInt32(Request.QueryString["idraporti"]) != null)
                        IdRaporti = DbCore.clsFunksione.ktheIdRaporti(Request);

                    clsPerdorues perdoruesi = DbCore.mySessionObjects.kthePerdorues(Session);

                    pathStyle.Value = StylePath;
                    if (!DbCore.mySessionObjects.isLogedIn(Session))
                    {
                        DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
                    }

                    clsPeriudhaKontabel periudhaKontabel = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
                    ReportObject = new clsRaporti();
                    if (!String.IsNullOrEmpty(Request.QueryString["emriReal"]))
                        ReportObject = new clsRaporti(IdGjuha, Request.QueryString["emriReal"]);
                    else
                        ReportObject = new clsRaporti(IdGjuha, IdRaporti);
                    RaportiEmerReal = ReportObject.RaportiEmriReal;
                    idRaportiModul = ReportObject.IdModul;
                    bool kaSubRaport = clsRaporti.KaSubRaporte(ReportObject.IdRaporti);
                    var dizajnet = new colRaporteDesign(IdNdermarrja, ReportObject.IdRaporti);
                    var rapdes = dizajnet.MerrDizajnTeZgjedhur();

                    guidString = Guid.NewGuid().ToString();
                    hfState.Set("arsyeReload", string.Empty);
                    hfState.Set("guidString", guidString);
                    hfState.Set("RaportiEmerReal", RaportiEmerReal);
                    hfState.Set("idRaportiModul", idRaportiModul);
                    hfState.Set("RaportiDesign", rapdes.Pershkrim);
                    hfState.Set("oldViewer", false);
                    hfState.Set("reportPageCount", 0);

                    hfState.Set("_idViti", Convert.ToString(DbCore.mySessionObjects.ktheVitiNdermarrjes(Session)));
                    DbCore.DbAdmin.clsKomponente oKomponente = new DbCore.DbAdmin.clsKomponente("Raporti.aspx");
                    DbCore.DbAdmin.clsLogu logu = new DbCore.DbAdmin.clsLogu(0, oKomponente.IdKomponente, IdNdermarrja, IdPerdoruesi, DateTime.Now, "0", ReportObject.IdRaporti, DbCore.mySessionObjects.merrRuajLog(Session));
                    ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                    vendosHfMePerkthime(rm);
                    hfgjuha.Add("idGjuha", IdGjuha);
                    hfeshtememe.Add("eshteMeme", Meme);
                    //vjen nga URL me faqen paraardhese
                    percaktoTemplateMenu(ASPxMenuToolBar, IdViti, IdPerdoruesi, IdNdermarrja);
                    konfiguroVleraFillestare(IdPerdoruesi, IdNdermarrja, IdGjuha);
                    EmrateLabelave();
                    merrKontrolleFiltra(ReportObject.IdRaporti, IdNdermarrja, IdPerdoruesi, periudhaKontabel);
                    EnableStyle = ReportObject.StilEnabled;
                    ReportStyle = perdoruesi.StilRaportiFileName;
                    SetReportStyles();
                    ShfaqButonEksportoVeprimtariDitore = RaportiEmerReal == "veprimtariaDitore";
                    ShfaqButonEksportoBirthdayCard = RaportiEmerReal == "birthdayCard";
                    if (ReportObject.ExportPerTatime)
                    {
                        ShfaqButonEksportoPerTatime = true;
                        if (RaportiEmerReal == "teArdhuraShpenzimeQendraKosto")
                            hfState.Set("exportTatimeTooltip", "Eksporto sipas qendrave te kostos");
                    }

                    clsTeDrejtaRaporte teDrejtaRap = new clsTeDrejtaRaporte();
                    teDrejtaRap.merrTeDrejtaPerRaportPerPerdorues(RaportiEmerReal, IdPerdoruesi, IdNdermarrja, IdViti);
                    hfTeDrejtaRaporti.Set("dGjitheDok", Convert.ToInt32(teDrejtaRap.DGjitheDok));
                    DbCore.DbAdmin.colRolPerdorues rolPerd = new DbCore.DbAdmin.colRolPerdorues();
                    rolPerd.mbushRolePerdoruesSipasPerdoruesi(IdPerdoruesi);
                    DbCore.DbAdmin.clsRoli rol = new DbCore.DbAdmin.clsRoli(rolPerd[0].IdRoli);
                    hfPerdorues.Set("Roli", rol.KodRoli);
                    hfPerdorues.Set("Perdoruesi", perdoruesi.PerdoruesUsername);

                    if (mbushComboBoxFiltra(IdPerdoruesi, IdNdermarrja))
                    {
                        int idFiltri = DbCore.clsFunksione.merrIDFiltriPersonalizuar(Request);
                        DevExpress.Web.MenuItem itemButton = ASPxMenuToolBar.Items.FindByName("TemplatedItemFilter");
                        ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                        ASPxButton btnFshiFilter = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFshi") as ASPxButton;
                        btnFshiFilter.ClientEnabled = true;
                        NgarkoFiltraTePersonalizuar(idFiltri);

                    }
                    else
                    {
                        rregulloFiltraDateDokumenti();
                    }
                    vendosVleraDefaultPerDateDokumentiPerRaporteTeVecante();

                    if (!ReportFunctions.EshteNdertuarDisajniIRaportit(IdNdermarrja, ReportObject.IdRaporti, ReportOrientation))
                    {
                        krijoParametratPerSql(ReportObject.IdSp, IdNdermarrja, periudhaKontabel, IdNdermarrjeVit, IdPerdoruesi, guidString);
                        krijoParametratPerTuShfaqurNeRaport(ReportObject.IdSp, IdNdermarrja, periudhaKontabel, IdNdermarrjeVit, IdPerdoruesi, guidString, kaSubRaport);
                        clsRaportDesign design = new colRaporteDesign(IdNdermarrja, ReportObject.IdRaporti).MerrDizajnTeZgjedhur();
                        ReportOrientation = string.IsNullOrEmpty(design.FileName) ? clsRaportDesign._rap_portrait : clsRaportDesign._rap_landscape;
                        hfState.Set("RaportIPaImplementuar", true);
                        hfState.Set("IdRaportDesign", design.IdRaportDesign);
                        hfState.Set("Orientimi", ReportOrientation);
                        hfState.Set("SpEmri", clsSP.GetStoredProcedureName(ReportObject.IdSp));
                        mySessionObjects.RuajEmerRaporti(HttpContext.Current.Session, design.GetReportName(ReportOrientation), guidString);
                    }
                    if (!DbCore.clsFunksione.ktheFiltroQueryString(Request) )

                        if (!Convert.ToBoolean(Request.QueryString["VjenNgaSubraporti"]))
                            afisho(ReportObject.IdRaporti, IdNdermarrja, IdViti, IdNdermarrjeVit, periudhaKontabel, IdPerdoruesi, kaSubRaport, guidString);
                       else {
                            SetDesigns(out clsRaportDesign rapDes);
                            int idModulSubRaporti = (new clsRaporti(IdGjuha, rapDes.IdRaporti)).IdModul;
                            hfState.Set("idModulSubRaporti", idModulSubRaporti);
                            afishoSubRaport( ReportObject.IdRaporti, Request.QueryString["guidString"], IdNdermarrjeVit);
                         }   

                    else if (kaSubRaport) //nqs raporti ka subraport, atehere parametrat e subraportit duhen krijuar qe ne fillim kur hapet faqja.
                    {
                        konfigurimeSubRaporti(IdNdermarrja, periudhaKontabel, IdNdermarrjeVit, IdPerdoruesi, guidString);
                    }
                }

                catch (DbCore.MyException m)
                {
                    ImbLogger.Error(m);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, m.Message, pnlMesazhi);
                }
                catch (Exception m)
                {
                    ImbLogger.Error(m);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, m.Message, pnlMesazhi);
                }
            }
            else
            {
                guidString = hfState["guidString"].ToString();
                RaportiEmerReal = hfState["RaportiEmerReal"].ToString();
                idRaportiModul = Convert.ToInt32(hfState["idRaportiModul"]);

                if (Convert.ToBoolean(hfState.Get("oldViewer")))
                    reportViewer2.Report = GetReport();
            }
        }

        private void vendosVleraRadioKontrolleraveNgaSuperRaporti(string VleraRadiokontrolli)
        {
            switch (VleraRadiokontrolli)
            {
                case "filterNumerLlogarie":
                    (navBarFiltrat.Groups[1].FindControl("txtBtnNrLlog1") as ASPxButtonEdit).Text = VleraRadiokontrolli;
                    break;
            }
        }
       private void VendosFiltraPerSubRaportQeVijneNgaUrl( string parametri, string vlera)
       {
            switch(parametri)
            {
                case "filterNumerLlogarie":
                    (navBarFiltrat.Groups[1].FindControl("txtBtnNrLlog1") as ASPxButtonEdit).Text = vlera;
                    break;
            }
       }

        private void VendosFiltratQeVijneNgaSuperRaporti(string parametriSubRaporti, string vleraSubRaporti, int IdNdermarrjeVit)
        {
            clsPeriudhaKontabel periudhaKontabel = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            DateTime  dtmbarimiNdermarrje = new clsNdermarrjeViti(IdNdermarrjeVit).NdermarrjeVitiFund;
            DateTime dtFillimiNdermarrje = new clsNdermarrjeViti(IdNdermarrjeVit).NdermarrjeVitiFillim;

            clsNdermarrjeViti ndermviti = new clsNdermarrjeViti();
            ndermviti.mbushNdermVitFillimFundPerGjitheVitetSipasNdermarjes(DbCore.mySessionObjects.ktheVitiNdermarrjes(Session));
            DateTime dtmbarimiGjithVitet = ndermviti.NdermarrjeVitiFund;
            DateTime dtFillimiGjithVitet = ndermviti.NdermarrjeVitiFillim;

            switch (parametriSubRaporti)
            {
                case "filterNumerDokumenti":
                    (navBarFiltrat.Groups[1].FindControl("txtNrDok1") as ASPxTextBox).Text = vleraSubRaporti; 
                    (navBarFiltrat.Groups[1].FindControl("txtNrDok2") as ASPxTextBox).Text = vleraSubRaporti; 
                    break;
                   case "filterDtDok":
                    DateTime DtNga = Convert.ToDateTime(vleraSubRaporti.Substring(0, vleraSubRaporti.IndexOf("-")));
                    DateTime DtDeri = Convert.ToDateTime(vleraSubRaporti.Substring(vleraSubRaporti.IndexOf("-") + 1));
                    (navBarFiltrat.Groups[0].FindControl("txtNgaDok") as ASPxDateEdit).Value = DtNga;
                    (navBarFiltrat.Groups[0].FindControl("txtDeriDok") as ASPxDateEdit).Value = DtDeri;
                    if (DtNga == periudhaKontabel.FillimiPeriudha && DtDeri == periudhaKontabel.MbarimiPeriudha)
                        ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDok")).SelectedIndex = 0;
                    else if (DtNga == dtFillimiGjithVitet && DtDeri == dtmbarimiGjithVitet)
                        ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDok")).SelectedIndex = 3;                 
                    else if (DtNga == dtFillimiNdermarrje && DtDeri == dtmbarimiNdermarrje)
                        ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDok")).SelectedIndex = 2;
                    else
                    ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDok")).SelectedIndex = 1;
                    break;
                case "filterDtRegj":
                    DateTime DtNgaRegj = Convert.ToDateTime(vleraSubRaporti.Substring(0, vleraSubRaporti.IndexOf("-")));
                    DateTime DtDeriRegj = Convert.ToDateTime(vleraSubRaporti.Substring(vleraSubRaporti.IndexOf("-") + 1));
                    (navBarFiltrat.Groups[1].FindControl("txtNgaRegj") as ASPxDateEdit).Value = DtNgaRegj;
                    (navBarFiltrat.Groups[1].FindControl("txtDeriRegj") as ASPxDateEdit).Value = DtDeriRegj;
                    if (DtNgaRegj == periudhaKontabel.FillimiPeriudha && DtDeriRegj == periudhaKontabel.MbarimiPeriudha)
                        ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtRegj")).SelectedIndex = 0;
                    else if (DtNgaRegj == dtFillimiNdermarrje && DtDeriRegj == dtmbarimiNdermarrje)
                        ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtRegj")).SelectedIndex = 2;
                    else
                        ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtRegj")).SelectedIndex = 1;
                    break;
                case "filterNrReference":
                    (navBarFiltrat.Groups[1].FindControl("txtRef1") as ASPxTextBox).Text = vleraSubRaporti;
                    (navBarFiltrat.Groups[1].FindControl("txtRef2") as ASPxTextBox).Text = vleraSubRaporti;
                    break;
                case "filterPershkrimi":
                    (navBarFiltrat.Groups[1].FindControl("txtPershk1") as ASPxTextBox).Text = vleraSubRaporti;
                    (navBarFiltrat.Groups[1].FindControl("txtPershk2") as ASPxTextBox).Text = vleraSubRaporti;
                    break;
                case "filterLlojDokumenti":
                    (navBarFiltrat.Groups[1].FindControl("txtBtnLlojDok1") as ASPxButtonEdit).Text = vleraSubRaporti;
                    (navBarFiltrat.Groups[1].FindControl("txtBtnLlojDok2") as ASPxButtonEdit).Text = vleraSubRaporti;
                    break;
                case "filterKategoriShpenzimi":
                    (navBarFiltrat.Groups[1].FindControl("btnKatShpenzimi1") as ASPxButtonEdit).Text = vleraSubRaporti;
                    break;
                case "filterEmertimLlog":
                    if(vleraSubRaporti == "0")
                    (navBarFiltrat.Groups[1].FindControl("cmbEmertimLlog") as ASPxComboBox).Text = rm.GetString("cmbboxItemEmertim", ci) + " 1";
                    else
                    (navBarFiltrat.Groups[1].FindControl("cmbEmertimLlog") as ASPxComboBox).Text = rm.GetString("cmbboxItemEmertim", ci) + " 2";
                    break;
                case "Azhornim":
                    if (vleraSubRaporti == "Po" || vleraSubRaporti == "Yes")
                        (navBarFiltrat.Groups[1].FindControl("cbAzhornim") as ASPxCheckBox).CheckState = CheckState.Checked;
                    break;
                case "filterMeMbylljeViti":
                    if(vleraSubRaporti == "Po" || vleraSubRaporti == "Yes")                 
                    (navBarFiltrat.Groups[1].FindControl("cbMbylljeViti") as ASPxCheckBox).CheckState = CheckState.Checked;
                    break;
            }
        }

        private List<Dictionary<string, string>> MerrTeDhenaRapNgaSesioni(bool changedDesign, int idDesign, string raportiEmerReal)
        {
            return clsFunksione.merrTeDhenaRaportiPerHapjeRaportiTeShpejte(raportiEmerReal, changedDesign, Request.QueryString["idDokumenti"], idDesign, Session);
        }

        protected void afishoSubRaport( int idRaporti, String guidString, int IdNdermarrjeVit)
        {
            SetDesigns(out clsRaportDesign rapDes);
            List<Dictionary<string, string>> teDhenaRap = MerrTeDhenaRapNgaSesioni(false, rapDes.IdRaportDesign, RaportiEmerReal);

            XtraReport report = new XtraReport();
            string orientim = Request.QueryString["Orientimi"];
            bool azhornim = false;
            int idVit = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            if (!string.IsNullOrEmpty(orientim))
                ReportOrientation = orientim;
        
            clsRaporti oRap = null;
            if (Request.QueryString["Sesioni"] == "true")// hapja e subraporteve
            {
                KontrolloTeDrejtaRaporti(idRaporti, IdPerdoruesi, IdNdermarrja, IdViti);
                report = krijoSubRaport(idRaporti, rapDes.IdRaportDesign, guidString, IdNdermarrjeVit);
            }
         
            if (idRaporti >= 0)
                hapRaportFaturashOseSubRaport(report, idRaporti, guidString, teDhenaRap);
        }

        private XtraReport krijoSubRaport(int idRaporti, int idDesign, string guidString, int IdNdermarrjeVit)
        {
            XtraReport report = new XtraReport();
            string emriReal = Request.QueryString["emriReal"];
            int idSubRaporti = Convert.ToInt32(Request.QueryString["idraporti"]);
            if (idSubRaporti == 0)
                idSubRaporti = clsRaporti.KtheIdRaportiSipasEmritReal(emriReal);
            clsPeriudhaKontabel periudhaKontabel = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            colParameter sqlParamShfaqRaport = mySessionObjects.merrParametratShfaqSubRaportitNgaSesioni(Session, idSubRaporti, guidString);
            for (int i = 0; i < sqlParamShfaqRaport.Count; i++)
            {
                if (Request.QueryString[sqlParamShfaqRaport[i].Emri] != null )
                {
                    string parametri = sqlParamShfaqRaport[i].Emri;
                    var vlera = Convert.ToString(Request.QueryString[parametri]);
                    VendosFiltraPerSubRaportQeVijneNgaUrl(parametri, vlera);
                    sqlParamShfaqRaport[i].Vlera = vlera;
                }

                VendosFiltratQeVijneNgaSuperRaporti(sqlParamShfaqRaport[i].Emri, sqlParamShfaqRaport[i].Vlera, IdNdermarrjeVit);
            }
            return ReportFunctions.krijoObjektRaporti("", IdGjuha, IdPerdoruesi, IdViti, idRaporti, IdNdermarrja, sqlParamShfaqRaport, idDesign, ReportOrientation, guidString, Request.QueryString[ScopeManager.ScopeIdKey]);
        }
      
        private void hapRaportFaturashOseSubRaport(XtraReport report, int idRaporti, string guidString, List<Dictionary<string, string>> teDhenaRap)
        {
            shtoFaturaOseSubRaportNeRaport(idRaporti, report, guidString, teDhenaRap);
            report.StyleSheet.LoadFromFile(NdertoPathStyleSheet(StylePath, ReportStyle));
            report.PrintingSystem.ContinuousPageNumbering = false;

            if (teDhenaRap.Count <= 1)
            {
                hfState.Set("isMultiDesignReport", false);
                bool RSU = colRolPerdorues.merrRoleSipasPerdoruesiDheKodRoli(mySessionObjects.ktheIdPerdoruesi(Session), "RSU") == -1;

                if (((System.Data.DataSet)report.DataSource).Tables[0].Rows.Count < 10000 || RSU)
                    report.CreateDocument();

                if (RSU)
                    DbCore.mySessionObjects.ruajReportNeSession(Session, guidString, report);
            }
            else
                hfState.Set("isMultiDesignReport", true);

            CachedReportSourceWeb cachedReport = new CachedReportSourceWeb(report);
            reportViewer.OpenReport(cachedReport);
        }


        private void shtoFaturaOseSubRaportNeRaport(int idRaporti, XtraReport report, string guidString, List<Dictionary<string, string>> teDhenaRap)
        {
            var azhornim = false;
            if (report.Extensions["filtrat"] != null)
            {
                var parameters = JsonConvert.DeserializeObject<colParameter>(report.Extensions["filtrat"]);
                var paramAzh = parameters?.Find(x => x.Emri == "Azhornim");
                if (paramAzh != null && (paramAzh.Vlera == "Po" || paramAzh.Vlera == "Yes"))
                    azhornim = true;
            }

            foreach (var item in teDhenaRap)
            {
                var idDesignRap = string.IsNullOrEmpty(item["IdDesign"]) ? 0 : int.Parse(item["IdDesign"]);
                mySessionObjects.hiqObjectNeSesion(Session, $"clsReportRradhes_{idDesignRap}");
            }

            var dtmbarimi = DateTime.Today;
            var wfStatus = Request.QueryString["wf"] != null ? Convert.ToInt16(Request.QueryString["wf"]) : -1;
            var test = Request.QueryString["vjen"] == "testim";
            var idPeriudhaKontabel = clsPeriudhaKontabel.ktheIdPeriudheSipasDatesDheNdermarrjes(dtmbarimi, IdNdermarrja);
            var perdoruesi = new clsPerdorues(IdPerdoruesi);
            var idKonfigAmbjentiNkm = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("NKM", IdNdermarrja);
            //var oRap = new clsRaporti(IdGjuha, idRaporti);

            foreach (var item in teDhenaRap)
            {
                int.TryParse(item["IdKoka"], out var id);
                int idDesignRap = string.IsNullOrEmpty(item["IdDesign"]) ? 0 : int.Parse(item["IdDesign"]);

                if (teDhenaRap.Count <= 1)
                {
                    var oRap = new clsRaporti(IdGjuha, idRaporti);
                    oRap.MbushParametra();
                    report = ReportFunctions.mbushRaportNgaDB(Request, Session, report, IdNdermarrja, IdPerdoruesi, azhornim,
                        IdNdermarrjeVit, dtmbarimi, idKonfigAmbjentiNkm, idPeriudhaKontabel, guidString, id,
                        idDesignRap, test, this.RaportiEmerReal, oRap);
                    KonfigFleteRaporti(report, perdoruesi);
                    return;

                }
            }
        }


        private void exportPerTatimet(int idNdermarrje, clsPerdorues perdoruesi, int idVit, int idNderVit, clsPeriudhaKontabel periudhaKontabel)
        {
            if (RaportiEmerReal == "teArdhuraShpenzimeQendraKosto")
            {
                exportRaportTeArdhuraQendraKosto(hfState["guidString"].ToString(), idNdermarrje, idNderVit, perdoruesi);
                return;
            }

            var dt = ((System.Data.DataSet)(GetReport().DataSource)).Tables[0];
            if(dt.Columns.Contains("NDERMARJEPERSHK"))
                dt.Columns.Remove("NDERMARJEPERSHK");

            var rapDes = new clsRaportDesign(IdReportDesign);
            string DesignPath = String.IsNullOrEmpty(rapDes.FileName) ? rapDes.FileNamePortrait : rapDes.FileName;
            var dtFillimi = ktheVlereDateLibrat(idNdermarrje, idVit, idNderVit, periudhaKontabel);
            DbCore.clsFunksione.eksportoLibrin(RaportiEmerReal, DesignPath, dtFillimi, dt, idNdermarrje, Session, Request, HttpContext.Current.Response);
            Container.Attributes["src"] = "";
        }

        private void exportRaportBirthdayCard()
        {
            XtraReport raporti = GetReport();
            XtraReport repPDF = new XtraReport();
            MemoryStream stream = new MemoryStream();
            ZipOutputStream zipStream = new ZipOutputStream(stream);
            zipStream.SetLevel(3);
            string fileNameZip = "Birthday cards.zip";
            DataRow[] rows = ((System.Data.DataSet)(raporti.DataSource)).Tables[0].Select();
            String[] fileNamePdf = Array.ConvertAll(rows, row => row["fileNamePdf"].ToString());
            raporti.CreateDocument();
            for (int i = 0; i < raporti.Pages.Count; i++)
            {
                repPDF.Pages.Add(raporti.Pages[i]);
                object[] params1 = DbCore.mySessionObjects.merrParametratERaportitNgaSesioni(Session, guidString);
                MemoryStream inStream = new MemoryStream();
                repPDF.ExportToPdf(inStream);
                inStream.Position = 0;
                repPDF.Pages.Clear();
                var newEntry = new ZipEntry("Birthday wish " + fileNamePdf[i] + ".pdf");
                newEntry.DateTime = DateTime.Now;
                zipStream.PutNextEntry(newEntry);
                ICSharpCode.SharpZipLib.Core.StreamUtils.Copy(inStream, zipStream, inStream.GetBuffer());
                inStream.Close();
                zipStream.CloseEntry();
            }
            zipStream.IsStreamOwner = false;    // False stops the Close also Closing the underlying stream.
            zipStream.Close();          // Must finish the ZipOutputStream before using outputMemStream.
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Accept-Header", stream.Length.ToString());
            Response.AddHeader("Content-Disposition", ("Attachment") + "; filename=" + fileNameZip);
            Response.AddHeader("Content-Length", stream.Length.ToString());
            Response.BinaryWrite(stream.ToArray());
            Response.End();
        }

        private void exportVeprimtariaDitore()
        {
            string pathDir = HttpContext.Current.Server.MapPath(null) + @"\AlphaWebExport\";
            VeprimtariaDitoreExporter exporter = new VeprimtariaDitoreExporter(((DataSet)GetReport().DataSource).Tables[0], pathDir, mySessionObjects.merrIdNdermarrjeSesioni(Session), rm, ci);
            clsMesazh mesazh = exporter.Eksporto();
            clsMenuInfo.ShtoMesazh(MenuInfo, mesazh, pnlMesazhi);
            DbCore.mySessionObjects.shtoMesazhNeSession(Session, mesazh, guidString);
        }
        private void exportRaportTeArdhuraQendraKosto(string guidString, int idNdermarrje, int idNderVit, clsPerdorues perdoruesi)
        {
            try
            {
                string pathDir = HttpContext.Current.Server.MapPath(null) + @"\AlphaWebExport\";
                DirectoryExtension.CreateDirIfNotExists(pathDir);
                string username = DbCore.mySessionObjects.ktheEmerPerdorues(Session);
                string filePathToWrite = pathDir + "QK" + "_" + username + "_" + DateTime.Now.ToString("ddMMyyyyHHmm");
                XtraReport raporti = GetReport();
                DevExpress.XtraReports.UI.Band band = raporti.Bands.GetBandByType(typeof(DevExpress.XtraReports.UI.ReportHeaderBand));
                DevExpress.XtraReports.UI.Band bandFooter = raporti.Bands.GetBandByType(typeof(DevExpress.XtraReports.UI.PageFooterBand));
                if (band != null)
                {
                    raporti.Bands.Remove(band);
                    raporti.Bands.Remove(bandFooter);
                    //raporti.CreateDocument();
                }
                DevExpress.XtraPrinting.XlsxExportOptions o = new DevExpress.XtraPrinting.XlsxExportOptions();
                o.SheetName = "Totali";
                o.RawDataMode = true;
                raporti.ExportOptions.Xlsx.RawDataMode = true;
                raporti.ExportToXlsx(filePathToWrite + ".xlsx", o);

                object[] params1 = DbCore.mySessionObjects.merrParametratERaportitNgaSesioni(Session, guidString);
                DbCore.DbQendraKosto.colQendraKosto colqendra = new DbCore.DbQendraKosto.colQendraKosto();
                colqendra.mbushGjitheQendraKostoBijSipasNdermarjesAktiv(idNdermarrje);
                for (int q = 0; q < colqendra.Count; q++)
                {
                    SqlParameter[] pars = (System.Data.SqlClient.SqlParameter[])params1[4];
                    SqlParameter paramQK = Array.Find(pars, x => x.ParameterName == "filterQK");
                    paramQK.Value = String.Format(" = ('{0}') ", colqendra[q].Kodi);
                    ReportFunctions.konfigDataSetRaporti(raporti, raporti.DataMember, perdoruesi.IdPerdorues, (bool)params1[0], idNdermarrje, idNderVit, (DateTime)params1[1], (int)params1[2], (int)params1[3], pars);
                    KonfigFleteRaporti(raporti, perdoruesi, false);
                    o.SheetName = colqendra[q].Kodi + "(" + colqendra[q].Pershkrimi + ")";
                    o.RawDataMode = true;
                    raporti.ExportOptions.Xlsx.RawDataMode = true;
                    raporti.ExportToXlsx(filePathToWrite + q + ".xlsx", o);
                }
                DbCore.clsFunksione.shtoKoloneQKTeRaportiExcelWorkbook(filePathToWrite + ".xlsx", Response, perdoruesi.ShfaqDtPrintimi);

            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex.Message);
            }
        }

        private void vendosHfMePerkthime(ResourceManager rm)
        {
            base.SetReportToolbarTranslations();
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
            hfState.Set("msgZgjidhKartenKlientit", rm.GetString("msgZgjidhKartenKlientit", ci));
            hfState.Set("msgRaportiDuhetTeFshiniNdaresinNgaFushaEKodit", rm.GetString("msgRaportiDuhetTeFshiniNdaresinNgaFushaEKodit", ci));
            hfState.Set("msgZgjidhKF", rm.GetString("msgZgjidhKF", ci));
            hfState.Set("msgZgjidhniKapitullin", rm.GetString("msgZgjidhniKapitullin", ci));
            hfState.Set("msgZgjidhniTitullin", rm.GetString("msgZgjidhniTitullin", ci));
            hfState.Set("msgZgjidhKategorineEMaturimit", rm.GetString("msgZgjidhKategorineEMaturimit", ci));
            hfState.Set("msgZgjidhPerdoruesin", rm.GetString("msgZgjidhPerdoruesin", ci));
            hfState.Set("msgZgjidhAgjentinEShitjes", rm.GetString("msgZgjidhAgjentinEShitjes", ci));
            hfState.Set("msgZgjidhniNivelinECmimit", rm.GetString("msgZgjidhniNivelinECmimit", ci));
            hfState.Set("msgZgjidhKrijuesin", rm.GetString("msgZgjidhKrijuesin", ci));
            hfState.Set("msgZgjidhQendrenEKostos", rm.GetString("msgZgjidhQendrenEKostos", ci));
            hfState.Set("headerPopUpZgjidhFurnitorin", rm.GetString("headerPopUpZgjidhFurnitorin", ci));
            hfState.Set("roundPanelZgjidhArtikullin", rm.GetString("roundPanelZgjidhArtikullin", ci));
            hfState.Set("roundPanelZgjidhNjesiProdhimi", rm.GetString("roundPanelZgjidhNjesiProdhimi", ci));
            hfState.Set("roundPanelZgjidhModelin", rm.GetString("roundPanelZgjidhModelin", ci));
            hfState.Set("roundPanelZgjidhObjektinGis", rm.GetString("roundPanelZgjidhObjektinGis", ci));
            hfState.Set("msgZgjidhKodbar", rm.GetString("msgZgjidhKodbar", ci));
            hfState.Set("msgZgjidhDetajimin", rm.GetString("msgZgjidhDetajimin", ci));
            hfState.Set("msgZgjidhPikeShitjeFurnizimi", rm.GetString("msgZgjidhPikeShitjeFurnizimi", ci));
            hfState.Set("msgZgjidhDegeAdministrative", rm.GetString("msgZgjidhDegeAdministrative", ci));
            hfState.Set("msgZgjidhGrupin1TeArtikullit", rm.GetString("msgZgjidhGrupin1TeArtikullit", ci));
            hfState.Set("msgZgjidhGrupin2TeArtikullit", rm.GetString("msgZgjidhGrupin2TeArtikullit", ci));
            hfState.Set("msgZgjidhGrupin3TeArtikullit", rm.GetString("msgZgjidhGrupin3TeArtikullit", ci));
            hfState.Set("msgzgjidhGrupinArkaOseBanka", rm.GetString("msgzgjidhGrupinArkaOseBanka", ci));
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
            hfState.Set("zgjidhLayerElem", rm.GetString("zgjidhLayerElem", ci));
            hfState.Set("popupKategoriShpenzimi", rm.GetString("popupKategoriShpenzimi", ci));
            hfState.Set("MsgVleraNeIntervaleGabim", rm.GetString("MsgVleraNeIntervaleGabim", ci));
            hfState.Set("msgZgjidhNjesiVartese", rm.GetString("msgZgjidhNjesiVartese", ci));
        }

        protected void ReportViewer2_Unload(object sender, EventArgs e)
        {
            ((ReportViewer)sender).Report = null;
        }

        private int kthekategoridok()
        {
            clsRaporti clsrap;
            if (Request.QueryString["idraporti"] != null)
                clsrap = new clsRaporti(DbCore.mySessionObjects.ktheGjuhe(Session), Convert.ToInt32(Request.QueryString["idraporti"].ToString()));
            else
                clsrap = new clsRaporti(DbCore.mySessionObjects.ktheGjuhe(Session), Request.QueryString["emriReal"].ToString());
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

            switch (idRaportiModul)
            {
                case 12:
                    cmbNivel.SelectedItem = cmbNivel.Items.FindByText("FSH");
                    break;
                case 13:
                    if (RaportiEmerReal == "blerjeAnalitike")
                        cmbNivel.SelectedItem = cmbNivel.Items.FindByText("OB");
                    else
                        cmbNivel.SelectedItem = cmbNivel.Items.FindByText("FB");
                    break;
                default:
                    break;
            }
        }

        private void mbushComboLikuiduar()
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
        private void mbushcomboLlojArtikulli(int idRaport)
        {
            ASPxComboBox cmbLlojArtikulli = new ASPxComboBox();
            string kontrollEmri = "cmbLlojArtikulli";
            cmbLlojArtikulli = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbLlojArtikulli.Items.Add(rm.GetString("cmbboxItemFilterAvancTeGjithe", ci), rm.GetString("cmbboxItemFilterAvancTeGjithe", ci));
            cmbLlojArtikulli.Items.Add(rm.GetString("cmbboxItemFilterAvancAfatshk", ci), rm.GetString("cmbboxItemFilterAvancAfatshk", ci));
            cmbLlojArtikulli.Items.Add(rm.GetString("cmbboxItemFilterAvancAfatgjt", ci), rm.GetString("cmbboxItemFilterAvancAfatgjt", ci));
            if (RaportiEmerReal == "artikujTePashitur")
                cmbLlojArtikulli.SelectedIndex = 1;
            else
                cmbLlojArtikulli.SelectedIndex = 0;
            cmbLlojArtikulli.DataBind();
        }

        private void mbushcomboStatus(int idRaport)
        {
            ASPxComboBox cmbStatus = new ASPxComboBox();
            string kontrollEmri = "cmbStatus";
            cmbStatus = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            if (RaportiEmerReal == "analitikShitje")
            {
                cmbStatus.Items.Add(rm.GetString("cmbGjitha", ci), 0);
                cmbStatus.Items.Add(rm.GetString("cmbJoFshira", ci), 1);
                cmbStatus.Items.Add(rm.GetString("cmbFshira", ci), 2);
            }
            if (RaportiEmerReal == "dokumentaIntegrimi")
            {
                cmbStatus.Items.Add(rm.GetString("cmbGjitha", ci), 0);
                cmbStatus.Items.Add(rm.GetString("cmbPatransferuara", ci), 1);
                cmbStatus.Items.Add(rm.GetString("cmbTransferuara", ci), 2);
            }
            else
            {
                cmbStatus.Items.Add(rm.GetString("cmbStatusiItemTeGjitha", ci), 0);
                cmbStatus.Items.Add(rm.GetString("cmbStatusJoTeFshira", ci), 1);
                cmbStatus.Items.Add(rm.GetString("cmbStatusTeFshira", ci), 2);
            }
            cmbStatus.SelectedIndex = 1;

            cmbStatus.DataBind();
        }

        private void mbushcomboLlojSubjekti()
        {
            ASPxComboBox cmbLlojSubjekti = new ASPxComboBox();
            string kontrollEmri = "cmbLlojSubjekti";
            cmbLlojSubjekti = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbLlojSubjekti.Items.Add("", 0);
            cmbLlojSubjekti.Items.Add(rm.GetString("comboItemBlerjeShitjeKlient", ci), 1);
            cmbLlojSubjekti.Items.Add(rm.GetString("comboItemBlerjeShitjeFurnitor", ci), 2);
            cmbLlojSubjekti.Items.Add(rm.GetString("labelRaportLlogari", ci), 3);
            cmbLlojSubjekti.Items.Add(rm.GetString("comboItemBlerjeShitjePunonjes", ci), 4);
            cmbLlojSubjekti.SelectedIndex = 0;
            cmbLlojSubjekti.DataBind();
        }

        private void mbushcomboLlojSubjektiKF()
        {
            ASPxComboBox cmbLlojSubjektiKF = new ASPxComboBox();
            string kontrollEmri = "cmbLlojSubjektiKF";
            cmbLlojSubjektiKF = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbLlojSubjektiKF.Items.Add("", "");
            cmbLlojSubjektiKF.Items.Add(rm.GetString("comboItemBlerjeShitjeKlient", ci), 1);
            cmbLlojSubjektiKF.Items.Add(rm.GetString("comboItemBlerjeShitjeFurnitor", ci), 0);
            cmbLlojSubjektiKF.SelectedIndex = 0;
            cmbLlojSubjektiKF.DataBind();
        }

        private void mbushComboStatus()
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

        private void mbushComboGrupoSipas()
        {
            ASPxComboBox cmbGrupoSipas = new ASPxComboBox();
            string kontrollEmri = "cmbGrupoSipas";
            cmbGrupoSipas = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbGrupoSipas.Items.Add("", 0);
            cmbGrupoSipas.Items.Add(rm.GetString("cmbGrupoSipasAgjent", ci), 1);
            cmbGrupoSipas.Items.Add(rm.GetString("cmbGrupoSipasGR1KF", ci), 2);
            cmbGrupoSipas.Items.Add(rm.GetString("cmbGrupoSipasGR2KF", ci), 3);
            cmbGrupoSipas.Items.Add(rm.GetString("cmbGrupoSipasGR3KF", ci), 4);
            cmbGrupoSipas.SelectedIndex = 3;
            cmbGrupoSipas.DataBind();
        }

        private void mbushComboGrupoSipasAgjenteve()
        {
            ASPxComboBox cmbGrupoSipasAgjenteve = new ASPxComboBox();
            string kontrollEmri = "cmbGrupoSipasAgjenteve";
            cmbGrupoSipasAgjenteve = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbGrupoSipasAgjenteve.Items.Add(rm.GetString("cmbGrupoSipasAgjent1", ci), 0);
            cmbGrupoSipasAgjenteve.Items.Add(rm.GetString("cmbGrupoSipasAgjent2", ci), 1);
            cmbGrupoSipasAgjenteve.Items.Add(rm.GetString("cmbGrupoSipasAgjent3", ci), 2);
            cmbGrupoSipasAgjenteve.SelectedIndex = 0;
            cmbGrupoSipasAgjenteve.DataBind();
        }
        private void mbushComboVeprimePeriudhe()
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

        private void mbushComboGjendjeDetyrimi()
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
        private void mbushComboStatusCRM()
        {
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
        private void mbushComboLlojiArtBurim()
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
        private void mbushComboKategorizimArtikuj(int idRaport)
        {
            ASPxComboBox cmbKategorizimArtikuj = new ASPxComboBox();
            string kontrollEmri = "cmbKategorizimArtikuj";
            cmbKategorizimArtikuj = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbKategorizimArtikuj.Items.Add(rm.GetString("cmbboxItemFilterAvancTeGjithe", ci), 0);
            cmbKategorizimArtikuj.Items.Add(rm.GetString("lblArtNeProgram", ci), 1);
            cmbKategorizimArtikuj.Items.Add(rm.GetString("lblArtJoNeProgram", ci), 2);
            cmbKategorizimArtikuj.Items.Add(rm.GetString("lblArtJoNeMagazine", ci), 3);
            cmbKategorizimArtikuj.Items.Add(rm.GetString("lblArtPerbashket", ci), 4);
            cmbKategorizimArtikuj.SelectedIndex = 0;
            cmbKategorizimArtikuj.DataBind();
        }
        private void mbushComboShfaqArt(int idRaport)
        {
            ASPxComboBox cmbShfaqArt = new ASPxComboBox();
            string kontrollEmri = "cmbShfaqArt";
            switch (RaportiEmerReal)
            {
                case "gjendjeArtikujshMinMax":
                case "gjendjeArtikujshMinMaxSipasMagazines":

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
                    break;
            }
            cmbShfaqArt.DataBind();
        }

        private void mbushComboGjendjeArt(int idRaport)
        {
            ASPxComboBox cmbGjendjeArt = new ASPxComboBox();
            string kontrollEmri = "cmbGjendjeArt";
            switch (RaportiEmerReal)
            {
                case "situacionKlienti":
                case "situacioniKlienteveMeGrupime":
                case "situacionFurnitori":
                case "situacionFurnitoriMeGrupime":
                case "situacionKlienteshSipasMakinave":
                case "situacionKlienteshAutorizime":
                case "situacionKlienteshMaturime":
                case "situacionPermbledhesKlienteNivelRaportues":
                case "situacionPermbledhesFurnitorNivelRaportues":
                case "situacionKlientiAfateMaturimi":
                case "situacioniMonedheKlienti":
                case "situacioniMonedheFurnitori":
                case "SituacioniIKlienteveSipasMuajve":
                case "situacionKlientiEkspozim":
                case "situacionKlientiEkspozimVjetersi":
                case "vjetersiDetyrimeshKliente":
                    STR_ArtBurimGjitha = rm.GetString("cmbboxItemFilterAvancTeGjithe", ci);
                    STR_GjendjeJoZero = rm.GetString("filterRaportMeGjendje", ci);
                    STR_GjendjeZero = rm.GetString("filterRaportMeGjendje", ci) + " 0";
                    cmbGjendjeArt = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
                    cmbGjendjeArt.Items.Add(STR_GjendjeJoZero, 0);
                    cmbGjendjeArt.Items.Add(STR_GjendjeZero, 1);
                    cmbGjendjeArt.Items.Add(STR_ArtBurimGjitha, 2);
                    cmbGjendjeArt.SelectedIndex = 0;
                    break;

                case "gjendjaMagazine":
                case "gjendjaMagazineSipasAutorizimeve":
                    STR_ArtBurimGjitha = rm.GetString("cmbboxItemFilterAvancTeGjithe", ci);
                    STR_GjendjeJoZero = rm.GetString("labelRaportArtikujMeGjendje", ci);
                    STR_GjendjeZero = rm.GetString("labelRaportArtikujMeGjendjeZero", ci);
                    STR_MeVeprime = rm.GetString("lblGjendjaEMagGjendjaArtMeVeprime", ci);
                    cmbGjendjeArt = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
                    cmbGjendjeArt.Items.Add(STR_ArtBurimGjitha, 0);
                    cmbGjendjeArt.Items.Add(STR_GjendjeJoZero, 1);
                    cmbGjendjeArt.Items.Add(STR_GjendjeZero, 2);
                    cmbGjendjeArt.Items.Add(STR_MeVeprime, 3);
                    cmbGjendjeArt.SelectedIndex = (KlientSpecifik.VodafoneShops.ToString().EqualsIgnoreCase(clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.Klienti))) ? 0 : 1;
                    break;

                case "regjisterAsetesh":
                    STR_ArtBurimGjitha = rm.GetString("cmbboxItemFilterAvancTeGjithe", ci);
                    STR_GjendjeJoZero = rm.GetString("filterItemAseteGjendje", ci);
                    STR_GjendjeZero = rm.GetString("filterItemAseteGjendje", ci) + " 0";
                    STR_MeVeprime = rm.GetString("filterItemAseteVeprime", ci);
                    cmbGjendjeArt = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
                    cmbGjendjeArt.Items.Add(STR_ArtBurimGjitha, 0);
                    cmbGjendjeArt.Items.Add(STR_GjendjeJoZero, 1);
                    cmbGjendjeArt.Items.Add(STR_GjendjeZero, 2);
                    cmbGjendjeArt.Items.Add(STR_MeVeprime, 3);
                    cmbGjendjeArt.SelectedIndex = 3;
                    break;

                case "gjendjeArtikujsh":
                case "gjendjeArtikujshAutorizime":
                case "gjendjeArtikujshAutorizime_Format2":
                case "gjendjaEProdukteveLoan":
                case "gjendjaCmimeShitjeArtikujsh":
                case "gjendjeArtikujTePerbere":
                case "gjendjaMagazinesSeriale":
                case "fleteInventarizimi":
                case "analizeArtikujsh":
                case "gjendjaPermbledhurArtikujBlerjeShitje":
                case "gjendjaPermbledhurArtikujNjesiMatese":
                case "gjendjaMagazinesSipasFurnitoreve":
                case "gjendjaArtikujSasiVlere":
                case "gjendjaMagazines3NjesiMatese":
                case "gjendjeArtikujMagazineVartese":
                case "gjendjaMagazinesEkspozitor":
                case "gjendjaMagazinesNivelRaportues":
                case "gjendjepermbledhurArtikujshNdermarje":
                case "gjendjaCmimeShitjeArtikujshFastech":
                case "gjendjaPermbledhurArtikuj":
                    STR_ArtBurimGjitha = rm.GetString("cmbboxItemFilterAvancTeGjithe", ci);
                    STR_GjendjeJoZero = rm.GetString("labelRaportArtikujMeGjendje", ci);
                    STR_GjendjeZero = rm.GetString("labelRaportArtikujMeGjendjeZero", ci);
                    cmbGjendjeArt = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
                    cmbGjendjeArt.Items.Add(STR_ArtBurimGjitha, 0);
                    cmbGjendjeArt.Items.Add(STR_GjendjeJoZero, 1);
                    cmbGjendjeArt.Items.Add(STR_GjendjeZero, 2);
                    cmbGjendjeArt.SelectedIndex = 1;
                    break;

                case "gjendjaMagazinesVlefte":
                    STR_ArtBurimGjitha = rm.GetString("cmbboxItemFilterAvancTeGjithe", ci);
                    STR_GjendjeJoZero = rm.GetString("labelRaportArtikujMeGjendje", ci);
                    STR_GjendjeZero = rm.GetString("labelRaportArtikujMeGjendjeZero", ci);
                    STR_MeVeprime = rm.GetString("lblGjendjaEMagGjendjaArtMeVeprime", ci);
                    cmbGjendjeArt = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
                    cmbGjendjeArt.Items.Add(STR_ArtBurimGjitha, 0);
                    cmbGjendjeArt.Items.Add(STR_GjendjeJoZero, 1);
                    cmbGjendjeArt.Items.Add(STR_GjendjeZero, 2);
                    cmbGjendjeArt.Items.Add(STR_MeVeprime, 3);
                    cmbGjendjeArt.SelectedIndex = 3;
                    break;

                case "gjendjaMagazineCmimeShitje":
                    STR_ArtBurimGjitha = rm.GetString("cmbboxItemFilterAvancTeGjithe", ci);
                    STR_GjendjeJoZero = rm.GetString("labelRaportArtikujMeGjendje", ci);
                    STR_GjendjeZero = rm.GetString("labelRaportArtikujMeGjendjeZero", ci);
                    cmbGjendjeArt = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
                    cmbGjendjeArt.Items.Add(STR_ArtBurimGjitha, 0);
                    cmbGjendjeArt.Items.Add(STR_GjendjeJoZero, 1);
                    cmbGjendjeArt.Items.Add(STR_GjendjeZero, 2);
                    cmbGjendjeArt.SelectedIndex = 0;
                    break;

                case "gjendjaPermbledhurEArkes":
                case "levizjetelikujditeteve":
                case "gjendjaPermbledhurEBankes":
                case "gjendjaPermbledheseArkeBankeMonedheKerkuar":
                    STR_ArtBurimGjitha = rm.GetString("cmbboxItemFilterAvancTeGjithe", ci);
                    STR_MeVeprime = rm.GetString("cbmGjendjaArkaBankaMeVeprime", ci);
                    cmbGjendjeArt = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
                    cmbGjendjeArt.Items.Add(STR_ArtBurimGjitha, 0);
                    cmbGjendjeArt.Items.Add(STR_MeVeprime, 1);
                    cmbGjendjeArt.SelectedIndex = 1;
                    break;
                case "gjendjeArtikujshMinMax":
                case "gjendjeArtikulliSipasMagazines":
                case "listeArtikujZbritjeAnalitike":
                case "gjendjeArtikujshMinMaxSipasMagazines":
                case "veprimtariaDitore":
                case "gjendjeArtikujtVodafone":
                    STR_ArtBurimGjitha = rm.GetString("cmbboxItemFilterAvancTeGjithe", ci);
                    STR_GjendjeJoZero = rm.GetString("labelRaportArtikujMeGjendje", ci);
                    STR_GjendjeZero = rm.GetString("labelRaportArtikujMeGjendjeZero", ci);
                    cmbGjendjeArt = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
                    cmbGjendjeArt.Items.Add(STR_ArtBurimGjitha, 0);
                    cmbGjendjeArt.Items.Add(STR_GjendjeJoZero, 1);
                    cmbGjendjeArt.Items.Add(STR_GjendjeZero, 2);
                    cmbGjendjeArt.SelectedIndex = 0;
                    break;
                default:
                    STR_ArtBurimGjitha = rm.GetString("cmbboxItemFilterAvancTeGjithe", ci);
                    STR_GjendjeJoZero = rm.GetString("labelRaportArtikujMeGjendje", ci);
                    STR_GjendjeZero = rm.GetString("labelRaportArtikujMeGjendjeZero", ci);
                    //STR_MeVeprime = rm.GetString("lblGjendjaEMagGjendjaArtMeVeprime", ci);
                    cmbGjendjeArt = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
                    cmbGjendjeArt.Items.Add(STR_ArtBurimGjitha, 0);
                    cmbGjendjeArt.Items.Add(STR_GjendjeJoZero, 1);
                    cmbGjendjeArt.Items.Add(STR_GjendjeZero, 2);
                    //cmbGjendjeArt.Items.Add(STR_MeVeprime, 3);
                    cmbGjendjeArt.SelectedIndex = 1;
                    break;
            }
            cmbGjendjeArt.DataBind();
        }

        private void mbushComboLlojPorosie(int idRaport)
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

        private void mbushComboLlojGrupoKlientSipas(int idRaport)
        {
            ASPxComboBox cmbGrupoKlientSipas = new ASPxComboBox();
            string kontrollEmri = "cmbGrupoKlientSipas";

            switch (RaportiEmerReal)
            {
                case "situacionFurnitoriMeGrupime":
                    STR_Emertim1 = rm.GetString("labelFilterAvancuarGrupim1Furnitor", ci);
                    STR_Emertim2 = rm.GetString("labelFilterAvancuarGrupim2Furnitor", ci);
                    STR_Emertim3 = rm.GetString("labelFilterAvancuarGrupim3Furnitor", ci);

                    cmbGrupoKlientSipas = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
                    cmbGrupoKlientSipas.Items.Add(STR_Emertim1, 1);
                    cmbGrupoKlientSipas.Items.Add(STR_Emertim2, 2);
                    cmbGrupoKlientSipas.Items.Add(STR_Emertim3, 3);
                    cmbGrupoKlientSipas.SelectedIndex = 0;
                    break;

                case "ShperndarjaKlienteve":
                    cmbGrupoKlientSipas = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
                    cmbGrupoKlientSipas.Items.Add(MessagesResource.Messages["labelFilterAvancuarGrupim1KF"], 1);
                    cmbGrupoKlientSipas.Items.Add(MessagesResource.Messages["labelFilterAvancuarGrupim2KF"], 2);
                    cmbGrupoKlientSipas.Items.Add(MessagesResource.Messages["labelFilterAvancuarGrupim3KF"], 3);
                    cmbGrupoKlientSipas.SelectedIndex = 0;
                    break;

                default:
                    STR_Emertim1 = rm.GetString("labelFilterAvancuarGrupim1KF", ci);
                    STR_Emertim2 = rm.GetString("labelFilterAvancuarGrupim2KF", ci);
                    STR_Emertim3 = rm.GetString("labelFilterAvancuarGrupim3KF", ci);
                    STR_Emertim4 = rm.GetString("labelQyteti", ci);
                    STR_Emertim5 = rm.GetString("agjentShitjeTab", ci);
                    cmbGrupoKlientSipas = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
                    cmbGrupoKlientSipas.Items.Add(STR_Emertim1, 1);
                    cmbGrupoKlientSipas.Items.Add(STR_Emertim2, 2);
                    cmbGrupoKlientSipas.Items.Add(STR_Emertim3, 3);
                    cmbGrupoKlientSipas.Items.Add(STR_Emertim4, 4);
                    cmbGrupoKlientSipas.Items.Add(STR_Emertim5, 5);
                    cmbGrupoKlientSipas.SelectedIndex = 0;
                    break;
            }
            cmbGrupoKlientSipas.DataBind();
        }


        private void mbushComboGrupoArtikullSipas(int idRaport)
        {
            STR_Emertim1 = rm.GetString("labelFilterGrup1Art", ci);
            STR_Emertim2 = rm.GetString("labelFilterGrup2Art", ci);
            STR_Emertim3 = rm.GetString("labelFilterGrup3Art", ci);
            STR_Emertim4 = rm.GetString("labelFilterFurnitorKryesor", ci);

            ASPxComboBox cmbGrupoArtikullSipas = new ASPxComboBox();
            string kontrollEmri = "cmbGrupoArtikullSipas";
            cmbGrupoArtikullSipas = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            if (RaportiEmerReal == "shitjesipasgrupeartikjvestatistikor")
            {
                cmbGrupoArtikullSipas.Items.Add(STR_Emertim1, 1);
                cmbGrupoArtikullSipas.Items.Add(STR_Emertim2, 2);
                cmbGrupoArtikullSipas.Items.Add(STR_Emertim3, 3);
                cmbGrupoArtikullSipas.SelectedIndex = 1;
            }
            if (RaportiEmerReal == "evidencaShitjeve")
            {
                cmbGrupoArtikullSipas.Items.Add(STR_Emertim1, 1);
                cmbGrupoArtikullSipas.Items.Add(STR_Emertim2, 2);
              
                cmbGrupoArtikullSipas.SelectedIndex = 1;
            }
            if (RaportiEmerReal != "gjendjaemagazinessipasgrupimeveteartikujve" &&  RaportiEmerReal != "shitjesipasgrupeartikjvestatistikor" && RaportiEmerReal!= "evidencaShitjeve")
            {
                cmbGrupoArtikullSipas.Items.Add(" ", 0);
            }
            if(RaportiEmerReal != "shitjesipasgrupeartikjvestatistikor" && RaportiEmerReal!= "evidencaShitjeve")
            { 
            cmbGrupoArtikullSipas.Items.Add(STR_Emertim1, 1);
            cmbGrupoArtikullSipas.Items.Add(STR_Emertim2, 2);
            cmbGrupoArtikullSipas.Items.Add(STR_Emertim3, 3);
            cmbGrupoArtikullSipas.Items.Add(STR_Emertim4, 4);
            }
            if (RaportiEmerReal == "marzhiShitjeveSipasGrupimeArtikujve"  )
            {
                cmbGrupoArtikullSipas.SelectedIndex = 1;
            }
            else
            {
                cmbGrupoArtikullSipas.SelectedIndex = 0;
            }
           
            cmbGrupoArtikullSipas.DataBind();
        }

        private void mbushComboLlojKrahasimKostoje(int idRaport)
        {
            STR_LlojKrahasimKostoUPP = rm.GetString("cmbboxItemFilterLlojKrahasimiUPP", ci);
            STR_LlojKrahasimKostoJOUPP = rm.GetString("cmbboxItemFilterLlojKrahasimiJOUPP", ci);
            ASPxComboBox cmbkrahasimKosto = new ASPxComboBox();
            string kontrollEmri = "cmbkrahasimKosto";
            cmbkrahasimKosto = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbkrahasimKosto.Items.Add(STR_LlojKrahasimKostoUPP, 1);
            cmbkrahasimKosto.Items.Add(STR_LlojKrahasimKostoJOUPP, 2);
            cmbkrahasimKosto.SelectedIndex = 0;
            cmbkrahasimKosto.DataBind();
        }
        private void mbushComboDogana(CultureInfo ci)
        {
            STR_Po = rm.GetString("cmbboxItemFilterAvancPo", ci);
            STR_Jo = rm.GetString("cmbboxItemFilterAvancJo", ci);
            ASPxComboBox cmbdogana = new ASPxComboBox();
            string kontrollemridog = "cmbdogana";
            cmbdogana = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollemridog);
            cmbdogana.Items.Add("", 0);
            cmbdogana.Items.Add(STR_Po, 1);
            cmbdogana.Items.Add(STR_Jo, 2);
            cmbdogana.SelectedIndex = 0;
            cmbdogana.DataBind();
        }

        private void mbushComboKlientAktiv(int idRaport)
        {
            STR_Po = rm.GetString("cmbboxItemFilterAvancPo", ci);
            STR_Jo = rm.GetString("cmbboxItemFilterAvancJo", ci);
            STR_KliTeGjithe = rm.GetString("cmbboxItemFilterAvancTeGjithe", ci);
            ASPxComboBox cmbKlientAktiv = new ASPxComboBox();
            string kontrollEmri = "cmbKlientAktiv";
            cmbKlientAktiv = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbKlientAktiv.Items.Add(STR_KliTeGjithe, 1);
            cmbKlientAktiv.Items.Add(STR_Po, 2);
            cmbKlientAktiv.Items.Add(STR_Jo, 3);
            cmbKlientAktiv.SelectedIndex = 0;
            cmbKlientAktiv.DataBind();
        }

        private void mbushComboKontabilizuar(int idRaport)
        {
            STR_Po = rm.GetString("cmbboxItemFilterAvancPo", ci);
            STR_Jo = rm.GetString("cmbboxItemFilterAvancJo", ci);
            STR_KliTeGjithe = rm.GetString("cmbboxItemFilterAvancTeGjithe", ci);
            ASPxComboBox cmbKontabilizuar = new ASPxComboBox();
            string kontrollEmri = "cmbKontabilizuar";
            cmbKontabilizuar = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbKontabilizuar.Items.Add(STR_KliTeGjithe, 1);
            cmbKontabilizuar.Items.Add(STR_Po, 2);
            cmbKontabilizuar.Items.Add(STR_Jo, 3);
            cmbKontabilizuar.SelectedIndex = 0;
            cmbKontabilizuar.DataBind();
        }
        private void mbushComboKategoriShpenzimiAktive(int idRaport)
        {
            STR_Po = rm.GetString("cmbboxItemFilterAvancPo", ci);
            STR_Jo = rm.GetString("cmbboxItemFilterAvancJo", ci);
            STR_KliTeGjithe = rm.GetString("cmbboxItemFilterAvancTeGjithe", ci);
            ASPxComboBox cmbFilterKatAktive = new ASPxComboBox();
            string kontrollEmri = "cmbFilterKatAktive";
            cmbFilterKatAktive = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbFilterKatAktive.Items.Add(STR_KliTeGjithe, 1);
            cmbFilterKatAktive.Items.Add(STR_Po, 2);
            cmbFilterKatAktive.Items.Add(STR_Jo, 3);
            cmbFilterKatAktive.SelectedIndex = 0;
            cmbFilterKatAktive.DataBind();
        }

        private void mbushComboLlojCmimiMePaTVSH(int idRaport)
        {
            STR_LlojCmimiPaTVSH = rm.GetString("labelRaportCmimiPaTVSH", ci);
            STR_LlojCmimiMeTVSH = rm.GetString("labelRaportCmimiMeTVSH", ci);

            ASPxComboBox cmbCmimMePaTVSH = new ASPxComboBox();
            string kontrollEmri = "cmbCmimMePaTVSH";
            cmbCmimMePaTVSH = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbCmimMePaTVSH.Items.Add(STR_LlojCmimiPaTVSH, 1);
            cmbCmimMePaTVSH.Items.Add(STR_LlojCmimiMeTVSH, 2);
            cmbCmimMePaTVSH.SelectedIndex = 0;
            cmbCmimMePaTVSH.DataBind();
        }
        private void mbushComboLlojStatusRezervimi(int idRaport)
        {
            STR_LlojStatusRezervimiUPP = rm.GetString("cmbNeProces", ci);
            STR_LlojStatusRezervimiJOUPP = rm.GetString("cmbEkzekutuar", ci);

            ASPxComboBox cmbStatusRezervimi = new ASPxComboBox();
            string kontrollEmri = "cmbStatusRezervimi";
            cmbStatusRezervimi = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbStatusRezervimi.Items.Add(" ", 0);
            cmbStatusRezervimi.Items.Add(STR_LlojStatusRezervimiUPP, 1);
            cmbStatusRezervimi.Items.Add(STR_LlojStatusRezervimiJOUPP, 2);
            cmbStatusRezervimi.SelectedIndex = 0;
            cmbStatusRezervimi.DataBind();
        }

        private void mbushComboStatusProdhuar(int idRaport)
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


        private void mbushComboEmertimLlog(int idgjuha)
        {
            STR_Emertim1 = rm.GetString("cmbboxItemEmertim", ci) + " 1";
            STR_Emertim2 = rm.GetString("cmbboxItemEmertim", ci) + " 2";
            ASPxComboBox cmbEmertimLlog = new ASPxComboBox();
            string kontrollEmri = "cmbEmertimLlog";
            cmbEmertimLlog = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbEmertimLlog.Items.Add(STR_Emertim1, 0);
            cmbEmertimLlog.Items.Add(STR_Emertim2, 1);
            cmbEmertimLlog.SelectedIndex = 0;
            cmbEmertimLlog.DataBind();
        }
        private void mbushComboGjendja()
        {

            ASPxComboBox cmbGjendja = new ASPxComboBox();
            string kontrollEmri = "cmbGjendja";
            cmbGjendja = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            if (RaportiEmerReal == "permbledheseRezultatiQendraKosto" || RaportiEmerReal == "RaportiQendraveTeKostosSipasZeraveDheNenzeraveTeDetajuar" || RaportiEmerReal == "RezultatiQendraveTeKostosMeZeraDheNenzera" || RaportiEmerReal == "RezultatiQendraveTeKostosMeZera")
            {
                STR_Emertim1 = rm.GetString("cbmGjendjaArkaBankaMeVeprime", ci);
                STR_Emertim2 = rm.GetString("cmbboxItemFilterAvancTeGjithe", ci);
                cmbGjendja.Items.Add(STR_Emertim1, 0);
                cmbGjendja.Items.Add(STR_Emertim2, 1);
                cmbGjendja.SelectedIndex = 0;
            }
            else if (RaportiEmerReal == "gjendjePermbledhurKartaKlienti")
            {
                cmbGjendja.Items.Add(rm.GetString("cmbboxItemFilterAvancTeGjitha", ci), 0);
                cmbGjendja.Items.Add(rm.GetString("cmbRaportMegjendje", ci), 1);
                cmbGjendja.Items.Add(rm.GetString("cbmGjendjaArkaBankaMeVeprime", ci), 2);
                cmbGjendja.Items.Add(rm.GetString("cmbGjendjaMeGjendjeZero", ci), 3);
                cmbGjendja.SelectedIndex = 2;
            }
            else
            {
                STR_Emertim1 = rm.GetString("cmbboxItemFilterAvancTeGjithe", ci);
                STR_Emertim2 = rm.GetString("cmbboxItemLlogGjendje", ci);
                cmbGjendja.Items.Add(STR_Emertim1, 0);
                cmbGjendja.Items.Add(STR_Emertim2, 1);
                cmbGjendja.SelectedIndex = 0;
            }

            cmbGjendja.DataBind();
        }

        private void mbushGrupDokumenti01(int idgrupi, string emri, int idNdermarrje, int idPerdoruesi)
        {//mbush kombon e monedhes me te dhena nga databasa
            DbCore.DbRegjistrim.colGrupimDokumentiKoka col = new DbCore.DbRegjistrim.colGrupimDokumentiKoka();
            col.Add(new DbCore.DbRegjistrim.clsGrupimDokumentiKoka());
            clsRaporti rap = new clsRaporti(DbCore.mySessionObjects.ktheGjuhe(Session), IdRaporti);
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
            ASPxComboBox cmbGrup01 = new ASPxComboBox();
            string kontrollEmri = emri;
            cmbGrup01 = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);

            cmbGrup01.DataSource = col;
            cmbGrup01.TextField = "Kodi";
            cmbGrup01.ValueField = "IdGrupimKoka";
            cmbGrup01.DataBind();
        }

        private void mbushComboAktiv()
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
        private void mbushComboPaguar(int idRaport)
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
            cmbPaguar.SelectedIndex = 3;

            cmbPaguar.DataBind();
        }

        private void mbushComboViteNderm(int idNdermarje, ASPxComboBox cmbViti, bool selectItem, string Viti = "")
        {
            DataTable dt = DbCore.DbAdmin.colVitet.merrVitetNdermarjeDT(idNdermarje);
            if (!selectItem)
                dt.Rows.InsertAt(dt.NewRow(), 0);
            cmbViti.DataSource = dt;
            cmbViti.TextField = "KodiViti";
            cmbViti.ValueField = "IdViti";
            cmbViti.DataBind();
            if (selectItem)
                cmbViti.SelectedItem = cmbViti.Items.FindByText(string.IsNullOrEmpty(Viti) ? Convert.ToString(DbCore.mySessionObjects.ktheVitiNdermarrjes(Session)) : Viti);
            hfState.Set("cmbVitiSelectedItem", cmbViti.SelectedIndex);
        }

        private void mbushComboLlojDetajim(bool opsionTeGjitha)
        {
            Detajim1 = rm.GetString("labelDetajim1", ci);
            Detajim2 = rm.GetString("labelDetajim2", ci);
            STR_TeGjitha = rm.GetString("cmbboxItemFilterAvancTeGjitha", ci);
            ASPxComboBox cmbLlojDetajim = new ASPxComboBox();
            string kontrollEmri = "cmbLlojDetajim";
            cmbLlojDetajim = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);

            if (opsionTeGjitha)
                cmbLlojDetajim.Items.Add(STR_TeGjitha, 0);
            cmbLlojDetajim.Items.Add(Detajim1, 1);
            cmbLlojDetajim.Items.Add(Detajim2, 2);
            cmbLlojDetajim.SelectedIndex = 0;

            cmbLlojDetajim.DataBind();
        }


        private void mbushComboStatusAfatDok()
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
        private void mbushComboStatusKonvertimiDyte()
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

            if (RaportiEmerReal == "dokumentaKonvertuar")
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
        private void mbushComboStatusKonvertimi()
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

        private void mbushComboCikli()
        {
            ASPxComboBox cmbCikli = new ASPxComboBox();
            string kontrollEmri = "cmbCikli";
            cmbCikli = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            if (RaportiEmerReal == "grafikuShitjeveSipasOreve" || RaportiEmerReal == "grafikuVleresSeShiturSipasOreve")
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

        private void mbushComboShfaqVlerat()
        {
            ASPxComboBox cmbShfaqVlerat = new ASPxComboBox();
            string kontrollEmri = "cmbShfaqVlerat";
            cmbShfaqVlerat = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbShfaqVlerat.Items.Add(rm.GetString("cmbboxRaportiTeDukshme", ci), 0);
            cmbShfaqVlerat.Items.Add(rm.GetString("cmbboxRaportiTePaDukshme", ci), 1);
            cmbShfaqVlerat.SelectedIndex = 1;
            cmbShfaqVlerat.DataBind();
        }

        private void mbushComboTipGrafiku()
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
            if (RaportiEmerReal == "grafikuShitjeveSipasOreve" || RaportiEmerReal == "grafikuVleresSeShiturSipasOreve")
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
            if (RaportiEmerReal.EqualsAnyIgnoreCase("deklarimNeFinance", "perAprovim", "kartelaEPages", "kartelaEPagesFormat2"))
            {
                var muaji = Converter.MerrVlereOseDefault<string>(Request.QueryString["Muaji"]);
                ConfigureAspxComboBox.mbushComboMuajt(cmbMuaji, idGjuha, muaji, mySessionObjects.merrPeriudheKontabel(Session));
            }
            else
            {
                if (RaportiEmerReal != "formularPagesash" && RaportiEmerReal != "formularSigurimesh")
                    cmbMuaji.Items.Add("", 0);
                ConfigureAspxComboBox.mbushComboMuajt(cmbMuaji, idGjuha, "", null);
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

        private void mbushComboTipGrafikuKrahasues()
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
        private void mbushComboArtikullIVjeter(CultureInfo ci)
        {
            ASPxComboBox cmbArtikullIVjeter = new ASPxComboBox();
            string kontrollEmri = "cmbArtikullIVjeter";
            cmbArtikullIVjeter = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbArtikullIVjeter.Items.Add(rm.GetString("cmbBoxItemProdukteAktuale", ci), 0);
            cmbArtikullIVjeter.Items.Add(rm.GetString("cmbBoxItemProdukteTeVjeter", ci), 1);
            cmbArtikullIVjeter.Items.Add(rm.GetString("cmbBoxItemProdukteTeGjithe", ci), 2);
            switch (RaportiEmerReal)
            {
                case "gjendjaArtikujveIMEIEkspozitor":
                case "gjendjaArtikujveIMEI":
                case "gjendjeArtikujtVodafone":
                    cmbArtikullIVjeter.SelectedIndex = 2;
                    break;
                default:
                    cmbArtikullIVjeter.SelectedIndex = 0;
                    break;
            }
            cmbArtikullIVjeter.DataBind();
        }
        private void mbushComboStatusShperndarje(CultureInfo ci)
        {
            STR_OnConsigment = "On consignment";
            STR_SalesOnCredit = "Sales on credit";
            STR_Gjithe = rm.GetString("cmbboxItemFilterAvancGjitheDok", ci);
            ASPxComboBox cmbStatusShperndarje = new ASPxComboBox();
            string kontrollEmri = "cmbStatusShperndarje";
            cmbStatusShperndarje = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbStatusShperndarje.Items.Add(STR_OnConsigment, 0);
            cmbStatusShperndarje.Items.Add(STR_SalesOnCredit, 1);
            cmbStatusShperndarje.Items.Add(STR_Gjithe, 2);
            cmbStatusShperndarje.SelectedIndex = 2;
            cmbStatusShperndarje.DataBind();
        }

        private void mbushComboStatusDokumenti(CultureInfo ci)
        {
            STR_TeGjitha = rm.GetString("cmbStatusiItemTeGjitha", ci);
            STR_Draft = rm.GetString("cmbStatusiItemDraft", ci);
            STR_Ruajtur = rm.GetString("cmbStatusiItemRuajtur", ci);
            ASPxComboBox cmbStatusiDok = new ASPxComboBox();
            string kontrollEmri = "cmbStatusiDok";
            cmbStatusiDok = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbStatusiDok.Items.Add(STR_TeGjitha, 2);
            cmbStatusiDok.Items.Add(STR_Draft, 0);
            cmbStatusiDok.Items.Add(STR_Ruajtur, 1);
            cmbStatusiDok.SelectedIndex = 2;
            cmbStatusiDok.DataBind();
        }
        private void mbushComboStatusRiparimi(int idNdermarrje)
        {
            DbCore.DbInventari.colStatusRiparimi col = new DbCore.DbInventari.colStatusRiparimi(idNdermarrje);
            ASPxComboBox cmbStatusRiparimi = new ASPxComboBox();
            string kontrollEmri = "cmbStatusRiparimi";
            cmbStatusRiparimi = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl(kontrollEmri);
            cmbStatusRiparimi.DataSource = col;
            cmbStatusRiparimi.TextField = "Pershkrimi";
            cmbStatusRiparimi.ValueField = "Id";
            cmbStatusRiparimi.DataBind();
            cmbStatusRiparimi.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        private void mbushComboPasqyra(int idNdermarrje, int idgjuha)
        {
            ASPxComboBox cmbPasqyra = new ASPxComboBox();
            clsNdermarrje ndermarrja = new clsNdermarrje(idNdermarrje);
            string kontrollEmri = "cmbPasqyra";
            cmbPasqyra = (ASPxComboBox)navBarFiltrat.Groups[0].FindControl(kontrollEmri);

            DbCore.DbKontabiliteti.colPasqyratFinaciare colPas;
            if (RaportiEmerReal == "bilanci" || RaportiEmerReal == "bilanciQendraKosto" || RaportiEmerReal == "pasqyreKonsoliduarGjendjeFinanciare" || RaportiEmerReal == "bilancikontabelformat2")
                colPas = new DbCore.DbKontabiliteti.colPasqyratFinaciare("Bilanc", idNdermarrje);
            else if (RaportiEmerReal == "burimeShpenzimeInvestime" || RaportiEmerReal == "pasqyraLevizjeFondesh" || RaportiEmerReal == "pasqyraLevizjesNeCash" || RaportiEmerReal == "gjendjeNdryshimeAktiveTeQendrueshem" || RaportiEmerReal == "pasqyraAmortizimeve" || RaportiEmerReal == "analizeAktiveQarkullues" || RaportiEmerReal == "pasqyrePermbledheseDebitore" || RaportiEmerReal == "pasqyrePermbledheseFurnitore" || RaportiEmerReal == "analizaDetyrime" || RaportiEmerReal == "pasqyraLevizjeveAQT" || RaportiEmerReal == "pasqyraAmortizimitAseteve" || RaportiEmerReal == "RapPasqyraNdryshimeveNeAktivetNetoFondetNeto")
                colPas = new DbCore.DbKontabiliteti.colPasqyratFinaciare("Buxhetor", idNdermarrje, IdRaporti);
            else if (RaportiEmerReal == "GjendjaDheNdryshimetEAktiveveAfatgjataVleraNeto")
                colPas = new DbCore.DbKontabiliteti.colPasqyratFinaciare("Buxhetor", idNdermarrje, clsRaporti.KtheIdRaportiSipasEmritReal("gjendjeNdryshimeAktiveTeQendrueshem"));
            else if (RaportiEmerReal == "ardhura" || RaportiEmerReal == "teArdhuraShpenzimeQendraKosto" || RaportiEmerReal == "pasqyreEKonsoliduarTeArdhuraShpenzime" || RaportiEmerReal == "analizeArdhuraShpenzime" || RaportiEmerReal == "teardhuraKesh" || RaportiEmerReal == "ardhura_shpenzime_sipas_muajve" || RaportiEmerReal == "RaportiPermbledheseRealizimevedheParashikimeve" || RaportiEmerReal == "RaportiPermbledheseRealizimevedheParashikimeveTeArdhuraShpenzime")
                colPas = new DbCore.DbKontabiliteti.colPasqyratFinaciare("PASH", idNdermarrje);
            else if (RaportiEmerReal == "PasqyraAktiviteteve")
                colPas = new DbCore.DbKontabiliteti.colPasqyratFinaciare("PASHOJF", idNdermarrje);
            else if (RaportiEmerReal == "PasqyraPozicionFinanciar")
                colPas = new DbCore.DbKontabiliteti.colPasqyratFinaciare("BilancOJF", idNdermarrje);
            else if (RaportiEmerReal == "PasqyraFlukseveMjeteve")
                colPas = new DbCore.DbKontabiliteti.colPasqyratFinaciare("CashflowOJF", idNdermarrje);
            else
                colPas = new DbCore.DbKontabiliteti.colPasqyratFinaciare("Cash Flow", idNdermarrje);
            cmbPasqyra.SelectedIndex = (idgjuha == 0) ? 0 : 1;
            cmbPasqyra.ValueField = "IdPasqyresFin";
            cmbPasqyra.TextField = "KodiPasqyresFin";
            cmbPasqyra.DataSource = colPas;
            cmbPasqyra.DataBind();
            var dizajnet = new colRaporteDesign(idNdermarrje, RaportiEmerReal);
            var rapdes = dizajnet.MerrDizajnTeZgjedhur();
            if (ndermarrja.Lloji == 2) //rasti kur jane ndermarjet buxhetore
            {
                if (RaportiEmerReal == "ardhura")
                    cmbPasqyra.Text = idgjuha == 0 ? "PB" : "PAng1";
                if (RaportiEmerReal == "burimeShpenzimeInvestime")
                    cmbPasqyra.Text ="BSHI2018";
                if (RaportiEmerReal == "gjendjeNdryshimeAktiveTeQendrueshem" || RaportiEmerReal == "GjendjaDheNdryshimetEAktiveveAfatgjataVleraNeto")
                    cmbPasqyra.Text = "GJAQ2018";
                if (rapdes.Pershkrim == "Bilanci per Buxhetore")
                    cmbPasqyra.Text = "BB2018";
                if (rapdes.Pershkrim == "Pash buxhetor")
                    cmbPasqyra.Text = "PB2018";
                if (rapdes.Pershkrim == "Cash Flow buxhetor")
                    cmbPasqyra.Text =  "Cash_Flow_PB2018";
                if (RaportiEmerReal == "pasqyreEKonsoliduarTeArdhuraShpenzime")
                    cmbPasqyra.Text = idgjuha == 0 ? "PKASH" : "PKASH Ang";
                if (RaportiEmerReal == "pasqyreKonsoliduarGjendjeFinanciare")
                    cmbPasqyra.Text = idgjuha == 0 ? "PKGJF" : "PKGJF Ang";
                if (RaportiEmerReal == "analizeArdhuraShpenzime")
                    cmbPasqyra.Text = idgjuha == 0 ? "AAS" : "AAS Ang";
                if (RaportiEmerReal == "ardhura_shpenzime_sipas_muajve")
                    cmbPasqyra.Text = idgjuha == 0 ? "P1" : "PAng1";
            }
            else
            {
                if (RaportiEmerReal == "ardhura" || RaportiEmerReal == "teardhuraKesh" || RaportiEmerReal == "ardhura_shpenzime_sipas_muajve")
                    cmbPasqyra.Text = idgjuha == 0 ? "P1" : "PAng1";
                if (RaportiEmerReal == "bilanci" || RaportiEmerReal == "bilancikontabelformat2")
                    cmbPasqyra.Text = idgjuha == 0 ? "B" : "BAng";
                if (RaportiEmerReal == "cashFlow")
                    cmbPasqyra.Text = idgjuha == 0 ? "Cash Flow" : "Cash Flow Ang";
            }
        }

        private bool mbushComboBoxFiltra(int idPerdoruesi, int idNdermarrje)
        {
            colFilterKoka colFiltra = new colFilterKoka(IdRaporti, idPerdoruesi, idNdermarrje);
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
                ValueField = "IdKokaFilter",
                TextField = "KokaFilterKodi",
                DataSource = colFiltra
            };
            DbCore.mySessionObjects.RuajNeSession(System.Web.HttpContext.Current.Session, model, $"filtraGride_{1}");
            DevExpress.Web.MenuItem itemButton = ASPxMenuToolBar.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            cmbFiltra.SelectedIndex = 0;
            return done;
        }

        private void konfiguroVleraFillestare(int idPerdoruesi, int idNdermarrje, int idGjuha)
        {
            ConfigureAspxComboBox.KonfiguroComboBoxStatusMarreveshje((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbStatusMarreveshje"), true, RaportiEmerReal == "Rap_GjendjaBuxhetit" || RaportiEmerReal == "Rap_StatusiMarreveshjeve", 1);
            ConfigureAspxComboBox.mbushComboStatusiHr((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbStatusHR"), rm, ci);

            SetDesigns(out clsRaportDesign rapdes);
            ShfaqButonEdit = rapdes.IModifikueshem;
            SetOrientations(rapdes);

            AspxWebControlUtils.vendosDateEditMask(((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDok")), ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDok")),
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDokQenderKosto")), ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDokQenderKosto")),
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDtKrahasuesQK")), ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDtKrahasuesQK")),
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtNgaDtFillimi")), ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtDeriDtFillimi")),
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtNgaDtMbarimi")), ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtDeriDtMbarimi")),
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtNgaRegj")), ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtDeriRegj")));
            if(RaportiEmerReal== "KontrolliSkadencesArtikujve")
                mbushComboNivelKonfigurimPerKonvertimiFd(idPerdoruesi, idNdermarrje, idGjuha);
            else 
            mbushComboNivelKonfigurimPerKonvertimi(idPerdoruesi, idNdermarrje, idGjuha);
            mbushComboKonvertoNe(idPerdoruesi, IdRaporti);
            mbushComboStandarte(idNdermarrje);
            mbushComboLikuiduar();
            mbushcomboLlojArtikulli(IdRaporti);
            mbushcomboStatus(IdRaporti);
            mbushcomboLlojSubjekti();
            mbushcomboLlojSubjektiKF();
            mbushComboStatus();
            mbushComboLlojiArtBurim();
            mbushComboKategorizimArtikuj(IdRaporti);
            mbushComboVeprimePeriudhe();
            mbushComboGjendjeDetyrimi();
            mbushComboStatusCRM();
            mbushComboGrupoSipas();
            mbushComboGrupoSipasAgjenteve();
            mbushComboGjendjeArt(IdRaporti);
            mbushComboShfaqArt(IdRaporti);
            mbushComboLlojPorosie(IdRaporti);
            mbushComboLlojKrahasimKostoje(IdRaporti);
            mbushComboLlojGrupoKlientSipas(IdRaporti);
            mbushComboGrupoArtikullSipas(IdRaporti);
            mbushComboKlientAktiv(IdRaporti);
            mbushComboKategoriShpenzimiAktive(IdRaporti);
            mbushComboLlojCmimiMePaTVSH(IdRaporti);
            mbushComboLlojStatusRezervimi(IdRaporti);
            mbushComboKontabilizuar(IdRaporti);
            ConfigureAspxComboBox.mbushComboStatusFaturuar((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbFaturuar1"), rm, ci);
            ConfigureAspxComboBox.mbushComboStatusFaturuar((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbFaturuar2"), rm, ci);
            ConfigureAspxComboBox.mbushComboStatusPerfunduar((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbPerfunduar1"), rm, ci);
            ConfigureAspxComboBox.mbushComboStatusPerfunduar((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbPerfunduar2"), rm, ci);
            mbushComboStatusProdhuar(IdRaporti);
            mbushComboEmertimLlog(idGjuha);
            mbushComboGjendja();
            mbushComboAktiv();
            mbushComboPaguar(IdRaporti);
            mbushComboLlojDetajim(RaportiEmerReal == "gjendjaMagazinesSipasDetajimeve");
            mbushComboStatusAfatDok();
            mbushComboCikli();
            mbushComboShfaqVlerat();
            ConfigureAspxComboBox.mbushComboKLlojElementesh(idNdermarrje, (ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLlojiElem"), idGjuha);
            ConfigureAspxComboBox.mbushComboStatusiElem(idNdermarrje, (ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbStatusElem"), idGjuha);

            mbushComboPasqyra(idNdermarrje, idGjuha);
            mbushComboNiveli(idPerdoruesi, idNdermarrje);
            mbushComboTipGrafiku();

            mbushComboStatusDokumenti(ci);
            string viti = Request.QueryString["Viti"] == null ? "" : Request.QueryString["Viti"];
            mbushComboViteNderm(idNdermarrje, (ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbViti"), true, viti);
            if (RaportiEmerReal.StartsWith("bonuse2MujoreKlientesh"))
                mbushComboMuajtInterval2Mujore();
            else
                mbushComboMuajt((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbMuaji"), idGjuha);
            mbushComboMuajt((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbMuajRaportimi"), idGjuha);

            mbushComboViteNderm(idNdermarrje, (ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVitRaportimi"), false);
            mbushComboTipGrafikuKrahasues();
            mbushComboStatusKonvertimiDyte();
            mbushComboStatusKonvertimi();
            mbushComboArtikullIVjeter(ci);
            mbushComboStatusShperndarje(ci);
            mbushComboStatusRiparimi(idNdermarrje);
            mbushGrupDokumenti01(1, "cmbGrupP01", idNdermarrje, idPerdoruesi);
            mbushGrupDokumenti01(1, "cmbGrupP02", idNdermarrje, idPerdoruesi);
            mbushComboDogana(ci);

            ASPxComboBox combo = (ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbFormatNumri");
            ConfigureAspxComboBox.mbushComboFormateNumrashNew(combo);

            switch (RaportiEmerReal)
            {
                case "bilancikontabelformat2":
                case "liber_shitje2015":
                case "LibriBlerjes2019":
                case "LibriShitjeveVodafone":
                case "LibriShitjes2019":
                    combo.SelectedIndex = 0;
                    break;

                default:
                    combo.SelectedIndex = 2;
                    break;
            }

            ConfigureAspxComboBox.mbushComboStatusMagazine(idNdermarrje, (ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbStatusMagazine"));
            ConfigureAspxComboBox.mbushComboKlasaArtikullit((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbKlasaArtikullit1"));
            ConfigureAspxComboBox.mbushComboKlasaArtikullit((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbKlasaArtikullit2"));
            ConfigureAspxComboBox.KonfiguroComboBoxComboQytete(IdNdermarrja, (ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbQyteti1"));
            ConfigureAspxComboBox.KonfiguroComboBoxComboQytete(IdNdermarrja, (ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbQyteti2"));
            ConfigureAspxComboBox.mbushComboLlojLayerMagazine((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLlojLayeri"));
            ConfigureAspxComboBox.mbushComboPajisje((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbPajisje1"), idNdermarrje);
            ConfigureAspxComboBox.mbushComboPajisje((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbPajisje2"), idNdermarrje);
            ConfigureAspxComboBox.mbushComboLlojKosto((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbGrupKosto"), ci);
            ConfigureAspxComboBox.mbushComboMenyrePagese((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbMenyrePagese1"), true);
            ConfigureAspxComboBox.mbushComboMenyrePagese((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbMenyrePagese2"), true);
            ConfigureAspxComboBox.mbushComboLlojVeprimi((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLlojVeprimi1"), rm, ci, RaportiEmerReal);
            ConfigureAspxComboBox.mbushComboLlojVeprimi((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLlojVeprimi2"), rm, ci, RaportiEmerReal);
            ConfigureAspxComboBox.mbushComboTipKontrate((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbTipKontrate"), idNdermarrje, idGjuha);
            ConfigureAspxComboBox.mbushComboMenuja((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbMenu1"));
            ConfigureAspxComboBox.mbushComboMenuja((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbMenu2"));
            ConfigureAspxComboBox.mbushComboLlojCmimi((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLlojCmimi"));
            ConfigureAspxComboBox.KonfiguroComboBoxNjesiKohe((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbNjesiKohe"), 2);
            ConfigureAspxComboBox.mbushComboLlojModeli((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLlojFushaShtese"), true);
            ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("dteDtMaturimi")).Date = DateTime.Today;
            AspxWebControlUtils.vendosDateEditMask(((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("dteDtMaturimi")));
            ((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtInterval1")).Text = (RaportiEmerReal == "PagesatPerMPesaSipasIntervaleve") ? "1" : "0";
            if (RaportiEmerReal == "PagesatPerMPesaSipasIntervaleve")
                ((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtInterval6")).Text = "10000";
            else if (RaportiEmerReal == "shitjeKlienteveIntervale")
                ((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtInterval6")).Text = "42000000";
            else ((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtInterval6")).Text = "150";
            ((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtInterval7")).Text = (RaportiEmerReal == "PagesatPerMPesaSipasIntervaleve") ? "15000" : "140000000";


            if (RaportiEmerReal == "shitjeanalizaarikujprodhim")
            {
                ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbKlasaArtikullit1")).Text = "Prodhim";
            }

            if (RaportiEmerReal == "teardhuraKesh")
            {
                combo.SelectedIndex = 0;
            }

            string interval2, interval3, interval4, interval5;

            switch (RaportiEmerReal)
            {
                case "vjetersiDetyrimeshKliente":
                    {
                        interval5 = "360";
                        interval2 = "90";
                        interval3 = "180";
                        interval4 = "270";
                        break;
                    }
                case "maturimStokuArtikujAfatgjate":
                case "MaturimiStokutPerArtikujtMeSeriale":
                    {
                        interval2 = "90";
                        interval5 = "365";
                        interval3 = "180";
                        interval4 = "270";
                        break;
                    }
                case "shitjeKlienteveIntervale":
                    {
                        interval5 = "1400000";
                        interval3 = "210000";
                        interval2 = "70000";
                        interval4 = "420000";
                        break;
                    }

                case "MaturimiFaturaveTeKlienteve":
                    {
                        interval2 = "15";
                        interval3 = "31";
                        interval4 = "61";
                        interval5 = "";
                        break;
                    }

                case "MaturimiFaturaveTeFurnitoreve":
                    {
                        interval2 = "15";
                        interval3 = "31";
                        interval4 = "61";
                        interval5 = "";
                        break;
                    }

                case "PagesatPerMPesaSipasIntervaleve":
                    {
                        interval2 = "1000";
                        interval3 = "2000";
                        interval4 = "3000";
                        interval5 = "5000";
                        break;
                    }
                default:
                    {
                        interval2 = "30";
                        interval3 = "60";
                        interval4 = "90";
                        interval5 = "120";
                        break;
                    }
            }

            ((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtInterval5")).Text = interval5;
            ((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtInterval2")).Text = interval2;
            ((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtInterval3")).Text = interval3;
            ((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtInterval4")).Text = interval4;

            if (RaportiEmerReal == "shitjeSipasMuajveKrahasues") ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimePeriudhe")).SelectedIndex = 2;
            if (RaportiEmerReal.ToLower().Contains("marzhi"))
                percaktoVleraDefaultFiltriKlasaArtikullit();

            if (RaportiEmerReal == "analitikVeprimtari" && Request.QueryString["idKlienti"] != null)
            {
                ASPxButtonEdit btnEdit = navBarFiltrat.Groups[1].FindControl("btneKodKF1") as ASPxButtonEdit;

                btnEdit.Text = new DbCore.DbKontabiliteti.clsKlientFurnitor(Convert.ToInt32(Request.QueryString["idKlienti"])).KodKlientFurnitor;
            }
            ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("dateDateKontrate")).Date = DateTime.Now;
            ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("dataSkadence")).Date = DateTime.Now;
            if (RaportiEmerReal.ToLower().Equals("marzhishitjevesipasklienteve") || RaportiEmerReal.ToLower().Equals("marzhishitjevesipasgrupimeartikujve"))
            {
                ((ASPxCheckBox)navBarFiltrat.Groups[1].FindControl("cbdetajuar")).Checked = true;
            }
        }

        private void mbushComboNivelKonfigurimPerKonvertimi(int idPerdoruesi, int idNdermarrje, int idGjuha)
        {
            var idub = DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi("UB", idNdermarrje);
            cmbKonverto.Items.Add("UB", idub);
            cmbKonverto.SelectedIndex = 0;
            colKonfigurimAmbjenti col = new colKonfigurimAmbjenti();
            col.mbushKonfigAmbjSipasIdKategoriIdNivel(2, idub, idPerdoruesi, idGjuha, false);
            cmbKonf.DataSource = col;
            cmbKonf.TextField = "KodKonfigAmbjente";
            cmbKonf.ValueField = "IdKonfigAmbjente";
            cmbKonf.DataBind();
            cmbKonf.SelectedIndex = 0;
        }
        private void mbushComboNivelKonfigurimPerKonvertimiFd(int idPerdoruesi, int idNdermarrje, int idGjuha)
        {
            var idub =  DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi("FD", idNdermarrje);
            cmbKonverto.Items.Add("FD", idub);
            cmbKonverto.SelectedIndex = 0;
            colKonfigurimAmbjenti col = new colKonfigurimAmbjenti();
            col.mbushKonfigAmbjSipasIdKategoriIdNivelDMT(6, idub, idPerdoruesi, idGjuha, true);
            cmbKonf.DataSource = col;
            cmbKonf.TextField = "KodKonfigAmbjente";
            cmbKonf.ValueField = "IdKonfigAmbjente";
            cmbKonf.DataBind();
            cmbKonf.SelectedIndex = 0;
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
                    if (kontrolli.KodKontrolli == "cmbMonedhaQK")
                    {
                        ASPxComboBox cmbMonedha = (ASPxComboBox)uiKontroll;
                        if (RaportiEmerReal == "veprimtariaDitore")
                        {
                            cmbMonedha.Items.Add("LEK", "LEK");
                            cmbMonedha.Items.Add("EUR", "EUR");
                        }
                        else
                        {
                            cmbMonedha.Items.Add("Monedhe QK", 0);
                            cmbMonedha.Items.Add("Monedhe baze", 1);
                        }
                        cmbMonedha.SelectedIndex = 1;
                        cmbMonedha.DataBind();
                    }
                    else if (kontrolli.KodKontrolli.StartsWith("cmbMonedha") || kontrolli.KodKontrolli.StartsWith("cmbKonvertoNe"))
                    {
                        ConfigureAspxComboBox.mbushComboMonedha(idPerdoruesi, idNdermarrje, (ASPxComboBox)uiKontroll, true, RaportiEmerReal == "gjendjaPermbledheseArkeBankeMonedheKerkuar");
                        hfState.Set("cmbMonedhaSelectedIndex", ((ASPxComboBox)uiKontroll).SelectedIndex);
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
                    if (RaportiEmerReal == "grafikTeArdhurash" || RaportiEmerReal == "grafikKrahasueshTeArdhurash" || RaportiEmerReal == "MaturimiFaturaveTeKlienteve" || RaportiEmerReal == "MaturimiFaturaveTeFurnitoreve" || RaportiEmerReal == "klienteMeAfateMaturimi" || RaportiEmerReal == "pasqyreEKonsoliduarTeArdhuraShpenzime" || RaportiEmerReal == "permbledhesSipasKlientevePajisjeve" || RaportiEmerReal == "analitikSipasKartave" || RaportiEmerReal == "LargimetPerfaqesuesveTeShitjesVjetore" || RaportiEmerReal == "LargimetPerfaqesuesveTeShitjesMujore" || RaportiEmerReal == "RaportiPermbledheseRealizimevedheParashikimeve" || RaportiEmerReal == "RaportiPermbledheseRealizimevedheParashikimeveTeArdhuraShpenzime")
                    {
                        if (kontrolli.KodKontrolli.StartsWith("radDtDok"))
                        {
                            ((ASPxRadioButtonList)uiKontroll).Value = "VitiUshtrimor";
                        }
                        else if (kontrolli.KodKontrolli.StartsWith("radDtDok") && (RaportiEmerReal == "permbledhesSipasKlientevePajisjeve" || RaportiEmerReal == "analitikSipasKartave"))
                        {
                            ((ASPxRadioButtonList)uiKontroll).Value = "GjitheVitet";
                        }
                    }
                    if (RaportiEmerReal == "EksportimiFaturaShitje")
                    {
                        if (kontrolli.KodKontrolli.StartsWith("radDtDok"))
                        {
                            ((ASPxRadioButtonList)uiKontroll).Value = "Periudha";
                        }
                    }
                    if (RaportiEmerReal == "arketimeDitore")
                    {
                        if (kontrolli.KodKontrolli.StartsWith("radDtDok"))
                        {
                            ((ASPxRadioButtonList)uiKontroll).Value = "Aktuale";
                        }
                    }

                    if (RaportiEmerReal == "analizaStokutEkspozitorKrahasimShitje" || RaportiEmerReal == "analizeStokuKrahasimShitje")
                    {
                        if (kontrolli.KodKontrolli.StartsWith("radDtDokShitje"))
                        {
                            ((ASPxRadioButtonList)uiKontroll).Value = "VitiUshtrimor";
                        }
                    }
   
                    if (kontrolli.KodKontrolli.Equals("txtNgaDokShitje"))
                    {
                        if (periudha != null)
                            ((ASPxDateEdit)uiKontroll).Date = periudha.FillimiPeriudha;
                        continue;
                    }

                    if (kontrolli.KodKontrolli.Equals("txtDeriDokShitje"))
                    {
                        if (periudha != null)
                            ((ASPxDateEdit)uiKontroll).Date = periudha.MbarimiPeriudha;
                        continue;
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
                    if (kontrolli.KodKontrolli.Equals("txtNgaDttakimi"))
                    {
                        if (periudha != null)
                            ((ASPxDateEdit)uiKontroll).Date = periudha.FillimiPeriudha;
                        continue;
                    }

                    if (kontrolli.KodKontrolli.Equals("txtDeriDttakimi"))
                    {
                        if (periudha != null)
                            ((ASPxDateEdit)uiKontroll).Date = periudha.MbarimiPeriudha;
                        continue;
                    }
                    if (kontrolli.KodKontrolli.Equals("txtNgaDok"))
                    {
                        if (periudha != null && RaportiEmerReal != "HyrjetSipasDyqaneve" && RaportiEmerReal != "EksportimiFaturaShitje")
                            ((ASPxDateEdit)uiKontroll).Date = periudha.FillimiPeriudha;
                        else if (RaportiEmerReal == "EksportimiFaturaShitje")
                            ((ASPxDateEdit)uiKontroll).Date = DateTime.Now;
                        else
                        continue;
                    }

                    if (kontrolli.KodKontrolli.Equals("txtDeriDok"))
                    {
                        if (periudha != null && RaportiEmerReal != "HyrjetSipasDyqaneve" && RaportiEmerReal != "EksportimiFaturaShitje")
                            ((ASPxDateEdit)uiKontroll).Date = periudha.MbarimiPeriudha;
                        else if (RaportiEmerReal == "EksportimiFaturaShitje")
                            ((ASPxDateEdit)uiKontroll).Date = DateTime.Now;
                        else
                            continue;
                    }
                    if (kontrolli.KodKontrolli.Equals("txtNgaDokQenderKosto"))
                    {
                        if (periudha != null)
                            ((ASPxDateEdit)uiKontroll).Date = periudha.FillimiPeriudha;
                        continue;
                    }

                    if (kontrolli.KodKontrolli.Equals("txtDeriDokQenderKosto"))
                    {
                        if (periudha != null)
                            ((ASPxDateEdit)uiKontroll).Date = periudha.MbarimiPeriudha;
                        continue;
                    }
                    if (kontrolli.KodKontrolli.Equals("txtNgaKrijimi"))
                    {
                        if (periudha != null)
                            ((ASPxDateEdit)uiKontroll).Date = periudha.FillimiPeriudha;
                        continue;
                    }

                    if (kontrolli.KodKontrolli.Equals("txtDeriKrijimi"))
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

                    if (RaportiEmerReal == "grafikKrahasueshTeArdhurash")
                        if (kontrolli.KodKontrolli.StartsWith("radDtKrahasues"))
                        {
                            ((ASPxRadioButtonList)uiKontroll).Value = "VitiUshtrimor";
                        }

                    if (kontrolli.KodKontrolli.StartsWith("txtNgaDtKrahasues"))
                    {
                        if (periudha != null)
                            ((ASPxDateEdit)uiKontroll).Date = periudha.FillimiPeriudha.AddYears(-1);
                    }

                    if (kontrolli.KodKontrolli.Equals("txtDeriDtKrahasues") || kontrolli.KodKontrolli.Equals("txtDeriDtKrahasuesQK"))
                    {
                        if (periudha != null)
                            ((ASPxDateEdit)uiKontroll).Date = periudha.MbarimiPeriudha.AddYears(-1);
                        continue;
                    }


                    if (kontrolli.KodKontrolli.Equals("txtNgaRegj"))
                    {
                        DateTime fillimiPeriudha = new DateTime(1900, 01, 01);
                        if (RaportiEmerReal == "HistorikuTeDhenaveTePerfaqesuesveTeShitjes")
                            ((ASPxDateEdit)uiKontroll).Date = periudha.FillimiPeriudha;
                        else
                            ((ASPxDateEdit)uiKontroll).Date = fillimiPeriudha;
                        continue;
                    }

                    if (kontrolli.KodKontrolli.Equals("txtDeriRegj"))
                    {
                        DateTime mbarimiPeriudha = DateTime.MaxValue.AddDays(-1);
                        if (RaportiEmerReal == "HistorikuTeDhenaveTePerfaqesuesveTeShitjes")
                            ((ASPxDateEdit)uiKontroll).Date = periudha.MbarimiPeriudha;
                        else
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
                        if (RaportiEmerReal == "maturimiPorosive")
                            fillimiPeriudha = DateTime.Today;
                        ((ASPxDateEdit)uiKontroll).Date = fillimiPeriudha;
                        continue;
                    }

                    if (kontrolli.KodKontrolli.StartsWith("txtDeriPeriudheMaturimi"))
                    {
                        DateTime mbarimiPeriudha = DateTime.MaxValue.AddDays(-1);
                        if (RaportiEmerReal == "maturimiPorosive")
                            mbarimiPeriudha = DateTime.Today;
                        if (RaportiEmerReal == "MaturimiFaturaveTeKlienteve" || RaportiEmerReal == "MaturimiFaturaveTeFurnitoreve")
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
                    if (kontrolli.KodKontrolli.Equals("txtNgaDokDateLidhes"))
                    {
                        if (periudha != null)
                            ((ASPxDateEdit)uiKontroll).Date = periudha.FillimiPeriudha;
                        continue;
                    }

                    if (kontrolli.KodKontrolli.Equals("txtDeriDokDateLidhes"))
                    {
                        if (periudha != null)
                            ((ASPxDateEdit)uiKontroll).Date = periudha.MbarimiPeriudha;
                        continue;
                    }

                    if (kontrolli.KodKontrolli.Equals("txtNgaDokShitje"))
                    {
                        if (periudha != null)
                            ((ASPxDateEdit)uiKontroll).Date = periudha.FillimiPeriudha;
                        continue;
                    }

                    if (kontrolli.KodKontrolli.Equals("txtDeriDokShitje"))
                    {
                        if (periudha != null)
                            ((ASPxDateEdit)uiKontroll).Date = periudha.MbarimiPeriudha;
                        continue;
                    }

                    if (RaportiEmerReal == "KontrolliSkadencesArtikujve")
                    {
                        if (kontrolli.KodKontrolli.StartsWith("radDtSkadence"))
                        {
                            ((ASPxRadioButtonList)uiKontroll).Value = "Periudha";
                        }
                    }
                    if (RaportiEmerReal == "KontrolliSkadencesArtikujve")
                    {
                        if (kontrolli.KodKontrolli.Equals("radDtDok"))
                        {
                            ((ASPxRadioButtonList)uiKontroll).Value = "GjitheVitet";
                        }
                    }
                    if (kontrolli.KodKontrolli.Equals("txtNgaSkadence"))
                    {
                        if (periudha != null)
                            ((ASPxDateEdit)uiKontroll).Date = DateTime.Now;
                        continue;
                    }

                    if (kontrolli.KodKontrolli.Equals("txtDeriSkadence"))
                    {
                        if (periudha != null)
                            ((ASPxDateEdit)uiKontroll).Date = DateTime.Now.AddMonths(3);
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

        }

        protected void exportButton_Click(object sender, EventArgs e)
        {            
            string arsye = exportOption.Value;

            switch (arsye)
            {
                case "SaveToDiskTatime":
                    exportPerTatimet(IdNdermarrja, mySessionObjects.kthePerdorues(Session), IdViti, IdNdermarrjeVit, mySessionObjects.merrPeriudheKontabel(Session));
                    break;
                case "saveToDiskVeprimtariaDitore":
                    exportVeprimtariaDitore();
                    break;
                case "saveToDiskBirthdayCard":
                    exportRaportBirthdayCard();
                    break;
                case "CustomExport_xlsx":
                case "CustomExport_xls":
                case "CustomExport_csv":
                    string exportFormat1 = arsye.Split('_')[1];
                    ExportReport(exportFormat1, false);
                    break;
                case "CustomExport_xlsx_raw":
                case "CustomExport_xls_raw":
                    string exportFormat2 = arsye.Split('_')[1];
                    ExportReport(exportFormat2, true);
                    break;
            }
        }
        private void ExportReport(string exportFormat, bool rawFormat)
        {
            XtraReport report = GetReport();
            SetExportOptions(report, RaportiEmerReal, exportFormat, rawFormat);

            using (MemoryTributary ms = new MemoryTributary())
            {
                switch (exportFormat)
                {
                    case "xls":
                        report.ExportToXls(ms);
                        break;
                    case "xlsx":
                        report.ExportToXlsx(ms);
                        break;
                    case "csv":
                        report.ExportToCsv(ms);
                        break;
                }
                ms.Seek(0, SeekOrigin.Begin);
                WriteDocumentToResponse(ms, exportFormat, false, $"{report.GetType().Name}.{exportFormat}");
            }
        }

        void WriteDocumentToResponse(MemoryTributary documentStream, string format, bool isInline, string fileName)
        {
            string contentType;
            string disposition = (isInline) ? "inline" : "attachment";

            switch (format.ToLower())
            {
                case "xls":
                    contentType = "application/vnd.ms-excel";
                    break;
                case "xlsx":
                    contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    break;
                case "mht":
                    contentType = "message/rfc822";
                    break;
                case "html":
                    contentType = "text/html";
                    break;
                case "txt":
                case "csv":
                    contentType = "text/plain";
                    break;
                case "png":
                    contentType = "image/png";
                    break;
                default:
                    contentType = String.Format("application/{0}", format);
                    break;
            }

            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = contentType;
            Response.AddHeader("Accept-Header", documentStream.Length.ToString());
            Response.AddHeader("Content-Length", documentStream.Length.ToString());
            Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            foreach (byte[] block in documentStream.Blocks)
            {
                Response.BinaryWrite(block);
                Response.Flush();
            }
            Response.CacheControl = "No-cache";
            Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        public void SetExportOptions(XtraReport r, string EmerRaporti, string exportFormat, bool rawFormat)
        {
            switch (exportFormat)
            {
                case "xlsx":
                    r.ExportOptions.Xlsx.RawDataMode = rawFormat;
                    hiqReportHeaderNgaRaporti(r, EmerRaporti);
                    break;
                case "xls":
                    r.ExportOptions.Xls.RawDataMode = rawFormat;
                    hiqReportHeaderNgaRaporti(r, EmerRaporti);
                    break;
                case "csv": //to do Per Vodafone ne raportin e shitjeve ditore
                    DevExpress.XtraPrinting.CsvExportOptions csvOptions = r.ExportOptions.Csv;
                    csvOptions.Encoding = Encoding.Unicode;
                    csvOptions.Separator = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator.ToString();
                    hiqReportHeaderNgaRaporti(r, EmerRaporti);
                    hiqPageFooterNgaRaporti(r, EmerRaporti);
                    break;
            }
        }

        private void hiqReportHeaderNgaRaporti(XtraReport r, string EmerRaporti)
        {
            try
            {
                switch (EmerRaporti)
                {
                    case "hyrjeVodafone":
                    case "gjendjeArtikujtVodafone":
                    case "veprimeTeAnulluaraVodafone":
                    case "gjendjaEProdukteveLoan":
                    case "porosiDealerVodafone":
                    case "shitjeAnalitikeVodafone":
                    case "hyrjeVodafoneNdermarrjeBije":
                    case "veprimeTeAnulluaraVodafoneNdermarrjeBije":
                    case "gjendjaMagazinesSipasDetajimeve":
                    case "logePerKartelePunonjesi":
                    case "RptKartelaLlogariveFormat2":
                    case "labourOfficeReport":
                    case "lejeVjetore":
                    case "punonjesQenderKosto":
                    case "listeArketimeAnullimePostpaid":
                        {
                            Band band = r.Bands.GetBandByType(typeof(ReportHeaderBand));
                            band.Visible = false;
                        }
                        break;
                }
            }
            catch (Exception err)
            {
                ImbLogger.Error(err.Message);
            }
        }

        public void hiqPageFooterNgaRaporti(XtraReport r, string emri)
        {
            switch (emri)
            {
                case "listeArketimeAnullimePostpaid":
                    Band band = r.Bands.GetBandByType(typeof(PageFooterBand));
                    band.Visible = false;
                    break;
            }
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
            if (RaportiEmerReal == "rivleresimKolateraleshIFRS")
                cmbStandarti.SelectedIndex = 1;
            else
                cmbStandarti.SelectedIndex = 0;
            cmbStandarti.DataBind();
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
                        case "ASPxRadioButtonList":
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

        private string ktheVlereParametri(clsParameter parametri, int idNdermarrje, clsPeriudhaKontabel periudhaKontabel, int idNderviti, int idPerdorues, colKontrolle oColKontrolleRaporti)
        {
            string parametriAktual = "";
            try
            {
                string vlera = "";
                int idGjuha = Convert.ToInt32(hfgjuha.Get("idGjuha"));
                switch (parametri.Emri.ToLower())
                {
                    case "idraport":
                        return vlera = IdRaporti.ToString();
                    case "idndermarje":
                        return vlera = idNdermarrje.ToString();// DbCore.mySessionObjects.ktheNdermarrjeVit(Session).ToString();
                    case "idgjuha":
                        return ci.Name == "sq-AL" ? "0" : "1";
                    case "salt":
                        return vlera = System.Web.Configuration.WebConfigurationManager.AppSettings["salt"];
                    case "idnderviti":
                        return vlera = idNderviti.ToString();
                    case "idperdoruesi":
                        return vlera = idPerdorues.ToString();
                    case "shikogjithedokumentat":
                        return vlera = hfTeDrejtaRaporti.Get("dGjitheDok").ToString();
                    case "idpasqyrafinaciarekoka":
                        object idpasqyrfinckoka = ((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbPasqyra")).Value;
                        return vlera = idpasqyrfinckoka == null ? "0" : idpasqyrfinckoka.ToString();
                    case "filterGrupoSipasAgjenteve":
                        return vlera = ((ASPxComboBox)navBarFiltrat.Groups[0].FindControl("cmbGrupoSipasAgjenteve")).Value.ToString();
                    case "filterkodifikimartp":
                        object tmpValue1 = ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("GrupimPareButtonEdit1")).Value;
                        return vlera = tmpValue1 == null ? "" : tmpValue1.ToString();
                    case "filterkodifikimartd":
                        object tmpValue2 = ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("GrupimDyteButtonEdit1")).Value;
                        return vlera = tmpValue2 == null ? "" : tmpValue2.ToString();
                    case "filterkodifikimartt":
                        object tmpValue3 = ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("GrupimTreteButtonEdit1")).Value;
                        return vlera = tmpValue3 == null ? "" : tmpValue3.ToString();
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
                        int.TryParse(((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtInterval6")).Text, out interval6);
                        return vlera = interval6.ToString();
                    case "txtinterval7":
                        int interval7 = 0;
                        int.TryParse(((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtInterval7")).Text, out interval7);
                        return vlera = interval7.ToString();
                    case "filterpikeshitjefurnizmi":
                        if (RaportiEmerReal == "ndjekjaECiklitTeKonvertimeve")
                            return ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("pikeShFButtonEdit1")).Text;
                        break;
                    case "filterfurnitor":
                        if (RaportiEmerReal == "ndjekjaECiklitTeKonvertimeve")
                            return ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("btneKodKF1")).Text;
                        break;
                    case "filterklientfurnitor":
                        if (RaportiEmerReal == "Kartolina_ditelindjes_klientit")
                            return ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("btneKodKF1")).Text;
                        break;
                    case "filterdegeadministrative":
                        if (RaportiEmerReal == "ndjekjaECiklitTeKonvertimeve")
                            return ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("degeAdminButtonEdit1")).Text;
                        break;
                    case "filtermuajraportimi":
                        int muaji = Convert.ToInt32(((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbMuajRaportimi")).Value);
                        if (muaji == 0)
                            return vlera = "";
                        return vlera = String.Format("( {0} = {1} ) AND ", parametri.KolonaDb, muaji);
                    case "filtervitraportimi":
                        int viti = Convert.ToInt32(((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVitRaportimi")).Value);
                        if (viti == 0)
                        {
                            return vlera = "";
                        }
                        if (RaportiEmerReal == "deklarateTatimi")
                        {
                            return vlera = ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVitRaportimi")).Text;
                        }
                        else
                        {
                            return vlera = String.Format("( {0} = {1} ) AND ", parametri.KolonaDb, viti);
                        }

                    case "filterkategorishpenzimi":
                        return ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("btnKatShpenzimi1")).Text;
                    case "filternrdokkonvertuar":
                        if (RaportiEmerReal == "dokumentaKonvertuar")
                            return ((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtNrDokKonvertuar1")).Text;
                        break;

                    case "filtergrupimkf1":
                        object tmpVlKF1 = ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("btneGrupimiKF1")).Value;
                        return vlera = tmpVlKF1 == null ? "" : tmpVlKF1.ToString();
                        break;

                    case "filtergrupimkf2":
                        object tmpVlKF2 = ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("btneGrupimiKF2")).Value;
                        return vlera = tmpVlKF2 == null ? "" : tmpVlKF2.ToString();
                        break;

                    case "filtergrupimkf3":
                        object tmpVlKF3 = ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("btneGrupimiKF3")).Value;
                        return vlera = tmpVlKF3 == null ? "" : tmpVlKF3.ToString();
                        break;
                    case "filterfurnitor1":
                        return ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("DpshKlientFurnitorButtonEdit")).Text;
                        break;
                    case "filterkategorishpenzimi1":
                        return ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("DpshKategShpenzimiButtonEdit")).Text;
                        break;
                    case "filternumerllogarie1":
                        return ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("DpshNrLlogarieButtonEdit")).Text;
                        break;
                    case "filtermagazina1":
                        return ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("DpshMagazinaButtonEdit")).Text;
                        break;
                    case "filtershenimeshitje1":
                        return ((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("DpshShenimeTextBox")).Text;
                        break;
                    case "filternumerdokumenti1":
                        return ((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("DpshNrDokumentiTextBox")).Text;
                        break;
                    case "filteridklientFurnitor":
                      return vlera;
                        break;
                    case "filtermagazineskadence":
                        return ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("txtBtnMagazineSkadence")).Text;
                        break;
                    case "filtereic":
                        string eic = ((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtEIC")).Text.ToString();
                        if (eic != "")
                            return String.Format("( {0} =" + "'" + "{1}" + "'" + " ) AND ", parametri.KolonaDb, eic);
                        else
                            eic = eic;
                        break;
                    case "filterdetajim1receptura":
                        string filteri = ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("btnDetajimiReceptura1")).Text.ToString();
                        if (filteri != "")
                        {
                            clsDetajimArtikulli detajim1 = new clsDetajimArtikulli(filteri, idNdermarrje);
                            return String.Format("( {0} = {1}  ) AND ", parametri.KolonaDb, detajim1.IdDetajimArtikulli);
                        }
                        else
                            return "";
                        break;
                    case "filterdetajim2receptura":
                        string filteri2 = ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("btnDetajimiReceptura2")).Text.ToString();
                        if (filteri2 != "")
                        {
                            clsDetajimArtikulli detajim2 = new clsDetajimArtikulli(filteri2, idNdermarrje);
                            return String.Format("{0}",detajim2.IdDetajimArtikulli);
                        }
                        else
                        {
                            return "";
                        }
                        break;
                    case "filterdetajim1artikulli":
                        string filteri3 = ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("btnDetajimiProdukti1")).Text.ToString();
                        if (filteri3 != "")
                        {
                            clsDetajimArtikulli detajim3 = new clsDetajimArtikulli(filteri3, idNdermarrje);
                            return String.Format("( {0} = {1}  ) AND ", parametri.KolonaDb, detajim3.IdDetajimArtikulli);
                        }
                        else
                            return "";
                        break;
                    case "filterdetajim2artikulli":
                        string filteri4 = ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("btnDetajimiProdukti2")).Text.ToString();
                        if (filteri4 != "")
                        {
                            clsDetajimArtikulli detajim4 = new clsDetajimArtikulli(filteri4, idNdermarrje);
                            return String.Format("( {0} = {1}  ) AND ", parametri.KolonaDb, detajim4.IdDetajimArtikulli);
                        }
                        else
                            return "";
                        break;
                    case "filterdtdok":
                        System.Web.UI.Control uiKontroll = navBarFiltrat.Groups[0].FindControl("radDtDok");
                        ASPxRadioButtonList radioKontrolli = ((ASPxRadioButtonList)uiKontroll);
                        if (parametri.KolonaDb == "T_FATURAEINVOICE.RecDateTime")
                        {
                            if (radioKontrolli.SelectedItem.Value.ToString() == "Aktuale")
                            {
                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", parametri.KolonaDb, periudhaKontabel.FillimiPeriudha.ToShortDateString(), periudhaKontabel.MbarimiPeriudha.ToShortDateString());
                                break;
                            }
                            if (radioKontrolli.SelectedItem.Value.ToString() == "GjitheVitet")
                            {
                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", parametri.KolonaDb, Convert.ToDateTime("1900-01-01").ToShortDateString(), DateTime.MaxValue.ToShortDateString());
                                break;
                            }
                            else if (radioKontrolli.SelectedItem.Value.ToString() == "VitiUshtrimor")
                            {
                                clsNdermarrjeViti ndermviti = new clsNdermarrjeViti(idNderviti);
                                if (idNderviti != -1)
                                {
                                    vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", parametri.KolonaDb, ndermviti.NdermarrjeVitiFillim.ToShortDateString(), ndermviti.NdermarrjeVitiFund.ToShortDateString());
                                    break;
                                }
                            }
                            else
                            {
                                return String.Format(" {0}>=convert(datetime,'{1}',103) and {0}<=convert(datetime,'{2}',103) AND ", parametri.KolonaDb, ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDok")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDok")).Date.ToShortDateString());
                                break;
                            }

                        }
                        break;
                    case "filterstatuseinvoice":
                        string statusi = ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbStatusiEinvoice")).Text;
                        switch (statusi)
                        {
                            case "Aprovuar":
                                statusi = "ACCEPTED";
                                break;
                            case "Refuzuar":
                                statusi = "REFUSED";
                                break;
                            case "Derguar":
                                statusi = "DELIVERED";
                                break;
                            case "Te Gjitha":
                                statusi = "";
                                break;
                        }
                        if (statusi != "")
                            return String.Format("( {0} =" + "'" + "{1}" + "'" + " ) AND ", parametri.KolonaDb, statusi);
                        else
                            return "";
                        break;

                    default:
                        break;
                }
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
                    if(parametri.KolonaDb == "T_KOKAEKZEKUTIMPRODHIMI.NRDOK")
                    {

                    }
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
                                    parametriAktual = (((ASPxLabel)uiKontroll).Text).TrimEnd(':');
                                    continue;
                                #endregion

                                #region ASPxTextBox
                                case "ASPxTextBox":
                                    ASPxTextBox textBoxi = (ASPxTextBox)uiKontroll;
                                    if (kodKontrolli.Equals("txtNrSigurimesh") || kodKontrolli.Equals("txtArsyeja") || kodKontrolli.Equals("txtKujtIAdresohet") || kodKontrolli.Equals("txtSAPID") || kodKontrolli.Equals("txtDrejtuar") || kodKontrolli.Equals("txtNrLlogBankare"))
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
                                    if (kodKontrolli.ToLower().StartsWith("txttarga"))
                                    {
                                        vlera += String.Format("{0} like '%{1}%' ", kolonaFilter, textBoxi.Text);
                                        continue;
                                    }
                                    if (kodKontrolli.ToLower().StartsWith("txtshoferi"))
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
                                        vlera += String.Format("{0} like '%{1}' ", kolonaFilter, "-" + textBoxi.Text);
                                    }
                                    if (kodKontrolli.ToLower().StartsWith("txtemmbshoqeri"))
                                    {
                                        vlera += String.Format("{0} like '%{1}%' ", kolonaFilter, textBoxi.Text);
                                        continue;
                                    }
                                    if (kodKontrolli.ToLower().StartsWith("txtnrtel"))
                                    {
                                        vlera += String.Format("{0} like '%{1}%' ", kolonaFilter, textBoxi.Text);
                                        continue;
                                    }
                                    if (kodKontrolli.ToLower().StartsWith("txtcustomernumber"))
                                    {
                                        vlera += String.Format("{0} like '%{1}%' ", kolonaFilter, textBoxi.Text);
                                    }
                                    if (kodKontrolli.ToLower().StartsWith("txtvlereshitjep"))
                                    {
                                        vlera += String.Format("{0} > {1} ", kolonaFilter, textBoxi.Text);

                                    }
                                    if (kodKontrolli.ToLower().StartsWith("txtvlereshitjed"))
                                    {
                                        if (vlera == "")
                                            vlera += String.Format(" {0} < {1} ", kolonaFilter, textBoxi.Text);
                                        else
                                            vlera += String.Format("{2} {0} < {1} ", kolonaFilter, textBoxi.Text, " AND ");

                                    }
                                    if (kodKontrolli.Equals("txtNrDok1"))
                                    {
                                        if (RaportiEmerReal == "ndjekjaECiklitTeKonvertimeve")
                                            return textBoxi.Text;
                                    }
                                    if (kodKontrolli.Equals("txtSeriale"))
                                        return textBoxi.Text;
                                    if (kodKontrolli.Equals("txtBox_Airtime"))
                                        return String.Format("{0} = '{1}' AND", kolonaFilter, textBoxi.Text);
                                    if (kodKontrolli.ToLower().StartsWith("txttac"))
                                    {
                                        vlera += String.Format("{0} like '%{1}%' ", kolonaFilter, textBoxi.Text);
                                        continue;
                                    }
                                    if (kodKontrolli.ToLower().StartsWith("txtmarveshja"))
                                    {
                                        vlera += String.Format("{0} like '%{1}%' ", kolonaFilter, textBoxi.Text);
                                    }
                                    if (kodKontrolli.ToLower().StartsWith("txteic"))
                                    {
                                        vlera += String.Format("{0} like '%{1}%' ", kolonaFilter, textBoxi.Text);
                                    }
                                    if (kodKontrolli.ToLower().StartsWith("cmbstatusieinvoice"))
                                    {
                                        vlera += String.Format("{0} like '%{1}%' ", kolonaFilter, textBoxi.Text);
                                    }
                                    continue;

                                #endregion

                                #region ASPxComboBox
                                case "ASPxComboBox":
                                    ASPxComboBox combo = (ASPxComboBox)uiKontroll;
                                    if (combo.Text.Trim() == "")
                                        continue;
                                    if (kodKontrolli.ToLower().StartsWith("cmblidhesa") && !kodKontrolli.Equals("cmbLidhesaGrupimPKF") && !kodKontrolli.Equals("cmbLidhesaGrupimDKF") && !kodKontrolli.Equals("cmbLidhesaGrupimTKF") && !kodKontrolli.Equals("cmbLidhesaGrupimPArt") && !kodKontrolli.Equals("cmbLidhesaGrupimDArt")) // && !kodKontrolli.ToLower().StartsWith("cmbLidhesaPershk")
                                    {
                                        lidhes = combo.Value.ToString();
                                        continue;
                                    }
                                    if (kodKontrolli == "cmbGrupKosto")
                                    {
                                        return combo.SelectedItem.Value.ToString();
                                    }

                                    if (kodKontrolli.ToLower().StartsWith("cmblidhesa") && (parametri.Emri.ToLower().Equals("filtergrupimpkf") || parametri.Emri.ToLower().Equals("filtergrupimdkf") || parametri.Emri.ToLower().Equals("filtergrupimtkf"))) // && !kodKontrolli.ToLower().StartsWith("cmbLidhesaPershk")
                                    {
                                        lidhes = combo.Value.ToString();
                                        continue;
                                    }
                                    if (kodKontrolli.StartsWith("cmbGrupP") || kodKontrolli.StartsWith("cmbGrupD") || kodKontrolli.StartsWith("cmbGrupT"))
                                    {
                                        if (kodKontrolli.Contains("1"))
                                        {
                                            if (RaportiEmerReal == "ndjekjaECiklitTeKonvertimeve")
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


                                    if (kodKontrolli == "cmbNjesiKohe" || kodKontrolli == "cmbBuxhetet" || kodKontrolli == "cmbkrahasimKosto" || kodKontrolli == "cmbStatusRezervimi" || kodKontrolli == "cmbCmimMePaTVSH" || kodKontrolli == "cmbdogana")
                                    {
                                        return combo.Value.ToString();
                                    }

                                    if (kodKontrolli == "cmbKlientAktiv" || kodKontrolli == "cmbFilterKatAktive" || kodKontrolli == "cmbKontabilizuar")
                                    {
                                        switch (combo.SelectedIndex)
                                        {
                                            case 0:
                                                vlera = " ";
                                                break;
                                            case 1:
                                                vlera = String.Format("{0} = {1}", kolonaFilter, "1") + " AND";
                                                break;
                                            case 2:
                                                vlera = String.Format("{0} = {1}", kolonaFilter, "0") + " AND";
                                                break;
                                            default:
                                                vlera = " ";
                                                break;
                                        }
                                        return vlera;

                                    }
                                    if (kodKontrolli == "cmbMonedhaQK")
                                    {
                                        return combo.Value.ToString();
                                    }

                                    if (kodKontrolli == "cmbMonedhaKerkuar")
                                    {
                                        if (RaportiEmerReal == "gjendjaPermbledheseArkeBankeMonedheKerkuar")
                                            return combo.Value.ToString();
                                    }

                                    if (kodKontrolli == "cmbKonvertoNe")
                                    {
                                        return combo.Value.ToString();
                                    }
                                    if (kodKontrolli == "cmbLlojFaze")
                                    {
                                        return combo.SelectedIndex.ToString();
                                    }

                                    if (kodKontrolli == "cmbLlojCmimi")
                                    {
                                        vlera = combo.Value.ToString();
                                        return vlera;
                                    }

                                    if (kodKontrolli == "cmbStatusiDok")
                                    {
                                        vlera = combo.Value.ToString();
                                        return vlera;
                                    }

                                    if (kodKontrolli.ToString() == "cmbStatusShperndarje")
                                    {
                                        if (combo.Value.ToString() == "2")
                                            vlera = "  ";
                                        else if (combo.Value.ToString() == "0")
                                            vlera = "0";
                                        else vlera = "1";
                                        return vlera;

                                    }

                                    if (kodKontrolli.ToString() == "cmbArtikullIVjeter")
                                    {

                                        switch (combo.SelectedIndex)
                                        {
                                            case 0:
                                                vlera = String.Format("{0} = '{1}' AND ", kolonaFilter, "true");
                                                break;
                                            case 1:
                                                vlera = String.Format("{0} = '{1}' AND ", kolonaFilter, "false");
                                                break;
                                            default:
                                                vlera = " ";
                                                break;
                                        }
                                        return vlera;
                                    }

                                    if (kodKontrolli.ToString() == "cmbGrupoKlientSipas")
                                    {

                                        return combo.Value.ToString();

                                    }
                                    if (kodKontrolli.ToString() == "cmbGrupoArtikullSipas")
                                    {
                                        return combo.Value.ToString();
                                    }

                                    if (kodKontrolli.ToString() == "cmbGrupoSipas")
                                    {
                                        return combo.Value.ToString();

                                    }
                                    if (kodKontrolli.ToString() == "cmbGrupoSipasAgjenteve")
                                    {
                                        return combo.Value.ToString();

                                    }

                                    if (kodKontrolli.ToString().ToLower().StartsWith("cmbstatusriparimi"))
                                    {
                                        vlera = combo.Text;
                                        return vlera;
                                    }
                                    if (parametri.Emri.Equals("filterMuaji"))
                                    {
                                        if (RaportiEmerReal.EqualsAnyIgnoreCase("raportiBuxhetimit", "buxhetimeSipasUrdherPagesave", "buxhetimiSipasShpenzimeve", "kartelaKategoriShpenzimi", "ndryshimeBuxhetore"))
                                        {
                                            vlera = String.Format("{0} = ('{1}')", kolonaFilter, combo.Text);
                                            continue;
                                        }
                                        else if (RaportiEmerReal.EqualsAnyIgnoreCase("bonuse2MujoreKlientesh", "bonuse2MujoreKlienteshAutorizime", "deklarimNeFinance", "perAprovim", "kartelaEPages", "kartelaEPagesFormat2", "permbledhesSituacionShpenzimesh"))
                                            return combo.Value.ToString();

                                    }
                                    if (parametri.Emri.Equals("filterGrupArtP3"))
                                    {
                                        vlera = ((ASPxComboBox)navBarFiltrat.Groups[g].FindControl("cmbLidhesaGrupimPArt")).Text;
                                        if (vlera == "ose") vlera = "or";
                                        else if (vlera == "dhe") vlera = "and";
                                        return vlera;

                                    }
                                    if (parametri.Emri.Equals("filterGrupArtP1"))
                                    {
                                        //return ((ASPxComboBox)navBarFiltrat.Groups[g].FindControl("cmbVeprimi1GrupPArt")).Value.ToString();
                                        ASPxComboBox cBox = ((ASPxComboBox)navBarFiltrat.Groups[g].FindControl("cmbVeprimi1GrupPArt"));
                                        if (cBox.SelectedIndex != -1)
                                            return cBox.SelectedIndex.ToString();
                                        else
                                            return "1";


                                    }

                                    if (parametri.Emri.Equals("filterGrupArtP4"))
                                    {

                                        ASPxComboBox cBox = ((ASPxComboBox)navBarFiltrat.Groups[g].FindControl("cmbVeprimi2GrupPArt"));
                                        if (cBox.SelectedIndex != -1)
                                            return cBox.SelectedIndex.ToString();
                                        else
                                            return "";

                                    }

                                    if (parametri.Emri.Equals("filterGrupArtD3"))
                                    {
                                        vlera = ((ASPxComboBox)navBarFiltrat.Groups[g].FindControl("cmbLidhesaGrupimDArt")).Text;
                                        if (vlera == "ose") vlera = "or";
                                        else if (vlera == "dhe") vlera = "and";
                                        return vlera;

                                    }
                                    if (parametri.Emri.Equals("filterGrupArtD1"))
                                    {
                                        //return ((ASPxComboBox)navBarFiltrat.Groups[g].FindControl("cmbVeprimi1GrupDArt")).Value.ToString();
                                        ASPxComboBox cBox = ((ASPxComboBox)navBarFiltrat.Groups[g].FindControl("cmbVeprimi1GrupDArt"));
                                        if (cBox.SelectedIndex != -1)
                                            return cBox.SelectedIndex.ToString();
                                        else
                                            return "1";
                                    }
                                    if (parametri.Emri.Equals("filterGrupArtD4"))
                                    {

                                        ASPxComboBox cBox = ((ASPxComboBox)navBarFiltrat.Groups[g].FindControl("cmbVeprimi2GrupDArt"));
                                        if (cBox.SelectedIndex != -1)
                                            return cBox.SelectedIndex.ToString();
                                        else
                                            return "";

                                    }

                                    if (kodKontrolli.ToLower().StartsWith("cmbvep"))
                                    {
                                        if (kodKontrolli.Contains("1") && (parametri.Emri.ToLower().Equals("filtergrupimpkf") || parametri.Emri.ToLower().Equals("filtergrupimdkf") || parametri.Emri.ToLower().Equals("filtergrupimtkf")))
                                        {

                                            veprimiPara1 = combo.Value.ToString();
                                            continue;
                                        }

                                        if (kodKontrolli.Contains("1") && !kodKontrolli.Equals("cmbVeprimi1GrupPKF") && !kodKontrolli.Equals("cmbVeprimi1GrupDKF") && !kodKontrolli.Equals("cmbVeprimi1GrupTKF") && !kodKontrolli.Equals("cmbVeprimi1GrupPArt") && !kodKontrolli.Equals("cmbVeprimi1GrupDArt"))
                                        {

                                            veprimiPara1 = combo.Value.ToString();
                                            continue;
                                        }
                                        if (kodKontrolli.Contains("2") && !kodKontrolli.Equals("cmbVeprimi2GrupPKF") && !kodKontrolli.Equals("cmbVeprimi2GrupDKF") && !kodKontrolli.Equals("cmbVeprimi2GrupTKF") && !kodKontrolli.Equals("cmbVeprimi2GrupPArt") && !kodKontrolli.Equals("cmbVeprimi2GrupDArt"))
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
                                            if (kodKontrolli.ToLower().Contains("faturuar") || kodKontrolli.ToLower().StartsWith("cmbpajisje1") || kodKontrolli.ToLower().Contains("prodhuar") || kodKontrolli.ToLower().Contains("perfunduar"))
                                            {
                                                vlere1 = combo.Text.ToString();
                                            }
                                            else
                                                vlere1 = combo.Value.ToString();
                                            continue;
                                        }
                                        if (kodKontrolli.Contains("2"))
                                        {
                                            if (kodKontrolli.ToLower().Contains("faturuar") || kodKontrolli.ToLower().StartsWith("cmbpajisje2") || kodKontrolli.ToLower().Contains("prodhuar") || kodKontrolli.ToLower().Contains("perfunduar"))
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
                                            if (RaportiEmerReal.EqualsAnyIgnoreCase("aseteJashtePerdorimi", "blerjeMujore", "permbledheseGrupetNivelRaportues", "gjendjaAseteveSipasNdermarrjeve", "kartelaPermbledheseArtikullitSipasNdermarrjeve", "permbledhesAseteNivelRaportues", "regjistriAktiveve", "aseteJashtePerdorimiRezervaRivleresimi"))
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
                                        case "cmbStatusMarreveshje":
                                            vlera = combo.Value.ToString();
                                            return vlera;
                                        case "cmbKategorizimArtikuj":
                                            vlera = combo.Value.ToString();
                                            return vlera;
                                        case "cmbLikuiduar":
                                            vlera = combo.Value.ToString();
                                            return vlera;
                                        case "cmbLlojPorosie":
                                            vlera = combo.Value.ToString();
                                            return vlera;
                                        case "cmbArtCmim":
                                            vlera = combo.Value.ToString();
                                            return vlera;
                                        case "cmbCmimeArtikulli":
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

                                        case "cmbEmertimLlog":
                                            vlera = combo.Value.ToString();
                                            return vlera;
                                        case "cmbPasqyra":
                                            vlera = combo.Value.ToString();
                                            return vlera;
                                        case "cmbGjendja":
                                            vlera = combo.Value.ToString();
                                            return vlera;
                                        case "cmbTipGrafiku":
                                            vlera = combo.Value.ToString();
                                            return vlera;
                                        case "cmbStatus":
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
                                        case "cmbLlojiElem":
                                            vlera = String.Format("{0} = {1}", kolonaFilter, Convert.ToInt32(combo.Value.ToString()));
                                            return vlera + " AND ";

                                        case "cmbStatusElem":
                                            vlera = String.Format("{0} = {1}", kolonaFilter, Convert.ToInt32(combo.Value.ToString()));
                                            return vlera + " AND ";

                                        case "cmbLlojSubjekti":
                                            vlera = combo.Value.ToString();
                                            return vlera;
                                        case "cmbLlojSubjektiKF":
                                            vlera = String.Format("{0} = {1}", kolonaFilter, Convert.ToInt32(combo.Value.ToString()));
                                            return vlera + " AND "; 
                                        case "cmbPaguar":
                                            vlera = combo.Value.ToString();
                                            return vlera;
                                        case "cmbViti":
                                            vlera = combo.Text;
                                            return vlera;
                
                                     default:
                                            vlera = String.Format("{0} = {1}", kolonaFilter, combo.Value);
                                            if (kodKontrolli.Contains("1") )
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
                                    if (kodKontrolli == "dteDtMaturimi" || kodKontrolli == "dateDateKontrate" || kodKontrolli == "dataSkadence")
                                    {
                                        vlera = dateEdit.Text.Trim();
                                        continue;
                                    }
                                    if (kodKontrolli.ToLower().StartsWith("txtngaorekrijimi"))
                                    {
                                        vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaOreKrijimi")).Date.ToLongTimeString();
                                    }
                                    if (kodKontrolli.ToLower().StartsWith("txtderiorekrijimi"))
                                    {
                                        vlera += " " + ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriOreKrijimi")).Date.ToLongTimeString();
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

                                    if (kodKontrolli.Equals("GrupeBankeButtonEdit1"))
                                    {
                                        return buttonEdit.Text;
                                    }

                                    if (kodKontrolli.Equals("btnJobTitle"))
                                    {
                                        return buttonEdit.Text.Split(';')[0];
                                    }
                                    if (parametri.Emri.Equals("filterGrupArtP2"))
                                    {
                                        vlera = ((ASPxButtonEdit)navBarFiltrat.Groups[g].FindControl("btneGrupPArt1")).Text;
                                        if (vlera == "")
                                            return "";
                                        else
                                            return String.Format("'{0}'", vlera.Replace(",", "','"));

                                    }
                                    if (parametri.Emri.Equals("filterGrupArtP5"))
                                    {
                                        ASPxButtonEdit bEdit = ((ASPxButtonEdit)navBarFiltrat.Groups[g].FindControl("btneGrupPArt2"));
                                        if (bEdit.IsEnabled())
                                        {
                                            vlera = bEdit.Text.ToString();
                                            if (vlera == "")
                                                return "";
                                            else
                                                return String.Format("'{0}'", vlera.Replace(",", "','"));
                                        }
                                        else
                                            return "";

                                    }
                                    if (parametri.Emri.Equals("filtroKapitull"))
                                    {
                                        ASPxButtonEdit bEdit = ((ASPxButtonEdit)navBarFiltrat.Groups[g].FindControl("btneKapitulli"));

                                        vlera = bEdit.Text.ToString();
                                        if (vlera == "")
                                            return "";
                                        else
                                            return $" {parametri.KolonaDb} IN ('{vlera.Replace(",", "','")}') AND ";


                                    }
                                    if (parametri.Emri.Equals("filtroProgram"))
                                    {
                                        ASPxButtonEdit bEdit = ((ASPxButtonEdit)navBarFiltrat.Groups[g].FindControl("btneProgrami"));

                                        vlera = bEdit.Text.ToString();
                                        if (vlera == "")
                                            return "";
                                        else
                                            return $" {parametri.KolonaDb} IN ('{vlera.Replace(",", "','")}') AND ";


                                    }
                                    if (parametri.Emri.Equals("filterGrupArtD2"))
                                    {
                                        // return ((ASPxButtonEdit)navBarFiltrat.Groups[g].FindControl("btneGrupDArt1")).Text.ToString();
                                        vlera = ((ASPxButtonEdit)navBarFiltrat.Groups[g].FindControl("btneGrupDArt1")).Text;
                                        if (vlera == "")
                                            return "";
                                        else
                                            return String.Format("'{0}'", vlera.Replace(",", "','"));

                                    }
                                    if (parametri.Emri.Equals("filterGrupArtD5"))
                                    {
                                        ASPxButtonEdit bEdit = ((ASPxButtonEdit)navBarFiltrat.Groups[g].FindControl("btneGrupDArt2"));
                                        if (bEdit.IsEnabled())
                                        {
                                            vlera = bEdit.Text.ToString();
                                            if (vlera == "")
                                                return "";
                                            else
                                                return String.Format("'{0}'", vlera.Replace(",", "','"));
                                        }
                                        else
                                            return "";


                                    }
                                    if (buttonEdit.Text.Trim() == "")
                                    {
                                        if ((kodKontrolli == "btneGrupPKF1") || (kodKontrolli == "btneGrupDKF1") || (kodKontrolli == "btneGrupTKF1"))
                                            return vlera = "";

                                        else continue;
                                    }

                                    string parametriEmri = parametri.Emri.ToLower();
                                    if (kodKontrolli.Contains("1") && (parametriEmri.Equals("filtergrupimpkf") || parametriEmri.Equals("filtergrupimdkf") || parametriEmri.Equals("filtergrupimtkf") || parametriEmri.Equals("filterDepartamentiPunonjesit") || parametriEmri.Equals("filterNenDepartamentiPunonjesit") || parametriEmri.Equals("filterKodDep") || parametriEmri.Equals("filterKodNenDep")))
                                    {
                                        vlere1 = buttonEdit.Value.ToString();
                                        continue;
                                    }


                                    if (kodKontrolli.Contains("2") && !kodKontrolli.Equals("btneGrupPKF2") && !kodKontrolli.Equals("btneGrupDKF2") && !kodKontrolli.Equals("btneGrupTKF2") && !kodKontrolli.Equals("btneGrupDArt2") && !kodKontrolli.Equals("btneGrupPArt2"))
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
                                        vlera = ((ASPxButtonEdit)navBarFiltrat.Groups[g].FindControl("btneGrupPKF1")).Text;
                                        return String.Format("{0} in ({1}) ", kolonaFilter, String.Format("'{0}'", vlera.Replace(",", "','")));
                                    }
                                    if (parametri.Emri.Equals("filterGrupKFP3"))
                                    {
                                        vlera = ((ASPxButtonEdit)navBarFiltrat.Groups[g].FindControl("btneGrupPKF2")).Text;
                                        return String.Format("{0} in ({1}) ", kolonaFilter, String.Format("'{0}'", vlera.Replace(",", "','")));
                                    }
                                    if (parametri.Emri.Equals("filterGrupKFD1"))
                                    {
                                        vlera = ((ASPxButtonEdit)navBarFiltrat.Groups[g].FindControl("btneGrupDKF1")).Text;
                                        return String.Format("{0} in ({1}) ", kolonaFilter, String.Format("'{0}'", vlera.Replace(",", "','")));
                                    }
                                    if (parametri.Emri.Equals("filterGrupKFD3"))
                                    {
                                        vlera = ((ASPxButtonEdit)navBarFiltrat.Groups[g].FindControl("btneGrupDKF2")).Text;
                                        return String.Format("{0} in ({1}) ", kolonaFilter, String.Format("'{0}'", vlera.Replace(",", "','")));
                                    }
                                    if (parametri.Emri.Equals("filterGrupKFT1"))
                                    {
                                        vlera = ((ASPxButtonEdit)navBarFiltrat.Groups[g].FindControl("btneGrupTKF1")).Text;
                                        return String.Format("{0} in ({1}) ", kolonaFilter, String.Format("'{0}'", vlera.Replace(",", "','")));
                                    }

                                    if (parametri.Emri.Equals("filterGrupKFT3"))
                                    {
                                        vlera = ((ASPxButtonEdit)navBarFiltrat.Groups[g].FindControl("btneGrupTKF2")).Text;
                                        return String.Format("{0} in ({1}) ", kolonaFilter, String.Format("'{0}'", vlera.Replace(",", "','")));
                                    }

                                    if (kodKontrolli == "txtBtnMagazina" || kodKontrolli == "txtBtnKartela" || kodKontrolli == "txtBtnKatSeriali" || kodKontrolli == "txtBtnLlojDok" || kodKontrolli == "btnePerdorues" || kodKontrolli == "txtBtnKrijuesi" || kodKontrolli == "btnAgjentShitje" || kodKontrolli == "btnNivelCmimi" || kodKontrolli == "btneAuto" || kodKontrolli == "btneKompania" || kodKontrolli == "btnSeriali" || kodKontrolli == "btnBurimi" || kodKontrolli == "btnAktiviteti" || kodKontrolli == "" || kodKontrolli == "btnNivelZbritje" || kodKontrolli == "txtBtnMagazinaPaLidhese" || kodKontrolli == "degeAdminPaLidheseButtonEdit" || kodKontrolli == "btnStatusPerdorues" || kodKontrolli == "btnModPerdorues")
                                    {
                                        if (RaportiEmerReal == "permbledheseRezultatiQendraKosto" || RaportiEmerReal == "RezultatiQendraveTeKostosMeZera" || RaportiEmerReal == "gjendjaPermbledhurArkesNivelRaportues" || RaportiEmerReal == "gjendjaPermbledhurBankesNivelRaportues"
                                            || RaportiEmerReal == "permbledheseGrupetNivelRaportues" || RaportiEmerReal == "gjendjaAseteveSipasNdermarrjeve" || RaportiEmerReal == "kartelaPermbledheseArtikullitSipasNdermarrjeve"
                                            || RaportiEmerReal == "gjendjaMagazinesNivelRaportues" || RaportiEmerReal == "situacionPermbledhesKlienteNivelRaportues" || RaportiEmerReal == "situacionPermbledhesFurnitorNivelRaportues"
                                            || RaportiEmerReal == "permbledhesAseteNivelRaportues" || RaportiEmerReal == "bilanciEnergjitikPermbledhes" || RaportiEmerReal == "ditariTotalBankaRaportues")
                                            return vlera = buttonEdit.Text;
                                        if (RaportiEmerReal == "bilanciQendraKosto" || RaportiEmerReal == "cashFlowQendraKosto" || RaportiEmerReal == "teArdhuraShpenzimeQendraKosto")
                                            vlera = buttonEdit.Value.ToString();
                                        else
                                            vlera = String.Format("{0} = '{1}'", kolonaFilter, buttonEdit.Value.ToString());
                                        continue;
                                    }
                                    if (kodKontrolli.Contains("1") && !kodKontrolli.Equals("btneGrupPKF1") && !kodKontrolli.Equals("btneGrupDKF1") && !kodKontrolli.Equals("btneGrupPArt1") && !kodKontrolli.Equals("btneGrupDArt1"))
                                    {
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
                                    if (kodKontrolli == "cbShperndarjeDhuratash")
                                    {
                                        if (check.Checked)
                                            vlera = "1";
                                        else
                                            vlera = "0";
                                        return vlera;
                                    }

                                    if (kodKontrolli == "cbShfaqGrup")
                                    {
                                        if (check.Checked)
                                            vlera = "1";
                                        else
                                            vlera = "0";
                                        return vlera;
                                    }
                                    if (kodKontrolli == "cbNenprodukte")
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
                                    if (kodKontrolli == "cbSasiaPakonvertuar")
                                    {
                                        if (check.Checked)
                                            vlera = rm.GetString("cmbFilterPo", ci);
                                        else
                                            vlera = rm.GetString("cmbFilterJo", ci);
                                        return vlera;
                                    }
                                    if (kodKontrolli == "cbSasiaPerTuPorositur")
                                    {
                                        if (check.Checked)
                                            vlera = rm.GetString("cmbFilterPo", ci);
                                        else
                                            vlera = rm.GetString("cmbFilterJo", ci);
                                        return vlera;
                                    }
                                    if (kodKontrolli == "cbgrupoKatShpenzim" || kodKontrolli == "cbBuxhetSipasKapitujve" || kodKontrolli == "cbshitjeKomisionZero")
                                    {
                                        if (check.Checked)
                                            vlera = rm.GetString("cmbFilterPo", ci);
                                        else
                                            vlera = rm.GetString("cmbFilterJo", ci);
                                        return vlera;
                                    }
                                    if (kodKontrolli == "cbAzhornim")
                                    {
                                        if (check.Checked)
                                            vlera = rm.GetString("cmbFilterPo", ci);
                                        else
                                            vlera = rm.GetString("cmbFilterJo", ci);
                                        return vlera;
                                    }
                                    if (kodKontrolli == "checkLlogariSintetike")
                                    {
                                        if (check.Checked)
                                            vlera = rm.GetString("cmbFilterPo", ci);
                                        else
                                            vlera = rm.GetString("cmbFilterJo", ci);
                                        return vlera;
                                    }

                                    if (kodKontrolli == "cmbShfaqnendep")
                                    {
                                        if (check.Checked)
                                            vlera = rm.GetString("cmbFilterPo", ci);
                                        else
                                            vlera = rm.GetString("cmbFilterJo", ci);
                                        return vlera;
                                    }
                                    if (kodKontrolli == "cbGrupimSipasKF" || kodKontrolli == "cbMonedheKF" || kodKontrolli == "checkAfishoPagen" || kodKontrolli == "cbMonedheLl" || kodKontrolli == "cbKonvertuarNgaKontrata" || kodKontrolli == "cbkthime")
                                    {
                                        if (check.Checked)
                                            vlera = rm.GetString("cmbFilterPo", ci);
                                        else
                                            vlera = rm.GetString("cmbFilterJo", ci);
                                        return vlera;

                                    }
                                    if (kodKontrolli == "cbMonedheLl" || kodKontrolli == "cbGrupoSipasGrupim1")
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
                                    if (kodKontrolli == "cbmagvartese")
                                    {
                                        if (check.Checked)
                                            vlera = "1";
                                        else
                                            vlera = "0";
                                        return vlera;
                                    }
                                    if (kodKontrolli == "cbGrupimQK")
                                    {
                                        if (check.Checked)
                                            vlera = "1";
                                        else
                                            vlera = "0";
                                        return vlera;
                                    }
                                    break;
                                #endregion

                                #region ASPxRadioButtonList
                                case "ASPxRadioButtonList":
                                    ASPxRadioButtonList radioKontrolli = ((ASPxRadioButtonList)uiKontroll);
                                    if (radioKontrolli.SelectedItem.Value.ToString() == "")
                                        continue;
                                    if (radioKontrolli.ID.StartsWith("radDtTakimi"))
                                    {
                                        if (radioKontrolli.SelectedItem.Value.ToString() == "Aktuale")
                                        {
                                            vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, periudhaKontabel.FillimiPeriudha.ToShortDateString(), periudhaKontabel.MbarimiPeriudha.ToShortDateString());
                                            continue;
                                        }

                                        if (radioKontrolli.SelectedItem.Value.ToString() == "Periudha")
                                        {


                                            if (parametri.Emri.Contains("1"))
                                            {
                                                return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDttakimi")).Date.ToShortDateString();
                                            }
                                            if (parametri.Emri.Contains("2"))
                                            {
                                                return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDttakimi")).Date.ToShortDateString();
                                            }
                                            vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDttakimi")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDttakimi")).Date.ToShortDateString());
                                            continue;
                                        }
                                        if (radioKontrolli.SelectedItem.Value.ToString() == "Ditore")
                                        {
                                            vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"));
                                            continue;
                                        }
                                        if (radioKontrolli.SelectedItem.Value.ToString() == "Javore")
                                        {
                                            vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"));
                                            continue;
                                        }
                                        if (radioKontrolli.SelectedItem.Value.ToString() == "Vit ushtrimor")
                                        {
                                            clsNdermarrjeViti ndermviti = new clsNdermarrjeViti(idNderviti);
                                            if (idNderviti != -1)
                                            {
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ndermviti.NdermarrjeVitiFillim.ToShortDateString(), ndermviti.NdermarrjeVitiFund.ToShortDateString());
                                                continue;
                                            }
                                        }
                 
                                    }
                                    if (radioKontrolli.ID.StartsWith("radDtDok") || radioKontrolli.ID.StartsWith("radDtKrijimi") || radioKontrolli.ID.StartsWith("radDtRegj") 
                                        || radioKontrolli.ID.StartsWith("radDtDokAfatKohor") || radioKontrolli.ID.StartsWith("radPeriudheMaturimi") 
                                        || radioKontrolli.ID.StartsWith("radPeriudheFillimi") || radioKontrolli.ID.StartsWith("radPeriudheMbarimi") 
                                        || radioKontrolli.ID.StartsWith("radDtPlanifikimi") || radioKontrolli.ID.StartsWith("radDtProdhimi") 
                                        || radioKontrolli.ID.StartsWith("radDtKrijimiAqtSerial") || radioKontrolli.ID.StartsWith("radDtPorosie") 
                                        || radioKontrolli.ID.StartsWith("radDtDtFillimi") || radioKontrolli.ID.StartsWith("radDtMbarimi") 
                                        || radioKontrolli.ID.StartsWith("DtDokFillimiVod") || radioKontrolli.ID.StartsWith("DtDokLargimiVod") || radioKontrolli.ID.StartsWith("radDateLidhes") || radioKontrolli.ID.StartsWith("radDtSkadence"))
                                    {
                                        if (radioKontrolli.SelectedItem.Value.ToString() == "Aktuale")
                                        {
                                            if (periudhaKontabel != null)
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = periudhaKontabel.FillimiPeriudha.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = periudhaKontabel.MbarimiPeriudha.ToShortDateString();
                                                }
                                                if (parametri.Emri.ToLower().Equals("filterdtdokkrahasuesfillim"))
                                                {
                                                    return vlera = periudhaKontabel.FillimiPeriudha.AddYears(-1).ToShortDateString();
                                                }

                                                if (parametri.Emri.ToLower().Equals("filterdtdokkrahasuesmbarim"))
                                                {
                                                    return vlera = periudhaKontabel.MbarimiPeriudha.AddYears(-1).ToShortDateString();
                                                }

                                                if (parametri.Emri.ToLower().Equals("filterdtkrijimiseriali"))
                                                {
                                                    vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, periudhaKontabel.FillimiPeriudha.ToShortDateString(), periudhaKontabel.MbarimiPeriudha.ToShortDateString());
                                                    continue;
                                                }
                                                if (parametri.Emri.ToLower().Equals("filterdtkrijimi"))
                                                {
                                                    vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, periudhaKontabel.FillimiPeriudha.ToShortDateString(), periudhaKontabel.MbarimiPeriudha.ToShortDateString());
                                                    continue;
                                                }

                                                if (parametri.Emri.ToLower().Equals("filterdtdokkrahasues"))
                                                {
                                                    vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, periudhaKontabel.FillimiPeriudha.AddYears(-1).ToShortDateString(), periudhaKontabel.MbarimiPeriudha.AddYears(-1).ToShortDateString());
                                                    continue;
                                                }
                                                if (parametri.Emri.ToLower().Equals("filterdtdokkrahasuesqk"))
                                                {
                                                    vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, periudhaKontabel.FillimiPeriudha.AddYears(-1).ToShortDateString(), periudhaKontabel.MbarimiPeriudha.AddYears(-1).ToShortDateString());
                                                    continue;
                                                }
                                                if (parametri.Emri.ToLower().Equals("filterdtdokplanifikimi") || parametri.Emri.ToLower().Equals("filterdtdokprodhimi"))
                                                {
                                                    vlera = String.Format("( ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) ) or {0} IS NULL )", kolonaFilter, periudhaKontabel.FillimiPeriudha.ToShortDateString(), periudhaKontabel.MbarimiPeriudha.ToShortDateString());
                                                    continue;
                                                }
                                                if (parametri.Emri.ToLower().Equals("filterdatedoklidhes"))
                                                {
                                                    vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, periudhaKontabel.FillimiPeriudha.ToShortDateString(), periudhaKontabel.MbarimiPeriudha.ToShortDateString());
                                                    continue;
                                                }

                                                if (parametri.Emri.ToLower().StartsWith("filterdt"))
                                                {
                                                    vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, periudhaKontabel.FillimiPeriudha.ToShortDateString(), periudhaKontabel.MbarimiPeriudha.ToShortDateString());
                                                    continue;
                                                }

                                                if (parametri.Emri.ToLower().Equals("filterdtskadence"))
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

                                                if (parametri.Emri.ToLower().Equals("filterdtdokkrahasuesfillim"))
                                                {
                                                    return vlera = ndermviti.NdermarrjeVitiFillim.AddYears(-1).ToShortDateString();
                                                }
                                                if (parametri.Emri.ToLower().Equals("filterdtdokkrahasuesmbarim"))
                                                {
                                                    return vlera = ndermviti.NdermarrjeVitiFund.AddYears(-1).ToShortDateString();
                                                }

                                                if (parametri.Emri.ToLower().Equals("filterdtkrijimiseriali"))
                                                {
                                                    vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ndermviti.NdermarrjeVitiFillim.ToShortDateString(), ndermviti.NdermarrjeVitiFund.ToShortDateString());
                                                    continue;
                                                }
                                                if (parametri.Emri.ToLower().Equals("filterdtkrijimi"))
                                                {
                                                    vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ndermviti.NdermarrjeVitiFillim.ToShortDateString(), ndermviti.NdermarrjeVitiFund.ToShortDateString());
                                                    continue;
                                                }
                                                if (parametri.Emri.ToLower().Equals("filterdtdokkrahasues"))
                                                {
                                                    vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) ) ", kolonaFilter, ndermviti.NdermarrjeVitiFillim.AddYears(-1).ToShortDateString(), ndermviti.NdermarrjeVitiFund.AddYears(-1).ToShortDateString());
                                                    continue;
                                                }
                                                if (parametri.Emri.ToLower().Equals("filterdtdokkrahasuesqk"))
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
                                                if (parametri.Emri.ToLower().Equals("filterdatedoklidhes"))
                                                {
                                                    vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ndermviti.NdermarrjeVitiFillim.ToShortDateString(), ndermviti.NdermarrjeVitiFund.ToShortDateString());
                                                    continue;
                                                }

                                                if (parametri.Emri.ToLower().Equals("filterdtskadence"))
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
                                            if (parametri.Emri.ToLower().Equals("filterdtdokkrahasuesfillim"))
                                            {
                                                return vlera = Convert.ToDateTime("1900-01-01").ToShortDateString();
                                            }
                                            if (parametri.Emri.ToLower().Equals("filterdtdokkrahasuesmbarim"))
                                            {
                                                return vlera = DateTime.MaxValue.ToShortDateString();
                                            }

                                            if (parametri.Emri.ToLower().Equals("filterdtdokkrahasues"))
                                            {
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) ) ", kolonaFilter, Convert.ToDateTime("1900-01-01").ToShortDateString(), DateTime.MaxValue.ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().Equals("filterdtdokkrahasuesqk"))
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
                                            if (parametri.Emri.ToLower().Equals("filterdatedoklidhes"))
                                            {
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, Convert.ToDateTime("1900-01-01").ToShortDateString(), DateTime.MaxValue.ToShortDateString());
                                                continue;
                                            }

                                            if (parametri.Emri.ToLower().Equals("filterdtskadence"))
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
                                            if (parametri.Emri.ToLower().Equals("filterdtkrijimi"))
                                            {
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaKrijimi")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriKrijimi")).Date.ToShortDateString());
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

                                            if (parametri.Emri.ToLower().Equals("filterdtdokkrahasuesqk"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDtKrahasuesQK")).Date.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDtKrahasuesQK")).Date.ToShortDateString();
                                                }
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDtKrahasuesQK")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDtKrahasuesQK")).Date.ToShortDateString());
                                                continue;
                                            }

                                            if (parametri.Emri.ToLower().Equals("filterdtdokkrahasuesfillim"))
                                            {
                                                return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDtKrahasues")).Date.ToShortDateString();
                                            }

                                            if (parametri.Emri.ToLower().Equals("filterdtdokkrahasuesmbarim"))
                                            {
                                                return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDtKrahasues")).Date.ToShortDateString();
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
                                            if (parametri.Emri.ToLower().StartsWith("filterdtdokshitjerap"))
                                            {
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokShitje")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokShitje")).Date.ToShortDateString());
                                                continue;
                                            }
                                            if ((parametri.Emri.ToLower().StartsWith("filterdtdok") || (parametri.Emri.ToLower().StartsWith("filterdtkonvertopike"))) && !(parametri.Emri.ToLower().StartsWith("filterdtdokqenderkosto")))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDok")).Date.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDok")).Date.ToShortDateString();
                                                }


                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDok")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDok")).Date.ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().StartsWith("filterdtkrijimi"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaKrijimi")).Date.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriKrijimi")).Date.ToShortDateString();
                                                }
                                                vlera = String.Format(" (convert(date,{0},103)>=convert(datetime,'{1}',103) and convert(date,{0},103)<=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaKrijimi")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriKrijimi")).Date.ToShortDateString());
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
                                            if (parametri.Emri.ToLower().StartsWith("filterdtfillimivod"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokFillimiVod")).Date.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokFillimiVod")).Date.ToShortDateString();
                                                }
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokFillimiVod")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokFillimiVod")).Date.ToShortDateString());
                                                continue;
                                            }

                                            if (parametri.Emri.ToLower().StartsWith("filterdtlargimivod"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokLargimiVod")).Date.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokLargimiVod")).Date.ToShortDateString();
                                                }
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokLargimiVod")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokLargimiVod")).Date.ToShortDateString());
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

                                            if (parametri.Emri.ToLower().StartsWith("filterdtperiudhematurimi") || parametri.Emri.ToLower().StartsWith("filterdtmaturimi"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaPeriudheMaturimi")).Date.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriPeriudheMaturimi")).Date.ToShortDateString();
                                                }
                                                vlera = String.Format("(T_KOKASHITJE.DTMATURIMI is null or ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) ))", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaPeriudheMaturimi")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriPeriudheMaturimi")).Date.ToShortDateString());
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

                                            if (parametri.Emri.ToLower().StartsWith("filterdatedoklidhes"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokDateLidhes")).Date.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokDateLidhes")).Date.ToShortDateString();
                                                }
                                                vlera = String.Format(" ({0}>=convert(datetime,'{1}',103) and {0} <=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokDateLidhes")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokDateLidhes")).Date.ToShortDateString());
                                                continue;
                                            }

                                            if (parametri.Emri.ToLower().StartsWith("filterdtskadence"))
                                            {
                                                if (parametri.Emri.Contains("1"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaSkadence")).Date.ToShortDateString();
                                                }
                                                if (parametri.Emri.Contains("2"))
                                                {
                                                    return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriSkadence")).Date.ToShortDateString();
                                                }
                                                vlera = String.Format(" (convert(date,{0},103)>=convert(datetime,'{1}',103) and convert(date,{0},103)<=convert(datetime,'{2}',103) )", kolonaFilter, ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaSkadence")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriSkadence")).Date.ToShortDateString());
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
                    if ((parametri.Emri.ToLower() != "filterdateskadence") && (!vlera.StartsWith("KODIAGJENTSHITJE")) && (!vlera.StartsWith("NRLLOGARI") && (!parametri.Emri.ToLower().StartsWith("filterqk")) && (!RaportiEmerReal.ContainsAnyIgnoreCase("bilanciQendraKosto", "cashFlowQendraKosto", "teArdhuraShpenzimeQendraKosto", "KontrolliTeritorit", "RasteKultivimi", "MonitorimiAjror", "ShkresaDheInformacion"))))
                        vlera = String.Format("( {0} ) AND ", vlera);
                }
                else
                    vlera = vlera.Trim();

                return vlera.Replace("+@salt+", System.Web.Configuration.WebConfigurationManager.AppSettings["salt"]);

            }
            catch (Exception ex)
            {
                string mesazhi = "Ju lutem plotësoni saktë filtrin: " + parametriAktual;
                ImbLogger.Error(ex.Message);
                throw new DbCore.MyException(mesazhi);
            }
        }

        /// <summary>
        /// nderton parametrin e sql-se dhe e kontrollon per vlera te pa lejuara
        /// </summary>
        /// <param name="parametri"></param>
        /// <returns></returns>
        private string ktheVlerenEParametrit(clsParameter parametri, int idNdermarrje, clsPeriudhaKontabel periudhaKontabel, int idNderviti, int idPerdorues, colKontrolle oColKontrolle)
        {
            string vlera = ktheVlereParametri(parametri, idNdermarrje, periudhaKontabel, idNderviti, idPerdorues, oColKontrolle);
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
            return styleNamePrefix + styleName + styleNamePostfix;
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
                styleSheet.FileName = pathStyle + ktheStyleSheet(styleName);
                styleSheet.SaveToFile(pathStyle + styleNamePrefix + styleNameDefault + styleNamePostfix);
                return true;
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex.Message);
                return false;
            }
        }
                

        private SqlParameter[] krijoParametratPerSql(int idSp, int idNdermarrje, clsPeriudhaKontabel periudhaKontabel, int idNderViti, int idPerdorues, string guidString)
        {
            SqlParameter[] sqlParam = krijoParametratSql(idSp, idNdermarrje, periudhaKontabel, idNderViti, idPerdorues);
            DbCore.mySessionObjects.ruajParametraRaporti(Session, sqlParam, guidString);
            return sqlParam;
        }
        private colParameter krijoParametratPerTuShfaqurNeRaport(int idSp, int idNdermarrje, clsPeriudhaKontabel periudhaKontabel, int idNderViti, int idPerdorues, string guidString, bool kaSubRaport)
        {
            colParameter sqlParamShfaqRaport = krijoParametratShfaqRaporti(idSp, idNdermarrje, periudhaKontabel, idNderViti, ci);
            if (kaSubRaport) //konfigurimeSubRaporti duhet thirrur ketu vetem nqs raporti i hapur ka subraport.
            {
                konfigurimeSubRaporti(idNdermarrje, periudhaKontabel, idNderViti, idPerdorues, guidString);
            }
            return sqlParamShfaqRaport;
        }
        protected void afisho(int idRaporti, int idNdermarrje, int idViti, int idNderViti, clsPeriudhaKontabel periudhaKontabel, int idPerdorues, bool kaSubRaport, String guidString)
        {
            KontrolloTeDrejtaRaporti(idRaporti, idPerdorues, idNdermarrje, idViti);

            var idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            string emriReal = Request.QueryString["emriReal"];
            hfState.Set("eshteHapurRaporti", true);
            clsRaporti oRap = new clsRaporti(idGjuha, emriReal);

            if (string.IsNullOrEmpty(emriReal)) oRap = new clsRaporti(idGjuha, idRaporti);

            clsSP oSp = new clsSP(oRap.IdSp);

            SqlParameter[] sqlParam = krijoParametratPerSql(oRap.IdSp, idNdermarrje, periudhaKontabel, idNderViti, idPerdorues, guidString);
            colParameter sqlParamShfaqRaport = krijoParametratPerTuShfaqurNeRaport(oRap.IdSp, idNdermarrje, periudhaKontabel, idNderViti, idPerdorues, guidString, kaSubRaport);

            clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasKod("NKM", idNdermarrje);
            DateTime dtmbarimi = DateTime.Today;
            bool azhornim = false;
            if (((ASPxCheckBox)navBarFiltrat.Groups[1].FindControl("cbAzhornim")).Checked && RaportiEmerReal != "gjendjaPermbledhurEArkes" && RaportiEmerReal != "gjendjaPermbledhurEBankes")
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
            int idPeriudha = clsPeriudhaKontabel.ktheIdPeriudheSipasDatesDheNdermarrjes(dtmbarimi, idNdermarrje);
            XtraReport report = ReportFunctions.krijoObjektRaporti("", idGjuha, idPerdorues, idViti, oRap.IdRaporti, idNdermarrje, sqlParamShfaqRaport, IdReportDesign, ReportOrientation, guidString, Request.QueryString[ScopeManager.ScopeIdKey]);
            report.StyleSheet.LoadFromFile(NdertoPathStyleSheet(pathStyle.Value, ReportStyle));
            afisho(oRap.IdRaporti, report, oSp, sqlParam, idPerdorues, azhornim, idNdermarrje, idNderViti, dtmbarimi, konf.IdKonfigAmbjente, idPeriudha);

        }

        private void merrDataSourceRaport(XtraReport reportLibriPaMerge, XtraReport report, int idPerdorues)
        {
            clsPerdorues perdoruesi = DbCore.mySessionObjects.kthePerdorues(Session);
            reportLibriPaMerge.DataSource = report.DataSource;
            reportLibriPaMerge.DataAdapter = report.DataAdapter;
            reportLibriPaMerge.DataMember = report.DataMember;
            KonfigFleteRaporti(reportLibriPaMerge, perdoruesi);
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
            int filertrupikontrolleLength = filertrupikontrolle.Length;
            for (int i = 0; i < filertrupikontrolleLength; i++)
            {
                string kodKontrolli = filertrupikontrolle[i];
                clsKontroll kontrolli = clsKontroll.merrKontrollSipasKoditKomponentes(kodKontrolli, 649);
                string tipiKontrollit = Enum.GetName(typeof(DbCore.DbShare.TipeKontrolli), kontrolli.IdTipiKontrollit);
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
            colRaporti subRaportet = new colRaporti(IdGjuha, IdRaporti);
            Dictionary<int, colParameter> paramReturn = new Dictionary<int, colParameter>();
            if (subRaportet.Count != 0)
            {
                colParameter[] parametratPerbashket = new colParameter[subRaportet.Count];
                for (int i = 0; i < subRaportet.Count; i++)
                {
                    colParameter paramPerbashketR1R2 = colParameter.merrParametraPebashketRaportesh(IdRaporti, subRaportet[i].IdRaporti);
                    parametratPerbashket[i] = paramPerbashketR1R2;
                    //Ruan ne dictionary id e subraportit dhe collection-in e parametrave te perbashket
                    paramReturn.Add(subRaportet[i].IdRaporti, parametratPerbashket[i]);
                }
            }
            return paramReturn;
        }

        private void konfigurimeSubRaporti(int idNdermarrje, clsPeriudhaKontabel periudhaKontabel, int idNderviti, int idPerdorues, String guidString)
        {
            //Marrim listen e parametrave te perbashket per secilin subraport
            Dictionary<int, colParameter> paramPerbashketSubRaport = ktheParametratPerbashketSubRaportet();
            if (paramPerbashketSubRaport.Count != 0) //nese ka subraporte
            {
                foreach (KeyValuePair<int, colParameter> kv in paramPerbashketSubRaport)
                {
                    Dictionary<int, string> vleraParamSubRaport = new Dictionary<int, string>();
                    clsRaporti subRaport = new clsRaporti(DbCore.mySessionObjects.ktheGjuhe(Session), kv.Key);

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
                                vleraParamSubRaport.Add(param.IdParametri, ktheVlerenEParametrit(param, idNdermarrje, periudhaKontabel, idNderviti, idPerdorues, oColKontrolle));
                        }
                        else //Per parametrat e vecante te subraportit  vendoset nje string bosh
                            vleraParamSubRaport.Add(param.IdParametri, "");
                    }

                    //Vlerat e parametrave te secilit raport ruhen ne nje session(me id e subraporit) per tu aksesuar nga faqja RaportiShpejte.Aspx
                    DbCore.mySessionObjects.ruajParametratSubRaportitNeSesion(Session, vleraParamSubRaport, kv.Key, guidString);

                    //pjesa 2
                    colParameter paramSubRaport2 = new colParameter(subRaport.IdSp, IdRaporti);
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


        private SqlParameter[] krijoParametratSql(int idSp, int idNdermarrje, clsPeriudhaKontabel periudhaKontabel, int idNderviti, int idPerdorues)
        {
            colParameter parametraSp = new colParameter(idSp);
            colKontrolle oColKontrolle = new colKontrolle();
            oColKontrolle.merrKontrolletRaportiSipasIdSp(idSp);
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

                    vlera = ktheVlerenEParametrit(parametraSp[i], idNdermarrje, periudhaKontabel, idNderviti, idPerdorues, oColKontrolle);
                    if (Request.QueryString["vjenNga"] == "GIS" && !String.IsNullOrEmpty(Request.QueryString["kodiMagGis"]))
                    {
                        if (parametraSp[i].Emri == "filterMagazina")
                        {
                            string kodi = Request.QueryString["kodiMagGis"];
                            vlera = " ( T_NJESIADMINISTRATIVE.KODI = ('" + kodi + "')  ) AND ";
                            ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("txtBtnMagazina1")).Text = kodi;
                        }
                    }
                    if (Request.QueryString["vjenNga"] == "GIS" && !String.IsNullOrEmpty(Request.QueryString["kodDegeAdministrative"]))
                    {
                        if (parametraSp[i].Emri == "filterDegeAdminPaLidhese")
                        {
                            string kodi = Request.QueryString["kodDegeAdministrative"];
                            vlera = " ( T_DEGEADMINISTRATIVE.KODI = ('" + kodi + "')  ) AND ";
                            ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("degeAdminButtonEdit1")).Text = kodi;
                        }
                    }
                    sqlparam[i] = new SqlParameter();
                    sqlparam[i].ParameterName = parametraSp[i].Emri;
                    sqlparam[i].Value = vlera;
                }
            }
            else
                sqlparam = new SqlParameter[0];
            return sqlparam;
        }

        private colParameter krijoParametratShfaqRaporti(int idSp, int idNdermarrje, clsPeriudhaKontabel periudhaKontabel, int idNderviti, CultureInfo ci)
        {
            colParameter parametraSp = new colParameter(idSp, IdRaporti);
            colKontrolle colKontrollet = new colKontrolle();
            colKontrollet.merrKontrolletRaporti(IdRaporti);
            for (int i = 0, j = 0; i < parametraSp.Count; i++, j++)
            {
                //string vlera = "";

                string parametraSpEmri = parametraSp[i].Emri;
                if (parametraSpEmri.ToLower() == "idraport" || parametraSpEmri.ToLower() == "idperdoruesi" || (parametraSpEmri.Contains("1") &&
                    !parametraSpEmri.EqualsAnyIgnoreCase("filterNumerDokumenti1", "filterFurnitor1", "filterMagazina1", "filterNumerLlogarie1", "filterKategoriShpenzimi1", "filterShenimeShitje1", "txtInterval1", "grupoSipasGrupim1")) || (parametraSpEmri.Contains("2") && !parametraSpEmri.ToString().Equals("txtInterval2")) || parametraSpEmri == "filterDtDokShitje" || parametraSpEmri == "filterDtRegjShitje" || parametraSpEmri == "filterDtMaturimi" || parametraSpEmri.ToString().Equals("intervalet"))
                {
                    j--;
                    continue;
                }
                parametraSp[i].Vlera = ktheVlerenEParametritShfaqRaport(parametraSp[i], idNdermarrje, periudhaKontabel, idNderviti, ci, colKontrollet);
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
            if (parametri.Emri.ToLower() == "filterdtdokdergimi")
                return "";//nese hapet nga ambjenti raporti te hape te gjitha te dhenat
            if (emriParam.Equals("filterkodifikimartp"))
            {
                object tmpValue = ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("GrupimPareButtonEdit1")).Value;
                return vlera = tmpValue == null ? "" : tmpValue.ToString();
            }
            if (emriParam.Equals("filtergrupebanke"))
            {
                object tmpValue = ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("GrupeBankeButtonEdit1")).Value;
                return vlera = tmpValue == null ? "" : tmpValue.ToString();
            }
            if (emriParam.Equals("filtroKapitull"))
            {
                object tmpValue = ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("btneKapitulli")).Value;
                return vlera = tmpValue == null ? "" : tmpValue.ToString();
            }
            if (emriParam.Equals("filtroProgram"))
            {
                object tmpValue = ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("btneProgrami")).Value;
                return vlera = tmpValue == null ? "" : tmpValue.ToString();
            }
            if (emriParam.Equals("filterkategorishpenzimi"))
            {
                object tmpValue = ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("btnKatShpenzimi1")).Value;
                return vlera = tmpValue == null ? "" : tmpValue.ToString();
            }
            if (emriParam.Equals("filterkodifikimartd"))
            {
                object tmpValue = ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("GrupimDyteButtonEdit1")).Value;
                return vlera = tmpValue == null ? "" : tmpValue.ToString();
            }
            if (emriParam.Equals("filterkodifikimartt"))
            {
                object tmpValue = ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("GrupimTreteButtonEdit1")).Value;
                return vlera = tmpValue == null ? "" : tmpValue.ToString();
            }
            if (emriParam.Equals("filtermagazineskadence"))
            {
                vlera = ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("txtBtnMagazineSkadence")).Text;
                return vlera;
            }
            if (emriParam.Equals("vitiaktual"))
            {
                if (periudhaKontabel != null)
                {
                    vlera = periudhaKontabel.FillimiPeriudha.Year.ToString();
                }
                return vlera;
            }
            if (emriParam.Equals("vitiparaardhes"))
            {

                if (periudhaKontabel != null)
                {
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
                if (!RaportiEmerReal.EqualsAnyIgnoreCase("maturimStokuArtikujAfatgjate", "MaturimiStokutPerArtikujtMeSeriale", "MaturimiFaturaveTeKlienteve", "MaturimiFaturaveTeFurnitoreve") && vlera == "")
                {
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
                                if (kodKontrolli.ToLower().StartsWith("txtpershkrimfature"))
                                {
                                    vlera += textBoxi.Text;
                                    continue;

                                }
                                if (kodKontrolli.ToLower().StartsWith("txtTarga"))
                                {
                                    vlera += textBoxi.Text;
                                    continue;

                                }
                                if (kodKontrolli.ToLower().StartsWith("txtShoferi"))
                                {
                                    vlera += textBoxi.Text;
                                    continue;

                                }
                                if (kodKontrolli.ToLower().StartsWith("llogdebi_textbox"))
                                {
                                    vlera += textBoxi.Text;
                                }

                                if (kodKontrolli.ToLower().StartsWith("dega_textbox"))
                                {


                                    vlera += textBoxi.Text;
                                }

                                if (kodKontrolli.ToLower().StartsWith("txtemmbshoqeri"))
                                {

                                    vlera += textBoxi.Text;
                                    continue;
                                }
                                if (kodKontrolli.ToLower().StartsWith("txtnrtel"))
                                {

                                    vlera += textBoxi.Text;
                                    continue;
                                }

                                if (kodKontrolli.ToLower().StartsWith("txtcustomernumber"))
                                {

                                    vlera += textBoxi.Text;
                                    continue;
                                }
                                if (kodKontrolli.ToLower().StartsWith("txtvlereshitjep"))
                                {
                                    //vlera += String.Format("{0} > {1} ", kolonaFilter, textBoxi.Text);
                                    vlera += textBoxi.Text;
                                    vlera += " <";
                                }
                                if (kodKontrolli.ToLower().StartsWith("txtvlereshitjed"))
                                {
                                    if (vlera == "")
                                    {
                                        vlera += "< ";
                                        vlera += textBoxi.Text;
                                    }
                                    else
                                    {
                                        vlera = vlera.Replace(" <", " - ");
                                        vlera += textBoxi.Text;
                                    }
                                    //vlera += String.Format("{2} {0} < {1} ", kolonaFilter, textBoxi.Text, " AND ");
                                }

                                if (kodKontrolli.ToLower().ContainsAnyIgnoreCase("DpshNrDokumentiTextBox", "DpshShenimeTextBox"))
                                {
                                    vlera += textBoxi.Text;
                                }
                                if (kodKontrolli.ToLower().StartsWith("txttac"))
                                {
                                    vlera += textBoxi.Text;
                                }
                                if (kodKontrolli.ToLower().StartsWith("txtmarveshja"))
                                {
                                    vlera += textBoxi.Text;
                                }
                                if (kodKontrolli.ToLower().StartsWith("txteic"))
                                {
                                    vlera += textBoxi.Text;
                                }
                                if (kodKontrolli.ToLower().StartsWith("cmbstatusieinvoice"))
                                {
                                    vlera += textBoxi.Text;
                                }
                                continue;
                            #endregion

                            #region ASPxComboBox
                            case "ASPxComboBox":
                                ASPxComboBox combo = (ASPxComboBox)uiKontroll;
                                if (combo.Text.Trim() == "")
                                    continue;
                                if (kodKontrolli.ToLower().StartsWith("cmblidhesa")) // && !kodKontrolli.ToLower().StartsWith("cmbLidhesaPershk")
                                {
                                    lidhes = combo.Value.ToString();
                                    if (lidhes == "and")
                                        lidhes = "dhe";
                                    if (lidhes == "or")
                                        lidhes = "ose";
                                    continue;
                                }
                                if (kodKontrolli.ToLower().StartsWith("cmbvep"))
                                {
                                    if (kodKontrolli.Contains("1"))
                                    {
                                        veprimiPara1 = combo.Value.ToString();
                                        continue;
                                    }
                                    if (kodKontrolli.Contains("2"))
                                    {
                                        veprimiPara2 = combo.Value.ToString();
                                        continue;
                                    }
                                }
                                if (kodKontrolli == "cmbGrupKosto")
                                {
                                    return combo.SelectedItem.Text;
                                }

                                if (kodKontrolli == "cmbLlojCmimi")
                                {
                                    return combo.SelectedItem.Text;
                                }

                                if (kodKontrolli.StartsWith("cmbFormatNumri"))
                                {

                                    return vlera = Convert.ToString(combo.SelectedIndex);
                                }
                                if (kodKontrolli == "cmbLlojFaze")
                                {
                                    return combo.SelectedIndex.ToString();
                                    vlera = String.Format("{0}", combo.Value);

                                }
                                if (kodKontrolli.ToLower().StartsWith("cmbarkabanka"))
                                {


                                    vlera = String.Format("{0}", combo.Text);
                                }


                                

                                if (kodKontrolli.ToLower().StartsWith("cmbniveli"))
                                {

                                    vlera = String.Format("{0}", combo.Text);
                                }

                                if (kodKontrolli.ToLower().StartsWith("cmbstandarti"))
                                {

                                    vlera = String.Format("{0}", combo.Text);
                                }
                                if (kodKontrolli.ToLower().StartsWith("cmbstatusmagazine"))
                                {
                                    vlera = String.Format("{0}", combo.Text);
                                }
                                if (kodKontrolli.ToLower().StartsWith("cmbNjesiKohe"))
                                {
                                    vlera = String.Format("{0}", combo.Value);
                                }
                                if (kodKontrolli == "cmbMonedhaQK")
                                {
                                    vlera = String.Format("{0}", combo.Value);
                                    continue;
                                }
                                if (kodKontrolli.ToLower().StartsWith("cmbmonedha") || kodKontrolli.ToLower().StartsWith("cmbkonvertone"))
                                {
                                    vlera = String.Format("{0}", combo.Text);
                                }
                                if (kodKontrolli.ToLower().StartsWith("cmbgrup"))
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
                                

                                
                                if (kodKontrolli.ToLower().StartsWith("cmbpajisje1") || kodKontrolli.ToLower().StartsWith("cmbqyteti1") || kodKontrolli.ToLower().StartsWith("cmbklasaartikullit1") || kodKontrolli.ToLower().StartsWith("cmbstatuskonvertimidyte1") || kodKontrolli.ToLower().StartsWith("cmbmenyrepagese1"))
                                {
                                    vlere1 = combo.Text;
                                    vlera = "";
                                }
                                if (kodKontrolli.ToLower().StartsWith("cmbpajisje2") || kodKontrolli.ToLower().StartsWith("cmbqyteti2") || kodKontrolli.ToLower().StartsWith("cmbklasaartikullit2") || kodKontrolli.ToLower().StartsWith("cmbstatuskonvertimidyte2") || kodKontrolli.ToLower().StartsWith("cmbmenyrepagese2"))
                                {
                                    vlere2 = combo.Text;
                                    vlera = "";
                                }
                                if (kodKontrolli.Contains("1") && kodKontrolli.ToLower().Contains("llojveprimi"))
                                {
                                    vlere1 = combo.Value.ToString();
                                    vlera = "";
                                }
                                if (kodKontrolli.Contains("2") && kodKontrolli.ToLower().Contains("llojveprimi"))
                                {
                                    vlere2 = combo.Value.ToString();
                                    vlera = "";
                                }
                                if (kodKontrolli.ToLower() == "cmbllojielem")
                                    vlera = combo.Text;
                                if (kodKontrolli.ToLower() == "cmbstatuselem")
                                    vlera = combo.Text;
                                if (kodKontrolli.ToLower() == "cmbkrahasimkosto" || kodKontrolli.ToLower() == "cmbstatusrezervimi" || kodKontrolli.ToLower() == "cmbcmimmepatvsh")
                                    return combo.Text;
                                if (kodKontrolli.ToLower() == "cmbklientaktiv" || kodKontrolli.ToLower() == "cmbKontabilizuar")
                                    return combo.Text;
                                if (kodKontrolli.ToLower() == "cmbFilterKatAktive")
                                    return combo.Text;
                                if (kodKontrolli.ToLower() == "cmbgrupoklientsipas")
                                    return combo.Text;
                                if (kodKontrolli.ToLower() == "cmbgrupoartikullsipas")
                                    return combo.Text;
                                if (kodKontrolli.ToLower() == "cmbstatus")
                                    return combo.Text;
                                if (kodKontrolli.ToLower() == "cmbstatusmarreveshje")
                                    return combo.Text;
                                if (kodKontrolli.ToLower() == "cmbgjendjeart")
                                    return combo.Text;
                                if (kodKontrolli.ToLower() == "cmbShfaqArt")
                                    return combo.Text;
                                if (kodKontrolli.ToLower() == "cmbstatusidok")
                                    return combo.Text;
                                if (kodKontrolli.ToLower() == "cmbgjendja")
                                    return combo.Text;
                                if (kodKontrolli.ToLower() == "cmbtipgrafiku")
                                    return combo.Text;
                                if (kodKontrolli.ToLower() == "cmbkategorizimartikuj")
                                    return combo.Text;
                                if (kodKontrolli.ToLower() == "cmblikuiduar")
                                    return combo.Text;
                                if (kodKontrolli.ToLower() == "cmbmuaji")
                                    return combo.Text;
                                if (kodKontrolli.ToLower() == "cmbveprimeperiudhe")
                                    return combo.Text;
                                if (kodKontrolli.ToLower() == "cmbartcmim")
                                    vlera = combo.Text;
                                if (kodKontrolli.ToLower() == "cmbcmimeartikulli")
                                    vlera = combo.Text;
                                if (kodKontrolli.ToLower() == "cmbgruposipas")
                                    return combo.Text;
                                if (kodKontrolli.ToLower() == "cmbGrupoSipasAgjenteve")
                                    return combo.Text;
                                continue;
                            #endregion

                            #region ASPxCheckBox

                            case "ASPxCheckBox":
                                ASPxCheckBox checkBoxi = (ASPxCheckBox)uiKontroll;
                                if (kodKontrolli.ToLower().StartsWith("cbgrupimsipaskf") || kodKontrolli.ToLower().StartsWith("cbmonedhekf") || kodKontrolli.ToLower().StartsWith("cbdetajuar") || kodKontrolli.ToLower().StartsWith("cbkthime") || kodKontrolli.ToLower().StartsWith("cbnenprodukte") || kodKontrolli.ToLower().StartsWith("cbgrupimqk"))
                                {

                                    return checkBoxi.Checked.ToString();
                                }

                                if (kodKontrolli == "cbMbylljeViti")
                                {
                                    if (checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterPo", ci);
                                    else if (!checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterJo", ci);

                                    return vlera;

                                }

                                if (kodKontrolli == "cbShperndarjeDhuratash")
                                {
                                    if (checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterPo", ci);
                                    else if (!checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterJo", ci);

                                    return vlera;

                                }

                                if (kodKontrolli == "cbShfaqGrup")
                                {
                                    if (checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterPo", ci);
                                    else if (!checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterJo", ci);

                                    return vlera;

                                }

                                if (kodKontrolli == "cmbShfaqnendep")
                                {
                                    if (checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterPo", ci);
                                    else if (!checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterJo", ci);

                                    return vlera;

                                }

                                if (kodKontrolli == "cbAzhornim")
                                {
                                    if (checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterPo", ci);
                                    else if (!checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterJo", ci);

                                    return vlera;

                                }

                                if (kodKontrolli == "checkLlogariSintetike")
                                {
                                    if (checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterPo", ci);
                                    else if (!checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterJo", ci);

                                    return vlera;
                                }
                                if (kodKontrolli == "cbgrupoKatShpenzim" || kodKontrolli == "cbBuxhetSipasKapitujve" || kodKontrolli == "cbshitjeKomisionZero")
                                {
                                    if (checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterPo", ci);
                                    else
                                        vlera = rm.GetString("cmbFilterJo", ci);
                                    return vlera;
                                }
                                if (kodKontrolli == "cbShfaqArtGjendjeZero")
                                {
                                    if (checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterPo", ci);
                                    else if (!checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterJo", ci);

                                    return vlera;

                                }
                                if (kodKontrolli == "cbDraft")
                                {
                                    if (checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterPo", ci);
                                    else if (!checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterJo", ci);

                                    return vlera;

                                }

                                if (kodKontrolli == "cbShfaqLlogP")
                                {
                                    if (checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterPo", ci);
                                    else if (!checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterJo", ci);

                                    return vlera;
                                }
                                if (kodKontrolli == "cbSasiaPakonvertuar")
                                {
                                    if (checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterPo", ci);
                                    else if (!checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterJo", ci);

                                    return vlera;
                                }
                                if (kodKontrolli == "cbGrupimSipasKF" || kodKontrolli == "cbMonedheKF" || kodKontrolli == "checkAfishoPagen" || kodKontrolli == "cbMonedheLl" || kodKontrolli == "cbKonvertuarNgaKontrata" || kodKontrolli == "cbGrupoSipasGrupim1" || kodKontrolli.ToLower().StartsWith("cbGrupoSipasGrupim1") || kodKontrolli == "cbkthime")
                                {
                                    if (checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterPo", ci);
                                    else if (!checkBoxi.Checked)
                                        vlera = rm.GetString("cmbFilterJo", ci);

                                    return vlera;

                                }
                                if (kodKontrolli.ToLower().StartsWith("cbazhornim"))
                                {
                                    if (checkBoxi.Checked)
                                        return rm.GetString("cmbFilterPo", ci);
                                    return rm.GetString("cmbFilterJo", ci);
                                }
                               
                                if (kodKontrolli.ToLower().StartsWith("cmbShfaqnendep"))
                                {
                                    if (checkBoxi.Checked)
                                        return rm.GetString("cmbFilterPo", ci);
                                    return rm.GetString("cmbFilterJo", ci);
                                }
                                if (kodKontrolli.ToLower().StartsWith("checkllogarisintetike"))
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
                                if (kodKontrolli == "dteDtMaturimi" || kodKontrolli == "dateDateKontrate" || kodKontrolli == "dataSkadence")
                                {
                                    vlera = dateEdit.Text.Trim();
                                    continue;
                                }
                                if (kodKontrolli.ToLower().StartsWith("txtngaorekrijimi"))
                                {
                                    vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaOreKrijimi")).Date.ToLongTimeString();
                                    continue;
                                }
                                if (kodKontrolli.ToLower().StartsWith("txtderiorekrijimi"))
                                {
                                    vlera += " " + ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriOreKrijimi")).Date.ToLongTimeString();
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
                                    if (kodKontrolli.ToLower().StartsWith("btnarkabankaemra") || kodKontrolli.ToLower().StartsWith("cmbarkabanka"))
                                    {
                                        DbCore.DbArkaBanka.clsBanka b = new DbCore.DbArkaBanka.clsBanka();
                                        b.mbushBankeSipasKodit(arr[v], idNdermarrje);
                                        pershkrimet += b.EmerBanka + ",";
                                    }
                                    else
                                        if (kodKontrolli.ToLower().StartsWith("btnekodkfkryesore") || kodKontrolli.ToLower().StartsWith("btnekodkf") || kodKontrolli.ToLower().StartsWith("btnefurnart"))
                                    {


                                        DbCore.DbKontabiliteti.clsKlientFurnitor b = new DbCore.DbKontabiliteti.clsKlientFurnitor();
                                        b.mbushKlientFurnitorSipasKodit(arr[v], idNdermarrje);
                                        pershkrimet += b.EmertimiKF + ",";

                                    }
                                    else
                                            if (kodKontrolli.ToLower().StartsWith("txtbtnmagazina") || kodKontrolli.ToLower().StartsWith("txtbtnmagazinades"))
                                    {
                                        DbCore.DbRegjistrim.clsNjesiAdministrative b = new DbCore.DbRegjistrim.clsNjesiAdministrative(arr[v], idNdermarrje);

                                        pershkrimet += b.Pershkrimi + ",";
                                    }
                                    else
                                                if (kodKontrolli.ToLower().StartsWith("txtbtnkartela"))
                                    {
                                        DbCore.DbInventari.clsArtikulli b = new DbCore.DbInventari.clsArtikulli();
                                        b.mbushArtikull(arr[v], idNdermarrje);
                                        pershkrimet += b.PershkrimArtikulli + ",";
                                    }
                                    else
                                                    if (kodKontrolli.ToLower().StartsWith("pikeshfbuttonedit"))
                                    {
                                        DbCore.DbRegjistrim.clsPikeShitjeFurnizimi b = new DbCore.DbRegjistrim.clsPikeShitjeFurnizimi(arr[v], idNdermarrje);

                                        pershkrimet += b.Pershkrimi + ",";
                                    }
                                    else


                                               
                                    if (kodKontrolli.ToLower().StartsWith("degeadminbuttonedit"))
                                    {
                                        DbCore.DbRegjistrim.clsDegeAdministrative b = new DbCore.DbRegjistrim.clsDegeAdministrative(arr[v], idNdermarrje);

                                        pershkrimet += b.Pershkrimi + ",";
                                    }
                                    else
                                                            if (kodKontrolli.ToLower().StartsWith("btneqenderkosto"))
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
                                if (kodKontrolli.Contains("1") )
                                {
                                    vlere1 = String.Format("{0}", pershkrimet);


                                    continue;
                                }



                                if (kodKontrolli.Contains("2"))
                                {

                                    vlere2 = String.Format("{0}", pershkrimet);


                                    continue;
                                }
                                if (kodKontrolli == "txtBtnMagazina"   || kodKontrolli == "txtBtnKartela" || kodKontrolli == "txtBtnKatSeriali" || kodKontrolli == "txtBtnLlojDok" || kodKontrolli == "btnePerdorues" || kodKontrolli == "txtBtnKrijuesi" || kodKontrolli == "btnAgjentShitje" || kodKontrolli == "btnNivelCmimi" || kodKontrolli == "btneQenderKosto" || kodKontrolli == "btneAuto" || kodKontrolli == "btneKompania" || kodKontrolli == "btnSeriali1" || kodKontrolli == "lblKategoriSeriali" || kodKontrolli == "btnBurimi" || kodKontrolli == "btnAktiviteti" || kodKontrolli == "btnNivelZbritje" || kodKontrolli == "txtBtnMagazinaPaLidhese" || kodKontrolli == "degeAdminPaLidheseButtonEdit" || kodKontrolli == "DpshKlientFurnitorButtonEdit" || kodKontrolli == "DpshNrLlogarieButtonEdit" || kodKontrolli == "DpshKategShpenzimiButtonEdit" || kodKontrolli == "DpshMagazinaButtonEdit" || kodKontrolli == "btnStatusPerdorues" || kodKontrolli == "btnModPerdorues")
                                {
                                    // vlera = buttonEdit.Value.ToString();
                                    vlera = String.Format("{0}", pershkrimet);
                                    continue;
                                }

                                if (kodKontrolli == "GrupeBankeButtonEdit1")
                                {

                                }


                                continue;
                            #endregion

                            #region ASPxRadioButtonList
                            case "ASPxRadioButtonList":
                                ASPxRadioButtonList radioKontrolli = ((ASPxRadioButtonList)uiKontroll);
                                if (radioKontrolli.SelectedItem.Value.ToString() == "")
                                    continue;

                                if (radioKontrolli.ID.StartsWith("radDtDok") || radioKontrolli.ID.StartsWith("radDtRegj") || radioKontrolli.ID.StartsWith("radDtDtFillimi") || 
                                    radioKontrolli.ID.StartsWith("radDtDokAfatKohor") || radioKontrolli.ID.StartsWith("radPeriudhe") || radioKontrolli.ID.StartsWith("radDtPlanifikimi") 
                                    || radioKontrolli.ID.StartsWith("radDtProdhimi") || radioKontrolli.ID.StartsWith("radDtMbarimi") || radioKontrolli.ID.StartsWith("radDtKrijimi")
                                    || radioKontrolli.ID.StartsWith("radDateLidhes") || radioKontrolli.ID.StartsWith("radDtSkadence"))
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
                                            if (parametri.Emri.ToLower().Equals("filterdtkrijimi"))
                                            {
                                                vlera = String.Format("{0} - {1}", periudhaKontabel.FillimiPeriudha.ToShortDateString(), periudhaKontabel.MbarimiPeriudha.ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().Equals("filterdtdokkrahasues"))
                                            {
                                                vlera = String.Format("{0} - {1}", periudhaKontabel.FillimiPeriudha.AddYears(-1).ToShortDateString(), periudhaKontabel.MbarimiPeriudha.AddYears(-1).ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().Equals("filterdtdokkrahasuesqk"))
                                            {
                                                vlera = String.Format("{0} - {1}", periudhaKontabel.FillimiPeriudha.AddYears(-1).ToShortDateString(), periudhaKontabel.MbarimiPeriudha.AddYears(-1).ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().Equals("filterdtdok2"))
                                            {
                                                return vlera = periudhaKontabel.MbarimiPeriudha.ToShortDateString();
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().Equals("filterdtdokkrahasuesmbarim"))
                                            {
                                                vlera = periudhaKontabel.MbarimiPeriudha.AddYears(-1).ToShortDateString();
                                                continue;
                                            }

                                            if (parametri.Emri.ToLower().StartsWith("filterdt"))
                                            {
                                                vlera = String.Format("{0} - {1}", periudhaKontabel.FillimiPeriudha.ToShortDateString(), periudhaKontabel.MbarimiPeriudha.ToShortDateString());
                                                continue;
                                            }


                                            if (parametri.Emri.ToLower().Equals("filterdatedoklidhes"))
                                            {
                                                vlera = String.Format("{0} - {1}", periudhaKontabel.FillimiPeriudha.ToShortDateString(), periudhaKontabel.MbarimiPeriudha.ToShortDateString());
                                                continue;
                                            }

                                            if (parametri.Emri.ToLower().Equals("filterdtskadence"))
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
                                            if (parametri.Emri.ToLower().Equals("filterdtdokkrahasuesqk"))
                                            {
                                                vlera = String.Format("{0} - {1}", ndermviti.NdermarrjeVitiFillim.AddYears(-1).ToShortDateString(), ndermviti.NdermarrjeVitiFund.AddYears(-1).ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().Equals("filterdtdokkrahasuesmbarim"))
                                            {
                                                return vlera = ndermviti.NdermarrjeVitiFund.AddYears(-1).ToShortDateString();
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().Equals("filterdtdok2"))
                                            {
                                                return vlera = ndermviti.NdermarrjeVitiFund.ToShortDateString();
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().Equals("filterdtkrijimiseriali"))
                                            {
                                                vlera = String.Format("{0} - {1}", ndermviti.NdermarrjeVitiFillim.ToShortDateString(), ndermviti.NdermarrjeVitiFund.ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().Equals("filterdtkrijimi"))
                                            {
                                                vlera = String.Format("{0} - {1}", ndermviti.NdermarrjeVitiFillim.ToShortDateString(), ndermviti.NdermarrjeVitiFund.ToShortDateString());
                                                continue;
                                            }
                                            if (parametri.Emri.ToLower().StartsWith("filterdt"))
                                            {
                                                vlera = String.Format("{0} - {1}", ndermviti.NdermarrjeVitiFillim.ToShortDateString(), ndermviti.NdermarrjeVitiFund.ToShortDateString());
                                                continue;
                                            }


                                            if (parametri.Emri.ToLower().Equals("filterdatedoklidhes"))
                                            {
                                                vlera = String.Format("{0} - {1}", ndermviti.NdermarrjeVitiFillim.ToShortDateString(), ndermviti.NdermarrjeVitiFund.ToShortDateString());
                                                continue;
                                            }

                                            if (parametri.Emri.ToLower().Equals("filterdtskadence"))
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
                                        if (parametri.Emri.ToLower().Equals("filterdtdokkrahasuesqk"))
                                        {
                                            vlera = String.Format("{0} - {1}", Convert.ToDateTime("1900-01-01").ToShortDateString(), DateTime.MaxValue.ToShortDateString());
                                            continue;
                                        }
                                        if (parametri.Emri.ToLower().Equals("filterdtdokkrahasuesmbarim"))
                                        {
                                            return vlera = DateTime.MaxValue.ToShortDateString();
                                            continue;
                                        }
                                        if (parametri.Emri.ToLower().Equals("filterdtdok2"))
                                        {
                                            return vlera = DateTime.MaxValue.ToShortDateString();
                                            continue;
                                        }
                                        if (parametri.Emri.ToLower().Equals("filterdtkrijimi"))
                                        {
                                            vlera = String.Format("{0} - {1}", Convert.ToDateTime("1900-01-01").ToShortDateString(), DateTime.MaxValue.ToShortDateString());
                                            continue;
                                        }

                                        if (parametri.Emri.ToLower().Equals("filterdatedoklidhes"))
                                        {
                                            vlera = String.Format("{0} - {1}", Convert.ToDateTime("1900-01-01").ToShortDateString(), DateTime.MaxValue.ToShortDateString());
                                            continue;
                                        }

                                        if (parametri.Emri.ToLower().StartsWith("filterdtskadence"))
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
                                        if (parametri.Emri.ToLower().Equals("filterdtkrijimi"))
                                        {
                                            vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaKrijimi")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriKrijimi")).Date.ToShortDateString());
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

                                        if (parametri.Emri.ToLower().Equals("filterdatedoklidhes"))
                                        {
                                            vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokDateLidhes")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokDateLidhes")).Date.ToShortDateString());
                                            continue;
                                        }

                                        if (parametri.Emri.ToLower().StartsWith("filterdtskadence"))
                                        {
                                            vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaSkadence")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriSkadence")).Date.ToShortDateString());
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
                                        if (parametri.Emri.ToLower().Equals("filterdtdokkrahasuesqk"))
                                        {
                                            vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDtKrahasuesQK")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDtKrahasuesQK")).Date.ToShortDateString());
                                            continue;
                                        }
                                        if (parametri.Emri.ToLower().Equals("filterdtdokkrahasuesmbarim"))
                                        {
                                            return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDtKrahasues")).Date.ToShortDateString();
                                            continue;
                                        }
                                        if (parametri.Emri.ToLower().Equals("filterdtdok2"))
                                        {
                                            return vlera = ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDok")).Date.ToShortDateString();
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

                                        if (parametri.Emri.ToLower().StartsWith("filterdtdokshitjerap"))
                                        {
                                            vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokShitje")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokShitje")).Date.ToShortDateString());
                                            continue;
                                        }
                                        if (parametri.Emri.ToLower().StartsWith("filterdtdokqenderkosto"))
                                        {
                                            vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokQenderKosto")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokQenderKosto")).Date.ToShortDateString());
                                            continue;
                                        }
                                        if (parametri.Emri.ToLower().StartsWith("filterdtdok"))
                                        {
                                            if (parametri.Emri.ToLower().StartsWith("filterdtdokqenderkosto"))
                                            {
                                                vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokQenderKosto")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokQenderKosto")).Date.ToShortDateString());
                                                continue;
                                            }

                                            //else if (RaportiEmerReal == "gjendjaLlogarive")

                                            //    vlera = String.Format("{0} - {1}", Convert.ToDateTime("1900-01-01").ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDok")).Date.ToShortDateString());
                                            else
                                                vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDok")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDok")).Date.ToShortDateString());
                                            continue;

                                        }
                                        if (parametri.Emri.ToLower().StartsWith("filterdtkonvertopike"))
                                        {
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
                                        if (parametri.Emri.ToLower().StartsWith("filterdtdokshitjerap"))
                                        {
                                            vlera = String.Format("{0} - {1}", ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtNgaDokShitje")).Date.ToShortDateString(), ((ASPxDateEdit)navBarFiltrat.Groups[g].FindControl("txtDeriDokShitje")).Date.ToShortDateString());
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

        /// <summary>
        /// Afishon raportin mbasi krijon duke i vene datasetin, duke konfiguruar fleten e raportit
        /// </summary>
        /// <param name="idRap">id-ja raportit</param>
        /// <param name="ruajStyle">te ruhet style apo jo</param>
        protected void afisho(int idRaporti, XtraReport report, clsSP oSp, SqlParameter[] sqlParam, int idPerdorues, bool azhornim, int idNdermarje, int idnderviti, DateTime dtmbarimi, int idkonfig, int idperiudha)
        {
            if (idRaporti >= 0)
            {
                String guidString = hfState["guidString"].ToString();
                if (RaportiEmerReal == "teArdhuraShpenzimeQendraKosto")
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
                ReportFunctions.konfigDataSetRaporti(report, oSp.SpEmri, idPerdorues, azhornim, idNdermarje, idnderviti, dtmbarimi, idkonfig, idperiudha, sqlParam);
                if (((DataSet)report.DataSource).Tables[0].Rows.Count == 0)
                    ImbLogger.Warn($"Raporti me emer {RaportiEmerReal} nuk ka te dhena per periudhen e zgjedhur.");

                clsPerdorues perdoruesi = mySessionObjects.kthePerdorues(Session);
                KonfigFleteRaporti(report, perdoruesi, false);

                if (RaportiEmerReal == "gjendjeArtikujshMinMaxSipasMagazines" || RaportiEmerReal == "KontrolliSkadencesArtikujve")
                    mySessionObjects.ruajdtNeSession(Session, ((DataSet)report.DataSource).Tables[0]);

                var designSettings = clsRaportDesign.GetReportDesignSettings(IdReportDesign);
                bool RSU = colRolPerdorues.merrRoleSipasPerdoruesiDheKodRoli(mySessionObjects.ktheIdPerdoruesi(Session), "RSU") == -1;

                //komentuar perkohesisht sa te mbarojne testimet
                //if (designSettings.ruajNeSession || designSettings.rollPaper || designSettings.autoWidth || designSettings.oldViewer)
                if (((System.Data.DataSet)report.DataSource).Tables[0].Rows.Count < 1000 || designSettings.ruajNeSession || designSettings.rollPaper || designSettings.autoWidth || designSettings.oldViewer)
                {
                    if(designSettings.autoWidth)
                        report.PrintingSystem.Document.AutoFitToPagesWidth = 1;
                    if (designSettings.rollPaper)
                        report.RollPaper = true;

                    report.CreateDocument();

                    if (designSettings.autoWidth)
                        SetAutoWidthToReport(report);
                }

                if (designSettings.ruajNeSession || designSettings.oldViewer || RSU)
                    SaveReport(report);

                if (designSettings.oldViewer)
                {
                    hfState.Set("oldViewer", true);
                    hfState.Set("reportPageCount", report.Pages.Count);
                    reportViewer2.Report = report;
                }
                else
                {
                    hfState.Set("oldViewer", false);
                    hfState.Set("reportPageCount", 0);
                    CachedReportSourceWeb cachedReport = new CachedReportSourceWeb(report);
                    reportViewer.OpenReport(cachedReport);
                }

            }
        }

        protected void btnRuaj_Click(object sender, EventArgs e)
        {
        }
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            AfishoRaport();
        }



        private colFilterKoka ktheFiltraRaporti()
        {
            return null;
        }

        protected void Apliko_ASPxButton_Click(object sender, EventArgs e)
        {

            //kap item qe ka template ne menune e kesaj faqeje
            DevExpress.Web.MenuItem itemButton = ASPxMenuToolBar.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            colFilterTrupi filtraTrupi = new colFilterTrupi(Convert.ToInt32(cmbFiltra.Value));
        }


        public void ASPxButtonFshiFilterOk_Click(object sender, EventArgs e)
        {
            DevExpress.Web.MenuItem itemButton = ASPxMenuToolBar.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
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
            percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), aSPxMenu1, MenuInfo, Ruaj_ASPxButton_Click, ASPxButtonFshiFilterOk_Click, null, null, DbCore.mySessionObjects.merrEshteMemeSesioni(Session), idNdermarrje, this, idPerdorues, idViti, RaportiEmerReal);
        }


        public static void percaktoTemplateMenu(int idgjuha, ASPxMenu aSPxMenu1, ASPxMenu MenuInfo, EventHandler Ruaj_ASPxButton_Click, EventHandler FshiFilter_ASPxButton_Click, EventHandler btnPo_Click, EventHandler btnJo_Click, bool meme, int idNdermarrje, Page page, int idperdorues, int idviti, string RaportiEmerReal)
        {
            DbCore.DbShare.colMenuItem menu = new DbCore.DbShare.colMenuItem(idgjuha);
            menu.merrMenuItemSipasKomponentes(idgjuha, 649);

            foreach (DbCore.DbShare.clsMenuItem m in menu)
            {
                if (m.Name != "ItemFilter" && m.Name != "ItemFrame" && m.Name != "ItemExport")
                {
                    if (!m.Enabled) continue;
                    clsToolbarConfig.ShtoMenuItem(page.Theme, aSPxMenu1, m);
                    if (m.Name == "Ruaj")
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = true;
                    if (m.Name == "Sinkronizo" && !meme)
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                    if (m.Name == "Trasfero" && !meme)
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                    if (m.Name == "Gjenero")
                    {
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                        if (RaportiEmerReal == "gjendjeArtikujshMinMaxSipasMagazines" || RaportiEmerReal== "KontrolliSkadencesArtikujve")
                        {
                            clsTeDrejtaRoli tedrejtaInfo = new clsTeDrejtaRoli();
                            
                                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idperdorues, idNdermarrje, idviti, RaportiEmerReal == "gjendjeArtikujshMinMaxSipasMagazines"?"GjeneroUrdherBlerje": "RegjistrimMagazine.aspx?lloj=dalje");
                            
                            if (tedrejtaInfo.DShtim)
                                aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = true;
                        }
                    }
                }
                else
                    if (m.Name == "ItemFilter")
                {
                    //krijohen handler per te caktuar evente server side per kontrolle
                    //keto handler i kalohen si parametra user control per filtrat
                    EventHandler handlerPerRuajFilter = new EventHandler(Ruaj_ASPxButton_Click);
                    EventHandler handlerPerFshiFilter = new EventHandler(FshiFilter_ASPxButton_Click);

                    //shtohet ne menu user control per filtrat e grides
                    clsToolbarConfig.ShtoMenuItemPerFilterRaport(page, aSPxMenu1, handlerPerRuajFilter, handlerPerFshiFilter);
                }

                else
                {
                    clsToolbarConfig.ShtoMenuItemPerFrame(page, aSPxMenu1, DbCore.clsFunksione.ktheUrlHelpi(m.UrlHelp).Item1);
                }
                if (m.Name == "Shto" || m.Name == "Ndihme" || m.Name == "ItemFilter" || m.Name == "ItemFrame" || m.Name == "Grupo" || m.Name == "Kerko" || m.Name == "Pastro" || m.Name == "ItemExport")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].BeginGroup = true;

                if (m.Name == "Arkiva")
                {
                    DbCore.DbAdmin.clsNdermarrje nd = new DbCore.DbAdmin.clsNdermarrje(idNdermarrje);
                    if (!nd.isArkiva) aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                }
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
                IdRaporti, ASPxCheckBoxLocalRaport.Checked, ASPxCheckBoxLocalPerdorues.Checked, ASPxCheckBoxLocalNdermarrje.Checked, 1);
            //krijohet obj i kokes se filtrit

            colFilterTrupi kolFilterTrupi = new colFilterTrupi();
            colKontrolle oColKontrolle = new colKontrolle();
            oColKontrolle.merrKontrolletRaporti(IdRaporti);
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

            if (!String.IsNullOrEmpty(Request.QueryString["radButon"]))
            {
                int dtDokPeriudhaZgjedhur = Convert.ToInt32(Request.QueryString["radButon"]);
                ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDok")).SelectedIndex = dtDokPeriudhaZgjedhur;
                ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDateLidhes")).SelectedIndex = dtDokPeriudhaZgjedhur;
                ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtKrijimi")).SelectedIndex = dtDokPeriudhaZgjedhur;
                ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDokUrdherPagese")).SelectedIndex = dtDokPeriudhaZgjedhur;
                ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtTakimi")).SelectedIndex = dtDokPeriudhaZgjedhur;
                ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDokAprovimit")).SelectedIndex = dtDokPeriudhaZgjedhur;
                ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDokQenderKosto")).SelectedIndex = dtDokPeriudhaZgjedhur;
                ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDokKonvertuar")).SelectedIndex = dtDokPeriudhaZgjedhur;
                ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDtFillimi")).SelectedIndex = dtDokPeriudhaZgjedhur;
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtNgaDokShitje")).Text = Request.QueryString["dateNga"].ToString();
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtNgaDokShitje")).EnableClientSideAPI = true;

            }
            if (!String.IsNullOrEmpty(Request.QueryString["dateNga"]))
            {
                string dataNga = Convert.ToString(Request.QueryString["dateNga"]);
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDokQenderKosto")).Text = dataNga;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDokQenderKosto")).EnableClientSideAPI = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDok")).Text = dataNga;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDok")).EnableClientSideAPI = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtNgaKrijimi")).Text = dataNga;
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtNgaKrijimi")).EnableClientSideAPI = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDttakimi")).Text = dataNga;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDttakimi")).EnableClientSideAPI = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDokUrdherPagese")).Text = dataNga;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDokUrdherPagese")).EnableClientSideAPI = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDokAprovimit")).Text = dataNga;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDokAprovimit")).EnableClientSideAPI = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDokKonvertuar")).Text = dataNga;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDokKonvertuar")).EnableClientSideAPI = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtNgaDtFillimi")).Text = dataNga;
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtNgaDtFillimi")).EnableClientSideAPI = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtNgaDokDateLidhes")).Text = dataNga;
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtNgaDokDateLidhes")).EnableClientSideAPI = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtNgaDokShitje")).Text = Request.QueryString["dateNga"].ToString();
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtNgaDokShitje")).EnableClientSideAPI = true;
            }
            if (!String.IsNullOrEmpty(Request.QueryString["dateDeri"]))
            {
                string dataDeri = Convert.ToString(Request.QueryString["dateDeri"]);
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDokQenderKosto")).Text = dataDeri;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDokQenderKosto")).Enabled = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDok")).Text = dataDeri;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDok")).Enabled = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtDeriKrijimi")).Text = dataDeri;
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtDeriKrijimi")).Enabled = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDttakimi")).Text = dataDeri;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDttakimi")).Enabled = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDokUrdherPagese")).Text = dataDeri;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDokUrdherPagese")).Enabled = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDokAprovimit")).Text = dataDeri;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDokAprovimit")).Enabled = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtDeriDokDateLidhes")).Text = dataDeri;
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtDeriDokDateLidhes")).Enabled = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDokKonvertuar")).Text = dataDeri;  
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDokKonvertuar")).Enabled = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtDeriDtFillimi")).Text = dataDeri; 
               ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtDeriDtFillimi")).Enabled = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtDeriDokShitje")).Text = Request.QueryString["dateDeri"].ToString();
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtDeriDokShitje")).Enabled = true;
            }
        }
        private void vendosVleraDefaultPerDateDokumentiPerRaporteTeVecante()
        {
            clsPeriudhaKontabel periudhaKontabel = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            if (RaportiEmerReal.EqualsAnyIgnoreCase("shitjeDitore", "shitjeDetyrimeshAutorizime", "shitjeArketimeDitoreAutorizime", "veprimtariaDitore", "Kartolina_ditelindjes_klientit"))
            {
                ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDok")).SelectedIndex = 1;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDok")).Date = DateTime.Today;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDok")).Date = DateTime.Today;
            }
            if (RaportiEmerReal.EqualsAnyIgnoreCase("permbledhesSipasKlientevePajisjeve", "analitikSipasKartave", "klienteMeKontrate", "maturimiKontrataShitje", "VeprimetEKlientitPerDatatMeTeFundit"))
            {   // vendos default "Gjithe vitet"
                ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDok")).SelectedIndex = 3;
            }
            if (RaportiEmerReal.EqualsAnyIgnoreCase("raportiBuxhetimit", "KartelaPunonjesveMePagesa"))
            {
                ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDok")).SelectedIndex = 2;
            }

            if (RaportiEmerReal.EqualsAnyIgnoreCase("historikuVeprimeve", "veprimeDitore", "HistorikuVeprimeveCmimet"))
            {
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtNgaRegj")).Date = DateTime.Today;
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtDeriRegj")).Date = DateTime.Today;
            }
            if (RaportiEmerReal.EqualsAnyIgnoreCase("grafikMarzhiShitje", "analizeAktiveQarkullues", "analizeArdhuraShpenzime", "analizaDetyrime",
                "deklarimArdhuraVjetore", "memoAnnualBonus", "regjistriVjetorPunonjes", "kontrataPensionit", "permbledhesKliente",
                "shitjeKlienteveIntervale", "analizeCmimeShitje", "analizaPorosive", "bonuseVjetoreTeKlienteve", "shitjeSipasMuajveGrafik",
                "regjisterAseteshRezervaRivleresimi", "ardhura_shpenzime_sipas_muajve", "Marreveshje_pensioni", "Rap_BuxhetiDheFondesh")

            )
                ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDok")).SelectedIndex = 2;


            if (RaportiEmerReal.EqualsAnyIgnoreCase("fushatShteseHistorikuVeprimeve"))
            {
                ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDok")).SelectedIndex = 1;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDok")).Date = new DateTime(1900, 01, 01);
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDok")).Date = new DateTime(9999, 12, 30);
                ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtRegj")).SelectedIndex = 0;
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtNgaRegj")).ClientEnabled = false;
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtDeriRegj")).ClientEnabled = false;
            }
            if (RaportiEmerReal.EqualsAnyIgnoreCase("ShitjetSipasDegeveAdministrative"))
            {
                ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDok")).SelectedIndex = 1;
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDok")).Date = new DateTime(1900, 01, 01);
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtDeriDok")).Date = DateTime.Now;
            }
            if (RaportiEmerReal.EqualsAnyIgnoreCase("Depreciation"))
            {

                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtNgaDokReg")).ClientEnabled = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtDeriDokReg")).ClientEnabled = true;
            }
            if (RaportiEmerReal.EqualsAnyIgnoreCase("porosiPromotions"))
            {

                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtNgaDokAfatKohor")).Date = periudhaKontabel.FillimiPeriudha;
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtDeriDokAfatKohor")).Date = periudhaKontabel.MbarimiPeriudha;
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtNgaDokAfatKohor")).ClientEnabled = false;
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtDeriDokAfatKohor")).ClientEnabled = false;
            }
            if (RaportiEmerReal.EqualsAnyIgnoreCase("gjendjeKerkeseArtikujshPerProdhim"))
            {

                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtNgaDokAfatKohor")).Date = periudhaKontabel.FillimiPeriudha;
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtDeriDokAfatKohor")).Date = periudhaKontabel.MbarimiPeriudha;
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtNgaDokAfatKohor")).ClientEnabled = false;
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtDeriDokAfatKohor")).ClientEnabled = false;
            }
            if (RaportiEmerReal.EqualsAnyIgnoreCase("regjistriVjetorPunonjes"))
            {

                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtNgaDtMbarimi")).ClientEnabled = true;
                ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtDeriDtMbarimi")).ClientEnabled = true;
            }
            if (RaportiEmerReal.EqualsAnyIgnoreCase("HistorikuTeDhenaveTePerfaqesuesveTeShitjes"))
            {
                ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtRegj")).SelectedIndex = 0;
            }

            if (RaportiEmerReal.EqualsAnyIgnoreCase("labourOfficeReport", "regjistriVjetorPunonjes"))
                ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDtFillimi")).SelectedIndex = 1;

            ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtNgaDtFillimi")).Date = new DateTime(1900, 01, 01);
            ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtDeriDtFillimi")).Date = new DateTime(9999, 12, 30);

            if (RaportiEmerReal == "regjistriVjetorPunonjes")
                ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtMbarimi")).SelectedIndex = 1;

            ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtNgaDtMbarimi")).Date = new DateTime(1900, 01, 01);
            ((ASPxDateEdit)navBarFiltrat.Groups[1].FindControl("txtDeriDtMbarimi")).Date = new DateTime(9999, 12, 30);

            if (RaportiEmerReal == "amendimKontratePune")
                ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDtFillimi")).SelectedIndex = 0;

            if (RaportiEmerReal.EqualsAnyIgnoreCase("analizeAktiveQarkullues", "analizeArdhuraShpenzime", "analizaDetyrime", "ardhura_shpenzime_sipas_muajve"))
                ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDokKrahasues")).SelectedIndex = 2;

            if (RaportiEmerReal == "maturimFaturaPerTuPaguarFinanca")
            {
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNrDok")).Visible = false;
                ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimDok1")).Visible = false;
                ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaDok")).Visible = false;
                ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimDok2")).Visible = false;
                ((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtNrDok1")).Visible = false;
                ((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtNrDok2")).Visible = false;

            }
            if (RaportiEmerReal == "regjistriIProduktitERecepturaveMeDetajime")
            {
                ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("btnDetajimiReceptura1")).Visible = true;
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDetajimiReceptura1")).Visible = true;
                ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("btnDetajimiReceptura2")).Visible = true;
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDetajimiReceptura2")).Visible = true;
                ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("btnDetajimiProdukti1")).Visible = true;
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDetajimiProdukti1")).Visible = true;
                ((ASPxButtonEdit)navBarFiltrat.Groups[1].FindControl("btnDetajimiProdukti2")).Visible = true;
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDetajimiProdukti2")).Visible = true;


            }
            if (RaportiEmerReal != "FaturaShitjeEinvoice" && RaportiEmerReal != "FaturaBlerjeEinvoice")
            {
                ((ASPxTextBox)navBarFiltrat.Groups[1].FindControl("txtEIC")).Visible = false;
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblEIC")).Visible = false;
                ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbStatusiEinvoice")).Visible = false;
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblStatusiEinvoice")).Visible = false;


            }

            if (RaportiEmerReal == "Rap_GjendjaBuxhetit")
            {
                ((ASPxDateEdit)navBarFiltrat.Groups[0].FindControl("txtNgaDok")).Enabled = false;
            }
            if (RaportiEmerReal == "karteleKlienti" || RaportiEmerReal == "situacionKlienti")
            {
                ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbKontabilizuar")).SelectedIndex = 1;
            }
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave()
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


            if (RaportiEmerReal == "birthdayCard")
                ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblDtDok")).Text = rm.GetString("labelRaportData", ci);

            else if (RaportiEmerReal == "kontrataPensionit" || RaportiEmerReal == "Marreveshje_pensioni")
                ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblDtDok")).Text = rm.GetString("filterDtDokCmimArtikulli", ci);

            else if (RaportiEmerReal == "klienteMeKontrate")
                ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblDtDok")).Text = rm.GetString("filterDtMbarimi", ci);

            else if (RaportiEmerReal == "LargimetPerfaqesuesveTeShitjesVjetore" || RaportiEmerReal == "LargimetPerfaqesuesveTeShitjesMujore")
                ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblDtDok")).Text = rm.GetString("labelPeriudhaEng", ci);
            else
                ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblDtDok")).Text = rm.GetString("labelFilterKryesorDtDokumenti", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDok")).Items[0].Text = rm.GetString("RadioButtonListEditItemAktuale", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDok")).Items[1].Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDok")).Items[2].Text = rm.GetString("RadioButtonListEditItemVitiUshtrimor", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtDok")).Items[3].Text = rm.GetString("RadioButtonListEditItemGjitheVitet", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDtKrijimi")).Text = rm.GetString("labelFilterAvancuarDateKrijimi", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtKrijimi")).Items[0].Text = rm.GetString("RadioButtonListEditItemAktuale", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtKrijimi")).Items[1].Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtKrijimi")).Items[2].Text = rm.GetString("RadioButtonListEditItemVitiUshtrimor", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtKrijimi")).Items[3].Text = rm.GetString("RadioButtonListEditItemGjitheVitet", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblOreKrijimi")).Text = rm.GetString("labelFilterAvancuarOreKrijimi", ci);

            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblDtTakimi")).Text = rm.GetString("lblDataTakimit", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtTakimi")).Items[0].Text = rm.GetString("RadioButtonListEditItemDitore", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtTakimi")).Items[1].Text = rm.GetString("RadioButtonListEditItemJavore", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtTakimi")).Items[2].Text = rm.GetString("RadioButtonListEditItemAktuale", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtTakimi")).Items[3].Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[0].FindControl("radDtTakimi")).Items[4].Text = rm.GetString("RadioButtonListEditItemVitiUshtrimor", ci);
            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblNgaDtTakimi")).Text = rm.GetString("labelRaportiNga", ci);
            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblDeriDttakimi")).Text = rm.GetString("labelRaportDeri", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGrupimQK")).Text = rm.GetString("lblGrupimQK", ci);
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

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDraft")).Text = rm.GetString("labelDokumentaDraft", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDrejtuar")).Text = rm.GetString("lblDrejtuar", ci);

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
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim1NP")).Items[7].Text = rm.GetString("cmbboxItemFilterKryeBenPjeseNe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaNP")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaNP")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim2NP")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim2NP")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim2NP")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim2NP")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprim2NP")).Items[7].Text = rm.GetString("cmbboxItemFilterKryeBenPjeseNe", ci);

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
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNrLlogBankare")).Text = rm.GetString("lblNrLlogBankare", ci);

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

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblTipKontrate")).Text = rm.GetString("lblRaportTipKontrate", ci);

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

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDtDokFillimiVod")).Text = rm.GetString("filterStartDate", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("DtDokFillimiVod")).Items[0].Text = rm.GetString("RadioButtonListEditItemAktuale", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("DtDokFillimiVod")).Items[1].Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("DtDokFillimiVod")).Items[2].Text = rm.GetString("RadioButtonListEditItemVitiUshtrimor", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNgaDokFillimiVod")).Text = rm.GetString("labelRaportiNga", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDeriDokFillimiVod")).Text = rm.GetString("labelRaportDeri", ci);


            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDtDokLargimiVod")).Text = rm.GetString("filterLeaveDate", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("DtDokLargimiVod")).Items[0].Text = rm.GetString("RadioButtonListEditItemAktuale", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("DtDokLargimiVod")).Items[1].Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("DtDokLargimiVod")).Items[2].Text = rm.GetString("RadioButtonListEditItemVitiUshtrimor", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNgaDokLargimiVod")).Text = rm.GetString("labelRaportiNga", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDeriDokLargimiVod")).Text = rm.GetString("labelRaportDeri", ci);


            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDtDokShitje")).Text = rm.GetString("labelFilterKryesorDtDokumentiShitje", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDokShitje")).Items[0].Text = rm.GetString("RadioButtonListEditItemAktuale", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDokShitje")).Items[1].Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDokShitje")).Items[2].Text = rm.GetString("RadioButtonListEditItemVitiUshtrimor", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNgaDokShitje")).Text = rm.GetString("labelRaportiNga", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDeriDokShitje")).Text = rm.GetString("labelRaportDeri", ci);


            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDtFillimi")).Text = RaportiEmerReal == "amendimKontratePune" ? rm.GetString("RadioButtonListEditItemPeriudha", ci) : rm.GetString("lblDtFillimi", ci);
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


            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDokLidhes")).Text = rm.GetString("lblDokLidhes", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDateLidhes")).Items[0].Text = rm.GetString("RadioButtonListEditItemAktuale", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDateLidhes")).Items[1].Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDateLidhes")).Items[2].Text = rm.GetString("RadioButtonListEditItemVitiUshtrimor", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDateLidhes")).Items[3].Text = rm.GetString("RadioButtonListEditItemGjitheVitet", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNgadateLidhes")).Text = rm.GetString("labelRaportiNga", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDeriDateLidhes")).Text = rm.GetString("labelRaportDeri", ci);


            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDtDokLidhes")).Text = rm.GetString("labelFilterAvancuarDtDokLidhes", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDokLidhes")).Items[0].Text = rm.GetString("RadioButtonListEditItemAktuale", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDokLidhes")).Items[1].Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDokLidhes")).Items[2].Text = rm.GetString("RadioButtonListEditItemVitiUshtrimor", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radDtDokLidhes")).Items[3].Text = rm.GetString("RadioButtonListEditItemGjitheVitet", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNgaDokLidhes")).Text = rm.GetString("labelRaportiNga", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDeriDokLidhes")).Text = rm.GetString("labelRaportDeri", ci);


            if (RaportiEmerReal == "produktetSipasPorosiveUnivers")
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNrDok")).Text = rm.GetString("lblRaportNumerPorosie", ci);
            else if (RaportiEmerReal == "permbledhesSipasKlientevePajisjeve" || RaportiEmerReal == "analitikSipasKartave")
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNrDok")).Text = rm.GetString("filterNrKarte", ci);
            else

                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblKategorizimArtikuj")).Text = rm.GetString("filterKategorizimArtikuj", ci);
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

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNrSerial")).Text = rm.GetString("labelFilterAvancuarNrSerial", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrSerial1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrSerial1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrSerial1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrSerial1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaSerial")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaSerial")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrSerial2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrSerial2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrSerial2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimNrSerial2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGrada")).Text = rm.GetString("lblGrada", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimGrada1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimGrada1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimGrada1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimGrada1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaGrada")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaGrada")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimGrada2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimGrada2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimGrada2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimGrada2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblVjetersiaVite")).Text = rm.GetString("lblVjetersiaVite", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimVjetersiaVite1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimVjetersiaVite1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimVjetersiaVite1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimVjetersiaVite1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaVjetersiaVite")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaVjetersiaVite")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimVjetersiaVite2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimVjetersiaVite2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimVjetersiaVite2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimVjetersiaVite2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            //per raportin Produktet sipas porosive i vendosim pershkrim tjeter
            if (RaportiEmerReal == "produktetSipasPorosiveUnivers")
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNrDokKonvertuar")).Text = rm.GetString("lblRaportNumerProdhimi", ci);
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

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblLlojFaze")).Text = rm.GetString("labelLlojFaze", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLlojFaze")).Items[0].Text = rm.GetString("labelRaportMujore", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLlojFaze")).Items[1].Text = rm.GetString("cmbItemTreMujore", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLlojFaze")).Items[2].Text = rm.GetString("cmbItemKaterMujore", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLlojFaze")).Items[3].Text = rm.GetString("cmbItemGjashteMujore", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLlojFaze")).Items[4].Text = rm.GetString("cmbItemVjecare", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblAktivitetiKlient")).Text = rm.GetString("lblAktivitetiKlient", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAktivitetiKlient1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAktivitetiKlient1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAktivitetiKlient1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAktivitetiKlient1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesAktivitetiKlient")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesAktivitetiKlient")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAktivitetiKlient2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAktivitetiKlient2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAktivitetiKlient2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAktivitetiKlient2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

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

            if(RaportiEmerReal == "gjendjaLlogarive")
              ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblLlogariaSAP")).Text = rm.GetString("labelRaportiShenime", ci) + " 5";
            else
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblLlogariaSAP")).Text = rm.GetString("labelFilterAvancuarPershkLlogariaSAP", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1LlogariaSAP")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1LlogariaSAP")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1LlogariaSAP")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1LlogariaSAP")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaLlogariaSAP")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaLlogariaSAP")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2LlogariaSAP")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2LlogariaSAP")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2LlogariaSAP")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2LlogariaSAP")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);



            if (RaportiEmerReal == "ndryshimiKostosSeArtikujve")
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblKlientFurnitori")).Text = rm.GetString("filterFurnitorArt", ci);
            else if (RaportiEmerReal == "veprimtariaDitore")
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblKlientFurnitori")).Text = rm.GetString("labelRaportKlienti", ci);
            else if (RaportiEmerReal == "kartelaKlienteveMeMarreveshje")
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblKlientFurnitori")).Text = rm.GetString("labelRaportKlienti", ci);
            else
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

            if (RaportiEmerReal == "veprimtariaDitore")
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblFurnArt")).Text = rm.GetString("lblRaportFurnitori", ci);
            else
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


            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblLlojDokLidhes")).Text = RaportiEmerReal == "analizeQendraKostosh" ? rm.GetString("labelFilterAvancuarLlojDokGjenerues", ci) : rm.GetString("labelFilterAvancuarLlojDokLidhes", ci);
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


            if (RaportiEmerReal == "HistorikuTeDhenaveTePerfaqesuesveTeShitjes")
            {
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblmagazina")).Text = rm.GetString("labelShopCode", ci);
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblPershkrimMag")).Text = rm.GetString("labelShopName", ci);
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblStatusiDok")).Text = rm.GetString("labelStatus", ci);
            }
            else
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblmagazina")).Text = rm.GetString("labelFilterAvancuarMagazina", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblKartaKlient")).Text = rm.GetString("labelFilterAvancuarKartaKlienti", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblPolitike")).Text = rm.GetString("labelPolitike", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblnjesiVartese")).Text = rm.GetString("labelNjesiVartese", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblKategori")).Text = rm.GetString("labelKategori", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblmagazinades")).Text = rm.GetString("labelFilterAvancuarMagazinaDes", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblMagazinaPaLidhese")).Text = rm.GetString("labelFilterAvancuarMagazina", ci);


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
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDateKontrate")).Text = rm.GetString("lblDateKontrate", ci);

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

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblShenimeShitjeje")).Text = rm.GetString("filterRaportShenime2Shitje", ci);
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

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblShenimeShitje")).Text = rm.GetString("filterRaportShenimeShitje", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiShenimeShitjeje1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiShenimeShitjeje1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiShenimeShitjeje1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiShenimeShitjeje1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaShenimeShitje")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaShenimeShitje")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiShenimeShitje2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiShenimeShitje2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiShenimeShitje2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiShenimeShitje2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

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
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblgrupeBanke")).Text = rm.GetString("lblGrupBanke", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("grupimDyteLabel")).Text = rm.GetString("labelFilterAvancuarGrupimD", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("grupimTreteLabel")).Text = rm.GetString("labelFilterAvancuarGrupimT", ci);
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

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblLlojFushaShtese")).Text = rm.GetString("labelFilterLlojFushaShtese", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblModeliFushaShtese")).Text = rm.GetString("labelFilterModeliFushaShtese", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblObjekteGis")).Text = rm.GetString("labelFilterObjekteGis", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGrupPArt")).Text = rm.GetString("labelFilterAvancuarGrupimP", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1GrupPArt")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1GrupPArt")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1GrupPArt")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1GrupPArt")).Items[4].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaGrupimPArt")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaGrupimPArt")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2GrupPArt")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2GrupPArt")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2GrupPArt")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2GrupPArt")).Items[4].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);


            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGrupDArt")).Text = rm.GetString("labelFilterAvancuarGrupimD", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1GrupDArt")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1GrupDArt")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1GrupDArt")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi1GrupDArt")).Items[4].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaGrupimDArt")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaGrupimDArt")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2GrupDArt")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2GrupDArt")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2GrupDArt")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimi2GrupDArt")).Items[4].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);



            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblMonedha")).Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblMonedhaQk")).Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGrupi")).Text = rm.GetString("labelFilterAvancuarGrupi", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNengrupi")).Text = rm.GetString("labelFilterAvancuarNengrupi", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblTipi")).Text = rm.GetString("labelFilterAvancuarTipi", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNiveli")).Text = rm.GetString("labelFilterAvancuarNengrupi", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDtMaturimi")).Text = rm.GetString("labelFilterAvancuarDtMaturimi", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblIntervalet")).Text = rm.GetString("labelFilterAvancuarIntervalet", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblFormati")).Text = rm.GetString("labelFilterAvancuarFormati", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblLlogKoresp")).Text = rm.GetString("labelFilterAvancuarLlogKoresp", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblMonedheKF")).Text = rm.GetString("labelFilterAvancuarMonedheKF", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblBuxhetSipasKapitujve")).Text = rm.GetString("labelShfaqBuxhetSipasKapitujve", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblshitjeKomisionZero")).Text = rm.GetString("labelshitjeKomisionZero", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblKonvertuarNgakontrata")).Text = rm.GetString("lblKonvertuarNgaKontrata", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblKontabilizuar")).Text = rm.GetString("labelFilterKontabilizuar", ci);
            if (((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblMonedheLl")) != null)
            { ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblMonedheLl")).Text = rm.GetString("labelFilterAvancuarMonedheLl", ci); }
            if (((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGrupoSipasGrupim1")) != null)
            { ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGrupoSipasGrupim1")).Text = rm.GetString("labelFilterAvancuarSipasGrupim1", ci); }


            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGjendjaLlog")).Text = rm.GetString("labelFilterAvancuarGjendjaLlog", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbGjendjaLlog")).Items[1].Text = rm.GetString("cmbboxItemFilterAvancDebitore", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbGjendjaLlog")).Items[2].Text = rm.GetString("cmbboxItemFilterAvancKreditore", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblVeprimePeriudhe")).Text = rm.GetString("lblVeprimePeriudhe", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblStatusCRM")).Text = rm.GetString("labelFilterAvancuarStatusi", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGjendjeDetyrime")).Text = rm.GetString("labelRaportGjendje", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblqyteti")).Text = rm.GetString("labelFilterAvancuarQyteti", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimQyteti1")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimQyteti1")).Items[2].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaQyteti")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaQyteti")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimQyteti2")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimQyteti2")).Items[2].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblPajisje")).Text = rm.GetString("labelFilterAvancuarPajisje", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimPajisje1")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimPajisje1")).Items[2].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaPajisje")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaPajisje")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimPajisjei2")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimPajisjei2")).Items[2].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

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


            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblIdPunonjes")).Text = rm.GetString("lblIdPunonjes", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimIdPunonjes1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimIdPunonjes1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimIdPunonjes1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimIdPunonjes1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaPunonjes")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaPunonjes")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimIdPunonjes2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimIdPunonjes2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimIdPunonjes2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimIdPunonjes2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);


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
            if ((RaportiEmerReal == "MZHUQendraKostoPermbledhes") || (RaportiEmerReal == "MZHUKostoPerVit") || (RaportiEmerReal == "MZHUKostoPerBanor")) ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDegeAdministrative")).Text = rm.GetString("labelBashkia", ci);

            if (RaportiEmerReal == "blerjetsipasfurnitorevestatistikor")
            {
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblAdreseKlienti")).Text = rm.GetString("filterAdreseFurnitori", ci);
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGrupPKF")).Text = rm.GetString("labelFilterAvancuarGrupim1Furnitor", ci);
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGrupDKF")).Text = rm.GetString("labelFilterAvancuarGrupim2Furnitor", ci);
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGrupTKF")).Text = rm.GetString("labelFilterAvancuarGrupim3Furnitor", ci);
            }
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDegeAdminPaLidhese")).Text = rm.GetString("labelFilterAvancuarDegeAdministrative", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNjesia")).Text = rm.GetString("labelFilterAvancuarNjesia", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbNjesia")).Items[1].Text = rm.GetString("cmbboxItemFilterAvancNjesiaP", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbNjesia")).Items[2].Text = rm.GetString("cmbboxItemFilterAvancNjesiaD", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblPeriudheRaportimi")).Text = rm.GetString("labelPeriudheRaportimi", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("cmimiLabel")).Text = rm.GetString("labelFilterAvancuarCmimi", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbCmimeArtikulli")).Items[0].Text = rm.GetString("cmbboxItemFilterAvancCmimiP", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbCmimeArtikulli")).Items[1].Text = rm.GetString("cmbboxItemFilterAvancCmimiD", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblArtCmim")).Text = rm.GetString("labelFilterArtCmim", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbArtCmim")).Items[0].Text = rm.GetString("cmbboxItemFilterAvancTeGjithe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbArtCmim")).Items[1].Text = rm.GetString("cmbItemFilterArtCmim", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbArtCmim")).Items[2].Text = rm.GetString("cmbItemFilterArtPaCmim", ci);

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
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLlojSubjekti")).Items[4].Text = rm.GetString("comboItemBlerjeShitjePunonjes", ci);



            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblLlojSubjektiKF")).Text = rm.GetString("filterLlojSubjekti", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLlojSubjektiKF")).Items[1].Text = rm.GetString("comboItemBlerjeShitjeKlient", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLlojSubjektiKF")).Items[2].Text = rm.GetString("comboItemBlerjeShitjeFurnitor", ci);
          


            if (RaportiEmerReal == "ArketimetOrare" || RaportiEmerReal == "pagesaDealer" || RaportiEmerReal == "listeArketimeAnullime" || RaportiEmerReal == "postPaidPayments" || RaportiEmerReal == "billPaymentsPerDay" || RaportiEmerReal == "dailyGuaranteePayment" || RaportiEmerReal == "arketimeDitore" || RaportiEmerReal == "listeArketimeAnullimePostpaid")
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblArkaBankaEmra")).Text = rm.GetString("labelFilterAvancuarDyqan", ci);

            else if (RaportiEmerReal == "listepagesaBanka")
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
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblShfaqGrup")).Text = rm.GetString("labelShfaqGrup", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblShfaqVlerat")).Text = rm.GetString("labelFilterAvancuarVlerat", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblTipGrafiku")).Text = rm.GetString("labelFilterAvancuarLlojGrafiku", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblMbylljeViti")).Text = rm.GetString("labelFilterAvancuarMbylljeViti", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblShperndarjeDhuratash")).Text = rm.GetString("labelFilterAvancuarShperndarjeDhuratash", ci);
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

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGrup01")).Text = rm.GetString("filterGrupimDokArsye", ci);

            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupP01")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupP01")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupP01")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupP01")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaGrup01")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaGrup01")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupP02")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupP02")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupP02")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiGrupP02")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);



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
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblShfaqArt")).Text = rm.GetString("labelFilterAvancuarShfaq", ci);
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

            if (RaportiEmerReal != "HistorikuTeDhenaveTePerfaqesuesveTeShitjes")
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblPerdorues")).Text = rm.GetString("filterRaportPerdoruesi", ci);
            else
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblPerdorues")).Text = rm.GetString("lblPerdoruesVod", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblFilterKrijuesi")).Text = rm.GetString("filterRaportKrijuesi", ci);

            lblMsgbox.Text = rm.GetString("labelRaportMsgboxFshiRaport", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblPeriudheMaturimi")).Text = rm.GetString("labelFilterAvancuarDtMaturimi", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radPeriudheMaturimi")).Items[0].Text = rm.GetString("RadioButtonListEditItemAktuale", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radPeriudheMaturimi")).Items[1].Text = rm.GetString("RadioButtonListEditItemPeriudha", ci);
            ((ASPxRadioButtonList)navBarFiltrat.Groups[1].FindControl("radPeriudheMaturimi")).Items[2].Text = rm.GetString("RadioButtonListEditItemVitiUshtrimor", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNgaPeriudheMaturimi")).Text = rm.GetString("labelRaportiNga", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDeriPeriudheMaturimi")).Text = rm.GetString("labelRaportDeri", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblPershkrimFature")).Text = rm.GetString("filterRaportPershkrimFature", ci) + ":";
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblTarga")).Text = rm.GetString("labelRaportTarga", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblShoferi")).Text = rm.GetString("filterRaportiShoferi", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDega")).Text = rm.GetString("filterDega", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblLlogDebiProcredit")).Text = rm.GetString("filterllogaridebiprocredit", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblLlogKrediProcredit")).Text = rm.GetString("filterllogarikrediprocredit", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblAgjentShitje")).Text = rm.GetString("filterRaportiAgjentetShitjes", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblEmMbShoqeri")).Text = rm.GetString("filterRaportiShoqeria", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblNrTel")).Text = rm.GetString("filterRaportiNrTel", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblPaguar")).Text = rm.GetString("filterPaguar", ci);
            ((ASPxLabel)navBarFiltrat.Groups[0].FindControl("lblViti")).Text = rm.GetString("koloneLoginNdermarrjeViti", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblEmertimLlog")).Text = rm.GetString("filterEmertimLlog", ci);
            if (RaportiEmerReal == "PagesatEKryeraPerMPesa")
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblCustomerNumber")).Text = rm.GetString("filterSrNumber", ci);
            if (RaportiEmerReal == "gjendjePermbledhurKartaKlienti")
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGjendja")).Text = rm.GetString("lblcmbGjendjeKarte", ci);
            else
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGjendja")).Text = rm.GetString("filterGjendja", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lbShfaqLlogP")).Text = rm.GetString("filterLlogariPacaktuar", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblSasiaPakonvertuar")).Text = rm.GetString("filterSasiaPakonvertuar", ci);



            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblVlereShitje")).Text = rm.GetString("labelRaportVlera", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDeriVlereSh")).Text = rm.GetString("filterRaportiDeri", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblStatusKonvertimiDyte")).Text = rm.GetString("filterStatusKonvertimi", ci);

            switch (RaportiEmerReal)
            {
                case "gjendjaPermbledhurArkesNivelRaportues":
                case "gjendjaPermbledhurBankesNivelRaportues":
                case "permbledheseGrupetNivelRaportues":
                case "gjendjaAseteveSipasNdermarrjeve":
                case "kartelaPermbledheseArtikullitSipasNdermarrjeve":
                case "gjendjaMagazinesNivelRaportues":
                case "situacionPermbledhesKlienteNivelRaportues":
                case "situacionPermbledhesFurnitorNivelRaportues":
                case "permbledhesAseteNivelRaportues":
                case "bilanciEnergjitikPermbledhes":
                case "pasqyraSigurimeveRaportuese":
                case "ditariTotalBankaRaportues":
                    ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblKompania")).Text = rm.GetString("filterRaportIdNdermarje", ci);
                    break;

                case "pagesaDealer":
                    ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblKompania")).Text = rm.GetString("koloneLoginNdermarrjeNdermarrja", ci);
                    break;
                case "daljeNgaDyqani":
                case "maturimFaturaPerTuPaguarFinanca":
                case "shitjeAparatesh":
                case "ArketimetOrare":
                case "maturimFaturaPerTuPaguar":
                case "LargimetPerfaqesuesveTeShitjesVjetore":
                case "LargimetPerfaqesuesveTeShitjesMujore":
                case "HistorikuTeDhenaveTePerfaqesuesveTeShitjes":
                case "listeArketimeAnullimePostpaid":
                    ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblKompania")).Text = "Dealer";
                    break;
                default:
                    ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblKompania")).Text = (RaportiEmerReal == "daljeNgaDyqani") ? "Dealer" : rm.GetString("filterKompania", ci);
                    break;
            }


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
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGrupoKlientSipas")).Text = rm.GetString("filterGrupoKlientSipas", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGrupoArtikullSipas")).Text = rm.GetString("filterGrupoSipasArt", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblKrahasimKosto")).Text = rm.GetString("filterKrahasimKosto", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblKlientAktiv")).Text = rm.GetString("filterKlientAktiv", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblFilterKatAktive")).Text = rm.GetString("filterKatShpAktive", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblCmimMePaTVSH")).Text = rm.GetString("labelCmimi", ci);
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

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblKategoriSeriali")).Text = rm.GetString("filterlblKategoriSeriali", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKatSeriali1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKatSeriali1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKatSeriali1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKatSeriali1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaKatSeriali")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaKatSeriali")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKatSeriali2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKatSeriali2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKatSeriali2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimiKatSeriali2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);



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
            if (RaportiEmerReal == "kartelaKartaMePike" || RaportiEmerReal == "gjendjePermbledhurKartaKlienti")
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblKlientVartes")).Text = rm.GetString("filterKlientKarte", ci);
            else if (RaportiEmerReal == "situacioniklientevememarreveshje")
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblKlientVartes")).Text = rm.GetString("cmbLlojiKlient", ci);
            else
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblKlientVartes")).Text = rm.GetString("filterKlientVartes", ci);

            if (RaportiEmerReal == "kartelaFurnitorit")
                ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblAdreseKlienti")).Text = rm.GetString("filterAdreseFurnitor", ci);
            else
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

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblAktivitetiKlient")).Text = rm.GetString("filterAktivitetiKlient", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAktivitetiKlient1")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAktivitetiKlient1")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAktivitetiKlient1")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAktivitetiKlient1")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesAktivitetiKlient")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesAktivitetiKlient")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAktivitetiKlient2")).Items[3].Text = rm.GetString("cmbboxItemFilterKryeNdryshem", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAktivitetiKlient2")).Items[4].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAktivitetiKlient2")).Items[5].Text = rm.GetString("cmbboxItemFilterKryeMbaron", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimAktivitetiKlient2")).Items[6].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);

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
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblKapitulli")).Text = rm.GetString("labelRaportKapitulli", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblProgrami")).Text = rm.GetString("labelProgrami", ci);
            if (RaportiEmerReal == "analitikShitje") ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblStatus")).Text = rm.GetString("filterStatus", ci);
            else if (RaportiEmerReal == "dokumentaIntegrimi") ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblStatus")).Text = rm.GetString("labelFilterAvancuarStatusi", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblReceptura")).Text = rm.GetString("filterRecepturaNenprodukte", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblgrupoKatShpenzim")).Text = rm.GetString("labelGrupoSipasKategorisePrind", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGrupKosto")).Text = rm.GetString("labelRaportiLloji", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblElemLayer")).Text = rm.GetString("lblElemLayer", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblLlojiElem")).Text = rm.GetString("lblLlojiElem", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblStatusElem")).Text = rm.GetString("labelFilterAvancuarStatusi", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblStatusRezervimi")).Text = rm.GetString("labelFilterAvancuarStatusi", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGrupKosto")).Text = rm.GetString("shfaqMagVartese", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblArtikullIVjeter")).Text = rm.GetString("lblAfishoArtikuj", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblStatusShperndarje")).Text = rm.GetString("filterStatusShperndarje", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblStatusRiparimi")).Text = rm.GetString("filterStatusRiparimi", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblShenimeLP")).Text = rm.GetString("filterRaportShenimeShitje", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lbldogana")).Text = rm.GetString("lbldogana", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGrupimiKF1")).Text = rm.GetString("labelFilterAvancuarGrupim1KF", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGrupimiKF2")).Text = rm.GetString("labelFilterAvancuarGrupim2KF", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblGrupimiKF3")).Text = rm.GetString("labelFilterAvancuarGrupim3KF", ci);

            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDpshMagazina")).Text = rm.GetString("filterMagazina", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDpshKategShpenzimi")).Text = rm.GetString("MenuItemKategoriShpenzimi", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDpshNrLlogarie")).Text = rm.GetString("labelFilterAvancuarNrLlogarie", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDpshKlientFurnitor")).Text = rm.GetString("labelFilterKryeAvancKlientFurnitor", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDpshShenime")).Text = rm.GetString("filterRaportShenimeShitje", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblTac")).Text = rm.GetString("filterRaportTac", ci);
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblMarveshja")).Text = rm.GetString("filterIdMarveshje", ci);
            //((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblDpshNrDokumenti")).Text = rm.GetString("labelFilterAvancuarNrDok", ci);


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
            hfgjuha.Add("lblDokLidhes", rm.GetString("lblDokLidhes", ci));
            hfgjuha.Add("lblAdreseKlienti", rm.GetString("filterAdreseKlienti", ci));
            //  ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblnrDokIntegrimi")).Text = rm.GetString("labelFilterAvancuarNrDok", ci);


            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaLlojVeprimi")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeDhe", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbLidhesaLlojVeprimi")).Items[2].Text = rm.GetString("cmbboxItemFilterKryeOse", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimLlojVeprimi1")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimLlojVeprimi1")).Items[2].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimLlojVeprimi2")).Items[1].Text = rm.GetString("cmbboxItemFilterKryeFillon", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbVeprimLlojVeprimi2")).Items[2].Text = rm.GetString("cmbboxItemFilterKryePermban", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbKategorizimArtikuj")).Items[1].Text = rm.GetString("lblArtNeProgram", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbKategorizimArtikuj")).Items[2].Text = rm.GetString("lblArtJoNeProgram", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbKategorizimArtikuj")).Items[3].Text = rm.GetString("lblArtJoNeMagazine", ci);
            ((ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbKategorizimArtikuj")).Items[4].Text = rm.GetString("lblArtPerbashket", ci);
        }

        private void SaveReport(XtraReport report)
        {
            mySessionObjects.ruajMyReportNeSession(Session, hfState.Get("guidString").ToString(), report);
        }
        private XtraReport GetReport()
        {
            return mySessionObjects.merrMyReportNgaSessioni<XtraReport>(Session, hfState.Get("guidString").ToString());
        }

        //Report Viewer HTML5
        protected void ASPxCallbackPanel1_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
        {
            string parameter = Convert.ToString(e.Parameter);
            switch (parameter)
            {
                case "changeDesign":
                    ChangeDesignRaporti();
                    break;
                case "changeOrientation":
                    ChangeReportOrientation();
                    break;
                case "Shiko":
                case "changeStyle":
                    AfishoRaport();
                    break;
                default:
                    if (Convert.ToBoolean(hfState.Get("oldViewer")))
                        AlphaWebReports.raporteUtil.HapRaportDetails(source, e, GetReport(), reportViewer2);
                    else
                        AlphaWebReports.raporteUtil.HapRaportDetails(source, e, GetReport(), reportViewer);
                    break;
            }
        }

        private void AfishoRaport()
        {
            clsFunksione.dergoLogAlphaweb(new clsNdermarrje(IdNdermarrja).NdermarrjeKodi, "Hapje raporti", RaportiEmerReal, clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar());
            afisho(base.IdRaporti, IdNdermarrja, IdViti, IdNdermarrjeVit, mySessionObjects.merrPeriudheKontabel(base.Session), IdPerdoruesi, clsRaporti.KaSubRaporte(base.IdRaporti), hfState.Get("guidString").ToString());
        }

        private void ChangeReportOrientation()
        {
            AfishoRaport();
        }

        private void ChangeDesignRaporti()
        {
            clsRaportDesign rapDes = new clsRaportDesign(IdReportDesign);
            SetOrientations(rapDes);
            AfishoRaport();
        }
    }
}