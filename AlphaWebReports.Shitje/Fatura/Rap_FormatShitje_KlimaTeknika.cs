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
    public partial class Rap_FormatShitje_KlimaTeknika: DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatShitje_KlimaTeknika(){InitializeComponent();} 
        
        public Rap_FormatShitje_KlimaTeknika(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatShitje_KlimaTeknika(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel1.Text = rm.GetString("lblRaportFatureTitull", ci);
            xrLabel44.Text = rm.GetString("labelRaportNumriFatures", ci);
            xrLabel8.Text = rm.GetString("lblRaportNumriSerial", ci);
            xrLabel7.Text = rm.GetString("labelRaportDateFature", ci);
            xrLabel47.Text = rm.GetString("labelRaportiShenime", ci);
            xrTableCell11.Text = rm.GetString("filterMagazina", ci);
            xrLabel50.Text = rm.GetString("labelRaportiSubjekti", ci);
            xrLabel13.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel12.Text = rm.GetString("labelNIPT", ci);
            xrLabel14.Text = rm.GetString("labelRaportTelFax", ci);
            xrLabel4.Text = rm.GetString("labelRaportSubjektBleres", ci);
            xrLabel2.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel16.Text = rm.GetString("labelNIPT", ci);
            xrLabel18.Text = rm.GetString("labelRaportTelFax", ci);
            xrLabel20.Text = rm.GetString("lblRaportDetyrimi", ci);
            xrTableCell3.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell5.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell4.Text = rm.GetString("lblRaportNrkartel");
            xrTableCell11.Text = rm.GetString("", ci);
           xrTableCell7.Text = rm.GetString("labelNjesia", ci);
            xrTableCell9.Text = rm.GetString("labelSasia", ci);
            xrTableCell10.Text = rm.GetString("labelCmimi", ci);
            xrTableCell17.Text = rm.GetString("labelRaportiShenime", ci);
            xrTableCell19.Text = rm.GetString("labelRaport�Skonto", ci);
            xrTableCell8.Text = rm.GetString("labelRaportVlera", ci);
            xrLabel24.Text = rm.GetString("labelRaportiTotali", ci) + ":";
            xrLabel22.Text = rm.GetString("labelRaportPranuesi", ci);        
            // xrLabel26.Text = rm.GetString("lblRaportPreventivuesi", ci);

          }

       }
}
