using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_KomisionetAnalitikeAgjenteve_SePDeFn_131583172726761533 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_KomisionetAnalitikeAgjenteve_SePDeFn_131583172726761533()
        {
            InitializeComponent();
        }
        String agjenti = "";
        public Rap_KomisionetAnalitikeAgjenteve_SePDeFn_131583172726761533(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_KomisionetAnalitikeAgjenteve_SePDeFn_131583172726761533(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {

            ResourceManager rm = new ResourceManager("Resources.Strings",
                    System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
            parameter6.Value = raport.Parameters[5].Value;
            if (raport.Parameters[4].Value.ToString() != "")
            {
                agjenti = raport.Parameters[4].Value.ToString();
            }
        }
    }
}
