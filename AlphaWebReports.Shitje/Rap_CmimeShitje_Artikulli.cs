using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraReports.UI.PivotGrid;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_CmimeShitje_Artikulli : DevExpress.XtraReports.UI.XtraReport
    {
        private bool isLocked;
		public Rap_CmimeShitje_Artikulli()
        {
            InitializeComponent();
        } 

        public Rap_CmimeShitje_Artikulli(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_CmimeShitje_Artikulli(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, XtraReport report)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            Ndermarja.Value = report.Parameters[0].Value;
            Monedha.Value = report.Parameters[1].Value;
            Kartela.Value = report.Parameters[2].Value;
            Grupim1.Value = report.Parameters[4].Value;
            Grupim2.Value = report.Parameters["filterCmimeArtikulli"].Value;
            DtDok.Value = report.Parameters[6].Value;
            Cmimi.Value = report.Parameters[7].Value;
            PershkrimArt.Value = report.Parameters[8].Value;            
            XRPivotGridField fieldKartela = new XRPivotGridField("Kartela", PivotArea.RowArea);
            fieldKartela.FieldName = "KODARTIKULLI";
            fieldKartela.Caption = rm.GetString("labelKartela", ci);


         
            //fieldKartela.Width = 50;
            XRPivotGridField fieldKodbari = new XRPivotGridField("Kodbari", PivotArea.RowArea);
            fieldKodbari.FieldName = "KODBARI";
            fieldKodbari.Caption = rm.GetString("labelFilterAvancuarKodbari", ci);
            //fieldKodbari.Width = 50;

            XRPivotGridField fieldPershkrimArtikulli = new XRPivotGridField("Emertimi", PivotArea.RowArea);
            fieldPershkrimArtikulli.FieldName = "PERSHKRIMARTIKULLI";
            fieldPershkrimArtikulli.Caption = rm.GetString("labelRaportiEmertimi", ci);
            fieldPershkrimArtikulli.Appearance.FieldValue.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            fieldPershkrimArtikulli.ColumnValueLineCount = 3;
            //fieldPershkrimArtikulli.Width = 70;
            XRPivotGridField fieldNjesia = new XRPivotGridField("Njesia", PivotArea.RowArea);
            if (Grupim2.Value.ToString() == "Çmimi parë")
            {
                fieldNjesia.FieldName = "KODNJESIA";
            }
            else fieldNjesia.FieldName = "njesi2";
            fieldNjesia.Caption = rm.GetString("labelNjesia", ci);
            //fieldNjesia.Width = 30;
            XRPivotGridField fieldNivelCmimi = new XRPivotGridField("Cmimet", PivotArea.ColumnArea);
            fieldNivelCmimi.FieldName = "KODNIVELCMIMI";
            fieldNivelCmimi.Caption = rm.GetString("labelRaportiCmimet", ci);
            //fieldNivelCmimi.Width = 30;
            XRPivotGridField fieldVleraCmimi = new XRPivotGridField("Cmimet", PivotArea.DataArea);
            if (Grupim2.Value.ToString() == "Çmimi dytë")
            { 
                fieldVleraCmimi.FieldName = "CMIMI2";
            }
            else
            {
                fieldVleraCmimi.FieldName = "CMIMI";
            } 
            fieldVleraCmimi.Caption = rm.GetString("labelRaportiCmimet", ci);
            fieldVleraCmimi.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldVleraCmimi.CellFormat.FormatString = "F02";
            fieldVleraCmimi.Width = 30;
            cmimetPivotGrid.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {fieldKartela, fieldKodbari, fieldPershkrimArtikulli,
                fieldNjesia, fieldNivelCmimi, fieldVleraCmimi});            
            cmimetPivotGrid.Styles.FieldHeaderStyle = cmimetPivotGrid.Styles.FieldHeaderStyle;
            cmimetPivotGrid.Styles.FieldValueStyle = cmimetPivotGrid.Styles.FieldHeaderStyle;
            cmimetPivotGrid.FieldValueGrandTotalStyleName = cmimetPivotGrid.Styles.FieldHeaderStyle.Name;
            cmimetPivotGrid.WordWrap = true;
        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
            System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportListeCmimeshShitjeTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
        }

        private void Rap_CmimeShitje_Artikulli_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            isLocked = false;
        }

        private void Rap_CmimeShitje_Artikulli_AfterPrint(object sender, EventArgs e)
        {
            //this.PageWidth = cmimetPivotGrid.ActualWidth + 1000;
            //SetCustomPageSize();
            cmimetPivotGrid.WordWrap = true;
        }
        
        private void SetCustomPageSize()
        {
            if (!isLocked)
            {
                isLocked = true;
                PrintingSystem.Document.AutoFitToPagesWidth = 1;
                float scaleFactor = PrintingSystem.Document.ScaleFactor;
                DevExpress.XtraPrinting.XtraPageSettingsBase pageSettings = PrintingSystem.PageSettings;
                Size customPaperSize = Size.Round(new SizeF(pageSettings.UsablePageSize.Width / scaleFactor + pageSettings.Margins.Left + pageSettings.Margins.Right, pageSettings.Bounds.Height));
                DevExpress.XtraPrinting.XtraPageSettingsBase.ApplyPageSettings(pageSettings, System.Drawing.Printing.PaperKind.Custom, customPaperSize, pageSettings.Margins, pageSettings.MinMargins, true);
                PrintingSystem.Document.AutoFitToPagesWidth = 0;
                PrintingSystem.Document.ScaleFactor = 1;
            }
        }
    }
}
