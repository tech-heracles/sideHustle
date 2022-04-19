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
    public partial class Rap_FormatShitje_Sinani : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatShitje_Sinani(){InitializeComponent();}
        public Rap_FormatShitje_Sinani(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatShitje_Sinani(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel8.Text = rm.GetString("lblRaportNumriSerial", ci);
            xrLabel7.Text = rm.GetString("labelRaportDataFatures", ci);
            xrLabel50.Text = rm.GetString("labelRaportSubjektShites", ci);
            xrLabel13.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel12.Text = rm.GetString("labelNIPT", ci);
            xrLabel14.Text = rm.GetString("labelRaportTel", ci);
            xrLabel4.Text = rm.GetString("labelRaportSubjektBleres", ci);
            xrLabel2.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel16.Text = rm.GetString("labelNIPT", ci);
            xrLabel18.Text = rm.GetString("labelRaportTel", ci);
            xrTableCell14.Text = rm.GetString("lblRaportNrkartel", ci);
            xrTableCell15.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell7.Text = rm.GetString("labelNjesiaUpperCase", ci);
            xrTableCell9.Text = rm.GetString("label_SASIA", ci);
            xrTableCell10.Text = rm.GetString("label_CMIMI", ci);
            xrTableCell8.Text = rm.GetString("labelVLERA_ME_TVSH", ci);
            xrLabel22.Text = rm.GetString("labelBleresi", ci);
            xrLabel23.Text = rm.GetString("labelTransportuesi", ci);
            xrLabel25.Text = rm.GetString("lblRaportKontrollori", ci);
            xrLabel26.Text = rm.GetString("lblShitesKrijues", ci);
            xrLabel33.Text = rm.GetString("lblRaportMagazina", ci) + ":";
            xrTableCell4.Text = rm.GetString("lblRaportNrkartelUpperCase", ci);
            xrTableCell5.Text = rm.GetString("labelPERSHKRIMI", ci);
            xrLabel35.Text = rm.GetString("labelKursi", ci) + ":";
            xrLabel24.Text = rm.GetString("lblRaportTotaliNeto", ci);
            xrLabel58.Text = rm.GetString("lblRaportTotaliBruto", ci);
        }
        
    }
}
