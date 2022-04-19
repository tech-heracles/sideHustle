using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_MarzhiShitjeve : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_MarzhiShitjeve()
        {
            InitializeComponent();
        }
        int formatNumri = 0;

        public Rap_MarzhiShitjeve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_MarzhiShitjeve(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
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
            parameter7.Value = raport.Parameters[17].Value;
            parameter8.Value = raport.Parameters["filterKodbari"].Value;
            parameter9.Value = raport.Parameters["filterAktivitetiKlient"].Value;

               formatNumri = Convert.ToInt32(raport.Parameters["filterFormatNumri"].Value);
            parameterFormatNumri.Value = formatNumri;
            caktoFormatinENumrave();
        }

      private void  caktoFormatinENumrave()
        {
            xrLabel1.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel2.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel3.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel5.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel6.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel7.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel27.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            KMSHTotali.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            VleraMeZbritjeTotali.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            MarzhiMeZbritjeTotali.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel27.Summary.FormatString = "{0:n" + formatNumri + "}";
            KMSHTotali.Summary.FormatString = "{0:n" + formatNumri + "}";
            VleraMeZbritjeTotali.Summary.FormatString = "{0:n" + formatNumri + "}";
            MarzhiMeZbritjeTotali.Summary.FormatString = "{0:n" + formatNumri + "}";
                xrLabel1.XlsxFormatString = xrLabel2.XlsxFormatString = xrLabel3.XlsxFormatString = xrLabel4.XlsxFormatString = xrLabel5.XlsxFormatString = xrLabel6.XlsxFormatString = xrLabel7.XlsxFormatString
                = xrLabel27.XlsxFormatString = KMSHTotali.XlsxFormatString = VleraMeZbritjeTotali.XlsxFormatString = MarzhiMeZbritjeTotali.XlsxFormatString = MarzhiPerqindjeTotali.XlsxFormatString
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
            EmertimiArtikullit.Text = rm.GetString("labelEmertimiArtikullit", ci);
            Njesia.Text = rm.GetString("labelNjesia", ci);
            Sasia.Text = rm.GetString("labelSasia", ci);
            KostoNjesi.Text = rm.GetString("labelKostoNjesi", ci);
            KMSHT.Text = rm.GetString("labelKMSH", ci);
            Cmimi.Text = rm.GetString("labelCmimShitje", ci);
            ShitjaMeZbritje.Text = rm.GetString("labelVleraShitjesMeZbritje", ci);
            MarzhiBrutoZbritje.Text = rm.GetString("labelMarzhiBrutoMeZbritje", ci);
            MarzhiBrutoPerqindjeT.Text = rm.GetString("labelMarzhiBrutoPerqindje", ci);
            TotaliLabel.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel32.Text = rm.GetString("labelLogoIMB", ci);
           
        }
    }
}
