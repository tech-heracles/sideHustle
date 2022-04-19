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
    public partial class Rap_GjendjaMagDet_Vodafone2 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_GjendjaMagDet_Vodafone2(){InitializeComponent();} 
        int shifraPasPresjes = 0;      
        public Rap_GjendjaMagDet_Vodafone2(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci,param.IdSubRaporti, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.IdGjuha,param.GuidString, report)
        {

        }

        public Rap_GjendjaMagDet_Vodafone2(CultureInfo ci,int idSubRaporti, int idNdermarrje, int idViti, int idPerdoruesi, int idGjuha, string guidString, DevExpress.XtraReports.UI.XtraReport raport)
        {        
            InitializeComponent();
            EmrateLabelave(ci);
            caktoFormatinENumrave();
            shifraPasPresjes = Convert.ToInt32(raport.Parameters["filterFormatNumri"].Value);
        }

        private void caktoFormatinENumrave()
        {
            xrLabel33.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel6.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel3.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel23.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel7.XlsxFormatString = xrLabel24.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));


            xrLabel12.Text = rm.GetString("RaportGjendjaArtikujveMeIMEI", ci);

            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel13.Text = rm.GetString("labelKartela", ci);
            xrLabel21.Text = rm.GetString("labelRaportiPershkrimi", ci);     
            xrLabel20.Text = rm.GetString("labelDetajim1", ci);
            xrLabel42.Text = rm.GetString("labelFilterAvancuarKodbari", ci);
            xrLabel155.Text = rm.GetString("labelRaportGjendjaeMeparshme", ci);
            xrLabel18.Text = rm.GetString("labelRaportSasiHyrje", ci);
            xrLabel17.Text = rm.GetString("labelRaportSasiaDalje", ci);
            xrLabel16.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
            xrLabel70.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel2.Text = rm.GetString("lblRaportMagazina", ci);
            xrLabel14.Text = rm.GetString("labelFilterAvancuarStatusi", ci);


        }
     
    }
}
