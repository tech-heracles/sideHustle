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
         

            XRPivotGridField fieldData = new XRPivotGridField("data", PivotArea.ColumnArea);
            fieldData.FieldName = "data";
            fieldData.Caption = "data";

            XRPivotGridField fieldAmortizimi = new XRPivotGridField("amortizimi", PivotArea.DataArea);
            //else
            fieldAmortizimi.FieldName = "amortizimi";
            fieldAmortizimi.Caption = "Amortizimi shtese";
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
