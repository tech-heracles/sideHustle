using System;
using System.Globalization;
using System.Resources;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs.KlientFurnitor.Raporte
{
    public partial class Rap_Vjetersia_Detyrimeve_Klienteve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Vjetersia_Detyrimeve_Klienteve(){InitializeComponent();} 
        public Rap_Vjetersia_Detyrimeve_Klienteve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_Vjetersia_Detyrimeve_Klienteve(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
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
        }
     
    }
}
