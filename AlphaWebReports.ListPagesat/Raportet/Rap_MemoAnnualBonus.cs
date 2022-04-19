using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs.ListPagesat.Raportet
{
    public partial class Rap_MemoAnnualBonus : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_MemoAnnualBonus(){InitializeComponent();} 
        public Rap_MemoAnnualBonus(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje)
        {

        }
        
        public Rap_MemoAnnualBonus(System.Globalization.CultureInfo ci, int idNdermarrje)
        {
            InitializeComponent();
        }

        private void Rap_MemoAnnualBonus_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
            System.Data.DataSet ds = ((System.Data.DataSet)this.DataSource);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                GroupHeader1.Visible = false;
                Detail.Visible = false;
                GroupFooter1.Visible = false;
            }
        }

        private void xrLabel17_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if(GetCurrentColumnValue("Bonusi") != null)
            {
                xrLabel17.Text = "is " + GetCurrentColumnValue("Bonusi").ToString() + " ALL.";
            }
            else
            {
                xrLabel17.Text = "is     ALL.";
            }
            
        }

        private void xrLabel20_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("vitiFiltruarParaArdhes") == null) return;

            xrLabel20.Text = $"New hire on or before 2 March {GetCurrentColumnValue("vitiFiltruarParaArdhes").ToString()} will be eligible for bonus pay-out for the Financial Year {GetCurrentColumnValue("vitiFiltruarParaArdhes").ToString()} - {GetCurrentColumnValue("vitiFiltruar").ToString()}.";

        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }
    }
}
