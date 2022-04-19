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
    public partial class Rap_FatureShitjeMeArtikujUniversReklamaMePermasa : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureShitjeMeArtikujUniversReklamaMePermasa(){InitializeComponent();} 
  
        public Rap_FatureShitjeMeArtikujUniversReklamaMePermasa(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {
        }
        public Rap_FatureShitjeMeArtikujUniversReklamaMePermasa(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel37.Text = rm.GetString("labelNIPT", ci) + ":";
            xrLabel25.Text = rm.GetString("labelRaportTel", ci) + ":";
            xrLabel24.Text = rm.GetString("labelRaportEmail", ci) + ":";
            xrLabel56.Text = rm.GetString("labelNIPT", ci) + ":";
            xrLabel48.Text = rm.GetString("labelAdreseFaturimi", ci);
            xrLabel32.Text = rm.GetString("labelRaportTel", ci) + ":";
            xrLabel47.Text = rm.GetString("labelAdreseDergimi", ci) + ":";
            xrLabel33.Text = rm.GetString("labelRaportKontakti", ci) + ":";
            xrLabel6.Text = rm.GetString("labelNumerFature", ci);
            xrLabel14.Text = rm.GetString("labelRaportReferenca", ci);
            xrLabel7.Text = rm.GetString("labelRaportDateFature", ci);
            xrLabel41.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrLabel12.Text = rm.GetString("labelAgjenti", ci);
            xrLabel13.Text = rm.GetString("labelKushtePagese", ci);
            xrTableCell4.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell3.Text = rm.GetString("labelRaportiShenime", ci);
            xrTableCell13.Text = rm.GetString("labelNjesia", ci);
            xrTableCell28.Text = rm.GetString("labelCope", ci);
            xrTableCell5.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell9.Text = rm.GetString("labelCmimi", ci);
            xrTableCell8.Text = rm.GetString("labelZbritje", ci) + "%";
            xrTableCell6.Text = rm.GetString("labelVleraPaTVSH", ci);
            xrLabel20.Text = rm.GetString("labelNentotal", ci) + ":";
            xrLabel21.Text = rm.GetString("labelBlerjeShitjeZbritje", ci) + ":";
            xrLabel15.Text = rm.GetString("labelTVSH", ci) + ":";
            xrLabel51.Text = rm.GetString("labelRaportiTotali", ci) + ":";
            xrLabel52.Text = rm.GetString("labelKursi", ci) + ":";         
            xrLabel40.Text = rm.GetString("labelBleresi", ci);
            xrLabel49.Text = rm.GetString("labelEmerMbiemerFirma", ci);
            xrLabel38.Text = rm.GetString("lblShitesKrijues", ci);
            xrLabel50.Text = rm.GetString("labelEmerMbiemerFirma", ci);
            xrTableCell17.Text = rm.GetString("labelKodi", ci);
            xrTableCell18.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell19.Text = rm.GetString("labelGarancia", ci);
            xrLabel2.Text = rm.GetString("labelKushtetEGarancise", ci);
            xrLabel31.Text = rm.GetString("LabelKushtGaranicePerProdProdhimi", ci);
            xrLabel35.Text = rm.GetString("LabelLuhatjeTensioni", ci);
            xrLabel36.Text = rm.GetString("labelRiparimiOseZevendesimiProduktit", ci);
        }

 
    }
}
