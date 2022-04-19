using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_KartelaArtikulliMagazinaFormat2 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_KartelaArtikulliMagazinaFormat2(){InitializeComponent();} 
        public Rap_KartelaArtikulliMagazinaFormat2(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_KartelaArtikulliMagazinaFormat2(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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
            xrTableCell7.Text = rm.GetString("labelLlojDokumenti", ci);

            xrTableCell8.Text = rm.GetString("labelRaportiNrDok", ci);
            xrTableCell10.Text = rm.GetString("labelRaportiDtDok", ci);
            xrTableCell9.Text = rm.GetString("labelFilterAvancuarMagazina", ci);
            xrTableCell11.Text = rm.GetString("labelNjesia", ci);
            xrTableCell12.Text = rm.GetString("labelRaportHyrje", ci);
            xrTableCell13.Text = rm.GetString("labelCmimi", ci);
            xrTableCell14.Text = rm.GetString("labelRaportVleraHyrje", ci);
            xrTableCell15.Text = rm.GetString("labelRaportDalje", ci);
            xrTableCell16.Text = rm.GetString("labelCmimi", ci);
            xrTableCell17.Text = rm.GetString("labelRaportVleraDalje", ci);
            xrTableCell18.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
            xrTableCell19.Text = rm.GetString("labelRaportVlera", ci);
            xrLabel69.Text = rm.GetString("labelLogoIMB", ci);
        }

    }
}
