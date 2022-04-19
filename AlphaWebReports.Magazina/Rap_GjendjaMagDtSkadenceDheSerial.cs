using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_GjendjaMagDtSkadenceDheSerial : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_GjendjaMagDtSkadenceDheSerial(){InitializeComponent();} 
        public Rap_GjendjaMagDtSkadenceDheSerial(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, report)
        {

        }
        public Rap_GjendjaMagDtSkadenceDheSerial(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
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
            xrLabel12.Text = rm.GetString("lblRaportTitullGjendjeMag", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell7.Text = rm.GetString("labelKartela", ci);
            xrTableCell9.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell10.Text = rm.GetString("labelNjesia", ci);
            xrTableCell11.Text = rm.GetString("labelDetajim1", ci);
            xrTableCell12.Text = rm.GetString("labelDetajim2", ci);
            xrTableCell8.Text = rm.GetString("labelFilterAvancuarKodbari", ci);
            xrTableCell13.Text = rm.GetString("labelRaportLLogariInventar", ci);
            xrTableCell14.Text = rm.GetString("labelRaportGjendjaeMeparshme", ci);
            xrTableCell15.Text = rm.GetString("labelRaportSasiHyrje", ci);
            xrTableCell16.Text = rm.GetString("labelRaportSasiaDalje", ci);
            xrTableCell18.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
            xrTableCell17.Text = rm.GetString("labelRaportKosto", ci);
            xrTableCell19.Text = rm.GetString("labelVlefta", ci);
            xrLabel34.Text = rm.GetString("filterMagazina", ci);
            TotaliGjithMAgazinave.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel67.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel70.Text = rm.GetString("labelLogoIMB", ci);

        }
    }
}
