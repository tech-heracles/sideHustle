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
    public partial class RAP_Shitje_kerkesat_ofertat_porosite_sipasautorizimeve : DevExpress.XtraReports.UI.XtraReport
    {
		public RAP_Shitje_kerkesat_ofertat_porosite_sipasautorizimeve(){InitializeComponent();} 
    
        public RAP_Shitje_kerkesat_ofertat_porosite_sipasautorizimeve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }

        public RAP_Shitje_kerkesat_ofertat_porosite_sipasautorizimeve(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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
            NrRendKoka.Text = rm.GetString("labelNrRendor", ci);
            LlojiKoka.Text = rm.GetString("labelRaportiLloji", ci);
            DokumentiKoka.Text = rm.GetString("labelDokumenti", ci);
            MonFatKoka.Text = rm.GetString("labelMonedhaFature", ci);
            VlMonBazKoka.Text = rm.GetString("labelVleraNeMonedhenBaze", ci);
            KlientiKoka.Text = rm.GetString("labelRaportKlienti", ci);
            KrijuesiKoka.Text = rm.GetString("labelKrijuesi", ci);
            NrKoka.Text = rm.GetString("labelRaportiNr", ci);
            DtDokKoka.Text = rm.GetString("labelRaportiDtDok", ci);
            KursiKoka.Text = rm.GetString("labelKursi", ci);
            DtPlanKoka.Text = rm.GetString("labelDtPlanifikimi", ci);
            DtProdKoka.Text = rm.GetString("labelDtProdhimi", ci);
            NentotalKoka.Text = rm.GetString("labelNentotal", ci);
            ZbritjeKoka.Text = rm.GetString("labelZbritje", ci);
            TVSHKoka.Text = rm.GetString("labelTVSH", ci);
            TotalKoka.Text = rm.GetString("labelRaportiTotali", ci);
            TVSH2Koka.Text = rm.GetString("labelTVSH", ci);
            Total2Koka.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel42.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel50.Text = rm.GetString("labelLogoIMB", ci);
            
        }
    }
}
