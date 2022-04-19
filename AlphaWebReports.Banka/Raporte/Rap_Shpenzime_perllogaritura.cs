using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.Banka.Raporte
{
    public partial class Rap_Shpenzime_perllogaritura : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Shpenzime_perllogaritura(){InitializeComponent();} 
        ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

        public Rap_Shpenzime_perllogaritura(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_Shpenzime_perllogaritura(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[4].Value;
            parameter5.Value = raport.Parameters[5].Value;
            parameter6.Value = raport.Parameters[6].Value;

        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
            System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel2.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel43.Text = rm.GetString("labelLogoIMB", ci);            
        }
    }
}
