using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.ListPagesat.Raportet
{
    public partial class Rap_ListepagesaStandarte_SePDeFn_131860691217595526 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_ListepagesaStandarte_SePDeFn_131860691217595526()
        {
            InitializeComponent();
        }
        public Rap_ListepagesaStandarte_SePDeFn_131860691217595526(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
           this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_ListepagesaStandarte_SePDeFn_131860691217595526(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
        }

    }
}
