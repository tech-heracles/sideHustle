using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Collections.Generic;
using System.Data;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatEtiketatERecepturave : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatEtiketatERecepturave(){InitializeComponent();} 
        public Rap_FormatEtiketatERecepturave(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatEtiketatERecepturave(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
        }

        private void xrBarCode1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KODBARI") == null || GetCurrentColumnValue("KODBARI").ToString() == "")

            {
                xrBarCode1.Visible = false;
            }
        }
    }
}
