using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Resources;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_MaturimiFaturave : DevExpress.XtraReports.UI.XtraReport
    {
  
        public Rap_MaturimiFaturave()
        {
            InitializeComponent();
        }
        public Rap_MaturimiFaturave(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report)
        {

        }
        public Rap_MaturimiFaturave(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();

            xrLabel54.Text = raport.Parameters["IdNdermarje"].Description;
            parameter1.Value = raport.Parameters["IdNdermarje"].Value;
            xrLabel55.Text = raport.Parameters["filterKartela"].Description;
            parameter2.Value = raport.Parameters["filterKartela"].Value;
            xrLabel56.Text = raport.Parameters["filterKompania"].Description;
            parameter3.Value = raport.Parameters["filterKompania"].Value;
            parameterIdNderm.Value = idNdermarrje;
        }
    }
}
