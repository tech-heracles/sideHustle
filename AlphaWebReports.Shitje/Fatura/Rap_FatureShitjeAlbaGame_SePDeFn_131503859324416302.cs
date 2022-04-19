using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureShitjeAlbaGame_SePDeFn_131503859324416302 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FatureShitjeAlbaGame_SePDeFn_131503859324416302()
        {
            InitializeComponent();
        }

        public Rap_FatureShitjeAlbaGame_SePDeFn_131503859324416302(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitjeAlbaGame_SePDeFn_131503859324416302(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }
    }
}
