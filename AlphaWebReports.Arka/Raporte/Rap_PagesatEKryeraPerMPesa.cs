using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.Arka.Raporte
{
    public partial class Rap_PagesatEKryeraPerMPesa : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_PagesatEKryeraPerMPesa() { InitializeComponent(); }
        public Rap_PagesatEKryeraPerMPesa(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
     


     
        public Rap_PagesatEKryeraPerMPesa(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();

        }

    }
}
