using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Resources;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_Shitje_VFONE : DevExpress.XtraReports.UI.XtraReport
    {
      
        public Rap_Shitje_VFONE()
        {
            InitializeComponent();
        }
        public Rap_Shitje_VFONE(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report)
        {

        }
        public Rap_Shitje_VFONE(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            xrLabel54.Text = raport.Parameters["IdNdermarje"].Description;
            parameter1.Value = raport.Parameters["IdNdermarje"].Value;
            xrLabel55.Text = raport.Parameters["filterDtDok"].Description;
            parameter2.Value = raport.Parameters["filterDtDok"].Value;
            xrLabel56.Text = raport.Parameters["filterMagazina"].Description;
            parameter3.Value = raport.Parameters["filterMagazina"].Value;
            xrLabel57.Text = raport.Parameters["filterKartela"].Description;
            parameter4.Value = raport.Parameters["filterKartela"].Value;
            xrLabel58.Text = raport.Parameters["filterKompania"].Description;
            parameter5.Value = raport.Parameters["filterKompania"].Value;
            xrLabel67.Text = raport.Parameters["filterDetajimP"].Description;
            parameter6.Value = raport.Parameters["filterDetajimP"].Value;

        }
    }
}
