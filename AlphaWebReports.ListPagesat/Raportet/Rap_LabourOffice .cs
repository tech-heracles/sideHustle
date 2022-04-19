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
    public partial class Rap_LabourOffice : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_LabourOffice(){InitializeComponent();} 

        public Rap_LabourOffice(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.IdGjuha, report)
        {

        }
        public Rap_LabourOffice(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi,int idgjuha, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            
            parameter1.Value = raport.Parameters["IdNdermarje"].Value;
            parameter3.Value = raport.Parameters["filterKodDep"].Value;
            parameter4.Value = raport.Parameters["filterNrPersonalPunonjesi"].Value;
            gjuha.Value = idgjuha;
            
        }

        private void xrTableCell15_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

    }
}
