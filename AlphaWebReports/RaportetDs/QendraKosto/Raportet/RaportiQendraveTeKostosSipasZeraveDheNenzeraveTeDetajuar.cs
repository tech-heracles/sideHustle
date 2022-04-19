using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Collections.Generic;

namespace AlphaWebReports.RaportetDs.QendraKosto.Raportet
{
    public partial class RaportiQendraveTeKostosSipasZeraveDheNenzeraveTeDetajuar : DevExpress.XtraReports.UI.XtraReport
    {
        public RaportiQendraveTeKostosSipasZeraveDheNenzeraveTeDetajuar()
        {
            InitializeComponent();
        }

        public RaportiQendraveTeKostosSipasZeraveDheNenzeraveTeDetajuar(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, report)
        {

        }
        public RaportiQendraveTeKostosSipasZeraveDheNenzeraveTeDetajuar(CultureInfo ci, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();

        }

    }
}












