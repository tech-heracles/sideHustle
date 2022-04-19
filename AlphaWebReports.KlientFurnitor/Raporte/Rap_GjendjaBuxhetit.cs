using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.KlientFurnitor.Raporte
{
    public partial class Rap_GjendjaBuxhetit : XtraReport
    {
        public Rap_GjendjaBuxhetit()
        {
            InitializeComponent();
        }

        public Rap_GjendjaBuxhetit(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }

        public Rap_GjendjaBuxhetit(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, XtraReport raport)
        {
            InitializeComponent();
        }
    }
}
