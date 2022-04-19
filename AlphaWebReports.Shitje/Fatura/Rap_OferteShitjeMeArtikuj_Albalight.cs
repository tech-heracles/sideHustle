using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_OferteShitjeMeArtikuj_Albalight : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_OferteShitjeMeArtikuj_Albalight() {  InitializeComponent(); }
        public Rap_OferteShitjeMeArtikuj_Albalight(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_OferteShitjeMeArtikuj_Albalight(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }
    }
}
