using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatShitjeA5_SePDeFn_131359480100963703 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FormatShitjeA5_SePDeFn_131359480100963703()
        {
            InitializeComponent();
        }

     
        public Rap_FormatShitjeA5_SePDeFn_131359480100963703(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatShitjeA5_SePDeFn_131359480100963703(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();

        }

    }
}
