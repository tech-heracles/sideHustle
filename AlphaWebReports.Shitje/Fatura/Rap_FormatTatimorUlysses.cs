using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatTatimorUlysses : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FormatTatimorUlysses()
        {
            InitializeComponent();
        }

        public Rap_FormatTatimorUlysses(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatTatimorUlysses(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }
    }
}
