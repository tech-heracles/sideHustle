using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Prodhimi.Raportet
{
    public partial class Rap_AnalizaProduktiSipasRecepturave : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_AnalizaProduktiSipasRecepturave(){InitializeComponent();} 
        private decimal vlefta = 0, sasi = 0, kostototalemag = 0, kostototale = 0;
        
        public Rap_AnalizaProduktiSipasRecepturave(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_AnalizaProduktiSipasRecepturave(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);

            xrLabel52.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel57.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            xrLabel63.Text = raport.Parameters[2].Description;
            parameter3.Value = raport.Parameters[2].Value;
            xrLabel46.Text = raport.Parameters[3].Description;
            parameter4.Value = raport.Parameters[3].Value;
            xrLabel60.Text = raport.Parameters[4].Description;
            parameter5.Value = raport.Parameters[4].Value;

            xrLabel64.Text = raport.Parameters[5].Description;
            parameter6.Value = raport.Parameters[5].Value;


            xrLabel48.Text = raport.Parameters[6].Description;
            parameter7.Value = raport.Parameters[6].Value; 
            xrLabel26.Text = raport.Parameters[7].Description;
            parameter8.Value = raport.Parameters[7].Value; 
            xrLabel28.Text = raport.Parameters[8].Description;
            parameter9.Value = raport.Parameters[8].Value; 
            xrLabel31.Text = raport.Parameters[9].Description;
            parameter10.Value = raport.Parameters[9].Value;
        }



        private void xrLabel11_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (sasi == 0)
                e.Result = 0;
            else
                e.Result = vlefta / sasi;
            e.Handled = true;
        }

        private void xrLabel11_SummaryReset(object sender, EventArgs e)
        {
            vlefta = 0;
            sasi = 0;
        }

        private void xrLabel11_SummaryRowChanged(object sender, EventArgs e)
        {
            vlefta += Convert.ToDecimal(GetCurrentColumnValue("kostototalerec"));
            sasi += Convert.ToDecimal(GetCurrentColumnValue("sasiaktrec"));
        }

        private void xrLabel9_SummaryReset(object sender, EventArgs e)
        {

        }

        private void xrLabel18_SummaryReset(object sender, EventArgs e)
        {
            kostototale += kostototalemag;
            kostototalemag = 0;
        }

        private void xrLabel18_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = kostototalemag;
            e.Handled = true;
        }

        private void xrLabel9_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            kostototalemag += Convert.ToDecimal(e.CalculatedValues.Count > 0 ? e.CalculatedValues[0] : 0);
        }

        private void Rap_AnalizaProduktiSipasRecepturave_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);

        }

        private void xrLabel21_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = kostototale;
            e.Handled = true;

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
            xrLabel12.Text = rm.GetString("RaportAnalizaProduktitSipasRecepturaveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel36.Text = rm.GetString("labelKodi", ci);
            xrLabel38.Text = rm.GetString("labelNjesia", ci);
            xrLabel37.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel39.Text = rm.GetString("labelRaportSasiaPlan", ci);
            xrLabel13.Text = rm.GetString("labelRaportSasiaAktuale", ci);
            xrLabel40.Text = rm.GetString("labelRaportVariencaSasise", ci);
            xrLabel42.Text = rm.GetString("labelRaportKosto", ci);

            xrLabel1.Text = rm.GetString("labelRaportVleraAktuale", ci);
            xrLabel19.Text = rm.GetString("labelRaportTotaliMagazines", ci);
            
            xrLabel23.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel20.Text = rm.GetString("filterMagazina", ci);
        }
    }
}
