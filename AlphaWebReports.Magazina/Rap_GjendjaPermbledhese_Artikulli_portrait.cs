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
    public partial class Rap_GjendjaPermbledhese_Artikulli_portrait : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_GjendjaPermbledhese_Artikulli_portrait(){InitializeComponent();} 

        int shifraPasPresjes = 0;    
        public Rap_GjendjaPermbledhese_Artikulli_portrait(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, report)
        {

        }
        public Rap_GjendjaPermbledhese_Artikulli_portrait(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
            System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
         
            shifraPasPresjes = Convert.ToInt32(raport.Parameters["filterFormatNumri"].Value);
            caktoFormatinENumrave();
        }
        private void caktoFormatinENumrave()
        {
            xrLabel13.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel14.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel15.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel16.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel20.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel21.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel22.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
            System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel17.Text = rm.GetString("RaportGjendjaPermbledhurArtikujveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel1.Text = rm.GetString("labelKartela", ci);
            xrLabel26.Text = rm.GetString("labelFilterAvancuarKodbari", ci);
            xrLabel2.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel3.Text = rm.GetString("labelNjesia", ci);
            xrLabel4.Text = rm.GetString("labelRaportGjendjaeMbartur", ci);
            xrLabel5.Text = rm.GetString("labelRaportHyrje", ci);
            xrLabel6.Text = rm.GetString("labelRaportDalje", ci);
            xrLabel7.Text = rm.GetString("labelRaportGjendje", ci);
            xrLabel8.Text = rm.GetString("labelRaportKosto", ci);
            xrLabel9.Text = rm.GetString("labelVlefta", ci);
            xrLabel23.Text = rm.GetString("labelRaportiShuma", ci);
            xrLabel32.Text = rm.GetString("labelLogoIMB", ci);
        }
    }
}
