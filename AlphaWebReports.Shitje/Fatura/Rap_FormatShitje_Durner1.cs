using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatShitje_Durner1 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FormatShitje_Durner1() { InitializeComponent(); }
        public Rap_FormatShitje_Durner1(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatShitje_Durner1(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel6.Text = rm.GetString("labelRaportNrFature", ci);
            xrLabel7.Text = rm.GetString("labelRaportDateFature", ci);
            xrLabel8.Text = rm.GetString("labelRaportNrSerial", ci);
            xrLabel12.Text = rm.GetString("labelNIPT", ci);
            xrLabel13.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel14.Text = rm.GetString("labelRaportTelFax", ci);
            xrTableCell4.Text = rm.GetString("labelKodi", ci);
            xrTableCell5.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell7.Text = rm.GetString("labelNjesia", ci);
            xrTableCell9.Text = rm.GetString("labelSasia", ci);
            xrTableCell10.Text = rm.GetString("labelCmimi", ci);
            xrTableCell6.Text = rm.GetString("labelVleftameTVSH", ci);
            tableCell1.Text = rm.GetString("labelRaportiZbritjaPerqindje", ci);
            xrLabel24.Text = rm.GetString("labelRaportTotaliBrutoNe", ci);
            xrLabel26.Text = rm.GetString("labelRaportTotalNeto", ci);
            label3.Text = rm.GetString("labelBlerjeShitjeZbritje", ci);


        }



   }
}
