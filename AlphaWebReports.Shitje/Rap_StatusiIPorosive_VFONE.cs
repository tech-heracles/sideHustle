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
  
    public partial class Rap_StatusiIPorosive_VFONE : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_StatusiIPorosive_VFONE(){InitializeComponent();}
        
        public Rap_StatusiIPorosive_VFONE(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report)
        {

        }
        public Rap_StatusiIPorosive_VFONE(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();

            parameter1.Value = raport.Parameters["IdNdermarje"].Value;
            parameter2.Value = raport.Parameters["filterMagazina"].Value;
            parameter3.Value = raport.Parameters["filterkodifikimartP"].Value;
            parameter4.Value = raport.Parameters["filterDtDok"].Value;
            parameter5.Value = raport.Parameters["filterKartela"].Value;
            parameter7.Value = raport.Parameters["filterKompania"].Value;
            EmraTeLabelave(ci);
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }


        private void EmraTeLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel20.Text = rm.GetString("labelRaportiDataEPorosise", ci);
            xrLabel46.Text = rm.GetString("labelRaportiDataEShitjes", ci);
            xrLabel17.Text = rm.GetString("labelRaportiShitesiQeBeriOrderin", ci);
            xrLabel16.Text = rm.GetString("labelRaportiShitesiQeBeriShitjen", ci);
            xrLabel14.Text = rm.GetString("labelFilterAvancuarDyqan", ci);
            xrLabel13.Text = rm.GetString("labelRaportiKompania", ci);
            xrLabel11.Text = rm.GetString("labelRaportTel", ci);
            xrLabel1.Text = rm.GetString("labelRaportiKodProdukti", ci);
            xrLabel10.Text = rm.GetString("labelRaportProdukti", ci);
            xrLabel4.Text = rm.GetString("labelCmimi", ci);
            xrLabel9.Text = rm.GetString("labelFilterAvancuarStatusi", ci);
            xrLabel12.Text = rm.GetString("TitullRaportiStatusiVFONEPOROSI", ci);

            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel52.Text = rm.GetString("labelFooterNdermarrja", ci) + ":";
           // xrLabel1.Text = rm.GetString("labelFilterNengrupArtikulli", ci) + ":";
            xrlabel100.Text = rm.GetString("labelRaportiDtDok", ci) + ":";
            xrLabel70.Text = rm.GetString("labelFilterGrupArtikulli", ci) + ":";
            xrLabel38.Text = rm.GetString("labelRaportiKompania", ci) + ":";
            xrLabel55.Text = rm.GetString("labelFilterAvancuarDyqan", ci) + ":";
            xrLabel18.Text = rm.GetString("labelRaportProdukti", ci) + ":";
        }

        private void Rap_StatusiIPorosive_VFONE_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);

        }
    }
}
