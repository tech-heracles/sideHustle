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
    public partial class Rap_AseteJashtePerdorimitMeRezerveRivleresimi : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_AseteJashtePerdorimitMeRezerveRivleresimi(){
            InitializeComponent();
        }
   
        public Rap_AseteJashtePerdorimitMeRezerveRivleresimi(ParametraRaporti param, XtraReport report):
            this()
        {
            EmrateLabelave(param.Ci);
        }


        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel17.Text = rm.GetString("titullRaportiAseteJashtePerdorimiRezervaRivleresimi", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel27.Text = rm.GetString("lblRaportDtDalje", ci);
            xrLabel29.Text = rm.GetString("labelAsetKodi", ci);
            xrLabel36.Text = rm.GetString("filterSeriali", ci);
            xrLabel30.Text = rm.GetString("labelDateBlerje", ci);
            xrLabel31.Text = rm.GetString("labelJetegjatesiaMbetur", ci);
            xrLabel24.Text = rm.GetString("lblRaportVleraBlerje", ci);
            xrLabel25.Text = rm.GetString("lblRaportJetegjatesia", ci);
            xrLabel33.Text = rm.GetString("lblRaportAmortAkumuluar", ci);
            xrLabel40.Text = rm.GetString("labelVleraMbetur", ci);
            xrLabel62.Text = rm.GetString("labelRaportiTotali", ci) + ":";
            xrLabel28.Text = rm.GetString("labelRaportPershkrimi", ci);
            xrLabel32.Text = rm.GetString("labelRaportGrupi", ci);
            xrLabel71.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel14.Text = rm.GetString("lblRaportMagazina", ci);
            xrLabel21.Text = rm.GetString("labelRezervaBrutoPakesimShtesa", ci);
            xrLabel4.Text = rm.GetString("labelRezervaRivleresimi", ci);
            xrLabel7.Text = rm.GetString("labelRimarrjaEVitit", ci);
        }

    }
}
