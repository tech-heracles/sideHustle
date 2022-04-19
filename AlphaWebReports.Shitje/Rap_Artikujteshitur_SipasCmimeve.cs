using System;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    
    public partial class Rap_Artikujteshitur_SipasCmimeve : XtraReport
    {
		public Rap_Artikujteshitur_SipasCmimeve(){InitializeComponent();} 
       
		public Rap_Artikujteshitur_SipasCmimeve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, report) { }
        
        public Rap_Artikujteshitur_SipasCmimeve(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport) {
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


            titulliLabel.Text = rm.GetString("RaportArtikujTeShiturSipasCmimitTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);

            labelKodi.Text = rm.GetString("labelKodi", ci);
            labelKodbari.Text = "Kodbari";
            labelRaportiPershkrimi.Text = rm.GetString("labelRaportiPershkrimi", ci);
            labelNjesia.Text = rm.GetString("labelNjesia", ci);
            labelSasia.Text = rm.GetString("labelSasia", ci);
            labelCmimi.Text = rm.GetString("labelCmimi", ci);
            labelZbritjeAnalitike.Text = rm.GetString("labelZbritjeAnalitike", ci);
            labelVleftapaTVSH.Text = rm.GetString("labelVleftapaTVSH", ci);
            labelZbritjaTotale.Text = rm.GetString("labelZbritjaTotale", ci);
            labelTVSH.Text = rm.GetString("labelTVSH", ci);
            labelVleftaMe_Tvsh.Text = rm.GetString("labelVleftaMe_Tvsh", ci);

            labelRaportiTotali.Text = rm.GetString("labelRaportiTotali", ci);
            labelLogoIMB.Text = rm.GetString("labelLogoIMB", ci);
            
            //Report header
            //titulliLabelReportHeader.Text = rm.GetString("RaportArtikujTeShiturTitulli", ci);
            //xrLabel67.Text = rm.GetString("labelKodi", ci);
            //xrLabel75.Text = rm.GetString("labelRaportiPershkrimi", ci);
            //xrLabel74.Text = rm.GetString("labelNjesia", ci);
            //xrLabel73.Text = rm.GetString("labelSasia", ci);
            //xrLabel72.Text = rm.GetString("labelCmimi", ci);
            //xrLabel71.Text = rm.GetString("labelZbritjeAnalitike", ci);
            //xrLabel70.Text = rm.GetString("labelVleftapaTVSH", ci);
            //xrLabel66.Text = rm.GetString("labelZbritjaTotale", ci);
            //xrLabel69.Text = rm.GetString("labelTVSH", ci);
            //xrLabel76.Text = rm.GetString("labelVleftaMe_Tvsh", ci);
        }
    }
}
