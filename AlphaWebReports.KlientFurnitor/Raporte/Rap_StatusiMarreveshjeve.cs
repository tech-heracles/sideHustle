using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.KlientFurnitor.Raporte
{
    public partial class Rap_StatusiMarreveshjeve : XtraReport
    {
        public Rap_StatusiMarreveshjeve()
        {
            InitializeComponent();
        }

        public Rap_StatusiMarreveshjeve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }

        public Rap_StatusiMarreveshjeve(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, XtraReport raport)
        {
            InitializeComponent();
        }
    }
}
