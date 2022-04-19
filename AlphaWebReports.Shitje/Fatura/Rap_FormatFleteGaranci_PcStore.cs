using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatFleteGaranci_PcStore : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FormatFleteGaranci_PcStore() { InitializeComponent(); }
        public Rap_FormatFleteGaranci_PcStore(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatFleteGaranci_PcStore(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel12.Text = rm.GetString("labelFleteGarancieUpperCase", ci);
            xrTableCell12.Text = rm.GetString("lblAdresa", ci);
            xrTableCell29.Text = rm.GetString("lblAdresaOp", ci);
            xrTableCell14.Text = xrTableCell31.Text = rm.GetString("lblteli", ci);
            xrTableCell16.Text = xrTableCell33.Text = rm.GetString("labelAdministrimiNIPT", ci);
            xrTableCell19.Text = rm.GetString("labelAdministrimiMonedha", ci);
            xrTableCell7.Text = rm.GetString("labelRaportData", ci) + ":";
            xrTableCell21.Text = rm.GetString("labelKursi", ci) + ":";
            xrTableCell23.Text = rm.GetString("lblRaportOra", ci) + ":";
            tableCell1.Text = rm.GetString("labelRaportiNrUpperCase", ci);
            tableCell2.Text = rm.GetString("labelPERSHKRIMI", ci);
            tableCell3.Text = rm.GetString("labelNjesiaUpperCase", ci);
            xrTableCell6.Text = rm.GetString("label_SASIA", ci);

        }
    }
}
