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
    public partial class FormatAUTOELITE1 : DevExpress.XtraReports.UI.XtraReport
    {
		public FormatAUTOELITE1(){InitializeComponent();} 
        public FormatAUTOELITE1(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public FormatAUTOELITE1(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            parameterIdNderm.Value = idNdermarrje;
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
            xrLabel50.Text = rm.GetString("labelSubjektiUpperCase", ci);
            xrLabel13.Text = rm.GetString("labelAdresaUpperCase", ci);
            xrLabel14.Text = rm.GetString("labelTelUpperCase", ci);
            xrLabel44.Text = rm.GetString("labelNrFatureUpperCase", ci);
            xrLabel8.Text = rm.GetString("labelDateFatureUpperCase", ci);
            xrLabel7.Text = rm.GetString("labelKmAktuale", ci);
            xrLabel9.Text = rm.GetString("labelPershkrimVeprimiUpperCase", ci);
            xrLabel4.Text = rm.GetString("labelSubjektiBleresUpperCase", ci);
            xrLabel20.Text = rm.GetString("labelModeliUpperCase", ci);
            xrLabel2.Text = rm.GetString("labelAdresaUpperCase", ci);
            xrLabel18.Text = rm.GetString("labelTelUpperCase", ci);
            xrLabel40.Text = rm.GetString("labelTargaUpperCase", ci);

            xrTableCell3.Text = rm.GetString("labelKodiBaze", ci);
            xrTableCell5.Text = rm.GetString("labelPERSHKRIMI", ci) + " 2";
            xrTableCell11.Text = rm.GetString("labelNjesiaUpperCase", ci);
            xrTableCell16.Text = rm.GetString("label_SASIA", ci);
            xrTableCell7.Text = rm.GetString("label_CMIMI", ci);
            xrTableCell9.Text = rm.GetString("labelZbritjaUpperCase", ci) + " %";
            xrTableCell10.Text = rm.GetString("labelTotaliPaZbritjeUpperCase", ci);
            xrTableCell8.Text = rm.GetString("labelTotaliMeZbritjeUpperCase", ci);

            xrLabel12.Text = rm.GetString("labelShitesiUpperCase", ci) + ":";

            xrLabel16.Text = rm.GetString("labelMonedhaUpperCase", ci);
            xrLabel25.Text = rm.GetString("labelDetyrimParaUpperCase", ci);
            xrLabel31.Text = rm.GetString("labelPageseUpperCase", ci);
            xrLabel37.Text = rm.GetString("labelDetyrimTotalUpperCase", ci);
            xrLabel27.Text = rm.GetString("labelTotalBrutoUpperCase", ci);
            xrLabel33.Text = rm.GetString("labelZbritjeTotal", ci);
            xrLabel39.Text = rm.GetString("labelTotalNeto", ci);
        }
    }
}
