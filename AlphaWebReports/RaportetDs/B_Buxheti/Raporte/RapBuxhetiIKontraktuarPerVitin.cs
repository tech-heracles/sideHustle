using System;
using System.Collections;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Data;

namespace AlphaWebReports.RaportetDs.B_Buxheti.Raporte
{
    public partial class RapBuxhetiIKontraktuarPerVitin : XtraReport
    {
        public RapBuxhetiIKontraktuarPerVitin()
        {
            InitializeComponent();
        }

        public RapBuxhetiIKontraktuarPerVitin(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, report)
        {

        }

        public RapBuxhetiIKontraktuarPerVitin(CultureInfo ci, XtraReport raport)
        {
            InitializeComponent();

        }
    }
}

   
      
