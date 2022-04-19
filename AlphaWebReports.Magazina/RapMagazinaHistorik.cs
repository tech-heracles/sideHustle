using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class RapMagazinaHistorik : DevExpress.XtraReports.UI.XtraReport
    {
		public RapMagazinaHistorik(){InitializeComponent();} 
        public RapMagazinaHistorik(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report)
        {

        }
        public RapMagazinaHistorik(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            //EmrateLabelave(ci);
            ResourceManager rm = new ResourceManager("Resources.Strings",
                         System.Reflection.Assembly.Load("App_GlobalResources"));
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameterIdNderm.Value = idNdermarrje;
        }

    }
}
