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
    public partial class Rap_OferteShitjeKlimateknika : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_OferteShitjeKlimateknika(){InitializeComponent();} 
        public Rap_OferteShitjeKlimateknika(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_OferteShitjeKlimateknika(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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

            xrTableCell17.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell4.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell5.Text = rm.GetString("labelNjesia", ci);
            xrTableCell7.Text = rm.GetString("labelSasia", ci);
            xrTableCell8.Text = rm.GetString("labelCmimi", ci);
            xrTableCell6.Text = rm.GetString("labelRaportVlera", ci);
            xrLabel24.Text = rm.GetString("labelRaportiTotaliMeTvsh", ci);
            xrLabel4.Text = rm.GetString("lbltotskonto", ci);
            xrLabel14.Text=rm.GetString("lblPorositesi",ci);
            xrLabel8.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel13.Text = rm.GetString("lblSipermarresi", ci);
            xrLabel20.Text = rm.GetString("lblNrOferte", ci);
            xrLabel9.Text = rm.GetString("lblVlefshmeriaKlimateknika", ci);
            xrLabel10.Text = rm.GetString("labelGarancia", ci);
            xrLabel12.Text = rm.GetString("lbl7Vjecare", ci);
            xrLabel15.Text = rm.GetString("lblKlimateknika", ci);
            xrLabel22.Text = rm.GetString("lblShenimKlimateknika", ci);
            xrLabel28.Text = rm.GetString("lblpjesa2ShenimeKlimateknika", ci);
            xrLabel18.Text = rm.GetString("lblPorositesi", ci);
            xrLabel23.Text = rm.GetString("lblKlimateknikashpk", ci);
            xrLabel12.Text = rm.GetString("msgSkedarMbi10mb", ci);


          }

    }
}
