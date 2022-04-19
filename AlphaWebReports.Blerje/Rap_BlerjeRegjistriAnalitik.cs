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
    public partial class Rap_BlerjeRegjistriAnalitik : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_BlerjeRegjistriAnalitik(){InitializeComponent();} 
      
        int shifraPasPresjes = 0;
        public Rap_BlerjeRegjistriAnalitik(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_BlerjeRegjistriAnalitik(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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
            parameter20.Value = raport.Parameters["filterKodbari"].Value;
            shifraPasPresjes = Convert.ToInt32(raport.Parameters["filterFormatNumri"].Value);
            caktoFormatinENumrave();
        }

        private void caktoFormatinENumrave()
        {
       //     xrLabel28.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell6.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
          //  xrLabel38.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell8.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
         //   xrLabel27.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell17.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
        //    xrLabel32.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell16.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
        //    xrLabel36.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell10.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell15.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell14.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell11.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";

            xrTableCell1.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell2.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell3.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell4.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell32.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell33.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";

            xrTableCell5.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
          xrTableCell7.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
       //     xrTablecell9.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";

          

            xrLabel25.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel26.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrLabel47.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrLabel34.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrLabel42.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrLabel50.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrLabel49.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrLabel51.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrLabel39.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrLabel40.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrLabel41.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel25.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel26.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell1.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell2.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell3.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell4.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell32.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell33.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell5.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
           xrTableCell7.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell9.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel25.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrLabel26.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell6.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell8.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell17.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell16.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell10.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell15.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell14.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell11.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell1.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell2.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell3.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell4.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell32.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell33.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell6.XlsxFormatString = xrTableCell8.XlsxFormatString = xrTableCell17.XlsxFormatString = xrTableCell16.XlsxFormatString
                = xrTableCell10.XlsxFormatString = xrTableCell15.XlsxFormatString = xrTableCell14.XlsxFormatString = xrTableCell11.XlsxFormatString
                = xrTableCell1.XlsxFormatString = xrTableCell2.XlsxFormatString = xrTableCell13.XlsxFormatString = xrTableCell13.XlsxFormatString = xrTableCell32.XlsxFormatString
                = xrTableCell33.XlsxFormatString = xrTableCell5.XlsxFormatString 
            = xrTableCell7.XlsxFormatString 
                = xrTableCell9.XlsxFormatString
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
          

            xrLabel17.Text = rm.GetString("RaportRegjistriAnalitikBlerjeveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel10.Text = rm.GetString("labelDokumentArtikulli", ci);
            xrLabel3.Text = rm.GetString("labelKodi", ci);
            xrLabel4.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel7.Text = rm.GetString("labelNjesia", ci);
            xrLabel8.Text = rm.GetString("labelSasia", ci);
            xrLabel13.Text = rm.GetString("labelVlefta", ci);
            xrLabel14.Text = rm.GetString("labelGjithsej", ci);
            xrLabel15.Text = rm.GetString("labelZbritjeAnalitike", ci);
            xrLabel23.Text = rm.GetString("labelVleftapaTVSH", ci);
            xrLabel19.Text = rm.GetString("labelZbritjaTotale", ci);
            xrLabel24.Text = rm.GetString("labelVleftameZbritje", ci);
            xrLabel21.Text = rm.GetString("labelMonLlogari", ci);
            xrLabel22.Text = rm.GetString("labelMonBaze", ci);
            xrLabel44.Text = rm.GetString("labelShumaPaTvsh", ci);
            xrLabel45.Text = rm.GetString("labelTVSH", ci);
            xrLabel46.Text = rm.GetString("labelShumaMeTvsh", ci);
            xrLabel43.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel69.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel6.Text = rm.GetString("filterMonedha", ci);
          
        }

        

    
    }
}
