using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs
{
    public partial class RapOfertaBlerje : DevExpress.XtraReports.UI.XtraReport
    {
        public RapOfertaBlerje() { InitializeComponent(); }

        public RapOfertaBlerje(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci)
        { }

        public RapOfertaBlerje(CultureInfo ci) { InitializeComponent(); }
    }
}
