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
    public partial class RAP_BANKA_DITARITOTAL : DevExpress.XtraReports.UI.XtraReport
    {
		public RAP_BANKA_DITARITOTAL(){InitializeComponent();} 

    
      
        public RAP_BANKA_DITARITOTAL(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public RAP_BANKA_DITARITOTAL(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
              parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[5].Value;
            parameter6.Value = raport.Parameters[6].Value;
            parameter7.Value = raport.Parameters[7].Value;
            parameter8.Value = raport.Parameters[8].Value;
            parameter9.Value = raport.Parameters[9].Value;
            parameter10.Value = raport.Parameters[11].Value;
            parameter11.Value = raport.Parameters[12].Value;
            DegaAdministrative.Value = raport.Parameters[10].Value;
            parameter13.Value = raport.Parameters[4].Value;
        }


        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportDitariTotalTitullijoKapitale", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell1.Text = rm.GetString("labelLloj", ci);
            xrTableCell12.Text = rm.GetString("labelDateDok", ci);
            xrTableCell15.Text = rm.GetString("cmbItemRaportNumri", ci);
            xrTableCell9.Text = rm.GetString("labelDtRegj", ci);
            xrTableCell10.Text = rm.GetString("labelKunderParti", ci);
            xrTableCell3.Text = rm.GetString("labelRaportiEmertimi", ci);
            xrTableCell2.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell21.Text = rm.GetString("labelLlojPag", ci);
            xrTableCell13.Text = rm.GetString("labelRaportNrFature", ci);
            xrTableCell14.Text = rm.GetString("labelDtFature", ci);
            xrTableCell17.Text = rm.GetString("labelRaportVlArketuar", ci);
            xrTableCell22.Text = rm.GetString("labelRaportVlPaguar", ci);
            xrTableCell24.Text = rm.GetString("labelTotaliArketuar", ci);
            xrTableCell11.Text = rm.GetString("labelTotaliPaguar", ci);
            xrTableCell5.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrTableCell36.Text = rm.GetString("labelRaportGjendjePerpara", ci) + ":";
            xrTableCell41.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell53.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
            xrLabel7.Text = rm.GetString("labelLogoIMB", ci);
        }

    }
}
