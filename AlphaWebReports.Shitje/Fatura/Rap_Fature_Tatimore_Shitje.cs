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
    public partial class Rap_Fature_Tatimore_Shitje : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Fature_Tatimore_Shitje(){InitializeComponent();} 

        
        public Rap_Fature_Tatimore_Shitje(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_Fature_Tatimore_Shitje(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel44.Text = rm.GetString("labelRaportNumriFatures", ci);
            xrLabel8.Text = rm.GetString("lblRaportNumriSerial", ci);
            xrLabel7.Text = rm.GetString("labelRaportDataFatures", ci);
            xrLabel32.Text = rm.GetString("lblEmriITransportuesitLowerCase", ci);
            xrLabel34.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel39.Text = rm.GetString("lblRaportTargaeMjetit", ci);
            xrLabel38.Text = rm.GetString("lblRaportOraeFurnizimit", ci);
            xrLabel4.Text = rm.GetString("labelRaportSubjektiBleres", ci);
            xrLabel2.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel64.Text = rm.GetString("labelRaportAdresa", ci) + ":";
            xrTableCell4.Text = rm.GetString("labelRarportKartele", ci).ToUpper();
            xrTableCell32.Text = rm.GetString("lblRaportKodbar", ci).ToUpper();
            xrTableCell5.Text = rm.GetString("labelPERSHKRIMI", ci);
            xrTableCell17.Text = rm.GetString("labelRaportNr", ci).ToUpper();
            xrTableCell7.Text = rm.GetString("labelNjesiaUpperCase", ci);
            xrTableCell9.Text = rm.GetString("label_SASIA", ci);
            xrTableCell10.Text = rm.GetString("label_CMIMI", ci);
            xrTableCell61.Text = rm.GetString("lblskonto", ci).ToUpper();
            xrTableCell3.Text = rm.GetString("labelVlera_Pa_Tvsh", ci);
            xrTableCell6.Text = rm.GetString("labelTVSH", ci);
            xrTableCell8.Text = rm.GetString("labelVLERA_ME_TVSH", ci);
            xrTableCell21.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell24.Text = rm.GetString("lblRaportTotaliNe", ci);
            xrTableCell48.Text = rm.GetString("labelKursi", ci);
            xrTableCell41.Text = rm.GetString("lblskonto", ci);
            xrTableCell57.Text = rm.GetString("labelRaportTotaliMeZbritje", ci) + ":";
            xrTableCell28.Text = rm.GetString("lblNgaTeCilat", ci);
            xrTableCell38.Text = rm.GetString("lblFurnizimeTatueshme", ci);
            xrTableCell31.Text = rm.GetString("lblFurnizimePatueshme", ci);
            xrLabel3.Text = rm.GetString("pikeShitjeTab", ci).ToUpper() + ":";
            xrLabel48.Text = xrLabel53.Text = xrLabel55.Text = rm.GetString("labelRaportEmerMbiemerFirma", ci);

        }

    }
}
