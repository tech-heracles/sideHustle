using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureShitjeAlbas : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureShitjeAlbas(){InitializeComponent();} 


        public Rap_FatureShitjeAlbas(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitjeAlbas(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel1.Text = rm.GetString("TitullRaportiFatureTatimoreShitje", ci);
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
            xrLabel37.Text = rm.GetString("labelRaportMagazina", ci);
            xrLabel69.Text = rm.GetString("labelTransportuesi", ci);
            xrTableCell4.Text = rm.GetString("label_CMIMI", ci);
            xrTableCell3.Text = rm.GetString("label_SASIA", ci);
            xrTableCell15.Text = rm.GetString("lblKodbari", ci);
            xrTableCell10.Text = rm.GetString("labelNjesiaUpperCase", ci);
            xrTableCell17.Text = rm.GetString("labelRaportNRKARTELE", ci);
            //xrLabel73.Text = rm.GetString("footerRaportPerdorimiFaturesLejuarNgaDPTatimeve", ci);
          //  xrLabel45.Text = rm.GetString("MenuItemRaportBanka", ci);
            xrLabel51.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            //xrLabel54.Text = rm.GetString("lblRaportNrllogarise", ci);
            //xrLabel55.Text = rm.GetString("lblRaportNrLlogariRaiffeisenBank1", ci);
            //xrLabel56.Text = rm.GetString("lblRaportNrLlogariRaiffeisenBank2", ci);
            //xrLabel62.Text = rm.GetString("lblRaportNrLlogariRaiffeisenBank3", ci);
            //xrLabel61.Text = rm.GetString("lblRaportNrLlogariBKT", ci);
            xrLabel46.Text = rm.GetString("lblRaportRaiffesienBank", ci);
            xrLabel47.Text = rm.GetString("lblRaportRaiffesienBank", ci);
            //xrLabel57.Text = rm.GetString("lblRaportRaiffesienBank", ci);
            //xrLabel58.Text = rm.GetString("lblRaportBKT", ci);
            xrLabel80.Text = rm.GetString("raiffeisenLekeAlbas", ci);
            xrLabel82.Text = rm.GetString("raiffeisenEuroAlbas", ci);
            xrLabel81.Text = rm.GetString("raiffeisenSwiftAlbas", ci);
            xrLabel46.Text = rm.GetString("bktLekeAlbas", ci);
            xrLabel51.Text = rm.GetString("bktEuroAlbas", ci);
            xrLabel47.Text = rm.GetString("bktNrLlogAlbas", ci);
            xrLabel52.Text = rm.GetString("bktSwiftAlbas", ci);
            xrLabel73.Text = rm.GetString("filterRaportPershkrimi", ci);
        }
  
    }
}
