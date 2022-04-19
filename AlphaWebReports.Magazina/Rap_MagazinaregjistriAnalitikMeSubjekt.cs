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
    public partial class Rap_MagazinaregjistriAnalitikMeSubjekt : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_MagazinaregjistriAnalitikMeSubjekt(){InitializeComponent();} 
        int shifraPasPresjes = 0;
        public Rap_MagazinaregjistriAnalitikMeSubjekt(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_MagazinaregjistriAnalitikMeSubjekt(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {   InitializeComponent();
        EmrateLabelave(ci);

            shifraPasPresjes = Convert.ToInt32(raport.Parameters["filterFormatNumri"].Value);
            caktoFormatinENumrave();
            parameter20.Value = shifraPasPresjes.ToString();
        }
        private void caktoFormatinENumrave()
        {
            xrLabel27.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel19.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel21.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel22.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel27.XlsxFormatString = xrLabel19.XlsxFormatString = xrLabel21.XlsxFormatString = xrLabel22.XlsxFormatString = xrLabel24.XlsxFormatString= xrLabel30.XlsxFormatString 
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
            xrTableCell7.Text = rm.GetString("labelFilterAvancuarLlojDok", ci);
            xrTableCell13.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell8.Text = rm.GetString("labelFilterAvancuarNrDok", ci);
            xrTableCell9.Text = rm.GetString("labelFilterKryesorDtDokumenti", ci);
            xrTableCell10.Text = rm.GetString("labelFilterAvancuarDtRegjistrimi", ci);
            xrTableCell12.Text = rm.GetString("labelKartela", ci);
            xrTableCell14.Text = rm.GetString("labelNjesia", ci);
            xrTableCell15.Text = rm.GetString("labelSasia", ci);
            xrTableCell17.Text = rm.GetString("labelVlefta", ci);
            xrTableCell16.Text = rm.GetString("labelCmimi", ci);
            xrLabel23.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel20.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
           xrLabel36.Text = rm.GetString("labelLogoIMB", ci);
           xrLabel28.Text = rm.GetString("labelRaportMagazina", ci);
        }
     
    }
}
