using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_ArtikujGjendjeNegative : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ArtikujGjendjeNegative(){InitializeComponent();} 

        public Rap_ArtikujGjendjeNegative(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_ArtikujGjendjeNegative(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            Ndermarja.Value = raport.Parameters[0].Value;
            DtDok.Value = raport.Parameters[1].Value;
            Kartela.Value = raport.Parameters[2].Value;
            Magazina.Value = raport.Parameters[3].Value;
            Kodbari.Value = raport.Parameters[4].Value;
            parameter1.Value = raport.Parameters[5].Value;
            DegaAdministrative.Value = raport.Parameters[6].Value;
            
        }

       
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel17.Text = rm.GetString("RaportArtikujMeGjendjeNegativeTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel18.Text = rm.GetString("labelKartela", ci);
             xrLabel5.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell9.Text = rm.GetString("labelNjesia", ci);
            xrTableCell10.Text = rm.GetString("labelFilterAvancuarMagazina", ci);
            xrTableCell12.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
            xrTableCell11.Text = rm.GetString("labelRaportData", ci);
            xrLabel69.Text = rm.GetString("labelLogoIMB", ci);
        }
        
    }
}
