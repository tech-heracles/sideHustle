using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Collections.Generic;
namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_MagazinaGjendjaKodDoganor : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_MagazinaGjendjaKodDoganor(){InitializeComponent();} 
        string windowWidth = "";         
        int formatNumri = 0;
        public Rap_MagazinaGjendjaKodDoganor(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, report)
        {

        }
        public Rap_MagazinaGjendjaKodDoganor(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            
            formatNumri = Convert.ToInt32(raport.Parameters["filterFormatNumri"].Value);
            parameterformatnumri.Value = formatNumri;
            caktoFormatinENumrave();
        }
        private void caktoFormatinENumrave()
        {
            xrLabel7.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel33.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel3.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel6.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel11.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel10.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel64.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel65.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel66.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel67.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel69.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel69.Summary.FormatString = "{0:n" + formatNumri + "}";
            xrLabel70.Summary.FormatString = "{0:n" + formatNumri + "}";
            xrLabel71.Summary.FormatString = "{0:n" + formatNumri + "}";
            xrLabel72.Summary.FormatString = "{0:n" + formatNumri + "}";
            xrLabel70.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel71.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel72.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel7.XlsxFormatString = xrLabel33.XlsxFormatString = xrLabel67.XlsxFormatString = xrLabel69.XlsxFormatString = xrLabel70.XlsxFormatString = xrLabel71.XlsxFormatString = xrLabel72.XlsxFormatString = xrLabel3.XlsxFormatString = xrLabel6.XlsxFormatString = xrLabel11.XlsxFormatString = xrLabel10.XlsxFormatString = xrLabel64.XlsxFormatString = xrLabel65.XlsxFormatString = xrLabel66.XlsxFormatString 
                = 0.ToString("N" + formatNumri);
        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportGjendjaeMagazinesTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel13.Text = rm.GetString("labelKartela", ci);
            xrLabel42.Text = rm.GetString("labelFilterAvancuarKodbari", ci);
            xrLabel21.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel20.Text = rm.GetString("labelNjesia", ci);
            xrLabel19.Text = rm.GetString("labelRaportLLogariInventar", ci);
            xrLabel18.Text = rm.GetString("labelRaportSasiHyrje", ci);
            xrLabel17.Text = rm.GetString("labelRaportSasiaDalje", ci);
            xrLabel16.Text = rm.GetString("labelVlefta", ci);
            xrLabel16.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
            xrLabel15.Text = rm.GetString("labelRaportKosto", ci);
            TotaliGjithMAgazinave.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel31.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel34.Text = rm.GetString("filterMagazina", ci);
            xrLabel74.Text = rm.GetString("lblRaportKodiDoganor", ci) + " 2";
            xrLabel75.Text = rm.GetString("lblRaportKodiDoganor", ci);
            xrLabel37.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel155.Text = rm.GetString("labelRaportGjendjaeMeparshme", ci);
        }
        
    }


}
