using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura.Fatura_AutoElite
{
    public partial class FormatAUTOELITE4 : DevExpress.XtraReports.UI.XtraReport
    {
		public FormatAUTOELITE4(){InitializeComponent();} 
        public FormatAUTOELITE4(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public FormatAUTOELITE4(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            switch (ci.ToString())
            {
                case "sq-AL": //shqip
                    parameter1.Value = 0;
                    break;
                case "en_US": //anglisht
                    parameter1.Value = 1;
                    break;
                default: break;

            }
          
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));

          //  xrLabel1.Text = rm.GetString("RaportLibretMirembajtjeAutomjetiTitulli", ci);
            xrLabel13.Text = rm.GetString("labelAdresaUpperCase", ci);
            xrLabel7.Text = rm.GetString("labelKmAktuale", ci);
            xrLabel9.Text = rm.GetString("labelPershkrimVeprimiUpperCase", ci);
            xrLabel2.Text = rm.GetString("labelAdresaUpperCase", ci);
            xrTableCell3.Text = rm.GetString("labelKodiBaze", ci);
            xrTableCell5.Text = rm.GetString("labelPERSHKRIMI", ci) + " 2";
            xrTableCell11.Text = rm.GetString("labelNjesiaUpperCase", ci);
            xrTableCell16.Text = rm.GetString("label_SASIA", ci);
            xrTableCell7.Text = rm.GetString("label_CMIMI", ci);
            xrLabel22.Text = rm.GetString("labelDetyrimTotalUpperCase", ci);
        }

    }
}
