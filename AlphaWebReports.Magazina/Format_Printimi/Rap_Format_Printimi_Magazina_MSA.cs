using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.Magazina.Format_Printimi
{
    public partial class Rap_Format_Printimi_Magazina_MSA : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_Format_Printimi_Magazina_MSA()
        {
            InitializeComponent();
        }

        public Rap_Format_Printimi_Magazina_MSA(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {
        }
        public Rap_Format_Printimi_Magazina_MSA(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }
    }
}
