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
    public partial class Rap_FleteInventarizimiSipasSerialeveArtikujveAfatgjate : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FleteInventarizimiSipasSerialeveArtikujveAfatgjate(){InitializeComponent();} 
    
       
        public Rap_FleteInventarizimiSipasSerialeveArtikujveAfatgjate(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.IdRaporti, param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.IdGjuha, report)
        {

        }
        public Rap_FleteInventarizimiSipasSerialeveArtikujveAfatgjate(int idRaporti, CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, int idGjuha, DevExpress.XtraReports.UI.XtraReport raport)
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
            xrLabel12.Text = rm.GetString("RaportFleteInventarizimiSipasSerialeveArtikujAfatgjateTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell20.Text = rm.GetString("labelNr2", ci);
            xrTableCell21.Text = rm.GetString("labelKodi2", ci);
            xrTableCell22.Text = rm.GetString("labelRaportSeriali", ci);
            xrTableCell23.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell24.Text = rm.GetString("lblNjesia", ci);
            xrTableCell72.Text = rm.GetString("lblRaportMagazina", ci);
            xrTableCell77.Text = rm.GetString("labelVendndodhja", ci);
            xrTableCell76.Text = rm.GetString("lblRaportVitiBlerjes", ci);
            xrTableCell75.Text = rm.GetString("labelPersoniPergjegjes", ci);
            xrTableCell74.Text = rm.GetString("labelCmimi2", ci);

            xrTableCell73.Text = rm.GetString("labelRaportTedhenatKontabilitetit", ci);
            xrTableCell93.Text = rm.GetString("labelSasia", ci);
            xrTableCell94.Text = rm.GetString("labelKategoria", ci);
            xrTableCell95.Text = rm.GetString("labelKategoria1", ci);
            xrTableCell57.Text = rm.GetString("labelKategoria2", ci);
            xrTableCell36.Text = rm.GetString("lblRaportVleftaLeke", ci);

            xrTableCell27.Text = rm.GetString("lblRaportTedhenaInventar", ci);
            xrTableCell37.Text = rm.GetString("labelSasia", ci);
            xrTableCell48.Text = rm.GetString("labelKategoria", ci);
            xrTableCell38.Text = rm.GetString("lblRaportVleftaLeke", ci);
            xrTableCell65.Text = rm.GetString("labelKategoria1", ci);
            xrTableCell59.Text = rm.GetString("labelKategoria2", ci);

            xrTableCell29.Text = rm.GetString("lblRaportRezultatet", ci);
            xrTableCell39.Text = rm.GetString("lblMungesat", ci);
            xrTableCell40.Text = rm.GetString("lblRaportTepricat", ci);
            xrTableCell50.Text = rm.GetString("labelSasia", ci);
            xrTableCell51.Text = rm.GetString("labelSasia", ci);
            xrTableCell30.Text = rm.GetString("labelVerejtjeShenime", ci);

            xrLabel37.Text = rm.GetString("labelLogoIMB", ci);
            //xrLabel155.Text = rm.GetString("labelRaportGjendjaeMeparshme", ci);
        }
        
    }


}
