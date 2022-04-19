using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Resources;
using System.Globalization;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatShitjePreventivArton2011 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatShitjePreventivArton2011(){InitializeComponent();} 
        private ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
        public Rap_FormatShitjePreventivArton2011(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatShitjePreventivArton2011(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            xrLabel9.Text = rm.GetString("labelEmriBleresit", ci) + ":";
            xrLabel10.Text = rm.GetString("labelRaportAdresa", ci) + ":";
            xrLabel1.Text = rm.GetString("lblTelefoni", ci) + ":";
            xrLabel2.Text = rm.GetString("labelAdministrimiNIPT", ci);
            xrLabel8.Text = rm.GetString("labelAdministrimiNIPT", ci);
            xrLabel17.Text = rm.GetString("labelAdministrimiNIPT", ci);
            xrLabel5.Text = rm.GetString("labelRaportAdresa", ci) + ":";
            xrLabel3.Text = rm.GetString("labelTerritor", ci);
            xrLabel6.Text = rm.GetString("lblRaportTarga", ci) + ":";
            xrLabel7.Text = rm.GetString("lblRaportOraeFurnizimit", ci) ;
            xrLabel20.Text = rm.GetString("labelAdministrimiTel", ci) ;
            xrLabel19.Text = rm.GetString("labelAdministrimiFax", ci) ;
            xrLabel24.Text = rm.GetString("lblRaportFatureTitull", ci);
            xrLabel25.Text = rm.GetString("labelNumriSerise", ci) + ":";
            xrLabel26.Text = rm.GetString("labelRaportNumriFatures", ci) + ":";
            xrLabel27.Text = rm.GetString("labelDataeDokumentit", ci);
            xrLabel28.Text = rm.GetString("labelDataefunditelikuidimit", ci);
            xrLabel34.Text = rm.GetString("labelTotalipaTVSHpazbritje", ci);
            xrLabel35.Text = rm.GetString("labelTotaliZbritjeLine", ci);
            xrLabel36.Text = rm.GetString("labelTotaliZbritjeFature", ci);
            xrLabel37.Text = rm.GetString("labelRaportiLineAmount", ci)+":";
            xrLabel43.Text = rm.GetString("labelRaportiTotali", ci) + " " + rm.GetString("labelRaportiVatIdentifier", ci) + ":";
            xrLabel44.Text = rm.GetString("labelPertupaguar", ci);
        }
        
    }
}
