using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.ListPagesat.Raportet
    //AlphaWebReports.RaportetDs.ListPagesat.Raportet.Rap_ListepagesaBanka_RFZ
{
    public partial class Rap_ListepagesaBanka_RFZ : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ListepagesaBanka_RFZ(){InitializeComponent();}
        
        int shifraPasPresjes = 0;
        ResourceManager rm = new ResourceManager("Resources.Strings",
                     System.Reflection.Assembly.Load("App_GlobalResources"));
        CultureInfo ci;
        double totalipaga = 0.00;
        public Rap_ListepagesaBanka_RFZ(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_ListepagesaBanka_RFZ(CultureInfo ci, int idNdermarrje, int idViti, XtraReport report)
        {
            this.ci = ci;
            InitializeComponent();
            EmrateLabelave(ci);         
            Muaji.Value = report.Parameters["filterMuaji"].Value.ToString();
            caktoFormatinENumrave();
        }

        private void caktoFormatinENumrave()
        {
            //xrLabel7.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrLabel7.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            ////xrLabel9.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            //xrLabel9.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrLabel9.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrLabel7.BeforePrint += label_BeforePrint;
            //xrLabel9.BeforePrint += label_BeforePrint;
        }


 
        private void xrLabel1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //xrLabel1.Text = Convert.ToString(i++);
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));

           // xrLabel11.Text = rm.GetString("labelRaportEmri", ci);
           // xrLabel12.Text = rm.GetString("RaportListëpagesaBankaTitulli", ci);
           // FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
           // xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
           //xrLabel41.Text = rm.GetString("labelRaportiNr", ci);
           // xrLabel1.Text = rm.GetString("labelRaportEmri", ci);
           // xrLabel45.Text = rm.GetString("labelRaportAtesia", ci);
           // xrLabel13.Text = rm.GetString("labelRaportMbiemri", ci);
           // xrLabel14.Text = rm.GetString("labelRaportNrSigurimeve", ci);
           // xrLabel47.Text = rm.GetString("labelRaportNrLlogariseRrjedhese", ci);
           // xrLabel15.Text = rm.GetString("labelRaportPagaNeto", ci);
          //  xrLabel8.Text = rm.GetString("labelRaportiTotali", ci);


        }

        private void xrLabel52_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel52.Text = rm.GetString("labelDebit/Credit", ci);
        }

        private void xrLabel46_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel46.Text = rm.GetString("labelKoment", ci);
        }

        private void xrLabel21_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel21.Text = Muaji.Value.ToString();
        }

        private void xrLabel34_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = totalipaga;
        }

        private void xrLabel34_SummaryReset(object sender, EventArgs e)
        {
            totalipaga = 0.00;
        }

        private void xrLabel34_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("PAGANETO") != null)
            {
                totalipaga += Convert.ToDouble(GetCurrentColumnValue("PAGANETO"));
            }
        }

        private void xrLabel34_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }

        private void Rap_ListepagesaBanka_RFZ_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
        }
    }
}
