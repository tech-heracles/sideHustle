using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Blerje
{
    public partial class Rap_FormatBlerjeKlimateknika : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatBlerjeKlimateknika(){InitializeComponent();} 
        public Rap_FormatBlerjeKlimateknika(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatBlerjeKlimateknika(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrTableCell11.Text = rm.GetString("lblRaportMagazina", ci);
            xrLabel50.Text = rm.GetString("labelRaportiSubjekti", ci);
            xrLabel13.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel12.Text = rm.GetString("labelNIPT", ci);
            xrLabel14.Text = rm.GetString("labelRaportTelFax", ci);
            xrLabel4.Text = rm.GetString("labelRaportSubjektShites", ci);
            xrLabel2.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel16.Text = rm.GetString("labelNIPT", ci);
            xrLabel18.Text = rm.GetString("labelRaportTelFax", ci);
            xrLabel20.Text = rm.GetString("labelRaportDetyrimi", ci);
            xrTableCell4.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell5.Text = rm.GetString("lblRaportNrkartel", ci);
            xrTableCell6.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell7.Text = rm.GetString("labelNjesia", ci);
            xrTableCell9.Text = rm.GetString("labelSasia", ci);
            xrTableCell10.Text = rm.GetString("labelCmimi", ci);
            xrTableCell8.Text = rm.GetString("labelRaportVlera", ci);
            xrLabel24.Text = rm.GetString("labelRaportiTotali", ci) + ":";
            xrTableCell17.Text = rm.GetString("labelRaportiShenime", ci);
            xrLabel23.Text = rm.GetString("labelShitesi", ci);
            xrLabel25.Text = rm.GetString("lblRaportTransportuesi", ci);
            xrLabel26.Text = rm.GetString("lblRaportKontrollori", ci);
            xrLabel27.Text = rm.GetString("labelBleresi", ci);
        }

    }
}
