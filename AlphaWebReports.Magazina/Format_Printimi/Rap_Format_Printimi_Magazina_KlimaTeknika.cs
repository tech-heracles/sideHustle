using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.Magazina.Format_Printimi
{
    public partial class Rap_Format_Printimi_Magazina_KlimaTeknika : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Format_Printimi_Magazina_KlimaTeknika(){InitializeComponent();} 

        public Rap_Format_Printimi_Magazina_KlimaTeknika(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_Format_Printimi_Magazina_KlimaTeknika(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }


    }
}
