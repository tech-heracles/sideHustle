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
    public partial class Rap_GjendjaArtikujveSasiVlere : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_GjendjaArtikujveSasiVlere(){InitializeComponent();} 
       
        int formatNumri = 0;

        public Rap_GjendjaArtikujveSasiVlere(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, report)
        {

        }
        public Rap_GjendjaArtikujveSasiVlere(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
        {
          
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[8].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[9].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
            parameter6.Value = raport.Parameters[5].Value;
            parameter7.Value = raport.Parameters[6].Value;
            parameter8.Value = raport.Parameters[13].Value;
            parameter10.Value = raport.Parameters[12].Value;
            parameter11.Value = raport.Parameters[15].Value;
            DegaAdministrative.Value = raport.Parameters[11].Value;
            formatNumri = Convert.ToInt32(raport.Parameters[15].Value);
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

            xrLabel4.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel27.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel28.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel29.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";

            xrLabel30.Summary.FormatString = "{0:n" + formatNumri + "}";
            xrLabel33.Summary.FormatString = "{0:n" + formatNumri + "}";
            xrLabel64.Summary.FormatString = "{0:n" + formatNumri + "}";
            xrLabel65.Summary.FormatString = "{0:n" + formatNumri + "}";
            xrLabel66.Summary.FormatString = "{0:n" + formatNumri + "}";
            xrLabel67.Summary.FormatString = "{0:n" + formatNumri + "}";
            xrLabel68.Summary.FormatString = "{0:n" + formatNumri + "}";
            xrLabel32.Summary.FormatString = "{0:n" + formatNumri + "}";

            xrLabel70.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel71.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel72.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";

            xrLabel7.XlsxFormatString = xrLabel33.XlsxFormatString = xrLabel67.XlsxFormatString = xrLabel69.XlsxFormatString = xrLabel70.XlsxFormatString = 
                xrLabel71.XlsxFormatString = xrLabel72.XlsxFormatString = xrLabel3.XlsxFormatString = xrLabel6.XlsxFormatString = xrLabel11.XlsxFormatString = 
                xrLabel10.XlsxFormatString = xrLabel64.XlsxFormatString = xrLabel65.XlsxFormatString = xrLabel66.XlsxFormatString
                = xrTableCell24.XlsxFormatString = xrLabel27.XlsxFormatString = xrLabel28.XlsxFormatString = xrLabel29.XlsxFormatString
                  = xrLabel30.XlsxFormatString = xrLabel33.XlsxFormatString = xrLabel64.XlsxFormatString = xrLabel65.XlsxFormatString
                    = xrLabel66.XlsxFormatString = xrLabel67.XlsxFormatString = xrLabel68.XlsxFormatString = xrLabel32.XlsxFormatString
                = 0.ToString("N" + formatNumri);
        }
 

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("TitullRaportGjendjaArtikujveSasiVlere", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell7.Text = rm.GetString("labelKartela", ci);
            xrTableCell8.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell9.Text = rm.GetString("labelNjesia", ci);
            //xrLabel19.Text = rm.GetString("labelRaportLLogariInventar", ci);
            //xrLabel55.Text = rm.GetString("labelRaportGjendjaeMeparshme", ci);
            //xrLabel18.Text = rm.GetString("labelRaportSasiHyrje", ci);
            //xrLabel17.Text = rm.GetString("labelRaportSasiaDalje", ci);
            //xrLabel14.Text = rm.GetString("labelVlefta", ci);
            //xrLabel16.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
            xrTableCell10.Text = rm.GetString("labelRaportKosto", ci);
            // TotaliGjithMAgazinave.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell28.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell27.Text = rm.GetString("filterMagazina", ci);
            xrLabel37.Text = rm.GetString("labelLogoIMB", ci);
            //xrLabel155.Text = rm.GetString("labelRaportGjendjaeMeparshme", ci);
        }

      
    }


}
