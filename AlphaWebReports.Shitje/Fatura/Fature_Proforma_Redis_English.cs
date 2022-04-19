using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Fature_Proforma_Redis_English : DevExpress.XtraReports.UI.XtraReport
    {
        public Fature_Proforma_Redis_English()
        {
            InitializeComponent();
        }
        public Fature_Proforma_Redis_English(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Fature_Proforma_Redis_English(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }

    }
}
