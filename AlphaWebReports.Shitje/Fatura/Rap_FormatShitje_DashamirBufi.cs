using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatShitje_DashamirBufi : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatShitje_DashamirBufi(){InitializeComponent();}       
        public Rap_FormatShitje_DashamirBufi(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {
        }
        public Rap_FormatShitje_DashamirBufi(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel1.Text = rm.GetString("lblRaportFatureTitull", ci);
            xrLabel8.Text = rm.GetString("lblNrSerial", ci);
            xrLabel7.Text = rm.GetString("labelRaportDataFatures", ci);
            xrTableCell47.Text = rm.GetString("lblEmriITransportuesitLowerCase", ci);
            xrTableCell74.Text = rm.GetString("lblRaportTargaeMjetit", ci);
            xrTableCell80.Text = rm.GetString("lblRaportOraeFurnizimit", ci);
            xrTableCell86.Text = rm.GetString("labelAdministrimiKodiFiskal", ci);
            xrTableCell58.Text = rm.GetString("labelEmriBleresit", ci) + ":";
            xrTableCell68.Text = rm.GetString("labelAdministrimiNIPT", ci);
            xrTableCell70.Text = rm.GetString("labelRrethi", ci) + ":";
            xrTableCell14.Text = rm.GetString("lblRaportNrkartel", ci);
            xrTableCell15.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell7.Text = rm.GetString("labelNjesiaMatjes", ci);
            xrTableCell3.Text = rm.GetString("labelVleftapaTVSH", ci); 
            xrTableCell38.Text = rm.GetString("labelEtatueshme", ci);
            xrTableCell29.Text = rm.GetString("labelEpatatueshme", ci);
            xrTableCell6.Text = rm.GetString("lblVlTvsh", ci);
            xrTableCell9.Text = rm.GetString("lblRaportSasia", ci);
            xrTableCell10.Text = rm.GetString("lblRaportCmimiPerNjesiPaTvsh", ci);
            xrTableCell8.Text = rm.GetString("labelVleftameTVSH", ci);
            xrLabel22.Text = rm.GetString("labelBleresi", ci);
            xrLabel23.Text = rm.GetString("labelTransportuesi", ci); 
            xrLabel44.Text = rm.GetString("labelNrPreventivit", ci);
            xrTableCell24.Text = rm.GetString("labelEmriShitesit", ci) + ":";
            xrTableCell60.Text = rm.GetString("labelRaportAdresa", ci) + ":";
            xrTableCell62.Text = rm.GetString("labelRaportAdresa", ci) + ":";
            xrTableCell64.Text = rm.GetString("labelRaportAdresa", ci) + ":";
            xrTableCell72.Text = rm.GetString("labelAdministrimiNIPT", ci);
            xrTableCell76.Text = rm.GetString("labelAdministrimiNIPT", ci);
            xrTableCell66.Text = rm.GetString("lblTelefoni", ci) + ":";
            xrLabel26.Text = rm.GetString("lblShitesKrijues", ci);
            xrTableCell78.Text = rm.GetString("labelAdministrimiKodiFiskal", ci) + ":";
            xrTableCell4.Text = rm.GetString("labelAsetKodi", ci);
            xrTableCell5.Text = rm.GetString("labelPershkrimimallit", ci);
            xrLabel34.Text = rm.GetString("lblRaportEmerMbiemerFirma", ci);
            xrLabel33.Text = rm.GetString("lblRaportEmerMbiemerFirma", ci);
            xrLabel32.Text = rm.GetString("lblRaportEmerMbiemerFirma", ci); 
            xrLabel28.Text = rm.GetString("labelGjithsej", ci); 
            xrTableCell33.Text = rm.GetString("lblRaportTotaliNe", ci);
        }
    }
}
