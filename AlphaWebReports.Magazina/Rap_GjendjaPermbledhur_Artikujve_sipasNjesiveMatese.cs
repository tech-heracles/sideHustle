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
    public partial class Rap_GjendjaPermbledhur_Artikujve_sipasNjesiveMatese : DevExpress.XtraReports.UI.XtraReport
    {           
		public Rap_GjendjaPermbledhur_Artikujve_sipasNjesiveMatese(){InitializeComponent();} 
        string windowWidth = "";
        int shifraPasPresjes = 0;

        public Rap_GjendjaPermbledhur_Artikujve_sipasNjesiveMatese(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.IdGjuha, report)
        {

        }
        public Rap_GjendjaPermbledhur_Artikujve_sipasNjesiveMatese(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, int idRaporti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            parameter3.Value = raport.Parameters[3].Value;
            parameter4.Value = raport.Parameters[4].Value;
            parameter5.Value = raport.Parameters[7].Value;
            parameter7.Value = raport.Parameters[9].Value;
            parameter6.Value = raport.Parameters[10].Value;
            parameter9.Value = raport.Parameters[1].Value;
            windowWidth = Convert.ToString(raport.Parameters[6].Value);
            shifraPasPresjes = Convert.ToInt32(raport.Parameters["filterFormatNumri"].Value);
           
            caktoFormatinENumrave();
        }
        private void caktoFormatinENumrave()
        {
            xrTableCell6.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell7.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell8.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell9.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell10.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell11.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell12.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell5.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell1.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            //xrTableCell8.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel10.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";

            xrTableCell6.XlsxFormatString =
               xrTableCell7.XlsxFormatString =
               xrTableCell8.XlsxFormatString
               = xrTableCell9.XlsxFormatString
               = xrTableCell10.XlsxFormatString =
               xrTableCell11.XlsxFormatString =
               xrTableCell12.XlsxFormatString =
               xrTableCell5.XlsxFormatString =
               xrTableCell1.XlsxFormatString =
               xrLabel10.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);

            xrLabel10.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
           // xrLabel17.Text = rm.GetString("RaportGjendjaPermbledhurArtikujveTitulli", ci);
           // FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
           // xrLabel1.Text = rm.GetString("labelKartela", ci);
           // xrLabel26.Text = rm.GetString("labelFilterAvancuarKodbari", ci);
           // xrLabel2.Text = rm.GetString("labelRaportiPershkrimi", ci);
           // xrLabel3.Text = rm.GetString("labelNjesia", ci);
           // xrLabel4.Text = rm.GetString("labelRaportGjendjaeMbartur", ci);
           // xrLabel5.Text = rm.GetString("labelRaportHyrje", ci);
           // xrLabel6.Text = rm.GetString("labelRaportDalje", ci);
           // xrLabel7.Text = rm.GetString("labelRaportGjendje", ci);
           // xrLabel8.Text = rm.GetString("labelRaportKosto", ci);
           // xrLabel9.Text = rm.GetString("labelVlefta", ci);
           //// xrLabel23.Text = rm.GetString("labelRaportiShuma", ci);
           // xrLabel32.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel11.Text = rm.GetString("lblRaportTotali", ci);
        }
    }
}
