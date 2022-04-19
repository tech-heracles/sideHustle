using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Collections.Generic;

namespace AlphaWebReports.RaportetDs.QendraKosto.Raportet
{
    public partial class RezultatiQendraveTeKostosMeZeraDheNenzera : DevExpress.XtraReports.UI.XtraReport
    {
        public RezultatiQendraveTeKostosMeZeraDheNenzera()
        {
            InitializeComponent();
        }

        public RezultatiQendraveTeKostosMeZeraDheNenzera(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, report)
        {

        }
        public RezultatiQendraveTeKostosMeZeraDheNenzera(CultureInfo ci, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();

        }

    }
}












