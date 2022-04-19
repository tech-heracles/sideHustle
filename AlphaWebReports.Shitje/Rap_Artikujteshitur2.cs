using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_Artikujteshitur2 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Artikujteshitur2(){InitializeComponent();}
        
        public Rap_Artikujteshitur2(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) : this(param.IdNdermarrje,  report) { }
        public Rap_Artikujteshitur2(int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
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
            xrLabel15.Text = raport.Parameters[6].Description;
            parameter7.Value = raport.Parameters[6].Value;
            llojiArtikullit.Text = raport.Parameters[7].Description;
            parameter8.Value = raport.Parameters[7].Value;
             KlasaArtikllitLabel.Text = raport.Parameters[10].Description;
            KlasaArtikulli.Value = raport.Parameters[10].Value;
            xrLabel37.Text = raport.Parameters[11].Description;
            parameter9.Value = raport.Parameters[11].Value;
            xrLabel35.Text = raport.Parameters[12].Description;
            parameter10.Value = raport.Parameters[12].Value;
            xrLabel33.Text = raport.Parameters[13].Description;
            parameter11.Value = raport.Parameters[13].Value;
            xrLabel41.Text = raport.Parameters[15].Description;
            parameter12.Value = raport.Parameters[15].Value;
            xrLabel39.Text = raport.Parameters[16].Description;
            parameter13.Value = raport.Parameters[16].Value;
            xrLabel43.Text = raport.Parameters[17].Description;
            parameter14.Value = raport.Parameters[17].Value;
            degaAdminLabel.Text = raport.Parameters[14].Description;
            DegaAdministrative.Value = raport.Parameters[14].Value;
            xrLabel44.Text = raport.Parameters[18].Description;
            parameter15.Value = raport.Parameters[18].Value;
            xrPictureBox1.ImageUrl = @"/images/RaporteLogo.bmp";
        }

        private decimal shuma;
        private decimal sasia;
        private void xrLabel9_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (sasia != 0)
                e.Result = shuma / sasia;
            else e.Result = shuma;
            e.Handled = true;
        }

        private void xrLabel31_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {

        }

        private void xrLabel30_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //e.Result = (Convert.ToDouble(lbltotalipatvshmezbr.Summary.GetResult()) / Convert.ToDouble(lblsasia.Summary.GetResult())).ToString();
            //e.Handled = true;
        }

        private void xrLabel12_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrLabel31_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (xrLabel8.Summary.GetResult() != null)
            //{
            //    xrLabel31.Text = ((Convert.ToDouble(xrLabel8.Summary.GetResult()) / Convert.ToDouble(xrLabel5.Summary.GetResult())) * 100).ToString();
            //}
        }

        private void xrLabel12_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (xrLabel6.Summary.GetResult() != null)
            //{
            //    xrLabel12.Text = ((Convert.ToDouble(xrLabel6.Summary.GetResult()) / Convert.ToDouble(xrLabel11.Summary.GetResult())) * 100).ToString();
            //}
        }

        private void xrLabel9_SummaryReset(object sender, EventArgs e)
        {
            shuma = 0;
            sasia = 0;
        }

        private void xrLabel9_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("VLEFTAPATVSH") != null)
            {
                shuma += Decimal.Parse(GetCurrentColumnValue("VLEFTAPATVSH").ToString()) * Decimal.Parse(GetCurrentColumnValue("KURSI").ToString());
                sasia += Decimal.Parse(GetCurrentColumnValue("SASIA").ToString());
            }
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void Rap_Artikujteshitur2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);


        }
    }
}
