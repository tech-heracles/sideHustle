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
    public partial class Rap_KartelaArtikulliMagazina_NrRend : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_KartelaArtikulliMagazina_NrRend(){InitializeComponent();} 
        int shifraPasPresjes = 0;
        public Rap_KartelaArtikulliMagazina_NrRend(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_KartelaArtikulliMagazina_NrRend(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
            parameter6.Value = raport.Parameters[5].Value;
            parameter7.Value = raport.Parameters[6].Value;
            parameter8.Value = raport.Parameters[7].Value;
            parameter9.Value = raport.Parameters[8].Value;
            parameter10.Value = raport.Parameters[9].Value;
            parameter11.Value = raport.Parameters[10].Value;
            parameter12.Value = raport.Parameters[13].Value;
            parameter14.Value = raport.Parameters[14].Value;
            parameter15.Value = raport.Parameters[15].Value;
            parameter13.Value = raport.Parameters[11].Value;
            DegaAdministrative.Value = raport.Parameters[12].Value;
            if(raport.Parameters.Count == 18)
                shifraPasPresjes = Convert.ToInt32(raport.Parameters[17].Value);
            else if (raport.Parameters.Count == 19)
                shifraPasPresjes = Convert.ToInt32(raport.Parameters[18].Value);
            caktoFormatinENumrave();

            parameterShifraPasPresjes.Value = shifraPasPresjes.ToString();
            parameterIdNderm.Value = idNdermarrje;
            parameterIdPerdoruesi.Value = idPerdoruesi;
            parameterIdViti.Value = idViti;

        }

        private void caktoFormatinENumrave()
        {
            xrLabel71.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel73.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel84.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel83.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";
            xrLabel36.XlsxFormatString = xrLabel37.XlsxFormatString = xrLabel38.XlsxFormatString = xrLabel39.XlsxFormatString = xrLabel40.XlsxFormatString = xrLabel41.XlsxFormatString = xrLabel42.XlsxFormatString = xrLabel44.XlsxFormatString = xrLabel71.XlsxFormatString = xrLabel73.XlsxFormatString = xrLabel74.XlsxFormatString = xrLabel84.XlsxFormatString = xrLabel83.XlsxFormatString = xrLabel88.XlsxFormatString
                = 0.ToString("N" + shifraPasPresjes);
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

            xrTableCell8.Text = rm.GetString("labelRaportiNrDok", ci);
            xrTableCell9.Text = rm.GetString("labelRaportiDtDok", ci);
            xrTableCell10.Text = rm.GetString("labelFilterAvancuarMagazina", ci);
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
