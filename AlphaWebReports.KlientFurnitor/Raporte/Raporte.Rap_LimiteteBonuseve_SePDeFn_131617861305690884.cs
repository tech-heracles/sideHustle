using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.KlientFurnitor.Raporte
{
    public partial class Rap_LimiteteBonuseve_SePDeFn_131617861305690884 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_LimiteteBonuseve_SePDeFn_131617861305690884(){InitializeComponent();} 
        public Rap_LimiteteBonuseve_SePDeFn_131617861305690884(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_LimiteteBonuseve_SePDeFn_131617861305690884(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
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

            xrLabel12.Text = rm.GetString("lblRaportiLimiteteBonuseve", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
        }
    }
}
