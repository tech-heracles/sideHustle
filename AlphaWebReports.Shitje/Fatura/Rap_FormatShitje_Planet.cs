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
    public partial class Rap_FormatShitje_Planet : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatShitje_Planet(){InitializeComponent();} 

        public Rap_FormatShitje_Planet(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatShitje_Planet(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel1.Text = rm.GetString("RaportTitullKuotime", ci);
            xrLabel60.Text = rm.GetString("labelNIPT", ci);
            xrLabel44.Text = rm.GetString("labelRaportNumriFatures", ci);
            xrLabel7.Text = rm.GetString("lblRaportDataFatures", ci);
            xrLabel61.Text = rm.GetString("labelRaportTel", ci);
            xrLabel12.Text = rm.GetString("labelRaportWeb", ci);
            xrLabel4.Text = rm.GetString("labelRaportSubjektBleres", ci);
            xrLabel2.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel16.Text = rm.GetString("labelNIPT", ci);
            xrLabel18.Text = rm.GetString("labelRaportTel", ci);
            xrTableCell4.Text = rm.GetString("labelRaportNRKARTELE", ci);
            xrTableCell5.Text = rm.GetString("labelPERSHKRIMI", ci);
            xrTableCell7.Text = rm.GetString("labelNjesiaUpperCase", ci);
            xrTableCell9.Text = rm.GetString("label_SASIA", ci);
            xrTableCell10.Text = rm.GetString("label_CMIMI", ci);
            xrTableCell8.Text = rm.GetString("labelVlera_Pa_Tvsh",ci);
            //xrLabel6.Text = rm.GetString("labelFaturePlanet", ci);
            xrTableCell3.Text = rm.GetString("labelTVSH", ci);
            xrTableCell6.Text = rm.GetString("labelVLERA_ME_TVSH", ci);
            xrLabel13.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel20.Text = rm.GetString("lblRaportTotaliNe", ci);
            xrLabel22.Text = rm.GetString("labelKursi", ci);
            xrLabel27.Text = rm.GetString("labelBleresi", ci);
            xrLabel36.Text = rm.GetString("labelShitesi", ci);
            xrLabel31.Text = rm.GetString("labelEmerMbiemerFirma", ci);
            xrLabel37.Text = rm.GetString("labelEmerMbiemerFirma", ci);

            xrLabel8.Text = rm.GetString("labelFatureEmail", ci);
            //xrLabel10.Text = rm.GetString("labelFaturePlanetEmail", ci);
            //xrLabel11.Text = rm.GetString("labelRaportPlanetWeb", ci);
            xrLabel14.Text = rm.GetString("labelRaportFax", ci);
        }
                
    }
}
