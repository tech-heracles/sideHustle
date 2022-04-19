using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Blerje.Format_Printimi
{
    public partial class Rap_FatureBlerje_Fear : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureBlerje_Fear(){InitializeComponent();}     
        public Rap_FatureBlerje_Fear(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureBlerje_Fear(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel1.Text = rm.GetString("RaportFatureBlerjeTitulli", ci);

            xrLabel2.Text = rm.GetString("labelRaportSubjektBleres", ci) ;
            xrLabel4.Text = rm.GetString("labelRaportSubjektShites", ci) ;
            xrLabel12.Text = rm.GetString("labelNIPT", ci);
            xrLabel6.Text = rm.GetString("labelRaportNumriFatures", ci);
            xrLabel13.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel7.Text = rm.GetString("labelRaportDataFatures", ci);
            xrLabel14.Text = rm.GetString("labelRaportTel", ci);
            xrLabel8.Text = rm.GetString("labelFilterAvancuarNrSerial", ci);
            xrLabel44.Text = rm.GetString("labelFilterAvancuarMagazina", ci);
            xrLabel47.Text = rm.GetString("labelNIPT", ci);
            xrLabel46.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel48.Text = rm.GetString("labelRaportTel", ci);
            xrTableCell4.Text = rm.GetString("labelRaportNRKARTELE", ci);
            xrTableCell5.Text = rm.GetString("labelPERSHKRIMI", ci);
            xrTableCell7.Text = rm.GetString("labelNjesiaUpperCase", ci);
            xrTableCell9.Text = rm.GetString("label_SASIA", ci);
            xrTableCell10.Text = rm.GetString("label_CMIMI", ci);
            xrTableCell8.Text = rm.GetString("labelVlera_Pa_Tvsh", ci);
            xrTableCell25.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel15.Text = rm.GetString("lblRaportTotaliNe", ci);
            xrTableCell37.Text = rm.GetString("labelKursi", ci);
            xrLabel52.Text = rm.GetString("lblEmriBleresitDheFirma", ci);
            xrLabel53.Text = rm.GetString("lblShitesKrijues", ci);
            xrTableCell18.Text = rm.GetString("labelVLERA_ME_TVSH", ci);
        }      
    }
}
