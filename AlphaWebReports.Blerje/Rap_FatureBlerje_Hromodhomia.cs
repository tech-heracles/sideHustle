using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.Blerje
{
    public partial class Rap_FatureBlerje_Hromodhomia : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FatureBlerje_Hromodhomia()
        {
            InitializeComponent();
        }
        public Rap_FatureBlerje_Hromodhomia(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureBlerje_Hromodhomia(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }
    }
}
