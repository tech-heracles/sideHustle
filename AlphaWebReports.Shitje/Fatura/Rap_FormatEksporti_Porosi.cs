using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatEksporti_Porosi : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FormatEksporti_Porosi()
        {
            InitializeComponent();
        }
        public Rap_FormatEksporti_Porosi(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatEksporti_Porosi(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }
    }
}
