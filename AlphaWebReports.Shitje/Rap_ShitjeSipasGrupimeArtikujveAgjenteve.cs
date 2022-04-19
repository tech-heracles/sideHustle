using System.Globalization;
using System.Resources;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraReports.UI.PivotGrid;
using System;
using DevExpress.XtraReports.UI;
using System.Collections;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_ShitjeSipasGrupimeArtikujveAgjenteve : DevExpress.XtraReports.UI.XtraReport
    {

        ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

        public Rap_ShitjeSipasGrupimeArtikujveAgjenteve()
        {
             InitializeComponent();
        }
        
        public Rap_ShitjeSipasGrupimeArtikujveAgjenteve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) : this(param.Ci, report)
        { }

        public Rap_ShitjeSipasGrupimeArtikujveAgjenteve(CultureInfo ci, DevExpress.XtraReports.UI.XtraReport report)
        {
            InitializeComponent();
            EmrateLabelave(ci);

            //inicializimi i pivotgrides:

            //Agjenti
            XRPivotGridField fieldAgjenti = new XRPivotGridField("Agjenti", PivotArea.RowArea);
            fieldAgjenti.FieldName = "AGJENTI";
            fieldAgjenti.Caption = rm.GetString("RptShitjeSipasGrupArtikulliMenaxheret", ci);
            fieldAgjenti.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Default;
            fieldAgjenti.Options.ShowGrandTotal = false;

            //Klienti
            XRPivotGridField fieldKodKlienti = new XRPivotGridField("KLIENTI", PivotArea.RowArea);
            fieldKodKlienti.FieldName = "KLIENTI";
            fieldKodKlienti.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Default;
            fieldKodKlienti.Caption = rm.GetString("RptShitjeSipasGrupArtikulliKlienetetAgjentet", ci);
            fieldKodKlienti.Options.ShowGrandTotal = false;
            fieldKodKlienti.Options.ShowTotals = false;

			//Pershkrimi
			XRPivotGridField fieldPershkrimi = new XRPivotGridField("PERSHKRIMI", PivotArea.RowArea);
			fieldPershkrimi.FieldName = "PERSHKRIMI";
			fieldPershkrimi.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Default;
			fieldPershkrimi.Caption = rm.GetString("RptShitjeSipasGrupArtikulliPershkrimi", ci);
            fieldPershkrimi.Options.ShowGrandTotal = false;


            //GrupiMSHITJE
            XRPivotGridField fieldgrupimshitje = new XRPivotGridField("GRUPIMSHITJE", PivotArea.ColumnArea);
			fieldgrupimshitje.FieldName = "GRUPIMSHITJE";
			fieldgrupimshitje.SortMode = PivotSortMode.Custom;
			fieldgrupimshitje.Options.ShowGrandTotal = true;
			fieldgrupimshitje.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;

			//Grupim  artikulli
			XRPivotGridField fieldGrup = new XRPivotGridField("Grupi", PivotArea.ColumnArea);
            fieldGrup.FieldName = "GRUP";
            fieldGrup.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;


            //Totali SHitjes
            XRPivotGridField fieldShitje = new XRPivotGridField("VLERA", PivotArea.DataArea);
            fieldShitje.FieldName = "VLERA";
            fieldShitje.Caption = "Vlera";
            fieldShitje.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldShitje.CellFormat.FormatString = "{0:#,#.#0}";
            fieldShitje.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            fieldShitje.TotalCellFormat.FormatString = "{0:n2}";
            fieldShitje.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            fieldShitje.GrandTotalCellFormat.FormatString = "{0:n2}";

            XRPivotGridField fieldShitjetipi = new XRPivotGridField("TIPI", PivotArea.DataArea);
            fieldShitjetipi.FieldName = "TIPI";
            fieldShitjetipi.Caption = "TIPI";
            fieldShitjetipi.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldShitjetipi.Options.ShowValues = false;
            fieldShitjetipi.Options.ShowTotals = false;
            fieldShitjetipi.Options.ShowGrandTotal = true;

            XRPivotGridField fieldShitjev1 = new XRPivotGridField("v1", PivotArea.DataArea);
            fieldShitjev1.FieldName = "v1";
            fieldShitjev1.Caption = "v1";
            fieldShitjev1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldShitjev1.Options.ShowValues = false;
            fieldShitjev1.Options.ShowTotals = false;
            fieldShitjev1.Options.ShowGrandTotal = false;

            XRPivotGridField fieldShitjev2 = new XRPivotGridField("v2", PivotArea.DataArea);
            fieldShitjev2.FieldName = "v2";
            fieldShitjev2.Caption = "v2";
            fieldShitjev2.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldShitjev2.Options.ShowValues = false;
            fieldShitjev2.Options.ShowTotals = false;
            fieldShitjev2.Options.ShowGrandTotal = false;

            xrPivotGrid1.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] { fieldAgjenti, fieldKodKlienti, fieldPershkrimi,
				fieldgrupimshitje,
				fieldGrup, fieldShitje, fieldShitjetipi, fieldShitjev1, fieldShitjev2 
			});
            xrPivotGrid1.Styles.FieldHeaderStyle = xrPivotGrid1.Styles.FieldHeaderStyle;
            xrPivotGrid1.Styles.FieldValueStyle = xrPivotGrid1.Styles.FieldHeaderStyle;
            xrPivotGrid1.FieldValueGrandTotalStyleName = xrPivotGrid1.Styles.FieldHeaderStyle.Name;

        }

        private void EmrateLabelave(CultureInfo ci)
        {
            xrLabel1.Text = rm.GetString("RptShitjeSipasGrupArtikulliAgjentiTitulli", ci);
        }
        
          
    }
}
