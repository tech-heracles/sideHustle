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
    public partial class Rap_BorderojaPagave : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_BorderojaPagave(){InitializeComponent();} 
       
        int shifraPasPresjes = 0;

        public Rap_BorderojaPagave(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_BorderojaPagave(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
          
            InitializeComponent();
            EmrateLabelave(ci);
            
            parameter1.Value = raport.Parameters[0].Value;
            
            parameter2.Value = raport.Parameters[3].Value;
            
            parameter3.Value = raport.Parameters[2].Value;
            

            parameter4.Value = raport.Parameters[1].Value;
            shifraPasPresjes = Convert.ToInt16(raport.Parameters["filterFormatNumri"].Value);
            caktoFormatinENumrave();
        }

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel1.Text = rm.GetString("lblBorderojaPagave", ci);
            xrLabel11.Text = rm.GetString("lblInstitucioni", ci);
            
            xrLabel13.Text = rm.GetString("lblKodInstitucioni", ci);
            xrLabel15.Text = rm.GetString("lblEmeriMbiemeri", ci);
            xrLabel16.Text = rm.GetString("lblEmertesaStrukturesMiratuar", ci);
            xrLabel17.Text = rm.GetString("lblDitePune", ci);
            xrLabel18.Text = rm.GetString("lblKategoria", ci);
            xrLabel19.Text = rm.GetString("lblPagaGrupit", ci);
            xrLabel20.Text = rm.GetString("lblPagaFunskion", ci);
            xrLabel21.Text = rm.GetString("lblRaporteMjekesore", ci);
            xrLabel26.Text = rm.GetString("lblShtesePageVjtPune", ci);
            xrLabel25.Text = rm.GetString("lblVite",ci);
            xrLabel27.Text = rm.GetString("lblsimbolperq", ci);
            xrLabel28.Text = rm.GetString("lblLek", ci);
            xrLabel24.Text = rm.GetString("lblShtesaVeshtiresiRreziqe", ci);
            xrLabel22.Text = rm.GetString("lblShtesaPagefunx", ci);
            xrLabel23.Text = rm.GetString("lblShtesaPageJashteOrari", ci);
            xrLabel29.Text = rm.GetString("lblShtesaLargesiQendraBanuara", ci);
            xrLabel30.Text = rm.GetString("lblShtesaKualifikim", ci);
            xrLabel31.Text = rm.GetString("lblKufiriPageses", ci);
            xrLabel32.Text = rm.GetString("lblNdalesat", ci);
            xrLabel33.Text = rm.GetString("lblSigshoq", ci);
            xrLabel34.Text = rm.GetString("lblTatimPage", ci);
            xrLabel35.Text = rm.GetString("lblSigSupl", ci);
            xrLabel37.Text = rm.GetString("lblTetjera", ci);
            xrLabel36.Text = rm.GetString("lblShumaNdalesa", ci);
            xrLabel38.Text = rm.GetString("lblShumaNeto", ci);
            xrLabel39.Text = rm.GetString("lblFinanca", ci);
            xrLabel40.Text = rm.GetString("lblDrejtori", ci);
            xrLabel9.Text = rm.GetString("labelDateDokumenti", ci);


        }
        private void caktoFormatinENumrave()
        {
            xrTableCell16.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell16.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell17.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell17.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell18.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell18.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
           
            xrTableCell32.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell32.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell33.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell33.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell34.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell34.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell35.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell35.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell36.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell36.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell37.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell37.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell38.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell38.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell39.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell39.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell40.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell40.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell1.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell1.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell2.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell2.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell41.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell41.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell3.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell3.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            

            xrTableCell68.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell68.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell68.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell69.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell69.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell69.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell72.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell72.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell72.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell73.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell73.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell73.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell74.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell74.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell74.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell75.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell75.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell75.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell76.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell76.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell76.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell77.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell77.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell77.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            
            xrTableCell79.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell79.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell79.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell80.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell80.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell80.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell81.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell81.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell81.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell82.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell82.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell82.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell83.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell83.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell83.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell84.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell84.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell84.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";

        }
        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrTableCell20_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //xrTableCell20.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrTableCell20.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            double perqindja = Convert.ToDouble(GetCurrentColumnValue("svKomp"));
            xrTableCell20.Text = String.Format("{0:n" + shifraPasPresjes + "}", perqindja);
        }

        private void xrTableCell16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //xrTableCell16.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrTableCell16.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
        }

    }
}
