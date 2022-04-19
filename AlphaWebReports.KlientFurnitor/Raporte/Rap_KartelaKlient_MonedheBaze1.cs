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
    public partial class Rap_KartelaKlient_MonedheBaze1 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_KartelaKlient_MonedheBaze1(){InitializeComponent();} 

        public Rap_KartelaKlient_MonedheBaze1(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_KartelaKlient_MonedheBaze1(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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
            xrTableCell10.Text = rm.GetString("filterRaportiNumerLlogarie", ci);
            xrTableCell12.Text = rm.GetString("labelRaportMonedha", ci);
            xrTableCell14.Text = rm.GetString("labelNIPT", ci);
            xrLabel10.Text = rm.GetString("labelNrRendor", ci);
            xrLabel14.Text = rm.GetString("labelDtRegj", ci);
            xrLabel15.Text = rm.GetString("labelLlojDokumenti", ci);
            xrLabel17.Text = rm.GetString("labelRaportiNrDok", ci);
            xrLabel18.Text = rm.GetString("labelRaportiDtDok", ci);
            xrLabel19.Text = rm.GetString("labelPershkrimVeprimi", ci);
            xrLabel26.Text = rm.GetString("labelMonedhaBaze", ci);
            xrLabel27.Text = rm.GetString("labelRaportMonedheLlogarie", ci);
            xrLabel20.Text = rm.GetString("labelRaportiDebi", ci);
            xrLabel21.Text = rm.GetString("labelRaportiKredi", ci);
            xrLabel22.Text = rm.GetString("labelRaportiProgresivi", ci);
            xrLabel23.Text = rm.GetString("labelRaportiDebi", ci);
            xrLabel24.Text = rm.GetString("labelRaportiKredi", ci);
            xrLabel25.Text = rm.GetString("labelRaportiProgresivi", ci);
            xrLabel28.Text = rm.GetString("labelRaportiGjendjaNeFillim", ci);
            xrTableCell16.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell50.Text = rm.GetString("labelRaportDebitorKreditor", ci);
            xrTableCell8.Text = rm.GetString("lblRaportLimitBllokues", ci);
            xrLabel84.Text = rm.GetString("labelRaportLlogariBankare", ci);
            xrLabel86.Text = rm.GetString("labelFilterAvancuarArkaBanka", ci);

        }

       
    }
}
