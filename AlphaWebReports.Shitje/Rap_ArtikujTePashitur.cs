using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_ArtikujTePashitur : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ArtikujTePashitur(){InitializeComponent();} 
        public Rap_ArtikujTePashitur(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report)
        { }
        public Rap_ArtikujTePashitur(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }

        private void EmrateLabelave(CultureInfo ci)
        {
            var rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel30.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel1.Text = rm.GetString("TitullRaportArtikujPashitur", ci);
            xrLabel31.Text = rm.GetString("labelKodi", ci);
            xrLabel32.Text = rm.GetString("lblRaportKodbar", ci);
            xrLabel33.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel34.Text = rm.GetString("labelNjesia", ci);
            xrLabel35.Text = rm.GetString("lblRaportGjendja", ci);
            xrLabel41.Text = rm.GetString("labelLogoIMB", ci);
        }

    }
}
