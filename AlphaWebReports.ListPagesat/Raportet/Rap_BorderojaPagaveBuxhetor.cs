using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.ListPagesat.Raportet
{
    public partial class Rap_BorderojaPagaveBuxhetor : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_BorderojaPagaveBuxhetor()
        { InitializeComponent();} 

        int shifraPasPresjes = 0;
        public Rap_BorderojaPagaveBuxhetor(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }

        public Rap_BorderojaPagaveBuxhetor(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
            parameter3.Value = raport.Parameters[3].Value;
            parameter4.Value = raport.Parameters[6].Value;
            caktoFormatinENumrave();
        }

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));

           

        }
        private void caktoFormatinENumrave()
        {
            //xrTableCell1.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrTableCell1.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            //xrTableCell2.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrTableCell2.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            //xrTableCell41.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrTableCell41.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            //xrTableCell3.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrTableCell3.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
        }
        //private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{

        //}

        //private void xrTableCell20_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{

        //}

        //private void xrTableCell16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    //xrTableCell16.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
        //    //xrTableCell16.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
        //}

    }
}
