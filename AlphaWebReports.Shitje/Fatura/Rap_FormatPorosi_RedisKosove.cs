using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatPorosi_RedisKosove : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FormatPorosi_RedisKosove()
        {
            InitializeComponent();
        }
        public Rap_FormatPorosi_RedisKosove(AlphaWebReports.Common.ParametraRaporti param, XtraReport report)
            :this(param.Ci,param.IdNdermarrje,param.IdPerdoruesi){ }
        public Rap_FormatPorosi_RedisKosove(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }



    }
}
