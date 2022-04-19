using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureShitjeMeArtikujModel8 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureShitjeMeArtikujModel8(){InitializeComponent();}
        public Rap_FatureShitjeMeArtikujModel8(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitjeMeArtikujModel8(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            switch (ci.ToString())
            {
                case "sq-AL": //shqip
                    parameter1.Value = 0;
                    break;
                case "en_US": //anglisht
                    parameter1.Value = 1;
                    break;
                default: break;

            }
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel7.Text = rm.GetString("labelRaportTotaliZbritjeMB", ci);
            xrLabel11.Text = rm.GetString("lblRaportTotaliNe", ci);
            xrLabel2.Text = rm.GetString("labelRaportDateFature", ci);
            xrTableCell17.Text = rm.GetString("labelKartela", ci).ToUpper();
            xrLabel6.Text = rm.GetString("labelRaportiNrUpperCase", ci);
            xrLabel13.Text = rm.GetString("labelRaportAdresa", ci) + ":";
            xrLabel54.Text = rm.GetString("labelRaportTel", ci) + ":";
            xrLabel14.Text = rm.GetString("labelRaportTel", ci) + ":";
            xrLabel53.Text = rm.GetString("labelEmriBleresit", ci) + ":";
            xrLabel55.Text = rm.GetString("labelRaportAdresa", ci) + ":";
            xrLabel15.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel35.Text = rm.GetString("labelEmriShitesit", ci) + ":";
            xrTableCell4.Text = rm.GetString("labelRaportiNr", ci).ToUpper();
            xrTableCell5.Text = rm.GetString("labelRaportiPershkrimi", ci).ToUpper();
            xrTableCell7.Text = rm.GetString("labelNjesia", ci).ToUpper();
            xrTableCell9.Text = rm.GetString("labelSasia", ci).ToUpper();
            xrTableCell10.Text = rm.GetString("labelCmimi", ci).ToUpper();
            xrTableCell8.Text = rm.GetString("labelVleftapaTVSH", ci).ToUpper();
            xrTableCell11.Text = rm.GetString("labelTVSH", ci).ToUpper();
            xrTableCell6.Text = rm.GetString("labelVleftameTVSH", ci).ToUpper();
            xrLabel24.Text = rm.GetString("labelKursi", ci);
            xrLabel44.Text = rm.GetString("lblRaportBleresEmerFirme", ci);
            xrLabel46.Text = rm.GetString("labelRaportMenyrePagese", ci) + ":";
            xrLabel45.Text = rm.GetString("lblShitesKrijues", ci);
        }

    }
}
