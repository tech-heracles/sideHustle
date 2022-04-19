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
    public partial class Rap_ListepagesaBankaMin : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ListepagesaBankaMin(){InitializeComponent();} 
        int shifraPasPresjes = 0;

        public Rap_ListepagesaBankaMin(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_ListepagesaBankaMin(CultureInfo ci, int idNdermarrje, int idViti, XtraReport report)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            shifraPasPresjes = Convert.ToInt16(report.Parameters["filterFormatNumri"].Value);
            caktoFormatinENumrave();
        }

        private void caktoFormatinENumrave()
        {
            xrTableCell13.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell13.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
             
            xrTableCell19.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrTableCell19.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrTableCell19.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
        }

        

    
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel11.Text = rm.GetString("labelRaportEmri", ci);
            xrLabel12.Text = rm.GetString("RaportListëpagesaBankaTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel41.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell7.Text = rm.GetString("labelRaportEmri", ci);
            xrLabel45.Text = rm.GetString("labelRaportAtesia", ci);
            xrTableCell11.Text = rm.GetString("labelRaportMbiemri", ci);
            xrLabel14.Text = rm.GetString("labelRaportNrSigurimeve", ci);
            xrLabel47.Text = rm.GetString("labelRaportNrLlogariseRrjedhese", ci);
            xrLabel15.Text = rm.GetString("labelRaportPagaNeto", ci);
            xrTableCell3.Text = rm.GetString("labelRaportiTotali", ci);


        }
       
    }
}
