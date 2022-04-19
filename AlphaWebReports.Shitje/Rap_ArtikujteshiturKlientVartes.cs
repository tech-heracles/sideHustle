using System;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_ArtikujteshiturKlientVartes : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ArtikujteshiturKlientVartes(){InitializeComponent();} 

        int shifraPasPresjes = 0;
        public Rap_ArtikujteshiturKlientVartes(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, report)
        {

        }
        public Rap_ArtikujteshiturKlientVartes(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter6.Value = raport.Parameters[5].Value;
            parameter7.Value = raport.Parameters[6].Value;
            parameter8.Value = raport.Parameters[7].Value;
            KlasaArtikulli.Value = raport.Parameters[10].Value;
            parameter9.Value = raport.Parameters[11].Value;
            parameter10.Value = raport.Parameters[12].Value;
            parameter11.Value = raport.Parameters[13].Value;
            parameter12.Value = raport.Parameters[15].Value;
            parameter13.Value = raport.Parameters[16].Value;
            parameter14.Value = raport.Parameters[17].Value;
            DegaAdministrative.Value = raport.Parameters[14].Value;
            parameter15.Value = raport.Parameters[18].Value;
            parameter16.Value = raport.Parameters[19].Value;
            parameter17.Value = raport.Parameters[20].Value;
            parameter18.Value = raport.Parameters[21].Value;
            adresaFaturimit.Value = raport.Parameters[27].Value;

            shifraPasPresjes = Convert.ToInt32(raport.Parameters["filterFormatNumri"].Value);
            caktoFormatinENumrave();
        }
        private void caktoFormatinENumrave()
        {
            
            xrLabel85.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel78.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel84.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel83.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel81.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel79.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel89.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel94.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel90.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel91.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel92.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel93.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel7.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel8.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel31.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel6.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            lbltotalipatvshmezbr.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel11.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel10.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel5.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel4.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel9.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel14.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel63.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            lblsasia.Summary.FormatString = "{0:N" + shifraPasPresjes + "}";
            
        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            var rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));


            xrLabel13.Text = rm.GetString("RaportArtikujTeShiturKlientVartesTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel16.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel12.Text = rm.GetString("labelLogoIMB", ci);

            //Report header
            rm.GetString("RaportArtikujTeShiturKlientVartesTitulli", ci);
            KodiKokaR.Text = rm.GetString("labelKodi", ci);
            PershkrimiKokaR.Text = rm.GetString("labelRaportiPershkrimi", ci);
            NjesiaKokaR.Text = rm.GetString("labelNjesia", ci);
            SasiaKokaR.Text = rm.GetString("labelSasia", ci);
            CmimiKokaR.Text = rm.GetString("labelCmimi", ci);
            ZbritjaAnltkKokaR.Text = rm.GetString("labelZbritjeAnalitike", ci);
            VlpaTVSHKokaR.Text = rm.GetString("labelVleftapaTVSH", ci);
            ZbritjaTotKokaR.Text = rm.GetString("labelZbritjaTotale", ci);
            TVSHKokaR.Text = rm.GetString("labelTVSH", ci);
            VlmeTVSHKokaR.Text = rm.GetString("labelVleftaMe_Tvsh", ci);
        }
        
    }
}
