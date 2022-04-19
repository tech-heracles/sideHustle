using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.Arka.Raporte
{
    public partial class Rap_ARKETIMETORARE : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_ARKETIMETORARE()
        {
            InitializeComponent();
        }
     
        public Rap_ARKETIMETORARE(AlphaWebReports.Common.ParametraRaporti param, XtraReport report)
            :this(param.Ci, param.IdNdermarrje, report) { }
        public Rap_ARKETIMETORARE(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            if (this.Extensions["filtrat"] == null || this.Extensions["filtrat"] == "null") // vjen nga dergimi me email
                PageHeader.Visible = false;
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }


        private void xrTable1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            object objEshteArketim = GetCurrentColumnValue("ESHTEARKETIM");
            if (objEshteArketim != null && objEshteArketim != DBNull.Value)
            {
                if (Convert.ToInt32(objEshteArketim) == 1)
                    xrTable1.ForeColor = Color.Black;
                else
                    xrTable1.ForeColor = Color.Red;
            }
        }

        private void filtraTable_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.Extensions["filtrat"] != "null" && this.Extensions["filtrat"] != null)
                AlphaWebReports.raporteUtil.shtoFiltra((XRTable)sender, null, AlphaWebReports.raporteUtil.DeserializoParametrat(this.Extensions["filtrat"]));
        }
    }
}

