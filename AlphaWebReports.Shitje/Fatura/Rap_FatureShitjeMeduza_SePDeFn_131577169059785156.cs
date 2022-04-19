using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Data;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureShitjeMeduza_SePDeFn_131577169059785156 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureShitjeMeduza_SePDeFn_131577169059785156(){InitializeComponent();}

        public Rap_FatureShitjeMeduza_SePDeFn_131577169059785156(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitjeMeduza_SePDeFn_131577169059785156(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
        }
        
    }
}
