using System;
using System.Drawing;
using System.Globalization;
using DevExpress.XtraReports.UI;
using System.Resources;

namespace AlphaWebReports.RaportetDs.Raporte_Menaxheriale
{
    public partial class Rap_HistorikuVeprimeveBuxhetimi : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_HistorikuVeprimeveBuxhetimi(){InitializeComponent();} 
        public Rap_HistorikuVeprimeveBuxhetimi(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.ScopeID, report)
        {

        }
        public Rap_HistorikuVeprimeveBuxhetimi(CultureInfo ci, int idNdermarrje, int idViti, string scopeID, DevExpress.XtraReports.UI.XtraReport raport)
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
        }
    }
}
