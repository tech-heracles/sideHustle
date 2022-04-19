using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;


namespace AlphaWebReports.RaportetDs.Prodhimi
{
    public partial class Rap_UrdherPuneMeReceptura : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_UrdherPuneMeReceptura() { InitializeComponent(); }
      
        public Rap_UrdherPuneMeReceptura(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_UrdherPuneMeReceptura(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {

            InitializeComponent();
            EmrateLabelave(ci);
        

        }



        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel12.Text = rm.GetString("RaportUrdherPuneTitulliUpperCase", ci);
            xrLabel1.Text = rm.GetString("labelNrUpperCase", ci);
            xrLabel10.Text = rm.GetString("labelKodi", ci) + ":";
            xrLabel13.Text = rm.GetString("labelRaportiPershkrimi", ci) + ":";
            xrLabel15.Text = rm.GetString("labelSasia", ci) + ":";
            xrLabel17.Text = rm.GetString("LabelDtFillimitUpperCase", ci);
            xrLabel19.Text = rm.GetString("LabelDtPerfundimitUpperCase", ci);
            xrLabel23.Text = rm.GetString("labelKodi", ci);
            xrLabel24.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel25.Text = rm.GetString("labelNjesia", ci);
            xrLabel26.Text = rm.GetString("labelRaportSasiaPlan", ci);
            xrLabel27.Text = rm.GetString("labelRaportiDiferenca", ci);
            xrLabel43.Text = rm.GetString("labelRaportiNrLower", ci);
            xrLabel3.Text = rm.GetString("LabelBazaMateriale", ci);
            xrLabel2.Text = rm.GetString("labelRaportSasiaAktuale", ci);
            xrLabel48.Text = rm.GetString("labelRaportProdukti", ci) + ":";
            xrLabel5.Text = rm.GetString("labelFormatPreventivuesi", ci);

        }


    }
}
