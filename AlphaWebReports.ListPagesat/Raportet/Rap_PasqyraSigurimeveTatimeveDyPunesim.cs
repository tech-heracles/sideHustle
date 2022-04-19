using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using DevExpress.XtraReports.Web;	
namespace AlphaWebReports.RaportetDs.ListPagesat.Raportet
{
    public partial class Rap_PasqyraSigurimeveTatimeveDyPunesim : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_PasqyraSigurimeveTatimeveDyPunesim(){InitializeComponent();} 
        private CultureInfo ci;
        int shifraPasPresjes = 0;
        private ResourceManager rm = new ResourceManager("Resources.Strings",
                          System.Reflection.Assembly.Load("App_GlobalResources"));
        public Rap_PasqyraSigurimeveTatimeveDyPunesim(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_PasqyraSigurimeveTatimeveDyPunesim(CultureInfo ci, int idNdermarrje, int idViti, XtraReport report)
        {
            this.ci = ci;
           
            InitializeComponent();
            EmrateLabelave(ci);
          
            Monedha.Value = report.Parameters[1].Value;
            Ndermarja.Value = report.Parameters[0].Value;
            NdermarjaEmri.Text = report.Parameters[0].Value.ToString();
            Departamenti.Value = report.Parameters[4].Value;
            Nendepartamenti.Value = report.Parameters[5].Value;
            NrPunonjesit.Value = report.Parameters[3].Value;
            PeriudhaMuaji.Text = report.Parameters["filterMuaji"].Value.ToString();
            shifraPasPresjes = Convert.ToInt16(report.Parameters["filterFormatNumri"].Value);
            caktoFormatinENumrave();
        }
        private void caktoFormatinENumrave()
        {
           
            xrTableCell3.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell3.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell17.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell17.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell15.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell15.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell10.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell10.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell8.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell8.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell9.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell9.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell16.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell16.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell14.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell14.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell20.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell20.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell21.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell21.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell18.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell18.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell19.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell19.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell28.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell28.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell28.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell27.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell27.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell27.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell29.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell29.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell29.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell23.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell23.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell23.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell26.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell26.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell26.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell25.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell25.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell25.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell30.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell30.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell30.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell33.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell33.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell33.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell31.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell31.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell31.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell32.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell32.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell32.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell34.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell34.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell34.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell35.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell35.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell35.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel31.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrLabel31.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel31.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";

        }


        //int i = 1;
        double runnPagaFaktike = 0;
        double sumPagePagaFaktike = 0;
        double runnPagaMeKufi = 0;
        double sumPagePagaMeKufi = 0;
        double runGjithesej = 0;
        double sumaPageGjithesej = 0;
        double runnPunedhenesi = 0;
        double sumPagePunedhenesi = 0;
        double runPunemarresi = 0;
        double sumPagePunemarresi = 0;
        double runnSigShend = 0;
        double sumPageSigShend = 0;
        double runPagaTatim = 0;
        double sumPagePagaTatim = 0;
        double runnTaksePage = 0;
        double sumPageTaksePage = 0;

        private void xrTableCell13_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //xrLabel48.Text = Convert.ToString(i++);
        }

        /// <summary>
        /// Running sum : Paga faktike
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xrLabel13_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (e.PageIndex == 0)
            {
                runnPagaFaktike = 0;
                (sender as XRLabel).Text = String.Format(Convert.ToString(runnPagaFaktike), "{0:#.00}");
                return;
            }

            runnPagaFaktike += sumPagePagaFaktike;
            (sender as XRLabel).Text = String.Format(Convert.ToString(runnPagaFaktike), "{0:#.00}");
        }

        private void xrLabel2_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            sumPagePagaFaktike = Convert.ToDouble(e.Value);
        }

        /// <summary>
        /// Runninga sum : paga me kufi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xrLabel74_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (e.PageIndex == 0)
            {
                runnPagaMeKufi = 0;
                (sender as XRLabel).Text = String.Format(Convert.ToString(runnPagaMeKufi), "{0:#.00}");
                return;
            }

            runnPagaMeKufi += sumPagePagaMeKufi;
            (sender as XRLabel).Text = String.Format(Convert.ToString(runnPagaMeKufi), "{0:#.00}");
        }

        private void xrLabel3_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            sumPagePagaMeKufi = Convert.ToDouble(e.Value);
        }

        /// <summary>
        /// Running sum : Gjithesej
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xrLabel11_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (e.PageIndex == 0)
            {
                runGjithesej = 0;
                (sender as XRLabel).Text = String.Format(Convert.ToString(runGjithesej), "{0:#.00}");
                return;
            }

            runGjithesej += sumaPageGjithesej;
            (sender as XRLabel).Text = String.Format(Convert.ToString(runGjithesej), "{0:#.00}");
        }

        private void xrLabel4_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            sumaPageGjithesej = Convert.ToDouble(e.Value);
        }


        /// <summary>
        /// Running sum : Punedhenesi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xrLabel12_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (e.PageIndex == 0)
            {
                runnPunedhenesi = 0;
                (sender as XRLabel).Text = String.Format(Convert.ToString(runnPunedhenesi), "{0:#.00}");
                return;
            }

            runnPunedhenesi += sumPagePunedhenesi;
            (sender as XRLabel).Text = String.Format(Convert.ToString(runnPunedhenesi), "{0:#.00}");
        }

        private void xrLabel5_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            sumPagePunedhenesi = Convert.ToDouble(e.Value);
        }

        /// <summary>
        /// Running sum : Punemarresi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xrLabel75_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (e.PageIndex == 0)
            {
                runPunemarresi = 0;
                (sender as XRLabel).Text = String.Format(Convert.ToString(runPunemarresi), "{0:#.00}");
                return;
            }

            runPunemarresi += sumPagePunemarresi;
            (sender as XRLabel).Text = String.Format(Convert.ToString(runPunemarresi), "{0:#.00}");
        }

        private void xrLabel6_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            sumPagePunemarresi = Convert.ToDouble(e.Value);
        }

        /// <summary>
        /// Running sum : Sig Shendetesore
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xrLabel79_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (e.PageIndex == 0)
            {
                runnSigShend = 0;
                (sender as XRLabel).Text = String.Format(Convert.ToString(runnSigShend), "{0:#.00}");
                return;
            }

            runnSigShend += sumPageSigShend;
            (sender as XRLabel).Text = String.Format(Convert.ToString(runnSigShend), "{0:#.00}");
        }

        private void xrLabel8_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            sumPageSigShend = Convert.ToDouble(e.Value);
        }

        /// <summary>
        /// Running sum : Paga tatim
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xrLabel76_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (e.PageIndex == 0)
            {
                runPagaTatim = 0;
                (sender as XRLabel).Text = String.Format(Convert.ToString(runPagaTatim), "{0:#.00}");
                return;
            }

            runPagaTatim += sumPagePagaTatim;
            (sender as XRLabel).Text = String.Format(Convert.ToString(runPagaTatim), "{0:#.00}");
        }

        private void xrLabel9_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            sumPagePagaTatim = Convert.ToDouble(e.Value);
        }

        /// <summary>
        /// Running sum : Takse Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xrLabel10_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            sumPageTaksePage = Convert.ToDouble(e.Value);
        }

        private void xrLabel77_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            if (e.PageIndex == 0)
            {
                runnTaksePage = 0;
                (sender as XRLabel).Text = String.Format(Convert.ToString(runnTaksePage), "{0:#.00}");
                return;
            }

            runnTaksePage += sumPageTaksePage;
            (sender as XRLabel).Text = String.Format(Convert.ToString(runnTaksePage), "{0:#.00}");
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>

        private void EmrateLabelave(CultureInfo ci)
        {

            xrLabel66.Text = rm.GetString("DeklarataListpgKontributetSigShoq", ci);
            xrLabel68.Text = "1)" + rm.GetString("labelAdministrimiNIPT", ci) ;
            xrLabel70.Text = "2)" + rm.GetString("labelRaportEmriTatimpaguesit", ci)+ ":";
            xrLabel71.Text = "3)" + rm.GetString("labelRaportPeriudhaTatimore", ci) + ":";
            xrLabel72.Text = rm.GetString("filterMuaji", ci);
            xrLabel69.Text = "4)" + rm.GetString("lblPeriodicitetiDeklarimit", ci) + ":";
            xrLabel65.Text = "5)" + rm.GetString("lblAdresaKryesore", ci) + ":";
            xrLabel81.Text = "6)" + rm.GetString("labelRaportVeprimtariaKryesore", ci) + ":";
            xrLabel82.Text = rm.GetString("labelRaportTregti", ci);
            xrLabel83.Text = "7)" + rm.GetString("labelRaportVeprimtariaDegesNjesise",ci) + ":";
            xrLabel73.Text = "8)";
            xrLabel104.Text = rm.GetString("lblPaVeprimtari", ci);
            xrLabel41.Text = rm.GetString("lblNr", ci);
            xrLabel37.Text ="9)"+ rm.GetString("lblNumërPersonal", ci);
            xrLabel39.Text = "10)" + rm.GetString("lblEmriMbiemri", ci);
            xrLabel40.Text = "11)" + rm.GetString("lblDetFunxProfPuna", ci);
            xrLabel30.Text = "12)" + rm.GetString("lblNrKatPerKontribute", ci);
            xrLabel15.Text = "13)" + rm.GetString("lblDiteKalendarikePaPunuar", ci);
            xrLabel16.Text = "14)" + rm.GetString("lblDiteKalendarikePunuar", ci);
            xrLabel34.Text = "15)" + rm.GetString("lblPagaBrutoLEK", ci);
            xrLabel35.Text = "16)" + rm.GetString("lblPagaBrutoPerSigShoq",ci);
            xrLabel17.Text = rm.GetString("lblKontrubutePerSigShoq", ci);
            xrLabel21.Text = rm.GetString("lblNgaKëto", ci)+ ":";
            xrLabel23.Text = rm.GetString("lblKontributeSupl", ci);
            xrLabel1.Text = "17)" + rm.GetString("lblPunedhenesi", ci);
            xrLabel18.Text = "18)" + rm.GetString("lblPunemarresi", ci);
            xrLabel24.Text = "19)" + rm.GetString("lblGjithsej", ci);
            xrLabel3.Text = "20)" + rm.GetString("lblPunedhenesi", ci);
            xrLabel2.Text = "21)" + rm.GetString("lblPunemarresi", ci);
            xrLabel4.Text = "22)" + rm.GetString("lblGjithsej2", ci);
            xrLabel49.Text = "23)" + rm.GetString("lblTotaliSigShoq", ci);
            xrLabel44.Text = "24)" + rm.GetString("lblPagaBrutoMbiKontributetSigShend", ci);
            xrLabel46.Text = "25)" + rm.GetString("lblKontributeSigShend", ci);
            xrLabel26.Text = "26)" + rm.GetString("lblTAP", ci);
            xrLabel27.Text = "27)" + " " + rm.GetString("lblshenimeUPPERCASE", ci);
            xrLabel96.Text = "28)" + rm.GetString("lblDeklarojDhenatListepagese", ci);
            xrLabel97.Text = "29)" + rm.GetString("lblPunonjesDuhetPagKontributeShoq", ci);
            xrLabel29.Text = rm.GetString("lblpersona", ci);
            xrLabel32.Text = rm.GetString("lbllekë", ci);
            xrLabel33.Text = "30)" + rm.GetString("lblAdministratori", ci);
            xrLabel36.Text = "(" + rm.GetString("lblemermbiemer", ci) + ")";
            xrLabel38.Text = "31)" + rm.GetString("lblDeklaruesi", ci);
            xrLabel43.Text = rm.GetString("lblemermbiemernenshkrimi", ci);
            xrLabel80.Text = rm.GetString("labelRaportMujore", ci);





          



        }

        private void xrTableCell22_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCell22.BackColor = System.Drawing.Color.Black;

        }

        private void Rap_PasqyraSigurimeveTatimeveDyPunesim_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);
            NdermarjeNipti.Text = parametraRaporti.NdermarrjeNipt;
            xrLabel67.Text = parametraRaporti.NdermarrjeVendi;
            PeriudhaViti.Text = rm.GetString("labelViti", ci) + " " + parametraRaporti.KodiViti;
        }
    }
}

