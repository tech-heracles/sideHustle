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
    public partial class Rap_SituacionIFurnitorevePageseFundit : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_SituacionIFurnitorevePageseFundit(){InitializeComponent();} 

        public Rap_SituacionIFurnitorevePageseFundit(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, report)
        {

        }
        public Rap_SituacionIFurnitorevePageseFundit(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
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


            xrLabel12.Text = rm.GetString("RaportSituacioniFurnitoritTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel20.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell17.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell7.Text = rm.GetString("labelNrRendor", ci);
            xrTableCell8.Text = rm.GetString("labelKodi", ci);
            xrTableCell9.Text = rm.GetString("labelRaportEmertimiFurnitorit", ci);
            xrTableCell10.Text = rm.GetString("labelRaportiNrLlogari", ci);
            xrTableCell12.Text = rm.GetString("labelRaportShumaDebi", ci);
            xrTableCell13.Text = rm.GetString("labelRaportShumaKredi", ci);
            xrTableCell14.Text = rm.GetString("labelRaportDetyrimi", ci);
            xrTableCell15.Text = rm.GetString("labelRaportLimiti", ci);
            xrTableCell16.Text = rm.GetString("labelRaportPesha", ci);
            xrTableCell2.Text = rm.GetString("labelRaportiDataEPagesesSeFundit", ci);
        }   

    }
}
