using System;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_ArtikujteshiturKodiDoganor : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ArtikujteshiturKodiDoganor(){InitializeComponent();} 
        public Rap_ArtikujteshiturKodiDoganor(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, report)
        {

        }
        public Rap_ArtikujteshiturKodiDoganor(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
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
            var rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel13.Text = rm.GetString("RaportArtikujTeShiturTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel16.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel12.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel67.Text = rm.GetString("labelKodi", ci);
            xrLabel75.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel74.Text = rm.GetString("labelNjesia", ci);
            xrLabel73.Text = rm.GetString("labelSasia", ci);
            xrLabel72.Text = rm.GetString("labelCmimi", ci);
            xrLabel71.Text = rm.GetString("labelZbritjeAnalitike", ci);
            xrLabel70.Text = rm.GetString("labelVleftapaTVSH", ci);
            xrLabel66.Text = rm.GetString("labelZbritjaTotale", ci);
            xrLabel69.Text = rm.GetString("labelTVSH", ci);
            xrLabel76.Text = rm.GetString("labelVleftaMe_Tvsh", ci);
        }
    }
}
