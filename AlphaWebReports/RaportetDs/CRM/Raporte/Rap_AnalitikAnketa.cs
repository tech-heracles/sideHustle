using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Reflection;
using System.Resources;
using DevExpress.XtraPivotGrid;
using DevExpress.Data.PivotGrid;
using DevExpress.XtraReports.UI.PivotGrid;


namespace AlphaWebReports.RaportetDs.CRM.Raporte
{
    public partial class Rap_AnalitikAnketa : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_AnalitikAnketa(){InitializeComponent();} 
        ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

        public Rap_AnalitikAnketa(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, report)
        {

        }
        public Rap_AnalitikAnketa(CultureInfo ci, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            //xrLabel3.Text = raport.Parameters[0].Description;
            if (raport.Parameters.Count > 0)
            {
                parameter1.Value = raport.Parameters[2].Value;
                parameter2.Value = raport.Parameters[1].Value;
            }
            //xrLabel5.Text = raport.Parameters[1].Description;
            //parameter2.Value = raport.Parameters[1].Value;
            //xrLabel7.Text = raport.Parameters[2].Description;
            //parameter3.Value = raport.Parameters[2].Value;
            //xrLabel9.Text = raport.Parameters[3].Description;
            //parameter4.Value = raport.Parameters[3].Value;
            //xrLabel11.Text = raport.Parameters[4].Description;
            //parameter5.Value = raport.Parameters[4].Value;
            //xrLabel13.Text = raport.Parameters[5].Description;
            //parameter6.Value = raport.Parameters[5].Value;

           

            XRPivotGridField fieldAgjent = new XRPivotGridField("Agjent", PivotArea.RowArea);
            fieldAgjent.FieldName = "EmerMbiemer";
            fieldAgjent.Caption = rm.GetString("labelAgjenti", ci);
            fieldAgjent.Options.ShowGrandTotal = true;
            //fieldAgjent.Appearance.Cell.WordWrap = true;
            fieldAgjent.Appearance.Cell.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Top;

            XRPivotGridField fieldDtTakimi = new XRPivotGridField("Date e Takimit", PivotArea.RowArea);
            fieldDtTakimi.FieldName = "DtTakimi";
            fieldDtTakimi.Caption = rm.GetString("lblDataTakimit", ci);
            //fieldDtTakimi.Width = 400;
            //fieldDtTakimi.Appearance.Cell.WordWrap = true;
            fieldDtTakimi.RowValueLineCount = 7;
            //fieldDtTakimi.ColumnValueLineCount = 10;

            XRPivotGridField fieldStatus = new XRPivotGridField("Status", PivotArea.RowArea);
            fieldStatus.FieldName = "status";
            fieldStatus.Caption = rm.GetString("filterStatus", ci);
            fieldStatus.RowValueLineCount = 7;
            fieldStatus.Width = 50;

            XRPivotGridField fieldOreFillimi = new XRPivotGridField("Ore fillimi", PivotArea.RowArea);
            fieldOreFillimi.FieldName = "OreFillimi";
            fieldOreFillimi.Caption = rm.GetString("labelraportOreFillimi", ci);
            //fieldOreFillimi.Appearance.Cell.WordWrap = true;
            fieldOreFillimi.RowValueLineCount = 7;
            fieldOreFillimi.Width = 80;

            XRPivotGridField fieldKohezgjatje = new XRPivotGridField("Kohezgjatje", PivotArea.RowArea);
            fieldKohezgjatje.FieldName = "Kohezgjatje";
            fieldKohezgjatje.Caption = rm.GetString("labelraportiKohezgjatje", ci);
            //fieldKohezgjatje.Appearance.Cell.WordWrap = true;
            fieldKohezgjatje.RowValueLineCount = 7;
            fieldKohezgjatje.Width = 80;

            XRPivotGridField fieldTeKlienti = new XRPivotGridField("Te Klienti", PivotArea.RowArea);
            fieldTeKlienti.FieldName = "TeKlienti";
            fieldTeKlienti.Caption = rm.GetString("labelraportiTeKlienti", ci);
            //fieldTeKlienti.Appearance.Cell.WordWrap = true;
            fieldTeKlienti.RowValueLineCount = 7;
            fieldTeKlienti.Width = 70;

            XRPivotGridField fieldKlienti = new XRPivotGridField("Klienti", PivotArea.RowArea);
            fieldKlienti.FieldName = "EMERTIMIKF";
            fieldKlienti.Caption = rm.GetString("labelRaportKlienti", ci);
            //fieldKlienti.Appearance.Cell.WordWrap = true;
            fieldKlienti.RowValueLineCount = 7;
            //fieldKlienti.ColumnValueLineCount = 10;

            XRPivotGridField fieldKomente = new XRPivotGridField("Komente", PivotArea.RowArea);
            fieldKomente.FieldName = "Komente";
            fieldKomente.Width = 600;
            //fieldKomente.Appearance.Cell.WordWrap = true;
            fieldKomente.RowValueLineCount = 7;

            XRPivotGridField fieldfusha = new XRPivotGridField("Emertimi", PivotArea.ColumnArea);
            fieldfusha.FieldName = "EMERTIMI";
            fieldfusha.Caption = "Emertimi";
            fieldfusha.Width = 560;
            fieldfusha.RowValueLineCount = 7;

            XRPivotGridField fieldShenime = new XRPivotGridField("Shenime", PivotArea.DataArea);
            fieldShenime.FieldName = "SHENIMETRUPI";
            fieldShenime.Caption = "Shenime";
            fieldShenime.Width = 400;
            //fieldShenime.Appearance.Cell.WordWrap = true;
            fieldShenime.RowValueLineCount = 7;
            //fieldShenime.ColumnValueLineCount = 10;

            //.TextOptions.WordWrap = WordWrap.Wrap;
            fieldShenime.SummaryType = PivotSummaryType.Max;
            //fieldShenime.CellFormat.FormatType = DevExpress.Utils.FormatType.Costum;
            //xrPivotGrid1.Fields["Shenime"].SummaryType = PivotSummaryType.Max;
            xrPivotGrid1.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {fieldAgjent,fieldKlienti,fieldDtTakimi,fieldStatus, fieldOreFillimi,fieldKohezgjatje,fieldTeKlienti,fieldKomente,  fieldfusha,
                fieldShenime});
            xrPivotGrid1.Styles.FieldHeaderStyle = xrPivotGrid1.Styles.FieldHeaderStyle;
            //xrPivotGrid1.Styles.FieldValueStyle = xrPivotGrid1.Styles.FieldHeaderStyle;
            //xrPivotGrid1.FieldValueGrandTotalStyleName = xrPivotGrid1.Styles.FieldHeaderStyle.Name;
            xrPivotGrid1.OptionsView.ShowColumnGrandTotals = false;
            xrPivotGrid1.OptionsView.ShowRowGrandTotals = false;
            xrPivotGrid1.OptionsView.ShowRowTotals = false;
            
        }

        private void EmrateLabelave(CultureInfo ci)
        {
           
           //// xrTableCell1.Text = rm.GetString("labelRaportiEmertimi", ci);
           // xrTableCell2.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
           // xrTableCell3.Text = rm.GetString("labelRaportLimiti", ci);
           // xrTableCell4.Text = rm.GetString("labelMenaxher", ci);
           // xrTableCell5.Text = rm.GetString("labelQyteti", ci);
            xrLabel20.Text = rm.GetString("labelLogoIMB", ci);
            //xrLabel2.Text = rm.GetString("FiltratEmertimi", ci);
            //xrTableCell11.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel1.Text = rm.GetString("labelRaportiAnalitikVeprimtariseTitulli", ci);

        }

        private void xrPivotGrid1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //xrPivotGrid1.BestFit();
            xrPivotGrid1.CanGrow = true;
            xrPivotGrid1.CanShrink = true;
            
            
            //XRPivotGrid grid = (XRPivotGrid)sender;
            //var groupValue = GetCurrentColumnValue("Menaxher");
            //grid.Prefilter.CriteriaString = string.Format("[Menaxher]", groupValue);
          
        }

        private void xrPivotGrid1_FieldValueDisplayText(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            //if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal && e.DisplayText == "Grand Total")
            //{
            //    e.DisplayText = "Totali";
            //}
            
        }
      
        
        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           // this.DataSource = null;
        }

        private void xrPivotGrid1_CustomRowHeight(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotCustomRowHeightEventArgs e)
        {
            
        }

        private void xrPivotGrid1_PrintFieldValue(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs e)
        {
            
        }
    }
}
