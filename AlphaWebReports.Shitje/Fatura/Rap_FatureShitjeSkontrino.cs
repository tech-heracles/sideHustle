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
    public partial class Rap_FatureShitjeSkontrino : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureShitjeSkontrino(){InitializeComponent();} 
        public Rap_FatureShitjeSkontrino(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitjeSkontrino(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel44.Text = rm.GetString("filterRaportIdNdermarje", ci);
            xrLabel12.Text = rm.GetString("labelNIPT", ci);
            xrLabel6.Text = rm.GetString("labelRaportNrFature", ci);
            xrLabel8.Text = rm.GetString("labelRaportNrSerial", ci);
            xrTableCell5.Text = rm.GetString("labelRaportiArtikull", ci);
            xrTableCell9.Text = rm.GetString("labelSasia", ci);
            xrTableCell10.Text = rm.GetString("labelCmimi", ci);
            xrTableCell8.Text = rm.GetString("labelVlefta", ci);           
            xrLabel45.Text = rm.GetString("labelTVSH", ci) + ":";
            xrLabel47.Text = rm.GetString("labelVleftameTVSH", ci) + ":";
            xrLabel24.Text = rm.GetString("labelRaportTotaliPaZbritje", ci) + ":";
            xrLabel51.Text = rm.GetString("labelZbritje", ci) + ":";
            xrLabel26.Text = rm.GetString("labelRaportTotaliMeZbritje", ci) + ":";
            xrLabel49.Text = rm.GetString("labelRaportJuFalemnderit", ci);
        }
    }
}
