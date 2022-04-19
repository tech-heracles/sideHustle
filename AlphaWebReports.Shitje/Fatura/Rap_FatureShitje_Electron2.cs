using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureShitje_Electron2 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureShitje_Electron2(){InitializeComponent();} 

        public Rap_FatureShitje_Electron2(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitje_Electron2(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel1.Text = rm.GetString("lblRaportFatureTitull2", ci);
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
            xrLabel17.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel18.Text = rm.GetString("labelNIPT", ci);
            xrLabel19.Text = rm.GetString("lblRaportTarga", ci);
            xrLabel24.Text = rm.GetString("lblRaportOraFurnizimit", ci);
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
            xrTableCell4.Text = rm.GetString("labelRaportCmimiPaTVSH", ci).ToUpper();
            xrTableCell3.Text = rm.GetString("label_SASIA", ci);
            xrTableCell10.Text = rm.GetString("labelNjesiaUpperCase", ci);
            xrTableCell17.Text = rm.GetString("labelRaportNRKARTELE", ci);
            xrLabel73.Text = rm.GetString("footerRaportGaranci", ci);
            xrLabel4.Text = "Nr. " + rm.GetString("labelRaportLlogBanke", ci);
        }

    }
}
