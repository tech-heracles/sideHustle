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
    public partial class Rap_FormatShitjeModel15 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatShitjeModel15(){InitializeComponent();} 
        private ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

        public Rap_FormatShitjeModel15(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatShitjeModel15(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel4.Text = rm.GetString("labelRaportSubjektiBleres", ci) + ":";
            xrLabel2.Text = rm.GetString("labelRaportSubjektiShites", ci) + ":";
            xrLabel12.Text = rm.GetString("labelRaportAdresa", ci) + ":";
            xrLabel6.Text = rm.GetString("lblnrfatures", ci) + ":";
            xrLabel13.Text = rm.GetString("labelRaportAdresa", ci) + ":";
            xrLabel7.Text = rm.GetString("lbldtefatures", ci) + ":";
            xrLabel14.Text = rm.GetString("labelRaportTelFax", ci) + ":";
            xrTableCell4.Text = rm.GetString("lblRaportArtik", ci);
            xrTableCell5.Text = rm.GetString("labelPERSHKRIMI", ci);
            xrTableCell19.Text = rm.GetString("lblvlmetvsh", ci);
            xrTableCell7.Text = rm.GetString("labelNjesiaUpperCase", ci);
            xrTableCell9.Text = rm.GetString("label_SASIA", ci);
            xrTableCell10.Text = rm.GetString("label_CMIMI", ci);
            //xrTableCell17.Text = rm.GetString("labelRaportLOTNR", ci);
            xrTableCell8.Text = rm.GetString("labelRaportZb", ci) + ". %";
            xrTableCell11.Text = rm.GetString("lblRaportVlZbritur", ci);
            xrTableCell6.Text = rm.GetString("labelTVSH", ci);
            xrLabel16.Text = rm.GetString("labelQyteti", ci) + ":";
            xrLabel20.Text = rm.GetString("labelAgjenti", ci) + ":";
            xrLabel26.Text = rm.GetString("labelVleraPaTVSH", ci);
            //xrLabel44.Text = rm.GetString("labelBleresi", ci);
            xrLabel45.Text = rm.GetString("labelRaportEmerMbiemerFirma", ci);
            xrLabel46.Text = rm.GetString("labelShitesi", ci);
            xrLabel47.Text = rm.GetString("labelRaportEmerMbiemerFirma", ci);
            xrLabel48.Text = rm.GetString("labelRaportEmail", ci) + ":";
           // xrLabel57.Text = rm.GetString("labelRaportNrFiskal", ci);
            xrLabel15.Text = rm.GetString("labelRaportNrFiskal", ci) + ":";
            xrTableCell23.Text = rm.GetString("labelRaportiNrUpperCase",ci);
            xrTableCell21.Text = rm.GetString("lblvlpatvsh",ci);
            xrLabel50.Text = rm.GetString("labelQyteti", ci) + ":";
            xrLabel14.Text = rm.GetString("labelRaportTel", ci) + ":";
            xrTableCell20.Text = rm.GetString("labelRaportTel", ci) + ":";
            xrLabel11.Text = rm.GetString("labelBlerjeShitjePershkrimi", ci);
            xrLabel36.Text = rm.GetString("labelZbritje", ci);
            xrLabel49.Text = rm.GetString("lblvlzbritur", ci);
            xrLabel55.Text = rm.GetString("labelTVSH", ci);
            xrLabel25.Text = rm.GetString("lbltotalperpagese", ci);
        }

    }
}
