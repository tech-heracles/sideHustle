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
    public partial class Rap_DorezimAparati_loan : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_DorezimAparati_loan(){InitializeComponent();} 
 
        public Rap_DorezimAparati_loan(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_DorezimAparati_loan(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

           
        }
        private void xrLabel35_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            if (GetCurrentColumnValue("kodloan") != null)
            {

                xrLabel35.Text = String.Format("{0:n2}", (GetCurrentColumnValue("CMIMI")).ToString());
            }
        }

        private void xrLabel16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
          
            if (GetCurrentColumnValue("kodloan") != null)
            {
               
                xrLabel16.Text = String.Format("{0:n2}", (GetCurrentColumnValue("CMIMI")).ToString());
            }
        }
           

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel2.Text = rm.GetString("RaportFormaDorezimitTeAparatitTitulli", ci);
            xrLabel14.Text = rm.GetString("labelAparatiIRiparuar", ci);
            xrLabel1.Text = rm.GetString("labelAparatIZevendesuar", ci);
            xrLabel3.Text = rm.GetString("labelDefektiJashteKushteveTeGarancise", ci);

            xrLabel38.Text = rm.GetString("label_IMEI_i_ri", ci);
            xrLabel31.Text = rm.GetString("labelDataZevendesimit", ci);
            xrLabel36.Text = rm.GetString("labelFilterAvancuarTipi", ci);

            xrLabel7.Text = rm.GetString("labelAparatiZevendesues", ci);
            xrLabel8.Text = rm.GetString("labelKthyer", ci);
            xrLabel9.Text = rm.GetString("labelJoKthyer", ci);
            xrLabel10.Text = rm.GetString("labelJoKthyer", ci);
            xrLabel32.Text = rm.GetString("labelIMEI", ci);
            xrLabel33.Text = rm.GetString("labelCmimi", ci);
            xrLabel15.Text = rm.GetString("labelCmimi", ci);
            xrLabel17.Text = rm.GetString("labelAksesoret", ci);
            xrLabel19.Text = rm.GetString("labelRaportiPaguar", ci);
            xrLabel21.Text = rm.GetString("labelJoPaguar", ci);
            

            xrLabel26.Text = rm.GetString("labelLexoniShenimetDhePlotesojiniPerkatesisht", ci) + ":";
            xrLabel23.Text = rm.GetString("labelAparatiCelularPersonal", ci) + ":";
            xrLabel24.Text = rm.GetString("labelVertetojSeVodafone", ci);

            xrLabel27.Text = rm.GetString("labelFirmaEKlientit", ci);
            xrLabel28.Text = rm.GetString("labelPranuarNga", ci);

        }

    }
}
