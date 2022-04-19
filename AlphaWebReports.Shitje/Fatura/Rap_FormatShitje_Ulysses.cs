using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatShitje_Ulysses : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FormatShitje_Ulysses()
        {
            InitializeComponent();
        }
        public Rap_FormatShitje_Ulysses(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen, param.IdViti, param.IdSubRaporti, report)
        { }
        public Rap_FormatShitje_Ulysses(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();

        }

    }
}
