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
    public partial class Rap_HistorikPunonjesi : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_HistorikPunonjesi(){InitializeComponent();} 
        public Rap_HistorikPunonjesi(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report)
        {

        }
        public Rap_HistorikPunonjesi(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
        }


        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",System.Reflection.Assembly.Load("App_GlobalResources"));

            xrTableCell1.Text = rm.GetString("lblData", ci);
            xrTableCell3.Text = rm.GetString("labelRaportVlera", ci);
            xrLabel2.Text = rm.GetString("msgLupaPunonjesPunonjesi", ci);
        }
   }
}
