using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_OfertShitje_FEAR : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_OfertShitje_FEAR(){InitializeComponent();}    
        double sasia_total;
        int nr = 0;
        public Rap_OfertShitje_FEAR(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {
        }
        public Rap_OfertShitje_FEAR(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }
    }
}
