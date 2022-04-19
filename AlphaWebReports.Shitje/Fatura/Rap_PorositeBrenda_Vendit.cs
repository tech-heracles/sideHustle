using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_PorositeBrenda_Vendit : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_PorositeBrenda_Vendit()
        {
            InitializeComponent();
        }
        public Rap_PorositeBrenda_Vendit(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_PorositeBrenda_Vendit(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }

    }
}
