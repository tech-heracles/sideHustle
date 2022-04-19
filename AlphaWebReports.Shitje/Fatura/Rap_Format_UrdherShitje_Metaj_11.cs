using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_Format_UrdherShitje_Metaj_11 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_Format_UrdherShitje_Metaj_11() { InitializeComponent(); }
        public Rap_Format_UrdherShitje_Metaj_11(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_Format_UrdherShitje_Metaj_11(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        /// 
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel50.Text = rm.GetString("labelSubjektiUpperCase", ci);
            xrTableCell3.Text = rm.GetString("labelKodiBaze", ci);
            xrTableCell11.Text = rm.GetString("labelNjesiaUpperCase", ci);
            xrTableCell5.Text = rm.GetString("labelPERSHKRIMI", ci) + " 1";
            xrTableCell16.Text = rm.GetString("label_SASIA", ci);
        }

        }
}
