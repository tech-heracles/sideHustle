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

namespace AlphaWebReports.RaportetDs.PasqyratFinaciare.Raporte
{
    public partial class Rap_SituacionPermbledhesShpenzimesh : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_SituacionPermbledhesShpenzimesh(){InitializeComponent();} 
        public Rap_SituacionPermbledhesShpenzimesh(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        { }
        public Rap_SituacionPermbledhesShpenzimesh(CultureInfo ci, int idNdermarrje, int idViti, XtraReport report)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            
            InitializeComponent();
            EmrateLabelave(ci);            


            // Percakton kolonat e pivot grides
            XRPivotGridField fieldNdermarrje = new XRPivotGridField("Njesite", PivotArea.RowArea);
            fieldNdermarrje.FieldName = "NDERMARJEPERSHK";
            fieldNdermarrje.Caption = rm.GetString("labelRaportiNjesite", ci);
            fieldNdermarrje.Width = 200;
            
           

            XRPivotGridField fieldKatShpenzimi = new XRPivotGridField("Shpenzimi", PivotArea.ColumnArea);
            fieldKatShpenzimi.FieldName = "SHPENZIMI";
             fieldKatShpenzimi.Caption = rm.GetString("labelShpenzimi", ci);
            fieldKatShpenzimi.Width = 200;
            fieldKatShpenzimi.ColumnValueLineCount = 2;

            XRPivotGridField fieldPlan = new XRPivotGridField("Plan", PivotArea.DataArea);
            fieldPlan.FieldName = "BUXHETI_2";
            fieldPlan.Caption = "Plan";
            fieldPlan.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldPlan.CellFormat.FormatString = "{0:n0}";
            

            XRPivotGridField fieldFakt = new XRPivotGridField("Fakt", PivotArea.DataArea);
            fieldFakt.FieldName = "FAKT";
            fieldFakt.Caption = rm.GetString("labelFakt", ci);
            fieldFakt.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldFakt.CellFormat.FormatString = "{0:n0}";

            shpenzimePivotGrid.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] { fieldNdermarrje, fieldKatShpenzimi, fieldPlan, fieldFakt});

            shpenzimePivotGrid.Styles.FieldHeaderStyle = shpenzimePivotGrid.Styles.FieldHeaderStyle;
            shpenzimePivotGrid.Styles.FieldValueStyle = shpenzimePivotGrid.Styles.FieldHeaderStyle;
            shpenzimePivotGrid.FieldValueGrandTotalStyleName = shpenzimePivotGrid.Styles.FieldHeaderStyle.Name;

            shpenzimePivotGrid.OptionsView.ShowRowGrandTotals = true;
            shpenzimePivotGrid.OptionsView.ShowRowTotals = true;
        }

        private void EmrateLabelave(CultureInfo ci)
        {

            ResourceManager rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));


             xrLabel12.Text = rm.GetString("TitullRaportiSituacionShpenzimeshPermbledhes", ci);

            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }

    }
}
