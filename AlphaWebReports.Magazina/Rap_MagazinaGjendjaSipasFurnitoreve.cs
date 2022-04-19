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
    public partial class Rap_MagazinaGjendjaSipasFurnitoreve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_MagazinaGjendjaSipasFurnitoreve(){InitializeComponent();} 
        int formatNumri = 0;
        string windowWidth = "";
        ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
        CultureInfo cult;
        public Rap_MagazinaGjendjaSipasFurnitoreve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, report)
        {

        }
        public Rap_MagazinaGjendjaSipasFurnitoreve(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
        {          
            cult = ci;
            InitializeComponent();
            EmrateLabelave(ci);
      
            formatNumri = Convert.ToInt32(raport.Parameters[20].Value);
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
            xrLabel72.Summary.FormatString = "{0:n" + formatNumri + "}";         
            xrLabel72.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel7.XlsxFormatString = xrLabel33.XlsxFormatString =  xrLabel72.XlsxFormatString = xrLabel3.XlsxFormatString = xrLabel6.XlsxFormatString = xrLabel11.XlsxFormatString = xrLabel10.XlsxFormatString =  
            0.ToString("N" + formatNumri);
        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportGjendjaeMagazinesFurnitorTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell7.Text = rm.GetString("labelKartela", ci);
            xrTableCell8.Text = rm.GetString("labelFilterAvancuarKodbari", ci);
            xrTableCell9.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell10.Text = rm.GetString("labelNjesia", ci);
            xrTableCell11.Text = rm.GetString("labelRaportLLogariInventar", ci);
            xrTableCell13.Text = rm.GetString("labelRaportSasiHyrje", ci);
            xrTableCell14.Text = rm.GetString("labelRaportSasiaDalje", ci);
            xrTableCell17.Text = rm.GetString("labelVlefta", ci);
            xrTableCell15.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
            xrTableCell16.Text = rm.GetString("labelRaportKosto", ci);
            TotaliGjithMAgazinave.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel79.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel31.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel34.Text = rm.GetString("filterMagazina", ci);
            xrLabel37.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell12.Text = rm.GetString("labelRaportGjendjaeMeparshme", ci);
            xrLabel77.Text = rm.GetString("lblRaportFurnitorKryesor", ci);
        }
        private void xrLabel78_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.GetCurrentColumnValue("IDKLIENTFURNITOR") != null)
            {
                if (string.IsNullOrEmpty(this.GetCurrentColumnValue("IDKLIENTFURNITOR").ToString()))
                    xrLabel78.Text = rm.GetString("lblPaFurnitor", cult);
                else xrLabel78.Text = this.GetCurrentColumnValue("EMERTIMIKF").ToString();
            }
        }

    }
}
