using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatShitje_Dulac_SePDeFn_131344901442231515 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FormatShitje_Dulac_SePDeFn_131344901442231515()
        {
            InitializeComponent();
        }
        public Rap_FormatShitje_Dulac_SePDeFn_131344901442231515(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatShitje_Dulac_SePDeFn_131344901442231515(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
          
        }

    }
}
