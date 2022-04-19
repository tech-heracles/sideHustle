using System;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_Artikujteshitur_sipasAgjenteve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Artikujteshitur_sipasAgjenteve(){InitializeComponent();} 
        int shifraPasPresjes = 0;
        public Rap_Artikujteshitur_sipasAgjenteve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen, param.IdViti, param.IdSubRaporti, report)
        { }
        public Rap_Artikujteshitur_sipasAgjenteve(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
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

            xrLabel1.Text = rm.GetString("lblRapTitArtShiturAgjenteve", ci);
          //  xrLabel13.Text = rm.GetString("RaportArtikujTeShiturTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
           // xrLabel16.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel12.Text = rm.GetString("labelLogoIMB", ci);

            //Report header
            xrLabel65.Text = rm.GetString("lblRapTitArtShiturAgjenteve", ci);
            KodiKoka.Text = KodiKokaR.Text = rm.GetString("labelKodi", ci);
            PershkrimiKoka.Text = PershkrimiKokaR.Text = rm.GetString("labelRaportiPershkrimi", ci);
            NjesiaKoka.Text = NjesiaKokaR.Text = rm.GetString("labelNjesia", ci);
            SasiaKoka.Text = SasiaKokaR.Text = rm.GetString("labelSasia", ci);
            CmimiKoka.Text = CmimiKokaR.Text = rm.GetString("labelCmimi", ci);
            ZbritjaAnalitikeKoka.Text = ZbritjaAnalitikeKokaR.Text = rm.GetString("labelZbritjeAnalitike", ci);
            VleftapaTVSHKoka.Text = VleftapaTVSHKokaR.Text = rm.GetString("labelVleftapaTVSH", ci);
            ZbritjaTotKoka.Text = ZbritjaTotKokaR.Text = rm.GetString("labelZbritjaTotale", ci);
            TVSHKoka.Text = TVSHKokaR.Text = rm.GetString("labelTVSH", ci);
            VleftameTVSHKoka.Text = VleftameTVSHKokaR.Text = rm.GetString("labelVleftaMe_Tvsh", ci);
        }

        
    }
    }

