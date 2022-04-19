using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Collections.Generic;
namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_KonsumiPerberesveSipasArtikujveTePerbere : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_KonsumiPerberesveSipasArtikujveTePerbere(){InitializeComponent();} 
        public Rap_KonsumiPerberesveSipasArtikujveTePerbere(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.IdGjuha, report)
        {

        }
        public Rap_KonsumiPerberesveSipasArtikujveTePerbere(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, int idGjuha, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
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
            xrLabel12.Text = rm.GetString("RaportKonsumiPerberesveSipasArtikujveTePerbere", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel13.Text = rm.GetString("labelRaportiNr", ci);
            xrLabel21.Text = rm.GetString("labelRaportiPerberes", ci);
            xrLabel30.Text = rm.GetString("labelKodi", ci);
            xrTableCell6.Text = rm.GetString("labelRaportEmertimi", ci);
            xrTableCell5.Text = rm.GetString("labelNjesia", ci);
            xrLabel16.Text = rm.GetString("labelRaportKonsumi", ci);
            xrLabel29.Text = rm.GetString("labelRaportSasia", ci);
            xrLabel24.Text = rm.GetString("labelVlefta", ci);
            xrLabel14.Text = rm.GetString("labelRaportProdukti", ci);
            xrLabel23.Text = rm.GetString("labelKodi", ci);
            xrLabel156.Text = rm.GetString("labelRaportEmertimi", ci);
            xrLabel37.Text = rm.GetString("labelLogoIMB", ci);
        }
        
    }


}
