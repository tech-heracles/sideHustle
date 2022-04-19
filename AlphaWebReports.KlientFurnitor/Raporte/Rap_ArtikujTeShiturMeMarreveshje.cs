using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.KlientFurnitor.Raporte
{
    public partial class Rap_ArtikujTeShiturMeMarreveshje : XtraReport
    {
        public Rap_ArtikujTeShiturMeMarreveshje()
        {
            InitializeComponent();
        }

        public Rap_ArtikujTeShiturMeMarreveshje(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }

        public Rap_ArtikujTeShiturMeMarreveshje(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, XtraReport raport)
        {
            InitializeComponent();
        }
    }
}
