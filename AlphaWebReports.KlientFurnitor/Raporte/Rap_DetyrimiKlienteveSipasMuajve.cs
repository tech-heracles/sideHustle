using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Reflection;
using System.Resources;
using DevExpress.XtraPivotGrid;
using DevExpress.Data.PivotGrid;
using DevExpress.XtraReports.UI.PivotGrid;


namespace AlphaWebReports.RaportetDs.KlientFurnitor.Raporte
{
    public partial class Rap_DetyrimiKlienteveSipasMuajve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_DetyrimiKlienteveSipasMuajve(){InitializeComponent();} 
        ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
        public Rap_DetyrimiKlienteveSipasMuajve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, report)
        {

        }
        public Rap_DetyrimiKlienteveSipasMuajve(CultureInfo ci, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }

        private void EmrateLabelave(CultureInfo ci)
        {
            xrLabel20.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel1.Text = rm.GetString("lblTitullDetyrimetKlienteveSipasMuajve", ci);

        }

    }
}
