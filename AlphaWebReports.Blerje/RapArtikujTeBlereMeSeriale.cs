using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.Blerje
{
    public partial class RapArtikujTeBlereMeSeriale : DevExpress.XtraReports.UI.XtraReport
    {
        public RapArtikujTeBlereMeSeriale()
        {
            InitializeComponent();
        }
        public RapArtikujTeBlereMeSeriale(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public RapArtikujTeBlereMeSeriale(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }
    }
}
