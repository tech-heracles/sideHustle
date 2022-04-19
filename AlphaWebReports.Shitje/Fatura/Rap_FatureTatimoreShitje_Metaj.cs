using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureTatimoreShitje_Metaj : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FatureTatimoreShitje_Metaj()
        {
            InitializeComponent();
        }
        public Rap_FatureTatimoreShitje_Metaj(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureTatimoreShitje_Metaj(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();

       
        }

    }
}
