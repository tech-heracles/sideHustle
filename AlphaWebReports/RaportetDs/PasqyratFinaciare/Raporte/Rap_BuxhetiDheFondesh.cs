using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.PasqyratFinaciare.Raporte
{
    public partial class Rap_BuxhetiDheFondesh : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_BuxhetiDheFondesh() { InitializeComponent(); }
        public Rap_BuxhetiDheFondesh(ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        { }
        public Rap_BuxhetiDheFondesh(CultureInfo ci, int idNdermarrje, int idViti, XtraReport raport)
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
            xrTableCell1.Text = rm.GetString("lblTitulliRaportBuxhetidheFondesh", ci);
            xrTableCell8.Text = rm.GetString("FiltratEmertimi", ci);
        }

    }
}
