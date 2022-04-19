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
    public partial class Rap_FatureShitje_FEAR : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureShitje_FEAR(){InitializeComponent();} 

        double totalipatvsh = 0;
        double totalimetvsh = 0;
        double totalipatvshkursi = 0;
        double totalimetvshkursi = 0;
        double sasia_total;
        double shuma;
        int nr = 0;
        public Rap_FatureShitje_FEAR(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {
        }
        public Rap_FatureShitje_FEAR(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel1.Text = rm.GetString("RaportFatureShitjeTitulli", ci);
            xrLabel8.Text = rm.GetString("lblRaportNumriSerial", ci) + " :";
            xrLabel7.Text = rm.GetString("lblRaportDataFatures", ci)+" :";
            xrLabel44.Text = rm.GetString("labelRaportNumriFatures", ci)+ " :";
            xrLabel50.Text = rm.GetString("labelRaportSubjektiShites", ci)+ " :";
            xrLabel13.Text = rm.GetString("labelRaportAdresa", ci) +" :";
            xrLabel12.Text = rm.GetString("labelNIPT", ci)+" :";
            xrLabel14.Text = rm.GetString("labelRaportTel", ci);
            xrLabel4.Text = rm.GetString("labelRaportSubjektiBleres", ci)+ " :";
            xrLabel2.Text = rm.GetString("labelRaportAdresa", ci)+" :";
            xrLabel16.Text = rm.GetString("labelNIPT", ci) +" :";
            xrLabel18.Text = rm.GetString("labelRaportTel", ci);
            xrTableCell4.Text = (rm.GetString("lblNRKARTELE", ci)).ToUpper();
            xrTableCell15.Text = (rm.GetString("labelRaportPershkrimi", ci)).ToUpper();
            xrTableCell7.Text = rm.GetString("labelNjesiaUpperCase", ci);
            xrTableCell3.Text = rm.GetString("labelVlera_Pa_Tvsh", ci);
            xrTableCell6.Text = rm.GetString("labelTVSH", ci);
            xrTableCell9.Text = rm.GetString("label_SASIA", ci);
            xrTableCell10.Text = rm.GetString("label_CMIMI", ci);
            xrTableCell8.Text = rm.GetString("labelVLERA_ME_TVSH", ci);
            xrLabel22.Text = rm.GetString("labelBleresi", ci);
            xrLabel23.Text = rm.GetString("labelTransportuesi", ci);
            xrLabel25.Text = rm.GetString("lblRaportKontrollori", ci);
            xrLabel26.Text = rm.GetString("lblShitesKrijues", ci);
            xrTableCell5.Text = rm.GetString("labelPERSHKRIMI", ci);
            xrLabel53.Text = rm.GetString("labelKursi", ci) + ":";
            xrLabel24.Text = rm.GetString("lblRaportTotaliNeto", ci);
            xrTableCell17.Text = rm.GetString("lblNrUpperCase", ci);
            xrLabel6.Text = rm.GetString("lblBKTTirane", ci);
            xrLabel20.Text = rm.GetString("lblNrLlogBKTFear", ci);
            xrLabel21.Text = rm.GetString("lblIBANFearBKT", ci);
            xrLabel14.Text = rm.GetString("lblRaiifesenFear", ci);
            xrLabel24.Text = rm.GetString("lblNrLlogRaiffeisenFear",ci);
            xrLabel25.Text = rm.GetString("lblNrLlogRaifesienEUROFear", ci);
            xrLabel18.Text = rm.GetString("lblProCreditFear", ci);
            xrLabel28.Text = rm.GetString("lblProCreditFearLek", ci);
            xrLabel29.Text = rm.GetString("lblProCreditFearEur", ci);
            xrLabel30.Text = rm.GetString("lblAdrsFear", ci);
            xrLabel31.Text = rm.GetString("lblKontaktFear", ci);
            xrLabel52.Text = rm.GetString("lblRaportTotaliNe", ci);
            xrLabel32.Text = rm.GetString("labelRaportiTotali", ci);
        }
    }
}
