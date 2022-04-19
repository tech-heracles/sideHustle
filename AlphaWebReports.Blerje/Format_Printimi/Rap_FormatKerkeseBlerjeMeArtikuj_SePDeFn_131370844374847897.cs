using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.Blerje.Format_Printimi
{
    public partial class Rap_FormatKerkeseBlerjeMeArtikuj_SePDeFn_131370844374847897 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FormatKerkeseBlerjeMeArtikuj_SePDeFn_131370844374847897()
        {
            InitializeComponent();
        }
        public Rap_FormatKerkeseBlerjeMeArtikuj_SePDeFn_131370844374847897(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatKerkeseBlerjeMeArtikuj_SePDeFn_131370844374847897(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel1.Text = rm.GetString("RaportFatureBlerjeTitulli", ci);
        }
    }
}
