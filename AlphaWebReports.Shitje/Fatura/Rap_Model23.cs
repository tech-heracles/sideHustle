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
    public partial class Rap_Model23 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Model23(){InitializeComponent();} 
    
        public Rap_Model23(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_Model23(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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

            xrLabel4.Text = rm.GetString("labelRaportSubjektBleres", ci) + ":";
            xrLabel2.Text = rm.GetString("labelRaportSubjektShites", ci) + ":";
            xrLabel12.Text = rm.GetString("labelNIPT", ci);
            xrLabel6.Text = rm.GetString("labelRaportNrFature", ci);
            xrLabel13.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel7.Text = rm.GetString("labelRaportDateFature", ci);
            xrLabel14.Text = rm.GetString("labelRaportTelFax", ci);
            xrLabel8.Text = rm.GetString("labelRaportNrSerial", ci);
            xrLabel15.Text = rm.GetString("labelRaportLlogariBankare", ci);
            xrTableCell4.Text = rm.GetString("labelKodi", ci);
            xrTableCell5.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell7.Text = rm.GetString("labelNjesia", ci);
            xrTableCell9.Text = rm.GetString("labelSasia", ci);
            xrTableCell10.Text = rm.GetString("labelCmimi", ci);
            xrTableCell8.Text = rm.GetString("labelVleftapaTVSH", ci);
            xrTableCell11.Text = rm.GetString("labelTVSH", ci);
            xrTableCell6.Text = rm.GetString("labelVleftameTVSH", ci);
           xrLabel24.Text = rm.GetString("labelRaportTotaliBrutoNe", ci);
            xrLabel25.Text = rm.GetString("labelKursi", ci) + ":";
            xrLabel26.Text = rm.GetString("labelRaportTotalNeto", ci);

          }
       
    }
}
