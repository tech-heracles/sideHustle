using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.Banka.Raporte
{
    public partial class rap_MandatArketimPages_SePDeFn_131370645061717747 : DevExpress.XtraReports.UI.XtraReport
    {
        public rap_MandatArketimPages_SePDeFn_131370645061717747()
        {
            InitializeComponent();
        }
        private ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));

        public rap_MandatArketimPages_SePDeFn_131370645061717747(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje)
        {

        }
        public rap_MandatArketimPages_SePDeFn_131370645061717747(CultureInfo ci, int idNdermarrje)
        {
            InitializeComponent();

            parameterlabelRaportArketoniNga.Value = rm.GetString("labelRaportArketoniNga", ci);
            parameterlabelRAportUPaguaPer.Value = rm.GetString("labelRAportUPaguaPer", ci);
            labelRaportUrdherXhirimKlienti.Value = rm.GetString("labelRaportUrdherXhirimKlienti", ci);
            parameterlabelRaportUrdherXhirimYne.Value = rm.GetString("labelRaportUrdherXhirimYne", ci);
            labelRaportMAndatArketimi.Value = rm.GetString("labelRaportMAndatArketimi", ci);
            parameterlabelRaportMandatPagese.Value = rm.GetString("labelRaportMandatPagese", ci);
            parameterlabelRaportDerdhje.Value = rm.GetString("labelRaportDerdhje", ci);
            parameterlabelRaportTerheqje.Value = rm.GetString("labelRaportTerheqje", ci);
            parameterlabelRaportDhenesi.Value = rm.GetString("labelRaportDhenesi", ci);
            parameterlabelRaportMarresi.Value = rm.GetString("labelRaportMarresi", ci);
        }

    }
}
