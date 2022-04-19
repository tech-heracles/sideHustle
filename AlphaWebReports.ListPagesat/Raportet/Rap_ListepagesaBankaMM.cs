using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.ListPagesat.Raportet
{
    public partial class Rap_ListepagesaBankaMM : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ListepagesaBankaMM(){InitializeComponent();}
        
        int shifraPasPresjes = 0;
        public Rap_ListepagesaBankaMM(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_ListepagesaBankaMM(CultureInfo ci, int idNdermarrje, int idViti, XtraReport report)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            Ndermarja.Value = report.Parameters[0].Value;
            Monedha.Value = report.Parameters[1].Value;
            dtDokLabel.Text ="Periudha";
            xrLabel53.Text = report.Parameters["filterDtDok"].Value.ToString();
            DtRegj.Value = report.Parameters[3].Value;
            NrDok.Value = report.Parameters[4].Value;
            KodiPunjesit.Value = report.Parameters[6].Value;
            Departamenti.Value = report.Parameters[7].Value;
            Nendepartamenti.Value = report.Parameters[8].Value;
            shifraPasPresjes = Convert.ToInt16(report.Parameters["filterFormatNumri"].Value);
            Banka.Value = report.Parameters[10].Value;
            caktoFormatinENumrave();
        }

        private void caktoFormatinENumrave()
        {
            xrLabel7.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel7.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrLabel9.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrLabel9.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel9.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
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

            xrLabel11.Text = rm.GetString("labelRaportEmri", ci);
            xrLabel12.Text = rm.GetString("RaportListëpagesaBankaTitulli", ci);
           // FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
           xrLabel41.Text = rm.GetString("labelRaportiNr", ci);
            xrLabel1.Text = rm.GetString("labelRaportEmri", ci);
            xrLabel45.Text = rm.GetString("labelRaportAtesia", ci);
            xrLabel13.Text = rm.GetString("labelRaportMbiemri", ci);
         //   xrLabel14.Text = rm.GetString("labelRaportNrSigurimeve", ci);
            xrLabel47.Text = rm.GetString("labelRaportNrLlogariseRrjedhese", ci);
            xrLabel15.Text = rm.GetString("labelRaportPagaNeto", ci);
            xrLabel8.Text = rm.GetString("labelRaportiTotali", ci);


        }

        private void Rap_ListepagesaBankaMM_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
        }
    }
}
