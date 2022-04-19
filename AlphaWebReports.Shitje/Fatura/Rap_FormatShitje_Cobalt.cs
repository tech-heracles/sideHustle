using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatShitje_Cobalt : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FormatShitje_Cobalt() { InitializeComponent(); }


        public Rap_FormatShitje_Cobalt(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatShitje_Cobalt(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel4.Text = rm.GetString("labelRaportSubjektiBleres", ci);
            xrLabel2.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel16.Text = rm.GetString("Qyteti", ci);
            xrLabel7.Text = rm.GetString("labelRaportDataFatures", ci);
            xrLabel8.Text = rm.GetString("labelRaportKodKlienti", ci);
            xrTableCell4.Text = rm.GetString("labelRaportiKF", ci);
            tableCell1.Text = rm.GetString("lblRaportiKodArtikull", ci);
            xrTableCell5.Text = rm.GetString("labelPERSHKRIMI", ci);
            xrTableCell9.Text = rm.GetString("label_CMIMI", ci);
            xrTableCell10.Text = rm.GetString("labelRaportiSasi", ci);
            xrTableCell8.Text = rm.GetString("labelVleraUpperCase", ci);
            label2.Text = rm.GetString("labelRaportTotaluUppercase", ci);
        }

    }
}
