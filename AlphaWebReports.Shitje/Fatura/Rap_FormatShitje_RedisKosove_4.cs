using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatShitje_RedisKosove_4 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FormatShitje_RedisKosove_4()
        {
            InitializeComponent();
        }
        public Rap_FormatShitje_RedisKosove_4(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):this(param.Ci,param.IdNdermarrje,param.IdPerdoruesi){ }
        public Rap_FormatShitje_RedisKosove_4(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }

    }
}
