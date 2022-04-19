using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.Arka.Raporte
{
    public partial class RAP_ARKA_DITARIKLASIK : DevExpress.XtraReports.UI.XtraReport
    {

		public RAP_ARKA_DITARIKLASIK(){InitializeComponent();} 
        public RAP_ARKA_DITARIKLASIK(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public RAP_ARKA_DITARIKLASIK(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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

          
            xrLabel12.Text = rm.GetString("RaportDitariKlasikTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel7.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell1.Text = rm.GetString("labelRaportiLloji", ci);
            xrTableCell12.Text = rm.GetString("labelRaportData", ci);
            xrTableCell2.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell17.Text = rm.GetString("labelRaportVlArketuar", ci);
            xrTableCell22.Text = rm.GetString("labelRaportVlPaguar", ci);
            xrTableCell24.Text = rm.GetString("labelRaportiProgresivi", ci);
            xrTableCell5.Text = rm.GetString("labelFilterAvancuarMonedha", ci) + ":";
            xrTableCell35.Text = rm.GetString("labelRaportLlogBanke", ci) + ":";
            xrTableCell36.Text = rm.GetString("labelRaportGjendjePerpara", ci);
            xrTableCell23.Text = rm.GetString("labelRaportiTotali", ci) + ":";
            xrTableCell27.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
            xrTableCell15.Text = rm.GetString("labelRaportNumer", ci);

        }
    }
}
