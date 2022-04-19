using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.Magazina
{
    public partial class Rap_KartelaArtikulliMagazina_SePDeFn_132061205179799426 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_KartelaArtikulliMagazina_SePDeFn_132061205179799426()
        {
            InitializeComponent();
        }
        public Rap_KartelaArtikulliMagazina_SePDeFn_132061205179799426(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
         this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_KartelaArtikulliMagazina_SePDeFn_132061205179799426(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }


        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));


            xrLabel17.Text = rm.GetString("RaportKartelaArtikullitTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel5.Text = rm.GetString("labelKartela", ci) + ":";
            xrLabel11.Text = rm.GetString("labelRaportiPershkrimi", ci) + ":";
            xrLabel7.Text = rm.GetString("filterKodbari", ci);
            xrLabel13.Text = rm.GetString("filterArkaBankaEmer", ci);
            xrLabel9.Text = rm.GetString("labelRaportMetodaKostos", ci);
            xrLabel15.Text = rm.GetString("labelFilterAvancuarGrupi", ci) + ":";
            xrLabel18.Text = rm.GetString("labelLlojDokumenti", ci);

            xrLabel19.Text = rm.GetString("labelRaportiNrDok", ci);
            xrLabel22.Text = rm.GetString("labelRaportiDtDok", ci);
            xrLabel23.Text = rm.GetString("labelFilterAvancuarMagazina", ci);
            xrLabel24.Text = rm.GetString("labelNjesia", ci);
            xrLabel35.Text = rm.GetString("labelRaportHyrje", ci);
            xrLabel21.Text = rm.GetString("labelCmimi", ci);
            xrLabel20.Text = rm.GetString("labelRaportVleraHyrje", ci);
            xrLabel26.Text = rm.GetString("labelRaportDalje", ci);
            xrLabel25.Text = rm.GetString("labelCmimi", ci);
            xrLabel27.Text = rm.GetString("labelRaportVleraDalje", ci);
            xrLabel28.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
            xrLabel34.Text = rm.GetString("labelRaportVlera", ci);
            xrLabel69.Text = rm.GetString("labelLogoIMB", ci);
        }
    }
}

