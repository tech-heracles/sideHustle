using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DevExpress.XtraReports.Web;
using DevExpress.XtraReports.UI;
using DbCore.DbAdmin;
using System.Globalization;
using System.Drawing;
using System.Data;
using DbCore.DbShare;

namespace PlatinumWeb.E_PaySlip
{
    public partial class ImbReportToolbar1 : System.Web.UI.UserControl
    {
        public string ArsyeReload
        {
            get => HfState.Contains("arsyeReload") ? HfState.Get("arsyeReload").ToString() : "";
            set => HfState.Set("arsyeReload", value);
        }
        
        /// <summary>
        /// Emri i file-it te stilit te raportit
        /// </summary>
        public string ReportStyle
        {
            get => HfState.Get("reportStyle").ToString();
            set => HfState.Set("reportStyle", value);
        }

        /// <summary>
        /// Faktori i zoomit. Page width apo vlere tjeter
        /// </summary>
        public int ZoomFactor
        {
            get => HfState.Contains("zoomFactor") ? Convert.ToInt32(HfState.Get("zoomFactor")) : -1;
            set => HfState.Set("zoomFactor", value);
        }

        public int PageCount
        {
            get
            {
                if (HfState.Contains("PageCount"))
                    return Convert.ToInt32(HfState.Get("PageCount"));
                else
                    return -1;
            }
            set => HfState.Set("PageCount", value);
        }
        /// <summary>
        /// Shkalla e zoom-imit. 
        /// </summary>
        public float scale
        {
            get
            {
                if (HfState.Contains("scale"))
                    return Convert.ToSingle(HfState.Get("scale"), new System.Globalization.CultureInfo("en-US"));
                else
                    return -1;
            }
            set => HfState.Set("scale", value);
        }
        /// <summary>
        /// Gjeresia e raportit perdoret per zoomimin PageWidth
        /// </summary>
        public int InitialPageWidth
        {
            get
            {

                if (HfState.Contains("initialPageWidth"))
                    return Convert.ToInt32(HfState.Get("initialPageWidth"));
                else
                    if (orientimi == clsRaportDesign._rap_portrait)
                    return 850;
                else
                    return 1100;

            }
            set => HfState.Set("initialPageWidth", value);
        }
        public int InitialPageHeight
        {
            get
            {
                if (HfState.Contains("initialPageHeight"))
                    return Convert.ToInt32(HfState.Get("initialPageHeight"));
                else
                    if (orientimi == clsRaportDesign._rap_portrait)
                    return 1100;
                else
                    return 850;
            }
            set => HfState.Set("initialPageHeight", value);
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
            set => HfState.Set("windowWidth", value);
        }
        /// <summary>
        /// Orientimi
        /// </summary>
        public string orientimi
        {
            get => HfState.Get("orientimi").ToString();
            set => HfState.Set("orientimi", value);
        }
        /// <summary>
        /// idGjuha
        /// </summary>
        public int idGjuha
        {
            get => Convert.ToInt32(HfState.Get("idGjuha").ToString());
            set => HfState.Set("idGjuha", value);
        }

        /// <summary>
        /// Emri i hidenfieldit per rilodim. 
        /// tipi i hidenfieldit duhet te jete i thjesht jo i devit
        /// </summary>
        public string HfRilodo
        {
            get => HfState.Get("HfRilodo").ToString();
            set => HfState.Set("HfRilodo", value);
        }

        public bool EnableStil
        {
            get => Convert.ToBoolean(HfState.Get("ShfaqStil"));
            set => HfState.Set("ShfaqStil", value);
        }


        /// <summary>
        /// ExportFormat
        /// </summary>
        public int reportExportFormat
        {
            get => Convert.ToInt32(HfState.Get("reportExportFormat"));
            set => HfState.Set("reportExportFormat", value);
        }

        /// <summary>
        /// ExportMode
        /// </summary>
        public int reportExportMode
        {
            get => Convert.ToInt32(HfState.Get("reportExportMode"));
            set => HfState.Set("reportExportMode", value);
        }



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
                System.Globalization.CultureInfo ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
                internacionalizo(ci);
            }
        }
        public void vendosFaqe()
        {
            ASPxTextBox_PageCount.Text = "0";

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="report"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="shtoOrePrintimi">nuk duhet shtuar ora vetem ne rastin kur eksportohet secili raport i qendrave te kostos i filtruar me qk bije per raportin dinamik pash qk </param>
        public void konfigFleteRaporti(DevExpress.XtraReports.UI.XtraReport report, int idPerdorues, string emerRaportiReal, bool shtoOrePrintimi = true)
        {
            float scaleFactor;
            //if (this.InitialPageWidth == -1)
            //    this.InitialPageWidth = report.PageWidth;
            //if (this.InitialPageHeight == -1)
            //    this.InitialPageHeight = report.PageHeight;
            if (orientimi == clsRaportDesign._rap_portrait)
            {
                if (report.PageWidth > 850)
                    this.InitialPageWidth = report.PageWidth;
            }

            else if (report.PageWidth > 1100)
                this.InitialPageWidth = report.PageWidth;

            scaleFactor = this.setScale(idPerdorues, this.InitialPageWidth) / 100f;
            reportExportFormat = this.setExportFormat(idPerdorues);
            reportExportMode = this.setExportMode(idPerdorues);
            report.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            report.PageWidth = Convert.ToInt32(this.InitialPageWidth * scaleFactor);
            report.PageHeight = Convert.ToInt32(this.InitialPageHeight * scaleFactor);
            if (shtoOrePrintimi)
            {
                clsPerdorues perdorues = new clsPerdorues(idPerdorues);
                if (perdorues.ShfaqDtPrintimi)
                    VendosDtPrintimiRaport(report);
            }
            DataSet ds = (DataSet)report.DataSource;
            if (ds.Tables[0].Rows.Count == 0)
                shtomesazh(report, emerRaportiReal);
            //ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            //CultureInfo ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
            //ReportHeaderBand headerBand = new ReportHeaderBand()
            //{
            //    HeightF = 80
            //};
            //headerBand.Controls.Add(new XRLabel()
            //{
            //    Text = rm.GetString("lblTeDhenatNukJanePostuar", ci),
            //    // "Te dhenat per periudhen nuk jane postuar ende",
            //    SizeF =new  SizeF(7000, 80),

            //    Font = new Font("Arial", 15)
            //});



            //if (headerBand != null)
            //{
            //    report.Bands.Add(headerBand);
            //    report.CreateDocument();

            //}


            report.CreateDocument();
            report.PrintingSystem.Document.ScaleFactor = scaleFactor;

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
        public void shtomesazh(DevExpress.XtraReports.UI.XtraReport report, string emerRealRaporti)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
            DevExpress.XtraReports.UI.Band band = report.Bands.GetBandByType(typeof(DevExpress.XtraReports.UI.ReportHeaderBand));
            string textSkaTeDhena = emerRealRaporti == "kartelaEPages" ? rm.GetString("lblTeDhenatNukJanePostuar", ci) : rm.GetString("lblNukKaTeDhenaPerKetePeriudhe", ci);
           
            if (band == null)
            {
                ReportHeaderBand headerBand = new ReportHeaderBand();
                headerBand.HeightF = 30F;

               
                headerBand.Controls.Add(new XRLabel()
                {
                    Text = textSkaTeDhena,
                    // "Te dhenat per periudhen nuk jane postuar ende",
                    SizeF = new SizeF(600, 23),
                    Name = "labelMesazhTeDhenatJoPostuar"


                });
                if (headerBand != null)
                {
                    report.Bands.Add(headerBand);
                }

            }
            else {
                report.Bands.GetBandByType(typeof(DevExpress.XtraReports.UI.ReportHeaderBand)).HeightF += 30F;
                XRLabel label = new XRLabel()
                {
                    Text = textSkaTeDhena,
                    // "Te dhenat per periudhen nuk jane postuar ende",
                    SizeF = new SizeF(600, 23),
                    Name = "labelMesazhTeDhenatJoPostuar"


                };
                label.SizeF = new System.Drawing.SizeF(100F, 23F);
                //foreach (Control c in report.Bands["ReportHeader"].Controls)
                //{
                //    if (c.GetType() == typeof(XRLabel))
                //        if ((XRLabel)c == label) return;

                //}
                XRLabel label1 = (XRLabel)report.Bands.GetBandByType(typeof(DevExpress.XtraReports.UI.ReportHeaderBand)).FindControl("labelMesazhTeDhenatJoPostuar", true);
                if (label1 == null)
                    report.Bands.GetBandByType(typeof(DevExpress.XtraReports.UI.ReportHeaderBand)).Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] { label });
            }




        }

        public float setScale(int idPerdorues)
        {
            return setScale(idPerdorues, 1100);
        }
        public float setScale(int idPerdorues, int reportWidth)
        {
            int windowWidth = this.windowWidth;
            if (windowWidth == -1)
            {
                windowWidth = DbCore.clsFunksione.merrWindowWidthRequested(Request);
                if (windowWidth == -1)
                    windowWidth = reportWidth;
                this.windowWidth = windowWidth;
            }
            if (this.ZoomFactor == -1)
            {
                DbCore.DbAdmin.clsPerdorues perdorues = new DbCore.DbAdmin.clsPerdorues(idPerdorues);
                this.ZoomFactor = Convert.ToInt32(perdorues.ZoomFactor);
            }
            this.SetSelectionZoomFactor(this.ZoomFactor);
            if (this.ZoomFactor == 0)
            {
                windowWidth = windowWidth == 0 ? reportWidth : windowWidth;
                this.scale = (float)windowWidth / reportWidth * 100;
                return this.scale;
            }
            this.scale = this.ZoomFactor;
            return this.scale;
        }

        public int setExportMode(int idPerdorues)
        {
            DbCore.DbAdmin.clsPerdorues perdorues = new DbCore.DbAdmin.clsPerdorues(idPerdorues);
            return this.reportExportMode = perdorues.ExportMode;
        }

        public int setExportFormat(int idPerdorues)
        {
            DbCore.DbAdmin.clsPerdorues perdorues = new DbCore.DbAdmin.clsPerdorues(idPerdorues);
            return this.reportExportFormat = perdorues.ExportFormat;
        }

        private void internacionalizo(System.Globalization.CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            ASPxButton_Search.ToolTip = rm.GetString("ReportToolbarButtonSearch", ci);
            ASPxButton_Print.ToolTip = rm.GetString("ReportToolbarButtonPrint", ci);
            ASPxButton_PrintPage.ToolTip = rm.GetString("ReportToolbarButtonPrintPage", ci);
            ASPxButton_FirstPage.ToolTip = rm.GetString("ReportToolbarButtonFirstPage", ci);
            ASPxButton_PrevPage.ToolTip = rm.GetString("ReportToolbarButtonPreviousPage", ci);
            ASPxButton_NextPage.ToolTip = rm.GetString("ReportToolbarButtonNextPage", ci);
            labelPage.Text = rm.GetString("ReportToolbarLabelPage", ci);
            labelOf.Text = rm.GetString("ReportToolbarLabelOf", ci);
            ASPxComboBox_PageIndex.ToolTip = rm.GetString("ReportToolbarComboBoxPageIndex", ci);
            ASPxTextBox_PageCount.ToolTip = rm.GetString("ReportToolbarButtonPageCount", ci);
            ASPxButton_LastPage.ToolTip = rm.GetString("ReportToolbarButtonLastPage", ci);
            ASPxButton_Save.ToolTip = rm.GetString("ReportToolbarButtonSaveToDisk", ci);
            btn_SaveRapPerTatime.ToolTip = rm.GetString("ReportToolbarButtonSaveToDiskPerTatime", ci);
            btnSaveToDisk100.ToolTip = rm.GetString("ReportToolbarButtonSaveToDisk100", ci);
            ASPxButton_SaveWindow.ToolTip = rm.GetString("ReportToolbarButtonSaveToWindow", ci);
            ASPxComboBox_ExportFormat.ToolTip = rm.GetString("ReportToolbarExportFormatTooltip", ci);
            //ASPxCheckBox_Raw.ToolTip = rm.GetString("ReportToolbarExportExcelRaw", ci);
            ASPxButton_SaveRaw.ToolTip = rm.GetString("ReportToolbarExportExcelRaw", ci);
            //labelZoomFactor.Text = rm.GetString("ReportToolbarLabelScaleFactor", ci);
            ASPxComboBox_ZoomFactor.ToolTip = rm.GetString("ReportToolbarZoomFactorTooltip", ci);
            //labelStili.Text = rm.GetString("ReportToolbarLabelStili", ci);
            ASPxComboBox_Style.ToolTip = rm.GetString("ReportToolbarComboBoxStiliTooltip", ci);
            //labelOrientimi.Text = rm.GetString("ReportToolbarLabelOrientimi", ci);
            ASPxButton_SaveStyle.Text = rm.GetString("ReportToolbarButtonRuajStil", ci);
            ASPxComboBox_Orientimi.ToolTip = rm.GetString("ReportToolbarLabelOrientimi", ci);
            ListEditItem landscapeItem = ASPxComboBox_Orientimi.Items.FindByValue(clsRaportDesign._rap_landscape);
            if (landscapeItem != null)
                landscapeItem.Text = rm.GetString("ReportToolbarComboBoxOrientimiLandscape", ci);
            ListEditItem portraitItem = ASPxComboBox_Orientimi.Items.FindByValue(clsRaportDesign._rap_portrait);
            if (portraitItem != null)
                portraitItem.Text = rm.GetString("ReportToolbarComboBoxOrientimiPortrait", ci);
            HfState.Set("ReportToolbarExportModeSingleFile", rm.GetString("ReportToolbarExportModeSingleFile", ci));
            //HfState.Set("ReportToolbarExportModeDifferentFiles", rm.GetString("ReportToolbarExportModeDifferentFiles", ci));
            HfState.Set("ReportToolbarExportModeSingleFilePageByPage", rm.GetString("ReportToolbarExportModeSingleFilePageByPage", ci));
        }

        public void setExportOptions(DevExpress.XtraReports.UI.XtraReport r, int idRaporti)
        {
            ListEditItem aSPxComboBox_ExportModeSelectedItem = ASPxComboBox_ExportMode.SelectedItem;
            switch ((string)this.getExportFormatSelectedItem().Value)
            {
                case "rtf":
                    r.ExportOptions.Rtf.ExportMode = (DevExpress.XtraPrinting.RtfExportMode)Convert.ToInt32(aSPxComboBox_ExportModeSelectedItem.Value);
                    break;
                case "xlsx":
                    r.ExportOptions.Xlsx.RawDataMode = ASPxCheckBox_Raw.Checked;
                    r.ExportOptions.Xlsx.ExportMode = (DevExpress.XtraPrinting.XlsxExportMode)Convert.ToInt32(aSPxComboBox_ExportModeSelectedItem.Value);
                    hiqReportHeaderNgaRaporti(r, idRaporti);
                    break;
                case "xls":
                    r.ExportOptions.Xls.RawDataMode = ASPxCheckBox_Raw.Checked;
                    r.ExportOptions.Xls.ExportMode = (DevExpress.XtraPrinting.XlsExportMode)Convert.ToInt32(aSPxComboBox_ExportModeSelectedItem.Value);
                    hiqReportHeaderNgaRaporti(r, idRaporti);
                    break;
                //case "csv": //to do Per Vodafone ne raportin e shitjeve ditore
                //    if (idRaporti == 111)
                //    {
                //        r.FindControl("TopMargin", true).Visible = false; //i ben hide bandes ku eshte titulli bashke me filtrat
                //        for (int i = 1; i < 39; i++)
                //        {
                //            r.FindControl("xrLabel" + i, true).Visible = false;
                //        }
                //        r.FindControl("xrPictureBox1", true).Visible = false;
                //    }
                //    break;
                default:
                    break;
            }
        }

        public void showExportPerTatimeButton(int idRaporti)
        {
            if (idRaporti == 180)
                btn_SaveRapPerTatime.ToolTip = "Eksporto sipas qendrave te kostos";
            btn_SaveRapPerTatime.ClientVisible = true;
        }
        public void hiqButonExport()
        {

            btn_SaveRapPerTatime.ClientVisible = false;
            ASPxButton_Save.ClientVisible = false;
            btnSaveToDisk100.ClientVisible = false;
            ASPxButton_SaveWindow.ClientVisible = false;
            ASPxButton_SaveRaw.ClientVisible = false;
            ASPxComboBox_ExportFormat.ClientVisible = false;
            ASPxComboBox_ExportMode.ClientVisible = false;
            ASPxComboBox_Style.ClientVisible = false;
            ASPxButton_SaveStyle.ClientVisible = false;
            ASPxComboBox_Orientimi.ClientVisible = false;
        }



        /// <summary>
        /// heq banden HeaderReport nga raporti
        /// </summary>
        /// <param name="r"></param>
        /// <param name="idRaporti"></param>
        /// <returns></returns>
        private bool hiqReportHeaderNgaRaporti(DevExpress.XtraReports.UI.XtraReport r, int idRaporti)
        {
            try
            {
                switch (idRaporti)
                {
                    case 150:
                    case 151:
                    case 153:
                    case 154:
                    case 155:
                    case 156:
                    case 158:
                    case 159:
                    case 101:
                    case 273:
                        {
                            DevExpress.XtraReports.UI.Band band = r.Bands.GetBandByType(typeof(DevExpress.XtraReports.UI.ReportHeaderBand));
                            r.Bands.Remove(band);
                            r.CreateDocument();
                        }
                        break;
                    case 248:
                        {
                            DevExpress.XtraReports.UI.Band pageFooterBand = r.Bands.GetBandByType(typeof(DevExpress.XtraReports.UI.PageFooterBand));
                            DevExpress.XtraReports.UI.Band pageHeaderBand = r.Bands.GetBandByType(typeof(DevExpress.XtraReports.UI.PageHeaderBand));
                            r.Bands.Remove(pageFooterBand);
                            r.Bands.Remove(pageHeaderBand);
                            r.CreateDocument();
                        }
                        break;
                    case 22:
                        hiqPageHeaderTeDesignTeRaportit(r, "Rap_ShitjePermbledhes");
                        break;
                    case 29:
                        hiqPageHeaderTeDesignTeRaportit(r, "Rap_ShitjeAnalitik");
                        break;
                    case 43:
                        hiqPageHeaderTeDesignTeRaportit(r, "Rap_MarzhiShitjeve");
                        break;
                    case 33:
                        hiqPageHeaderTeDesignTeRaportit(r, "Rap_Artikujteshitur");
                        break;
                    case 1:
                        hiqPageHeaderTeDesignTeRaportit(r, "Rap_kontKartelaLlogarive");
                        break;
                    case 30:
                        hiqPageHeaderTeDesignTeRaportit(r, "Rap_MagazinaRegjistriPermbledhes");
                        break;
                    case 31:
                        hiqPageHeaderTeDesignTeRaportit(r, "Rap_MagazinaregjistriAnalitik");
                        break;
                    case 55:
                        hiqPageHeaderTeDesignTeRaportit(r, "Rap_KartelaArtikulliMagazina");
                        break;
                    case 27:
                        hiqPageHeaderTeDesignTeRaportit(r, "Rap_BlerjeRegjistriPermbledhes");
                        break;
                    case 28:
                        hiqPageHeaderTeDesignTeRaportit(r, "Rap_BlerjeRegjistriAnalitik");
                        break;
                    case 193:
                        hiqPageHeaderTeDesignTeRaportit(r, "Rap_ArtikujTeBlere");
                        break;
                    case 0:
                        hiqPageHeaderTeDesignTeRaportit(r, "Rap_KontDitari_Kastrati");
                        break;
                    case 20:
                        hiqPageHeaderTeDesignTeRaportit(r, "RAP_BANKA_DITARIKLASIK_Kastrati");
                        break;
                    case 133:
                        hiqPageHeaderTeDesignTeRaportit(r, "RAP_ARKA_DITARIKLASIK_Kastrati");
                        break;
                    case 262:
                        hiqPageHeaderTeDesignTeRaportit(r, "Rap_BilanciKomponentesKostos");
                        break;
                    case 257:
                        hiqPageHeaderTeDesignTeRaportit(r, "Rap_Permbledhese_Rezultati_i_QK");
                        break;
                    case 213:
                        hiqPageHeaderTeDesignTeRaportit(r, "Rap_KartelaKlient_SunPetroleum");
                        break;
                    case 261:
                        hiqPageHeaderTeDesignTeRaportit(r, "Rap_MarzhiShitjeve_SipasMagazinaveFormat2");
                        break;
                    case 266:
                        hiqPageHeaderTeDesignTeRaportit(r, "Rap_KartelaArtikulliMagazinaFormat2");
                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (Exception)
            { return false; }
        }

        public void hiqPageHeaderTeDesignTeRaportit(DevExpress.XtraReports.UI.XtraReport r, string emri)
        {
            string emriRap = r.ControlType.ToString();
            if (emriRap.Substring(0, emriRap.IndexOf(",")).Contains(emri))
            {
                DevExpress.XtraReports.UI.Band pageHeaderBand = r.Bands.GetBandByType(typeof(DevExpress.XtraReports.UI.PageHeaderBand));
                r.Bands.Remove(pageHeaderBand);
                r.CreateDocument();
            }
        }
        public void mbushComboOrientimi(clsRaportDesign rapdes)
        {
            if (ASPxComboBox_Orientimi.Items.Count == 0)
            {
                if (rapdes.FileName != "")
                {
                    this.orientimi = clsRaportDesign._rap_landscape;
                    ASPxComboBox_Orientimi.Items.Add(clsRaportDesign._rap_landscape, clsRaportDesign._rap_landscape).Selected = true;
                }
                if (rapdes.FileNamePortrait != "")
                {
                    if (ASPxComboBox_Orientimi.SelectedIndex == -1)
                    {
                        this.orientimi = clsRaportDesign._rap_portrait;
                        ASPxComboBox_Orientimi.Items.Add(clsRaportDesign._rap_portrait, clsRaportDesign._rap_portrait).Selected = true;
                    }
                    else
                    {
                        ASPxComboBox_Orientimi.Items.Add(clsRaportDesign._rap_portrait, clsRaportDesign._rap_portrait);
                    }
                }
            }
        }
        public void setOrientimi(string orientimi)
        {
            ListEditItem itemToSelect = ASPxComboBox_Orientimi.Items.FindByText(orientimi);
            if (itemToSelect == null)
                return;
            itemToSelect.Selected = true;
            this.orientimi = itemToSelect.Text;
        }

        /// <summary>
        /// mbush kombon e stileve
        /// </summary>
        private void MbushComboStile()
        {
            if (ASPxComboBox_Style.Items.Count == 0)
            {
                var reportStyles = DbCore.DbAdmin.ReportStyle.GetReportStyles(idGjuha);
                foreach (var reportStyle in reportStyles)
                {
                    var item = ASPxComboBox_Style.Items.Add(reportStyle.EmerStili, reportStyle.FileName);
                    if (ReportStyle == reportStyle.FileName)
                    {
                        ASPxComboBox_Style.SelectedItem = item;
                    }
                }
            }
        }
        public void SetSelectionZoomFactor()
        {
            SetSelectionZoomFactor(this.ZoomFactor);
        }
        public void SetSelectionZoomFactor(float zoomFactor)
        {
            Int32 zoomRoundedFactor = Convert.ToInt32(zoomFactor); //nese eshte null i japim vleren zero
            ListEditItem item = ASPxComboBox_ZoomFactor.Items.FindByValue(zoomRoundedFactor);
            if (item == null)
                ASPxComboBox_ZoomFactor.Items.FindByValue(0).Selected = true; //Page Width
            else
                item.Selected = true;
        }

        public void SetSelectionExportFormat()
        {
            ASPxComboBox_ExportFormat.SelectedIndex = this.reportExportFormat;
        }

        public void SetSelectionExportMode()
        {
            ASPxComboBox_ExportMode.Items.FindByValue(this.reportExportMode).Selected = true;
        }

        /// <summary>
        /// ExportFormatSelectedItem        
        /// </summary>
        public ListEditItem getExportFormatSelectedItem()
        {
            return ASPxComboBox_ExportFormat.SelectedItem;
        }

        internal void reloadStyle(ReportViewer ReportViewer, string pathStyle)
        {
            ReportViewer.Report.StyleSheet.LoadFromFile(Raporti.ndertoPathStyleSheet(pathStyle, this.ReportStyle));
            ReportViewer.Report.CreateDocument();
        }
    }
}