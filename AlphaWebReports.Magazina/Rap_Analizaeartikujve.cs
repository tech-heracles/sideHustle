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
    public partial class Rap_Analizaeartikujve : DevExpress.XtraReports.UI.XtraReport
    {           
		public Rap_Analizaeartikujve(){InitializeComponent();} 
        string windowWidth = "";       
        public Rap_Analizaeartikujve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.IdRaporti, param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.IdGjuha, report)
        {
        }
        public Rap_Analizaeartikujve(int idRaporti,CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, int idGjuha, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters["IdNdermarje"].Value;
            parameter2.Value = raport.Parameters["filterDtDok"].Value;
            parameter3.Value = raport.Parameters["filterDtRegj"].Value;
            parameter4.Value = raport.Parameters["filterKartela"].Value;
            parameter5.Value = raport.Parameters["filterFurnitorArt"].Value;
            parameter7.Value = raport.Parameters["filterkodifikimartP"].Value;
            parameter6.Value = raport.Parameters["filterkodifikimartD"].Value;
            parameter9.Value = raport.Parameters[1].Value;
            parameter10.Value = raport.Parameters["filterKodbari"].Value;
            parameter11.Value = raport.Parameters["filterMagazina"].Value;
            windowWidth = Convert.ToString(raport.Parameters[6].Value);
          
        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel17.Text = rm.GetString("RaportAnalizaArtikujveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel1.Text = rm.GetString("labelKartela", ci);
            xrLabel26.Text = rm.GetString("labelFilterAvancuarKodbari", ci);
            xrLabel2.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel3.Text = rm.GetString("labelNjesia", ci);
            xrLabel4.Text = rm.GetString("labelRaportGjendjaeMbartur", ci);
            xrLabel5.Text = rm.GetString("labelBlerje", ci);
            xrLabel6.Text = rm.GetString("labelShitje", ci);
            xrLabel7.Text = rm.GetString("labelRaportGjendje", ci);
            xrLabel8.Text = rm.GetString("labelRaportKosto", ci);
            xrLabel9.Text = rm.GetString("labelVlefta", ci);
            xrLabel32.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel26.Text = rm.GetString("labelRaportHyrje", ci);
            xrLabel36.Text = rm.GetString("labelRaportiNgamag", ci);
            xrLabel37.Text = rm.GetString("labelTetjera", ci);
            xrLabel5.Text = rm.GetString("labelRaportDalje", ci);
            xrLabel6.Text = rm.GetString("labelRaportiPershitje", ci);
            xrLabel38.Text = rm.GetString("labelRaportiPermag", ci);
            xrLabel39.Text = rm.GetString("labelTetjera", ci);

        }
    }
}
