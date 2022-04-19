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
    public partial class Rap_MaturimFurnitori : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_MaturimFurnitori(){InitializeComponent();} 
        public Rap_MaturimFurnitori(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }

        public Rap_MaturimFurnitori(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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
            xrLabel12.Text = rm.GetString("RaportMaturimFurnitoriTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel33.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell25.Text = rm.GetString("filterMonedha", ci);
            xrLabel3.Text = rm.GetString("labelRaportiFaturime", ci);
            xrTableCell20.Text = rm.GetString("labelRaportiLloji", ci);
            xrTableCell8.Text = rm.GetString("labelRaportiPagesaCashBank", ci);
            xrTableCell13.Text = rm.GetString("labelRaportiNrDok", ci);
            xrTableCell12.Text = rm.GetString("labelRaportiDtDok", ci);
            xrTableCell11.Text = rm.GetString("labelRaportDtMaturimi", ci);
            xrTableCell15.Text = rm.GetString("labelRaportiVleftaCash", ci);
            xrTableCell14.Text = rm.GetString("labelRaportiVleftaBank", ci);
            xrTableCell7.Text = rm.GetString("labelRaportiFurnitori", ci);
            xrTableCell16.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell17.Text = rm.GetString("labelRaportiTejkaluarCash" , ci);
            xrTableCell9.Text = rm.GetString("labelRaportiTejkaluarBank" , ci);
            xrTableCell19.Text = rm.GetString("labelRaportiNrDok", ci);
            xrTableCell18.Text = rm.GetString("labelRaportiDtDok", ci);
            xrTableCell22.Text = rm.GetString("labelVlefta", ci);
            xrTableCell21.Text = rm.GetString("labelRaportiProgresivi", ci);
            xrTableCell10.Text = rm.GetString("labelRaportiSaldo", ci);
        }     
    }
}
