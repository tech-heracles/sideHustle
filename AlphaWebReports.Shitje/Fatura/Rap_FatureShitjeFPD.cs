using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureShitjeFPD : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureShitjeFPD(){InitializeComponent();} 

        public Rap_FatureShitjeFPD(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitjeFPD(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
           
          
          
            //xrLabel67.Text = rm.GetString("Galina_Adresa", ci);
            xrLabel6.Text = rm.GetString("labelNriSerise", ci);

            xrLabel72.Text = rm.GetString("Galina_TiranaBank", ci);

            xrLabel10.Text = rm.GetString("lblskonto", ci);
            xrLabel12.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrLabel15.Text = rm.GetString("lblMenyraPageses", ci) + ":";
            xrLabel17.Text = rm.GetString("labelRaportiShenime", ci);
            xrLabel18.Text = rm.GetString("lblData", ci);
            xrLabel19.Text = rm.GetString("lblnrfatures", ci);
            xrLabel20.Text = rm.GetString("ReportToolbarLabelPage", ci);
            xrLabel60.Text = rm.GetString("lblBleresi", ci);
            xrLabel83.Text = rm.GetString("labelTransportuesi", ci);
            xrLabel53.Text = "1) " + rm.GetString("lblBleresi", ci);
            xrLabel52.Text = "2) " + rm.GetString("labelTransportuesi", ci);
            xrLabel51.Text = "3) " + rm.GetString("lblShitesi", ci);
            xrLabel86.Text = rm.GetString("labelRaportTarga", ci);
            xrLabel57.Text = xrLabel33.Text = rm.GetString("label_Emri", ci);
            xrLabel27.Text = xrLabel76.Text = rm.GetString("lblRaportAdresa", ci);
            xrLabel29.Text = xrLabel80.Text = rm.GetString("filterRaportiNrTel", ci) +":";
            xrLabel31.Text = xrLabel78.Text = rm.GetString("labelAdministrimiNIPT", ci);
            xrTableCell18.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell2.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell3.Text = rm.GetString("lblNjesia", ci);
            xrTableCell4.Text = rm.GetString("lblRaportSasia", ci);
            xrTableCell5.Text = rm.GetString("labelRaportCmimiPaTVSH", ci);
            xrTableCell6.Text = xrTableCell29.Text = rm.GetString("labelVleraPaTVSH", ci);
            xrTableCell7.Text = xrTableCell30.Text = rm.GetString("labelVleraTVSH", ci);
            xrTableCell8.Text = xrTableCell31.Text = rm.GetString("labelVleraMeTVSH", ci);
            xrTableCell36.Text = rm.GetString("labelRaportiTotali", ci)+" nÎ Euro";
            //xrTableCell1.Text = rm.GetString("Galina_SocieteGeneral", ci);
            xrTableCell35.Text = rm.GetString("lblTotLek", ci);
            xrTableCell45.Text = rm.GetString("lblFurnizimeTÎTatueshme", ci);
            xrTableCell50.Text = rm.GetString("lblFurnizimeTÎPaTatueshme", ci);
            xrLabel55.Text = xrLabel54.Text = rm.GetString("lblRaportEmerMbiemerNensh", ci);
            xrLabel50.Text = rm.GetString("lblRaportEmerMbiemerNenshkrimiVule", ci);
            xrLabel49.Text = rm.GetString("labelRaportLlogariBankare", ci);
            xrLabel69.Text = rm.GetString("lblSocieteGeneraleAlbaniaUPPER", ci);
            xrLabel71.Text = rm.GetString("lblIBANfpd", ci);
            xrLabel72.Text = rm.GetString("lblSWIFTCODEFPD", ci);
            //xrLabel70.Text = rm.GetString("Galina_SocieteGeneral", ci);

        }

        
    }
}
