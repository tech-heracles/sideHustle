using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Fature_Proforma_Redis_Macedonia : DevExpress.XtraReports.UI.XtraReport
    {
        public Fature_Proforma_Redis_Macedonia()
        {
            InitializeComponent();
        }
        public Fature_Proforma_Redis_Macedonia(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Fature_Proforma_Redis_Macedonia(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }

    }
}
