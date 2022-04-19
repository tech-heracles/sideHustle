using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_MagazinaGjendjaZero : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_MagazinaGjendjaZero(){InitializeComponent();} 
        private double shumagrup = 0.0;
        private double shumaTotale = 0.0;

        string windowWidth = "";
        
        public Rap_MagazinaGjendjaZero(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.IdNdermarrje, param.IdViti, param.ScopeID, report)
        {

        }
        public Rap_MagazinaGjendjaZero(int idNdermarrje, int idViti, string scopeID, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
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
            xrLabel59.Text = raport.Parameters[5].Description;
            parameter6.Value = raport.Parameters[5].Value;
            xrLabel60.Text = raport.Parameters[6].Description;
            parameter7.Value = raport.Parameters[6].Value;
            xrLabel40.Text = raport.Parameters[7].Description;
            parameter8.Value = raport.Parameters[7].Value;
            xrLabel35.Text = raport.Parameters[8].Description;
            parameter9.Value = raport.Parameters[8].Value; 
            xrLabel45.Text = raport.Parameters[9].Description;
            parameter10.Value = raport.Parameters[9].Value;
            xrLabel49.Text = raport.Parameters[11].Description;
            parameter11.Value = raport.Parameters[11].Value;
            degaAdminLabel.Text = raport.Parameters[12].Description;
            DegaAdministrative.Value = raport.Parameters[12].Value;
            windowWidth = Convert.ToString(raport.Parameters[10].Value);

        }

        private void xrLabel10_AfterPrint(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("vd") != null)
            {
                if (xrLabel10.Text == "")
                    xrLabel10.Text = "0";
                shumagrup = shumagrup + Convert.ToDouble(xrLabel10.Text);
                shumaTotale = shumaTotale + Convert.ToDouble(xrLabel10.Text);
            }
        }

        private void xrLabel9_AfterPrint(object sender, EventArgs e)
        {
            shumagrup = 0.0;
        }

        private void xrLabel32_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel32.Text = String.Format("{0:#,#.00}", shumagrup);
        }

        private void TotaliMagazinave_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TotaliMagazinave.Text = String.Format("{0:#,#.00}", shumaTotale);
        }

        private void xrLabel1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);

            xrLabel1.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('RaportiShpejte.aspx?scopeID=" + parametraRaporti.ScopeID + "&idraporti=55&filterKartela=" + GetCurrentColumnValue("KODARTIKULLI") + "&filterMagazina=" + GetCurrentColumnValue("MAGAZINA") + "&printo=0&Sesioni=true')";
            xrLabel1.Target = "_self";

        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void Rap_MagazinaGjendjaZero_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
        }
    }


}
