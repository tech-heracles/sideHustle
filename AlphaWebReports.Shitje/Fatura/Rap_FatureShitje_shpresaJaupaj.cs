using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureShitje_shpresaJaupaj : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FatureShitje_shpresaJaupaj() { InitializeComponent();}
        public Rap_FatureShitje_shpresaJaupaj(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitje_shpresaJaupaj(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }

    }
}
