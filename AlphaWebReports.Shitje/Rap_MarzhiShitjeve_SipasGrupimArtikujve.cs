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
    public partial class Rap_MarzhiShitjeve_SipasGrupimArtikujve : DevExpress.XtraReports.UI.XtraReport
    {
        bool hapurgjitha = false;
        public Rap_MarzhiShitjeve_SipasGrupimArtikujve(){InitializeComponent();} 
         int shifraPasPresjes = 0;
        public Rap_MarzhiShitjeve_SipasGrupimArtikujve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {
 
        }

        public Rap_MarzhiShitjeve_SipasGrupimArtikujve(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            shifraPasPresjes = Convert.ToInt32(raport.Parameters["filterFormatNumri"].Value);
            hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);

        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
            System.Reflection.Assembly.Load("App_GlobalResources"));
            MarzhiShitjeveLabel.Text = rm.GetString("RaportMarzhiShitjeveSipasGrupimArikujveTitull", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            KartelaLabelKoka.Text = rm.GetString("labelKartela", ci);
            ArtikulliLabelKoka.Text = rm.GetString("labelEmertimiArtikullit", ci);
            NjesiaLabelKoka.Text = rm.GetString("labelNjesia", ci);
            SasiaLabelKoka.Text = rm.GetString("labelSasia", ci);
            KostoNjesiLabelKoka.Text = rm.GetString("labelKostoNjesi", ci);
            KMSHLabelKoka.Text = rm.GetString("labelKMSH", ci);
            CmimiLabelKoka.Text = rm.GetString("labelCmimShitje", ci);
            ShitjaMeZbritjeLabelKoka.Text = rm.GetString("labelVleraShitjesMeZbritje", ci);
            MarzhiBrutoZbritjeLabelKoka.Text = rm.GetString("labelMarzhiBrutoMeZbritje", ci);
            MarzhiBrutoPerqindjeLAbelKoka.Text = rm.GetString("labelMarzhiBrutoPerqindje", ci);
            TotaliLabel.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel32.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel27.Text = rm.GetString("labelRaportMarzhiGrupimiGruplabel", ci);
            
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            e.Cancel = !hapurgjitha;
        }
    }
}
