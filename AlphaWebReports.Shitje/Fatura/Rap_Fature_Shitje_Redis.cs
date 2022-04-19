using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_Fature_Shitje_Redis : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_Fature_Shitje_Redis()
        {
            InitializeComponent();
        }
        public Rap_Fature_Shitje_Redis(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_Fature_Shitje_Redis(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }
    }
}
