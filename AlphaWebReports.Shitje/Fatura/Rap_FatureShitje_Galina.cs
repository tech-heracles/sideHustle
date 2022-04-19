using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureShitje_Galina : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureShitje_Galina(){InitializeComponent();} 

        public Rap_FatureShitje_Galina(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitje_Galina (CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
           
          
          
            xrLabel67.Text = rm.GetString("Galina_Adresa", ci);
            xrLabel68.Text = rm.GetString("Galina_BKT", ci);
            xrLabel70.Text = rm.GetString("Galina_SocieteGeneral", ci);
            xrLabel72.Text = rm.GetString("Galina_TiranaBank", ci);
        
        }
    }
}
