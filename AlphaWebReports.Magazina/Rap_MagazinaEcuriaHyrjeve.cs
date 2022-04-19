using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_MagazinaEcuriaHyrjeve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_MagazinaEcuriaHyrjeve(){InitializeComponent();} 
        public Rap_MagazinaEcuriaHyrjeve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
           this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }

        public Rap_MagazinaEcuriaHyrjeve(System.Globalization.CultureInfo ci, int idNdermarrje, int idViti, XtraReport report)
        {
            InitializeComponent();
        }

    }

}
