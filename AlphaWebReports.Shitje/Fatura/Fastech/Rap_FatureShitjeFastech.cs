using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura.Fastech
{
    public partial class Rap_FatureShitjeFastech : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureShitjeFastech(){InitializeComponent();} 
     
   
        public Rap_FatureShitjeFastech(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitjeFastech(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel72.Text = rm.GetString("labelRaportTelFax", ci) + ":";
            xrLabel79.Text = rm.GetString("labelNIPT", ci) + ":";
            xrLabel6.Text = rm.GetString("labelRaportNumriFatures", ci) + ":";
            xrLabel8.Text = rm.GetString("labelNumriSerise", ci) + ":";
            xrLabel7.Text = rm.GetString("labelRaportDateFature", ci);
            xrLabel41.Text = rm.GetString("labelNumriPorosise", ci) + ":";
            xrLabel2.Text = rm.GetString("labelBleresiUpperCase", ci) + ":";
            xrLabel23.Text = rm.GetString("labelMarresiUpperCase", ci) + ":";
            xrLabel4.Text = rm.GetString("labelTransportuesiUpperCase", ci) + ":";
            xrLabel75.Text = rm.GetString("labelRaportTelFax", ci) + ":";
            xrLabel74.Text = rm.GetString("labelRaportTelFax", ci) + ":";
            xrLabel88.Text = rm.GetString("labelRaportTelFax", ci) + ":";
            xrLabel12.Text = rm.GetString("labelNIPT", ci);
            xrLabel44.Text = rm.GetString("labelNIPT", ci);
            xrLabel51.Text = rm.GetString("labelNIPT", ci);

            xrTableCell17.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell4.Text = rm.GetString("labelKartela", ci);
            xrTableCell18.Text = rm.GetString("labelBarkodi", ci);
            xrTableCell5.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell7.Text = rm.GetString("labelNjesia", ci);
            xrTableCell9.Text = rm.GetString("labelSasia", ci);
            xrTableCell10.Text = rm.GetString("labelRaportCmimiPaTVSH", ci);
            xrTableCell8.Text = rm.GetString("labelVleftapaTVSH", ci);
            xrTableCell11.Text = rm.GetString("labelTVSH", ci);
            xrTableCell6.Text = rm.GetString("labelVleftameTVSH", ci);

            xrLabel14.Text = rm.GetString("labelFilterAvancuarMonedha", ci);

            xrLabel24.Text = rm.GetString("labelTotaliUpperCase", ci);
            xrLabel25.Text = rm.GetString("labelKursi", ci);
            xrLabel19.Text = rm.GetString("labelTotaliLekUpperCase", ci);

            xrLabel37.Text = rm.GetString("lblShitesKrijues", ci) + ":";
            xrLabel38.Text = rm.GetString("labelBleresi", ci) + ":";
            xrLabel53.Text = rm.GetString("labelTransportuesi", ci) + ":";

            xrLabel39.Text = rm.GetString("labelShenim", ci) + ":";
            xrLabel40.Text = rm.GetString("labelDokumentiNukPerbenFatureTatimore", ci);
            xrLabel54.Text = rm.GetString("labelKjoFatureDuhetPaguar", ci);


            xrLabel65.Text = rm.GetString("labelFleteGarancieUpperCase", ci);
            xrTableCell45.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell46.Text = rm.GetString("labelRaportSerial", ci);
            xrTableCell47.Text = rm.GetString("labelGarancia", ci);

            xrLabel64.Text = rm.GetString("labelFastechGarantonGaranciaOfrohet", ci);
            xrLabel56.Text = rm.GetString("labelKushtetGarancis�", ci) + ":";
            xrLabel55.Text = rm.GetString("labelFastechRiparonNesePlotesohenKushtet", ci);
            xrLabel63.Text = rm.GetString("labelP�rjashtimetNgaGarancia", ci);
            xrLabel57.Text = rm.GetString("labelGaranciaNukMerretParasysh", ci);
            
        }

    }
}
