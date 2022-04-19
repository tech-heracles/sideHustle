using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.KlientFurnitor.Raporte
{
    public partial class Rap_BuxhetiHarxhuar : XtraReport
    {
        public Rap_BuxhetiHarxhuar()
        {
            InitializeComponent();
        }

        public Rap_BuxhetiHarxhuar(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }

        public Rap_BuxhetiHarxhuar(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, XtraReport raport)
        {
            InitializeComponent();
        }

    }
}
