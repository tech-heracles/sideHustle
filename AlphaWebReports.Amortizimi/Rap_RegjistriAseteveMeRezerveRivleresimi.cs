
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.Amortizimi
{
    public partial class Rap_RegjistriAseteveMeRezerveRivleresimi : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_RegjistriAseteveMeRezerveRivleresimi(){InitializeComponent();} 
        

        public Rap_RegjistriAseteveMeRezerveRivleresimi(ParametraRaporti param, XtraReport report):this()
        {
            EmrateLabelave(param.Ci);
        }
        
        
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));

            xrLabel17.Text = rm.GetString("lblTitullRaportiRegjisterAseteshRezerveRivleresimi", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel27.Text = rm.GetString("labelVleraBruto", ci);
            xrLabel40.Text = rm.GetString("labelVleraBruto", ci);
            xrLabel4.Text = rm.GetString("labelVleraBruto", ci);
            xrLabel32.Text = rm.GetString("labelRaportGrupi", ci);
            xrLabel71.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel37.Text = rm.GetString("labelKodi", ci);
            xrLabel23.Text = rm.GetString("labelRaportPershkrimi", ci);
            xrLabel29.Text = rm.GetString("filterSeriali", ci);
            xrLabel28.Text = rm.GetString("labelRaportTarga", ci);
            xrLabel36.Text = rm.GetString("lblRaportVitiBlerjes", ci);
            xrLabel30.Text = rm.GetString("lblRaportJetegatesiaVite", ci);
            xrLabel25.Text = rm.GetString("labelRaportAmortizimiAkumuluar", ci);
            xrLabel7.Text = rm.GetString("labelRaportAmortizimiAkumuluar", ci);
            xrLabel78.Text = rm.GetString("labelShtesaVitiAktual", ci);
            xrLabel79.Text = rm.GetString("labelPakesimetVitiAktual", ci);
            xrLabel81.Text = rm.GetString("labelGjendjetVitiAktual", ci);
            xrLabel31.Text = rm.GetString("labelRezervaRivleresimitBruto", ci);
            xrLabel90.Text = rm.GetString("labelRezervaRivleresimitBruto", ci);
            xrLabel1.Text = rm.GetString("labelRezervaRivleresimitBruto", ci);
            xrLabel15.Text = rm.GetString("labelRaportAmortizimiAkumuluar", ci);
            xrLabel24.Text = rm.GetString("labelRezervaRivleresimiNeto", ci);
            xrLabel6.Text = rm.GetString("labelRezervaRivleresimiNeto", ci);
            xrLabel33.Text = rm.GetString("labelDataPakesimit", ci);
            xrLabel12.Text = rm.GetString("lblAmortizimiAkumuluarDatePakesimi", ci);
            xrLabel18.Text = rm.GetString("labelRimarrjaEVitit", ci);
            xrLabel89.Text = rm.GetString("labelRimarrjaEVitit", ci);
            xrLabel86.Text = rm.GetString("labelVleraBrutoShtesaPakesimi", ci);
            xrLabel85.Text = rm.GetString("labelAmortizimiAkumuluarShtesaPakesimi", ci);
            xrLabel14.Text = rm.GetString("labelShpenzimiAmortizimitViti", ci);
            xrLabel10.Text = rm.GetString("labelVleraNeto", ci);
            xrLabel3.Text = rm.GetString("labelVleraNeto", ci);
            xrLabel16.Text = rm.GetString("labelRezervaBrutoPakesimShtesa", ci);
            xrLabel74.Text = rm.GetString("labelRezervaRivleresimi", ci);
            xrLabel2.Text = rm.GetString("labelRimarrjaAkumuluar", ci);
            xrLabel5.Text = rm.GetString("labelGjendjetEMeparshme", ci);
        }

    

       
    }
}
