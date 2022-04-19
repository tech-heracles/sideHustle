using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_ShitjeFleteDoganoreExport_SePDeFn_131540115936852559 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_ShitjeFleteDoganoreExport_SePDeFn_131540115936852559()
        {
            InitializeComponent();
        }

        public Rap_ShitjeFleteDoganoreExport_SePDeFn_131540115936852559(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_ShitjeFleteDoganoreExport_SePDeFn_131540115936852559(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();

        }

    }
}
