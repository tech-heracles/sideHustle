using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.Shitje.Fatura
{
    public partial class Rap_FatureShitjeMeArtikujDAST_SePDeFn_131547752265565425 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FatureShitjeMeArtikujDAST_SePDeFn_131547752265565425()
        {
            InitializeComponent();
        }
        public Rap_FatureShitjeMeArtikujDAST_SePDeFn_131547752265565425(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
          this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitjeMeArtikujDAST_SePDeFn_131547752265565425(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {

            InitializeComponent();
            parameterIdNderm.Value = idNdermarrje;
            parameterIdPerdoruesi.Value = idPerdoruesi;
        }
    }
}
