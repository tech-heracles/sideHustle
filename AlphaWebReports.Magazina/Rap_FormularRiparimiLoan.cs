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
    public partial class Rap_FormularRiparimiLoan : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormularRiparimiLoan(){InitializeComponent();} 
        public Rap_FormularRiparimiLoan(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormularRiparimiLoan(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
       

        }
        private void xrLabel35_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            if (GetCurrentColumnValue("KODARTIKULLI") == null && GetCurrentColumnValue("CMIMI") == null)
                return;
            xrLabel29.Text = GetCurrentColumnValue("KODARTIKULLI").ToString();
            xrLabel35.Text = String.Format("{0:n2}", (GetCurrentColumnValue("CMIMI")).ToString()); 
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

            xrLabel26.Text = rm.GetString("labelAparatZevendesuesMeDorezim", ci);
            xrLabel32.Text = rm.GetString("labelIMEI", ci);
            xrLabel33.Text = rm.GetString("labelCmimi", ci);
            xrLabel28.Text = rm.GetString("labelLlojiAparatit", ci);
            xrLabel30.Text = rm.GetString("labelAksesore", ci) + ":";


            xrLabel18.Text = rm.GetString("labelFirmaEKlientit", ci);
            xrLabel19.Text = rm.GetString("labelPranuarNga", ci);

            xrLabel23.Text = rm.GetString("labelLexoniMeVemendjeShenimetMePoshte", ci) + ":";
            xrLabel22.Text = rm.GetString("labelVertetojDeklarojSe", ci);

            xrLabel24.Text = rm.GetString("labelMarrjeNeDorezimAparatiZevendesues", ci) + ":";
            xrLabel24.Text = rm.GetString("labelVertetojSeAparatiZevendesues", ci);


        }

    }
}
