using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.Blerje.Format_Printimi
{
    public partial class Rap_UrdherBlerjeVodafone : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_UrdherBlerjeVodafone(){InitializeComponent();}      
        public Rap_UrdherBlerjeVodafone(ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_UrdherBlerjeVodafone(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }
 
    }
}
