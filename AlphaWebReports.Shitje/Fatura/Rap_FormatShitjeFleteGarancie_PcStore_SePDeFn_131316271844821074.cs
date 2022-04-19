using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatShitjeFleteGarancie_PcStore_SePDeFn_131316271844821074 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FormatShitjeFleteGarancie_PcStore_SePDeFn_131316271844821074() { InitializeComponent(); }
        public Rap_FormatShitjeFleteGarancie_PcStore_SePDeFn_131316271844821074(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {
        }
        public Rap_FormatShitjeFleteGarancie_PcStore_SePDeFn_131316271844821074(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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

            xrLabel1.Text = rm.GetString("labelFleteGarancieUpperCase", ci);
            xrLabel4.Text = rm.GetString("labelEmerMbiemerFirma", ci);
            xrLabel2.Text = rm.GetString("labelRaportDateFature", ci);
            label1.Text = xrLabel12.Text = rm.GetString("labelNIPT", ci);
            xrLabel6.Text = rm.GetString("labelRaportNrFature", ci);
            xrLabel13.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel7.Text = rm.GetString("labelAfati", ci) + ":";
            xrLabel54.Text = xrLabel14.Text = rm.GetString("labelRaportTel", ci) + ":";
            xrLabel53.Text = rm.GetString("labelEmriBleresit", ci) + ":";
            xrLabel55.Text = rm.GetString("labelRaportAdresa", ci) + ":";
            xrLabel8.Text = rm.GetString("labelEmerMbiemerFirma", ci);
            xrLabel15.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel35.Text = rm.GetString("labelEmriShitesit", ci) + ":";
            xrLabel13.Text = rm.GetString("labelRaportAdresa", ci) + ":";
            xrTableCell4.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell5.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell7.Text = rm.GetString("labelNjesia", ci);
            xrTableCell9.Text = rm.GetString("labelSasia", ci);
            xrTableCell10.Text = rm.GetString("labelRaportCmimiPaTVSH", ci);
            xrLabel16.Text = rm.GetString("labelRaportiTotali", ci) + ":";
            xrLabel26.Text = rm.GetString("labelLikujdimi", ci) + ":";
            xrLabel5.Text = rm.GetString("labelEmerMbiemerFirma", ci);
            xrLabel11.Text = rm.GetString("labelGjithsej", ci);
            xrLabel44.Text = rm.GetString("labelShitesi", ci);
            xrLabel46.Text = rm.GetString("labelRaportTransportues", ci);
            xrLabel45.Text = rm.GetString("labelBleresi", ci);
            label9.Text = rm.GetString("labelMbetja", ci) + ":";
            label6.Text = rm.GetString("labelKursi", ci);
            label4.Text = rm.GetString("lblRaportTotaliNe", ci);
        }


        }
}
