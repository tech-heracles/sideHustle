using System;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureShitjeMeArtikujUniversReklama : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureShitjeMeArtikujUniversReklama(){InitializeComponent();} 

        public Rap_FatureShitjeMeArtikujUniversReklama(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitjeMeArtikujUniversReklama(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel8.Text = rm.GetString("labelNIPT", ci) + ":";
            xrLabel25.Text = rm.GetString("labelRaportTel", ci) + ":";
            xrLabel24.Text = rm.GetString("labelMailRaporti", ci) + ":";
            xrLabel56.Text = rm.GetString("labelNIPT", ci) + ":";
            xrLabel48.Text = rm.GetString("labelAdreseFaturimi", ci);
            xrLabel32.Text = rm.GetString("labelRaportTel", ci) + ":";
            xrLabel47.Text = rm.GetString("labelAdreseDergimi", ci) + ":";
            xrLabel34.Text = rm.GetString("labelRaportKontakti", ci) + ":";
            xrLabel6.Text = rm.GetString("labelNumerFature", ci);
            xrLabel14.Text = rm.GetString("labelRaportReferenca", ci);
            xrLabel7.Text = rm.GetString("labelRaportDateFature", ci);
            xrLabel41.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrLabel12.Text = rm.GetString("labelAgjenti", ci);
            xrLabel13.Text = rm.GetString("labelKushtePagese", ci);
            xrTableCell4.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell13.Text = rm.GetString("labelRaportiShenime", ci);
            xrTableCell5.Text = rm.GetString("labelRaportiTotal", ci);
            xrTableCell9.Text = rm.GetString("labelCmimi", ci);
            xrTableCell8.Text = rm.GetString("labelShitjeZbritje", ci) + "(%)";
            xrTableCell6.Text = rm.GetString("labelVleraPaTVSH", ci);
            xrTableCell3.Text = rm.GetString("labelTVSH", ci);
            xrTableCell7.Text = rm.GetString("labelVleraMeTVSH", ci);
            xrLabel20.Text = rm.GetString("labelVleftapaTVSH", ci) + ":";
            xrLabel21.Text = rm.GetString("labelBlerjeShitjeZbritje", ci) + ":";
            xrLabel15.Text = rm.GetString("labelTVSH", ci) + ":";
            xrLabel17.Text = rm.GetString("labelRaportiTotali", ci) + ":";
            xrLabel40.Text = rm.GetString("labelBleresi", ci);
            xrLabel37.Text = rm.GetString("labelTransportuesi", ci);
            xrLabel38.Text = rm.GetString("lblShitesKrijues", ci);
            xrTableCell17.Text = rm.GetString("labelKodi", ci);
            xrTableCell18.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell19.Text = rm.GetString("labelGarancia", ci);
            xrLabel2.Text = rm.GetString("labelKushtetEGarancise", ci);
            xrLabel31.Text = rm.GetString("LabelKushtGaranicePerProdProdhimi", ci);
            xrLabel35.Text = rm.GetString("LabelLuhatjeTensioni", ci);
            xrLabel36.Text = rm.GetString("labelRiparimiOseZevendesimiProduktit", ci); 
            xrLabel50.Text = rm.GetString("labelRaportAfatiKohor", ci) + ":";
        }
    }
}
