using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.Raporte_Menaxheriale
{
    public partial class Rap_LargimetPerfaqesuesveTeShitjesVjetore : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_LargimetPerfaqesuesveTeShitjesVjetore()
        {
            InitializeComponent();
        }
        public Rap_LargimetPerfaqesuesveTeShitjesVjetore(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje,  report)
        {

        }
        public Rap_LargimetPerfaqesuesveTeShitjesVjetore(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
        }


    }
}
