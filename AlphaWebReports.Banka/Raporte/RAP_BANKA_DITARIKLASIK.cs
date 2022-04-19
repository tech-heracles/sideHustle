using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Banka.Raporte
{
    public partial class RAP_BANKA_DITARIKLASIK : DevExpress.XtraReports.UI.XtraReport
    {
		public RAP_BANKA_DITARIKLASIK(){InitializeComponent();} 
        private ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
        
        public RAP_BANKA_DITARIKLASIK(AlphaWebReports.Common.ParametraRaporti param, XtraReport report)
        {
            InitializeComponent();
            EmrateLabelave(param.Ci);
            parameterRapKomision.Value = rm.GetString("labelRaportKomision", param.Ci) + ":";
            parameterFilterAvanc.Value = rm.GetString("cmbboxItemFilterAvancBanka", param.Ci) + ":";
            parameterFilterAvancArka.Value = rm.GetString("cmbboxItemFilterAvancArka", param.Ci) + ":";
            parameterRapLlogArke.Value = rm.GetString("labelRaportLlogArke", param.Ci) + ":";
            parameterRapLlogBanke.Value = rm.GetString("labelRaportLlogBanke", param.Ci) + ":";

        }

      

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportDitariKlasikTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel7.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell1.Text = rm.GetString("labelRaportiLloji", ci);
            xrTableCell12.Text = rm.GetString("labelRaportData", ci);
            xrTableCell2.Text = rm.GetString("labelRaportiPershkrimi", ci);
            //xrTableCell21.Text = rm.GetString("labelRaportVlera", ci);
            xrTableCell3.Text = rm.GetString("labelRaportKomision", ci);
            xrTableCell17.Text = rm.GetString("labelRaportVlArketuar", ci);
            xrTableCell22.Text = rm.GetString("labelRaportVlPaguar", ci);
            xrTableCell24.Text = rm.GetString("labelRaportiProgresivi", ci);
            xrTableCell5.Text = rm.GetString("labelFilterAvancuarMonedha", ci) + ":";
            xrTableCell35.Text = rm.GetString("labelRaportLlogBanke", ci) + ":";
            xrTableCell6.Text = rm.GetString("labelRaportiNr", ci) + ":";
            xrTableCell49.Text = rm.GetString("labelRaportGjendjePerpara", ci);
            xrLabel19.Text = rm.GetString("labelRaportiTotali", ci) + ":";
            //xrLabel20.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
            xrTableCell15.Text = rm.GetString("labelRaportNumer", ci);
        }
    }
}
