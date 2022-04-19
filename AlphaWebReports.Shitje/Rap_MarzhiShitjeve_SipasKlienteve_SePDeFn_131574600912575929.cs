using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Resources;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_MarzhiShitjeve_SipasKlienteve_SePDeFn_131574600912575929 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_MarzhiShitjeve_SipasKlienteve_SePDeFn_131574600912575929()
        {
            InitializeComponent();
        }
        public Rap_MarzhiShitjeve_SipasKlienteve_SePDeFn_131574600912575929(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }

        public Rap_MarzhiShitjeve_SipasKlienteve_SePDeFn_131574600912575929(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
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
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            KartelaLabelKoka.Text = rm.GetString("labelKartela", ci);
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
        }
    }
}
