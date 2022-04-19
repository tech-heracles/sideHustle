using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Resources;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.KlientFurnitor.Raporte
{
    public partial class Rap_GjendjaEPermbledhurEKartaveKlient : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_GjendjaEPermbledhurEKartaveKlient(){InitializeComponent();} 
        public Rap_GjendjaEPermbledhurEKartaveKlient(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_GjendjaEPermbledhurEKartaveKlient(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
          
        }

    }
}
