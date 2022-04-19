using System;
using System.Globalization;
using System.Resources;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs.KlientFurnitor.Raporte
{
    public partial class Rap_SituacioniPermbledhesKlienteve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_SituacioniPermbledhesKlienteve(){InitializeComponent();} 
   
        public Rap_SituacioniPermbledhesKlienteve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, report)
        {

        }
        public Rap_SituacioniPermbledhesKlienteve(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
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
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            xrTableCell45.Text = rm.GetString("lblNrUpperCase", ci);
            xrTableCell40.Text = rm.GetString("labelRaportiSubjekti", ci);
            xrTableCell41.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrTableCell42.Text = rm.GetString("lblGjendjaFillimPeriudhes", ci);
            xrTableCell51.Text = rm.GetString("lblDebiUpperCase", ci);
            xrTableCell48.Text = rm.GetString("lblKrediUpperCase", ci);
            xrTableCell44.Text = rm.GetString("lblLevizjeGjatePeriudhes", ci);
            xrTableCell52.Text = rm.GetString("lblDebiUpperCase", ci);
            xrTableCell49.Text = rm.GetString("lblKrediUpperCase", ci);
            xrTableCell43.Text = rm.GetString("lblMbetjaDebitore", ci);
            xrLabel12.Text = rm.GetString("lblTitRapSituacioniPermbledhesKL", ci);
            xrTableCell1.Text = rm.GetString("llogariaTab", ci);
            xrTableCell28.Text = rm.GetString("labelRaportTotal", ci);
            xrTableCell19.Text = rm.GetString("labelRaportTotaluUppercase", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel20.Text = rm.GetString("labelLogoIMB", ci);

        }

    }
}
