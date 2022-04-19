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
    public partial class Rap_LibriShitjeve2015_Raportuese : DevExpress.XtraReports.UI.XtraReport
    {
       
 
        int shifraPasPresjes = 0;
        
        public Rap_LibriShitjeve2015_Raportuese()    
        {
             InitializeComponent();

        }
        public Rap_LibriShitjeve2015_Raportuese(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_LibriShitjeve2015_Raportuese(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi,
            DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            // Modifikimi i fushes Muaji
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
            shifraPasPresjes = Convert.ToInt32(raport.Parameters[22].Value);
            caktoFormatinENumrave();
             
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
            xrTableCell10.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell11.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell12.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell7.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell13.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell4.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell119.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell118.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";

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


            xrTableCell1.XlsxFormatString = xrTableCell5.XlsxFormatString = xrTableCell8.XlsxFormatString = xrTableCell9.XlsxFormatString
                                          = xrTableCell6.XlsxFormatString = xrTableCell2.XlsxFormatString = xrTableCell3.XlsxFormatString 
                                          = xrTableCell10.XlsxFormatString = xrTableCell11.XlsxFormatString = xrTableCell12.XlsxFormatString 
                                          = xrTableCell7.XlsxFormatString = xrTableCell13.XlsxFormatString = xrTableCell4.XlsxFormatString
                                          = xrTableCell118.XlsxFormatString = xrTableCell119.XlsxFormatString 
                                          = 0.ToString("N" + shifraPasPresjes);

            xrLabel71.XlsxFormatString = xrLabel72.XlsxFormatString = xrLabel73.XlsxFormatString = xrLabel74.XlsxFormatString = xrLabel77.XlsxFormatString
                                       = xrLabel80.XlsxFormatString = xrLabel21.XlsxFormatString = xrLabel24.XlsxFormatString = xrLabel25.XlsxFormatString
                                       = xrLabel26.XlsxFormatString = xrLabel27.XlsxFormatString = xrLabel28.XlsxFormatString = xrLabel82.XlsxFormatString
                                       = 0.ToString("N" + shifraPasPresjes);
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {

            ResourceManager rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));
           
            //xrLabel1.Text = rm.GetString("RaportLibriShitjeveTitulli", ci);
            //xrLabel2.Text = rm.GetString("labelShoqeria", ci);
            //xrLabel3.Text = rm.GetString("labelNipti", ci);
            //xrLabel4.Text = rm.GetString("labelViti", ci);
            //xrLabel5.Text = rm.GetString("labelFilterKryesorMuaji", ci);
            //xrLabel6.Text = rm.GetString("labelPaVeprimtari", ci);
            //xrLabel8.Text = rm.GetString("labelKonfirmimTransaksioni", ci);
            //xrLabel9.Text = rm.GetString("labelFature", ci);
            //xrLabel10.Text = rm.GetString("labelNrFature", ci);
            //xrLabel11.Text = rm.GetString("labelFilterAvancuarNrSerial", ci);
            //xrLabel12.Text = rm.GetString("labelDataFormat", ci);
            //xrLabel13.Text = rm.GetString("labelBleresi", ci);
            //xrLabel14.Text = rm.GetString("labelEmerTregtarPerson", ci);
            //xrLabel15.Text = rm.GetString("labelRrethi", ci);
            //xrLabel16.Text = rm.GetString("labelNIPT", ci);
            //xrLabel17.Text = rm.GetString("labelTotalShitjesh", ci);
            //xrLabel19.Text = rm.GetString("labelShitjetePerjashtuara", ci);
            //xrLabel20.Text = rm.GetString("labelExporteFurnizime", ci) ;
            //xrLabel22.Text = rm.GetString("labelShitjeMeShkalle", ci) + " 20%";
            //xrLabel23.Text = rm.GetString("labelShitjeMeShkalle", ci) + " 10%";
            //xrLabel29.Text = rm.GetString("labelVleraTatueshme", ci);
            //xrLabel30.Text = rm.GetString("labelTVSH", ci);
            //xrLabel31.Text = rm.GetString("labelVleraTatueshme", ci);
            //xrLabel32.Text = rm.GetString("labelTVSH", ci);
            //xrLabel70.Text = rm.GetString("labelShumaTotale", ci);
            //xrLabel94.Text = rm.GetString("labelKutiaFormularitTeDeklarimitTePageses", ci);

            //xrLabel86.Text = rm.GetString("labelKutia", ci) + " (9)";
            //xrLabel87.Text = rm.GetString("labelKutia", ci) + " (10)";
            //xrLabel85.Text = rm.GetString("labelKutia", ci) + " (11)";
            //xrLabel88.Text = rm.GetString("labelKutia", ci) + " (12)";
            //xrLabel91.Text = rm.GetString("labelKutia", ci) + " (13)";
            //xrLabel90.Text = rm.GetString("labelKutia", ci) + " (14)";

            //xrLabel96.Text = rm.GetString("labelshpjegim", ci);
            //xrLabel97.Text = rm.GetString("labelEmerMbiemer", ci);
            //xrLabel95.Text = rm.GetString("labelShpjegimPrintimi", ci);
            //xrLabel98.Text = rm.GetString("labelShtimNrRreshtash", ci);
           
        }
    }
}
