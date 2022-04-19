using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatShitje_Shpresa : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FormatShitje_Shpresa() { InitializeComponent(); }

        public Rap_FormatShitje_Shpresa(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatShitje_Shpresa(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            label8.Text = rm.GetString("lblPreventiv", ci);
            xrLabel44.Text = rm.GetString("labelRaportNr", ci);
            xrLabel7.Text = rm.GetString("labelDate", ci);
        
            xrTableCell3.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell5.Text = rm.GetString("labelRaportiPershkrimi", ci);
      
            xrTableCell7.Text = rm.GetString("labelNjesia", ci);
            xrTableCell9.Text = rm.GetString("labelSasia", ci);
            xrTableCell10.Text = rm.GetString("labelCmimi", ci);
            xrTableCell8.Text = rm.GetString("labelRaportVlera", ci);
            label1.Text = rm.GetString("MenuItemRaportArka", ci);
            label3.Text = rm.GetString("labelVlerafatures", ci) + ":";
            label5.Text = rm.GetString("lblDETYRIMI", ci) + ":";
            label4.Text = rm.GetString("lblShumaPaguar", ci) + ":";
        }
    }
}
