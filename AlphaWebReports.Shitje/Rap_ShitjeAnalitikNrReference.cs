using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_ShitjeAnalitikNrReference : DevExpress.XtraReports.UI.XtraReport
    {
        private bool enabled;
        public Rap_ShitjeAnalitikNrReference()
        {
            InitializeComponent();
        }
        int shifraPasPresjes = 0;
      
        public Rap_ShitjeAnalitikNrReference(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_ShitjeAnalitikNrReference(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);shifraPasPresjes = Convert.ToInt32(raport.Parameters["filterFormatNumri"].Value);
            filterFormatNumri.Value = raport.Parameters["filterFormatNumri"].Value;
            caktoFormatinENumrave();
        }

        private void caktoFormatinENumrave()
        {
            xrLabel27.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel28.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrLabel38.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel32.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel36.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel33.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel37.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel35.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel27.XlsxFormatString = xrLabel28.XlsxFormatString 
                //= xrLabel38.XlsxFormatString 
                = xrLabel32.XlsxFormatString
                = xrLabel36.XlsxFormatString = xrLabel33.XlsxFormatString = xrLabel37.XlsxFormatString = xrLabel35.XlsxFormatString
                = 0.ToString("N" + shifraPasPresjes);

            xrLabel25.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel26.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel47.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel34.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel42.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel50.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel49.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel51.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel39.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel40.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel41.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel25.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel26.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel47.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel34.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel42.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel50.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel49.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel51.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel39.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel40.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel41.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel25.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrLabel26.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrLabel47.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrLabel34.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrLabel42.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrLabel50.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrLabel49.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrLabel51.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrLabel39.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrLabel40.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrLabel41.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
          ResourceManager  rm = new ResourceManager("Resources.Strings",
                     System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel17.Text = rm.GetString("RaportRegjistriAnalitikShitjeveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel44.Text = rm.GetString("labelShumaPaTvsh", ci);
            xrLabel45.Text = rm.GetString("labelTVSH", ci);
            xrLabel46.Text = rm.GetString("labelShumaMeTvsh", ci);
            xrLabel66.Text = rm.GetString("labelShumaPaTvsh", ci);
            xrLabel64.Text = rm.GetString("labelTVSH", ci);
            xrLabel65.Text = rm.GetString("labelShumaMeTvsh", ci);
            xrLabel1.Text = rm.GetString("labelNr", ci);
            xrLabel2.Text = rm.GetString("label_Klienti",ci);
            xrLabel71.Text = rm.GetString("label_Emri", ci);
            xrLabel5.Text = rm.GetString("labelDateDokumenti", ci);
            xrLabel6.Text = rm.GetString("filterMonedha", ci);
            xrLabel43.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel123.Text = rm.GetString("labelDokumentArtikulli", ci);
            xrLabel120.Text = rm.GetString("labelKodi", ci);
            xrLabel121.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel110.Text = rm.GetString("labelNjesia", ci);
            xrLabel111.Text = rm.GetString("labelSasia", ci);
            xrLabel117.Text = rm.GetString("labelVlefta", ci);
            xrLabel122.Text = rm.GetString("labelGjithsej", ci);
            xrTableCell10.Text = rm.GetString("labelZbritjeAnalitike", ci) + " %"; 
            xrLabel118.Text = rm.GetString("labelVleftapaTVSH", ci);
            xrLabel112.Text = rm.GetString("labelZbritjaTotale", ci) + " %";
            xrLabel119.Text = rm.GetString("labelVleftameZbritje", ci);
            xrLabel114.Text = rm.GetString("labelMonLlogari", ci);
            xrLabel115.Text = rm.GetString("labelMonBaze", ci);
            xrLabel124.Text = rm.GetString("labelFilterAvancuarPerdorues", ci);
            xrLabel125.Text = rm.GetString("filterRaportPershkrimFature", ci);
            xrLabel126.Text = rm.GetString("labelFilterAvancuarNrReference", ci);
        }
    }
}
