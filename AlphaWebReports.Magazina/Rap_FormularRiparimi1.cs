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
    public partial class Rap_FormularRiparimi1 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormularRiparimi1(){InitializeComponent();} 
     
        public Rap_FormularRiparimi1(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormularRiparimi1(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            
            xrLabel2.Text = rm.GetString("RaportFormularSherbimiPasShitjesTitulli", ci);
            xrLabel1.Text = rm.GetString("labelEmri_i_klientit", ci);
            xrLabel3.Text = rm.GetString("labelNumerKontakti", ci);
            xrLabel5.Text = rm.GetString("labelTipiAparatit", ci);
            xrLabel6.Text = rm.GetString("labelTipiDefektit", ci);
            xrLabel7.Text = rm.GetString("lblRaportNumriSerial", ci);
            xrLabel9.Text = rm.GetString("labelDataBlerjes", ci);
            xrLabel10.Text = rm.GetString("labelIMEI", ci);
            xrLabel11.Text = rm.GetString("labelAksesore", ci) + ":";

            xrLabel18.Text = rm.GetString("labelFirmaEKlientit", ci);
            xrLabel19.Text = rm.GetString("labelPranuarNga", ci);

            xrLabel23.Text = rm.GetString("labelLexoniMeVemendjeShenimetMePoshte", ci) + ":";
            xrLabel22.Text = rm.GetString("labelVertetojDeklarojSe", ci);
            
        }

    }
}
