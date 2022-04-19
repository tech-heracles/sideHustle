using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.KlientFurnitor.Fatura
{
    public partial class Rap_FormatPrintimiVeprimeKF : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FormatPrintimiVeprimeKF()
        {
            InitializeComponent();
        }
        public Rap_FormatPrintimiVeprimeKF(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatPrintimiVeprimeKF(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }

    }
}
