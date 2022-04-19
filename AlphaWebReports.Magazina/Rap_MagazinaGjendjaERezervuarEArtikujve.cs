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
    public partial class Rap_MagazinaGjendjaERezervuarEArtikujve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_MagazinaGjendjaERezervuarEArtikujve(){InitializeComponent();} 

        public Rap_MagazinaGjendjaERezervuarEArtikujve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, report)
        {

        }
        public Rap_MagazinaGjendjaERezervuarEArtikujve(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti,int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
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
            xrLabel12.Text = rm.GetString("lblRaportGjendjaRezervuarArtikujve", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell7.Text = rm.GetString("labelKartela", ci);
            xrTableCell8.Text = rm.GetString("labelFilterAvancuarKodbari", ci);
            xrTableCell9.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell10.Text = rm.GetString("labelNjesia", ci);
            xrTableCell11.Text = rm.GetString("lblRaportMagazina", ci);
            xrTableCell12.Text = rm.GetString("lblRaportGjendjaMagazine", ci);
            xrTableCell13.Text = rm.GetString("lblRaportHyrjeRezervimi", ci);
            xrTableCell14.Text = rm.GetString("lblRaportDaljeRezervimi", ci);
            xrTableCell15.Text = rm.GetString("lblRaportGjendjaNeRezervim", ci);
            xrTableCell16.Text = rm.GetString("lblRaportGjendjeDisponueshme", ci);
            TotaliGjithMAgazinave.Text = rm.GetString("labelRaportiTotali", ci);
        }
    }
}
