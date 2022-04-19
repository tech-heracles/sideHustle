using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Resources;
using System.Globalization;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.KlientFurnitor.Raporte
{
    public partial class Rap_ShitjeGjendjeKlienteshSipasAgjenteve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ShitjeGjendjeKlienteshSipasAgjenteve(){InitializeComponent();} 
        public Rap_ShitjeGjendjeKlienteshSipasAgjenteve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, report)
        {

        }
        public Rap_ShitjeGjendjeKlienteshSipasAgjenteve(CultureInfo ci, DevExpress.XtraReports.UI.XtraReport raport)
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
            xrTableCell13.Text = rm.GetString("labelRaportiEmertimi", ci);
            xrTableCell1.Text = rm.GetString("labelRaportiDebia", ci);
            xrTableCell17.Text = rm.GetString("labelRaportLimiti", ci);
            xrTableCell23.Text = rm.GetString("labelRaportTotal", ci);
            Titulli.Text = rm.GetString("lblRaportShitjeGjendjeKlienteshSipasAgjenteve", ci);
            xrLabel20.Text = rm.GetString("labelLogoIMB", ci);
            lblFiltra.Text = rm.GetString("FiltratEmertimi", ci);
        }

           
    }
}
