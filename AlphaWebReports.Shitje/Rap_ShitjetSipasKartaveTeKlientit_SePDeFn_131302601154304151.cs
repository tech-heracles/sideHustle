using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Resources;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_ShitjetSipasKartaveTeKlientit_SePDeFn_131302601154304151 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_ShitjetSipasKartaveTeKlientit_SePDeFn_131302601154304151() { InitializeComponent(); }

        public Rap_ShitjetSipasKartaveTeKlientit_SePDeFn_131302601154304151(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.IdRaporti, param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_ShitjetSipasKartaveTeKlientit_SePDeFn_131302601154304151(int idRaporti, CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            parameter1.Value = raport.Parameters[1].Value;
            parameter2.Value = raport.Parameters[3].Value;
            parameter3.Value = raport.Parameters[4].Value;
            parameter4.Value = raport.Parameters[5].Value;
            parameter5.Value = raport.Parameters[6].Value;
            parameter6.Value = raport.Parameters[7].Value;
          
        }

    }
}
