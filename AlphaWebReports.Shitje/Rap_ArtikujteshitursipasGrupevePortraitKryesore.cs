using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_ArtikujteshitursipasGrupevePortraitKryesore : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ArtikujteshitursipasGrupevePortraitKryesore(){InitializeComponent();}

        
        int shifraPasPresjes = 0;
        public Rap_ArtikujteshitursipasGrupevePortraitKryesore(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, report)
        {

        }
        public Rap_ArtikujteshitursipasGrupevePortraitKryesore(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
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
            xrLabel59.Text = raport.Parameters[5].Description;
            parameter6.Value = raport.Parameters[5].Value;
            xrLabel14.Text = raport.Parameters[6].Description;
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
            
            shifraPasPresjes =Convert.ToInt32(raport.Parameters[38].Value);
            caktoFormatinENumrave();
        }

        private decimal shuma;
        private decimal sasia;
        private void caktoFormatinENumrave()
        {
            lblsasia.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel9.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            lbltotalipatvshmezbr.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
         xrLabel5.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel31.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel7.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel8.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel6.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel14.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
       xrLabel4.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel10.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel11.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";

            xrLabel7.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel31.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            lblsasia.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel9.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel8.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel6.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            lbltotalipatvshmezbr.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel14.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel5.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel10.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel4.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel11.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";

            xrLabel31.XlsxFormatString =
                 xrLabel10.XlsxFormatString =
                 xrLabel6.XlsxFormatString =
                 xrLabel5.XlsxFormatString =
                        xrLabel11.XlsxFormatString =
                lbltotalipatvshmezbr.XlsxFormatString
                = xrLabel4.XlsxFormatString
                = lblsasia.XlsxFormatString
                = xrLabel7.XlsxFormatString =
                xrLabel8.XlsxFormatString =
           xrLabel14.XlsxFormatString =
              xrLabel9.XlsxFormatString =
            0.ToString("N" + shifraPasPresjes);
        }

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
                shuma += Convert.ToDecimal(GetCurrentColumnValue("VLEFTAPATVSH")) * Convert.ToDecimal(GetCurrentColumnValue("KURSI"));
                sasia += Convert.ToDecimal(GetCurrentColumnValue("SASIA"));
            }
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


            xrLabel13.Text = rm.GetString("lblTitullRapGrupkryesore", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel17.Text = rm.GetString("labelKodi", ci);
            xrLabel18.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel19.Text = rm.GetString("labelNjesia", ci);
            xrLabel20.Text = rm.GetString("labelSasia", ci);
            xrLabel21.Text = rm.GetString("labelCmimi", ci);
            xrLabel22.Text = rm.GetString("labelZbritjeAnalitike", ci);
            xrLabel23.Text = rm.GetString("labelVleftapaTVSH", ci);
            xrLabel32.Text = rm.GetString("labelZbritjaTotale", ci);
            xrLabel24.Text = rm.GetString("labelTVSH", ci);
            xrLabel25.Text = rm.GetString("labelVleftameTVSH", ci);
            xrLabel16.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel12.Text = rm.GetString("labelLogoIMB", ci);
        }

        
        private void Rap_ArtikujteshitursipasGrupevePortraitKryesore_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);

        }
    }
}
