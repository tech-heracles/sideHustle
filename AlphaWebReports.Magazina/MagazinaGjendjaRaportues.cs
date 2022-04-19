using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Collections.Generic;

namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class MagazinaGjendjaRaportues : DevExpress.XtraReports.UI.XtraReport
    {
		public MagazinaGjendjaRaportues(){InitializeComponent();} 
        int formatNumri = 0;
        string windowWidth = "";
        public MagazinaGjendjaRaportues(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.IdRaporti, param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.IdGjuha, report)
        {

        }
        public MagazinaGjendjaRaportues(int idRaporti, CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, int idGjuha, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
            parameter6.Value = raport.Parameters[5].Value;
            parameter7.Value = raport.Parameters[6].Value;
            parameter8.Value = raport.Parameters[13].Value;
            parameter9.Value = raport.Parameters[14].Value;
            parameter10.Value = raport.Parameters[7].Value;
            parameter11.Value = raport.Parameters[10].Value;
            DegaAdministrative.Value = raport.Parameters[11].Value;
            parameter12.Value = raport.Parameters["filterKodbari"].Value;
            windowWidth = Convert.ToString(raport.Parameters[9].Value);
            formatNumri = 2;
            caktoFormatinENumrave();

            
            parameterFormatNumri.Value = formatNumri;

        }

        private void caktoFormatinENumrave()
        {
            xrLabel9.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel33.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel7.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel8.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel11.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel10.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel64.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel65.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel66.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel67.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel69.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel69.Summary.FormatString = "{0:n" + formatNumri + "}";
            xrLabel70.Summary.FormatString = "{0:n" + formatNumri + "}";
            xrLabel71.Summary.FormatString = "{0:n" + formatNumri + "}";
            xrLabel72.Summary.FormatString = "{0:n" + formatNumri + "}";
            xrLabel70.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel71.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel72.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel9.XlsxFormatString = xrLabel33.XlsxFormatString = xrLabel67.XlsxFormatString = xrLabel69.XlsxFormatString = xrLabel70.XlsxFormatString = xrLabel71.XlsxFormatString = xrLabel72.XlsxFormatString = xrLabel7.XlsxFormatString = xrLabel8.XlsxFormatString = xrLabel11.XlsxFormatString = xrLabel10.XlsxFormatString = xrLabel64.XlsxFormatString = xrLabel65.XlsxFormatString = xrLabel66.XlsxFormatString
                = 0.ToString("N" + formatNumri);
        }

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportGjendjaeMagazinesRaportuesTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell11.Text = rm.GetString("labelKartela", ci);
            xrTableCell7.Text = rm.GetString("labelFilterAvancuarKodbari", ci);
            xrTableCell8.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell9.Text = rm.GetString("koloneLoginNdermarrjeNdermarrja", ci);
            xrTableCell10.Text = rm.GetString("labelNjesia", ci);
            xrTableCell19.Text = rm.GetString("labelRaportLLogariInventar", ci);
            xrTableCell17.Text = rm.GetString("labelRaportGjendjaeMeparshme", ci);
            xrTableCell21.Text = rm.GetString("labelRaportSasiHyrje", ci);
            xrTableCell23.Text = rm.GetString("labelRaportSasiaDalje", ci);
            xrTableCell29.Text = rm.GetString("labelVlefta", ci);
            xrTableCell25.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
            xrTableCell27.Text = rm.GetString("labelRaportKosto", ci);
            xrTableCell32.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel31.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell38.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell31.Text = rm.GetString("filterMagazina", ci);
            xrLabel77.Text = rm.GetString("labelLogoIMB", ci);
        }

    }
}
