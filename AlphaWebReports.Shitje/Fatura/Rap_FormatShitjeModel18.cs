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
    public partial class Rap_FormatShitjeModel18 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatShitjeModel18(){InitializeComponent();} 
        private ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
    
        public Rap_FormatShitjeModel18(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatShitjeModel18(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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

            xrLabel12.Text = rm.GetString("lblnrfatures", ci)+":";
            xrLabel6.Text = rm.GetString("labelRaportNumriFatures", ci);           
                  
            xrLabel15.Text = rm.GetString("labelRaportLlogariBankare", ci);
            xrTableCell4.Text = rm.GetString("lblRaportArtik", ci);
            xrTableCell5.Text = rm.GetString("lblDtSkadence", ci);
            xrTableCell7.Text = rm.GetString("labelNjesiaUpperCase", ci);
            xrTableCell9.Text = rm.GetString("label_SASIA", ci);
            xrTableCell10.Text = rm.GetString("label_CMIMI", ci);
            xrTableCell8.Text = rm.GetString("lblvlpatvsh", ci);
            xrTableCell11.Text = rm.GetString("lblRaportVlZbritur", ci);
            xrTableCell6.Text = rm.GetString("labelTVSH", ci);
            xrLabel20.Text = rm.GetString("labelAgjent", ci);
           
            xrLabel45.Text = rm.GetString("labelRaportEmerMbiemerFirma", ci);
            xrLabel46.Text = rm.GetString("labelShitesi", ci);
            xrLabel47.Text = rm.GetString("labelRaportEmerMbiemerFirma", ci);
            xrLabel15.Text = rm.GetString("labelRaportNrFiskal", ci);
            xrTableCell23.Text = rm.GetString("labelRaportiNrUpperCase",ci);
            xrLabel50.Text = rm.GetString("labelQyteti", ci) + ":";
            xrLabel59.Text = rm.GetString("labelRaportTel", ci)+ ":";
            xrLabel11.Text = rm.GetString("labelBlerjeShitjePershkrimi", ci);
            xrTableCell18.Text = rm.GetString("lblvlmetvsh", ci);
            xrTableCell21.Text = rm.GetString("labelPERSHKRIMI", ci);
            xrLabel43.Text = rm.GetString("labelRaportNRB", ci) + ":";
            xrLabel49.Text = rm.GetString("lableNRF", ci) + ":";
            xrLabel54.Text = rm.GetString("labelTVSH", ci) + ":";
            xrLabel62.Text = rm.GetString("labelRapBanke", ci) + ":";
            xrLabel1.Text = rm.GetString("labelRaportSubjektiShites", ci) + ":";
            xrLabel5.Text = rm.GetString("labelRaportAdresa", ci) + ":";
            xrLabel30.Text = rm.GetString("msgQyteti", ci) + ":";
            xrLabel4.Text = rm.GetString("labelAdministrimiTel", ci);
            xrLabel32.Text = rm.GetString("labelRaportEmail", ci) + ":";
            xrLabel61.Text = rm.GetString("RaportFatureShitjeTitulli", ci);
            xrLabel19.Text = rm.GetString("lbldtefatures", ci) + ":";
            xrLabel7.Text = rm.GetString("labelRaportSubjektiBleres", ci) + ":";
            xrLabel27.Text = rm.GetString("labelRaportAdresa", ci) + ":";
            xrLabel42.Text = rm.GetString("labelRaportNrFiskal", ci) + ":";
            xrTableCell25.Text = rm.GetString("labelAdministrimiTel", ci);
            xrLabel56.Text = rm.GetString("labelAgjenti", ci) + ":";
            xrLabel70.Text = rm.GetString("labelVleraPaTVSH", ci);
            xrLabel71.Text = rm.GetString("labelZbritje", ci);
            xrLabel74.Text = rm.GetString("lblvlzbritur", ci);
            xrLabel78.Text = rm.GetString("labelTVSH", ci);
            xrLabel67.Text = rm.GetString("lbltotalperpagese", ci);
        }
    
        }
    }
