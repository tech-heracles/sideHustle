using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureShitjeAlbaGame_SePDeFn_131659477344187076 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureShitjeAlbaGame_SePDeFn_131659477344187076()
        {
            InitializeComponent();
        } 

        public Rap_FatureShitjeAlbaGame_SePDeFn_131659477344187076(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitjeAlbaGame_SePDeFn_131659477344187076(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel10.Text = rm.GetString("lblAlbaGameLek3", ci);
            xrLabel12.Text = rm.GetString("lblAlbaGameLek2", ci);
            xrLabel13.Text = rm.GetString("lblAlbaGameIBAN", ci);
            xrLabel1.Text = rm.GetString("TitullRaportiFatureShitje", ci);
            xrTableCell25.Text = rm.GetString("labelRaportNumriFatures", ci);
            xrTableCell39.Text = rm.GetString("lblRaportMagazina", ci);
            xrTableCell43.Text = rm.GetString("lblMenyraPageses", ci);
            xrTableCell45.Text = rm.GetString("lblMenyraTransportit", ci);
            xrTableCell47.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell23.Text = rm.GetString("labelRaportSubjektBleres", ci);
            xrTableCell53.Text = rm.GetString("labelRaportTel", ci);
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
            xrLabel42.Text = rm.GetString("lblBleresi", ci);
            xrLabel69.Text = rm.GetString("labelTransportuesi", ci);
            xrTableCell34.Text = rm.GetString("lblNrSerial", ci);
        }
        
    }
}
