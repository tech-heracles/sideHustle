using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs.ListPagesat.Raportet
{
    public partial class Rap_KartelaPages_ESC : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_KartelaPages_ESC(){InitializeComponent();} 
        public Rap_KartelaPages_ESC(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje)
        {

        }
        public Rap_KartelaPages_ESC(System.Globalization.CultureInfo ci, int idNdermarrje)
        {
            InitializeComponent();
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
          
        }

        private void Rap_MemoAnnualBonus_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            System.Data.DataSet ds = ((System.Data.DataSet)this.DataSource);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                GroupHeader1.Visible = false;
                Detail.Visible = false;
                GroupFooter1.Visible = false;
            }
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {      System.Data.DataSet ds = ((System.Data.DataSet)this.DataSource);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                GroupHeader1.Visible = false;

        }

        private void PageFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            System.Data.DataSet ds = ((System.Data.DataSet)this.DataSource);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                GroupFooter1.Visible = false;
        }

        private void xrLabel42_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            switch (this.GetCurrentColumnValue("MUAJI").ToString())
            {
                case "1":
                    xrLabel42.Text = "January";
                    break;
                case "2":
                    xrLabel42.Text = "February";
                    break;
                case "3":
                    xrLabel42.Text = "March";
                    break;
                case "4":
                    xrLabel42.Text = "April";
                    break;
                case "5":
                    xrLabel42.Text = "May";
                    break;
                case "6":
                    xrLabel42.Text = "June";
                    break;
                case "7":
                    xrLabel42.Text = "July";
                    break;
                case "8":
                    xrLabel42.Text = "August";
                    break;
                case "9":
                    xrLabel42.Text = "September";
                    break;
                case "10":
                    xrLabel42.Text = "October";
                    break;
                case "11":
                    xrLabel42.Text = "November";
                    break;
                case "12":
                    xrLabel42.Text = "December";
                    break;
                default:
                    break;
            }
            xrLabel42.Text += " " + ((this.GetCurrentColumnValue("VITI") != null)?this.GetCurrentColumnValue("VITI").ToString() : "");

        }

     

    }
}
