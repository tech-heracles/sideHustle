using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.Arka.Raporte
{
    public partial class Rap_VleratDitoreteArkes : DevExpress.XtraReports.UI.XtraReport
    {

        
        public Rap_VleratDitoreteArkes()
        {
            InitializeComponent();
        }

        public Rap_VleratDitoreteArkes(AlphaWebReports.Common.ParametraRaporti param, XtraReport report)
                : this(param.Ci, param.IdNdermarrje, report) { }


        public Rap_VleratDitoreteArkes(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            xrlabel100.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel55.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
                       
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }
        
        private void EmraTeLabelave(CultureInfo ci, bool shtoFilterKompania)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            
        }

        private void Rap_VleratDitoreteArkes_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
        }
    }
}
