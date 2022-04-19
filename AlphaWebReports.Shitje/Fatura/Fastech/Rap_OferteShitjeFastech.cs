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
    public partial class Rap_OferteShitjeFastech : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_OferteShitjeFastech(){InitializeComponent();} 

      

        public Rap_OferteShitjeFastech(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_OferteShitjeFastech(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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

            xrLabel1.Text = rm.GetString("RaportOferteTitulli", ci);

            xrLabel6.Text = rm.GetString("labelNumriOfertes", ci) + ":";
            xrLabel45.Text = rm.GetString("labelDataOfertes", ci) + ":";
            xrLabel47.Text = rm.GetString("labelVlefshmeria", ci) + ":";
            xrLabel73.Text = rm.GetString("labelRaportKontakti", ci) + ":";
            xrLabel79.Text = rm.GetString("labelRaportTelefon", ci) + ":";
            xrLabel81.Text = rm.GetString("labelRaportEmail", ci) + ":";
            xrLabel23.Text = rm.GetString("labelRaportKlienti", ci) + ":";
            xrLabel74.Text = rm.GetString("labelRaportTelFax", ci) + ":";
            xrLabel44.Text = rm.GetString("labelNIPT", ci);

            xrTableCell17.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell4.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell18.Text = rm.GetString("labelBarkodi", ci);
            xrTableCell5.Text = rm.GetString("labelNjesia", ci);
            xrTableCell7.Text = rm.GetString("labelSasia", ci);
            xrTableCell10.Text = rm.GetString("labelRaportCmimiPaTVSH", ci);
            xrTableCell8.Text = rm.GetString("labelVleftapaTVSH", ci);
            xrTableCell11.Text = rm.GetString("labelTVSH", ci);
            xrTableCell6.Text = rm.GetString("labelVleftameTVSH", ci);

            xrLabel7.Text = rm.GetString("labelFilterAvancuarMonedha", ci);

            xrLabel24.Text = rm.GetString("labelTotaliUpperCase", ci);
            xrLabel25.Text = rm.GetString("labelKursi", ci);
            xrLabel4.Text = rm.GetString("labelTotaliLekUpperCase", ci);

            xrLabel15.Text = rm.GetString("labelKontaktiPerKeteOferte", ci);

            xrLabel17.Text = rm.GetString("labelRaportiShenime", ci);
            xrLabel18.Text = rm.GetString("labelShenimeFastechRapOferte", ci);
            

          }

    }
}
