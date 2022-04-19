using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.Amortizimi
{
    public partial class Rap_Amortizimi : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Amortizimi(){InitializeComponent();}

        
        public Rap_Amortizimi(ParametraRaporti param, DevExpress.XtraReports.UI.XtraReport raport)
        {

            ResourceManager rm = new ResourceManager("Resources.Strings",
                           System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
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
            xrLabel4.Text = rm.GetString("labelRaportDeriMe", param.Ci);
            parameter6.Value = raport.Parameters["filterDtDok"].Value.ToString().Split('-')[1];
            xrLabel3.Text = raport.Parameters[6].Description;
            parameter7.Value = raport.Parameters[6].Value;
            xrLabel6.Text = raport.Parameters[9].Description;
            parameter8.Value = raport.Parameters[9].Value;
            xrLabel9.Text = raport.Parameters[8].Description;
            parameter9.Value = raport.Parameters[8].Value;
     

          
            
        }


        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));


          
        }

        private void xrLabel19_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (parameter7.Value.Equals("Standarti Shqiptar")) xrLabel19.Text = "0.00";
            //else xrLabel19.Text = String.Format("{0:#,#.00}", GetCurrentColumnValue("init").ToString());
        }

        private void xrLabel44_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void Rap_Amortizimi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
        }
    }
}
