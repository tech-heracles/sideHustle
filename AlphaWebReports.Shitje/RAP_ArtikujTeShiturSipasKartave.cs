using System;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class RAP_ArtikujTeShiturSipasKartave : DevExpress.XtraReports.UI.XtraReport
    {
		public RAP_ArtikujTeShiturSipasKartave(){InitializeComponent();} 
     
        public RAP_ArtikujTeShiturSipasKartave(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen, param.IdViti, param.IdSubRaporti, report)
        { }
        public RAP_ArtikujTeShiturSipasKartave(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
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

            xrLabel1.Text = rm.GetString("lblRapTitArtShiturKarta", ci);      
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);        
            xrLabel12.Text = rm.GetString("labelLogoIMB", ci);

            //Report header           
            KodiKokaR.Text = KodiKokaR.Text = rm.GetString("labelKodi", ci);
            PershkrimiKokaR.Text = PershkrimiKokaR.Text = rm.GetString("labelRaportiPershkrimi", ci);
            NjesiaKokaR.Text = NjesiaKokaR.Text = rm.GetString("labelNjesia", ci);
            SasiaKokaR.Text = SasiaKokaR.Text = rm.GetString("labelSasia", ci);
            CmimiKokaR.Text = CmimiKokaR.Text = rm.GetString("labelCmimi", ci);
            ZbritjaAnalitikeKokaR.Text = ZbritjaAnalitikeKokaR.Text = rm.GetString("labelZbritjeAnalitike", ci);
            VleftapaTVSHKokaR.Text = VleftapaTVSHKokaR.Text = rm.GetString("labelVleftapaTVSH", ci);
            ZbritjaTotKokaR.Text = ZbritjaTotKokaR.Text = rm.GetString("labelZbritjaTotale", ci);
            TVSHKokaR.Text = TVSHKokaR.Text = rm.GetString("labelTVSH", ci);
            VleftameTVSHKokaR.Text = VleftameTVSHKokaR.Text = rm.GetString("labelVleftaMe_Tvsh", ci);
        }
    }
    }

