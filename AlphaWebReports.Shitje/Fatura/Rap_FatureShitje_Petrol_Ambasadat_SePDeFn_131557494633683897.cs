using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureShitje_Petrol_Ambasadat_SePDeFn_131557494633683897 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FatureShitje_Petrol_Ambasadat_SePDeFn_131557494633683897()
        {
            InitializeComponent();
        }
        public Rap_FatureShitje_Petrol_Ambasadat_SePDeFn_131557494633683897(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitje_Petrol_Ambasadat_SePDeFn_131557494633683897(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            parameterIdNderm.Value = idNdermarrje;
            parameterIdPerdoruesi.Value = idPerdoruesi;
        }
    }
}
