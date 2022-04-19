using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Data;
namespace AlphaWebReports.RaportetDs.KlientFurnitor.Raporte
{
    public partial class Rap_SituacionineMonedheneFurnitorit : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_SituacionineMonedheneFurnitorit() { InitializeComponent(); }

        public Rap_SituacionineMonedheneFurnitorit(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen, param.IdViti, param.IdSubRaporti, report)
        {

        }
        public Rap_SituacionineMonedheneFurnitorit(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
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
            //xrLabel12.Text = rm.GetString("RaportSituacioniKlientitTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel20.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell15.Text = rm.GetString("labelKodi", ci);
            xrTableCell16.Text = rm.GetString("labelRaportEmertimiFurnitorit", ci);
            xrTableCell18.Text = rm.GetString("labelRaportiNrLlogari", ci);
            xrTableCell20.Text = rm.GetString("labelRaportiFaturuar", ci);
            xrTableCell21.Text = rm.GetString("labelRaportiPaguar", ci);
            xrTableCell22.Text = rm.GetString("labelRaportDetyrimi", ci);
            xrTableCell19.Text = rm.GetString("lblRaportDetyrimIMeparshem", ci);
        }

    }
}
