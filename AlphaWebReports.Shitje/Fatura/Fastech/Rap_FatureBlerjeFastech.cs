using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura.Fastech
         
{
    public partial class Rap_FatureBlerjeFastech : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureBlerjeFastech(){InitializeComponent();}
        
        //double totalipatvsh = 0;
        //double totalitvsh = 0;
        //double totalimetvsh = 0;
        //double totalipatvshkursi = 0;
        //double totalitvshkursi = 0;
        //double totalimetvshkursi = 0;
        public Rap_FatureBlerjeFastech(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureBlerjeFastech(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }


        //private void xrLabel37_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        //{
        //    e.Result = totalipatvsh - Convert.ToDouble(GetCurrentColumnValue("ZBRITJE")) - Convert.ToDouble(GetCurrentColumnValue("TVSHTOTAL")); ;
        //    e.Handled = true;
        //}

        //private void xrLabel37_SummaryReset(object sender, EventArgs e)
        //{
        //    totalipatvsh = 0;
        //}

        //private void xrLabel37_SummaryRowChanged(object sender, EventArgs e)
        //{
        //    totalipatvsh += Convert.ToDouble(GetCurrentColumnValue("VLEFTAMETVSH"));
        //}

        //private void xrLabel38_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        //{
        //    e.Result = Convert.ToDouble(GetCurrentColumnValue("TVSHTOTAL"));
        //    e.Handled = true;
        //}

        //private void xrLabel38_SummaryReset(object sender, EventArgs e)
        //{
        //    totalitvsh = 0;
        //}

        //private void xrLabel39_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        //{
        //    e.Result = totalimetvsh - Convert.ToDouble(GetCurrentColumnValue("ZBRITJE")); ;
        //    e.Handled = true;
        //}

        //private void xrLabel39_SummaryReset(object sender, EventArgs e)
        //{
        //    totalimetvsh = 0;
        //}

        //private void xrLabel39_SummaryRowChanged(object sender, EventArgs e)
        //{
        //    totalimetvsh += Convert.ToDouble(GetCurrentColumnValue("VLEFTAMETVSH"));

        //}

        //private void xrLabel41_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        //{
        //    e.Result = (totalipatvshkursi - Convert.ToDouble(GetCurrentColumnValue("ZBRITJE")) - Convert.ToDouble(GetCurrentColumnValue("TVSHTOTAL"))) * Convert.ToDouble(GetCurrentColumnValue("KURSI"));
        //    e.Handled = true;
        //}

        //private void xrLabel41_SummaryReset(object sender, EventArgs e)
        //{
        //    totalipatvshkursi = 0;
        //}

        //private void xrLabel41_SummaryRowChanged(object sender, EventArgs e)
        //{
        //    totalipatvshkursi += Convert.ToDouble(GetCurrentColumnValue("VLEFTAMETVSH"));

        //}

        //private void xrLabel42_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        //{
        //    e.Result = Convert.ToDouble(GetCurrentColumnValue("TVSHTOTAL")) * Convert.ToDouble(GetCurrentColumnValue("KURSI"));
        //    e.Handled = true;
        //}

        //private void xrLabel42_SummaryReset(object sender, EventArgs e)
        //{
        //    totalitvshkursi = 0;
        //}

        //private void xrLabel42_SummaryRowChanged(object sender, EventArgs e)
        //{
        //}

        //private void xrLabel43_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        //{
        //    e.Result = (totalimetvshkursi - Convert.ToDouble(GetCurrentColumnValue("ZBRITJE"))) * Convert.ToDouble(GetCurrentColumnValue("KURSI"));
        //    e.Handled = true;
        //}

        //private void xrLabel43_SummaryReset(object sender, EventArgs e)
        //{
        //    totalimetvshkursi = 0;
        //}

        //private void xrLabel43_SummaryRowChanged(object sender, EventArgs e)
        //{
        //    totalimetvshkursi += Convert.ToDouble(GetCurrentColumnValue("VLEFTAMETVSH"));

        //}

        //private void xrLabel38_SummaryRowChanged(object sender, EventArgs e)
        //{

        //}

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

            xrLabel1.Text = rm.GetString("RaportFatureBlerjeTitulli", ci);
            xrLabel6.Text = rm.GetString("labelRaportNumriFatures", ci) + ":";
            xrLabel8.Text = rm.GetString("labelNumriSerise", ci) + ":";
            xrLabel7.Text = rm.GetString("labelRaportDateFature", ci);
            xrLabel41.Text = rm.GetString("labelNumriPorosise", ci);
            xrLabel4.Text = rm.GetString("labelBleresiUpperCase", ci) + ":";
            xrLabel2.Text = rm.GetString("labelShitesiUpperCase", ci) + ":";
            xrLabel43.Text = rm.GetString("labelRaportTelFax", ci);
            xrLabel50.Text = rm.GetString("labelRaportTelFax", ci);
            xrLabel12.Text = rm.GetString("labelNIPT", ci) + ":";
            xrLabel44.Text = rm.GetString("labelNIPT", ci) + ":";

            xrTableCell17.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell4.Text = rm.GetString("labelKartela", ci);
            xrTableCell18.Text = rm.GetString("labelBarkodi", ci);
            xrTableCell5.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell7.Text = rm.GetString("labelNjesia", ci);
            xrTableCell9.Text = rm.GetString("labelSasia", ci);
            xrTableCell10.Text = rm.GetString("labelRaportCmimiPaTVSH", ci);
            xrTableCell8.Text = rm.GetString("labelVleftapaTVSH", ci);
            xrTableCell11.Text = rm.GetString("labelTVSH", ci);
            xrTableCell6.Text = rm.GetString("labelVleftameTVSH", ci);

            xrLabel14.Text = rm.GetString("labelFilterAvancuarMonedha", ci);

            xrLabel24.Text = rm.GetString("labelTotaliUpperCase", ci);
            xrLabel25.Text = rm.GetString("labelKursi", ci) + ":";
            xrLabel19.Text = rm.GetString("labelTotaliLekUpperCase", ci);

            xrLabel37.Text = rm.GetString("lblShitesKrijues", ci) + ":";
            xrLabel38.Text = rm.GetString("labelBleresi", ci) + ":";

            xrLabel39.Text = rm.GetString("labelShenim", ci) + ":";
            xrLabel40.Text = rm.GetString("labelDokumentiNukPerbenFatureTatimore", ci) + ":";
          }

        private void xrLabel78_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void Rap_FatureBlerjeFastech_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);

        }
    }
}
