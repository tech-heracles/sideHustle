using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using DevExpress.XtraReports.UI.PivotGrid;
using DevExpress.XtraPivotGrid;

namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_GjendjaArtikujveSipasMagazines : DevExpress.XtraReports.UI.XtraReport
    {
        
    
        CultureInfo cultInf;
        public Rap_GjendjaArtikujveSipasMagazines()
        {
            InitializeComponent();
        }
        public Rap_GjendjaArtikujveSipasMagazines(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }

        public Rap_GjendjaArtikujveSipasMagazines(CultureInfo ci, int idNdermarrje, int idViti, XtraReport report)
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
            XRPivotGridField fieldKodbari = new XRPivotGridField("Kodbari", PivotArea.RowArea);
            fieldKodbari.FieldName = "KODBARI";
            fieldKodbari.Caption = rm.GetString("labelFilterAvancuarKodbari", ci);
            XRPivotGridField fieldPershkrimArtikulli = new XRPivotGridField("Emertimi", PivotArea.RowArea);
            fieldPershkrimArtikulli.FieldName = "PERSHKRIMARTIKULLI";
            fieldPershkrimArtikulli.Width = 130;
            fieldPershkrimArtikulli.Caption = rm.GetString("labelRaportiEmertimi", ci);
            XRPivotGridField fieldNjesia = new XRPivotGridField("Njesia", PivotArea.RowArea);
            fieldNjesia.FieldName = "KODNJESIA";
            fieldNjesia.Width = 70;
            fieldNjesia.Caption = rm.GetString("labelNjesia", ci);
            XRPivotGridField fieldMagazina = new XRPivotGridField("Magazina", PivotArea.ColumnArea);
            fieldMagazina.FieldName = "MAGAZINA";
            XRPivotGridField fieldGjendja = new XRPivotGridField("Gjendja", PivotArea.DataArea);
            fieldGjendja.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            fieldGjendja.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldGjendja.CellFormat.FormatString = "F02";
            gjendjeArtPivotGrid.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] { fieldKartela, fieldKodbari, fieldPershkrimArtikulli, fieldNjesia, fieldMagazina, fieldGjendja });
            cultInf = ci;
            gjendjeArtPivotGrid.Styles.FieldHeaderStyle = gjendjeArtPivotGrid.Styles.FieldHeaderStyle;
            gjendjeArtPivotGrid.Styles.FieldValueStyle = gjendjeArtPivotGrid.Styles.FieldHeaderStyle;
            gjendjeArtPivotGrid.FieldValueGrandTotalStyleName = gjendjeArtPivotGrid.Styles.FieldHeaderStyle.Name;
            parameterRaportFushaGrandTotal.Value = rm.GetString("RaportFushaGrandTotal", ci);
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

   
    }
}
