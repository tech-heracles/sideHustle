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
    public partial class Rap_LibriShitjeveVodafone : DevExpress.XtraReports.UI.XtraReport
    {
        int shifraPasPresjes = 0;
        public Rap_LibriShitjeveVodafone(){InitializeComponent();} 
       
       
        public Rap_LibriShitjeveVodafone(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_LibriShitjeveVodafone(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi,
            DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();

            string data = raport.Parameters["filterDtDok"].Value.ToString();
            string dtFillimi = data.Split('-')[0];
            string muaji = dtFillimi.Split('/')[1];
            switch (muaji)
            {
                case "01":
                    xrLabel103.Text = "1";
                    break;
                case "02":
                    xrLabel103.Text = "2";
                    break;
                case "03":
                    xrLabel103.Text = "3";
                    break;
                case "04":
                    xrLabel103.Text = "4";
                    break;
                case "05":
                    xrLabel103.Text = "5";
                    break;
                case "06":
                    xrLabel103.Text = "6";
                    break;
                case "07":
                    xrLabel103.Text = "7";
                    break;
                case "08":
                    xrLabel103.Text = "8";
                    break;
                case "09":
                    xrLabel103.Text = "9";
                    break;
                case "10":
                    xrLabel103.Text = muaji;
                    break;
                case "11":
                    xrLabel103.Text = muaji;
                    break;
                case "12":
                    xrLabel103.Text = muaji;
                    break;

                default:
                    break;
            }
            EmrateLabelave(ci);
            shifraPasPresjes = Convert.ToInt32(raport.Parameters[22].Value);
            caktoFormatinENumrave();
        }

       
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {

            ResourceManager rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));
           
        }

        private void caktoFormatinENumrave()
        {
            xrTableCell1.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell5.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell8.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell9.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell6.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell2.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell3.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell34.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell35.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell10.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell11.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell12.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell7.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell13.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell4.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";

            xrLabel71.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel72.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel73.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel74.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel77.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel80.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel21.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel24.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel25.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel26.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel27.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel28.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel82.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell36.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell37.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";


            xrTableCell1.XlsxFormatString = xrTableCell5.XlsxFormatString = xrTableCell8.XlsxFormatString = xrTableCell9.XlsxFormatString
                                          = xrTableCell6.XlsxFormatString = xrTableCell2.XlsxFormatString = xrTableCell3.XlsxFormatString
                                          = xrTableCell10.XlsxFormatString = xrTableCell11.XlsxFormatString = xrTableCell12.XlsxFormatString = xrTableCell34.XlsxFormatString = xrTableCell35.XlsxFormatString
                                          = xrTableCell7.XlsxFormatString = xrTableCell13.XlsxFormatString = xrTableCell4.XlsxFormatString
                                          = 0.ToString("N" + shifraPasPresjes);

            xrLabel71.XlsxFormatString = xrLabel72.XlsxFormatString = xrLabel73.XlsxFormatString = xrLabel74.XlsxFormatString = xrLabel77.XlsxFormatString
                                       = xrLabel80.XlsxFormatString = xrLabel21.XlsxFormatString = xrLabel24.XlsxFormatString = xrLabel25.XlsxFormatString = xrTableCell36.XlsxFormatString = xrTableCell37.XlsxFormatString
                                       = xrLabel26.XlsxFormatString = xrLabel27.XlsxFormatString = xrLabel28.XlsxFormatString = xrLabel82.XlsxFormatString
                                       = 0.ToString("N" + shifraPasPresjes);
        }

    }
}
