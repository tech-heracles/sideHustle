using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.KlientFurnitor.Raporte
{
    public partial class Rap_SituacionIKlienteve_Ekspozim_SePDeFn_131372611580675568 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_SituacionIKlienteve_Ekspozim_SePDeFn_131372611580675568()
        {
            InitializeComponent();
        }

        public Rap_SituacionIKlienteve_Ekspozim_SePDeFn_131372611580675568(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, report)
        {

        }
        public Rap_SituacionIKlienteve_Ekspozim_SePDeFn_131372611580675568(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();

        }
    }
}
