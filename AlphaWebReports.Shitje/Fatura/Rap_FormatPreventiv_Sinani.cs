using System;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatPreventiv_Sinani : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatPreventiv_Sinani(){InitializeComponent();}
        public Rap_FormatPreventiv_Sinani(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatPreventiv_Sinani(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            parameterIdNderm.Value = idNdermarrje;
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrTableCell17.Text = rm.GetString("labelRaportiNrUpperCase", ci);
            xrLabel1.Text = rm.GetString("lblRaportFatureTitull", ci);
            xrLabel8.Text = rm.GetString("lblRaportNumriSerial", ci);
            xrLabel7.Text = rm.GetString("labelRaportDataFatures", ci);
            xrTableCell4.Text = rm.GetString("lblRaportNrkartelUpperCase", ci);
            xrTableCell5.Text = rm.GetString("labelPERSHKRIMI", ci);
            xrLabel14.Text = rm.GetString("labelRaportTel", ci);
            xrLabel4.Text = rm.GetString("labelRaportSubjektBleres", ci);
            xrTableCell14.Text = rm.GetString("lblRaportNrkartel", ci);
            xrTableCell15.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell7.Text = rm.GetString("labelNjesiaUpperCase", ci);
            xrLabel35.Text = rm.GetString("labelKursi", ci) + ":";
            xrTableCell9.Text = rm.GetString("label_SASIA", ci);
            xrTableCell10.Text = rm.GetString("label_CMIMI", ci);
            xrTableCell8.Text = rm.GetString("labelVleraUpperCase", ci);
            xrLabel24.Text = rm.GetString("labelRaportTotaliMeZbritje", ci);
            xrLabel22.Text = rm.GetString("labelBleresi", ci);
            xrLabel23.Text = rm.GetString("labelTransportuesi", ci);
            xrLabel25.Text = rm.GetString("lblRaportKontrollori", ci);
            xrLabel26.Text = rm.GetString("lblShitesKrijues", ci);
            xrLabel33.Text = rm.GetString("lblRaportMagazina", ci) + ":";
            xrLabel17.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel21.Text = rm.GetString("labelRaportEmail", ci);
        }
        
    }
}
