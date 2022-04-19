using System;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatPreventiv_MeDetKlienti : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatPreventiv_MeDetKlienti(){InitializeComponent();} 
        private int counter = 0;
        public Rap_FormatPreventiv_MeDetKlienti(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {
        }
        public Rap_FormatPreventiv_MeDetKlienti(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrTableCell17.Text = rm.GetString("labelRaportiNrUpperCase", ci);
            xrLabel1.Text = rm.GetString("lblRaportFatureTitull", ci);
            xrLabel8.Text = rm.GetString("lblRaportNumriSerial", ci);
            xrLabel7.Text = rm.GetString("labelRaportDataFatures", ci);
            xrTableCell4.Text = rm.GetString("lblRaportNrkartelUpperCase", ci);
            xrTableCell5.Text = rm.GetString("labelPERSHKRIMI", ci);
            xrLabel14.Text = rm.GetString("labelRaportTel", ci);
            xrLabel4.Text = rm.GetString("labelRaportSubjektBleres", ci);
            xrTableCell14.Text = rm.GetString("lblRaportNrkartel", ci);
            xrTableCell15.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell7.Text = rm.GetString("labelNjesiaUpperCase", ci);
            xrLabel35.Text = rm.GetString("labelKursi", ci) + ":";
            xrTableCell9.Text = rm.GetString("label_SASIA", ci);
            xrTableCell10.Text = rm.GetString("label_CMIMI", ci);
            xrTableCell8.Text = rm.GetString("labelVleraUpperCase", ci);
            xrLabel24.Text = rm.GetString("labelRaportTotaliMeZbritje", ci);
            xrLabel22.Text = rm.GetString("labelBleresi", ci);
            xrLabel23.Text = rm.GetString("labelTransportuesi", ci);
            xrLabel25.Text = rm.GetString("lblRaportKontrollori", ci);
            xrLabel26.Text = rm.GetString("lblShitesKrijues", ci);
            xrLabel33.Text = rm.GetString("lblRaportMagazina", ci) + ":";
            xrLabel17.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel21.Text = rm.GetString("labelRaportEmail", ci);
            xrLabel43.Text = rm.GetString("lblDetyrimeNe", ci);
        }


















































        private void xrTableCell3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KODI") != null)
            {
                counter++;
                xrTableCell3.Text = counter.ToString();
            }
            else
            {
                xrTableCell3.Text = string.Empty;
            }
        }

        private void xrLabel24_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Convert.ToDouble(GetCurrentColumnValue("ZBRITJE")) == 0)
            {
                xrLabel24.Visible = false;
            }
        }

        private void xrLabel32_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Convert.ToDouble(GetCurrentColumnValue("ZBRITJE")) == 0)
            {
                xrLabel32.Visible = false;
            }
        }

        private void xrLabel25_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Convert.ToDouble(GetCurrentColumnValue("ZBRITJE")) == 0)
            {
                xrLabel25.Visible = false;
            }
        }

        private void xrLabel28_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Convert.ToDouble(GetCurrentColumnValue("ZBRITJE")) == 0)
            {
                xrLabel28.Visible = false;
            }
        }

        private void xrLabel35_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Convert.ToDouble(GetCurrentColumnValue("ZBRITJE")) == 0)
            {
                xrLabel35.Visible = false;
            }
        }

        private void xrLabel36_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Convert.ToDouble(GetCurrentColumnValue("ZBRITJE")) == 0)
            {
                xrLabel36.Visible = false;
            }
        }

        private void xrLabel38_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Convert.ToDouble(GetCurrentColumnValue("ZBRITJE")) == 0)
            {
                xrLabel38.Visible = false;
            }
        }

        private void xrLabel40_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Convert.ToDouble(GetCurrentColumnValue("ZBRITJE")) == 0)
            {
                xrLabel40.Visible = false;
            }
        }

        private void xrLabel19_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Convert.ToDouble(GetCurrentColumnValue("ZBRITJE")) == 0)
            {
                xrLabel19.Visible = false;
            }
        }

        private void xrLabel20_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (Convert.ToDouble(GetCurrentColumnValue("ZBRITJE")) == 0)
            {
                xrLabel20.Visible = false;
            }
        }
    }
}
