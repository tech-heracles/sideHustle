using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureShitjePromotions : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureShitjePromotions(){InitializeComponent();} 
   

        public Rap_FatureShitjePromotions(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitjePromotions(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel1.Text = rm.GetString("labelFature", ci);
            xrTableCell56.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell59.Text = rm.GetString("filterMonedha", ci);
            xrTableCell43.Text = rm.GetString("labelNIPT", ci);
            xrTableCell41.Text = rm.GetString("labelRaportKlienti",ci);
            xrTableCell45.Text = rm.GetString("labelRefKlientit",ci);
            xrLabel63.Text = rm.GetString("labelNIPT", ci) + ":";
            xrLabel59.Text = rm.GetString("labelAdministrimiKontakt", ci) + ":";      
            xrLabel57.Text = rm.GetString("labelRaportAdresa", ci) + ":";
            xrTableCell47.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel5.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel6.Text = rm.GetString("labelRaportTel", ci);
            xrTableCell51.Text = rm.GetString("labelRaportTel", ci);
            xrTableCell53.Text = rm.GetString("labelRaportEmail", ci);
            xrLabel7.Text = rm.GetString("labelRaportEmail", ci);
            xrTableCell49.Text = rm.GetString("labelRaportKontakti", ci);
            xrTableCell9.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell4.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell5.Text = rm.GetString("labelKodi", ci);
            xrTableCell7.Text = rm.GetString("labelNjesia", ci);
            xrTableCell9.Text = rm.GetString("labelSasia", ci);
            xrTableCell10.Text = rm.GetString("labelCmimi", ci);
            xrTableCell8.Text = rm.GetString("labelRaportVlera", ci);
            xrTableCell11.Text = rm.GetString("labelTVSH", ci);
            xrTableCell6.Text = rm.GetString("labelVleraMeTVSH", ci);
            xrTableCell19.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell17.Text = rm.GetString("labelRaportiShenime", ci);
            xrLabel10.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel18.Text = rm.GetString("labelKursi", ci);
            xrLabel23.Text = rm.GetString("lblRaportVleraNe", ci);
            xrLabel15.Text = rm.GetString("labelBleresi", ci);
            xrLabel36.Text = rm.GetString("lblShitesKrijues", ci);
            xrLabel19.Text = rm.GetString("labelEmerMbiemerFirma", ci);
            xrLabel37.Text = rm.GetString("labelEmerMbiemerFirma", ci);
            xrLabel4.Text = rm.GetString("labelAfatiPageses", ci);
            //xrLabel40.Text = rm.GetString("labelRaportTel", ci);
         
        }

       

       
    }
}
