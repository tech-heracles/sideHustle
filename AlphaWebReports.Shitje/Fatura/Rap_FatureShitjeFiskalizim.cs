using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Web.Configuration;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureShitjeFiskalizim : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureShitjeFiskalizim(){InitializeComponent();} 
            
        public Rap_FatureShitjeFiskalizim(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitjeFiskalizim(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel1.Text = rm.GetString("RaportFatureShitjeTitulli", ci);
            xrTableCell4.Text = rm.GetString("labelKodi", ci);
            xrTableCell5.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell7.Text = rm.GetString("labelNjesia", ci);
            xrTableCell9.Text = rm.GetString("labelSasia", ci);
            xrTableCell6.Text = rm.GetString("labelVleftameTVSH", ci);
            if (WebConfigurationManager.AppSettings["urlFiskalizimi"].Contains("test"))
                kontrollLinku.DisplayName = "true";
            else
                kontrollLinku.DisplayName = "false";
        }
    }
}
