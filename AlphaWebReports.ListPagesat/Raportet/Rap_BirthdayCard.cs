using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.ListPagesat.Raportet
{
    public partial class Rap_BirthdayCard : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_BirthdayCard(){InitializeComponent();} 
        public Rap_BirthdayCard(AlphaWebReports.Common.ParametraRaporti param, XtraReport report): this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report) {  }
        
        public Rap_BirthdayCard(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
        }
        private void xrPictureBox1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.ImageUrl = "~/images/Signature.png";
        }

     
        private void xrPictureBox2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //xrPictureBox2.ImageUrl = "~/images/logo.jpg";
        }

        private void xrLabel5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if(GetCurrentColumnValue("EMER") != null)
            {
                xrLabel5.Text = "Dear " + GetCurrentColumnValue("EMER").ToString() + " ";
            }
        }

        private void xrLabel3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("DATELINDJA") == null) return;
            DateTime dtl = DateTime.Parse(Convert.ToString(GetCurrentColumnValue("DATELINDJA")));
            xrLabel3.Text = dtl.ToString("dd MMM yyyy");
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
          
        }

        private void Rap_BirthdayCard_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox2.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
        }
    }
}
