using DevExpress.XtraReports.UI;
using System;
using System.Globalization;
using System.Resources;
namespace AlphaWebReports.RaportetDs.Banka.Raporte
{
    public partial class RAP_BANKA_GJENDJAPERMBLEDHUR_NIVEL_RAPORTUES : DevExpress.XtraReports.UI.XtraReport
    {
		public RAP_BANKA_GJENDJAPERMBLEDHUR_NIVEL_RAPORTUES(){InitializeComponent();} 

        private ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));


        public RAP_BANKA_GJENDJAPERMBLEDHUR_NIVEL_RAPORTUES(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public RAP_BANKA_GJENDJAPERMBLEDHUR_NIVEL_RAPORTUES(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
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
            parameter10.Value = raport.Parameters["filterKompania"].Value;
            parameterIdNderm.Value = idNdermarrje;
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportGjendjaPermbBankaNivelRaportuesTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel7.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell1.Text = rm.GetString("labelKodi", ci);
            xrTableCell12.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell15.Text = rm.GetString("filterRaportiNumerLlogarie", ci);
            xrTableCell2.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrTableCell4.Text = rm.GetString("labelRaportGjendjaeMeparshme", ci);
            xrTableCell17.Text = rm.GetString("lblRaportiArketuar", ci);
            xrTableCell22.Text = rm.GetString("filterPaguar", ci);
            xrTableCell24.Text = rm.GetString("lblRaportiGjendjaFund", ci);
            xrTableCell6.Text = rm.GetString("lblRaportiGjendjaMB", ci);
            xrTableCell18.Text = rm.GetString("koloneLoginNdermarrjeNdermarrja", ci);
            xrTableCell20.Text = rm.GetString("lblRaportiTotaliBankave", ci) + ":";
        }
 
    }
}
