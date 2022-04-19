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
    public partial class Rap_Kartela_E_Furnitorit : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Kartela_E_Furnitorit(){InitializeComponent();} 
        public Rap_Kartela_E_Furnitorit(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }

        public Rap_Kartela_E_Furnitorit(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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


            xrLabel12.Text = rm.GetString("TitullRaportKartelaFurnitorit", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel6.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell47.Text = rm.GetString("labelFurnitori", ci) + ":";
            xrTableCell60.Text = rm.GetString("filterRaportiNumerLlogarie", ci);
            xrTableCell50.Text = rm.GetString("labelRaportMonedha", ci);
            xrTableCell62.Text = rm.GetString("labelNIPT", ci);
            xrTableCell16.Text = rm.GetString("labelNrRendor", ci);
            xrTableCell17.Text = rm.GetString("labelDtRegj", ci);
            xrTableCell19.Text = rm.GetString("labelLlojDokumenti", ci);
            xrTableCell18.Text = rm.GetString("labelRaportiNrDok", ci);
            xrTableCell13.Text = rm.GetString("labelRaportiDtDok", ci);
            xrTableCell20.Text = rm.GetString("labelPershkrimVeprimi", ci);
            xrTableCell59.Text = rm.GetString("labelRaportMonedheLlogarie", ci);
            xrTableCell41.Text = rm.GetString("labelRaportiDebi", ci);
            xrTableCell40.Text = rm.GetString("labelRaportiKredi", ci);
            xrTableCell36.Text = rm.GetString("labelRaportiProgresivi", ci);
            xrTableCell46.Text = rm.GetString("labelRaportiGjendjaNeFillim", ci);
            xrTableCell7.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell15.Text = rm.GetString("labelRaportDebitorKreditor", ci);
        }
    }
}
