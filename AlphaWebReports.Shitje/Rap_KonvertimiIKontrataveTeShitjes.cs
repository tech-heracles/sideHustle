using System.Globalization;
using System.Resources;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_KonvertimiIKontrataveTeShitjes : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_KonvertimiIKontrataveTeShitjes(){InitializeComponent();} 

        public Rap_KonvertimiIKontrataveTeShitjes(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report)
        {

        }
        public Rap_KonvertimiIKontrataveTeShitjes(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter5.Value = raport.Parameters[5].Value;
        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            LlojiKoka.Text = rm.GetString("labelRaportiLloji", ci);
            NrKoka.Text = rm.GetString("labelRaportiNr", ci);
            DtDokKoka.Text = rm.GetString("labelRaportiDtDok", ci);
            PershkrimiKoka.Text = rm.GetString("cmbCmimeArtikulliCaptionPershkrimi", ci);
            VlMbeturKoka.Text = rm.GetString("labelVleraMbetur", ci);
            MonKoka.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            AfatiKohKoka.Text = rm.GetString("labelRaportAfatiKohor", ci);
            KlientiKoka.Text = rm.GetString("labelRaportKlienti", ci);
            TotaliKoka.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel21.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel63.Text = rm.GetString("lblTitulliRaportKonvertimiKontrataveShitjes", ci);
        }

    }
}
