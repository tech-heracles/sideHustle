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
    public partial class Rap_CostCenterReport : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_CostCenterReport(){InitializeComponent();} 
        CultureInfo cult;
        public Rap_CostCenterReport(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_CostCenterReport(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            cult = ci;
        }
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel12.Text = rm.GetString("TitullRaportRaportiPunonjësQK", ci);
        }

        private void xrTableCell45_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("JOBTITLE") == null)
                xrTableCell45.Text = "";
            else if (cult.Name == "sq-AL")
                 xrTableCell45.Text = GetCurrentColumnValue("JOBTITLE").ToString();
            else
                xrTableCell45.Text = GetCurrentColumnValue("JOBTITLEENG").ToString();
        }
    }
}
