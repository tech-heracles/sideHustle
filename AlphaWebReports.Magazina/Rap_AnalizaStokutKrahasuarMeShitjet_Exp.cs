using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_AnalizaStokutKrahasuarMeShitjet_Exp : DevExpress.XtraReports.UI.XtraReport
    {

        public Rap_AnalizaStokutKrahasuarMeShitjet_Exp()
        {
            InitializeComponent();
        }
        public Rap_AnalizaStokutKrahasuarMeShitjet_Exp(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_AnalizaStokutKrahasuarMeShitjet_Exp(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            parameter1.Value = raport.Parameters["IdNdermarje"].Value;
            parameter2.Value = raport.Parameters["filterKompania"].Value;
            parameter3.Value = raport.Parameters["filterDtDok"].Value;
            parameter4.Value = raport.Parameters["filterkodifikimartP"].Value;
            parameter5.Value = raport.Parameters["filterMagazina"].Value;
            parameter6.Value = raport.Parameters["filterKartela"].Value;

        }

    }
}
