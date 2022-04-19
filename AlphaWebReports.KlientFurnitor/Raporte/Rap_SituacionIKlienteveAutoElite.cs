using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.KlientFurnitor.Raporte
{
    public partial class Rap_SituacionIKlienteveAutoElite : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_SituacionIKlienteveAutoElite(){InitializeComponent();} 


        public Rap_SituacionIKlienteveAutoElite(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, report)
        {

        }
        public Rap_SituacionIKlienteveAutoElite(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
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
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
             xrLabel12.Text = rm.GetString("RaportSituacioniKlientitSipasMakinaveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel20.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell21.Text = rm.GetString("labelRaportiTotali", ci);
            //   xrLabel10.Text = rm.GetString("labelNrRendor", ci);
            xrTableCell1.Text = rm.GetString("labelKodi", ci);
            xrTableCell3.Text = rm.GetString("labelRaportEmertimiKlientit", ci);
            xrTableCell2.Text = rm.GetString("labelRaportiNrLlogari", ci);
            xrTableCell6.Text = rm.GetString("labelRaportShumaDebi", ci);
            xrTableCell8.Text = rm.GetString("labelRaportShumaKredi", ci);
            xrTableCell7.Text = rm.GetString("labelRaportDetyrimi", ci);
            xrTableCell5.Text = rm.GetString("labelRaportModeli", ci);
            xrTableCell4.Text = rm.GetString("labelRaportTarga", ci);
        }
    }
}
