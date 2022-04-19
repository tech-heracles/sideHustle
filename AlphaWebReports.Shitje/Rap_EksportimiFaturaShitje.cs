using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Resources;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_EksportimiFaturaShitje : DevExpress.XtraReports.UI.XtraReport
    {


        public Rap_EksportimiFaturaShitje()
        {
            InitializeComponent();
        }
        
        public Rap_EksportimiFaturaShitje(AlphaWebReports.Common.ParametraRaporti param, XtraReport report)
            :this(param.Ci, param.IdNdermarrje,report) { }
        public Rap_EksportimiFaturaShitje(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            xrLabel54.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel55.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void xrTableCell14_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void Rap_EksportimiFaturaShitje_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);


        }
    }
}
