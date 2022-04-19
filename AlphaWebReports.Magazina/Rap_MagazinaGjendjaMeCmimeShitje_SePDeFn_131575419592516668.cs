using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_MagazinaGjendjaMeCmimeShitje_SePDeFn_131575419592516668 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_MagazinaGjendjaMeCmimeShitje_SePDeFn_131575419592516668()
        {
            InitializeComponent();
        }
        int formatNumri = 0;
        string windowWidth = "";
        private bool teGrupuar = false;

        public Rap_MagazinaGjendjaMeCmimeShitje_SePDeFn_131575419592516668(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen, param.IdViti, param.IdSubRaporti, report)
        {

        }
        public Rap_MagazinaGjendjaMeCmimeShitje_SePDeFn_131575419592516668(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
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
            teGrupuar = (raport.Parameters["grupoSipasGrupim1"].Value.ToString() == "Po" || raport.Parameters["grupoSipasGrupim1"].Value.ToString() == "Yes");
            windowWidth = Convert.ToString(raport.Parameters[9].Value);
            formatNumri = Convert.ToInt32(raport.Parameters[19].Value);
            caktoFormatinENumrave();
          
        }
        private void caktoFormatinENumrave()
        {
            xrLabel7.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel33.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel3.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
            xrLabel6.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";
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
            xrLabel7.XlsxFormatString = xrLabel33.XlsxFormatString = xrLabel67.XlsxFormatString = xrLabel69.XlsxFormatString = xrLabel70.XlsxFormatString = xrLabel71.XlsxFormatString = xrLabel72.XlsxFormatString = xrLabel3.XlsxFormatString = xrLabel6.XlsxFormatString = xrLabel11.XlsxFormatString = xrLabel10.XlsxFormatString = xrLabel64.XlsxFormatString = xrLabel65.XlsxFormatString = xrLabel66.XlsxFormatString
                = 0.ToString("N" + formatNumri);
        }
    }
}
