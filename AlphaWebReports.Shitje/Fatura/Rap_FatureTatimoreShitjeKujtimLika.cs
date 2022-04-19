using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureTatimoreShitjeKujtimLika : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureTatimoreShitjeKujtimLika(){InitializeComponent();} 
        public Rap_FatureTatimoreShitjeKujtimLika(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureTatimoreShitjeKujtimLika(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel1.Text = rm.GetString("labelTitullFATURETATIMORESHITJE", ci);
            xrLabel2.Text = rm.GetString("labelFaturepergatiturngableresi", ci);
            xrLabel4.Text = rm.GetString("labelNriSerise", ci) + ":";
            xrLabel6.Text = rm.GetString("labelRaportData", ci) + ":";
            xrLabel7.Text = rm.GetString("lblnrfatures", ci) + ":";
            xrLabel9.Text = rm.GetString("labelEmriiShitesit", ci) + ":";
            xrLabel11.Text = rm.GetString("labelAdresaUpperCase", ci) + ":";
            xrLabel13.Text = rm.GetString("labelTELEFONuppercase", ci) + ":";
            xrLabel15.Text = rm.GetString("labelNumriIdentifikimitNIPT", ci);
            xrLabel17.Text = rm.GetString("lblEmriIBleresit", ci);
            xrLabel19.Text = rm.GetString("labelAdresaUpperCase", ci) + ":";
            xrLabel21.Text = rm.GetString("labelTELEFONuppercase", ci) + ":";
            xrLabel23.Text = rm.GetString("labelNumriIdentifikimitNIPT", ci);
            xrLabel25.Text = rm.GetString("lblEmriITransportuesit", ci);
            xrLabel27.Text = rm.GetString("labelAdresaUpperCase", ci) + ":";
            xrLabel29.Text = rm.GetString("lblTargaEMjetit", ci);
            xrLabel34.Text = rm.GetString("lblOraEFurnizimit", ci);
            xrLabel31.Text = rm.GetString("labelNumriIdentifikimitNIPT", ci);
            xrTableCell1.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell2.Text = rm.GetString("lblPershkrimiiMallitOseSherbimit", ci);
            xrTableCell3.Text = rm.GetString("lblNjesiaMatjes", ci);
            xrTableCell4.Text = rm.GetString("lblRaportSasia", ci);
            xrTableCell5.Text = rm.GetString("lblRaportCmimiPerNjesiPaTvsh", ci);
            xrTableCell6.Text = rm.GetString("labelVleftapaTVSH", ci);
            xrTableCell7.Text = rm.GetString("lblVlTvsh", ci);
            xrTableCell8.Text = rm.GetString("labelVleftameTVSH", ci);
            xrLabel35.Text = rm.GetString("labelraportiAFAQE1", ci);
            xrLabel36.Text = rm.GetString("labelraportiBFAQE2", ci);
            xrLabel37.Text = rm.GetString("labelraportiBFAQE3", ci);
            xrLabel38.Text = rm.GetString("labelRaportiOriginaliBleresi", ci);
            xrLabel39.Text = rm.GetString("labelTransportuesi", ci);
            xrLabel40.Text = rm.GetString("lblShitesi", ci);
            xrLabel43.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel42.Text = rm.GetString("lblFurnizimeTatueshme", ci);
            xrLabel41.Text = rm.GetString("lblFurnizimePatueshme", ci);
            xrLabel48.Text = rm.GetString("lblNgaTeCilat", ci);
            xrLabel56.Text = rm.GetString("ShenimFatureTatimoreKujtimLika", ci);
        }
    }
}
