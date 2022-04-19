using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Resources;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.B_Buxheti.Raporte
{
    public partial class RapPlanifikimitMiratimitRialokimitTeBuxhetit : DevExpress.XtraReports.UI.XtraReport
    {
        public RapPlanifikimitMiratimitRialokimitTeBuxhetit(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }

        public RapPlanifikimitMiratimitRialokimitTeBuxhetit(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel1.Text = rm.GetString("RapPlanifikimitMiratimitRialokimitTeBuxhetit", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);

        }
    }
}
