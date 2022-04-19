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
    public partial class Rap_KartelaFurnitor_Format2 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_KartelaFurnitor_Format2(){InitializeComponent();} 
       
        public Rap_KartelaFurnitor_Format2(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_KartelaFurnitor_Format2(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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


            xrLabel12.Text = rm.GetString("RaportKartelaFurnitoritNeMBTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel6.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell51.Text = rm.GetString("labelRaportMonedha", ci);
            xrTableCell63.Text = rm.GetString("labelNIPT", ci);
            xrTableCell16.Text = rm.GetString("labelNrRendor", ci);
            xrTableCell17.Text = rm.GetString("labelDtRegj", ci);
            xrTableCell19.Text = rm.GetString("labelLlojDokumenti", ci);
            xrTableCell18.Text = rm.GetString("labelRaportiNrDok", ci);
            xrTableCell20.Text = rm.GetString("labelRaportiDtDok", ci);
            xrTableCell21.Text = rm.GetString("labelPershkrimVeprimi", ci);
            xrTableCell22.Text = rm.GetString("labelMonedhaBaze", ci);
            xrTableCell59.Text = rm.GetString("labelRaportMonedheLlogarie", ci);
            xrTableCell43.Text = rm.GetString("labelRaportiDebi", ci);
            xrTableCell42.Text = rm.GetString("labelRaportiKredi", ci);
            xrTableCell30.Text = rm.GetString("labelRaportiProgresivi", ci);
            xrTableCell41.Text = rm.GetString("labelRaportiDebi", ci);
            xrTableCell40.Text = rm.GetString("labelRaportiKredi", ci);
            xrTableCell15.Text = rm.GetString("labelRaportiProgresivi", ci);
            xrTableCell46.Text = rm.GetString("labelRaportiGjendjaNeFillim", ci);
            xrTableCell48.Text = rm.GetString("lblRaportLimitBllokues", ci);
            xrTableCell65.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell67.Text = rm.GetString("labelRaportDebitorKreditor", ci);
        }

     
    }
}
