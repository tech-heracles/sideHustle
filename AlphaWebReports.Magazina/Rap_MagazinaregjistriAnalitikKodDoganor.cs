using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_MagazinaregjistriAnalitikKodDoganor : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_MagazinaregjistriAnalitikKodDoganor(){InitializeComponent();} 

        int shifraPasPresjes = 0;
        public Rap_MagazinaregjistriAnalitikKodDoganor(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_MagazinaregjistriAnalitikKodDoganor(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {   InitializeComponent();
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
            parameter11.Value = raport.Parameters[11].Value;
            parameter12.Value = raport.Parameters[12].Value;
            parameter13.Value = raport.Parameters[13].Value;
            DegaAdministrative.Value = raport.Parameters[10].Value;
            parameter14.Value = raport.Parameters[14].Value;
            parameter15.Value = raport.Parameters[15].Value;  
            parameter16.Value = raport.Parameters[16].Value;
            PershkrimDetajimArt.Value = raport.Parameters[18].Value;
            parameter17.Value = raport.Parameters[19].Value;
            shifraPasPresjes = Convert.ToInt32(raport.Parameters["filterFormatNumri"].Value);
                     caktoFormatinENumrave();
            parameterShifraPasPresjes.Value = shifraPasPresjes.ToString();

        }
        private void caktoFormatinENumrave()
        {
            xrLabel27.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel19.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel21.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel22.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel27.XlsxFormatString = xrLabel19.XlsxFormatString = xrLabel21.XlsxFormatString = xrLabel22.XlsxFormatString = xrLabel24.XlsxFormatString = xrLabel26.XlsxFormatString = xrLabel30.XlsxFormatString 
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

          xrLabel17.Text = rm.GetString("RaportRegjistriAnalitikMagazinesTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel1.Text = rm.GetString("labelFilterAvancuarLlojDok", ci);
            Pershkrimi.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel2.Text = rm.GetString("labelFilterAvancuarNrDok", ci);
            xrTableCell9.Text = rm.GetString("labelFilterKryesorDtDokumenti", ci);
            xrTableCell10.Text = rm.GetString("labelFilterAvancuarDtRegjistrimi", ci);
            Kartela.Text = rm.GetString("labelKartela", ci);
            xrLabel15.Text = rm.GetString("labelNjesia", ci);
            xrLabel9.Text = rm.GetString("labelSasia", ci);
            xrLabel14.Text = rm.GetString("labelVlefta", ci);
            xrLabel10.Text = rm.GetString("labelCmimi", ci);
            xrLabel23.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel25.Text = rm.GetString("labelRaportTotaliMagazines", ci);
            xrLabel20.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
           xrLabel36.Text = rm.GetString("labelLogoIMB", ci);
           xrLabel69.Text = rm.GetString("lblRaportKodiDoganor", ci) + " 2";
           xrLabel70.Text = rm.GetString("lblRaportKodiDoganor", ci);
           xrLabel28.Text = rm.GetString("labelRaportMagazina", ci);
            
        }
        
    }
}
