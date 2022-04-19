using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.Arka.Raporte
{
    public partial class RAP_UserKrediti : DevExpress.XtraReports.UI.XtraReport
    {
        public RAP_UserKrediti()
        {
            InitializeComponent();
        }
        public RAP_UserKrediti(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report)
        {

        }
        public RAP_UserKrediti(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
        }
    }
}
