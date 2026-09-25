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

namespace AlphaWebReports.RaportetDs.ListPagesat.Raportet
{
    public partial class Rap_AnnualEmployeeRegister : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_AnnualEmployeeRegister(){InitializeComponent();} 
        public Rap_AnnualEmployeeRegister(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report)
        {

        }
        public Rap_AnnualEmployeeRegister(CultureInfo ci, int idNdermarrje, XtraReport report)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            
            InitializeComponent();
            parameter1.Value = report.Parameters["filterDtDok"].Value.ToString().Substring(19);

            var ndermarrja = new DbCore.DbAdmin.clsNdermarrje(idNdermarrje);
            xrLabel1.Text = ndermarrja.NdermarrjePershkrimi;
            xrLabel2.Text = ndermarrja.NdermarrjeVendi;
            xrLabel35.Text = $"Telefon {ndermarrja.NdermarrjeTel}            Fax {ndermarrja.NdermarrjeFax}";
         

        }

     
                 private void xrLabel33_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }
        private void xrTableCell162_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int diteleje = Convert.ToInt32(GetCurrentColumnValue("LEJEPUNE")?.ToString());
            if (diteleje == 0)
            {
                xrTableCell162.Text = " ";
            }
            else xrTableCell162.Text = GetCurrentColumnValue("LEJEPUNE")?.ToString();
        }

       
    }
}
