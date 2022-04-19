using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.KlientFurnitor.Raporte
{
    public partial class Rap_KartelaKlient_SePDeFn_131520114831729620 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_KartelaKlient_SePDeFn_131520114831729620()
        {
            InitializeComponent();
        }
        public Rap_KartelaKlient_SePDeFn_131520114831729620(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_KartelaKlient_SePDeFn_131520114831729620(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
        }

    }
}
