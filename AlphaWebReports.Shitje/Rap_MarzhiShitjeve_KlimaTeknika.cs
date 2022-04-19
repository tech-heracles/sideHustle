using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using DevExpress.XtraPrinting.BarCode;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_MarzhiShitjeve_KlimaTeknika : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_MarzhiShitjeve_KlimaTeknika(){InitializeComponent();}
    
        int formatNumri = 0;
        public Rap_MarzhiShitjeve_KlimaTeknika(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_MarzhiShitjeve_KlimaTeknika(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            Ndermarrja.Value = raport.Parameters[0].Value;
            DtDok.Value = raport.Parameters[1].Value;
            Klient.Value = raport.Parameters[2].Value;
            Artikull.Value = raport.Parameters[3].Value;
            Qyteti.Value = raport.Parameters[4].Value;
            Grupim1.Value = raport.Parameters[5].Value;
            Grupim2.Value = raport.Parameters[6].Value;
            PikeShitjeFurnizim.Value = raport.Parameters[7].Value;
            LlojArtikulli.Value = raport.Parameters[8].Value;
            KlasaArtikulli.Value = raport.Parameters[9].Value;
            parameter1.Value = raport.Parameters[10].Value;
            parameter2.Value = raport.Parameters[11].Value;
            parameter3.Value = raport.Parameters[12].Value;
            parameter4.Value = raport.Parameters[14].Value;
            parameter5.Value = raport.Parameters[15].Value;
            parameter6.Value = raport.Parameters[16].Value;
            DegaAdministrative.Value = raport.Parameters[13].Value;
            parameter7.Value=raport.Parameters[17].Value;
            formatNumri = Convert.ToInt32(raport.Parameters["filterFormatNumri"].Value);
            parameterFormatNumri.Value = formatNumri;
            caktoFormatinENumrave();
        }
         public void caktoFormatinENumrave()
        {
            xrTableCell27.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel31.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel1.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel33.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel2.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel3.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel5.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel37.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel36.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel6.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel7.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            KMSHTotali.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            VleraMeZbritjeTotali.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel38.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel39.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            MarzhiMeZbritjeTotali.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            KMSHTotali.Summary.FormatString = "{0:n" + formatNumri + "}";
            VleraMeZbritjeTotali.Summary.FormatString = "{0:n" + formatNumri + "}";
            xrLabel38.Summary.FormatString = "{0:n" + formatNumri + "}";
            xrLabel39.Summary.FormatString = "{0:n" + formatNumri + "}";
            MarzhiMeZbritjeTotali.Summary.FormatString = "{0:n" + formatNumri + "}";
                 xrTableCell27.XlsxFormatString = xrLabel31.XlsxFormatString = xrLabel1.XlsxFormatString = xrLabel4.XlsxFormatString = xrLabel33.XlsxFormatString = xrLabel3.XlsxFormatString = xrLabel5.XlsxFormatString
                = xrLabel37.XlsxFormatString = xrLabel36.XlsxFormatString = xrLabel6.XlsxFormatString = xrLabel7.XlsxFormatString = KMSHTotali.XlsxFormatString 
                = VleraMeZbritjeTotali.XlsxFormatString = xrLabel38.XlsxFormatString = xrLabel39.XlsxFormatString = MarzhiMeZbritjeTotali.XlsxFormatString = MarzhiPerqindjeTotali.XlsxFormatString
               = 0.ToString("N" + formatNumri);
        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {

            ResourceManager rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));


            MarzhiShitjeveLabelR.Text = rm.GetString("RaportMarzhiShitjeveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            Kartela.Text = rm.GetString("labelKartela", ci);
            Barkodi.Text = rm.GetString("labelBarkodi", ci);
            EmertimiArtikullit.Text = rm.GetString("labelEmertimiArtikullit", ci);
            Njesia.Text = rm.GetString("labelNjesia", ci);
            SasiaShitje.Text = rm.GetString("lblRaportSasiaShitje", ci);
            KostoNjesi.Text = rm.GetString("labelKostoNjesi", ci);
            KMSHT.Text = rm.GetString("labelKMSH", ci);
            CmimiMesatarShitjes.Text = rm.GetString("lblRaportCmimMesatarShitje", ci);
            VleraShitjesMeZbritjeT.Text = rm.GetString("labelVleraShitjesMeZbritje", ci);
            MarzhiBrutoMeZbritjeT.Text = rm.GetString("labelMarzhiBrutoMeZbritje", ci);
            MarzhiBruto.Text = rm.GetString("labelMarzhiBrutoPerqindje", ci);
            TotaliLabel.Text = rm.GetString("labelRaportiTotali", ci);
            LogoIMB.Text = rm.GetString("labelLogoIMB", ci);
            GjendjaMbartur.Text = rm.GetString("lblRaportGjendjeMbartur",ci);
            SasiaHyrje.Text = rm.GetString("lblRaportSasiahyrje", ci);
            SasiaGjendje.Text = rm.GetString("lblRaportSasiagjendje", ci);
            VleraNekostoHyrje.Text = rm.GetString("lblRaportVleraKostoHyrje", ci);
            VleraNeKostoGjendje.Text = rm.GetString("lblRaportVleraKostoGjendje", ci);
            
        }

    }
}
