using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_Artikujteshitur_SePDeFn_131308526368694547 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_Artikujteshitur_SePDeFn_131308526368694547() { InitializeComponent(); }
        int shifrapaspresjes = 0;
        public Rap_Artikujteshitur_SePDeFn_131308526368694547(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
         this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, report) { }

        public Rap_Artikujteshitur_SePDeFn_131308526368694547(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
        {

            InitializeComponent();
            EmrateLabelave(ci);

            shifrapaspresjes = Convert.ToInt32(raport.Parameters["filterFormatNumri"].Value);
            caktoFormatinENumrave();
        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            var rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));
            titulliLabel.Text = rm.GetString("labelTitulliArtikujTeShiturMePike", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            labelKodi.Text = rm.GetString("labelKodi", ci);
            labelKodbari.Text = "Kodbari";
            labelRaportiPershkrimi.Text = rm.GetString("labelRaportiPershkrimi", ci);
            labelNjesia.Text = rm.GetString("labelNjesia", ci);
            labelSasia.Text = rm.GetString("labelSasia", ci);
            labelCmimi.Text = rm.GetString("labelCmimi", ci);
            labelZbritjeAnalitike.Text = rm.GetString("labelZbritjeAnalitike", ci);
            labelVleftapaTVSH.Text = rm.GetString("labelVleftapaTVSH", ci);
            labelZbritjaTotale.Text = rm.GetString("labelZbritjaTotale", ci);
            labelTVSH.Text = rm.GetString("labelTVSH", ci);
            labelVleftaMe_Tvsh.Text = rm.GetString("labelVleftaMe_Tvsh", ci);
            labelRaportiTotali.Text = rm.GetString("labelRaportiTotali", ci);
            labelLogoIMB.Text = rm.GetString("labelLogoIMB", ci);
        }
        private void caktoFormatinENumrave()
        {
            xrTableCell10.DataBindings[0].FormatString = "{0:n" + shifrapaspresjes + "}";
            xrTableCell11.DataBindings[0].FormatString = "{0:n" + shifrapaspresjes + "}";
            xrTableCell12.DataBindings[0].FormatString = "{0:n" + shifrapaspresjes + "}";
            xrTableCell13.DataBindings[0].FormatString = "{0:n" + shifrapaspresjes + "}";
            xrTableCell14.DataBindings[0].FormatString = "{0:n" + shifrapaspresjes + "}";
            xrTableCell3.DataBindings[0].FormatString = "{0:n" + shifrapaspresjes + "}";
            xrTableCell15.DataBindings[0].FormatString = "{0:n" + shifrapaspresjes + "}";
            xrTableCell16.DataBindings[0].FormatString = "{0:n" + shifrapaspresjes + "}";
            xrTableCell19.DataBindings[0].FormatString = "{0:n" + shifrapaspresjes + "}";
            xrTableCell20.DataBindings[0].FormatString = "{0:n" + shifrapaspresjes + "}";
            xrTableCell21.DataBindings[0].FormatString = "{0:n" + shifrapaspresjes + "}";
            xrTableCell22.DataBindings[0].FormatString = "{0:n" + shifrapaspresjes + "}";
            xrTableCell17.DataBindings[0].FormatString = "{0:n" + shifrapaspresjes + "}";


            xrTableCell10.Summary.FormatString = xrTableCell11.Summary.FormatString = xrTableCell12.Summary.FormatString =
            xrTableCell13.Summary.FormatString = xrTableCell14.Summary.FormatString = xrTableCell3.Summary.FormatString =
            xrTableCell15.Summary.FormatString = xrTableCell16.Summary.FormatString = xrTableCell19.Summary.FormatString =
            xrTableCell20.Summary.FormatString = xrTableCell21.Summary.FormatString = xrTableCell22.Summary.FormatString =
            xrTableCell17.Summary.FormatString = "{0:n" + shifrapaspresjes + "}";

            xrTableCell10.XlsxFormatString = xrTableCell11.XlsxFormatString = xrTableCell12.XlsxFormatString =
                xrTableCell13.XlsxFormatString = xrTableCell14.XlsxFormatString = xrTableCell3.XlsxFormatString =
                xrTableCell15.XlsxFormatString = xrTableCell16.XlsxFormatString = xrTableCell19.XlsxFormatString =
                xrTableCell20.XlsxFormatString = xrTableCell21.XlsxFormatString = xrTableCell22.XlsxFormatString =
                xrTableCell17.XlsxFormatString = 0.ToString("N" + shifrapaspresjes);
        }
    }
}
