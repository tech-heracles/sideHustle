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
    public partial class Rap_ListaPunonjeveMePagatLandscape : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ListaPunonjeveMePagatLandscape(){InitializeComponent();}
        
        public Rap_ListaPunonjeveMePagatLandscape(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_ListaPunonjeveMePagatLandscape(CultureInfo ci, int idNdermarrje, int idViti, XtraReport raporti)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            dataLabel.Text = raporti.Parameters[1].Description;
            Data.Value = raporti.Parameters[1].Value;
            ndermarrjaLabel.Text = raporti.Parameters[0].Description;
            Ndermarja.Value = raporti.Parameters[0].Value;
            DepartamentiLabel.Text = raporti.Parameters[3].Description;
            Departamenti.Value = raporti.Parameters[3].Value;
            nendepartamentiLabel.Text = raporti.Parameters[4].Description;
            Nendepartamenti.Value = raporti.Parameters[4].Value;
            punonjesiLabel.Text = raporti.Parameters[2].Description;
            KodiPunonjesit.Value = raporti.Parameters[2].Value;
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


            xrLabel12.Text = rm.GetString("RaportListaPunonjësveMePagatTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel20.Text = rm.GetString("filterKodDep", ci);
            xrLabel21.Text = rm.GetString("filterKodNenDep", ci);
            xrLabel41.Text = rm.GetString("labelRaportNrPersonal", ci);
            xrLabel11.Text = rm.GetString("labelRaportEmri", ci);
            xrLabel45.Text = rm.GetString("labelRaportAtesia", ci);
            xrLabel13.Text = rm.GetString("labelRaportMbiemri", ci);
            xrLabel14.Text = rm.GetString("labelRaportNrSigurimeve", ci);
            xrLabel47.Text = rm.GetString("labelRaportDetyra", ci);
            xrLabel15.Text = rm.GetString("labelRaportLlojPagese", ci);
            xrLabel16.Text = rm.GetString("labelRaportMon", ci);
            xrLabel49.Text = rm.GetString("labelRaportPagaMujore", ci);
            xrLabel17.Text = rm.GetString("labelRaportAktiv", ci);
            xrLabel18.Text = rm.GetString("lblRaportDatelindja", ci);
            xrLabel19.Text = rm.GetString("labelQyteti", ci);
            xrLabel24.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel25.Text = rm.GetString("labelRaportTel", ci);
            xrLabel26.Text = rm.GetString("labelRaportEmail", ci);
            xrLabel27.Text = rm.GetString("lblRaportTipKontrate", ci);
        }

        private void Rap_ListaPunonjeveMePagatLandscape_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
        }
    }
}
