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
         

            //DateTime moment = DateTime.ParseExact(report.Parameters["filterDtDok2"].Value.ToString(), "dd/mm/yyyy", CultureInfo.InvariantCulture);
            //   // Convert.ToDateTime(DateTime.ParseExact(report.Parameters["filterDtDok2"].Value.ToString(), "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture));
           
            //int viti = moment.Year;
            //xrLabel33.Text = viti.ToString();
    
        }

     
                 private void xrLabel33_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
             //DateTime Viti= DateTime.ParseExact(GetCurrentColumnValue("Viti1").ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
             //xrLabel33.Text = Viti.ToString("dd/MM/yyyy");
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
