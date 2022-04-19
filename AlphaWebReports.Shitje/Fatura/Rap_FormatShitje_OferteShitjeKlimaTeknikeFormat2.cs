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
    public partial class Rap_FormatShitje_OferteShitjeKlimaTeknikeFormat2 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatShitje_OferteShitjeKlimaTeknikeFormat2(){
            InitializeComponent();
        } 
        public Rap_FormatShitje_OferteShitjeKlimaTeknikeFormat2(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatShitje_OferteShitjeKlimaTeknikeFormat2(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel8.Text = rm.GetString("lblRaportShitjeUpperCase", ci);
            xrLabel1.Text = rm.GetString("labelTotalimeScontomeTVSH", ci); 
            xrLabel24.Text = rm.GetString("labelTotalimeTVSH", ci);
            xrLabel44.Text = rm.GetString("labelRaportNumriFatures", ci);
            xrLabel7.Text = rm.GetString("labelRaportDateFature", ci);
            xrLabel47.Text = rm.GetString("labelRaportiShenime", ci);
            xrLabel50.Text = rm.GetString("labelRaportiSubjekti", ci);
            xrLabel13.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel12.Text = rm.GetString("labelNIPT", ci);
            xrLabel14.Text = rm.GetString("labelRaportTelFax", ci);
            xrLabel4.Text = rm.GetString("labelRaportSubjektBleres", ci);
            xrLabel2.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel16.Text = rm.GetString("labelNIPT", ci);
            xrLabel18.Text = rm.GetString("labelRaportTelFax", ci);
            xrTableCell3.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell20.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell22.Text = rm.GetString("labelNjesia", ci);
            xrTableCell23.Text = rm.GetString("labelSasia", ci);
            xrTableCell24.Text = rm.GetString("labelRaportCmimiMeTVSH", ci);
            xrTableCell21.Text = rm.GetString("labelRaportiShenime", ci);
            xrTableCell25.Text = rm.GetString("labelVleraMeTVSH", ci);
            xrTableCell11.Text = rm.GetString("labelRaportPranuesi", ci);
            xrTableCell19.Text = rm.GetString("labelOfertuesi", ci);
        }
    }
}
