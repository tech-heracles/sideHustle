using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Blerje
{
    public partial class Rap_FormatBlerjeAlbamediaAng : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatBlerjeAlbamediaAng(){InitializeComponent();} 
        public Rap_FormatBlerjeAlbamediaAng(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatBlerjeAlbamediaAng(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }
 

    }
}
