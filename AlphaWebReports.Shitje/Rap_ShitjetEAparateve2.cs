using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Resources;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_ShitjetEAparateve2 : DevExpress.XtraReports.UI.XtraReport
    {

        public Rap_ShitjetEAparateve2()
        {
            InitializeComponent();
        }
        public Rap_ShitjetEAparateve2(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, report)
        {

        }

        public Rap_ShitjetEAparateve2(CultureInfo ci, int idNdermarrje, XtraReport raport)
        {
            InitializeComponent();
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
            parameter6.Value = raport.Parameters[5].Value;
      
        }

    }
}
