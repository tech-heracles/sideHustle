using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_KartelaArtikulliShitje_SePDeFn_131497028865529078 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_KartelaArtikulliShitje_SePDeFn_131497028865529078()
        {
            InitializeComponent();
        }
        public Rap_KartelaArtikulliShitje_SePDeFn_131497028865529078(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_KartelaArtikulliShitje_SePDeFn_131497028865529078(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
           

        }
    }
}
