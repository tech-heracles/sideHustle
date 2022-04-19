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
    public partial class Rap_KontrolliSkadencesArtikujve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_KontrolliSkadencesArtikujve(){InitializeComponent();} 
        public Rap_KontrolliSkadencesArtikujve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, report)
        {

        }
        public Rap_KontrolliSkadencesArtikujve(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
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
            xrLabel12.Text = rm.GetString("lblRaportKontrolliSkadencesArtikujve", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell7.Text = rm.GetString("labelKodi", ci);
            xrTableCell9.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell10.Text = rm.GetString("labelNjesia", ci);
            xrTableCell11.Text = rm.GetString("labelDetajim1", ci);
            xrTableCell12.Text = rm.GetString("labelDetajim2", ci);
            xrTableCell8.Text = rm.GetString("labelBarkodi", ci);
            xrTableCell13.Text = rm.GetString("labelVendndodhja", ci);
            xrTableCell14.Text = rm.GetString("labelRaportLLogariInventar", ci);
            xrTableCell18.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
            xrTableCell17.Text = rm.GetString("labelRaportKosto", ci);
            xrTableCell19.Text = rm.GetString("labelVlefta", ci);
            xrLabel34.Text = rm.GetString("filterMagazina", ci);
            xrTableCell20.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel70.Text = rm.GetString("labelLogoIMB", ci);

        }
    }
}
