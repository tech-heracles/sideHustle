using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Resources;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.ListPagesat.Raportet
{
    public partial class Rap_KartelaPunonjesve_A5_SePDeFn_131305212231929495 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_KartelaPunonjesve_A5_SePDeFn_131305212231929495() { InitializeComponent(); }
        public Rap_KartelaPunonjesve_A5_SePDeFn_131305212231929495(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_KartelaPunonjesve_A5_SePDeFn_131305212231929495(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            Band.PageBreak = DevExpress.XtraReports.UI.PageBreak.AfterBand;
        }


        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));


            xrLabel12.Text = rm.GetString("RaportKartelaPunonjësveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell7.Text = rm.GetString("filterKodDep", ci);
            xrTableCell9.Text = rm.GetString("filterKodNenDep", ci);
            xrTableCell11.Text = rm.GetString("filterNrPersonalPunonjesi", ci);
            xrTableCell19.Text = rm.GetString("labelRaportListepagesa", ci);
            xrTableCell20.Text = rm.GetString("labelRaportPAGESA", ci);
            xrTableCell23.Text = rm.GetString("labelRaportNDALESA", ci);
            xrTableCell29.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell24.Text = rm.GetString("labelFilterKryesorMuaji", ci);
            xrTableCell30.Text = rm.GetString("labelRaportKomponentet", ci);
            xrTableCell25.Text = rm.GetString("labelVlefta", ci);
            xrTableCell26.Text = rm.GetString("labelRaportKomponentet", ci);
            xrTableCell27.Text = rm.GetString("labelVlefta", ci);
            xrTableCell28.Text = rm.GetString("labelRaportPagaNeto", ci);
            xrTableCell36.Text = rm.GetString("labelRaportShumaPagesa", ci);
            xrTableCell41.Text = rm.GetString("labelRaportiShuma", ci);
            xrTableCell48.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell38.Text = rm.GetString("labelRaportShumaNdalesa", ci);
            xrTableCell15.Text = rm.GetString("labelRaportiNrSigShoq", ci) + ":";
            xrTableCell17.Text = rm.GetString("labelRaportiLlogBankare", ci) + ":";


        }
    }
}
