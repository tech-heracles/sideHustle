using DevExpress.XtraReports.UI;
using System;
using System.Globalization;
using System.Resources;
namespace AlphaWebReports.RaportetDs.KlientFurnitor.Raporte
{
    public partial class Rap_SituacionIKlienteveSipasAfateveMaturimit : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_SituacionIKlienteveSipasAfateveMaturimit(){InitializeComponent();} 


        public Rap_SituacionIKlienteveSipasAfateveMaturimit(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, report)
        {

        }
        public Rap_SituacionIKlienteveSipasAfateveMaturimit(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
        {
          
            InitializeComponent();
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            EmrateLabelave(ci, rm);

        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        /// <param name="rm"></param>

        private void EmrateLabelave(CultureInfo ci, ResourceManager rm)
        {
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel20.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell30.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell18.Text = rm.GetString("labelNrRendor", ci);
            xrTableCell19.Text = rm.GetString("labelKodi", ci);
            xrTableCell21.Text = rm.GetString("labelRaportEmertimiKlientit", ci);
            xrTableCell20.Text = rm.GetString("labelRaportiNrLlogari", ci);
            xrTableCell23.Text = rm.GetString("labelRaportShumaDebi", ci);
            xrTableCell24.Text = rm.GetString("labelRaportShumaKredi", ci);
            xrTableCell25.Text = rm.GetString("labelRaportDetyrimi", ci);
            xrTableCell26.Text = rm.GetString("labelRaportLimiti", ci);
            xrTableCell27.Text = rm.GetString("labelRaportPesha", ci);
        }
     

    }
}
