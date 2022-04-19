using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_ShitjeAnalizaArtikujveProdhim : DevExpress.XtraReports.UI.XtraReport
    {

        public Rap_ShitjeAnalizaArtikujveProdhim() { InitializeComponent(); }
        public Rap_ShitjeAnalizaArtikujveProdhim(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.IdRaporti, param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_ShitjeAnalizaArtikujveProdhim(int idRaporti, CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
            System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel13.Text = rm.GetString("RaportAnalizaArtikujveNeProdhim", ci);
            xrLabel1.Text = rm.GetString("lblFaturaShitjeAnalizaArtikujveProdhim", ci);
            xrLabel9.Text = rm.GetString("lblLendaPareAnalizaArtikujveProdhim", ci);
        }

    }
}
