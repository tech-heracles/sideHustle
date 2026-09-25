using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatShitjeIMI_Farma : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatShitjeIMI_Farma(){InitializeComponent();} 
        private ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
    
        public Rap_FormatShitjeIMI_Farma(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatShitjeIMI_Farma(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel31.Text = rm.GetString("labelRaportiFormatIMI", ci);
        }


    }
}
