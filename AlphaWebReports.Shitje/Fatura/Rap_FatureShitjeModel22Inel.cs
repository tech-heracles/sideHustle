using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureShitjeModel22Inel : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureShitjeModel22Inel()
        { InitializeComponent();} 

        double furnizimeTeTatueshme = 0;
        double furnizimeTePaTatueshme = 0;
        public Rap_FatureShitjeModel22Inel(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitjeModel22Inel(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel1.Text = rm.GetString("RaportFatureShitjeTitulli", ci);
            xrLabel6.Text = rm.GetString("labelRaportNumriFatures", ci);
            xrLabel7.Text = rm.GetString("lblRaportDataFatures", ci);
            xrLabel8.Text = rm.GetString("labelFilterAvancuarNrSerial", ci);
            xrLabel2.Text = rm.GetString("labelRaportSubjektiShites", ci);
            xrLabel75.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel12.Text = rm.GetString("labelNIPT", ci);
            xrLabel74.Text = rm.GetString("labelRaportTel", ci);
            xrLabel3.Text = rm.GetString("labelRaportSubjektiBleres", ci);
            xrLabel48.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel13.Text = rm.GetString("labelNIPT", ci);
            xrLabel14.Text = rm.GetString("labelRaportTel", ci);
            xrLabel15.Text = rm.GetString("lblRaportTransportuesEmri", ci);
            xrLabel17.Text = rm.GetString("labelRaportAdresa", ci) + ":";
            xrLabel18.Text = rm.GetString("labelNIPT", ci) + ":";
            xrLabel19.Text = rm.GetString("lblRaportTarga", ci) + ":";
            xrLabel24.Text = rm.GetString("lblRaportOraFurnizimit", ci) + ":";
            xrTableCell17.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell5.Text = rm.GetString("labelPERSHKRIMI", ci);
            xrTableCell8.Text = rm.GetString("labelVlera_Pa_Tvsh", ci);
            xrTableCell11.Text = rm.GetString("labelTVSH", ci);
            xrTableCell6.Text = rm.GetString("labelVLERA_ME_TVSH", ci);
            xrLabel41.Text = rm.GetString("lblRaportTotaliNe", ci);
            xrLabel30.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel40.Text = rm.GetString("labelKursi", ci);
            xrLabel42.Text = rm.GetString("labelBleresi", ci);
            xrLabel43.Text = rm.GetString("labelShitesi", ci);
            xrLabel37.Text = rm.GetString("labelRaportMagazina", ci) + ":";
            xrLabel69.Text = rm.GetString("labelTransportuesi", ci);
            xrLabel63.Text = rm.GetString("labelRaportEmerMbiemerFirma", ci);
            xrLabel64.Text = rm.GetString("labelRaportEmerMbiemerFirma", ci);
            xrLabel70.Text = rm.GetString("labelRaportEmerMbiemerFirma", ci);
            xrTableCell4.Text = rm.GetString("label_CMIMI", ci);
            xrTableCell3.Text = rm.GetString("label_SASIA", ci);
            xrTableCell10.Text = rm.GetString("labelNjesiaUpperCase", ci);
            xrTableCell17.Text = rm.GetString("labelRaportNRKARTELE", ci);
            xrLabel45.Text = rm.GetString("NrLlogBankeLabel", ci);
            xrLabel52.Text = rm.GetString("labelNrLlogarie", ci);
            xrLabel55.Text = rm.GetString("labelNrLlogarie", ci);
            xrLabel56.Text = rm.GetString("labelNrLlogarie", ci);
            xrLabel77.Text = rm.GetString("labelNrLlogarie", ci);
            xrLabel79.Text = rm.GetString("labelNrLlogarie", ci);
            xrLabel60.Text = rm.GetString("IbanRaiffeissenLekINEL", ci);
            xrLabel71.Text = rm.GetString("IbanCredinsLekINEL", ci);
            xrLabel72.Text = rm.GetString("IbanBKTLekINEL", ci);
            xrLabel84.Text = rm.GetString("IbanRaiffeissenEuroINEL", ci);
            xrLabel87.Text = rm.GetString("IbanCredinsEuroINEL", ci);
            xrLabel53.Text = rm.GetString("NrLlogRaiffeissenLekINEL", ci);
            xrLabel57.Text = rm.GetString("NrLlogCredinsLekINEL", ci);
            xrLabel58.Text = rm.GetString("NrLlogBKTLekINEL", ci);
            xrLabel78.Text = rm.GetString("NrLlogRaiffeissenEuroINEL", ci);
            xrLabel81.Text = rm.GetString("NrLlogCredinsEuroINEL", ci);
            xrLabel46.Text = rm.GetString("RaiffeissenBankLEKLabel", ci);
            xrLabel47.Text = rm.GetString("CREDINS_BANK_LEK_label", ci);
            xrLabel51.Text = rm.GetString("BKTLEK_label", ci);
            xrLabel54.Text = rm.GetString("RaiffeissenBankEURO_label", ci);
            xrLabel73.Text = rm.GetString("CREDINS_BANK_EURO_label", ci);

        }

    }
}
