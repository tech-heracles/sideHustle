using System;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Drawing;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_UrdhershitjeSipasKlienteve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_UrdhershitjeSipasKlienteve(){InitializeComponent();} 
       
        public Rap_UrdhershitjeSipasKlienteve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_UrdhershitjeSipasKlienteve(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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
            var rm = new ResourceManager("Resources.Strings",
            System.Reflection.Assembly.Load("App_GlobalResources"));
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);

        }
    }
    
}
