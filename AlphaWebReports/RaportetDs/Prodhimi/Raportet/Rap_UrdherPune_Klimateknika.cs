using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;


namespace AlphaWebReports.RaportetDs.Prodhimi.Raportet
{
    public partial class Rap_UrdherPune_Klimateknika : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_UrdherPune_Klimateknika(){InitializeComponent();} 
        public Rap_UrdherPune_Klimateknika(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_UrdherPune_Klimateknika(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
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
            xrLabel3.Text = rm.GetString("labelRaportProdukti", ci) + ":";
            xrLabel4.Text = rm.GetString("labelKodi", ci) + ":";
            xrLabel6.Text = rm.GetString("labelRaportiPershkrimi", ci) + ":";
            xrLabel8.Text = rm.GetString("labelSasia", ci) + ":";
            xrLabel10.Text = rm.GetString("labelNrPorosie", ci);
            xrLabel13.Text = rm.GetString("LabelDtPorosie", ci);
            xrLabel15.Text = rm.GetString("labelRaportKlienti", ci) + ":";
            xrLabel17.Text = rm.GetString("LabelDtFillimit", ci);
            xrLabel19.Text = rm.GetString("LabelDtPerfundimit", ci);

            xrLabel21.Text = rm.GetString("LabelBazaMateriale", ci);
            xrLabel23.Text = rm.GetString("labelKodi", ci);
            xrLabel24.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel25.Text = rm.GetString("labelNjesia", ci);
            xrLabel26.Text = rm.GetString("labelRaportSasiaPlan", ci);
            xrLabel27.Text = rm.GetString("labelRaportSasiaAktuale", ci);
            xrLabel28.Text = rm.GetString("labelRaportiDiferenca", ci);
            xrLabel22.Text = rm.GetString("labelOrePuneProdhimi", ci);
            xrLabel36.Text = rm.GetString("labelOrePuneMontimi", ci);
            xrLabel39.Text = rm.GetString("labelFormatPergjegjesiProdhimit", ci);
            xrLabel41.Text = rm.GetString("labelFormatMagazinieri", ci);
            xrLabel50.Text = rm.GetString("labelFormatPreventivuesi", ci);
            xrLabel52.Text = rm.GetString("filterMagazina", ci);
        }
     
    }
}
