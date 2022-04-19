using System;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs
{
    public partial class Rap_FatureShitjeMeArtikujKosove : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureShitjeMeArtikujKosove(){InitializeComponent();} 

        public Rap_FatureShitjeMeArtikujKosove(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {
        }
        public Rap_FatureShitjeMeArtikujKosove(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel48.Text = rm.GetString("labelRaportNRB", ci) + ":";
            xrLabel9.Text = rm.GetString("labelInfoMbiLikujdimFature", ci) + ":";
            xrLabel50.Text = rm.GetString("labelTVSH", ci) + ":";
            xrLabel51.Text = rm.GetString("labelRaportNrFiskal", ci) + ":";
            xrLabel14.Text = rm.GetString("labelRaportStatusiFatures", ci) + ":";
            xrLabel20.Text = rm.GetString("labelRaportVleraMbetur", ci) + ":";
            xrLabel55.Text = rm.GetString("labelRaportFaturaDergohetNe", ci) + ":";
            xrLabel56.Text = rm.GetString("labelRaportMalliDorezohetNe", ci) + ":";
            xrLabel74.Text = rm.GetString("labelBleresi", ci) + ":";
            xrLabel73.Text = rm.GetString("labelRaportAdresa", ci) + ":";
            xrLabel72.Text = rm.GetString("labelRaportShteti", ci) + ":";
            xrLabel71.Text = rm.GetString("labelRaportNRB", ci) + ":";
            xrLabel70.Text = rm.GetString("labelRaportTel", ci) + ":";
            xrLabel69.Text = rm.GetString("labelRaportNrFiskal", ci) + ":";
            xrLabel68.Text = rm.GetString("labelRaportKontakti", ci) + ":";
            xrLabel67.Text = rm.GetString("labelRaportNrTVSH", ci) + ":";
            xrLabel93.Text = rm.GetString("labelBleresi", ci) + ":";
            xrLabel92.Text = rm.GetString("labelRaportAdresa", ci) + ":";
            xrLabel91.Text = rm.GetString("labelRaportShteti", ci) + ":";
            xrLabel89.Text = rm.GetString("labelRaportTel", ci) + ":";
            xrLabel85.Text = rm.GetString("labelRaportMenyraLiferimit", ci) + ":";
            xrLabel87.Text = rm.GetString("labelRaportKontakti", ci) + ":";
            xrLabel86.Text = rm.GetString("labelRaportFaturaNr", ci) + ":";
            xrLabel100.Text = rm.GetString("labelRaportDateFature", ci);
            xrLabel98.Text = rm.GetString("labelRaportKushtet", ci);
            xrLabel97.Text = rm.GetString("labelRaportDataSkadimit", ci);
            xrLabel96.Text = rm.GetString("labelRaportReferenca", ci);
            xrLabel90.Text = rm.GetString("labelRaportReferentiJuaj", ci);
            xrLabel99.Text = rm.GetString("labelRaportNrPorosi", ci);
            xrLabel95.Text = rm.GetString("labelRaportDataEPorosise", ci);
            xrLabel101.Text = rm.GetString("labelNjesiOrganizative", ci);
            xrLabel19.Text = rm.GetString("labelRaportFaqja", ci) + ":";
            xrTableCell4.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell17.Text = rm.GetString("labelRaportShifra", ci);
            xrTableCell5.Text = rm.GetString("labelRaportPershkrimiProduktit", ci);
            xrTableCell7.Text = rm.GetString("labelSasia", ci);
            xrTableCell9.Text = rm.GetString("labelNjesia", ci);
            xrTableCell10.Text = rm.GetString("labelRaportCmimiPaTVSH", ci);
            xrTableCell11.Text = rm.GetString("labelRaportCmimiMeTVSH", ci);
            xrTableCell19.Text = rm.GetString("labelZbritje", ci) + "%";
            xrTableCell8.Text = rm.GetString("labelTVSH", ci);
            xrTableCell6.Text = rm.GetString("labelVleftameTVSH", ci);
            xrLabel49.Text = rm.GetString("labelRaportiShenime", ci);
            xrLabel10.Text = rm.GetString("labelVleraPaTVSH", ci) + ":";
            xrLabel2.Text = rm.GetString("labelZbritje", ci) + ":";
            xrLabel4.Text = rm.GetString("labelRaportNgarkesaTeTjera", ci) + ":";
            xrLabel5.Text = rm.GetString("labelTVSH", ci) + ":";
            xrLabel6.Text = rm.GetString("labelRaportShumaFatures", ci) + ":";
            xrLabel1.Text = rm.GetString("labelRaportPagesatNderlidhura", ci) + ":";
            xrLabel7.Text = rm.GetString("labelRaportPagesatTanishme", ci) + ":";
            xrLabel8.Text = rm.GetString("labelRaportMbeturPerPagese", ci) + ":";
            xrLabel44.Text = rm.GetString("labelRaportFaturoi", ci);
            xrLabel45.Text = rm.GetString("labelPranoi", ci);
            xrLabel46.Text = rm.GetString("labelRaportDergoi", ci);
        }
    }
}
