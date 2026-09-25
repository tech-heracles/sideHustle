using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_UrdherShitjetSipasMenPageses : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_UrdherShitjetSipasMenPageses(){InitializeComponent();} 

        public Rap_UrdherShitjetSipasMenPageses(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_UrdherShitjetSipasMenPageses(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
        }
    }
}
