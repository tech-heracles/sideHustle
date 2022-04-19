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
    public partial class Rap_BlerjeMujore : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_BlerjeMujore(){InitializeComponent();} 
        CultureInfo ci;
        

        public Rap_BlerjeMujore(ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_BlerjeMujore(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            xrLabel54.Text = raport.Parameters[9].Description + ":";
            parameter1.Value = raport.Parameters[9].Value;
            xrLabel57.Text = raport.Parameters[1].Description + ":";
            parameter2.Value = raport.Parameters[1].Value;
            xrLabel60.Text = raport.Parameters[2].Description;
            parameter3.Value = raport.Parameters[2].Value;
            xrLabel70.Text = raport.Parameters[0].Description;
            parameter4.Value = raport.Parameters[0].Value;
            xrLabel1.Text =  rm.GetString("labelRaportGrupi", ci);
            parameter5.Value = raport.Parameters[4].Value;
            xrLabel8.Text = raport.Parameters[5].Description;
            parameter9.Value = raport.Parameters[5].Value;
            xrLabel3.Text = raport.Parameters[7].Description;
            parameter7.Value = raport.Parameters[7].Value;
            xrLabel6.Text = raport.Parameters[8].Description;
            parameter8.Value = raport.Parameters[8].Value;
            xrLabel4.Text = raport.Parameters["filterNumerProjekti"].Description + ":";
            parameter10.Value = raport.Parameters["filterNumerProjekti"].Value;
            EmrateLabelave(ci);
            this.ci = ci;

        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel17.Text = rm.GetString("lblRaportTitulliBlerjetMujore", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel27.Text = rm.GetString("labelFilterAvancuarGrupi", ci);
            xrLabel29.Text = rm.GetString("labelAsetKodi", ci);
            xrLabel36.Text = rm.GetString("filterSeriali", ci);
            xrLabel30.Text = rm.GetString("labelDateBlerje", ci);
            xrLabel31.Text = rm.GetString("labelRaportiEmertimi", ci);
            xrLabel24.Text = rm.GetString("lblRaportVleraBlerje", ci);
            xrLabel25.Text = rm.GetString("lblKodFurnitori", ci);
            xrLabel33.Text = rm.GetString("lblNrFature", ci);
            xrLabel40.Text = rm.GetString("labelRaportMagazina", ci);
            xrLabel62.Text = rm.GetString("labelRaportiTotali", ci) + ":";
            xrLabel28.Text = rm.GetString("labelRaportPershkrimi", ci);
            xrLabel32.Text = rm.GetString("labelRaportGrupi", ci);
            xrLabel71.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel14.Text = rm.GetString("lblRaportMagazina", ci);
            xrLabel16.Text = rm.GetString("labelNumerProjekti", ci);
        }

        private void xrLabel35_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel35.Text = rm.GetString("labelRaportiTotali", ci) + " " + GetCurrentColumnValue("Magazina") + ":";
        }

        private void xrLabel53_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("jetegjMbetur") != DBNull.Value)
            {
                if (Convert.ToDouble(GetCurrentColumnValue("jetegjMbetur")) < 0)
                    xrLabel53.Text = String.Format("{0:#,#.00}", 0);
            }
        }

        private void xrLabel20_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel20.Text = rm.GetString("labelRaportiTotali", ci) + " " + GetCurrentColumnValue("Grupi1") + ":";
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void Rap_BlerjeMujore_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
        }
    }
}
