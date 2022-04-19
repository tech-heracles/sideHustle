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
    public partial class Rap_FatureShitje_FormatTB2_KlimaTeknika : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureShitje_FormatTB2_KlimaTeknika(){InitializeComponent();} 

        public Rap_FatureShitje_FormatTB2_KlimaTeknika(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitje_FormatTB2_KlimaTeknika(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel1.Text = rm.GetString("RaportFatureShitjeTitulli", ci);
            xrLabel60.Text = rm.GetString("labelNIPT", ci);

            xrLabel44.Text = rm.GetString("labelRaportNumriFatures", ci);

          //  xrLabel34.Text = rm.GetString("labelRaportEmail", ci);
            xrLabel7.Text = rm.GetString("lblRaportDataFatures", ci);
            xrLabel61.Text = rm.GetString("labelRaportTelFax", ci);
            xrLabel8.Text = rm.GetString("lblRaportNumriSerial", ci);
           // xrLabel32.Text = rm.GetString("lblRaportCel", ci);


            xrLabel4.Text = rm.GetString("labelRaportSubjektBleres", ci);
            xrLabel2.Text = rm.GetString("labelRaportAdresa", ci);

            xrLabel16.Text = rm.GetString("labelNIPT", ci);
            xrLabel18.Text = rm.GetString("labelRaportTelFax", ci);

            xrTableCell4.Text = rm.GetString("lblRaportNrkartel", ci);

            xrTableCell5.Text = rm.GetString("labelRaportiPershkrimi", ci);

            xrTableCell7.Text = rm.GetString("labelNjesia", ci);
            xrTableCell9.Text = rm.GetString("labelSasia", ci);
            xrTableCell10.Text = rm.GetString("labelCmimi", ci);

            xrTableCell8.Text = rm.GetString("labelVleraPaTVSH", ci);
            xrTableCell3.Text = rm.GetString("labelTVSH", ci);
            xrTableCell6.Text = rm.GetString("labelVleraMeTVSH", ci);

            xrLabel12.Text = rm.GetString("labelRaportiTotali", ci);

            xrLabel24.Text = rm.GetString("lblRaportTotaliNe", ci);
            xrLabel26.Text = rm.GetString("labelKursi", ci);
            xrLabel27.Text = rm.GetString("labelBleresi", ci);
            xrLabel36.Text = rm.GetString("labelShitesi", ci);
            xrLabel31.Text = rm.GetString("labelEmerMbiemerFirma", ci);
            xrLabel37.Text = rm.GetString("labelEmerMbiemerFirma", ci);
            xrLabel59.Text = rm.GetString("lblRaportNrLlogarie", ci);

        }

    }
}
