using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Blerje
{
    public partial class Rap_UrdherBlerjeMeArtikuj : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_UrdherBlerjeMeArtikuj(){InitializeComponent();} 
        double totalipatvsh = 0;
        double totalimetvsh = 0;
        double totalipatvshkursi = 0;
        double totalimetvshkursi = 0;
        

        public Rap_UrdherBlerjeMeArtikuj(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_UrdherBlerjeMeArtikuj(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }
 
        private void xrLabel37_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = totalipatvsh - Convert.ToDouble(GetCurrentColumnValue("ZBRITJE")) - Convert.ToDouble(GetCurrentColumnValue("TVSHTOTAL")); ;
            e.Handled = true;
        }

        private void xrLabel37_SummaryReset(object sender, EventArgs e)
        {
            totalipatvsh = 0;
        }

        private void xrLabel37_SummaryRowChanged(object sender, EventArgs e)
        {
            totalipatvsh += Convert.ToDouble(GetCurrentColumnValue("VLEFTAMETVSH"));
        }

        private void xrLabel38_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = Convert.ToDouble(GetCurrentColumnValue("TVSHTOTAL"));
            e.Handled = true;
        }

        private void xrLabel38_SummaryRowChanged(object sender, EventArgs e)
        {
        }

        private void xrLabel39_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = totalimetvsh - Convert.ToDouble(GetCurrentColumnValue("ZBRITJE")); ;
            e.Handled = true;
        }

        private void xrLabel39_SummaryReset(object sender, EventArgs e)
        {
            totalimetvsh = 0;
        }

        private void xrLabel39_SummaryRowChanged(object sender, EventArgs e)
        {
            totalimetvsh += Convert.ToDouble(GetCurrentColumnValue("VLEFTAMETVSH"));

        }

        private void xrLabel41_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = (totalipatvshkursi - Convert.ToDouble(GetCurrentColumnValue("ZBRITJE")) - Convert.ToDouble(GetCurrentColumnValue("TVSHTOTAL"))) * Convert.ToDouble(GetCurrentColumnValue("KURSI"));
            e.Handled = true;
        }

        private void xrLabel41_SummaryReset(object sender, EventArgs e)
        {
            totalipatvshkursi = 0;
        }

        private void xrLabel41_SummaryRowChanged(object sender, EventArgs e)
        {
            totalipatvshkursi += Convert.ToDouble(GetCurrentColumnValue("VLEFTAMETVSH"));

        }

        private void xrLabel42_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = Convert.ToDouble(GetCurrentColumnValue("TVSHTOTAL")) * Convert.ToDouble(GetCurrentColumnValue("KURSI"));
            e.Handled = true;
        }

     

        private void xrLabel42_SummaryRowChanged(object sender, EventArgs e)
        {
        }

        private void xrLabel43_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = (totalimetvshkursi - Convert.ToDouble(GetCurrentColumnValue("ZBRITJE"))) * Convert.ToDouble(GetCurrentColumnValue("KURSI"));
            e.Handled = true;
        }

        private void xrLabel43_SummaryReset(object sender, EventArgs e)
        {
            totalimetvshkursi = 0;
        }

        private void xrLabel43_SummaryRowChanged(object sender, EventArgs e)
        {
            totalimetvshkursi += Convert.ToDouble(GetCurrentColumnValue("VLEFTAMETVSH"));
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
            xrLabel1.Text = rm.GetString("RaportUrdherBlerjeTitulli", ci);

            xrLabel2.Text = rm.GetString("labelRaportSubjektBleres", ci) + ":";
            xrLabel4.Text = rm.GetString("labelRaportSubjektShites", ci) + ":";
            xrLabel12.Text = rm.GetString("labelNIPT", ci);
            xrLabel6.Text = rm.GetString("labelRaportNrFature", ci);
            xrLabel13.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel7.Text = rm.GetString("labelRaportDateFature", ci);
            xrLabel14.Text = rm.GetString("labelRaportTelFax", ci);
            xrLabel8.Text = rm.GetString("labelRaportNrSerial", ci);
            xrLabel15.Text = rm.GetString("labelRaportLlogariBankare", ci);
            xrTableCell4.Text = rm.GetString("labelKodi", ci);
            xrTableCell5.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell7.Text = rm.GetString("labelNjesia", ci);
            xrTableCell9.Text = rm.GetString("labelSasia", ci);
            xrTableCell10.Text = rm.GetString("labelCmimi", ci);
            xrTableCell8.Text = rm.GetString("labelVleftapaTVSH", ci);
            xrTableCell11.Text = rm.GetString("labelTVSH", ci);
            xrTableCell6.Text = rm.GetString("labelVleftameTVSH", ci);

            xrLabel24.Text = rm.GetString("labelRaportTotaliBrutoNe", ci);
            xrLabel25.Text = rm.GetString("labelKursi", ci) + ":";
            xrLabel26.Text = rm.GetString("labelRaportTotalNeto", ci);
 
        }

        private void Rap_UrdherBlerjeMeArtikuj_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);

        }
    }
}
