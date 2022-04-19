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
    public partial class Rap_Magazina_MZHUQendraKostoPermbledhes : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Magazina_MZHUQendraKostoPermbledhes(){InitializeComponent();} 
        public Rap_Magazina_MZHUQendraKostoPermbledhes(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report)
        {

        }
        public Rap_Magazina_MZHUQendraKostoPermbledhes(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            parameter2.Value = raport.Parameters[1].Value;
            parameterIdNderm.Value = idNdermarrje;
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            lblTitullRpt.Text = rm.GetString("RaportMZHUPerllogaritjaKostosPermbledhes_Title", ci);
            lblGrupSipasNJQV.Text = rm.GetString("RaportMZHUPerllogaritjaKostos_lblEmerNjqv", ci);
        }
       
    }
}
