using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Banka.Raporte
{
    public partial class Rap_Permbledhese_UrdherPagesave : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Permbledhese_UrdherPagesave(){InitializeComponent();} 
        public Rap_Permbledhese_UrdherPagesave(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, report)
        {

        }

        public Rap_Permbledhese_UrdherPagesave(CultureInfo ci, XtraReport report)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            Ndermarja.Value = report.Parameters[0].Value;
            NrDok.Value = report.Parameters[1].Value;
            NrAprovimi.Value = report.Parameters[2].Value;
            Seriali.Value = report.Parameters[3].Value;
            Llogaria.Value = report.Parameters[4].Value;
            NenLlogaria.Value = report.Parameters[5].Value;
            DtDok.Value = report.Parameters[6].Value;
            DtAprovimi.Value = report.Parameters[7].Value;
        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportPermbledheseUrdherPagesaveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell7.Text = rm.GetString("labelFilterAvancuarLlogEkonomike", ci);
            xrTableCell8.Text = rm.GetString("labelRaportNenllogaria", ci);
            xrTableCell10.Text = rm.GetString("labelRaportiEmertimi", ci);
            xrTableCell11.Text = rm.GetString("labelRaportiShuma", ci);
            xrTableCell9.Text = rm.GetString("labelRaportPlanifikuar", ci);
            xrTableCell12.Text = rm.GetString("labelFilterAvancuarGrupi", ci) + ":";
            xrTableCell16.Text = rm.GetString("labelRaportTitulli", ci) + ":";
            xrTableCell17.Text = rm.GetString("labelRaportKapitulli", ci) + ":";
            xrTableCell14.Text = rm.GetString("labelRaportTotaliSipasArtikullit", ci);
            xrTableCell19.Text = rm.GetString("labelRaporttTotaliSipasKapitullit", ci);
            xrTableCell18.Text = rm.GetString("labelRaporttTotaliSipasTitullit", ci);
            xrTableCell20.Text = rm.GetString("labelRaporttTotaliSipasGrupit", ci);
            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
        }

    }
}
