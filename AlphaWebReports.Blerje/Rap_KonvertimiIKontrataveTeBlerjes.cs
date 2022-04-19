using System.Globalization;
using System.Resources;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs.Blerje

{
    public partial class Rap_KonvertimiIKontrataveTeBlerjes : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_KonvertimiIKontrataveTeBlerjes(){InitializeComponent();} 

        public Rap_KonvertimiIKontrataveTeBlerjes(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report)
        {

        }
        public Rap_KonvertimiIKontrataveTeBlerjes(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
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
            xrLabel1.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel32.Text = rm.GetString("labelRaportiLloji", ci);
            xrLabel3.Text = rm.GetString("labelRaportiNr", ci);
            xrLabel4.Text = rm.GetString("labelRaportiDtDok", ci);
            xrLabel5.Text = rm.GetString("cmbCmimeArtikulliCaptionPershkrimi", ci);
            xrLabel22.Text = rm.GetString("labelVleraMbetur", ci);
            xrLabel9.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrLabel6.Text = rm.GetString("labelRaportAfatiKohor", ci);
            xrLabel10.Text = rm.GetString("labelFurnitori", ci);
            xrLabel24.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel21.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel63.Text = rm.GetString("lblTitulliRaportKonvertimiKontrataveBlerjes", ci);
        }

    }
}
