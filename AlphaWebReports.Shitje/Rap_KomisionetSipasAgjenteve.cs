using System;
using System.Globalization;
using System.Resources;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_KomisionetSipasAgjenteve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_KomisionetSipasAgjenteve(){InitializeComponent();} 
        public Rap_KomisionetSipasAgjenteve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_KomisionetSipasAgjenteve(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
           // xrLabel52.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            //xrLabel56.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
           // xrLabel60.Text = raport.Parameters[2].Description;
            parameter3.Value = raport.Parameters[2].Value;
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            var rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel17.Text = rm.GetString("RaportiKomisionetSipasAgjenteveTeShitjesTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel24.Text = rm.GetString("labelAgjentiBashkepuntori", ci);
            xrLabel3.Text = rm.GetString("labelShitjePaTvsh", ci);
            xrLabel23.Text = rm.GetString("labelKomisione", ci);
            xrLabel1.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel50.Text = rm.GetString("labelLogoIMB", ci);
        }
    }
}
