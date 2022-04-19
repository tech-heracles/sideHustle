using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_Fature_Tatimore_Shitje_Vod_Aparate : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_Fature_Tatimore_Shitje_Vod_Aparate()
        {
            InitializeComponent();
        }
        public Rap_Fature_Tatimore_Shitje_Vod_Aparate(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_Fature_Tatimore_Shitje_Vod_Aparate(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }

    }
}
