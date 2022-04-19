using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureShitjeAlbaGame : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureShitjeAlbaGame(){InitializeComponent();} 

        public Rap_FatureShitjeAlbaGame(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitjeAlbaGame(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel2.Text = rm.GetString("lblAlbaGame", ci);
            xrLabel3.Text = rm.GetString("lblAlbaGameNipt", ci);
            xrLabel4.Text = rm.GetString("lblAdrAlbaGame", ci);
            xrLabel6.Text = rm.GetString("lblTel", ci);
            xrLabel7.Text = rm.GetString("lblInfoAlbaGame", ci);
            xrLabel11.Text = rm.GetString("lblBnkAlbaGame", ci);
            xrLabel8.Text = rm.GetString("lblBnkAlbaGame", ci);
            xrLabel9.Text = rm.GetString("blbAlbagameLek", ci);
            xrLabel10.Text = rm.GetString("lblAlbaGameLek3", ci);
            xrLabel12.Text = rm.GetString("lblAlbaGameLek2", ci);
            xrLabel13.Text = rm.GetString("lblAlbaGameIBAN", ci);
            xrLabel1.Text = rm.GetString("TitullRaportiFatureShitje", ci);
            xrTableCell25.Text = rm.GetString("labelRaportNumriFatures", ci);
            xrTableCell37.Text = rm.GetString("lblRaportDataFatures", ci);
            xrTableCell39.Text = rm.GetString("lblRaportMagazina", ci);
            xrTableCell41.Text = rm.GetString("labelAgjenti", ci);
            xrTableCell43.Text = rm.GetString("lblMenyraPageses", ci);
            xrTableCell45.Text = rm.GetString("lblMenyraTransportit", ci);
            xrTableCell47.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell51.Text = rm.GetString("labelNipti", ci);
            xrTableCell23.Text = rm.GetString("labelRaportSubjektBleres", ci);
            xrTableCell49.Text = rm.GetString("labelRaportAdresa", ci);
            xrTableCell53.Text = rm.GetString("labelRaportTel", ci);
            xrTableCell55.Text = rm.GetString("labelRaportEmail", ci);
            xrTableCell57.Text = rm.GetString("lblRaportNrLLogari", ci);
            xrTableCell17.Text = rm.GetString("labelKodi", ci);
            xrTableCell15.Text = rm.GetString("labelBarkodi", ci);
            xrTableCell5.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell10.Text = rm.GetString("labelNjesia", ci);
            xrTableCell3.Text = rm.GetString("labelSasia", ci);
            xrTableCell19.Text = rm.GetString("labelCmimi", ci);
            xrTableCell4.Text = rm.GetString("labelRaportCmimiPaTVSH", ci);
            xrTableCell8.Text = rm.GetString("labelVleftapaTVSH", ci);
            xrTableCell11.Text = rm.GetString("labelTVSH", ci);
            xrTableCell6.Text = rm.GetString("labelVleftameTVSH", ci);
            xrTableCell31.Text = rm.GetString("labelVleraMeTVSH", ci);
            xrTableCell9.Text = rm.GetString("labelZbritje", ci);
            xrTableCell59.Text = rm.GetString("lblVleraTotale", ci);
            xrLabel42.Text = rm.GetString("lblBleresi", ci);
            xrLabel43.Text = rm.GetString("lblShitesi", ci);
            xrLabel69.Text = rm.GetString("labelTransportuesi", ci);
            xrTableCell34.Text = rm.GetString("lblNrSerial", ci);
        }
        
    }
}
