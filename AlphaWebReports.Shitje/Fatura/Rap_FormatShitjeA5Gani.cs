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
    public partial class Rap_FormatShitjeA5Gani : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatShitjeA5Gani(){InitializeComponent();} 
        private ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));


        public Rap_FormatShitjeA5Gani(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatShitjeA5Gani(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel1.Text = rm.GetString("lblRaportShitjeUpperCase", ci);
            xrLabel6.Text = rm.GetString("labelRaportNumriFatures", ci);
            xrLabel7.Text = rm.GetString("lblRaportDataFatures", ci);
            xrLabel4.Text = rm.GetString("labelRaportSubjektiBleres", ci);
            xrTableCell4.Text = rm.GetString("lblRaportNrUpperCase", ci);
            xrTableCell5.Text = rm.GetString("labelPERSHKRIMI", ci);
            xrTableCell17.Text = rm.GetString("labelKartelaUpperCase", ci);
            xrTableCell7.Text = rm.GetString("labelNjesiUpperCase", ci);
            xrTableCell9.Text = rm.GetString("label_SASIA", ci);
            xrTableCell10.Text = rm.GetString("label_CMIMI", ci);
            xrTableCell6.Text = rm.GetString("lblVLERA", ci);
            xrLabel26.Text = rm.GetString("lblRaportTotaliNe", ci);
            xrLabel29.Text = rm.GetString("lblEmriBleresitDheFirma", ci);
            xrLabel30.Text = rm.GetString("lblShitesKrijues", ci);
            xrLabel2.Text = rm.GetString("labelRaportDetyrimi", ci);


        }

    }
}
