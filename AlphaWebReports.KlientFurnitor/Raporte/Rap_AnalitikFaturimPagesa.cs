using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.KlientFurnitor.Raporte
{
    public partial class Rap_AnalitikFaturimPagesa : DevExpress.XtraReports.UI.XtraReport
    {

        ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
        CultureInfo ci;
        public Rap_AnalitikFaturimPagesa()
        {
            InitializeComponent();
        }

        public Rap_AnalitikFaturimPagesa(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_AnalitikFaturimPagesa(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            this.ci = ci;
            InitializeComponent();
            EmrateLabelave(ci);
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>

        private void EmrateLabelave(CultureInfo ci)
        {
            xrLabel12.Text = rm.GetString("RaportAnalitikFaturimePagesaTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel33.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell7.Text = rm.GetString("filterMonedha", ci);
            xrTableCell9.Text = rm.GetString("labelRaportiLloji", ci);
            xrTableCell13.Text = rm.GetString("labelFilterAvancuarDok", ci);
            xrTableCell14.Text = rm.GetString("labelVlefta", ci);
            xrTableCell18.Text = rm.GetString("labelRaportiDokumentaQeRrisinDetyrimet", ci);
            xrTableCell15.Text = rm.GetString("labelRaportiDokumentaQeUlinDetyrimet", ci);
            xrTableCell20.Text = rm.GetString("labelRaportNumer", ci);
            xrTableCell19.Text = rm.GetString("labelRaportData", ci);
            xrTableCell21.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell23.Text = rm.GetString("labelRritjeDetyrimi", ci);
            xrTableCell22.Text = rm.GetString("labelUljeDetyrimi", ci);
            xrTableCell17.Text = rm.GetString("labelRaportiDiferenca", ci);
            xrTableCell32.Text = rm.GetString("labelRaportiShuma", ci) + ":";
            xrTableCell31.Text = rm.GetString("labelRaportiBalanca", ci) + ":";
            xrLabel26.Text = rm.GetString("labelFilterKryeAvancKlientFurnitor", ci) + ":";
            xrTableCell16.Text = rm.GetString("labelRaportDateMaturmi", ci);
        }   
    }
}
