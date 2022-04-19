using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureShitjeMeArtikuj_Anoria : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureShitjeMeArtikuj_Anoria(){InitializeComponent();} 
        private CultureInfo ci;
        public Rap_FatureShitjeMeArtikuj_Anoria(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitjeMeArtikuj_Anoria(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
           // xrLabel11.Text = rm.GetString("labelNIPT", ci);
            xrLabel6.Text = rm.GetString("labelRaportNumer", ci) + ":";
            xrLabel7.Text = rm.GetString("labelRaportData", ci);
            xrLabel14.Text = rm.GetString("labelRaportTel", ci);
            xrLabel8.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel15.Text = rm.GetString("labelRaportLlogariBankare", ci);
            xrTableCell4.Text = rm.GetString("labelKodi", ci);
            xrTableCell5.Text = rm.GetString("labelRaportiPershkrimi", ci);
            //xrTableCell7.Text = rm.GetString("labelNjesia", ci);
            //xrTableCell11.Text = rm.GetString("labelTVSH", ci);
            xrLabel2.Text = rm.GetString("lblRaportMagazina", ci) + ":";
            xrLabel12.Text = rm.GetString("lblRaportFatureAnoriaTelCel", ci);
        }

    }
}
