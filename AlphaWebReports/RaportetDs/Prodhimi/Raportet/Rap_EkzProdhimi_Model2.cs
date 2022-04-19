using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;


namespace AlphaWebReports.RaportetDs.Prodhimi.Raportet
{
    public partial class Rap_EkzProdhimi_Model2 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_EkzProdhimi_Model2(){InitializeComponent();} 
     
        public Rap_EkzProdhimi_Model2(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_EkzProdhimi_Model2(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {

            InitializeComponent();
            EmrateLabelave(ci);
            
        }

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));

        
        }

    }
}
