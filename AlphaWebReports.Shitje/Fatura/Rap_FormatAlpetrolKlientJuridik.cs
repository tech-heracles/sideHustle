using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Reflection;
using System.Resources;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatAlpetrolKlientJuridik : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatAlpetrolKlientJuridik(){InitializeComponent();} 
        public Rap_FormatAlpetrolKlientJuridik(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatAlpetrolKlientJuridik(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel1.Text = rm.GetString("labelRaportiFaturuesi", ci);
            xrLabel13.Text = rm.GetString("labelRaportiFaturuesiAlpetrol", ci);
            xrLabel12.Text = rm.GetString("labelNrFiskalAlpetroljuridik", ci) ;
            xrLabel11.Text = rm.GetString("labelTVSHALPETROL", ci);
            xrLabel10.Text = rm.GetString("labelNrBiznesitAlpetrol", ci);
            xrLabel9.Text = rm.GetString("label_Klienti", ci);
            xrLabel4.Text = rm.GetString("labelNrFiskal", ci)+":";
            
            
            xrLabel16.Text = rm.GetString("labelRaportiTABELAT", ci) + ":";
            xrLabel15.Text = rm.GetString("labelRaportiSHOFERI", ci) + ":";
            xrLabel20.Text = rm.GetString("labelNrIdentifikues", ci) + ":";
            xrLabel22.Text = rm.GetString("labelRaportNumriFatures", ci) + ":";
            xrLabel21.Text = rm.GetString("labelRaportiDateFaturimi", ci) + ":";
            xrLabel24.Text = rm.GetString("labelAfatiPageses", ci) + ":";
            xrLabel41.Text = rm.GetString("lblRaportVerejtje", ci) + ":";
            xrLabel40.Text = rm.GetString("labelRaportiAlpetrolTegjithapagesat", ci) ;
            xrLabel39.Text = rm.GetString("labelRaportiAlpetrolDERIVA", ci) ;
            xrLabel38.Text = rm.GetString("labelRaportiAlpetrolLlog1", ci) ;
            xrLabel37.Text = rm.GetString("labelRaportiAlpetrolshpk", ci);
            xrLabel36.Text = rm.GetString("labelRaportiAlpetrolLlog2", ci);
            xrLabel47.Text = rm.GetString("labelALPETROLADRES1", ci);
            xrLabel46.Text = rm.GetString("labelALPETROLADRES2", ci);
            xrLabel48.Text = rm.GetString("lblAlpetrolnrllogariTEB", ci);
        }
    }
}
