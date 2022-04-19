using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.Blerje
{
    public partial class Rap_FatureBlerjeMeArtikuj_SePDeFn_131312145548222287 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FatureBlerjeMeArtikuj_SePDeFn_131312145548222287() { InitializeComponent(); }



        public Rap_FatureBlerjeMeArtikuj_SePDeFn_131312145548222287(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }

        public Rap_FatureBlerjeMeArtikuj_SePDeFn_131312145548222287(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel1.Text = rm.GetString("labelPorosikapitale", ci);
            xrLabel6.Text = rm.GetString("labelRaportNumer", ci);
            xrLabel7.Text = rm.GetString("labelDate", ci);
            xrTableCell4.Text = rm.GetString("labelKodi", ci);
            xrTableCell5.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell7.Text = rm.GetString("labelRaportOrigjina", ci);
            xrTableCell9.Text = rm.GetString("labelKodDoganor", ci);
            xrTableCell10.Text = rm.GetString("labelSasia", ci);
            xrTableCell6.Text = rm.GetString("labelRaportVlefta", ci);
            xrLabel24.Text = rm.GetString("labelRaportTotaluUppercase", ci);

        }
    }
}
