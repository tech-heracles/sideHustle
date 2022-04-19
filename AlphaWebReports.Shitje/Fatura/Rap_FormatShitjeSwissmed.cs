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
    public partial class Rap_FormatShitjeSwissmed : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatShitjeSwissmed(){InitializeComponent();} 
        private ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

        public Rap_FormatShitjeSwissmed(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatShitjeSwissmed(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);

            xrTableCell4.BackColor = System.Drawing.ColorTranslator.FromHtml("#B8CCE4");
            xrTableCell5.BackColor = System.Drawing.ColorTranslator.FromHtml("#B8CCE4");
            xrTableCell19.BackColor = System.Drawing.ColorTranslator.FromHtml("#B8CCE4");
            xrTableCell17.BackColor = System.Drawing.ColorTranslator.FromHtml("#B8CCE4");
            xrTableCell7.BackColor = System.Drawing.ColorTranslator.FromHtml("#B8CCE4");
            xrTableCell9.BackColor = System.Drawing.ColorTranslator.FromHtml("#B8CCE4");
            xrTableCell10.BackColor = System.Drawing.ColorTranslator.FromHtml("#B8CCE4");
            xrTableCell21.BackColor = System.Drawing.ColorTranslator.FromHtml("#B8CCE4");
            xrTableCell8.BackColor = System.Drawing.ColorTranslator.FromHtml("#B8CCE4");
            xrTableCell11.BackColor = System.Drawing.ColorTranslator.FromHtml("#B8CCE4");
            xrTableCell6.BackColor = System.Drawing.ColorTranslator.FromHtml("#B8CCE4");
            xrLabel9.BackColor = System.Drawing.ColorTranslator.FromHtml("#B8CCE4");
            xrLabel26.BackColor = System.Drawing.ColorTranslator.FromHtml("#B8CCE4");
            xrLabel36.BackColor = System.Drawing.ColorTranslator.FromHtml("#B8CCE4");
            xrLabel37.BackColor = System.Drawing.ColorTranslator.FromHtml("#B8CCE4");
            xrLabel38.BackColor = System.Drawing.ColorTranslator.FromHtml("#B8CCE4");
            xrLabel39.BackColor = System.Drawing.ColorTranslator.FromHtml("#B8CCE4");
            xrLabel40.BackColor = System.Drawing.ColorTranslator.FromHtml("#B8CCE4");
            xrLabel41.BackColor = System.Drawing.ColorTranslator.FromHtml("#B8CCE4");
            xrLabel42.BackColor = System.Drawing.ColorTranslator.FromHtml("#B8CCE4");
            xrLabel43.BackColor = System.Drawing.ColorTranslator.FromHtml("#B8CCE4");

        }
        
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            xrLabel4.Text = rm.GetString("labelRaportSubjektiBleres", ci);
            xrLabel2.Text = rm.GetString("labelRaportSubjektiShites", ci);
            xrLabel12.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel6.Text = rm.GetString("labelRaportNumriFatures", ci);
            xrLabel13.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel7.Text = rm.GetString("lblRaportDataFatures", ci);
            xrLabel14.Text = rm.GetString("labelRaportTelFax", ci);
            xrLabel15.Text = rm.GetString("labelRaportLlogariBankare", ci);
            xrTableCell4.Text = rm.GetString("labelRaportNRKARTELE", ci);
            xrTableCell5.Text = rm.GetString("labelPERSHKRIMI", ci);
            xrTableCell19.Text = rm.GetString("labelRaportDTSKAD", ci);
            xrTableCell7.Text = rm.GetString("labelNjesiaUpperCase", ci);
            xrTableCell9.Text = rm.GetString("label_SASIA", ci);
            xrTableCell10.Text = rm.GetString("label_CMIMI", ci);
            xrTableCell17.Text = rm.GetString("labelRaportLOTNR", ci);
            xrTableCell8.Text = rm.GetString("labelVlera_Pa_Tvsh", ci);
            xrTableCell11.Text = rm.GetString("labelTVSH", ci);
            xrTableCell6.Text = rm.GetString("labelVLERA_ME_TVSH", ci);
            xrLabel16.Text = rm.GetString("labelRaportNrBiznesit", ci);
            xrLabel20.Text = rm.GetString("labelRaportNrBiznesit", ci);
            xrLabel26.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel44.Text = rm.GetString("labelBleresi", ci);
            xrLabel45.Text = rm.GetString("labelRaportEmerMbiemerFirma", ci);
            xrLabel46.Text = rm.GetString("labelShitesi", ci);
            xrLabel47.Text = rm.GetString("labelRaportEmerMbiemerFirma", ci);
            xrLabel48.Text = rm.GetString("labelRaportEmail", ci);
            xrLabel57.Text = rm.GetString("labelRaportNrFiskal", ci);
            xrLabel15.Text = rm.GetString("labelRaportNrFiskal", ci);
            xrLabel48.Text = rm.GetString("labelRaportNumerTVSH", ci);
            xrLabel50.Text = rm.GetString("labelRaportNumerTVSH", ci);
            xrLabel14.Text = rm.GetString("labelRaportTel", ci);
            xrLabel59.Text = rm.GetString("labelRaportTel", ci);
            xrLabel11.Text = rm.GetString("labelNumriLlogariseBankare", ci);
        }
        
    }
}
