using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraReports.UI.PivotGrid;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_Analize_Cmime_Shitje : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Analize_Cmime_Shitje(){InitializeComponent();}
        

        public Rap_Analize_Cmime_Shitje(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):this(param.Ci, param.IdNdermarrje,param.IdViti, report) { }
        public Rap_Analize_Cmime_Shitje(CultureInfo ci, int idNdermarrje, int idViti, XtraReport report)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
        }

      
        private void EmrateLabelave(CultureInfo ci)
        {

            ResourceManager rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel12.Text = rm.GetString("RaportAnalizecmimeShitjesh", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
        }

    }
}
