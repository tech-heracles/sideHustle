using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.KlientFurnitor.Raporte
{
    public partial class Rap_KartelaKlient_SePDeFn_131692964498814101 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_KartelaKlient_SePDeFn_131692964498814101()
        {
            InitializeComponent();
        }
        public Rap_KartelaKlient_SePDeFn_131692964498814101(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
     this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_KartelaKlient_SePDeFn_131692964498814101(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
          

        }
    }
}
