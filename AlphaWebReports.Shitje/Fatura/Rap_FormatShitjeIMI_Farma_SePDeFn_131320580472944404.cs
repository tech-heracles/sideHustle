using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatShitjeIMI_Farma_SePDeFn_131320580472944404 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FormatShitjeIMI_Farma_SePDeFn_131320580472944404()
        {
            InitializeComponent();
        }
        public Rap_FormatShitjeIMI_Farma_SePDeFn_131320580472944404(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatShitjeIMI_Farma_SePDeFn_131320580472944404(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
           // parameterIdNderm.Value = idNdermarrje;
        }
    }
}
