using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureShitjeAlbas_SePDeFn_131442608636216609 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FatureShitjeAlbas_SePDeFn_131442608636216609()
        {
            InitializeComponent();
        }
        public Rap_FatureShitjeAlbas_SePDeFn_131442608636216609(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitjeAlbas_SePDeFn_131442608636216609(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }
    }
}
