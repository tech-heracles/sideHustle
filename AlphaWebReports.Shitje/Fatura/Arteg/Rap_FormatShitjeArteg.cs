using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura.Arteg
{
    public partial class Rap_FormatShitjeArteg : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatShitjeArteg(){InitializeComponent();} 
 
        public Rap_FormatShitjeArteg(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatShitjeArteg(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            parameterIdNderm.Value = idNdermarrje;
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel32.Text = rm.GetString("nrBankeArtegBankaCredins");
            xrLabel34.Text = rm.GetString("nrBankeArtegBankaIntesa");
            xrLabel37.Text = rm.GetString("nrBankeArtegBankaBKT");
            xrLabel35.Text = rm.GetString("nrBankeArtegBankaRaiffeisen");
            xrLabel70.Text = rm.GetString("nrBankeArtegBankaProCredit");
            xrLabel69.Text = rm.GetString("nrBankeArtegBankaAlphaBank");
            
        }

  
    }
}
