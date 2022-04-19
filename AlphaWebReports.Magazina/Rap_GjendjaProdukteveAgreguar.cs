using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_GjendjaProdukteveAgreguar : DevExpress.XtraReports.UI.XtraReport
    {
        

        public Rap_GjendjaProdukteveAgreguar()
        {
            InitializeComponent();
        }
        public Rap_GjendjaProdukteveAgreguar(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report)
        {

        }

        public Rap_GjendjaProdukteveAgreguar(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            IdNdermarje.Value = raport.Parameters["IdNdermarje"].Value;
            filterKartela.Value = raport.Parameters["filterKartela"].Value;
            filterMagazina.Value = raport.Parameters["filterMagazina"].Value;
            filterDtDok.Value = raport.Parameters["filterDtDok"].Value;
            filterKompania.Value = raport.Parameters["filterKompania"].Value;
            parameterIdNderm.Value = idNdermarrje;

        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
        }
    }
}
