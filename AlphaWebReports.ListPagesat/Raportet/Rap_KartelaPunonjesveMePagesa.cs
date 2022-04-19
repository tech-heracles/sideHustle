using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.ListPagesat.Raportet
{
    public partial class Rap_KartelaPunonjesveMePagesa : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_KartelaPunonjesveMePagesa(){InitializeComponent();} 

        
        public Rap_KartelaPunonjesveMePagesa(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.ScopeID, report)
        {

        }
        public Rap_KartelaPunonjesveMePagesa(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, string scopeID, DevExpress.XtraReports.UI.XtraReport raport)
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
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel12.Text = rm.GetString("RaportKartelaPunonjësveMePagesaTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel45.Text = rm.GetString("labelPershkrimVeprimi", ci);
            xrLabel47.Text = rm.GetString("labelGjendjaMonedheBaze", ci);
            xrLabel36.Text = rm.GetString("labelLlojDokumenti", ci);
            xrLabel37.Text = rm.GetString("labelRaportiDtDok", ci);
            xrLabel38.Text = rm.GetString("labelRaportiNrDok", ci);
            xrLabel40.Text = rm.GetString("labelRaportiDebi", ci);
            xrLabel42.Text = rm.GetString("labelRaportiKredi", ci);
            xrLabel43.Text = rm.GetString("labelRaportiProgresivi", ci);
            xrLabel28.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel5.Text = rm.GetString("labelGjendjeFillim", ci);
        }
    }
}
