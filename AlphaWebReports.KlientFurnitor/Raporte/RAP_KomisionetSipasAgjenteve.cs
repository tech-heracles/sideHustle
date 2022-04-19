using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Resources;
using System.Globalization;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.KlientFurnitor.Raporte
{
    public partial class RAP_KomisionetSipasAgjenteve : DevExpress.XtraReports.UI.XtraReport
    {
		public RAP_KomisionetSipasAgjenteve(){InitializeComponent();} 
        CultureInfo ci;

        public RAP_KomisionetSipasAgjenteve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, report)
        {

        }
        public RAP_KomisionetSipasAgjenteve(CultureInfo ci, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            this.ci = ci;
        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrTableCell13.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell11.Text = rm.GetString("lblRaportAgjenti", ci);
            xrTableCell12.Text = rm.GetString("label_Emri", ci);
            xrTableCell14.Text = rm.GetString("lblEmerKlienti", ci);
            Titulli.Text = rm.GetString("lblRaportKomisionetAgjenteveSipasKlienteve", ci);
            xrLabel20.Text = rm.GetString("labelLogoIMB", ci);
            lblFiltra.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell15.Text = rm.GetString("labelRaportArketime", ci);
            xrTableCell16.Text = rm.GetString("lblPagesa", ci);
            xrTableCell1.Text = rm.GetString("lblRaportiTotaliBankaveArkave", ci);
            xrTableCell4.Text = rm.GetString("lblsimbolperq", ci);
            xrTableCell17.Text = rm.GetString("labelRaportKomision", ci);
            xrTableCell37.Text = rm.GetString("lblKodKlienti", ci);
            xrTableCell39.Text = rm.GetString("labelQyteti", ci);
            xrTableCell42.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
        }

    }
}
