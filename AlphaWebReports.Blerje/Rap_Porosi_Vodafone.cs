using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.Blerje
{
    public partial class Rap_Porosi_Vodafone : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Porosi_Vodafone(){InitializeComponent();} 
        public Rap_Porosi_Vodafone(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report)
        {

        }
        public Rap_Porosi_Vodafone(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
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


            xrLabel1.Text = rm.GetString("labelNrPorosie", ci);
            xrLabel2.Text = rm.GetString("labelDatePorosie", ci);
            xrTableCell4.Text = rm.GetString("labelKod", ci);
            xrTableCell5.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell7.Text = rm.GetString("labelDyqani", ci);
            xrTableCell9.Text = rm.GetString("labelSasiPorositur", ci);
            xrTableCell10.Text = rm.GetString("labelSasiAprovuar", ci);
            xrTableCell8.Text = rm.GetString("labelRaportiDiferenca", ci); ;
            xrTableCell11.Text = rm.GetString("labelCmimi", ci);
            xrTableCell6.Text = rm.GetString("labelVlefta", ci);
            xrLabel5.Text = rm.GetString("labelVleftaTotale", ci) + ":";
  
        }

    }
}
