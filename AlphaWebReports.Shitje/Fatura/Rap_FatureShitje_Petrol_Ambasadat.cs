using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureShitje_Petrol_Ambasadat : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureShitje_Petrol_Ambasadat(){InitializeComponent();}
  

        public Rap_FatureShitje_Petrol_Ambasadat(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitje_Petrol_Ambasadat(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            parameterIdNderm.Value = idNdermarrje;
            parameterIdPerdoruesi.Value = idPerdoruesi;
        }

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel1.Text = rm.GetString("lblAdresaReport", ci);
            xrLabel4.Text = rm.GetString("lblTedhenaAlPetrol", ci);
            xrLabel24.Text = rm.GetString("nrLlogarieAlPetrol", ci);
            xrLabel28.Text = rm.GetString("lblNrLlogarieAlPetrol1", ci);
            xrLabel5.Text = rm.GetString("labelRaportiAlpetrolshpkUpperCase", ci);
            xrLabel22.Text = rm.GetString("labelRaportiAlpetrolshpkUpperCase", ci);

        }
        
    }
}
