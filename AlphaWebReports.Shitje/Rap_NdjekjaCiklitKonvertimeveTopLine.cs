using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_NdjekjaCiklitKonvertimeveTopLine : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_NdjekjaCiklitKonvertimeveTopLine(){InitializeComponent();} 
        public Rap_NdjekjaCiklitKonvertimeveTopLine(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, report)
        {

        }
        public Rap_NdjekjaCiklitKonvertimeveTopLine(CultureInfo ci, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();

            xrLabel3.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel12.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            xrLabel10.Text = raport.Parameters[2].Description;
            parameter3.Value = raport.Parameters[2].Value;
            xrLabel15.Text = raport.Parameters[3].Description;
            parameter4.Value = raport.Parameters[3].Value;
            xrLabel19.Text = raport.Parameters[4].Description;
            parameter5.Value = raport.Parameters[4].Value;
            xrLabel23.Text = raport.Parameters[5].Description;
            parameter6.Value = raport.Parameters[5].Value;
            xrLabel5.Text = raport.Parameters[6].Description;
            parameter7.Value = raport.Parameters[6].Value;
            xrLabel8.Text = raport.Parameters[7].Description;
            parameter8.Value = raport.Parameters[7].Value;
            xrLabel13.Text = raport.Parameters[8].Description;
            parameter9.Value = raport.Parameters[8].Value;
            xrLabel17.Text = raport.Parameters[9].Description;
            parameter10.Value = raport.Parameters[9].Value;
            xrLabel21.Text = raport.Parameters[10].Description;
            parameter11.Value = raport.Parameters[10].Value;
            xrLabel25.Text = raport.Parameters[11].Description;
            parameter12.Value = raport.Parameters[11].Value;

            EmrateLabelave(ci);


        }
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel1.Text = rm.GetString("labelRaportNdekjaCiklitKonvertimeve", ci);
            xrLabel2.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel43.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel27.Text = rm.GetString("labelRaportNrRendor", ci);
            xrLabel28.Text = rm.GetString("lblLloji2", ci);
            xrLabel29.Text = rm.GetString("labelRaportiNr", ci);
            xrLabel30.Text = rm.GetString("labelRaportiDtDok", ci);
            xrLabel31.Text = rm.GetString("MenuItemKlientFurnitor", ci);
            xrLabel32.Text = rm.GetString("cmbCmimeArtikulliCaptionPershkrimi", ci);
            xrLabel33.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrLabel34.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel44.Text = rm.GetString("labelVleraMbetur", ci);
            xrLabel45.Text = rm.GetString("labelRaportAfatiKohor", ci);

        }

    }
}
