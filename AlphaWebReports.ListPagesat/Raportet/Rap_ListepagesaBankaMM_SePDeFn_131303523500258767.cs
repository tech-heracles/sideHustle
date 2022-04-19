using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.ListPagesat.Raportet
{
    public partial class Rap_ListepagesaBankaMM_SePDeFn_131303523500258767 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_ListepagesaBankaMM_SePDeFn_131303523500258767() { InitializeComponent(); }
        int shifraPasPresjes = 0;
        public Rap_ListepagesaBankaMM_SePDeFn_131303523500258767(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_ListepagesaBankaMM_SePDeFn_131303523500258767(CultureInfo ci, int idNdermarrje, int idViti, XtraReport report)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            shifraPasPresjes = Convert.ToInt16(report.Parameters["filterFormatNumri"].Value);
            caktoFormatinENumrave();
        
    }
        private void caktoFormatinENumrave()
        {

            xrLabel9.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
            xrLabel9.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel9.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            
        }


    

      
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel11.Text = rm.GetString("labelHeader", ci);
            xrLabel12.Text = rm.GetString("RaportListëpagesaBankaTitulli", ci);
            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel41.Text = rm.GetString("labelRaportiNr", ci);

            xrLabel14.Text = rm.GetString("labelEmriMbiemriBashkuar", ci);
            xrTableCell11.Text = rm.GetString("labelRaportPagaNeto", ci);
            xrLabel45.Text = rm.GetString("labelNumri", ci);
            xrLabel8.Text = rm.GetString("labelShuma", ci);


        }


    }

}

