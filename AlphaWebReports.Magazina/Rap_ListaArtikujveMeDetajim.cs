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
    public partial class Rap_ListaArtikujveMeDetajim : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ListaArtikujveMeDetajim(){InitializeComponent();} 
      
        public Rap_ListaArtikujveMeDetajim(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.IdGjuha, report)
        {

        }
        public Rap_ListaArtikujveMeDetajim(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, int idGjuha, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
            parameter6.Value = raport.Parameters[5].Value;
            parameter7.Value = raport.Parameters[6].Value;
            parameter8.Value = raport.Parameters[7].Value;
            parameter9.Value = raport.Parameters[8].Value;
            parameter10.Value = raport.Parameters[9].Value;
            parameterIdNderm.Value = idNdermarrje;

        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportListaArtikujveDetajimTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel13.Text = rm.GetString("labelKartela", ci);
            xrLabel42.Text = rm.GetString("labelFilterAvancuarKodbari", ci);
            xrLabel21.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel20.Text = rm.GetString("labelNjesia", ci);
            xrLabel155.Text = rm.GetString("labelRaportLLogariInventar", ci);
            xrLabel19.Text = rm.GetString("filterGrupimP", ci);
            xrLabel18.Text = rm.GetString("filterLlojiDetajimit", ci);
            xrLabel17.Text = rm.GetString("labelRaportKodiArtikullitTeFurnitori", ci);
            xrLabel16.Text = rm.GetString("labelRaportEmertimiFurnitorit", ci);
            xrLabel37.Text = rm.GetString("labelLogoIMB", ci);
            
        }
       
    }


}
