using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class RapArtikujTeShiturMeSeriale : DevExpress.XtraReports.UI.XtraReport
    {
        public RapArtikujTeShiturMeSeriale()
        {
            InitializeComponent();
        }

        public RapArtikujTeShiturMeSeriale(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public RapArtikujTeShiturMeSeriale(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }

    }
}
