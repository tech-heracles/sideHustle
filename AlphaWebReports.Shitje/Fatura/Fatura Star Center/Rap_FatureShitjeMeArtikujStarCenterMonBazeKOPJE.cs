using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura.Fatura_Star_Center
{
    public partial class Rap_FatureShitjeMeArtikujStarCenterMonBazeKOPJE : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureShitjeMeArtikujStarCenterMonBazeKOPJE(){InitializeComponent();} 
        public Rap_FatureShitjeMeArtikujStarCenterMonBazeKOPJE(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitjeMeArtikujStarCenterMonBazeKOPJE(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel1.Text = rm.GetString("RaportFatureShitjeTitulli", ci);

            xrLabel2.Text = rm.GetString("labelRaportDateFature", ci);
            xrLabel6.Text = rm.GetString("labelNrFature", ci);
            xrLabel7.Text = rm.GetString("labelRaportNrSerial", ci);
            xrLabel35.Text = rm.GetString("labelEmriShitesit", ci) + ":";
            xrLabel13.Text = rm.GetString("labelRaportAdresa", ci) + ":";
            xrLabel48.Text = rm.GetString("labelRaportNrSerial", ci);
            xrLabel14.Text = rm.GetString("labelRaportTel", ci) + ":";
            xrLabel48.Text = rm.GetString("labelRaportEmail", ci) + ":";
            xrLabel53.Text = rm.GetString("labelEmriBleresit", ci) + ":";
            xrLabel55.Text = rm.GetString("labelRaportAdresa", ci);

            xrLabel12.Text = rm.GetString("labelNIPT", ci);
            xrLabel54.Text = rm.GetString("labelRaportTel", ci) + ":";
            xrLabel57.Text = rm.GetString("labelRaportEmail", ci) + ":";

            xrTableCell4.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell17.Text = rm.GetString("labelKodi", ci);
            xrTableCell5.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell7.Text = rm.GetString("labelNjesia", ci);
            xrTableCell9.Text = rm.GetString("labelSasia", ci);
            xrTableCell10.Text = rm.GetString("labelRaportCmimiPaTVSH", ci);
            xrTableCell8.Text = rm.GetString("labelVleftapaTVSH", ci);
            xrTableCell11.Text = rm.GetString("labelVleraTVSH", ci);
            xrTableCell6.Text = rm.GetString("labelVleraMeTVSH", ci);

            xrLabel11.Text = rm.GetString("labelGjithsej", ci);
            xrLabel15.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel43.Text = rm.GetString("lblRaportTotaliNe", ci);
            xrLabel37.Text = rm.GetString("labelKursi", ci);

            xrLabel24.Text = rm.GetString("labelZbritje", ci) + ":";
            xrLabel25.Text = rm.GetString("labelZbritjeVlere", ci) + ":";
            xrLabel16.Text = rm.GetString("labelRaportiTotali", ci) + ":";
            xrLabel26.Text = rm.GetString("labelLikujdimi", ci) + ":";
            xrLabel20.Text = rm.GetString("labelMbetja", ci) + ":";

            xrLabel44.Text = rm.GetString("labelBleresi", ci);
            xrLabel4.Text = rm.GetString("labelEmerMbiemerFirma", ci);
            xrLabel46.Text = rm.GetString("labelRaportTransportues", ci);
            xrLabel5.Text = rm.GetString("labelEmerMbiemerFirma", ci);
            xrLabel45.Text = rm.GetString("labelShitesi", ci);
            xrLabel8.Text = rm.GetString("labelEmerMbiemerFirma", ci);
        }
    }
}
    
