using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraReports.UI.PivotGrid;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_Evidenca_E_Shitjeve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Evidenca_E_Shitjeve(){InitializeComponent();} 
        CultureInfo cultInf;
        public Rap_Evidenca_E_Shitjeve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_Evidenca_E_Shitjeve(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                          System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            Ndermarja.Value = raport.Parameters[0].Value;
            Monedha.Value = raport.Parameters[1].Value;
            DtDok.Value = raport.Parameters[2].Value;
            DtRegj.Value = raport.Parameters[3].Value;
            NrDok.Value = raport.Parameters[4].Value;
            Muaji.Value = raport.Parameters[5].Value;
            KodiPunonjesit.Value = raport.Parameters[6].Value;
            Departamenti.Value = raport.Parameters[7].Value;
            NenDepartamenti.Value = raport.Parameters[8].Value;


            // Percakton kolonat e pivot grides
            XRPivotGridField fieldGrupimi1 = new XRPivotGridField("pershkrimGrupi1", PivotArea.RowArea);
            fieldGrupimi1.Caption = rm.GetString("filterGrupimP", ci);
            fieldGrupimi1.Width = 70;
            XRPivotGridField fieldPikeShitje = new XRPivotGridField("pikeShitje", PivotArea.ColumnArea);
            //else 
            XRPivotGridField fieldSasiaNjesi1 = new XRPivotGridField("sasiaNjesi1", PivotArea.DataArea);
            fieldSasiaNjesi1.Caption = "Sasia njesi 1";
            fieldSasiaNjesi1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldSasiaNjesi1.CellFormat.FormatString = "{0:#,#.00}";
            XRPivotGridField fieldSasiaNjesi2 = new XRPivotGridField("sasiaNjesi2", PivotArea.DataArea);
            fieldSasiaNjesi2.Caption = "Sasia njesi 2";
            fieldSasiaNjesi2.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldSasiaNjesi2.CellFormat.FormatString = "{0:#,#.00}";
            XRPivotGridField fieldShitjeTotale = new XRPivotGridField("ShitjeTotale", PivotArea.DataArea);
            fieldShitjeTotale.Caption = "Shitje totale";
            fieldShitjeTotale.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldShitjeTotale.CellFormat.FormatString = "{0:#,#.00}";
       
            //Shton kolonat ne pivot gride
            xrPivotGrid2.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] { fieldGrupimi1, fieldPikeShitje, fieldSasiaNjesi1, fieldSasiaNjesi2, fieldShitjeTotale });
            xrPivotGrid2.Styles.FieldHeaderStyle = xrPivotGrid2.Styles.FieldHeaderStyle;
            xrPivotGrid2.Styles.FieldValueStyle = xrPivotGrid2.Styles.FieldHeaderStyle;
            xrPivotGrid2.FieldValueGrandTotalStyleName = xrPivotGrid2.Styles.FieldHeaderStyle.Name;
            cultInf = ci;
        }


        private void xrPivotGrid1_FieldValueDisplayText(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)
                if (e.DisplayText == "Grand Total")
                    e.DisplayText = rm.GetString("labelRaportiTotali", cultInf);
                else
                    e.DisplayText = e.DataField.SummaryType.ToString();
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("lblRaportEvidencaEShitjeve", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
             xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
        }
    }
}
