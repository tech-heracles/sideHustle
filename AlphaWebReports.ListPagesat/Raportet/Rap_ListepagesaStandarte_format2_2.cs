using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.ListPagesat.Raportet
{
    public partial class Rap_ListepagesaStandarte_format2_2 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ListepagesaStandarte_format2_2(){InitializeComponent();} 
      
		public Rap_ListepagesaStandarte_format2_2(AlphaWebReports.Common.ParametraRaporti param, DevExpress.XtraReports.UI.XtraReport raport):
		this(param.Ci, param.IdNdermarrje,param.IdViti,param.IdPerdoruesi,raport){}
        public Rap_ListepagesaStandarte_format2_2(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();


        }
    }
}
