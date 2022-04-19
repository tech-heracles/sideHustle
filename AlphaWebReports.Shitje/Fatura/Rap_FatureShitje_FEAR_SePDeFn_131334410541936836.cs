using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureShitje_FEAR_SePDeFn_131334410541936836 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FatureShitje_FEAR_SePDeFn_131334410541936836()
        {
            InitializeComponent();
        }
        public Rap_FatureShitje_FEAR_SePDeFn_131334410541936836(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {
        }
        public Rap_FatureShitje_FEAR_SePDeFn_131334410541936836(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }
    }
}
