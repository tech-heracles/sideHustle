using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.Arka.Raporte
{
    public partial class RAP_PagesatPerMPesaSipasIntervaleve : DevExpress.XtraReports.UI.XtraReport
    {
        public RAP_PagesatPerMPesaSipasIntervaleve() { InitializeComponent(); }
        public RAP_PagesatPerMPesaSipasIntervaleve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public RAP_PagesatPerMPesaSipasIntervaleve(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
         
        }



        
    }
}
    