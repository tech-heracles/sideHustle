using System.Globalization;
using System.Resources;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UI.PivotGrid;
using System;

namespace AlphaWebReports.RaportetDs.PasqyratFinaciare.Raporte
{
    public partial class Rap_BilanciVertetuesSipasNdermarrjeve : XtraReport
    {
		public Rap_BilanciVertetuesSipasNdermarrjeve(){InitializeComponent();} 
        ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
		public Rap_BilanciVertetuesSipasNdermarrjeve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
		this(param.Ci, report)
		{

		}

        public Rap_BilanciVertetuesSipasNdermarrjeve(CultureInfo ci, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            
            XRPivotGridField fieldNrLlogarie = new XRPivotGridField("NRLLOGARI", PivotArea.RowArea);
            fieldNrLlogarie.FieldName = (raport.Parameters["filterLlogariSintetike"].Value).ToString() == "Jo" ? "NRLLOGARI" : "kodikpf";
            fieldNrLlogarie.Caption = rm.GetString("labelRaportiNrLlogari", ci);
            fieldNrLlogarie.Options.ShowGrandTotal = false;


            XRPivotGridField fieldEmertimi = new XRPivotGridField("EMERLLOGARI_1", PivotArea.RowArea);
            fieldEmertimi.FieldName = (raport.Parameters["filterLlogariSintetike"].Value).ToString() == "Jo" ? "EMERLLOGARI_1" : "EMERTIMIKPF";
            fieldEmertimi.Caption = rm.GetString("filterRaportEmerLlogarie", ci);
            fieldEmertimi.Options.ShowGrandTotal = false;

            
            XRPivotGridField fieldMonedhaKod = new XRPivotGridField("MONEDHAKOD", PivotArea.RowArea);
            fieldMonedhaKod.FieldName = "MONEDHAKOD";
            fieldMonedhaKod.Caption = rm.GetString("labelFilterAvancuarMonedha", ci);
            fieldMonedhaKod.Options.ShowGrandTotal = false;

            XRPivotGridField fieldNdermarrje = new XRPivotGridField("NDERMARJEKODI", PivotArea.ColumnArea);
            fieldNdermarrje.FieldName = "NDERMARJEKODI";
            fieldNdermarrje.SortOrder = PivotSortOrder.Descending;


            XRPivotGridField fieldDebiFillestare = new XRPivotGridField("GjendjaParaDebi", PivotArea.DataArea);
            fieldDebiFillestare.FieldName = "GjendjaParaDebi";
            fieldDebiFillestare.Caption = $"{rm.GetString("labelRaportiGjendjeFillestare", ci)} {rm.GetString("labelRaportiDebi", ci)}";
            fieldDebiFillestare.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldDebiFillestare.CellFormat.FormatString = "{0:#,#0.00}";
            fieldDebiFillestare.Options.ShowGrandTotal = true;
            fieldDebiFillestare.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Custom;

            XRPivotGridField fieldKrediFillestare = new XRPivotGridField("GjendjaParaKredi", PivotArea.DataArea);
            fieldKrediFillestare.FieldName = "GjendjaParaKredi";
            fieldKrediFillestare.Caption = $"{rm.GetString("labelRaportiGjendjeFillestare", ci)} {rm.GetString("labelRaportiKredi", ci)}";
            fieldKrediFillestare.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldKrediFillestare.CellFormat.FormatString = "{0:#,#0.00}";
            fieldKrediFillestare.Options.ShowGrandTotal = true;
            fieldKrediFillestare.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Custom;

            XRPivotGridField fieldDebi = new XRPivotGridField("levizjadebitore", PivotArea.DataArea);
            fieldDebi.FieldName = "levizjadebitore";
            fieldDebi.Caption = $"{rm.GetString("filterRaportLevizje", ci)} {rm.GetString("labelRaportiDebi", ci)}";
            fieldDebi.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldDebi.CellFormat.FormatString = "{0:#,#0.00}";
            fieldDebi.Options.ShowGrandTotal = true;

            XRPivotGridField fieldKredi = new XRPivotGridField("levizjakreditore", PivotArea.DataArea);
            fieldKredi.FieldName = "levizjakreditore";
            fieldKredi.Caption = $"{rm.GetString("filterRaportLevizje", ci)} {rm.GetString("labelRaportiKredi", ci)}";
            fieldKredi.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldKredi.CellFormat.FormatString = "{0:#,#0.00}";
            fieldKredi.Options.ShowGrandTotal = true;

            XRPivotGridField fieldLevizja = new XRPivotGridField("levizja", PivotArea.DataArea);
            fieldLevizja.FieldName = "levizja";
            fieldLevizja.Caption = $"{rm.GetString("filterRaportLevizje", ci)} {rm.GetString("labelGjithsej", ci)}";
            fieldLevizja.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldLevizja.CellFormat.FormatString = "{0:#,#0.00}";
            fieldLevizja.Options.ShowGrandTotal = true;

            XRPivotGridField fieldDebiGjendje = new XRPivotGridField("GjendjaPasDebi", PivotArea.DataArea);
            fieldDebiGjendje.FieldName = "GjendjaPasDebi";
            fieldDebiGjendje.Caption = $"{rm.GetString("labelRaportGjendje", ci)} {rm.GetString("labelRaportiDebi", ci)}";
            fieldDebiGjendje.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldDebiGjendje.CellFormat.FormatString = "{0:#,#0.00}";
            fieldDebiGjendje.Options.ShowGrandTotal = true;
            fieldDebiGjendje.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Custom;

            XRPivotGridField fieldKrediGjendje = new XRPivotGridField("GjendjaPasKredi", PivotArea.DataArea);
            fieldKrediGjendje.FieldName = "GjendjaPasKredi";
            fieldKrediGjendje.Caption = $"{rm.GetString("labelRaportGjendje", ci)} {rm.GetString("labelRaportiKredi", ci)}";
            fieldKrediGjendje.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldKrediGjendje.CellFormat.FormatString = "{0:#,#0.00}";
            fieldKrediGjendje.Options.ShowGrandTotal = true;
            fieldKrediGjendje.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Custom;

            xrPivotGrid1.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {fieldNrLlogarie, fieldEmertimi,fieldMonedhaKod,
               fieldNdermarrje, fieldDebiFillestare, fieldKrediFillestare, fieldDebi, fieldKredi, fieldLevizja, fieldDebiGjendje, fieldKrediGjendje});


            xrPivotGrid1.OptionsPrint.MergeColumnFieldValues = true;
            xrPivotGrid1.OptionsPrint.MergeRowFieldValues = false;
            xrPivotGrid1.OptionsView.ShowColumnGrandTotals = true;
            xrPivotGrid1.OptionsView.ShowRowGrandTotals = raport.Parameters["monedheLl"].Value.ToString() == "Jo";
            
            

            
            xrPivotGrid1.Styles.FieldHeaderStyle = xrPivotGrid1.Styles.FieldHeaderStyle;
            xrPivotGrid1.Styles.FieldValueStyle = xrPivotGrid1.Styles.FieldHeaderStyle;
            xrPivotGrid1.FieldValueGrandTotalStyleName = xrPivotGrid1.Styles.FieldHeaderStyle.Name;
        }
        private void EmrateLabelave(CultureInfo ci)
        {

            xrLabel1.Text = rm.GetString("TitullRaportiBilanciVertetuesSipasNdermarrjes", ci);
        }

     
    }
}
