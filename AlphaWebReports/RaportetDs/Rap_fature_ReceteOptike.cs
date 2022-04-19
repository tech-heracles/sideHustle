using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs
{
    public partial class Rap_fature_ReceteOptike : DevExpress.XtraReports.UI.XtraReport
    {


        public Rap_fature_ReceteOptike() { InitializeComponent(); }

        public Rap_fature_ReceteOptike(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        { }

        public Rap_fature_ReceteOptike(CultureInfo ci, int idNdermarrje, int idPerdorues)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));


            xrLabel1.Text = rm.GetString("TitullRecete", ci);
            xrLabel2.Text = rm.GetString("lblRegjistruar", ci);
            xrLabel4.Text = rm.GetString("lblKlienti", ci);
            xrLabel9.Text = rm.GetString("lblDerguar", ci);
            xrLabel17.Text = rm.GetString("lblDtPrintimi", ci);
            xrLabel18.Text = rm.GetString("lblOrePrintimi", ci);

            xrTableCell36.Text = rm.GetString("lblSyriDjathte", ci);
            xrTableCell39.Text = rm.GetString("lblSyriMajte", ci);
            xrTableCell37.Text = rm.GetString("lblDiameter", ci);
            xrTableCell40.Text = rm.GetString("lblDiameter", ci);
            xrTableCell7.Text = rm.GetString("lblSfere", ci);
            xrTableCell4.Text = rm.GetString("lblSfere", ci);
            xrTableCell8.Text = rm.GetString("lblCilinder", ci);
            xrTableCell5.Text = rm.GetString("lblCilinder", ci);
            xrTableCell9.Text = rm.GetString("lblAksi", ci);
            xrTableCell19.Text = rm.GetString("lblAksi", ci);
            xrTableCell10.Text = rm.GetString("lblAdicioni", ci);
            xrTableCell20.Text = rm.GetString("lblAdicioni", ci);
            xrTableCell11.Text = rm.GetString("lblKanali", ci);
            xrTableCell21.Text = rm.GetString("lblKanali", ci);
            xrTableCell12.Text = rm.GetString("lblDegresioni", ci);
            xrTableCell22.Text = rm.GetString("lblDegresioni", ci);
            xrTableCell41.Text = rm.GetString("lblDiLarg", ci);
            xrTableCell42.Text = rm.GetString("lblDiAfer", ci);
            xrTableCell43.Text = rm.GetString("lblLartesia", ci);
            xrTableCell30.Text = rm.GetString("lblReceta", ci);
            xrTableCell31.Text = rm.GetString("lblDateDokumenti", ci);
            xrLabel12.Text = rm.GetString("lblReferimi", ci);
            xrLabel14.Text = rm.GetString("lblShenimeTeknike", ci);
        }

    }
}
