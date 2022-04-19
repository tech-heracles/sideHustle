using System;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_GjendjaMagDet_Vodafone : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_GjendjaMagDet_Vodafone(){InitializeComponent();} 


        public Rap_GjendjaMagDet_Vodafone(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, report)
        {

        }
        public Rap_GjendjaMagDet_Vodafone(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
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
            var rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));


            xrLabel12.Text = rm.GetString("RaportGjendjaMagazinesDetajimeArtikulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell7.Text = rm.GetString("labelKartela", ci);
            xrTableCell9.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell17.Text = rm.GetString("labelFooterNdermarrja", ci);
            xrTableCell10.Text = rm.GetString("labelRaportDetajimi", ci);
            xrTableCell8.Text = rm.GetString("labelFilterAvancuarKodbari", ci);

            xrTableCell11.Text = rm.GetString("labelRaportGjendjaeMeparshme", ci);
            xrTableCell12.Text = rm.GetString("labelRaportSasiHyrje", ci);
            xrTableCell13.Text = rm.GetString("labelRaportSasiaDalje", ci);
            xrTableCell14.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
            xrTableCell15.Text = rm.GetString("labelRaportKosto", ci);
            xrTableCell16.Text = rm.GetString("labelVlefta", ci);
            xrTableCell18.Text = rm.GetString("filterMagazina", ci);
            TotaliGjithMAgazinave.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell33.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel70.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell32.Text = rm.GetString("lblRaportMagazina", ci);
            xrTableCell31.Text = rm.GetString("labelFooterNdermarrja", ci);
        }
    }
}
