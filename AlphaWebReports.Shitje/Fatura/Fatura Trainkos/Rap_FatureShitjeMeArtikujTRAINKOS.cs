using DevExpress.XtraReports.UI;
using System;
using System.Globalization;
using System.Resources;
namespace AlphaWebReports.RaportetDs
{
    public partial class Rap_FatureShitjeMeArtikujTRAINKOS : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureShitjeMeArtikujTRAINKOS(){InitializeComponent();} 
    
        public Rap_FatureShitjeMeArtikujTRAINKOS(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitjeMeArtikujTRAINKOS(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel1.Text = rm.GetString("RaportFatureShitjeTitulli", ci);
            xrLabel6.Text = rm.GetString("labelRaportNumriFatures", ci);
            xrLabel7.Text = rm.GetString("labelRaportDataFatures", ci);
            xrLabel50.Text = rm.GetString("labelRaportFaturuarUpperCase", ci) + ":";
            xrLabel59.Text = rm.GetString("labelRaportLiferuarUpperCase", ci) + ":";
            xrLabel51.Text = rm.GetString("labelBleresi", ci) + ":";
            xrLabel52.Text = rm.GetString("labelRaportAdresa", ci) + ":";
            xrLabel54.Text = rm.GetString("labelRaportNRB", ci) + ":";
            xrLabel53.Text = rm.GetString("labelRaportTel", ci) + ":";
            xrLabel4.Text = rm.GetString("labelRaportKlientNr", ci) + ":";
            xrLabel12.Text = rm.GetString("labelRaportKlientNr", ci) + ":";
            xrLabel61.Text = rm.GetString("labelRaportEmri", ci) + ":";
            xrLabel62.Text = rm.GetString("labelRaportAdresa", ci) + ":";
            xrLabel63.Text = rm.GetString("labelRaportNRB", ci) + ":";
            xrTableCell17.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell4.Text = rm.GetString("labelRaportNrArt", ci);
            xrTableCell5.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell7.Text = rm.GetString("labelSasia", ci);
            xrTableCell9.Text = rm.GetString("labelNjesia", ci);
            xrTableCell10.Text = rm.GetString("labelCmimi", ci);
            xrTableCell8.Text = rm.GetString("labelVleftapaTVSH", ci);
            xrTableCell19.Text = rm.GetString("labelRaportZb", ci) + "%";
            xrTableCell11.Text = rm.GetString("labelTVSH", ci);
            xrTableCell6.Text = rm.GetString("labelVleftameTVSH", ci);
            xrLabel2.Text = rm.GetString("labelRaportiShenime", ci);
            xrLabel21.Text = rm.GetString("labelVleraPaTVSH", ci) + ":";
            xrLabel23.Text = rm.GetString("labelZbritje", ci) + ":";
            xrLabel44.Text = rm.GetString("labelRaportVleraMeZbritje", ci) + ":";
            xrLabel45.Text = rm.GetString("labelTVSH", ci) + ":";
            xrLabel13.Text = rm.GetString("labelRaportShenimeTeTjera", ci) + ":";
            xrLabel20.Text = rm.GetString("labelRaportTotaliPerPagese", ci) + ":";
            xrLabel15.Text = rm.GetString("labelRaportKlienti", ci) + ":";
            xrLabel18.Text = rm.GetString("labelRaportMenyrePagese", ci) + ":";
            xrLabel67.Text = rm.GetString("labelRaportPerpunoiFaturoi", ci);
            xrLabel68.Text = rm.GetString("labelRaportKontrolloi", ci);
            xrLabel69.Text = rm.GetString("labelDorezoi", ci);
            xrLabel70.Text = rm.GetString("labelRaportPranoiBleresi", ci);
        }
    }
}
