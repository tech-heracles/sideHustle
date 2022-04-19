using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureShitjeFPD_SePDeFn_131356953662075682 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FatureShitjeFPD_SePDeFn_131356953662075682()
        {
            InitializeComponent();
        }
        public Rap_FatureShitjeFPD_SePDeFn_131356953662075682(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitjeFPD_SePDeFn_131356953662075682(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }

    }
}
