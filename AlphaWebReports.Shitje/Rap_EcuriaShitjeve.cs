using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraReports.UI.PivotGrid;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_EcuriaShitjeve : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_EcuriaShitjeve() { InitializeComponent(); }
        public Rap_EcuriaShitjeve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_EcuriaShitjeve(System.Globalization.CultureInfo ci, int idNdermarrje, int idViti, XtraReport report)
        {
            InitializeComponent();
            parameter1.Value = report.Parameters["filterKompania"].Value;
            parameter2.Value = report.Parameters["filterMagazina"].Value;
            parameter3.Value = report.Parameters["filterkodifikimartP"].Value;
            parameter4.Value = report.Parameters["filterDtDok"].Value;
            parameter5.Value = report.Parameters["filterKartela"].Value;
            XRPivotGridField fieldDealer = new XRPivotGridField("dealer", DevExpress.XtraPivotGrid.PivotArea.RowArea);
            fieldDealer.FieldName = "dealer";
            fieldDealer.Caption = "Dealer";
            XRPivotGridField fieldDyqani = new XRPivotGridField("dyqani", DevExpress.XtraPivotGrid.PivotArea.RowArea);
            fieldDyqani.FieldName = "dyqani";
            fieldDyqani.Caption = "Dyqani";
            XRPivotGridField fieldKartela = new XRPivotGridField("kartela", DevExpress.XtraPivotGrid.PivotArea.RowArea);
            fieldKartela.FieldName = "kartela";
            fieldKartela.Caption = "Kartela";
            fieldKartela.SortMode = PivotSortMode.Custom;
            XRPivotGridField fieldPershkrimi = new XRPivotGridField("pershkrimi", DevExpress.XtraPivotGrid.PivotArea.RowArea);
            fieldPershkrimi.FieldName = "pershkrimi";
            fieldPershkrimi.Caption = "Pershkrimi";
            fieldPershkrimi.SortMode = PivotSortMode.Custom;
            XRPivotGridField fieldKategoria = new XRPivotGridField("kategoria", DevExpress.XtraPivotGrid.PivotArea.RowArea);
            fieldKategoria.FieldName = "kategoria";
            fieldKategoria.Caption = "Kategoria";
            fieldKategoria.SortMode = PivotSortMode.Custom;
            XRPivotGridField fieldNenKategoria = new XRPivotGridField("nenkategoria", DevExpress.XtraPivotGrid.PivotArea.RowArea);
            fieldNenKategoria.FieldName = "nenkategoria";
            fieldNenKategoria.Caption = "Nenkategoria";
            fieldNenKategoria.SortMode = PivotSortMode.Custom;
            XRPivotGridField fieldJava = new XRPivotGridField("data", PivotArea.ColumnArea);
            fieldJava.FieldName = "data";
            fieldJava.Caption = "Java";
            XRPivotGridField fieldVleraSasi = new XRPivotGridField("sasia", PivotArea.DataArea);
            fieldVleraSasi.FieldName = "sasia";
            fieldVleraSasi.Caption = "Sasia";
            shitjePivotGrid.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] { fieldDealer, fieldDyqani, fieldKartela, fieldPershkrimi, fieldKategoria, fieldNenKategoria, fieldJava, fieldVleraSasi });
            fieldJava.SortMode = PivotSortMode.Custom;
            fieldVleraSasi.SortMode = PivotSortMode.Custom;
            shitjePivotGrid.Styles.FieldHeaderStyle = shitjePivotGrid.Styles.FieldHeaderStyle;
            shitjePivotGrid.Styles.FieldValueStyle = shitjePivotGrid.Styles.FieldHeaderStyle;
            shitjePivotGrid.FieldValueGrandTotalStyleName = shitjePivotGrid.Styles.FieldHeaderStyle.Name;

        }
    }
}
