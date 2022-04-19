using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureShitjeMeArtikuj_SePDeFn_131400243735191731 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FatureShitjeMeArtikuj_SePDeFn_131400243735191731()
        {
            InitializeComponent();
        }
       
        public Rap_FatureShitjeMeArtikuj_SePDeFn_131400243735191731(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitjeMeArtikuj_SePDeFn_131400243735191731(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }


    }
}
