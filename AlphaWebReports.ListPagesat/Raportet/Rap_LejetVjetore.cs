using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.ListPagesat.Raportet
{
    public partial class Rap_LejetVjetore : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_LejetVjetore(){InitializeComponent();} 
      
        public Rap_LejetVjetore(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_LejetVjetore(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
           
            InitializeComponent();
            
            xrLabel2.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel4.Text = raport.Parameters[3].Description;
            parameter2.Value = raport.Parameters[3].Value;
            xrLabel7.Text = raport.Parameters[2].Description;
            parameter3.Value = raport.Parameters[2].Value;
            xrLabel9.Text = raport.Parameters[1].Description;
            parameter4.Value = raport.Parameters[1].Value;
        }

        private void xrTableCell15_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

    }
}
