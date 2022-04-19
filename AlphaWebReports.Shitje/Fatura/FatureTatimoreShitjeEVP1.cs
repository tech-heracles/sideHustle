using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class FatureTatimoreShitjeEVP1 : DevExpress.XtraReports.UI.XtraReport
    {
        public FatureTatimoreShitjeEVP1()
        {
            InitializeComponent();
        }
        public FatureTatimoreShitjeEVP1(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public FatureTatimoreShitjeEVP1(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();

        }

    }
}
