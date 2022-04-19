using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Blerje.Format_Printimi
         
{
    public partial class Rap_FatureBlerjeDast : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureBlerjeDast(){InitializeComponent();} 
        public Rap_FatureBlerjeDast(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureBlerjeDast(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel6.Text = rm.GetString("labelRaportNumriFatures", ci) + ":";
            xrLabel8.Text = rm.GetString("labelNumriSerise", ci) + ":";
            xrLabel7.Text = rm.GetString("labelRaportDateFature", ci);
            xrLabel41.Text = rm.GetString("labelNumriPorosise", ci);
            xrLabel4.Text = rm.GetString("labelBleresiUpperCase", ci) + ":";
            xrLabel2.Text = rm.GetString("labelShitesiUpperCase", ci) + ":";
            xrLabel43.Text = rm.GetString("labelRaportTelFax", ci);
            xrLabel50.Text = rm.GetString("labelRaportTelFax", ci);
            xrLabel12.Text = rm.GetString("labelNIPT", ci) + ":";
            xrLabel44.Text = rm.GetString("labelNIPT", ci) + ":";

            xrTableCell17.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell4.Text = rm.GetString("labelKartela", ci);
            xrTableCell18.Text = rm.GetString("labelBarkodi", ci);
            xrTableCell5.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell7.Text = rm.GetString("labelNjesia", ci);
            xrTableCell9.Text = rm.GetString("labelSasia", ci);
            xrTableCell10.Text = rm.GetString("labelRaportCmimiPaTVSH", ci);
            xrTableCell8.Text = rm.GetString("labelVleftapaTVSH", ci);
            xrTableCell11.Text = rm.GetString("labelTVSH", ci);
            xrTableCell6.Text = rm.GetString("labelVleftameTVSH", ci);

            xrLabel14.Text = rm.GetString("labelFilterAvancuarMonedha", ci);

            xrLabel24.Text = rm.GetString("labelTotaliUpperCase", ci);
            xrLabel25.Text = rm.GetString("labelKursi", ci) + ":";
            xrLabel19.Text = rm.GetString("labelTotaliLekUpperCase", ci);

            xrLabel37.Text = rm.GetString("lblShitesKrijues", ci) + ":";
            xrLabel38.Text = rm.GetString("labelBleresi", ci) + ":";

            xrLabel39.Text = rm.GetString("labelShenim", ci) + ":";
            xrLabel40.Text = rm.GetString("labelDokumentiNukPerbenFatureTatimore", ci) + ":";
            xrLabel52.Text = rm.GetString("lblRaportKontrollori", ci) + ":";
          }
    }
}
