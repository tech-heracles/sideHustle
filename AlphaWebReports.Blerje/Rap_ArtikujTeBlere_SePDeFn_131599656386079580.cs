using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.Blerje
{
    public partial class Rap_ArtikujTeBlere_SePDeFn_131599656386079580 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_ArtikujTeBlere_SePDeFn_131599656386079580()
        {
            InitializeComponent();
        }
        public Rap_ArtikujTeBlere_SePDeFn_131599656386079580(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen, param.IdViti, param.IdSubRaporti, report)
        {

        }

        public Rap_ArtikujTeBlere_SePDeFn_131599656386079580(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();


        }
    }
}
