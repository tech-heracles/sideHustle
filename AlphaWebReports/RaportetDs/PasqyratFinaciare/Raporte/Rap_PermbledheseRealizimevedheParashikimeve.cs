using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraReports.UI.PivotGrid;
using System.Globalization;
using System.Resources;
using System.Reflection;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.PasqyratFinaciare.Raporte
{
    public partial class Rap_PermbledheseRealizimevedheParashikimeve : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_PermbledheseRealizimevedheParashikimeve() { InitializeComponent(); }

        public Rap_PermbledheseRealizimevedheParashikimeve(ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        { }
        public Rap_PermbledheseRealizimevedheParashikimeve(CultureInfo ci, int idNdermarrje, int idViti, XtraReport report)
        {

            InitializeComponent();
        }

    

    }
}