using System;
using System.Drawing;
using System.Globalization;
using DevExpress.XtraReports.UI;
using System.Resources;

namespace AlphaWebReports.RaportetDs.Raporte_Menaxheriale
{
    public partial class Rap_FushatShteseHistorikuVeprimeve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FushatShteseHistorikuVeprimeve(){InitializeComponent();} 
       
        public Rap_FushatShteseHistorikuVeprimeve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_FushatShteseHistorikuVeprimeve(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
           
            parameter1.Value = raport.Parameters[0].Value;
            llojfushashtese.Value = raport.Parameters["llojFushaShtese"].Value;
            parameter8.Value = raport.Parameters["filterModeliFushaShtese"].Value;
            parameter11.Value = raport.Parameters["filterObjekteGIS"].Value;
            parameter12.Value = raport.Parameters["filterDtDok"].Value;
            parameter13.Value = raport.Parameters["filterDtRegj"].Value;
        }
           /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel17.Text = rm.GetString("lblRapHistorikuVleraveFushaveShtese", ci);
            xrTableCell12.Text = rm.GetString("cmbCmimeArtikulliCaptionKodi", ci);
            xrTableCell13.Text = rm.GetString("cmbCmimeArtikulliCaptionPershkrimi", ci);
            xrTableCell7.Text = rm.GetString("ItemModeli", ci);
            xrTableCell8.Text = rm.GetString("lblDateAktizivimi", ci);
            xrTableCell10.Text = rm.GetString("lblFusha", ci);
            xrTableCell11.Text = rm.GetString("lblVlerat", ci);
            xrTableCell14.Text = rm.GetString("filterDateKrijimi", ci);
            xrTableCell9.Text = rm.GetString("loginLoginPerdoruesi", ci);
        }
    }
}
