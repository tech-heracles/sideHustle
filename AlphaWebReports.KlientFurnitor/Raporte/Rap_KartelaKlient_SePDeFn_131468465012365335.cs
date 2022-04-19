using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.KlientFurnitor.Raporte
{
    public partial class Rap_KartelaKlient_SePDeFn_131468465012365335 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_KartelaKlient_SePDeFn_131468465012365335()
        {
            InitializeComponent();
        }
        public Rap_KartelaKlient_SePDeFn_131468465012365335(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
         this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_KartelaKlient_SePDeFn_131468465012365335(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));


            xrLabel12.Text = rm.GetString("RaportKartelaKlientitNeMBTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel6.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell13.Text = rm.GetString("filterRaportiNumerLlogarie", ci);
            xrTableCell15.Text = rm.GetString("labelRaportMonedha", ci);
            xrTableCell17.Text = rm.GetString("labelNIPT", ci);
            xrTableCell10.Text = rm.GetString("labelNrRendor", ci);
            xrTableCell25.Text = rm.GetString("labelDtRegj", ci);
            xrTableCell12.Text = rm.GetString("labelLlojDokumenti", ci);
            xrTableCell19.Text = rm.GetString("labelRaportiNrDok", ci);
            xrTableCell20.Text = rm.GetString("labelRaportiDtDok", ci);
            xrTableCell21.Text = rm.GetString("labelPershkrimVeprimi", ci);
            xrTableCell22.Text = rm.GetString("labelMonedhaBaze", ci);
            xrTableCell23.Text = rm.GetString("labelRaportMonedheLlogarie", ci);
            xrTableCell45.Text = rm.GetString("labelRaportiDebi", ci);
            xrTableCell44.Text = rm.GetString("labelRaportiKredi", ci);
            xrTableCell30.Text = rm.GetString("labelRaportiProgresivi", ci);
            xrTableCell41.Text = rm.GetString("labelRaportiDebi", ci);
            xrTableCell40.Text = rm.GetString("labelRaportiKredi", ci);
            xrTableCell31.Text = rm.GetString("labelRaportiProgresivi", ci);
            xrTableCell37.Text = rm.GetString("labelRaportiGjendjaNeFillim", ci);
            xrTableCell42.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell50.Text = rm.GetString("labelRaportDebitorKreditor", ci);
            xrTableCell8.Text = rm.GetString("lblRaportLimitBllokues", ci);


        }

    }
}
