using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureShitjeMD : DevExpress.XtraReports.UI.XtraReport
    {
       
        public Rap_FatureShitjeMD() { InitializeComponent(); }

        public Rap_FatureShitjeMD(ParametraRaporti param, XtraReport report) :
        this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi){}
        public Rap_FatureShitjeMD(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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


        }

        private void xrTableCell46_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KODARTIKULLI") != null || GetCurrentColumnValue("PERSHKRIMARTIKULLI") != null)
            {
                xrTableCell46.Text = GetCurrentColumnValue("KODARTIKULLI").ToString() + '\n' + GetCurrentColumnValue("PERSHKRIMARTIKULLI").ToString();
            }

        }

        private void xrLabel6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("PERDORUESEMRI") != null || GetCurrentColumnValue("PERDORUESMBIEMRI") != null)
            {
                xrLabel6.Text = GetCurrentColumnValue("PERDORUESEMRI").ToString() + " " + GetCurrentColumnValue("PERDORUESMBIEMRI").ToString();
            }
        }

        private void xrTableCell58_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCell58.Text = Math.Round((Convert.ToDouble(GetCurrentColumnValue("CMIMI")) / (1 + Convert.ToDouble(GetCurrentColumnValue("tvsh")))), 2).ToString("#,#0.00");
        }

        private string getBleres()
        {
            if (GetCurrentColumnValue("EMERKLIENTI") == null)
                return "";

            string emerKlienti = GetCurrentColumnValue("EMERKLIENTI").ToString();
            string emertimiKF = GetCurrentColumnValue("EMERTIMIKF").ToString();
            return string.IsNullOrEmpty(emerKlienti) ? emertimiKF : emerKlienti;
        }

        private void xrLabel10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRLabel)sender).Text = getBleres();
        }

        private void xrTableCell6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            ((XRTableCell)sender).Text = getBleres();
        }

    }
}
