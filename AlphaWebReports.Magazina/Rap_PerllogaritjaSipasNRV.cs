using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using DevExpress.XtraPivotGrid;
using DevExpress.Data.PivotGrid;
using DevExpress.XtraReports.UI.PivotGrid;

namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_PerllogaritjaSipasNRV : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_PerllogaritjaSipasNRV(){InitializeComponent();} 
        public Rap_PerllogaritjaSipasNRV(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, report)
        {

        }

        public Rap_PerllogaritjaSipasNRV(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                    System.Reflection.Assembly.Load("App_GlobalResources"));

            InitializeComponent();
            EmrateLabelave(ci);
           parameter2.Value = raport.Parameters["IdNdermarje"].Value;
            parameter4.Value = raport.Parameters["filterDtDok"].Value;
            parameter7.Value = raport.Parameters["filterKartela"].Value;
            // Percakton kolonat e pivot grides
            XRPivotGridField fieldKartela = new XRPivotGridField("Aparati", PivotArea.RowArea);
            fieldKartela.FieldName = "KODARTIKULLI";
             fieldKartela.Width = 130;
            fieldKartela.AreaIndex = 1;
            fieldKartela.Caption = "Aparati";

            XRPivotGridField fieldGjendjeMag = new XRPivotGridField("Gjendje", PivotArea.DataArea);
            fieldGjendjeMag.FieldName = "GjendjeMag";
            fieldGjendjeMag.Caption = "Gjendje";
            fieldGjendjeMag.AreaIndex = 2;

            XRPivotGridField fieldCmimiMag = new XRPivotGridField("Cmimi", PivotArea.DataArea);
            fieldCmimiMag.FieldName = "CmimMag";
            fieldCmimiMag.Caption = "Cmimi";
            fieldCmimiMag.AreaIndex = 3;

            XRPivotGridField fieldNRV_Effect = new XRPivotGridField("NRV Effect", PivotArea.DataArea);
            fieldNRV_Effect.FieldName = "NRV_Effect";
            fieldNRV_Effect.Caption = " NRV effect";
            fieldNRV_Effect.AreaIndex = 4;



            XRPivotGridField fieldMagazina = new XRPivotGridField("Magazina", PivotArea.ColumnArea);
            fieldMagazina.FieldName = "kodmag";
            fieldMagazina.Caption = rm.GetString("labelFilterAvancuarKodbari", ci);

            XRPivotGridField fieldGjendjeTotal = new XRPivotGridField("Gjendje total", PivotArea.RowArea);
            fieldGjendjeTotal.FieldName = "GjendjeTotal";
            fieldGjendjeTotal.Caption = "Gjendje total";
            fieldGjendjeTotal.AreaIndex = 5;

            XRPivotGridField fieldCmimiTotal = new XRPivotGridField("Cmimi total", PivotArea.RowArea);
            fieldCmimiTotal.FieldName = "CmimTotal";
            fieldCmimiTotal.Caption = "Cmimi total";
            fieldCmimiTotal.AreaIndex = 6;

            XRPivotGridField fieldNRV = new XRPivotGridField("Subject to NRV", PivotArea.RowArea);
            fieldNRV.FieldName = "NRV";
            fieldNRV.Caption = "Subject to NRV";
            fieldNRV.AreaIndex = 7;

            XRPivotGridField fieldNRV_Total = new XRPivotGridField("NRV Total", PivotArea.RowArea);
            fieldNRV_Total.FieldName = "NRV_Total";
            fieldNRV_Total.Caption = "NRV effect total";
            fieldNRV_Total.AreaIndex = 8;

            fieldCmimiMag.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            fieldCmimiMag.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldCmimiMag.CellFormat.FormatString = "F02";

            fieldGjendjeMag.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            fieldGjendjeMag.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldGjendjeMag.CellFormat.FormatString = "F02";

            fieldNRV_Effect.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            fieldNRV_Effect.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldNRV_Effect.CellFormat.FormatString = "F02";

            xrPivotGrid_NRV.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] { fieldKartela, fieldMagazina, fieldGjendjeMag, fieldCmimiMag, fieldNRV_Effect, fieldNRV, fieldGjendjeTotal, fieldCmimiTotal, fieldNRV_Total});
            xrPivotGrid_NRV.Styles.FieldHeaderStyle = xrPivotGrid_NRV.Styles.FieldHeaderStyle;
            xrPivotGrid_NRV.Styles.FieldValueStyle = xrPivotGrid_NRV.Styles.FieldHeaderStyle;
            xrPivotGrid_NRV.FieldValueGrandTotalStyleName = xrPivotGrid_NRV.Styles.FieldHeaderStyle.Name;
            this.PageWidth = Convert.ToInt32(xrPivotGrid_NRV.ActualWidth  + (xrPivotGrid_NRV.Fields.Count * 100));

        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));

           // xrLabel12.Text = rm.GetString("TitullRaportiGjendjaMagazinesMeIMEIExp", ci);
          

        }

    }
}
