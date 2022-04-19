using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureEksporti_Divel : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FatureEksporti_Divel() { InitializeComponent(); }
        public Rap_FatureEksporti_Divel(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureEksporti_Divel(System.Globalization.CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }

    }
}
