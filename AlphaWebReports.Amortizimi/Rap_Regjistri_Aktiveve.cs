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
    public partial class Rap_Regjistri_Aktiveve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Regjistri_Aktiveve(){InitializeComponent();}
        
        private int counter = 0;
        CultureInfo ci;

        public Rap_Regjistri_Aktiveve(ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_Regjistri_Aktiveve(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
           
            InitializeComponent();
            EmrateLabelave(ci);
            xrLabel54.Text = raport.Parameters[6].Description;
            parameter1.Value = raport.Parameters[6].Value;
            xrLabel57.Text = raport.Parameters[1].Description ;
            parameter2.Value = raport.Parameters[1].Value;
            xrLabel60.Text = raport.Parameters[2].Description;
            parameter3.Value = raport.Parameters[2].Value;
            xrLabel70.Text = raport.Parameters[4].Description;
            parameter4.Value = raport.Parameters[4].Value;
            xrLabel1.Text = raport.Parameters[5].Description;
            String grup1 = Convert.ToString( raport.Parameters[5].Value);

            EmrateLabelave(ci);
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

        private void xrLabel26_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //    counter++;
        }

        private void xrLabel35_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //ResourceManager rm = new ResourceManager("Resources.Strings",
        }

        private void xrLabel53_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }

        private void Rap_Regjistri_Aktiveve_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);

        }
    }
}
