using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;


namespace AlphaWebReports.RaportetDs.Prodhimi
{
    public partial class Rap_FormatPlanifikimiAnoria : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatPlanifikimiAnoria(){InitializeComponent();}
        
        public Rap_FormatPlanifikimiAnoria(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_FormatPlanifikimiAnoria(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel12.Text = rm.GetString("lblRaportFatureTitull", ci);
            xrLabel1.Text = rm.GetString("lblRaportPermase", ci);
            xrLabel3.Text = rm.GetString("labelRaportProdukti", ci) + ":";
            xrLabel4.Text = rm.GetString("lblRaportAdresaAnoria", ci);
            xrLabel6.Text = rm.GetString("labelRaportiPershkrimi", ci) + ":";
            xrLabel8.Text = rm.GetString("labelSasia", ci) + ":";
            xrLabel10.Text = rm.GetString("labelNrPorosie", ci);
            xrLabel13.Text = rm.GetString("LabelDtPorosie", ci);
            xrLabel15.Text = rm.GetString("labelRaportKlienti", ci) + ":";
            xrLabel23.Text = rm.GetString("labelKodi", ci);
            xrLabel24.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel25.Text = rm.GetString("labelNjesia", ci);
            xrLabel27.Text = rm.GetString("lblRaportGjeresia", ci);
            xrLabel42.Text = rm.GetString("labelRaportAdresa", ci) + ":";
            xrLabel50.Text = rm.GetString("labelRaportTelefon", ci) + ":";
            xrLabel52.Text = rm.GetString("labelAdministrimiNIPT", ci);
            xrLabel53.Text = rm.GetString("labelAdministrimiNIPT", ci);
            xrLabel5.Text = rm.GetString("labelAdministrimiTel", ci);
            xrLabel8.Text = rm.GetString("labelAdministrimiFax", ci);
            xrLabel9.Text = rm.GetString("lblRaportCel", ci) + ":";
            xrLabel14.Text = rm.GetString("lblRaportWebAnoria", ci);
            xrLabel26.Text = rm.GetString("labelSasia", ci);
            xrLabel29.Text = rm.GetString("labelSasia", ci);
            xrLabel10.Text = rm.GetString("lblTelAnoria", ci);
            xrLabel11.Text = rm.GetString("lblFaxAnoria");
            xrLabel13.Text = rm.GetString("lblCelAnoria");
            
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
            
        }
    }
}
