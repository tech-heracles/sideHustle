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
    public partial class Rap_MemoAnnualDeclaration : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_MemoAnnualDeclaration(){InitializeComponent();} 
        
        public Rap_MemoAnnualDeclaration(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje)
        {

        }
        public Rap_MemoAnnualDeclaration(System.Globalization.CultureInfo ci, int idNdermarrje)
        {
            InitializeComponent();
        }
       

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void Rap_MemoAnnualDeclaration_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);

            System.Data.DataSet ds = ((System.Data.DataSet)this.DataSource);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
             GroupHeader1.Visible = false;
             Detail.Visible = false;
             PageFooter.Visible = false;
            }
        }

        private void xrLabel10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string personi = GetCurrentColumnValue("Gjinia") == null ? " " : GetCurrentColumnValue("Gjinia").ToString() + GetCurrentColumnValue("EmerMbiemer").ToString();
            string vitiFiltruar = GetCurrentColumnValue("vitiFiltruar") == null ? "0" : GetCurrentColumnValue("vitiFiltruar").ToString();
            xrLabel10.Text = $"Vërtetojmë se {personi} Vodafone Albania Sh.A për periudhën tatimore {vitiFiltruar} ka  pasur të ardhurat e mëposhtme: ";
        }

        private void xrLabel16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string pageTatim = GetCurrentColumnValue("pageTatim") == null ? "0" : GetCurrentColumnValue("pageTatim").ToString();
            ((XRLabel)sender).Text = $"2. Të ardhurat bruto të tatueshme janë: {pageTatim}";
        }

        private void xrLabel15_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string vlera = GetCurrentColumnValue("VLERA") == null ? "0" : GetCurrentColumnValue("VLERA").ToString();
            ((XRLabel)sender).Text = $"1. Të ardhurat bruto nga paga, apo shpërblime të tjera nga marrëdhëniet e punësimit janë: {vlera}";
        }

        private void xrLabel18_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string taksePage = GetCurrentColumnValue("taksePage") == null ? "0" : GetCurrentColumnValue("taksePage").ToString();
            ((XRLabel)sender).Text = $"3. Tatimi mbi pagën i mbajtur në burim është: {taksePage}";
        }
    }
}
