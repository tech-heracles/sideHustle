using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class RapGjendjaEArtikujveMeSeriale : DevExpress.XtraReports.UI.XtraReport
    {
        public RapGjendjaEArtikujveMeSeriale()
        {
            InitializeComponent();
        }
        public RapGjendjaEArtikujveMeSeriale(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public RapGjendjaEArtikujveMeSeriale(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }
        public void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel13.Text = rm.GetString("labelRaportiTitulliGjendjaArtikujveMeSerial", ci);
            xrLabel17.Text = rm.GetString("labelRaportiArtikull", ci);
            xrLabel18.Text = rm.GetString("labelRaportPershkrimi", ci);
            xrLabel20.Text = rm.GetString("labelRaportNrDokumenti", ci);
            xrLabel22.Text = rm.GetString("lblDateDokumenti", ci);
            xrLabel24.Text = rm.GetString("lblRaportMagazina", ci);
            xrTableCell7.Text = rm.GetString("labelRaportSeriali", ci);
            xrTableCell8.Text = rm.GetString("labelRaportSerialiDy", ci);
            xrTableCell9.Text = rm.GetString("lblRaportGjendja", ci);


        }
    }
}

