using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class RAP_Shitje_kerkesat_ofertat_porosite : DevExpress.XtraReports.UI.XtraReport
    {
		public RAP_Shitje_kerkesat_ofertat_porosite(){InitializeComponent();} 
      
        public RAP_Shitje_kerkesat_ofertat_porosite(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public RAP_Shitje_kerkesat_ofertat_porosite(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);


        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {

            ResourceManager rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel12.Text = rm.GetString("KerkesatOfertatPorositeFaturatShitjesTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel31.Text = rm.GetString("labelNrRendor", ci);
            xrLabel45.Text = rm.GetString("labelRaportiLloji", ci);
            xrLabel27.Text = rm.GetString("labelDokumenti", ci);
            xrLabel29.Text = rm.GetString("labelMonedhaFature", ci);
            xrLabel33.Text = rm.GetString("labelVleraNeMonedhenBaze", ci);
            xrLabel3.Text = rm.GetString("labelRaportKlienti", ci);
            xrLabel6.Text = rm.GetString("labelKrijuesi", ci);
            xrLabel2.Text = rm.GetString("labelRaportiNr", ci);
            xrLabel4.Text = rm.GetString("labelRaportiDtDok", ci);
            xrLabel5.Text = rm.GetString("labelKursi", ci);
            xrLabel71.Text = rm.GetString("labelDtPlanifikimi", ci);
            xrLabel72.Text = rm.GetString("labelDtProdhimi", ci);
            xrLabel14.Text = rm.GetString("labelNentotal", ci);
            xrLabel22.Text = rm.GetString("labelZbritje", ci);
            xrLabel23.Text = rm.GetString("labelTVSH", ci);
            xrLabel24.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel35.Text = rm.GetString("labelTVSH", ci);
            xrLabel34.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel42.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel50.Text = rm.GetString("labelLogoIMB", ci);
            
        }
    }
}
