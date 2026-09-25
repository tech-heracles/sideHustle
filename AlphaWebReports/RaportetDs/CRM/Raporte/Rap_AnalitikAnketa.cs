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
            if (raport.Parameters.Count > 0)
            {
                parameter1.Value = raport.Parameters[2].Value;
                parameter2.Value = raport.Parameters[1].Value;
            }

           
            XRPivotGridField fieldAgjent = new XRPivotGridField("Agjent", PivotArea.RowArea);
            fieldAgjent.FieldName = "EmerMbiemer";
            fieldAgjent.Caption = rm.GetString("labelAgjenti", ci);
            fieldAgjent.Options.ShowGrandTotal = true;
            fieldAgjent.Appearance.Cell.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Top;

            XRPivotGridField fieldDtTakimi = new XRPivotGridField("Date e Takimit", PivotArea.RowArea);
            fieldDtTakimi.FieldName = "DtTakimi";
            fieldDtTakimi.Caption = rm.GetString("lblDataTakimit", ci);
            fieldDtTakimi.RowValueLineCount = 7;

            XRPivotGridField fieldStatus = new XRPivotGridField("Status", PivotArea.RowArea);
            fieldStatus.FieldName = "status";
            fieldStatus.Caption = rm.GetString("filterStatus", ci);
            fieldStatus.RowValueLineCount = 7;
            fieldStatus.Width = 50;

            XRPivotGridField fieldOreFillimi = new XRPivotGridField("Ore fillimi", PivotArea.RowArea);
            fieldOreFillimi.FieldName = "OreFillimi";
            fieldOreFillimi.Caption = rm.GetString("labelraportOreFillimi", ci);
            fieldOreFillimi.RowValueLineCount = 7;
            fieldOreFillimi.Width = 80;

            XRPivotGridField fieldKohezgjatje = new XRPivotGridField("Kohezgjatje", PivotArea.RowArea);
            fieldKohezgjatje.FieldName = "Kohezgjatje";
            fieldKohezgjatje.Caption = rm.GetString("labelraportiKohezgjatje", ci);
            fieldKohezgjatje.RowValueLineCount = 7;
            fieldKohezgjatje.Width = 80;

            XRPivotGridField fieldTeKlienti = new XRPivotGridField("Te Klienti", PivotArea.RowArea);
            fieldTeKlienti.FieldName = "TeKlienti";
            fieldTeKlienti.Caption = rm.GetString("labelraportiTeKlienti", ci);
            fieldTeKlienti.RowValueLineCount = 7;
            fieldTeKlienti.Width = 70;

            XRPivotGridField fieldKlienti = new XRPivotGridField("Klienti", PivotArea.RowArea);
            fieldKlienti.FieldName = "EMERTIMIKF";
            fieldKlienti.Caption = rm.GetString("labelRaportKlienti", ci);
            fieldKlienti.RowValueLineCount = 7;

            XRPivotGridField fieldKomente = new XRPivotGridField("Komente", PivotArea.RowArea);
            fieldKomente.FieldName = "Komente";
            fieldKomente.Width = 600;
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
            fieldShenime.RowValueLineCount = 7;

            fieldShenime.SummaryType = PivotSummaryType.Max;
            xrPivotGrid1.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {fieldAgjent,fieldKlienti,fieldDtTakimi,fieldStatus, fieldOreFillimi,fieldKohezgjatje,fieldTeKlienti,fieldKomente,  fieldfusha,
                fieldShenime});
            xrPivotGrid1.Styles.FieldHeaderStyle = xrPivotGrid1.Styles.FieldHeaderStyle;
            xrPivotGrid1.OptionsView.ShowColumnGrandTotals = false;
            xrPivotGrid1.OptionsView.ShowRowGrandTotals = false;
            xrPivotGrid1.OptionsView.ShowRowTotals = false;
            
        }

        private void EmrateLabelave(CultureInfo ci)
        {
           
           //// xrTableCell1.Text = rm.GetString("labelRaportiEmertimi", ci);
            xrLabel20.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel1.Text = rm.GetString("labelRaportiAnalitikVeprimtariseTitulli", ci);

        }

        private void xrPivotGrid1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPivotGrid1.CanGrow = true;
            xrPivotGrid1.CanShrink = true;
            
            
        }

        private void xrPivotGrid1_FieldValueDisplayText(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            
        }
      
        
        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }

        private void xrPivotGrid1_CustomRowHeight(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotCustomRowHeightEventArgs e)
        {
            
        }

        private void xrPivotGrid1_PrintFieldValue(object sender, DevExpress.XtraReports.UI.PivotGrid.CustomExportFieldValueEventArgs e)
        {
            
        }
    }
}
