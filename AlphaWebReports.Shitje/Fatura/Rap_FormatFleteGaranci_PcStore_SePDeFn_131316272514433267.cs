using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatFleteGaranci_PcStore_SePDeFn_131316272514433267 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FormatFleteGaranci_PcStore_SePDeFn_131316272514433267() { InitializeComponent(); }
        public Rap_FormatFleteGaranci_PcStore_SePDeFn_131316272514433267(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {
        }
        public Rap_FormatFleteGaranci_PcStore_SePDeFn_131316272514433267(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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


            xrLabel12.Text = rm.GetString("labelFleteGarancieUpperCase", ci);
            xrTableCell12.Text = rm.GetString("lblAdresa", ci);
            xrTableCell14.Text = xrTableCell31.Text = rm.GetString("labelRaportTel", ci) + ":";
            xrTableCell16.Text = xrTableCell33.Text = rm.GetString("labelNIPT", ci) + ":"; 
            xrTableCell27.Text = rm.GetString("lblEmriIBleresit", ci);
            xrTableCell10.Text = rm.GetString("labelEmriiShitesit", ci) + ":";
            xrTableCell7.Text = rm.GetString("labelRaportData", ci) + ":";
            xrTableCell21.Text = rm.GetString("labelKursi", ci) + ":";
            xrTableCell19.Text = rm.GetString("filterMonedha", ci);
            xrTableCell23.Text = rm.GetString("lblRaportOra", ci) + ":";
            xrTableCell25.Text = rm.GetString("labelAfati", ci) + ":";

            tableCell1.Text = rm.GetString("labelRaportiNrUpperCase", ci);
            tableCell2.Text = rm.GetString("labelPERSHKRIMI", ci);
            tableCell3.Text = rm.GetString("labelNjesiaUpperCase", ci);
            xrTableCell6.Text = rm.GetString("label_SASIA", ci);
            tableCell7.Text = rm.GetString("labelVLERA_ME_TVSH", ci);

            label19.Text = rm.GetString("lblTotaliUpperCase", ci) + ":";
            label20.Text =  rm.GetString("labelRaportPAGESA", ci) + ":";
            label3.Text = rm.GetString("labelShitesi", ci);
            label2.Text = rm.GetString("labelRaportTransportues", ci);
            label1.Text = rm.GetString("labelBleresi", ci);

                 

        }
    }
    }
