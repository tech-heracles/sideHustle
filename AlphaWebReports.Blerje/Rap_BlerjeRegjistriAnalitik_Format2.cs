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
    public partial class Rap_BlerjeRegjistriAnalitik_Format2 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_BlerjeRegjistriAnalitik_Format2(){InitializeComponent();} 

        int shifraPasPresjes = 0;

        public Rap_BlerjeRegjistriAnalitik_Format2(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_BlerjeRegjistriAnalitik_Format2(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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
            parameter13.Value = raport.Parameters[12].Value;  
            parameter14.Value = raport.Parameters[14].Value; 
            parameter15.Value = raport.Parameters[15].Value;  
            parameter16.Value = raport.Parameters[16].Value;
            DegaAdministrative.Value = raport.Parameters[13].Value;
            parameter17.Value = raport.Parameters[17].Value;
            parameter18.Value = raport.Parameters[18].Value;
            parameter19.Value = raport.Parameters[19].Value;
            shifraPasPresjes = Convert.ToInt32(raport.Parameters["filterFormatNumri"].Value);
            caktoFormatinENumrave();
        }

        private void caktoFormatinENumrave()
        {
            xrTableCell1.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell6.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell7.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell8.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell9.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell10.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell11.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell12.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell13.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell14.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell15.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell16.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";

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
            xrLabel14.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
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
            xrLabel14.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
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
            xrLabel14.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell6.XlsxFormatString = xrTableCell7.XlsxFormatString = xrTableCell8.XlsxFormatString = xrTableCell11.XlsxFormatString 
                = xrTableCell12.XlsxFormatString = xrTableCell13.XlsxFormatString = xrTableCell14.XlsxFormatString = xrTableCell15.XlsxFormatString
                = xrTableCell9.XlsxFormatString = xrTableCell10.XlsxFormatString = xrTableCell1.XlsxFormatString = xrTableCell16.XlsxFormatString
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
          

            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel44.Text = rm.GetString("labelShumaPaTvsh", ci);
            xrLabel45.Text = rm.GetString("labelTVSH", ci);
            xrLabel46.Text = rm.GetString("labelShumaMeTvsh", ci);
            xrLabel43.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel69.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel6.Text = rm.GetString("filterMonedha", ci);
            //report header
            //xrLabel94.Text = rm.GetString("RaportRegjistriAnalitikBlerjeveTitulli", ci);
            xrLabel108.Text = rm.GetString("labelVleftameZbritje", ci);
            xrLabel106.Text = rm.GetString("labelKodi", ci);
            xrLabel102.Text = rm.GetString("labelZbritjaTotale", ci);
            xrLabel96.Text = rm.GetString("labelMonLlogari", ci);
            xrLabel97.Text = rm.GetString("labelVlefta", ci);
            xrLabel107.Text = rm.GetString("labelZbritjeAnalitike", ci);
            xrLabel104.Text = rm.GetString("labelVleftapaTVSH", ci);
            xrLabel95.Text = rm.GetString("labelDokumentArtikulli", ci);
            xrLabel103.Text = rm.GetString("labelGjithsej", ci);
            xrLabel105.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel101.Text = rm.GetString("labelNjesia", ci);
            xrLabel100.Text = rm.GetString("labelSasia", ci);
            xrLabel98.Text = rm.GetString("labelMonBaze", ci);
            xrLabel21.Text = rm.GetString("labelRaportiCmimiParardhes", ci); 
            xrLabel22.Text = rm.GetString("labelRaportiDiferenca", ci);
        }
    }
}
