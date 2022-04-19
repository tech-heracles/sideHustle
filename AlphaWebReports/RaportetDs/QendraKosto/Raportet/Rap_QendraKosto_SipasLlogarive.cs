using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.QendraKosto.Raportet
{
    public partial class Rap_QendraKosto_SipasLlogarive : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_QendraKosto_SipasLlogarive(){InitializeComponent();} 
        public Rap_QendraKosto_SipasLlogarive(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, report)
        {

        }
        public Rap_QendraKosto_SipasLlogarive(CultureInfo ci, DevExpress.XtraReports.UI.XtraReport raport)
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
           
            xrLabel13.Text = rm.GetString("RaportiQendraveTeKostosSipasLlogariveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);

            xrTableCell8.Text = rm.GetString("labelNrLlogarie", ci);
            xrTableCell9.Text = rm.GetString("labelKodQender", ci);
            xrTableCell10.Text = rm.GetString("labelRaportiEmertimi", ci);

            xrTableCell20.Text = rm.GetString("labelVleraMonedheLlogari", ci);
            xrTableCell11.Text = rm.GetString("labelRaportiDebi", ci);
            xrTableCell12.Text = rm.GetString("labelRaportiKredi", ci);
            xrTableCell13.Text = rm.GetString("labelFilterAvancuarGjendja", ci);

            xrTableCell23.Text = rm.GetString("labelVlereMonedheBaze", ci);
            xrTableCell14.Text = rm.GetString("labelRaportiDebi", ci);
            xrTableCell15.Text = rm.GetString("labelRaportiKredi", ci);
            xrTableCell16.Text = rm.GetString("labelFilterAvancuarGjendja", ci);

            xrLabel40.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel44.Text = rm.GetString("labelLogoIMB", ci);
        }
    }
}
