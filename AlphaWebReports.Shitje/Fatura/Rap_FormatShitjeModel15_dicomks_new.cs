using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatShitjeModel15_dicomks_new : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FormatShitjeModel15_dicomks_new() {InitializeComponent();}
        public Rap_FormatShitjeModel15_dicomks_new(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatShitjeModel15_dicomks_new(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }
}
}
