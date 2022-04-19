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
    public partial class Rap_KartelaAseteve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_KartelaAseteve(){InitializeComponent();}
        
        private int counter = 0;
        CultureInfo ci;

        public Rap_KartelaAseteve(ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_KartelaAseteve(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                           System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[9].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            String grup1 = Convert.ToString( raport.Parameters[4].Value);
            parameter7.Value = raport.Parameters[6].Value;
            parameter6.Value = raport.Parameters["filterDtDok"].Value.ToString().Split('-')[1];
            parameter8.Value = raport.Parameters[8].Value;
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

            xrLabel17.Text = rm.GetString("labelKartelaAseteve", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel37.Text = rm.GetString("labelRaportiNr", ci);
            xrLabel27.Text = rm.GetString("lblNormeAmort", ci);
            xrLabel23.Text = rm.GetString("lblEmertimiMagazine", ci);
            xrLabel29.Text = rm.GetString("labelAsetKodi", ci);
            xrLabel36.Text = rm.GetString("filterSeriali", ci);
            xrLabel30.Text = rm.GetString("labelDateFillimiAmortizimi", ci);
            xrLabel31.Text = rm.GetString("lblAmortGjithsej", ci);
            xrLabel24.Text = rm.GetString("labelVleraGjendje", ci);
            xrLabel25.Text = rm.GetString("labelVleraGjendje", ci);
            xrLabel26.Text = rm.GetString("lblRaportiAmortVjetor", ci);
            xrLabel33.Text = rm.GetString("labelRaportAmortGjithsej", ci);
            xrLabel40.Text = rm.GetString("labelVleraMbetur", ci);
            xrLabel62.Text = rm.GetString("labelRaportiTotali", ci) + ":";
            xrLabel28.Text = rm.GetString("labelRaportPershkrimi", ci);
            xrLabel32.Text = rm.GetString("lblRaportMagazina", ci);
            xrLabel71.Text = rm.GetString("labelLogoIMB", ci);
        }

        private void xrLabel26_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("seriali") != null)
            {
                counter++;
                xrLabel26.Text = counter.ToString();
            }
            else xrLabel26.Text = ""; 
        }

        private void xrLabel35_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                           System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel35.Text = rm.GetString("labelRaportTotaliPer", ci) + " " + GetCurrentColumnValue("magazina") + ":";
        }

        private void xrLabel53_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("jetegjMbetur") != DBNull.Value)
            {
                if (Convert.ToDouble(GetCurrentColumnValue("jetegjMbetur")) < 0)
                    xrLabel53.Text = String.Format("{0:#,#.00}", 0);
            }
        }

        private void Rap_KartelaAseteve_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
        }
    }
}
