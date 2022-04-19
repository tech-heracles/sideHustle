using DbCore;
using DbCore.DbAdmin;
using DbCore.DbShare;
using DevExpress.Web;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Web.UI;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class ImbReportToolbar : System.Web.UI.UserControl
    {

        #region properties
        /// <summary>
        /// ClientInstanceName i ReportViewerit
        /// </summary>        
        public string ReportViewerClientID
        {
            get { return HfState.Get("ReportViewerClientID").ToString(); }
            set { HfState.Set("ReportViewerClientID", value); }
        }
        public string BottomToolbarVisible
        {
            get
            {
                if (HfState.Contains("BottomToolbarVisible"))
                    return HfState.Get("BottomToolbarVisible").ToString();
                else
                {
                    HfState.Set("BottomToolbarVisible", "False");
                    return "False";
                }
            }
            set { HfState.Set("BottomToolbarVisible", value); }
        }
        public string ArsyeReload
        {
            get
            {
                if (HfState.Contains("arsyeReload"))
                    return HfState.Get("arsyeReload").ToString();
                else
                    return "";
            }
            set { HfState.Set("arsyeReload", value); }
        }

        public string ClientInstanceName
        {
            get { return HfState.Get("ClientInstanceName").ToString(); }
            set { HfState.Set("ClientInstanceName", value); }
        }
        /// <summary>
        /// Emri i file-it te stilit te raportit
        /// </summary>
        public string ReportStyle
        {
            get { return HfState.Get("reportStyle").ToString(); }
            set { HfState.Set("reportStyle", value); }
        }
        /// <summary>
        /// Faktori i zoomit. Page width apo vlere tjeter
        /// </summary>
        public int zoomFactor
        {
            get
            {
                if (HfState.Contains("zoomFactor"))
                    return Convert.ToInt32(HfState.Get("zoomFactor"));
                else
                    return -1;
            }
            set { HfState.Set("zoomFactor", value); }
        }
        /// <summary>
        /// Shkalla e zoom-imit. 
        /// </summary>
        public float Scale
        {
            get
            {
                if (HfState.Contains("scale"))
                    return Convert.ToSingle(HfState.Get("scale"), new System.Globalization.CultureInfo("en-US"));
                else
                    return -1;
            }
            set { HfState.Set("scale", value); }
        }
        /// <summary>
        /// Gjeresia e raportit perdoret per zoomimin PageWidth
        /// </summary>
        public int initialPageWidth
        {
            get
            {

                if (HfState.Contains("initialPageWidth"))
                    return Convert.ToInt32(HfState.Get("initialPageWidth"));
                else
                    if (Orientimi == clsRaportDesign._rap_portrait)
                    return 850;
                else
                    return 1100;

            }
            set
            {
                HfState.Set("initialPageWidth", value);
            }
        }
        public int InitialPageHeight
        {
            get
            {
                if (HfState.Contains("initialPageHeight"))
                    return Convert.ToInt32(HfState.Get("initialPageHeight"));
                else
                    if (Orientimi == clsRaportDesign._rap_portrait)
                    return 1100;
                else
                    return 850;
            }
            set { HfState.Set("initialPageHeight", value); }
        }
        public int windowWidth
        {
            get
            {

                if (HfState.Contains("windowWidth"))
                    return Convert.ToInt32(HfState.Get("windowWidth"));
                else
                    return -1;

            }
            set { HfState.Set("windowWidth", value); }
        }
        /// <summary>
        /// Orientimi
        /// </summary>
        public string Orientimi
        {
            get { return HfState.Get("orientimi").ToString(); }
            set { HfState.Set("orientimi", value); }
        }
        /// <summary>
        /// idGjuha
        /// </summary>
        public int IdGjuha
        {
            get { return Convert.ToInt32(HfState.Get("idGjuha").ToString()); }
            set { HfState.Set("idGjuha", value); }
        }

        /// <summary>
        /// Emri i hidenfieldit per rilodim. 
        /// tipi i hidenfieldit duhet te jete i thjesht jo i devit
        /// </summary>
        public string HfRilodo
        {
            get { return HfState.Get("HfRilodo").ToString(); }
            set { HfState.Set("HfRilodo", value); }
        }

        public bool EnableStil
        {
            get { return Convert.ToBoolean(HfState.Get("ShfaqStil")); }
            set { HfState.Set("ShfaqStil", value); }
        }

        public string GuidString
        {
            get { return (string)HfState.Get("guidString"); }
            set { HfState.Set("guidString", value); }
        }
        public int IdRaportDesign
        {
            get { return (int)HfState.Get("idRaportDesign"); }
            set { HfState.Set("idRaportDesign", value); }
        }
        /// <summary>
        /// ExportFormat
        /// </summary>
        public int reportExportFormat
        {
            get { return Convert.ToInt32(HfState.Get("reportExportFormat")); }
            set { HfState.Set("reportExportFormat", value); }
        }

        /// <summary>
        /// ExportMode
        /// </summary>
        public int ReportExportMode
        {
            get { return Convert.ToInt32(HfState.Get("reportExportMode")); }
            set { HfState.Set("reportExportMode", value); }
        }

        public bool BtnReportEditVisible
        {
            get { return (bool)HfState.Get("BtnReportEditVisible"); }
            set
            {
                HfState.Set("BtnReportEditVisible", value);
                ShfaqMosShfaqBtnEdit(value);
            }
        }
        public bool BtnExportVeprimtariaDitore
        {
            get
            {
                return (bool)HfState.Get("BtnExportVeprimtariaDitore");
            }
            set
            {
                HfState.Set("BtnExportVeprimtariaDitore", value);
                ShfaqMosShfaqBtnExportVeprimtaria(value);
            }
        }

        public int IdNdermarrje
        {
            set { HfState.Set("idNdermarrje", value); }
            get { return (int)HfState.Get("idNdermarrje"); }

        }
        public int IdPerdoruesi
        {
            set { HfState.Set("idPerdoruesi", value); }
            get { return (int)HfState.Get("idPerdoruesi"); }

        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            Page.ClientScript.RegisterClientScriptBlock(GetType(), "PageScript",
            string.Format("var userControlPrefixes = \"{0}\";",
                ASPxPanel.ClientID + ClientIDSeparator),
            true);

            if (this.EnableStil) MbushComboStile();
            else
                ASPxComboBox_Style.Enabled = false;
            if (!IsPostBack || (Page.IsCallback && Request["__CALLBACKID"] == "ASPxCallbackPanel1"))
            {
                System.Globalization.CultureInfo ci = MessagesResource.KtheCultureInfo(IdGjuha);
                internacionalizo(ci);
            }
        }

        private void ShfaqMosShfaqBtnEdit(bool designIModifikueshem)
        {
            var shfaqButonModifikimi = (colRolPerdorues.merrRoleSipasPerdoruesiDheKodRoli(mySessionObjects.ktheIdPerdoruesi(Session), "RSU") == -1) && designIModifikueshem;
            btnDesigner.ClientVisible = shfaqButonModifikimi;
            btnDesigner.Enabled = shfaqButonModifikimi;
            btnDesigner2.ClientVisible = shfaqButonModifikimi;
            btnDesigner2.Enabled = shfaqButonModifikimi;

            //TEMPORALE
            cmbDesign_A.ClientVisible = shfaqButonModifikimi;
            cmbDesign_B.ClientVisible = shfaqButonModifikimi;
        }
        private void ShfaqMosShfaqBtnExportVeprimtaria(bool shfaq)
        {
            btnExportVeprimtariaDitore.ClientVisible = shfaq;
            btnExportVeprimtariaDitore.Enabled = shfaq;
            btnExportVeprimtariaDitore2.ClientVisible = shfaq;
            btnExportVeprimtariaDitore2.Enabled = shfaq;


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="shtoOrePrintimi">nuk duhet shtuar ora vetem ne rastin kur eksportohet secili raport i qendrave te kostos i filtruar me qk bije per raportin dinamik pash qk </param>
        public void KonfigFleteRaporti(DevExpress.XtraReports.UI.XtraReport report, clsPerdorues perdoruesi, bool shtoOrePrintimi = true)
        {
            report.Extensions["ndermarrjeLogo"] = Newtonsoft.Json.JsonConvert.SerializeObject((new clsNdermarrje(IdNdermarrje)).NdermarrjeLogo);
            float scaleFactor;
            if (Orientimi == clsRaportDesign._rap_portrait)
            {
                if (report.PageWidth > 850)
                    this.initialPageWidth = report.PageWidth;
            }

            else if (report.PageWidth > 1100)
                this.initialPageWidth = report.PageWidth;

            scaleFactor = this.setScale(Convert.ToInt32(perdoruesi.ZoomFactor), this.initialPageWidth) / 100f;
            reportExportFormat = this.setExportFormat(perdoruesi.ExportFormat);
            ReportExportMode = this.setExportMode(perdoruesi.ExportMode);
            report.PageWidth = Convert.ToInt32(this.initialPageWidth * scaleFactor);
            report.PageHeight = Convert.ToInt32(this.InitialPageHeight * scaleFactor);
            if (shtoOrePrintimi)
            {
                if (perdoruesi.ShfaqDtPrintimi)
                    VendosDtPrintimiRaport(report);
            }

            var designSettings = clsRaportDesign.GetReportDesignSettings(IdRaportDesign);
            if (designSettings.autoWidth)
                report.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            if (designSettings.rollPaper)
                report.RollPaper = true;

            report.CreateDocument();

            if (designSettings.autoWidth)
                SetAutoWidthToReport(report);

            report.PrintingSystem.Document.ScaleFactor = scaleFactor;
        }

        public void SetAutoWidthToReport(XtraReport report)
        {
            float scaleFactor = report.PrintingSystem.Document.ScaleFactor;
            if (scaleFactor < 1)
            {
                DevExpress.XtraPrinting.XtraPageSettingsBase pageSettings = report.PrintingSystem.PageSettings;
                System.Drawing.Size customPaperSize = System.Drawing.Size.Round(new System.Drawing.SizeF(pageSettings.UsablePageSize.Width / scaleFactor + pageSettings.Margins.Left + pageSettings.Margins.Right, pageSettings.Bounds.Height));
                DevExpress.XtraPrinting.XtraPageSettingsBase.ApplyPageSettings(pageSettings, System.Drawing.Printing.PaperKind.Custom, customPaperSize, pageSettings.Margins, pageSettings.MinMargins, false);
                report.PrintingSystem.Document.ScaleFactor = 1;
                report.PrintingSystem.Document.AutoFitToPagesWidth = 0;
            }
        }

        /// <summary>
        /// U vendos raporteve daten dhe oren e printimit nese eshte e konfiguruar
        /// </summary>
        /// <param name="report"></param>
        public void VendosDtPrintimiRaport(DevExpress.XtraReports.UI.XtraReport report)
        {
            XRLabel dateOreLabel = new XRLabel();
            dateOreLabel.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            dateOreLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            dateOreLabel.Name = "dateOreLabel";
            dateOreLabel.SizeF = new System.Drawing.SizeF(100F, 23F);
            dateOreLabel.Text = DateTime.Now.ToString();
            dateOreLabel.WidthF = 200F;
            DevExpress.XtraReports.UI.Band band = report.Bands.GetBandByType(typeof(DevExpress.XtraReports.UI.PageFooterBand));
            if (band != null)
            {

                if (report.Bands["PageFooter"].Controls["dateOreLabel"] != null)
                    report.Bands["PageFooter"].Controls.Remove(report.Bands["PageFooter"].Controls["dateOreLabel"]);
                report.Bands["PageFooter"].HeightF += 30F;
                dateOreLabel.LocationFloat = new DevExpress.Utils.PointFloat(0, report.Bands["PageFooter"].HeightF - 25F);
                report.Bands["PageFooter"].Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] { dateOreLabel });
            }
            else
            {
                PageFooterBand band1 = new PageFooterBand();
                band1.Name = "PageFooter";
                band1.HeightF = 30F;
                report.Bands.Add(band1);
                dateOreLabel.LocationFloat = new DevExpress.Utils.PointFloat(0, band1.HeightF - 25F);
                band1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] { dateOreLabel });
            }
        }

        public float setScale(int zoomFactor, int reportWidth)
        {
            int windowWidth = this.windowWidth;
            if (windowWidth == -1)
            {
                windowWidth = DbCore.clsFunksione.merrWindowWidthRequested(Request);
                if (windowWidth == -1)
                    windowWidth = reportWidth;
                this.windowWidth = windowWidth;
            }
            if (this.zoomFactor == -1)
                this.zoomFactor = zoomFactor;

            this.SetSelectionZoomFactor(this.zoomFactor);
            if (this.zoomFactor == 0)
            {
                windowWidth = windowWidth == 0 ? reportWidth : windowWidth;
                this.Scale = (float)windowWidth / reportWidth * 100;
                return this.Scale;
            }
            this.Scale = this.zoomFactor;
            return this.Scale;
        }

        public int setExportMode(int exportMode)
        {
            return this.ReportExportMode = exportMode;
        }

        public int setExportFormat(int exportFormat)
        {
            return this.reportExportFormat = exportFormat;
        }

        private void internacionalizo(System.Globalization.CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            ASPxButton_Search_B.ToolTip = ASPxButton_Search.ToolTip = rm.GetString("ReportToolbarButtonSearch", ci);
            ASPxButton_Print_B.ToolTip = ASPxButton_Print.ToolTip = rm.GetString("ReportToolbarButtonPrint", ci);
            ASPxButton_PrintPage_B.ToolTip = ASPxButton_PrintPage.ToolTip = rm.GetString("ReportToolbarButtonPrintPage", ci);
            ASPxButton_FirstPage_B.ToolTip = ASPxButton_FirstPage.ToolTip = rm.GetString("ReportToolbarButtonFirstPage", ci);
            ASPxButton_PrevPage_B.ToolTip = ASPxButton_PrevPage.ToolTip = rm.GetString("ReportToolbarButtonPreviousPage", ci);
            ASPxButton_NextPage_B.ToolTip = ASPxButton_NextPage.ToolTip = rm.GetString("ReportToolbarButtonNextPage", ci);
            labelPage_B.Text = labelPage.Text = rm.GetString("ReportToolbarLabelPage", ci);
            labelOf_B.Text = labelOf.Text = rm.GetString("ReportToolbarLabelOf", ci);
            ASPxComboBox_PageIndex_B.ToolTip = ASPxComboBox_PageIndex.ToolTip = rm.GetString("ReportToolbarComboBoxPageIndex", ci);
            ASPxTextBox_PageCount_B.ToolTip = ASPxTextBox_PageCount.ToolTip = rm.GetString("ReportToolbarButtonPageCount", ci);
            ASPxButton_LastPage_B.ToolTip = ASPxButton_LastPage.ToolTip = rm.GetString("ReportToolbarButtonLastPage", ci);
            ASPxButton_Save_B.ToolTip = ASPxButton_Save.ToolTip = rm.GetString("ReportToolbarButtonSaveToDisk", ci);
            btn_SaveRapPerTatime_B.ToolTip = btn_SaveRapPerTatime.ToolTip = rm.GetString("ReportToolbarButtonSaveToDiskPerTatime", ci);
            ASPxButton_SaveWindow_B.ToolTip = ASPxButton_SaveWindow.ToolTip = rm.GetString("ReportToolbarButtonSaveToWindow", ci);

            ASPxComboBox_ExportFormat_B.ToolTip = ASPxComboBox_ExportFormat.ToolTip = rm.GetString("ReportToolbarExportFormatTooltip", ci);
            //ASPxCheckBox_Raw.ToolTip = rm.GetString("ReportToolbarExportExcelRaw", ci);

            ASPxButton_SaveRaw_B.ToolTip = ASPxButton_SaveRaw.ToolTip = rm.GetString("ReportToolbarExportExcelRaw", ci);
            //labelZoomFactor.Text = rm.GetString("ReportToolbarLabelScaleFactor", ci);
            ASPxComboBox_ZoomFactor_B.ToolTip = ASPxComboBox_ZoomFactor.ToolTip = rm.GetString("ReportToolbarZoomFactorTooltip", ci);
            //labelStili.Text = rm.GetString("ReportToolbarLabelStili", ci);
            ASPxComboBox_Style_B.ToolTip = ASPxComboBox_Style.ToolTip = rm.GetString("ReportToolbarComboBoxStiliTooltip", ci);
            //labelOrientimi.Text = rm.GetString("ReportToolbarLabelOrientimi", ci);
            ASPxButton_SaveStyle_B.Text = ASPxButton_SaveStyle.Text = rm.GetString("ReportToolbarButtonRuajStil", ci);
            ASPxComboBox_Orientimi.ToolTip = ASPxComboBox_Orientimi.ToolTip = rm.GetString("ReportToolbarLabelOrientimi", ci);
            ListEditItem landscapeItem = ASPxComboBox_Orientimi.Items.FindByValue(clsRaportDesign._rap_landscape);
            ListEditItem landscapeItem_B = ASPxComboBox_Orientimi_B.Items.FindByValue(clsRaportDesign._rap_landscape);
            if (landscapeItem != null)
                landscapeItem.Text = rm.GetString("ReportToolbarComboBoxOrientimiLandscape", ci);
            if (landscapeItem_B != null)
                landscapeItem_B.Text = rm.GetString("ReportToolbarComboBoxOrientimiLandscape", ci);
            ListEditItem portraitItem_B = ASPxComboBox_Orientimi_B.Items.FindByValue(clsRaportDesign._rap_portrait);
            ListEditItem portraitItem = ASPxComboBox_Orientimi.Items.FindByValue(clsRaportDesign._rap_portrait);
            if (portraitItem != null)
                portraitItem.Text = rm.GetString("ReportToolbarComboBoxOrientimiPortrait", ci);
            if (portraitItem_B != null)
                portraitItem_B.Text = rm.GetString("ReportToolbarComboBoxOrientimiPortrait", ci);
            HfState.Set("ReportToolbarExportModeSingleFile", rm.GetString("ReportToolbarExportModeSingleFile", ci));
            //HfState.Set("ReportToolbarExportModeDifferentFiles", rm.GetString("ReportToolbarExportModeDifferentFiles", ci));
            HfState.Set("ReportToolbarExportModeSingleFilePageByPage", rm.GetString("ReportToolbarExportModeSingleFilePageByPage", ci));
        }

        public void SetExportOptions(DevExpress.XtraReports.UI.XtraReport r, string EmerRaporti)
        {
            ListEditItem aSPxComboBox_ExportModeSelectedItem = ASPxComboBox_ExportMode.SelectedItem;
            if (EmerRaporti == "deklarimNeFinance")
            {
                r.ExportOptions.Xls.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text;
                r.ExportOptions.Xlsx.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text;
            }
            switch ((string)this.GetExportFormatSelectedItem().Value)
            {
                case "rtf":
                    r.ExportOptions.Rtf.ExportMode = (DevExpress.XtraPrinting.RtfExportMode)Convert.ToInt32(aSPxComboBox_ExportModeSelectedItem.Value);
                    break;
                case "xlsx":
                    r.ExportOptions.Xlsx.RawDataMode = ASPxCheckBox_Raw.Checked;
                    r.ExportOptions.Xlsx.ExportMode = (DevExpress.XtraPrinting.XlsxExportMode)Convert.ToInt32(aSPxComboBox_ExportModeSelectedItem.Value);
                    if (EmerRaporti == "listeCmimeShitje" || EmerRaporti == "listeCmimeShitjeKosto" || EmerRaporti == "listeCmimeshShitjeAutorizimArtikujsh" || EmerRaporti == "analizeCmimeShitje" || EmerRaporti == "gjendjaCmimeShitjeArtikujsh" || EmerRaporti == "gjendjaCmimeShitjeArtikujshFastech" || EmerRaporti == "listeArtikujZbritjeAnalitike" || EmerRaporti == "gjendjeArtikulliSipasMagazines")
                        break;
                    hiqReportHeaderNgaRaporti(r, EmerRaporti);
                    break;
                case "xls":
                    r.ExportOptions.Xls.RawDataMode = ASPxCheckBox_Raw.Checked;
                    r.ExportOptions.Xls.ExportMode = (DevExpress.XtraPrinting.XlsExportMode)Convert.ToInt32(aSPxComboBox_ExportModeSelectedItem.Value);
                    if (EmerRaporti == "listeCmimeShitje" || EmerRaporti == "listeCmimeShitjeKosto" || EmerRaporti == "listeCmimeshShitjeAutorizimArtikujsh" || EmerRaporti == "analizeCmimeShitje" || EmerRaporti == "gjendjaCmimeShitjeArtikujsh" || EmerRaporti == "gjendjaCmimeShitjeArtikujshFastech" || EmerRaporti == "listeArtikujZbritjeAnalitike" || EmerRaporti == "gjendjeArtikulliSipasMagazines")
                        break;
                    hiqReportHeaderNgaRaporti(r, EmerRaporti);
                    break;
                case "csv": //to do Per Vodafone ne raportin e shitjeve ditore
                    var csv_encoding = clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.CSV_ENCODING);
                    DevExpress.XtraPrinting.CsvExportOptions csvOptions = r.ExportOptions.Csv;
                    csvOptions.Encoding = string.IsNullOrEmpty(csv_encoding) ? System.Text.Encoding.ASCII : System.Text.Encoding.GetEncoding(csv_encoding);
                    csvOptions.Separator = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator.ToString();
                    hiqReportHeaderNgaRaporti(r, EmerRaporti);
                    hiqPageFooterNgaRaporti(r);
                    break;
                default:
                    break;
            }
        }

        public void ShowExportPerTatimeButton(string EmerRealRaporti)
        {
            if (EmerRealRaporti == "teArdhuraShpenzimeQendraKosto")
                btn_SaveRapPerTatime.ToolTip = "Eksporto sipas qendrave te kostos";
            btn_SaveRapPerTatime.ClientVisible = true;
        }



        /// <summary>
        /// heq bandat nga designet e raporteve te cilat duhet te exportohen pa header ose footer
        /// duhet hequr ose jo percaktohet ne databaze sipas fushave perkatese
        /// </summary>
        /// <param name="r"></param>
        /// <param name="idRaporti"></param>
        /// <returns></returns>
        private bool hiqReportHeaderNgaRaporti(DevExpress.XtraReports.UI.XtraReport r, string EmerRaporti)
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
                    case "statusPorosiVFONE":
                    case "veprimeAparateEkspozitore":
                    case "gjendjaArtikujveIMEI":
                    case "postPaidPayments":
                    case "billPaymentsPerDay":
                    case "dailyGuaranteePayment":
                    case "gjendjaMagazinesEkspozitor":
                    case "gjendjaArtikujveIMEIEkspozitor":
                    case "analizeStokuKrahasimShitje":
                    case "analizaStokutEkspozitorKrahasimShitje":
                    case "gjendjaArtikujveVodafoneExp":
                    case "KontrolliPorosive":
                        hiqReporHeaderNgaRaporti(r);
                        break;
                    case "regjistriAnalitikMagazine":
                        hiqPageHeaderTeDesignTeRaportit(r, "Rap_MagazinaregjistriAnalitik");
                        break;
                    case "kartelaArtikullitInventar":
                        hiqPageHeaderTeDesignTeRaportit(r, "Rap_KartelaArtikulliMagazina");
                        break;

                    case "ditari":
                        hiqPageHeaderTeDesignTeRaportit(r, "Rap_KontDitari_Kastrati");
                        break;
                    case "bilanciKomponenteveTeKostos":
                        hiqPageHeaderTeDesignTeRaportit(r, "Rap_BilanciKomponentesKostos");
                        break;
                    case "permbledheseRezultatiQendraKosto":
                        hiqPageHeaderTeDesignTeRaportit(r, "Rap_Permbledhese_Rezultati_i_QK");
                        break;
                    case "karteleKlientiMonedheBaze":
                        hiqPageHeaderTeDesignTeRaportit(r, "Rap_KartelaKlient_SunPetroleum");
                        break;
                    case "EksportimiFaturaShitje":
                    case "arketimeDitore":
                    case "ecuriHyrjesh":
                    case "riparimeAparatesh":
                    case "maturimFaturaPerTuPaguar":
                    case "maturimFaturaPerTuPaguarFinanca":
                    case "shitjeAparatesh":
                    case "shitjeAparateshFinanca":
                    case "ecuriShitjesh":
                    case "VleratDitoreteArkes":
                    case "EksportimiFaturaBlerje":
                    case "aparatetBleraNgaDealer":
                    case "raportiAgreguarShitjeve":
                    case "gjendjaAgreguarProdukteve":
                    case "HyrjetSipasDyqaneve":
                   
                        hiqPageFooterNgaRaporti(r);
                        hiqReporHeaderNgaRaporti(r);
                        break;
                    default:
                        break;

                }

                r.CreateDocument();
                return true;
            }
            catch (Exception err)
            {
                ImbLogger.Error(err.Message);
                return false;
            }

        }
        public void hiqPageHeaderTeDesignTeRaportit(DevExpress.XtraReports.UI.XtraReport r, string emri)
        {
            string emriRap = r.ControlType.ToString();
            if (emriRap.Substring(0, emriRap.IndexOf(",")).Contains(emri))
            {
                DevExpress.XtraReports.UI.Band pageHeaderBand = r.Bands.GetBandByType(typeof(DevExpress.XtraReports.UI.PageHeaderBand));
                pageHeaderBand.Visible = false;
                //r.Bands.Remove(pageHeaderBand);
                r.CreateDocument();
                pageHeaderBand.Visible = true;
            }
        }
        public void hiqReporHeaderNgaRaporti(DevExpress.XtraReports.UI.XtraReport r)
        {
            DevExpress.XtraReports.UI.Band band = r.Bands.GetBandByType(typeof(DevExpress.XtraReports.UI.ReportHeaderBand));
            band.Visible = false;
            r.CreateDocument();
        }
        public void hiqPageFooterNgaRaporti(DevExpress.XtraReports.UI.XtraReport r)
        {
            DevExpress.XtraReports.UI.Band band = r.Bands.GetBandByType(typeof(DevExpress.XtraReports.UI.PageFooterBand));
            band.Visible = false;
            r.CreateDocument();

        }
        public void vendosPageHeaderTeDesignTeRaportit(DevExpress.XtraReports.UI.XtraReport r, List<string> ListeDsgn)
        {
            foreach (string emri in ListeDsgn)
            {
                string emriRap = r.ControlType.ToString();
                if (emriRap.Substring(0, emriRap.IndexOf(",")).Contains(emri))
                {
                    DevExpress.XtraReports.UI.Band pageHeaderBand = r.Bands.GetBandByType(typeof(DevExpress.XtraReports.UI.PageHeaderBand));
                    pageHeaderBand.Visible = true;
                    //r.Bands.Remove(pageHeaderBand);
                    r.CreateDocument();
                    break;
                }
            }
        }

        public void MbushComboOrientimi(clsRaportDesign rapdes)
        {
            if (ASPxComboBox_Orientimi.Items.Count == 0)
            {
                if (rapdes.FileName != "")
                {
                    this.Orientimi = clsRaportDesign._rap_landscape;
                    ASPxComboBox_Orientimi.Items.Add(clsRaportDesign._rap_landscape, clsRaportDesign._rap_landscape).Selected = true;
                    ASPxComboBox_Orientimi_B.Items.Add(clsRaportDesign._rap_landscape, clsRaportDesign._rap_landscape).Selected = true;
                }
                if (rapdes.FileNamePortrait != "")
                {
                    if (ASPxComboBox_Orientimi.SelectedIndex == -1)
                    {
                        this.Orientimi = clsRaportDesign._rap_portrait;
                        ASPxComboBox_Orientimi.Items.Add(clsRaportDesign._rap_portrait, clsRaportDesign._rap_portrait).Selected = true;
                        ASPxComboBox_Orientimi_B.Items.Add(clsRaportDesign._rap_portrait, clsRaportDesign._rap_portrait).Selected = true;
                    }
                    else
                    {
                        ASPxComboBox_Orientimi.Items.Add(clsRaportDesign._rap_portrait, clsRaportDesign._rap_portrait);
                        ASPxComboBox_Orientimi_B.Items.Add(clsRaportDesign._rap_portrait, clsRaportDesign._rap_portrait);
                    }
                }
            }
        }
        public void MbushComboDizajnesh(DbCore.DbShare.colRaporteDesign dizajnet, int idRaportDesignSelektuar = 0)
        {
            ListEditItem selectedItem = null;
            foreach (var dizajn in dizajnet)
            {

                cmbDesign_A.Items.Add(new ListEditItem { Value = dizajn.IdRaportDesign, Text = dizajn.Pershkrim });
                cmbDesign_B.Items.Add(new ListEditItem { Value = dizajn.IdRaportDesign, Text = dizajn.Pershkrim });
                if (dizajn.Zgjedhur)
                    selectedItem = new ListEditItem { Value = dizajn.IdRaportDesign, Text = dizajn.Pershkrim };
            }
            if (idRaportDesignSelektuar == 0)
            {
                if (selectedItem == null)
                {
                    var firstItem = dizajnet.OrderBy(x => x.IdRaportDesign).First();
                    selectedItem = new ListEditItem { Value = firstItem.IdRaportDesign, Text = firstItem.Pershkrim };
                }
                IdRaportDesign = (int)selectedItem.Value;
                cmbDesign_A.Value = selectedItem.Value;
                cmbDesign_B.Value = selectedItem.Value;
            }
            else
            {
                cmbDesign_A.Value = idRaportDesignSelektuar;
                cmbDesign_B.Value = idRaportDesignSelektuar;
            }
        }
        public void SetOrientimi(string orientimi)
        {
            ListEditItem itemToSelect = ASPxComboBox_Orientimi.Items.FindByText(orientimi);
            if (itemToSelect == null)
                return;
            itemToSelect.Selected = true;
            this.Orientimi = itemToSelect.Text;
        }
        /// <summary>
        /// mbush kombon e stileve
        /// </summary>
        private void MbushComboStile()
        {
            if (ASPxComboBox_Style.Items.Count == 0)
            {
                var reportStyles = DbCore.DbAdmin.ReportStyle.GetReportStyles(IdGjuha);
                foreach (var reportStyle in reportStyles)
                {
                    var item = ASPxComboBox_Style.Items.Add(reportStyle.EmerStili, reportStyle.FileName);
                    if (this.ReportStyle == reportStyle.FileName)
                    {
                        ASPxComboBox_Style.SelectedItem = item;
                        ASPxComboBox_Style_B.SelectedItem = item;
                    }
                }
            }
        }
        public void SetSelectionZoomFactor()
        {
            SetSelectionZoomFactor(this.zoomFactor);
        }
        public void SetSelectionZoomFactor(float zoomFactor)
        {
            Int32 zoomRoundedFactor = Convert.ToInt32(zoomFactor); //nese eshte null i japim vleren zero
            ListEditItem item = ASPxComboBox_ZoomFactor.Items.FindByValue(zoomRoundedFactor);

            if (item == null)
            {
                ASPxComboBox_ZoomFactor.Items.FindByValue(0).Selected = true; //Page Width
                ASPxComboBox_ZoomFactor_B.Items.FindByValue(0).Selected = true; //Page Width
            }
            else
            {
                item.Selected = true;
                ASPxComboBox_ZoomFactor_B.Items.FindByValue(zoomRoundedFactor).Selected = true; ;
            }
        }

        public void SetSelectionExportFormat()
        {
            ASPxComboBox_ExportFormat_B.SelectedIndex = ASPxComboBox_ExportFormat.SelectedIndex = this.reportExportFormat;
        }

        public void SetSelectionExportMode()
        {
            ASPxComboBox_ExportMode.Items.FindByValue(this.ReportExportMode).Selected = true;
            ASPxComboBox_ExportMode_B.Items.FindByValue(this.ReportExportMode).Selected = true;
        }

        public void SetExportFormat(string RapEmriReal, int indexDefault)
        {
            switch (RapEmriReal)
            {
                case "listeArketimeAnullimePostpaid":
                    this.setExportFormat(ASPxComboBox_ExportFormat.Items.FindByValue("csv").Index);
                    break;
                default:
                    this.setExportFormat(indexDefault);
                    break;
            }
        }

        /// <summary>
        /// ExportFormatSelectedItem        
        /// </summary>
        public ListEditItem GetExportFormatSelectedItem()
        {
            return ASPxComboBox_ExportFormat.SelectedItem;
        }

        internal void ReloadStyle(ReportViewer ReportViewer, string pathStyle)
        {
            ReportViewer.Report.StyleSheetPath = Raporti.ndertoPathStyleSheet(pathStyle, this.ReportStyle);
            ReportViewer.Report.CreateDocument();
        }
    }
}