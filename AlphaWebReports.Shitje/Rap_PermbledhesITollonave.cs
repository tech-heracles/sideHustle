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
    public partial class Rap_PermbledhesITollonave : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_PermbledhesITollonave(){InitializeComponent();}
        
   
        public Rap_PermbledhesITollonave(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, param.ScopeID, report)
        {

        }
        public Rap_PermbledhesITollonave(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, string scopeID, DevExpress.XtraReports.UI.XtraReport raport)
        {

            InitializeComponent();
            EmrateLabelave(ci);
            DtDokLabel.Text = raport.Parameters[1].Description;
            DtDok.Value = raport.Parameters[1].Value;
            KlientLabel.Text = raport.Parameters[2].Description;
            Klient.Value = raport.Parameters[2].Value;
            ArtikullLabel.Text = raport.Parameters[3].Description;
            Artikull.Value = raport.Parameters[3].Value;
            QytetiLabel.Text = raport.Parameters[4].Description;
            Qyteti.Value = raport.Parameters[4].Value;
            Grupim2Label.Text = raport.Parameters[0].Description;
            Grupim2.Value = raport.Parameters[0].Value;
        }
        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            MarzhiShitjeveLabel.Text = rm.GetString("RaportPermbledhesTollonaTitulli", ci);
            //FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            //KartelaLabelKoka.Text = rm.GetString("labelKartela", ci);
            //ArtikulliLabelKoka.Text = rm.GetString("labelEmertimiArtikullit", ci);
            //NjesiaLabelKoka.Text = rm.GetString("labelNjesia", ci);
            //SasiaLabelKoka.Text = rm.GetString("labelSasia", ci);
            //KostoNjesiLabelKoka.Text = rm.GetString("labelKostoNjesi", ci);
            //KMSHLabelKoka.Text = rm.GetString("labelKMSH", ci);
            //CmimiLabelKoka.Text = rm.GetString("labelCmimShitje", ci);
            //ShitjaMeZbritjeLabelKoka.Text = rm.GetString("labelVleraShitjesMeZbritje", ci);
            //MarzhiBrutoZbritjeLabelKoka.Text = rm.GetString("labelMarzhiBrutoMeZbritje", ci);
            //MarzhiBrutoPerqindjeLAbelKoka.Text = rm.GetString("labelMarzhiBrutoPerqindje", ci);
            //TotaliLabel.Text = rm.GetString("labelRaportiTotali", ci);
            //xrLabel32.Text = rm.GetString("labelLogoIMB", ci);
            //xrLabel27.Text = rm.GetString("labelRaportKlienti", ci);
        }

        private void xrLabel29_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
          {
            object klienti = GetCurrentColumnValue("KodKlienti");
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);

            if (klienti != null || klienti != DBNull.Value)
            {
                    xrLabel29.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('RaportiShpejte.aspx?scopeID=" + parametraRaporti.ScopeID + "&guidString=" + parametraRaporti.GuidString + "&idraporti=" + parametraRaporti.IdSubRaporti + "&filterKlientiProdhim=" + klienti + "&printo=0&Sesioni=true')";
                    xrLabel29.Target = "_self";
                    xrLabel29.ForeColor = Color.SteelBlue;
             
            }
        }
        double vleraFaturuar = 0;
        double sasiaFaturuar = 0;

        private void xrLabel34_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = String.Format("{0:#,#.00}", sasiaFaturuar == 0 ? 0 : (vleraFaturuar / sasiaFaturuar));
            e.Handled = true;
        }

        private void xrLabel34_SummaryReset(object sender, EventArgs e)
        {
            vleraFaturuar = 0;
            sasiaFaturuar = 0;
        }

        private void xrLabel34_SummaryRowChanged(object sender, EventArgs e)
        {
            object objVleraFaturuar = GetCurrentColumnValue("VleraFaturuar");
            object objSasiaFaturuar = GetCurrentColumnValue("SasiaFaturuar");
            if (objVleraFaturuar != null && objSasiaFaturuar != null && objVleraFaturuar != DBNull.Value && objSasiaFaturuar != DBNull.Value)
            {
                vleraFaturuar += Convert.ToDouble(objVleraFaturuar);
                sasiaFaturuar += Convert.ToDouble(objSasiaFaturuar);
            }
        }
        double vleraKonsumuar = 0;
        double sasiaKonsumuar = 0;
        private void xrLabel35_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = String.Format("{0:#,#.00}", sasiaKonsumuar == 0 ? 0 : (vleraKonsumuar / sasiaKonsumuar));
            e.Handled = true;
        }

        private void xrLabel35_SummaryReset(object sender, EventArgs e)
        {
            sasiaKonsumuar = 0;
            vleraKonsumuar = 0;
        }

        private void xrLabel35_SummaryRowChanged(object sender, EventArgs e)
        {
            object objVleraKonsumuar = GetCurrentColumnValue("VleraKonsumuar");
            object objSasiaKonsumuar = GetCurrentColumnValue("SasiaKonsumuar");
            if (objVleraKonsumuar != null && objSasiaKonsumuar != null && objVleraKonsumuar != DBNull.Value && objSasiaKonsumuar != DBNull.Value)
            {
                vleraKonsumuar += Convert.ToDouble(objVleraKonsumuar);
                sasiaKonsumuar += Convert.ToDouble(objSasiaKonsumuar);
            }
        }

        private void Rap_PermbledhesITollonave_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);

        }
    }
}
