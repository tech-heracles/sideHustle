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
    public partial class Rap_MagazinaGjendjaGrupimArtikujsh : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_MagazinaGjendjaGrupimArtikujsh(){InitializeComponent();} 
        int formatNumri = 0;
        string windowWidth = "";
        public Rap_MagazinaGjendjaGrupimArtikujsh(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, report)
        {

        }
        public Rap_MagazinaGjendjaGrupimArtikujsh(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;         
            parameter5.Value = raport.Parameters[4].Value;
            parameter6.Value = raport.Parameters[5].Value;
            parameter7.Value = raport.Parameters[6].Value;
            parameter8.Value = raport.Parameters[13].Value;
            parameter9.Value = raport.Parameters[14].Value;
            parameter10.Value = raport.Parameters[7].Value;
            parameter11.Value = raport.Parameters[10].Value;
            DegaAdministrative.Value = raport.Parameters[11].Value;
            parametertegrupuar.Value = (raport.Parameters["filterGrupoSipasArt"].Value.ToString() != "");
            windowWidth = Convert.ToString(raport.Parameters[9].Value);
            parameterwindowWidth.Value = windowWidth;
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
            xrLabel14.Text = rm.GetString("labelVlefta", ci);
            xrLabel16.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
            xrLabel15.Text = rm.GetString("labelRaportKosto", ci);
            TotaliGjithMAgazinave.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel31.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel34.Text = rm.GetString("filterMagazina", ci);
          //  xrLabel37.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel155.Text = rm.GetString("labelRaportGjendjaeMeparshme", ci);
            xrLabel81.Text = rm.GetString("labelGrupimKlientPare", ci);
            xrLabel82.Text = rm.GetString("labelGrupimKlientDyte", ci);

        }
        
    }


}
