using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatShitje_Brotech : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FormatShitje_Brotech()
        {
            InitializeComponent();
        }
        public Rap_FormatShitje_Brotech(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje)
        {

        }
        public Rap_FormatShitje_Brotech(CultureInfo ci, int idNdermarrje)
          {
            InitializeComponent();
          }
 
  }
}
