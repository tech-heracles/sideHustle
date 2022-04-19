using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Blerje.Format_Printimi
{
    public partial class Rap_FormatPrintimiBlerje_TopLine : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatPrintimiBlerje_TopLine(){InitializeComponent();} 
        public Rap_FormatPrintimiBlerje_TopLine(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatPrintimiBlerje_TopLine(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }

    }
}
