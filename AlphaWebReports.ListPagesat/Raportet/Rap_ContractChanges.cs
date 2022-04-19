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
    public partial class Rap_ContractChanges : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ContractChanges(){InitializeComponent();} 
      
        public Rap_ContractChanges(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.IdGjuha, report)
        {

        }
        public Rap_ContractChanges(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi,int idgjuha, DevExpress.XtraReports.UI.XtraReport raport)
        {
           
            InitializeComponent(); gjuha.Value = idgjuha;
            EmrateLabelave(ci);
        }
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));

           
         


        }

        private void xrTableCell50_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("ACTUALFUNCTION") != null && GetCurrentColumnValue("ACTUALFUNCTION").ToString() != "")
            {
                xrTableCell50.Text = GetCurrentColumnValue("ACTUALFUNCTION").ToString();
            }
            else if (GetCurrentColumnValue("ACTUALLEVEL1") != null)
            {
                xrTableCell43.Text = GetCurrentColumnValue("ACTUALLEVEL1").ToString();
            }
        }
    

        private void xrTableCell43_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if(GetCurrentColumnValue("PREVIOUSFUNCTION") != null && GetCurrentColumnValue("PREVIOUSFUNCTION").ToString() != "")
            {
                xrTableCell43.Text = GetCurrentColumnValue("PREVIOUSFUNCTION").ToString();
            }else if (GetCurrentColumnValue("PREVIOUSLEVEL1") != null)
            {
                xrTableCell43.Text = GetCurrentColumnValue("PREVIOUSLEVEL1").ToString();
            }
        }
    }
}
