using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.KlientFurnitor.Raporte
{
    public partial class Rap_Bonuset_Vjetore_Te_Klienteve_Delta : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_Bonuset_Vjetore_Te_Klienteve_Delta()
        {
            InitializeComponent();
        }
       

        public Rap_Bonuset_Vjetore_Te_Klienteve_Delta(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.IdGjuha, report)
        {

        }
        public Rap_Bonuset_Vjetore_Te_Klienteve_Delta(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, int idGjuha, DevExpress.XtraReports.UI.XtraReport raport)
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
            xrTableCell9.Text = rm.GetString("labelKodi", ci);
            xrTableCell10.Text = rm.GetString("labelRaportiEmertimi", ci);
        }

    }
}
