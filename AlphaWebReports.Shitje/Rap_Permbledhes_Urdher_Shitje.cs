using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_Permbledhes_Urdher_Shitje : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Permbledhes_Urdher_Shitje(){InitializeComponent();} 
        CultureInfo ci;
        public Rap_Permbledhes_Urdher_Shitje(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_Permbledhes_Urdher_Shitje(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            this.ci = ci;
            EmertoLabelat();
        }
        private void EmertoLabelat()
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel1.Text = rm.GetString("lblTitulliRaportPermbledheshUSH", ci);
            xrTableCell1.Text = rm.GetString("labelRaportMenyrePagese", ci);
            xrTableCell2.Text = rm.GetString("lblRaportVleftaPaTvshParaZbritjes", ci);
            xrTableCell4.Text = rm.GetString("lblRaportZbritjaPaTvsh", ci);
            xrTableCell5.Text = rm.GetString("labelTVSH", ci);
            xrTableCell3.Text = rm.GetString("lblRaportVleftaNeto", ci);
            xrLabel2.Text = rm.GetString("lblRaportLevizjaArkes", ci);
            xrLabel3.Text = rm.GetString("lblRaportiTotaliBankaveArkave", ci);
            xrLabel4.Text = rm.GetString("labelVeprimi", ci);
            xrLabel27.Text = rm.GetString("labelLogoIMB", ci);
        }
    }
}
