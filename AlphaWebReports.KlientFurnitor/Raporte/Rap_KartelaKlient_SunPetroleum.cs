using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.KlientFurnitor.Raporte
{
    public partial class Rap_KartelaKlient_SunPetroleum : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_KartelaKlient_SunPetroleum(){InitializeComponent();} 
  

        public Rap_KartelaKlient_SunPetroleum(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_KartelaKlient_SunPetroleum(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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


            xrLabel12.Text = rm.GetString("RaportKartelaKlientitNeMBTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel6.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell7.Text = rm.GetString("labelRaportKlienti", ci) + ":";
            xrTableCell9.Text = rm.GetString("filterRaportiNumerLlogarie", ci);
            xrTableCell14.Text = rm.GetString("labelRaportMonedha", ci);
            xrTableCell16.Text = rm.GetString("labelNIPT", ci);
            xrTableCell10.Text = rm.GetString("labelNrRendor", ci);
            xrTableCell19.Text = rm.GetString("MenuItemArkaBanka", ci);
            xrTableCell11.Text = rm.GetString("labelLlojDokumenti", ci);
            xrTableCell20.Text = rm.GetString("labelRaportiNrDok", ci);
            xrTableCell12.Text = rm.GetString("labelRaportiDtDok", ci);
            xrTableCell21.Text = rm.GetString("labelPershkrimVeprimi", ci);
            xrTableCell23.Text = rm.GetString("labelMonedhaBaze", ci);
            xrTableCell25.Text = rm.GetString("labelRaportMonedheLlogarie", ci);
            xrTableCell46.Text = rm.GetString("labelRaportiDebi", ci);
            xrTableCell47.Text = rm.GetString("labelRaportiKredi", ci);
            xrTableCell33.Text = rm.GetString("labelRaportiProgresivi", ci);
            xrTableCell49.Text = rm.GetString("labelRaportiDebi", ci);
            xrTableCell48.Text = rm.GetString("labelRaportiKredi", ci);
            xrTableCell34.Text = rm.GetString("labelRaportiProgresivi", ci);
            xrTableCell41.Text = rm.GetString("labelRaportiGjendjaNeFillim", ci);
            xrTableCell50.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell59.Text = rm.GetString("labelRaportDebitorKreditor", ci);
            xrTableCell18.Text = rm.GetString("filterDegeAdm", ci);


        }
    }
}
