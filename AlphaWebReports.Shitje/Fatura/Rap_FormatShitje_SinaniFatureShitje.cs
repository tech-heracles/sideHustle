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
    public partial class Rap_FormatShitje_SinaniFatureShitje : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatShitje_SinaniFatureShitje(){InitializeComponent();} 
        public Rap_FormatShitje_SinaniFatureShitje(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatShitje_SinaniFatureShitje(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel1.Text = rm.GetString("TitullRaportiFatureTatimoreShitje", ci);
            xrLabel8.Text = rm.GetString("lblRaportNumriSerial", ci);
            xrLabel7.Text = rm.GetString("labelRaportDataFatures", ci);
            xrLabel50.Text = rm.GetString("lblEmriITransportuesit", ci);
            xrLabel13.Text = rm.GetString("labelAdresaUpperCase", ci);
            xrLabel12.Text = rm.GetString("lblTargaEMjetit", ci);
            xrLabel14.Text = rm.GetString("lblOraEFurnizimit", ci);
            xrLabel20.Text = rm.GetString("labelAdministrimiNIPT", ci);
            xrLabel4.Text = rm.GetString("lblEmriIBleresit", ci);
            xrLabel2.Text = rm.GetString("labelAdresaUpperCase", ci);
            xrLabel16.Text = rm.GetString("labelAdministrimiNIPT", ci);
            xrLabel18.Text = rm.GetString("lblTelefon", ci);
            xrTableCell14.Text = rm.GetString("lblRaportNrkartel", ci);
            xrTableCell15.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell7.Text = rm.GetString("labelNjesiaUpperCase", ci);
            xrTableCell3.Text = rm.GetString("labelVlera_Pa_Tvsh", ci);
            xrTableCell6.Text = rm.GetString("labelTVSH", ci);
            xrTableCell9.Text = rm.GetString("label_SASIA", ci);
            xrTableCell10.Text = rm.GetString("lblRaportCmimiPerNjesiPaTvsh", ci);
            xrTableCell8.Text = rm.GetString("labelVLERA_ME_TVSH", ci);
            xrLabel22.Text = rm.GetString("labelBleresi", ci);
            xrLabel23.Text = rm.GetString("labelTransportuesi", ci);
            xrLabel26.Text = rm.GetString("lblShitesKrijues", ci);
            xrTableCell4.Text = rm.GetString("labelAsetKodi", ci);
            xrTableCell5.Text = rm.GetString("lblPershkrimiiMallitOseSherbimit", ci);
            xrLabel73.Text = rm.GetString("footerRaportPerdorimiFaturesLejuarNgaDPTatimeveMeShkreseNrDateSinani", ci);
            xrLabel48.Text = rm.GetString("lblRaportEmerMbiemerNensh", ci);
            xrLabel53.Text = rm.GetString("lblRaportEmerMbiemerNensh", ci);
            xrLabel55.Text = rm.GetString("lblRaportEmerMbiemerNenshkrimiVule", ci);
        }

    }
}
