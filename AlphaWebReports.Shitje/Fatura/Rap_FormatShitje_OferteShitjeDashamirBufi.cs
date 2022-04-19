using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatShitje_OferteShitjeDashamirBufi : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatShitje_OferteShitjeDashamirBufi(){InitializeComponent();} 

        public Rap_FormatShitje_OferteShitjeDashamirBufi(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {
        }
        public Rap_FormatShitje_OferteShitjeDashamirBufi(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);

        }

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel1.Text = rm.GetString("labelNumberOferte", ci);
            xrLabel6.Text = rm.GetString("labelRaportNumriFatures", ci);
            xrLabel7.Text = rm.GetString("lblRaportDataFatures", ci);
            xrLabel56.Text = rm.GetString("IBAN", ci);
            xrLabel57.Text = rm.GetString("Date", ci);
            xrLabel58.Text = rm.GetString("Vlefshmeria", ci);
            xrLabel73.Text = rm.GetString("CredinsBank", ci);
            xrLabel2.Text = rm.GetString("lblEmriIShitesit", ci);
            xrLabel12.Text = rm.GetString("lblEmriBleresit", ci);
            xrLabel13.Text = rm.GetString("lblAdresa", ci);
            xrLabel3.Text = rm.GetString("lblAdresa", ci);
            xrLabel48.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel6.Text = rm.GetString("lblEmail", ci);
            xrLabel14.Text = rm.GetString("lblNipt", ci);
            xrLabel15.Text = rm.GetString("lblteli", ci);
            xrLabel19.Text = rm.GetString("lblRaportTarga", ci);
            xrLabel4.Text = rm.GetString("lblNipt", ci);
            xrLabel41.Text = rm.GetString("lblRaportTotaliNe", ci);
            xrLabel30.Text = rm.GetString("lblTotaliMeZbritje", ci);
            xrLabel40.Text = rm.GetString("labelKursi", ci);
            xrLabel42.Text = rm.GetString("labelBleresi", ci);
            xrLabel43.Text = rm.GetString("labelShitesi", ci);
            xrLabel37.Text = rm.GetString("labelRaportMagazina", ci) + ":";
        }
    }
}
