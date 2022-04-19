using System;
using System.Globalization;
using System.Resources;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs.UrdherPagesa
{
    public partial class Rap_Format_UrdherPagese : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_Format_UrdherPagese() { InitializeComponent(); }
      
        public Rap_Format_UrdherPagese(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_Format_UrdherPagese(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel10.Text = rm.GetString("lblRaportMiratuarNga");
            xrLabel24.Text = rm.GetString("lblRaportMiratuarNga2");
        }
    }
}
