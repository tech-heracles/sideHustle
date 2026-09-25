using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.Arka.Raporte
{
  
    public partial class Rap_ArketimetSipasDiteve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ArketimetSipasDiteve(){InitializeComponent();} 
       
        public Rap_ArketimetSipasDiteve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report)
        {

        }
        public Rap_ArketimetSipasDiteve(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();

            parameter1.Value = raport.Parameters["filterDtDok"].Value;
            parameter2.Value = raport.Parameters["filterArkaBankaEmer"].Value;
            parameter3.Value = raport.Parameters["filterKompania"].Value;

        }

        private void EmraTeLabelave(CultureInfo ci)
        {


        }
       

    }
}
