using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureShitjeMeArtikuj_DeutschColor : DevExpress.XtraReports.UI.XtraReport
    { 
		public Rap_FatureShitjeMeArtikuj_DeutschColor(){InitializeComponent();} 
   

        public Rap_FatureShitjeMeArtikuj_DeutschColor(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitjeMeArtikuj_DeutschColor(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel32.Text = rm.GetString("lblFormat001");
            xrTableCell8.Text = rm.GetString("lblZbritjePerqindje", ci);
            xrTableCell6.Text = rm.GetString("labelVleraUpperCase", ci);
            //xrLabel9.Text = rm.GetString("lblDurres2015", ci);
            //xrLabel9.Text = rm.GetString("lblData", ci);
            //xrLabel9.Text = rm.GetString("labelInfoMbiLikujdimFature", ci) + ":";
            //xrLabel14.Text = rm.GetString("labelRaportStatusiFatures", ci) + ":";
            //xrLabel20.Text = rm.GetString("labelRaportVleraMbetur", ci) + ":";
            //xrLabel55.Text = rm.GetString("labelRaportFaturaDergohetNe", ci) + ":";
            //xrLabel74.Text = rm.GetString("labelBleresi", ci) + ":";
            //xrLabel73.Text = rm.GetString("labelRaportAdresa", ci) + ":";
            //xrLabel72.Text = rm.GetString("labelRaportShteti", ci) + ":";
            //xrLabel71.Text = rm.GetString("labelNIPT", ci) + ":";
            //xrLabel70.Text = rm.GetString("labelRaportTel", ci) + ":";
            //xrLabel69.Text = rm.GetString("labelRaportNrFiskal", ci) + ":";
            //xrLabel68.Text = rm.GetString("labelRaportKontakti", ci) + ":";
            //xrLabel67.Text = rm.GetString("labelRaportNrTVSH", ci) + ":";          
            //xrLabel86.Text = rm.GetString("labelRaportFaturaNr", ci) ;          
            // xrLabel19.Text = rm.GetString("labelRaportFaqja", ci) + ":";
            // xrTableCell4.Text = rm.GetString("labelRaportiNr", ci);
            // xrTableCell17.Text = rm.GetString("labelRaportShifra", ci);
            // xrTableCell5.Text = rm.GetString("labelRaportPershkrimiProduktit", ci);
            // xrTableCell7.Text = rm.GetString("labelSasia", ci);
            // xrTableCell9.Text = rm.GetString("labelNjesia", ci);
            // xrTableCell10.Text = rm.GetString("labelCmimi", ci);
            // xrTableCell6.Text = rm.GetString("labelRaportVlefta", ci);
            // xrLabel49.Text = rm.GetString("labelRaportiShenime", ci);
            // xrLabel10.Text = rm.GetString("labelVleraPaTVSH", ci) + ":";
            // xrLabel2.Text = rm.GetString("lblRaportTotaliFatures", ci) + ":";
            // xrLabel4.Text = rm.GetString("labelKursi", ci) + ":";
            // xrLabel6.Text = rm.GetString("labelRaportTotaliMeZbritje", ci) + ":";
            // xrLabel1.Text = rm.GetString("labelRaportTotaliZbritjeMB", ci);
            // xrLabel44.Text = rm.GetString("labelBleresi", ci);
            // xrLabel45.Text = rm.GetString("lblShitesKrijues", ci);
            // xrLabel46.Text = rm.GetString("labelTransportuesi", ci);
            // xrLabel5.Text = rm.GetString("lblPreventivShtije", ci);
        }

    }
}
