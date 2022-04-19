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
            //xrLabel2.Text = rm.GetString("labelRaportSubjektiShites", ci);
            //xrLabel12.Text = rm.GetString("labelRaportAdresa", ci);
            //xrLabel20.Text = rm.GetString("labelNIPT", ci);
            //xrLabel28.Text = rm.GetString("labelRaportTel", ci);
            //xrLabel13.Text = rm.GetString("labelRaportAdresa", ci);
            //xrLabel16.Text = rm.GetString("labelRaportNrBiznesit", ci);
            //xrLabel20.Text = rm.GetString("labelRaportNrBiznesit", ci);
            //xrLabel57.Text = rm.GetString("labelRaportNrFiskal", ci);
            //xrLabel15.Text = rm.GetString("labelRaportNrFiskal", ci);
            //xrLabel48.Text = rm.GetString("labelRaportNumerTVSH", ci);
            //xrLabel50.Text = rm.GetString("labelRaportNumerTVSH", ci);
            //xrLabel14.Text = "Nr. "+rm.GetString("labelRaportLlogBanke", ci);
            xrTableCell4.Text = rm.GetString("lblRaportNrUpperCase", ci);
            xrTableCell5.Text = rm.GetString("labelPERSHKRIMI", ci);
            xrTableCell17.Text = rm.GetString("labelKartelaUpperCase", ci);
            xrTableCell7.Text = rm.GetString("labelNjesiUpperCase", ci);
            xrTableCell9.Text = rm.GetString("label_SASIA", ci);
            xrTableCell10.Text = rm.GetString("label_CMIMI", ci);
            xrTableCell6.Text = rm.GetString("lblVLERA", ci);
            //xrTableCell15.Text = rm.GetString("labelVlera_Pa_Tvsh", ci);
            //xrTableCell16.Text = rm.GetString("labelTVSH", ci);
            //xrLabel34.Text = rm.GetString("labelRaportTotaliPerPagese", ci);
            xrLabel26.Text = rm.GetString("lblRaportTotaliNe", ci);
            xrLabel29.Text = rm.GetString("lblEmriBleresitDheFirma", ci);
            xrLabel30.Text = rm.GetString("lblShitesKrijues", ci);
            xrLabel2.Text = rm.GetString("labelRaportDetyrimi", ci);
            //xrLabel8.Text = rm.GetString("labelRaportEmerMbiemerFirma", ci);
            //xrLabel47.Text = rm.GetString("labelRaportEmerMbiemerFirma", ci);
            //xrLabel11.Text = rm.GetString("labelRaportTel", ci);
            //xrLabel28.Text = rm.GetString("labelRaportTel", ci);
            //xrLabel18.Text = rm.GetString("labelRaportEmail", ci);
            //xrLabel22.Text = rm.GetString("LAYOKontakt", ci);
            //xrLabel23.Text = rm.GetString("LAYOemail", ci);


        }

    }
}
