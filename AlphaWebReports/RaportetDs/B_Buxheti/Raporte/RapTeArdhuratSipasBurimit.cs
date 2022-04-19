using System;
using System.Collections;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.B_Buxheti.Raporte
{
    public partial class RapTeArdhuratSipasBurimit : XtraReport
    {
        public RapTeArdhuratSipasBurimit()
        {
            InitializeComponent();
        }

        public RapTeArdhuratSipasBurimit(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, report)
        {

        }

        public RapTeArdhuratSipasBurimit(CultureInfo ci, XtraReport raport)
        {
            InitializeComponent();
        }        
    }
}

   
      
