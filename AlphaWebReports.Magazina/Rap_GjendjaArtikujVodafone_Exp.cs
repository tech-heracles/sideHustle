using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_GjendjaArtikujVodafone_Exp : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_GjendjaArtikujVodafone_Exp()
        {
            InitializeComponent();
        }
        public Rap_GjendjaArtikujVodafone_Exp(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report)
        {

        }


        public Rap_GjendjaArtikujVodafone_Exp(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
        }
    }
}
