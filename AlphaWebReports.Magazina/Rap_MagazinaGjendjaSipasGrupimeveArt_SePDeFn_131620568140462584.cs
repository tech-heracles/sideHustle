using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Collections.Generic;
namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_MagazinaGjendjaSipasGrupimeveArt_SePDeFn_131620568140462584 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_MagazinaGjendjaSipasGrupimeveArt_SePDeFn_131620568140462584(){InitializeComponent();}
        int shifraPasPresjes = 0;
        public Rap_MagazinaGjendjaSipasGrupimeveArt_SePDeFn_131620568140462584(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, report)
        {
        }
        public Rap_MagazinaGjendjaSipasGrupimeveArt_SePDeFn_131620568140462584(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            shifraPasPresjes = Convert.ToInt32(raport.Parameters["filterFormatNumri"].Value);

        }
        

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportGjendjaeMagazinesGrupuarSipasArtikujveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel21.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel20.Text = rm.GetString("labelNjesia", ci);
            xrLabel19.Text = rm.GetString("labelRaportLLogariInventar", ci);
            xrLabel18.Text = rm.GetString("labelRaportSasiHyrje", ci);
            xrLabel17.Text = rm.GetString("labelRaportSasiaDalje", ci);
            xrLabel14.Text = rm.GetString("labelVlefta", ci);
            xrLabel16.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
            xrLabel15.Text = rm.GetString("labelRaportKosto", ci);
            xrLabel31.Text = rm.GetString("labelRaportiTotali", ci);
            //TotaliGjithMAgazinave.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel34.Text = rm.GetString("filterMagazina", ci);
            xrLabel37.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel155.Text = rm.GetString("labelRaportGjendjaeMeparshme", ci);
        }
    }
}
