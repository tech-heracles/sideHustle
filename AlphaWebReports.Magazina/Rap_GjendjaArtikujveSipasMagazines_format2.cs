using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using DevExpress.XtraReports.UI.PivotGrid;
using DevExpress.XtraPivotGrid;
using DevExpress.Utils;

namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_GjendjaArtikujveSipasMagazines_format2 : DevExpress.XtraReports.UI.XtraReport
    {

        CultureInfo cultInf;
        public Rap_GjendjaArtikujveSipasMagazines_format2()
        {
            InitializeComponent();
        }
        public Rap_GjendjaArtikujveSipasMagazines_format2(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_GjendjaArtikujveSipasMagazines_format2(CultureInfo ci, int idNdermarrje, int idViti, XtraReport report)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                         System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            Furnitor.Value = report.Parameters[1].Value;
            DtDok.Value = report.Parameters[2].Value;
            DtRegj.Value = report.Parameters[3].Value;
            Kartela.Value = report.Parameters[4].Value;
            parameter1.Value = report.Parameters[5].Value;
            KODNJESIA1.Value = report.Parameters[6].Value;
            FurnitorArt.Value = report.Parameters[7].Value;
            DegaAdministrative.Value = report.Parameters[8].Value;
            Grupimi1.Value = report.Parameters[10].Value;
            Grupimi2.Value = report.Parameters[11].Value;
            // Percakton kolonat e pivot grides
            XRPivotGridField fieldKartela = new XRPivotGridField("Kartela", PivotArea.RowArea);
            fieldKartela.FieldName = "KODARTIKULLI";
            fieldKartela.Caption = rm.GetString("labelKartela", ci);
            fieldKartela.Width = 102;
            XRPivotGridField fieldPershkrimArtikulli = new XRPivotGridField("Emertimi", PivotArea.RowArea);
            fieldPershkrimArtikulli.FieldName = "PERSHKRIMARTIKULLI";
            fieldPershkrimArtikulli.Width = 230;
            fieldPershkrimArtikulli.Caption = rm.GetString("labelRaportiEmertimi", ci);
            XRPivotGridField fieldNjesia = new XRPivotGridField("Njesia", PivotArea.RowArea);
            fieldNjesia.FieldName = "KODNJESIA";
            fieldNjesia.Width = 48;
            fieldNjesia.Caption = rm.GetString("labelNjesia", ci);
            XRPivotGridField fieldMagazina = new XRPivotGridField("Magazina", PivotArea.ColumnArea);
            fieldMagazina.FieldName = "MAGAZINA";
            XRPivotGridField fieldGjendja = new XRPivotGridField("Gjendja", PivotArea.DataArea);
            fieldGjendja.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            fieldGjendja.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldGjendja.CellFormat.FormatString = "F02";
            fieldGjendja.Width = 200;
            gjendjeArtPivotGrid.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] { fieldKartela, fieldPershkrimArtikulli, fieldNjesia, fieldMagazina, fieldGjendja });
            cultInf = ci;
            gjendjeArtPivotGrid.Styles.FieldHeaderStyle = gjendjeArtPivotGrid.Styles.FieldHeaderStyle;
            gjendjeArtPivotGrid.Styles.FieldValueStyle = gjendjeArtPivotGrid.Styles.FieldHeaderStyle;
            gjendjeArtPivotGrid.FieldValueGrandTotalStyleName = gjendjeArtPivotGrid.Styles.FieldHeaderStyle.Name;

        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportGjendjaArtikullitSipasMagazinesTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
        }


        /// <summary>
        /// ndryshohet emri default grand total dhe e riemertojme ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gjendjeArtPivotGrid_FieldValueDisplayText(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
                if (e.DisplayText == "Grand Total")
                    e.DisplayText = rm.GetString("RaportFushaGrandTotal", cultInf);
                else
                    e.DisplayText = e.DataField.SummaryType.ToString();
        }

 
    }
}
