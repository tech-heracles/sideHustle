using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class FatureTatimoreShitjeEVP : DevExpress.XtraReports.UI.XtraReport
    {
		public FatureTatimoreShitjeEVP(){InitializeComponent();} 

        public FatureTatimoreShitjeEVP(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public FatureTatimoreShitjeEVP(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);

        }

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel36.Text = rm.GetString("evpRaifeisenALL", ci);
            xrLabel37.Text = rm.GetString("evpRaifeisenEURO", ci);
            xrLabel38.Text = rm.GetString("evpIntesaALL", ci);
            xrLabel39.Text = rm.GetString("evpIntesaEURO", ci);
            xrLabel41.Text = rm.GetString("evpBKTALL", ci);
            xrLabel40.Text = rm.GetString("evpBKTEURO", ci);
            xrLabel43.Text = rm.GetString("evpBKTnrLLogarie", ci);
        }

    }
}
