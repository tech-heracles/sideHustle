using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_ArtikujGjendja_SePDeFn_131576498589849832 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_ArtikujGjendja_SePDeFn_131576498589849832()
        {
            InitializeComponent();
        }
        public Rap_ArtikujGjendja_SePDeFn_131576498589849832(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_ArtikujGjendja_SePDeFn_131576498589849832(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
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

            parameter7.Value = raport.Parameters[6].Value;
            parameter8.Value = raport.Parameters[11].Value;
            parameter9.Value = raport.Parameters[12].Value;
            parameter10.Value = raport.Parameters[7].Value;
            degaAdministrative.Value = raport.Parameters[8].Value;
            parameter11.Value = raport.Parameters[9].Value;
            if (raport.Parameters["grupoSipasGrupim1"].Value.ToString() == "Jo" || raport.Parameters["grupoSipasGrupim1"].Value.ToString() == "No")
                xrLabel13.Text = rm.GetString("labelKartela", ci);
            else
                xrLabel13.Text = rm.GetString("labelKodi", ci);

            parameterIdNderm.Value = idNdermarrje;
        }
    }
}
