using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Blerje
{
    public partial class Rap_Eksportimi_faturave_blerjeve : DevExpress.XtraReports.UI.XtraReport
    {
      

        public Rap_Eksportimi_faturave_blerjeve()
        {
            InitializeComponent();
        }
        
        public Rap_Eksportimi_faturave_blerjeve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report)
            :this(param.Ci, param.IdNdermarrje,param.IdViti,report) { }

        public Rap_Eksportimi_faturave_blerjeve(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            xrLabel54.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel55.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
        }

            private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }
             

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
           ResourceManager rm = new ResourceManager("Resources.Strings",
                     System.Reflection.Assembly.Load("App_GlobalResources"));
           
            
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel65.Text = rm.GetString("labelLogoIMB", ci);
           
        }

        private void Rap_Eksportimi_faturave_blerjeve_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);

        }
    }
}