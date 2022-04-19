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
    public partial class RapVeprimetEKlientitPerDatatMeTeFundit : DevExpress.XtraReports.UI.XtraReport
    {
        public RapVeprimetEKlientitPerDatatMeTeFundit()
        {
            InitializeComponent();
        }

        public RapVeprimetEKlientitPerDatatMeTeFundit(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public RapVeprimetEKlientitPerDatatMeTeFundit(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel2.Text = rm.GetString("VeprimeteKlientitperdatatmetefundit", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);

        }
    }
}
