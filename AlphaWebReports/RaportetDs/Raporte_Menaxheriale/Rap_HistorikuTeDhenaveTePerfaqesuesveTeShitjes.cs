using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.Raporte_Menaxheriale
{
    public partial class Rap_HistorikuTeDhenaveTePerfaqesuesveTeShitjes : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_HistorikuTeDhenaveTePerfaqesuesveTeShitjes()
        {
            InitializeComponent();
        }
        public Rap_HistorikuTeDhenaveTePerfaqesuesveTeShitjes(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje,  report)
        {

        }

        public Rap_HistorikuTeDhenaveTePerfaqesuesveTeShitjes(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
        }
    }
}
