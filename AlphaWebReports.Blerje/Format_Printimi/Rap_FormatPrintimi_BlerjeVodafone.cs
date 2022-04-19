using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.Blerje.Format_Printimi
{
    public partial class Rap_FormatPrintimi_BlerjeVodafone : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FormatPrintimi_BlerjeVodafone()
        {
            InitializeComponent();
        }
        public Rap_FormatPrintimi_BlerjeVodafone(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
        this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatPrintimi_BlerjeVodafone(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }
    }
}
