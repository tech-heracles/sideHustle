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
    public partial class Rap_EmployeeRegister : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_EmployeeRegister(){InitializeComponent();} 

        CultureInfo cult;

        public Rap_EmployeeRegister(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.IdGjuha, report)
        {

        }
        public Rap_EmployeeRegister(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi,int idgjuha, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            gjuha.Value = idgjuha;
            EmrateLabelave(ci);
            cult = ci;
          
        }

     
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));

           
         


        }

        private void xrTableCell19_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KOMBESIA") == null)
                xrTableCell19.Text = "";
            else if (cult.Name == "sq-AL")
                xrTableCell19.Text = GetCurrentColumnValue("KOMBESIA").ToString();
            else
                xrTableCell19.Text = GetCurrentColumnValue("KOMBESIAENG").ToString();


        }

        private void xrTableCell20_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("TIPKONTRATE") == null)
                xrTableCell20.Text = "";
            else if (cult.Name == "sq-AL")
                xrTableCell20.Text = GetCurrentColumnValue("TIPKONTRATE").ToString();
            else
                xrTableCell20.Text = GetCurrentColumnValue("TIPKONTRATEENG").ToString();
        }
    }
}
