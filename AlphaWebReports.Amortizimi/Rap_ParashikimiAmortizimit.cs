using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using DevExpress.XtraReports.UI.PivotGrid;
using DevExpress.XtraPivotGrid;
using DevExpress.Data.PivotGrid;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.Amortizimi
{
    public partial class Rap_ParashikimiAmortizimit : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ParashikimiAmortizimit(){InitializeComponent();}
        

        public Rap_ParashikimiAmortizimit(ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_ParashikimiAmortizimit(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {

            ResourceManager rm = new ResourceManager("Resources.Strings",
                           System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            xrLabel54.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel57.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            xrLabel60.Text = raport.Parameters[2].Description;
            parameter3.Value = raport.Parameters[2].Value;
            xrLabel70.Text = raport.Parameters[3].Description;
            parameter4.Value = raport.Parameters[3].Value;
            xrLabel1.Text = raport.Parameters[4].Description;
            parameter5.Value = raport.Parameters[4].Value;
            xrLabel4.Text = raport.Parameters[5].Description;
            parameter6.Value = raport.Parameters[5].Value;
            //xrLabel3.Text = raport.Parameters[6].Description;
            //parameter7.Value = raport.Parameters[6].Value;
          
            EmrateLabelave(ci);


            // Percakton kolonat e pivot grides
            XRPivotGridField fieldGrupi = new XRPivotGridField("Grupi", PivotArea.RowArea);
            fieldGrupi.FieldName = "KODKODIFIKIMI";
            fieldGrupi.Caption = rm.GetString("labelGrupi", ci);

            XRPivotGridField fieldPershkrimi = new XRPivotGridField("Pershkrimi", PivotArea.RowArea);
            fieldPershkrimi.FieldName = "pershkrimiPrind";
            fieldPershkrimi.Caption = 
                rm.GetString("lblEmertimi", ci);
            XRPivotGridField fieldAmortAkumuluar = new XRPivotGridField("Amort.Akumuluar", PivotArea.RowArea);
            fieldAmortAkumuluar.FieldName = "amortizimGjithsej";
            fieldAmortAkumuluar.Caption = 
                rm.GetString("labelAmortAkumuluar", ci);
            //fieldAmortAkumuluar.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //fieldAmortAkumuluar.CustomTotals.Add(PivotSummaryType.Sum);
            //fieldAmortAkumuluar.TotalsVisibility = PivotTotalsVisibility.CustomTotals;
         


            XRPivotGridField fieldData = new XRPivotGridField("data", PivotArea.ColumnArea);
            fieldData.FieldName = "data";
            fieldData.Caption = "data";
                //rm.GetString("labelRaportiCmimet", ci);

            XRPivotGridField fieldAmortizimi = new XRPivotGridField("amortizimi", PivotArea.DataArea);
            //if (Convert.ToInt32(Grupim2.Value.ToString()) == 2)
            //{
            //    CmimiValue.Text = rm.GetString("cmbboxItemFilterAvancCmimiD", ci);
            //    fieldVleraCmimi.FieldName = "CMIMI2";
            //}
            //else
            //{
                //CmimiValue.Text = rm.GetString("cmbboxItemFilterAvancCmimiP", ci);
            fieldAmortizimi.FieldName = "amortizimi";
           // }
            fieldAmortizimi.Caption = "Amortizimi shtese";
                //rm.GetString("labelRaportiCmimet", ci);
            fieldAmortizimi.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldAmortizimi.CellFormat.FormatString = "{0:#,#.#0}";
            fieldAmortAkumuluar.CellFormat.FormatString = "{0:#,#.#0}";
            cmimetPivotGrid.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {fieldGrupi, fieldPershkrimi, fieldAmortAkumuluar,
             fieldData   , fieldAmortizimi});
            cmimetPivotGrid.Styles.FieldHeaderStyle = cmimetPivotGrid.Styles.FieldHeaderStyle;
            cmimetPivotGrid.Styles.FieldValueStyle = cmimetPivotGrid.Styles.FieldHeaderStyle;
            cmimetPivotGrid.FieldValueGrandTotalStyleName = cmimetPivotGrid.Styles.FieldHeaderStyle.Name;
         
        }


        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
            this.DataSource = null;
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel17.Text = rm.GetString("lblRaportParashikimi", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            //xrLabel37.Text = rm.GetString("labelLlojDokumenti", ci);
            //xrLabel27.Text = rm.GetString("filterNrDokRezervime", ci);
            //xrLabel35.Text = rm.GetString("labelDateDok", ci);
            //xrLabel23.Text = rm.GetString("labelFilterAvancuarMagazina", ci);
            //xrLabel29.Text = rm.GetString("labelKodi", ci);
            //xrLabel36.Text = rm.GetString("filterSeriali", ci);
            //xrLabel30.Text = rm.GetString("lblRaportiDtAmortizimi", ci);
            //xrLabel31.Text = rm.GetString("lblRaportiDtMePare", ci);
            //xrLabel24.Text = rm.GetString("lblRaportiVleftaGjendje", ci);
            //xrLabel25.Text = rm.GetString("lblRaportiAmortAkumuluar", ci);
            //xrLabel26.Text = rm.GetString("lblRaportiAmortVjetor", ci);
            //xrLabel32.Text = rm.GetString("labelRaportVlefta", ci) + "+/-";
            //xrLabel33.Text = rm.GetString("lblRaportiHDAmortGjith", ci);
            //xrLabel40.Text = rm.GetString("lblRaportiHDAmortVjetor", ci);
            //xrLabel34.Text = rm.GetString("lblRaportiHDAmortShtese", ci);
            //xrLabel38.Text = rm.GetString("lblRaportiDite", ci);
            //xrLabel39.Text = rm.GetString("loginLoginPerdoruesi", ci);
            //xrLabel62.Text = rm.GetString("labelRaportiTotali", ci);
            //xrLabel71.Text = rm.GetString("labelLogoIMB", ci);
          
        }

        private void cmimetPivotGrid_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            cmimetPivotGrid.BestFit();
        }

        private void cmimetPivotGrid_FieldValueDisplayText(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotFieldDisplayTextEventArgs e)
        {
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal && e.DisplayText == "Grand Total")
            {
                e.DisplayText = "Total";
            }
        }

        private void Rap_ParashikimiAmortizimit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
        }
    }
}
