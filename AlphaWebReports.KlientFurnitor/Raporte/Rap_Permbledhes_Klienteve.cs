using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Resources;
using System.Globalization;


namespace AlphaWebReports.RaportetDs.KlientFurnitor
{
    public partial class Rap_Permbledhes_Klienteve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Permbledhes_Klienteve(){InitializeComponent();} 
        public Rap_Permbledhes_Klienteve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_Permbledhes_Klienteve(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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
            Titulli.Text = rm.GetString("labelRegjistriPermbledhesiKlienteve", ci);
        }
    }
}
 
