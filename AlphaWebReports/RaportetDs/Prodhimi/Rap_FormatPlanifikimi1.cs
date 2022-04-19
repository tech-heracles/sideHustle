using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;


namespace AlphaWebReports.RaportetDs.Prodhimi
{
    public partial class Rap_FormatPlanifikimi1 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatPlanifikimi1(){InitializeComponent();}
        
        public Rap_FormatPlanifikimi1(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_FormatPlanifikimi1(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {

            InitializeComponent();
            EmrateLabelave(ci);
            
        }



        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel12.Text = rm.GetString("RaportUrdherPuneTitulli", ci);
            xrLabel1.Text = rm.GetString("labelNr", ci);
            xrLabel10.Text = rm.GetString("labelNrPorosie", ci);
            xrLabel13.Text = rm.GetString("LabelDtPorosie", ci);
            xrLabel15.Text = rm.GetString("labelRaportKlienti", ci) + ":";
            xrLabel17.Text = rm.GetString("LabelDtFillimit", ci);
            xrLabel19.Text = rm.GetString("LabelDtPerfundimit", ci);
            xrLabel23.Text = rm.GetString("labelKodi", ci);
            xrLabel24.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel25.Text = rm.GetString("labelNjesia", ci);
            xrLabel26.Text = rm.GetString("labelRaportSasiaPlan", ci);
            xrLabel27.Text = rm.GetString("labelRaportiShenime", ci);
            xrLabel22.Text = rm.GetString("labelOrePuneProdhimi", ci);
            xrLabel36.Text = rm.GetString("labelOrePuneMontimi", ci);
            xrLabel39.Text = rm.GetString("labelFormatPergjegjesiProdhimit", ci);
            xrLabel41.Text = rm.GetString("labelFormatMagazinieri", ci);
            xrLabel50.Text = rm.GetString("labelFormatPreventivuesi", ci);
             xrLabel43.Text = rm.GetString("labelRaportiNrLower", ci);
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void Rap_FormatPlanifikimi1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
        }
    }
}
