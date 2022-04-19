
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.Arka.Raporte
{
    public partial class Rap_LevizjetELikujditeteve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_LevizjetELikujditeteve(){InitializeComponent();} 
        private CultureInfo ci;

        public Rap_LevizjetELikujditeteve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_LevizjetELikujditeteve(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            this.ci = ci;
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
            xrLabel12.Text = rm.GetString("LevizjaLikujditeteve", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel7.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell1.Text = rm.GetString("labelKodi", ci);
            xrTableCell12.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell2.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrTableCell4.Text = rm.GetString("LikujditeteNeFillim", ci);
            xrTableCell17.Text = rm.GetString("ArketimeNgaShitja", ci);
            xrTableCell22.Text = rm.GetString("ArketimeTeTjera", ci);
            xrTableCell24.Text = rm.GetString("PagesaPeriudhes", ci);
            xrTableCell6.Text = rm.GetString("LikujditeteFund", ci);

        }  
    }
}
