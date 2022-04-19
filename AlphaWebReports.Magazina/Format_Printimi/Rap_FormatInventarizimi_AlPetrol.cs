using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.Magazina.Format_Printimi
{
    public partial class Rap_FormatInventarizimi_AlPetrol : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FormatInventarizimi_AlPetrol() { InitializeComponent(); }

        

        public Rap_FormatInventarizimi_AlPetrol(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatInventarizimi_AlPetrol(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
          
            InitializeComponent();
        }

    }
}
