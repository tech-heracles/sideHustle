using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_ShitjeSipasGrupitArtikujveStatistikor : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_ShitjeSipasGrupitArtikujveStatistikor()
        {
            InitializeComponent();
        }

        public Rap_ShitjeSipasGrupitArtikujveStatistikor(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_ShitjeSipasGrupitArtikujveStatistikor(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);

        }

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
            System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel70.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel120.Text = rm.GetString("lblTitulliRapArt", ci);
            NrRendKoka.Text = rm.GetString("lblNrrend", ci);
            KodiKoka.Text = rm.GetString("lblKodi", ci);
            Grupi.Text = rm.GetString("labelFilterAvancuarGrupi", ci);
            Rreshta.Text = rm.GetString("lblRaportRreshta", ci);
            Sasia.Text = rm.GetString("labelRaportSasia", ci);
            Sasianjësinëdytë.Text = rm.GetString("lblRaportSasianjesin2", ci);
            Vlefta.Text = rm.GetString("labelRapVlefta", ci);
            VleftaNeto.Text = rm.GetString("lblRaportVleftaNeto", ci);
            NumërKlientesh.Text = rm.GetString("labelNrKlientesh", ci);
            Zbritje.Text = rm.GetString("lblRaportZbritjeTotale", ci);
            ZbritjeMesatare.Text = rm.GetString("lblRaportZbritjeMes", ci);
            PeshaGrupit.Text = rm.GetString("labelRaportPeshaGr", ci);
            xrLabel42.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel40.Text = "-";



        }
    }
}
