using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.Blerje
{
    public partial class Rap_Blerje_Analitike : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Blerje_Analitike(){InitializeComponent();} 
        public Rap_Blerje_Analitike(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje)
        {

        }
        public Rap_Blerje_Analitike(CultureInfo ci, int idNdermarrje)
        {
            InitializeComponent();
            EmrateLabelave(ci);

        }
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            //FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel1.Text = rm.GetString("titullRaportiBlerjeAnalitike", ci);
            xrTableCell1.Text = rm.GetString("labelAsetKodi", ci);
            xrTableCell2.Text = rm.GetString("labelRaportProdukti", ci);
            xrTableCell3.Text = rm.GetString("cmbCmimeArtikulliCaptionPershkrimi", ci);
            xrTableCell4.Text = rm.GetString("labelKategoria", ci);
            xrTableCell5.Text = rm.GetString("labelKategoria", ci) + " " + rm.GetString("labelAsetKodi", ci); ;
            xrTableCell6.Text = rm.GetString("lblNjesiaMatjes", ci);
            xrTableCell7.Text = rm.GetString("lblRaportFurnitori", ci);
            xrTableCell8.Text = rm.GetString("lblRaportFurnitori", ci) + " " + rm.GetString("labelAsetKodi", ci); ;
            xrTableCell9.Text = rm.GetString("lblRaportSasia", ci);
            xrTableCell10.Text = rm.GetString("MenuItemMonedhat", ci);
            xrTableCell11.Text = rm.GetString("labelCmimi", ci);
            xrTableCell12.Text = rm.GetString("lblRaportUlje", ci);
            xrTableCell13.Text = rm.GetString("labelRaportVlefta", ci);
            xrTableCell14.Text = rm.GetString("labelTVSH", ci);
            xrTableCell15.Text = rm.GetString("lblRaportiTotaliBankaveArkave", ci);
            xrTableCell16.Text = rm.GetString("lblRaportiLlojDokumenti", ci);
            xrTableCell17.Text = rm.GetString("labelRaportNr", ci) + " " + rm.GetString("msgDokumenti", ci); 
            xrTableCell18.Text = rm.GetString("labelDateDokumenti", ci);
            xrTableCell19.Text = rm.GetString("lblRaportiFormaPageses", ci);
            xrTableCell20.Text = rm.GetString("lblRaportiKantieri", ci);
            xrTableCell21.Text = rm.GetString("lblRaportiVariancaperVolum", ci);
            xrTableCell22.Text = rm.GetString("lblRaportiCertifikateCilesie", ci);
            xrTableCell23.Text = rm.GetString("lblRaportiCertifikateOrigjine", ci);
            xrTableCell24.Text = rm.GetString("lblRaportiVendiBlerjes", ci);
            xrTableCell25.Text = rm.GetString("lblRaportKodiDoganor", ci);
        }
    }
}
