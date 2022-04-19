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
    public partial class Rap_PasqyraSigurimeveTatimeve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_PasqyraSigurimeveTatimeve(){InitializeComponent();} 
        private CultureInfo ci;
        private ResourceManager rm = new ResourceManager("Resources.Strings",
                          System.Reflection.Assembly.Load("App_GlobalResources"));
        public Rap_PasqyraSigurimeveTatimeve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_PasqyraSigurimeveTatimeve(CultureInfo ci, int idNdermarrje, int idViti, XtraReport report)
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
        }

        private void nrFaqes_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            int nrCurrent = e.PageIndex + 1;
            int nrTotal = e.PageCount;
            nrFaqes.Text = nrCurrent + rm.GetString("labelRaportNgaLowCase", ci) + nrTotal;
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

        private void xrLabel48_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

            xrLabel66.Text = rm.GetString("RaportListpgKontributetSigShoqTitulli", ci);

            xrLabel65.Text = rm.GetString("labelRaportFormularNr", ci);
            xrLabel40.Text = rm.GetString("labelRaportiDetyraFunksioni", ci);
            xrLabel21.Text = "1)" + rm.GetString("labelRaportFaqja", ci);
            xrLabel41.Text = "2)" + rm.GetString("labelNipti", ci) + ":";
            xrLabel70.Text = "3)" + rm.GetString("labelRaportEmriTatimpaguesit", ci);
            xrLabel71.Text = "3)" + rm.GetString("labelRaportPeriudhaTatimore", ci) + ":";
            xrLabel72.Text = rm.GetString("filterMuaji", ci);
            xrLabel73.Text = "4)" + rm.GetString("labelRrethi", ci) + ":";
            xrLabel69.Text = "2)" + rm.GetString("labelRaportStatusiTatimpaguesit", ci) + ":";
            xrLabel80.Text = rm.GetString("labelRaportShoqeriPergjKufiz", ci);
            xrLabel81.Text = rm.GetString("labelRaportVeprimtariaKryesore", ci);
            xrLabel82.Text = rm.GetString("labelRaportTregti", ci);
            xrLabel83.Text = rm.GetString("labelRaportVeprimtariaDegesNjesise", ci) + ":";
            xrLabel41.Text = rm.GetString("labelRaportiNr", ci);
            xrLabel37.Text = "5)" + rm.GetString("labelRaportNrSigShoq", ci);
            xrLabel39.Text = "6)" + rm.GetString("labelEmerMbiemer", ci);
            xrLabel30.Text = "7)" + rm.GetString("labelRaportNrKatPunemarresiSigShoq", ci);
            xrLabel15.Text = "8)" + rm.GetString("labelRaportDiteKalendarikePaPunuar", ci);
            xrLabel16.Text = "9)" + rm.GetString("labelRaportDiteKalendarikePunuar", ci);
            xrLabel47.Text = rm.GetString("labelRaportPagaBruto", ci);
             xrLabel17.Text = rm.GetString("labelRaportKontrbutetSigShoq", ci);
            xrLabel34.Text = "10)" + rm.GetString("labelGjithsej", ci);
            xrLabel35.Text = "11)" + rm.GetString("labelRaportMbiCilenLlogKontrib", ci);
            xrLabel42.Text = "12)" + rm.GetString("labelGjithsej", ci) + " = (14 + 15 + 16)";
            xrLabel18.Text = "13)" + rm.GetString("labelRaportPunedhenesi", ci) ;
            xrLabel19.Text = "14)" + rm.GetString("labelRaportPunemarresi", ci);
            xrLabel20.Text = "15)" + rm.GetString("labelRaportKontribShtese", ci);
            xrLabel49.Text = "16)" + rm.GetString("labelRaportKontrSigShendet", ci);
            xrLabel44.Text = "17)" + rm.GetString("labelRaportPagBrutoTatimi", ci);
            xrLabel46.Text = "18)" + rm.GetString("labelRaportTatimArdhuratPunesimi", ci);
            xrLabel1.Text = rm.GetString("labelRaportShumaFaqes", ci);
            xrLabel14.Text = rm.GetString("labelRaportShumaMbartur", ci);
            xrLabel88.Text = rm.GetString("labelRaportTotaliListePageses", ci);
            xrLabel96.Text =  rm.GetString("labelRaportDeklaroj", ci);
            xrLabel97.Text = "19)" + rm.GetString("labelRaportListepagesaTAP", ci);
            xrLabel98.Text = rm.GetString("labelRaportPerfaqPersonitJuridik", ci);
            xrLabel99.Text =rm.GetString("labelRaportPersJuridikVule", ci);
            xrLabel100.Text = rm.GetString("labelRaportListepageseProtokollZSHT", ci);
            xrLabel101.Text = rm.GetString("labelRaportListepageseProtokollDRT", ci);
            xrLabel103.Text = rm.GetString("labelRaportEmerMbiemerFirmeFq1", ci);
            xrLabel105.Text = rm.GetString("labelRaportEmerMbiemerFirmeFq1", ci);
             xrLabel106.Text = rm.GetString("labelRaportVertetoj", ci);
            xrLabel102.Text = rm.GetString("labelRaportListepagesaInspektori", ci);
            xrLabel108.Text = rm.GetString("labelRaportEmerMbiemerFirmeFq1", ci);




        }

        private void Rap_PasqyraSigurimeveTatimeve_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);
            NdermarjeNipti.Text = parametraRaporti.NdermarrjeNipt;
            Rrethi.Text = parametraRaporti.NdermarrjeQytetiPershkrimi;
            PeriudhaViti.Text = rm.GetString("labelViti", ci) + parametraRaporti.KodiViti;
        }
    }
}

