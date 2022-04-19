using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;


namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_LibriShitjeveSipasMuajve : DevExpress.XtraReports.UI.XtraReport
    {
        int shifraPasPresjes = 0;


        public Rap_LibriShitjeveSipasMuajve() {InitializeComponent();  }

        public Rap_LibriShitjeveSipasMuajve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_LibriShitjeveSipasMuajve(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi,
          DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();

            EmrateLabelave(ci);
            shifraPasPresjes = Convert.ToInt32(raport.Parameters[22].Value);
            caktoFormatinENumrave();
  

        }

        private void EmrateLabelave(CultureInfo ci)
        {
 
            ResourceManager rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel1.Text = rm.GetString("RaportLibriShitjeveSipasMuajveTitulli", ci);

        }
        private void caktoFormatinENumrave()
        {
            xrTableCell14.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell15.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell12.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell16.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell9.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell18.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell19.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell22.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell21.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell24.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell10.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";

            xrTableCell27.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell29.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell30.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell31.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell32.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell35.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell36.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell41.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell42.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell43.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell44.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";

            xrTableCell14.XlsxFormatString = xrTableCell15.XlsxFormatString = xrTableCell12.XlsxFormatString = xrTableCell16.XlsxFormatString
                                           = xrTableCell9.XlsxFormatString  = xrTableCell18.XlsxFormatString = xrTableCell19.XlsxFormatString
                                           = xrTableCell22.XlsxFormatString = xrTableCell21.XlsxFormatString = xrTableCell24.XlsxFormatString
                                           = xrTableCell10.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);

            xrTableCell27.XlsxFormatString = xrTableCell29.XlsxFormatString = xrTableCell30.XlsxFormatString = xrTableCell31.XlsxFormatString
                                           = xrTableCell32.XlsxFormatString = xrTableCell35.XlsxFormatString = xrTableCell36.XlsxFormatString
                                           = xrTableCell41.XlsxFormatString = xrTableCell42.XlsxFormatString = xrTableCell43.XlsxFormatString
                                           = xrTableCell44.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);

        }

    }
}
