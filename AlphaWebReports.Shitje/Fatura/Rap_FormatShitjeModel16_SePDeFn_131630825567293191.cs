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
    public partial class Rap_FormatShitjeModel16_SePDeFn_131630825567293191 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatShitjeModel16_SePDeFn_131630825567293191(){InitializeComponent();} 
        private ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

        public Rap_FormatShitjeModel16_SePDeFn_131630825567293191(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatShitjeModel16_SePDeFn_131630825567293191(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel4.Text = rm.GetString("labelRaportSubjektiBleres", ci);           
            xrLabel12.Text = rm.GetString("labelRaportAdresa", ci);
        
            xrTableCell5.Text = rm.GetString("labelPERSHKRIMI", ci);
            xrTableCell7.Text = rm.GetString("labelNjesiaUpperCase", ci);
            xrTableCell9.Text = rm.GetString("label_SASIA", ci);
            xrTableCell10.Text = rm.GetString("label_CMIMI", ci);
          
            xrTableCell11.Text = rm.GetString("lblRaportVlZbritur", ci);
            xrTableCell6.Text = rm.GetString("labelTVSH", ci);
          
            xrLabel26.Text = rm.GetString("labelVleraPaTVSH", ci);
            xrLabel45.Text = rm.GetString("labelRaportEmerMbiemerFirma", ci);
       
            xrLabel47.Text = rm.GetString("labelRaportEmerMbiemerFirma", ci);
         
            xrTableCell23.Text = rm.GetString("labelRaportiNrUpperCase",ci);
            xrLabel50.Text = rm.GetString("labelQyteti", ci);
            xrLabel59.Text = rm.GetString("labelRaportTel", ci);
           
            xrTableCell18.Text = rm.GetString("labelVlMeTvsh", ci);
            xrTableCell17.Text = rm.GetString("labelZbritjePerqindje", ci);
        }

    }
}
