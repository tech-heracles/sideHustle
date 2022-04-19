using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.CRM.Raporte
{
    public partial class Rap_EvidencaDetyrave : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_EvidencaDetyrave(){InitializeComponent();} 
        public Rap_EvidencaDetyrave(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, report)
        {

        }
        public Rap_EvidencaDetyrave(CultureInfo ci, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);

            xrLabel6.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel8.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            xrLabel10.Text = raport.Parameters[2].Description;
            parameter3.Value = raport.Parameters[2].Value;
            xrLabel12.Text = raport.Parameters[4].Description;
            parameter4.Value = raport.Parameters[4].Value;
            
        }

        private void EmrateLabelave(CultureInfo ci)
        {
            
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            xrTableCell1.Text = rm.GetString("labelKodi", ci);
            xrTableCell2.Text = rm.GetString("labelRaportPershkrimi", ci);
            xrTableCell3.Text = rm.GetString("lblRaportDataECaktuar", ci);
            xrTableCell4.Text = rm.GetString("labelData_e_mbarimit", ci);
            xrTableCell5.Text = rm.GetString("lblRaportDataERealizuar", ci);
            xrTableCell6.Text = rm.GetString("lblRaportRendesia", ci);
            xrTableCell7.Text = rm.GetString("lblRaportStatusi", ci);
            xrTableCell8.Text = rm.GetString("labelRaportiShenime", ci);
            xrTableCell9.Text = rm.GetString("lblRaportFotot", ci);

        }
    }
}
