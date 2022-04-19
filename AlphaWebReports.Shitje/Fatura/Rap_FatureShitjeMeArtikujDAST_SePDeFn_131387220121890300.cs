using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureShitjeMeArtikujDAST_SePDeFn_131387220121890300 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FatureShitjeMeArtikujDAST_SePDeFn_131387220121890300()
        {
            InitializeComponent();
        }
        public Rap_FatureShitjeMeArtikujDAST_SePDeFn_131387220121890300(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitjeMeArtikujDAST_SePDeFn_131387220121890300(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {

            InitializeComponent();
            parameterIdNderm.Value = idNdermarrje;
            parameterIdPerdoruesi.Value = idPerdoruesi;
        }
    }
}
