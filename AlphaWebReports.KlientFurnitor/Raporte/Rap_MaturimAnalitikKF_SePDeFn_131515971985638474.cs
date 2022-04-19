using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.KlientFurnitor.Raporte
{
    public partial class Rap_MaturimAnalitikKF_SePDeFn_131515971985638474 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_MaturimAnalitikKF_SePDeFn_131515971985638474()
        {
            InitializeComponent();
        }
        public Rap_MaturimAnalitikKF_SePDeFn_131515971985638474(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.IdGjuha, param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_MaturimAnalitikKF_SePDeFn_131515971985638474(int idGjuha, CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
        }
    }
}
