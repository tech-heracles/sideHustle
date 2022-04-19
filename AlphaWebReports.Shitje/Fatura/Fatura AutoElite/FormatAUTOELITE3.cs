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
    public partial class FormatAUTOELITE3 : DevExpress.XtraReports.UI.XtraReport
    {
		public FormatAUTOELITE3(){InitializeComponent();} 
        public FormatAUTOELITE3(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public FormatAUTOELITE3(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }
   

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze 
        /// te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel1.Text = rm.GetString("RaportLibretMirembajtjeAutomjetiTitulli", ci);

            xrLabel50.Text = rm.GetString("labelSubjektiUpperCase", ci);
            xrLabel13.Text = rm.GetString("labelAdresaUpperCase", ci);
            xrLabel14.Text = rm.GetString("labelTelUpperCase", ci);

            xrLabel44.Text = rm.GetString("labelNrFatureUpperCase", ci);
            xrLabel8.Text = rm.GetString("labelDateFatureUpperCase", ci);
            xrLabel9.Text = rm.GetString("labelNrSerial", ci);
            xrLabel7.Text = rm.GetString("labelKmAktuale", ci);
            xrLabel4.Text = rm.GetString("labelSubjektiBleresUpperCase", ci);
            xrLabel20.Text = rm.GetString("labelModeliUpperCase", ci);
            xrLabel2.Text = rm.GetString("labelAdresaUpperCase", ci);
            xrLabel33.Text = rm.GetString("labelNIPT", ci);
            xrLabel18.Text = rm.GetString("labelTelUpperCase", ci);
            xrLabel26.Text = rm.GetString("labelTargaUpperCase", ci);

            xrTableCell3.Text = rm.GetString("labelKodiBaze", ci);
            xrTableCell5.Text = rm.GetString("labelPERSHKRIMI", ci) + " 2";
            xrTableCell11.Text = rm.GetString("labelNjesiaUpperCase", ci);
            xrTableCell16.Text = rm.GetString("label_SASIA", ci);
            xrTableCell7.Text = rm.GetString("label_CMIMI", ci);
            xrTableCell9.Text = rm.GetString("labelVleraUpperCase", ci);
            xrTableCell10.Text = rm.GetString("labelTVSH", ci);
            xrTableCell8.Text = rm.GetString("labelVLERA_ME_TVSH", ci);

            xrLabel12.Text = rm.GetString("labelShitesiUpperCase", ci) + ":";

            xrLabel22.Text = rm.GetString("labelTotaliUpperCase", ci);
            xrLabel29.Text = rm.GetString("labelTotaliNe", ci);
            xrLabel31.Text = rm.GetString("labelKursiUpperCase", ci);


            xrLabel38.Text = rm.GetString("labelDetyrimParaUpperCase", ci);
            xrLabel40.Text = rm.GetString("labelPageseUpperCase", ci);
            xrLabel42.Text = rm.GetString("labelDetyrimTotalUpperCase", ci);

        }
    }
}
