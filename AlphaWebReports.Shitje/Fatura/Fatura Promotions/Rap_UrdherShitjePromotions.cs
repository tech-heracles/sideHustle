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
    public partial class Rap_UrdherShitjePromotions : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_UrdherShitjePromotions(){InitializeComponent();} 

        public Rap_UrdherShitjePromotions(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_UrdherShitjePromotions(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel1.Text = rm.GetString("lblUrdherPorosiTitull", ci);
            xrLabel49.Text = rm.GetString("labelRaportiNr", ci);
            xrLabel50.Text = rm.GetString("filterMonedha", ci);
            xrTableCell36.Text = rm.GetString("labelNIPT", ci);
            xrTableCell53.Text = rm.GetString("labelNIPT", ci);
            xrTableCell43.Text = rm.GetString("labelRaportAdresa", ci);
            xrTableCell54.Text = rm.GetString("labelRaportAdresa", ci);
            xrTableCell55.Text = rm.GetString("labelRaportTel", ci);
            xrTableCell45.Text = rm.GetString("labelRaportTel", ci);
            xrTableCell47.Text = rm.GetString("labelRaportEmail", ci);
            xrTableCell56.Text = rm.GetString("labelRaportEmail", ci);
            xrTableCell60.Text = rm.GetString("labelRaportKontakti", ci);
           xrTableCell9.Text = rm.GetString("labelRaportiNr", ci);
           xrTableCell4.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell5.Text = rm.GetString("labelKodi", ci);
            xrTableCell7.Text = rm.GetString("labelNjesia", ci);
            xrTableCell9.Text = rm.GetString("labelSasia", ci);
             xrTableCell10.Text = rm.GetString("labelCmimi", ci);
             xrTableCell8.Text = rm.GetString("labelRaportVlera", ci);
            xrTableCell11.Text = rm.GetString("labelTVSH", ci);
            xrTableCell6.Text = rm.GetString("labelVleraMeTVSH", ci);
            xrTableCell19.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell17.Text = rm.GetString("labelRaportiShenime", ci);
            xrLabel6.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel14.Text = rm.GetString("labelKursi", ci);
            xrLabel20.Text = rm.GetString("lblRaportVleraNe", ci);
            xrLabel15.Text = rm.GetString("labelBleresi", ci);
            xrLabel36.Text = rm.GetString("lblShitesKrijues", ci);
            xrLabel19.Text = rm.GetString("labelEmerMbiemerFirma", ci);
            xrLabel37.Text = rm.GetString("labelEmerMbiemerFirma", ci);
            xrLabel38.Text = rm.GetString("labelRaportEmail", ci);
            xrLabel40.Text = rm.GetString("labelRaportTel", ci);
        }

    }
}
