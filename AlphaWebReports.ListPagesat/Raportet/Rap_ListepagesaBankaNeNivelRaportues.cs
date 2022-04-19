using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.ListPagesat.Raportet
{
    public partial class Rap_ListepagesaBankaNeNivelRaportues : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_ListepagesaBankaNeNivelRaportues()
        {
            InitializeComponent();
        }
        public Rap_ListepagesaBankaNeNivelRaportues(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_ListepagesaBankaNeNivelRaportues(CultureInfo ci, int idNdermarrje, int idViti, XtraReport report)
        {
            InitializeComponent();
        }

    }
}
