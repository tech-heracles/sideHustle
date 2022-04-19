using System;
using System.Globalization;
using System.Resources;
using DevExpress.XtraReports.UI;
namespace AlphaWebReports.RaportetDs
{
    public partial class Rap_kontKartelaLlogarive_new : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_kontKartelaLlogarive_new() { InitializeComponent(); }


        public Rap_kontKartelaLlogarive_new(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }

        public Rap_kontKartelaLlogarive_new(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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


            xrLabel17.Text = rm.GetString("RaportKartelaLlogariveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell7.Text = rm.GetString("labelNrRef", ci);
            xrTableCell10.Text = rm.GetString("FilterDateRegjistrimi", ci);
            xrTableCell8.Text = rm.GetString("labelRaportiLloji", ci);
            xrTableCell11.Text = rm.GetString("labelRaportiNrDok", ci);
            xrTableCell9.Text = rm.GetString("labelRaportiDtDok", ci);
            xrTableCell12.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell20.Text = rm.GetString("labelRaportiKredi", ci);
            xrTableCell21.Text = rm.GetString("labelRaportiKredi", ci);
            xrTableCell13.Text = rm.GetString("labelRaportiGjendMonBaze", ci);
            xrTableCell22.Text = rm.GetString("labelRaportiKredi", ci);
            xrTableCell24.Text = rm.GetString("labelRaportiDebi", ci);
            xrTableCell23.Text = rm.GetString("labelRaportiDebi", ci);
            xrTableCell26.Text = rm.GetString("labelRaportiGjendjaMePare", ci);
            xrTableCell37.Text = rm.GetString("lblLevizja", ci);
            xrTableCell38.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
            xrTableCell32.Text = rm.GetString("lblLevizjaGjithsej", ci);
            xrTableCell35.Text = rm.GetString("labelRaportiGjendjaGjithsej", ci);
            xrLabel64.Text = rm.GetString("labelLogoIMB", ci);


        }

    }
}
