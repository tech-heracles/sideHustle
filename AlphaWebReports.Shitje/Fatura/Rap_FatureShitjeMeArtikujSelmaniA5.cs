using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureShitjeMeArtikujSelmaniA5 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureShitjeMeArtikujSelmaniA5(){InitializeComponent();} 

        public Rap_FatureShitjeMeArtikujSelmaniA5(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitjeMeArtikujSelmaniA5(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            ResourceManager rm = new ResourceManager("Resources.Strings",System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel7.Text = rm.GetString("labelRaportDetyrimMeparshem", ci);
            xrLabel13.Text = rm.GetString("labelRaportAdresa", ci) + ":";
            xrLabel35.Text = rm.GetString("lblBleresi", ci).ToUpper();
            xrTableCell4.Text = rm.GetString("labelRaportiNr", ci).ToUpper();
            xrTableCell5.Text = rm.GetString("labelRaportiPershkrimi", ci).ToUpper();
            xrTableCell7.Text = rm.GetString("labelNjesia", ci).ToUpper();
            xrTableCell9.Text = rm.GetString("labelSasia", ci).ToUpper();
            xrTableCell10.Text = rm.GetString("labelCmimi", ci).ToUpper();
            xrTableCell6.Text = rm.GetString("labelVlefta", ci).ToUpper();
            xrLabel44.Text = rm.GetString("lblBleresi", ci).ToUpper();
            xrLabel45.Text = rm.GetString("lblShitesKrijues", ci).ToUpper();
        }

    }
}
