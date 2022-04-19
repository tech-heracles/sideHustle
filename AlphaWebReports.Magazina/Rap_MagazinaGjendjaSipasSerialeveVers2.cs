using System;
using System.Globalization;
using System.Resources;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_MagazinaGjendjaSipasSerialeveVers2 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_MagazinaGjendjaSipasSerialeveVers2(){InitializeComponent();} 

        public Rap_MagazinaGjendjaSipasSerialeveVers2(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.IdGjuha, report)
        {

        }
        public Rap_MagazinaGjendjaSipasSerialeveVers2(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, int idGjuha, DevExpress.XtraReports.UI.XtraReport raport)
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
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportGjendjaeMagazinesSipasSerialeveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell7.Text = rm.GetString("labelKartela", ci);
            xrTableCell8.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell9.Text = rm.GetString("labelRaportGrupi", ci);
            xrTableCell10.Text = rm.GetString("labelRaportMagazina", ci);
            xrTableCell11.Text = rm.GetString("labelNjesia", ci);
            xrTableCell13.Text = rm.GetString("labelRaportGjendjaeMeparshme", ci);
            xrTableCell14.Text = rm.GetString("labelRaportSasiHyrje", ci);
            xrTableCell15.Text = rm.GetString("labelRaportSasiaDalje", ci);
            TotaliGjithMAgazinave.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel31.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel34.Text = rm.GetString("filterMagazina", ci);
            xrLabel37.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell12.Text = rm.GetString("labelRaportSeriali", ci);
            xrTableCell16.Text = rm.GetString("labelRaportGjendje", ci);
        }

        
    }
}
