using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Blerje
{
    public partial class Rap_KartelaArtikulli : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_KartelaArtikulli(){InitializeComponent();} 
 
        int shifraPasPresjes = 0;

        public Rap_KartelaArtikulli(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_KartelaArtikulli(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
            parameter6.Value = raport.Parameters[5].Value;
            parameter7.Value = raport.Parameters[6].Value;
            parameter8.Value = raport.Parameters[7].Value;
            parameter9.Value = raport.Parameters[8].Value;
            parameter10.Value = raport.Parameters[9].Value;
            parameter11.Value = raport.Parameters[10].Value;
            parameter12.Value = raport.Parameters[11].Value;
            parameter13.Value = raport.Parameters[13].Value;
            parameter14.Value = raport.Parameters[14].Value;
            parameter15.Value = raport.Parameters[15].Value;
            DegaAdministrative.Value = raport.Parameters[12].Value;
            parameter16.Value = raport.Parameters[16].Value;
            parameter17.Value = raport.Parameters["filterKodbari"].Value;
            shifraPasPresjes = Convert.ToInt32(raport.Parameters["filterFormatNumri"].Value);
            caktoFormatinENumrave();
            parameterShifraPasPresje.Value = shifraPasPresjes;
        }

        private void caktoFormatinENumrave()
        {
            xrLabel25.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel84.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel32.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel39.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel26.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel27.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel33.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel40.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel28.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel36.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel41.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel42.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel29.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel37.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel32.Summary.FormatString = xrLabel33.Summary.FormatString = xrLabel36.Summary.FormatString = xrLabel37.Summary.FormatString = xrLabel39.Summary.FormatString = xrLabel40.Summary.FormatString = xrLabel41.Summary.FormatString = xrLabel42.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel32.XlsxFormatString = xrLabel33.XlsxFormatString = xrLabel36.XlsxFormatString = xrLabel37.XlsxFormatString = xrLabel39.XlsxFormatString = xrLabel40.XlsxFormatString = xrLabel41.XlsxFormatString = xrLabel42.XlsxFormatString=xrLabel84.XlsxFormatString
                = xrLabel25.XlsxFormatString = xrLabel26.XlsxFormatString = xrLabel27.XlsxFormatString = xrLabel28.XlsxFormatString = xrLabel29.XlsxFormatString = xrLabel30.XlsxFormatString 
                = 0.ToString("N" + shifraPasPresjes);
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
          ResourceManager  rm = new ResourceManager("Resources.Strings",
                     System.Reflection.Assembly.Load("App_GlobalResources"));
          

            xrLabel17.Text = rm.GetString("RaportKartelaArtikullitTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel1.Text = rm.GetString("labelKodi", ci) + ":";
            xrLabel7.Text = rm.GetString("labelRaportiEmertimi", ci) + ":";
            xrLabel11.Text = rm.GetString("filterKodbari", ci);
            xrLabel13.Text = rm.GetString("labelGrupimi", ci);

            xrTableCell7.Text = rm.GetString("labelFurnitori", ci);
            xrTableCell8.Text = rm.GetString("labelRaportiNrDok", ci);
            xrTableCell9.Text = rm.GetString("labelRaportiDtDok", ci);
            xrTableCell10.Text = rm.GetString("labelLlojDokumenti", ci);
            xrTableCell11.Text = rm.GetString("labelNjesia", ci);
            xrTableCell12.Text = rm.GetString("labelSasia", ci);
            xrTableCell13.Text = rm.GetString("labelCmimi", ci);
            xrTableCell15.Text = rm.GetString("labelVleraPaTVSH", ci);
            xrTableCell16.Text = rm.GetString("labelTVSH", ci);
            xrTableCell17.Text = rm.GetString("labelVleraMeTVSH", ci);
            xrTableCell18.Text = rm.GetString("labelProgresiviSasi", ci);
            xrLabel31.Text = rm.GetString("labelRaportiTotali", ci) + ":";
            xrLabel38.Text = rm.GetString("labelTotaliArtikullit", ci);
            xrLabel69.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell14.Text = rm.GetString("labelRaportiZbritjePerqindje", ci);

           
        }
    }
}
