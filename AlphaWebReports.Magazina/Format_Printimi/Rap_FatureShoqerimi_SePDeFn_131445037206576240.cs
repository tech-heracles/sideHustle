using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.Magazina.Format_Printimi
{
    public partial class Rap_FatureShoqerimi_SePDeFn_131445037206576240 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FatureShoqerimi_SePDeFn_131445037206576240()
        {
            InitializeComponent();
        }
        public Rap_FatureShoqerimi_SePDeFn_131445037206576240(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShoqerimi_SePDeFn_131445037206576240(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }

    }
}
