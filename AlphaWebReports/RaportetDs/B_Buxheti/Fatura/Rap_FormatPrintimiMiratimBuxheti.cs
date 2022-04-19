using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.B_Buxheti.Fatura
{
    public partial class Rap_FormatPrintimiMiratimBuxheti : XtraReport
    {
        public Rap_FormatPrintimiMiratimBuxheti()
        {
            InitializeComponent();
        }
        public Rap_FormatPrintimiMiratimBuxheti(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {
        }
        public Rap_FormatPrintimiMiratimBuxheti(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }

    }
}
