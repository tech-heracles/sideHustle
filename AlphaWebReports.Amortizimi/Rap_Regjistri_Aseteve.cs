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
    public partial class Rap_Regjistri_Aseteve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Regjistri_Aseteve(){InitializeComponent();}
        
        private int counter = 0;
        CultureInfo ci;

        public Rap_Regjistri_Aseteve(ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_Regjistri_Aseteve(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                           System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[9].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            String grup1 = Convert.ToString(raport.Parameters[4].Value);
            parameter7.Value = raport.Parameters[7].Value;
            parameter6.Value = raport.Parameters["filterDtDok"].Value.ToString().Split('-')[1];
            parameter8.Value = raport.Parameters[12].Value;
            EmrateLabelave(ci);
            this.ci = ci;
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

        ////    xrLabel75.Text = rm.GetString("labelRaportSerialiFillestar", ci);
        }

    
        private void xrLabel35_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                           System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel35.Text = rm.GetString("labelRaportTotaliPer", ci) + " " + GetCurrentColumnValue("GrupiNiv1") + ":";
        }

        private void xrLabel53_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("jetegjMbetur") != DBNull.Value)
            {
            }
        }

        private void xrLabel76_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }

        private void Rap_Regjistri_Aseteve_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
        }
    }
}
