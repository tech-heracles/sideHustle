using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_Lista_AnalitikElementeveSipasRajoneve_OSHEE : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Lista_AnalitikElementeveSipasRajoneve_OSHEE(){InitializeComponent();} 
        public Rap_Lista_AnalitikElementeveSipasRajoneve_OSHEE(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report, param.IdPerdoruesi , param.IdViti)
        {

        }
        public Rap_Lista_AnalitikElementeveSipasRajoneve_OSHEE(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport, int idPerdoruesi, int idViti)
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

            lblTitullRpt.Text = rm.GetString("titullRaporti", ci);
            lblGrupSipasNJQV.Text = rm.GetString("lblrajoni", ci);
            xrLabel1.Text = rm.GetString("lblrajoni", ci);
            xrLabel2.Text = rm.GetString("lblStacionKabine", ci);
            xrLabel4.Text = rm.GetString("lblLayerelem", ci);
            xrLabel5.Text = rm.GetString("cmbCmimeArtikulliCaptionKodi", ci);
            xrLabel6.Text = rm.GetString("cmbCmimeArtikulliCaptionPershkrimi", ci);
            xrLabel7.Text = rm.GetString("labelFilterAvancuarStatusi", ci);
            xrLabel24.Text = rm.GetString("lblDhenaTeknike", ci);
            xrLabel8.Text = rm.GetString("filterSeriali", ci);

        }
    }
}
