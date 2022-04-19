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
    public partial class RAP_ARKA_DITARIKLASIK_Kastrati : DevExpress.XtraReports.UI.XtraReport
    {
		public RAP_ARKA_DITARIKLASIK_Kastrati(){InitializeComponent();} 
        private CultureInfo ci;
        private ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));


        public RAP_ARKA_DITARIKLASIK_Kastrati(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public RAP_ARKA_DITARIKLASIK_Kastrati(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            this.ci = ci;
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
            parameter6.Value = raport.Parameters[5].Value;
            parameter7.Value = raport.Parameters[6].Value;
            parameter8.Value = raport.Parameters[7].Value;
            parameter9.Value = raport.Parameters[8].Value;
            parameter10.Value = raport.Parameters[10].Value;
            parameter11.Value = raport.Parameters[11].Value;
            DegaAdministrative.Value = raport.Parameters[9].Value;
            parameterRLB.Value = rm.GetString("labelRaportLlogBanke", ci) + ":";
            parameterRLA.Value = rm.GetString("labelRaportLlogArke", ci) + ":";
            parameterFilterBanka.Value = rm.GetString("cmbboxItemFilterAvancBanka", ci) + ":";
            parameterFilterArka.Value = rm.GetString("cmbboxItemFilterAvancArka", ci) + ":";

        }


        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));

          
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
            xrTableCell14.Text = rm.GetString("labelRaportiTotali", ci) + ":";
            xrTableCell19.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
            xrTableCell15.Text = rm.GetString("labelRaportNumer", ci);
        }

    }
}
