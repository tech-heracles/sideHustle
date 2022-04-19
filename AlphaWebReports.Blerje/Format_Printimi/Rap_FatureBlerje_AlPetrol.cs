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
    public partial class Rap_FatureBlerje_AlPetrol : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureBlerje_AlPetrol(){InitializeComponent();} 
        public Rap_FatureBlerje_AlPetrol(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureBlerje_AlPetrol(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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

            xrLabel2.Text = rm.GetString("labelRaportSubjektBleres", ci) + ":";
            xrLabel4.Text = rm.GetString("labelRaportSubjektShites", ci) + ":";
            xrLabel12.Text = rm.GetString("labelNIPT", ci);
            xrLabel6.Text = rm.GetString("labelRaportNumriFatures", ci);
            xrLabel13.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel7.Text = rm.GetString("labelRaportDataFatures", ci);
            xrLabel14.Text = rm.GetString("labelRaportTel", ci);
            xrLabel49.Text = rm.GetString("labelAdministrimiFax", ci);
            xrLabel8.Text = rm.GetString("labelRaportNrSerial", ci);
            xrLabel44.Text = rm.GetString("labelFilterAvancuarMagazina", ci);
            xrLabel47.Text = rm.GetString("labelNIPT", ci);
            xrLabel46.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel48.Text = rm.GetString("labelRaportTel", ci);
            xrTableCell4.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell5.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell7.Text = rm.GetString("labelNjesia", ci);
            xrTableCell9.Text = rm.GetString("labelSasia", ci);
            xrTableCell10.Text = rm.GetString("lblCmimblerje", ci);
            xrTableCell8.Text = rm.GetString("labelVleraPaTVSH", ci);
            xrTableCell11.Text = rm.GetString("labelTVSH", ci);
            xrTableCell6.Text = rm.GetString("labelVleftameTVSH", ci);
            xrTableCell11.Text = rm.GetString("lblcmimShitje", ci);
            xrTableCell6.Text = rm.GetString("lblVLSH", ci);
            xrTableCell25.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel15.Text = rm.GetString("lblRaportTotaliNe", ci);
            xrTableCell37.Text = rm.GetString("labelKursi", ci);
            xrLabel52.Text = rm.GetString("lblEmriBleresitDheFirma", ci);
            xrLabel53.Text = rm.GetString("lblShitesKrijues", ci);
            xrLabel23.Text = rm.GetString("filterPerdoruesi", ci);
        }

    }
}
