using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatShitje_Cobalt_SePDeFn_131294813606238545 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FormatShitje_Cobalt_SePDeFn_131294813606238545() { InitializeComponent(); }
        public Rap_FormatShitje_Cobalt_SePDeFn_131294813606238545(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatShitje_Cobalt_SePDeFn_131294813606238545(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        parameterIdNderm.Value = idNdermarrje;
        }
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel7.Text = rm.GetString("labelRaportDataFatures", ci);
            xrLabel8.Text = rm.GetString("labelRaportKodKlienti", ci);
            xrTableCell4.Text = rm.GetString("labelRaportiKF", ci);
            xrTableCell5.Text = rm.GetString("labelPERSHKRIMI", ci);
            xrTableCell10.Text = rm.GetString("labelRaportiSasi", ci);
            xrTableCell8.Text = rm.GetString("labelVendodhjaUppercase", ci);
        }
    }
}
