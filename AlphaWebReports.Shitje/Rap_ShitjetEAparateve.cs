using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Resources;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_ShitjetEAparateve : DevExpress.XtraReports.UI.XtraReport
    {

        public Rap_ShitjetEAparateve()
        {
            InitializeComponent();
        }
        
        public Rap_ShitjetEAparateve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report)
        {

        }
        public Rap_ShitjetEAparateve(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            xrLabel54.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel55.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            xrLabel56.Text = raport.Parameters[2].Description;
            parameter3.Value = raport.Parameters[2].Value;
            xrLabel57.Text = raport.Parameters[3].Description;
            parameter4.Value = raport.Parameters[3].Value;
            xrLabel58.Text = raport.Parameters[4].Description;
            parameter5.Value = raport.Parameters[4].Value;
            LlojiDok.Value = raport.Parameters[5].Value;
            xrLabel67.Text = raport.Parameters[5].Description;
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
            //if (GetCurrentColumnValue("SHFAQ_ROLE") != null)
            //{
            //    if (GetCurrentColumnValue("SHFAQ_ROLE").ToString() == "JO")
            //    {
            //        xrLabel4.Visible = false;
            //        xrLabel5.Visible = false;
            //        xrLabel69.Visible = false;
            //        xrLabel15.Visible = false;
                    
            //    }
            //}
            //else
            //{
            //    xrLabel4.Visible = false;
            //    xrLabel5.Visible = false;
            //    xrLabel69.Visible = false;
            //    xrLabel15.Visible = false;
                
            //}
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (GetCurrentColumnValue("SHFAQ_ROLE") != null)
            //{
            //    if (GetCurrentColumnValue("SHFAQ_ROLE").ToString() == "JO")
            //    {
                     
            //        xrTableCell13.Visible = false;
            //        xrTableCell17.Visible = false;
            //    }
            //}
            //else
            //{
               
            //    xrTableCell13.Visible = false;
            //    xrTableCell17.Visible = false;
            //}
        }

        private void Rap_ShitjetEAparateve_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);

        }
    }
}
