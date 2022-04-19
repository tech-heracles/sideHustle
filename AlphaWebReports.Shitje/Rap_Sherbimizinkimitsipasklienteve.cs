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
    public partial class Rap_Sherbimizinkimitsipasklienteve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Sherbimizinkimitsipasklienteve(){InitializeComponent();} 
        private bool enabled;
        public Rap_Sherbimizinkimitsipasklienteve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_Sherbimizinkimitsipasklienteve(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            caktoFormatinENumrave();
        }

        private void caktoFormatinENumrave()
        {
            //xrLabel25.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrLabel32.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrLabel39.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrLabel26.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrLabel27.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrLabel33.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrLabel28.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrLabel36.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrLabel41.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrLabel42.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrLabel29.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrLabel37.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrLabel32.Summary.FormatString = xrLabel33.Summary.FormatString = xrLabel36.Summary.FormatString = xrLabel37.Summary.FormatString = xrLabel39.Summary.FormatString =  xrLabel41.Summary.FormatString = xrLabel42.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrLabel32.XlsxFormatString = xrLabel33.XlsxFormatString = xrLabel36.XlsxFormatString = xrLabel37.XlsxFormatString = xrLabel39.XlsxFormatString = xrLabel41.XlsxFormatString = xrLabel42.XlsxFormatString
            //    = xrLabel25.XlsxFormatString = xrLabel26.XlsxFormatString = xrLabel27.XlsxFormatString = xrLabel28.XlsxFormatString = xrLabel29.XlsxFormatString = xrLabel30.XlsxFormatString
            //    = 0.ToString("N" + shifraPasPresjes);
        }

 


        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {

            ResourceManager rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel1.Text = rm.GetString("labelRaportKlienti", ci);
            xrLabel17.Text = rm.GetString("RaportSherbimizinkimitsipasklienteveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell1.Text = rm.GetString("labelRaportiDtmarrjes", ci);
            xrTableCell2.Text = rm.GetString("labelRaportiDtDorezimit", ci);
            xrTableCell3.Text = rm.GetString("labelRaportLlojimaterialit", ci);
            xrTableCell4.Text = rm.GetString("labelRaportPeshaezeze", ci);
            xrTableCell5.Text = rm.GetString("labelRaportPeshaezinkuar", ci);
            xrTableCell6.Text = rm.GetString("labelRaportPerzinkut", ci);
            xrTableCell7.Text = rm.GetString("lblRaportVerejtje", ci);
            xrLabel69.Text = rm.GetString("labelLogoIMB", ci);
            

        }

    }
}
