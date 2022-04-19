using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.Magazina.Format_Printimi
{
    public partial class Rap_Format_Printimi_Magazina_Cobalt : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_Format_Printimi_Magazina_Cobalt() { InitializeComponent(); }
        public Rap_Format_Printimi_Magazina_Cobalt(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_Format_Printimi_Magazina_Cobalt(CultureInfo ci, int IdNdermarrje, int IdPerdoruesi)
        {
            InitializeComponent();
        }

    }
}
