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
    public partial class Rap_GjendjaArtPerberes : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_GjendjaArtPerberes(){InitializeComponent();} 
        string windowWidth = "";
        public Rap_GjendjaArtPerberes(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.IdGjuha, report)
        {

        }
        public Rap_GjendjaArtPerberes(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, int idGjuha, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[1].Value;
            parameter2.Value = raport.Parameters[2].Value;
            parameter3.Value = raport.Parameters[3].Value;
            parameter4.Value = raport.Parameters[4].Value;
            parameter5.Value = raport.Parameters[5].Value;
            parameter6.Value = raport.Parameters[8].Value;
            parameter7.Value = raport.Parameters[6].Value;
            parameter8.Value = raport.Parameters[9].Value;
            parameter11.Value = raport.Parameters[10].Value;
            windowWidth = Convert.ToString(raport.Parameters[9].Value);         
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("lblTitullRaportGjendjaArtPerbere", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel13.Text = rm.GetString("labelKartela", ci);
            xrLabel21.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel20.Text = rm.GetString("labelNjesia", ci);
            xrLabel14.Text = rm.GetString("labelVlefta", ci);
            xrLabel16.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
            xrLabel15.Text = rm.GetString("labelRaportKosto", ci);
            TotaliGjithMAgazinave.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel31.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel34.Text = rm.GetString("filterMagazina", ci);
            xrLabel37.Text = rm.GetString("labelLogoIMB", ci);
        }
        
    }


}
