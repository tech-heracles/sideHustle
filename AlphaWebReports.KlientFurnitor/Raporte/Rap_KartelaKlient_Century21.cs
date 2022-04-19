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
    public partial class Rap_KartelaKlient_Century21 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_KartelaKlient_Century21(){InitializeComponent();} 

        public Rap_KartelaKlient_Century21(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_KartelaKlient_Century21(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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


            xrLabel12.Text = rm.GetString("lblRaportKartelaKlientit", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel6.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell16.Text = rm.GetString("labelRaportKlienti", ci) + ":";
            xrTableCell20.Text = rm.GetString("filterRaportiNumerLlogarie", ci);
            xrTableCell18.Text = rm.GetString("labelRaportMonedha", ci);
            xrTableCell22.Text = rm.GetString("labelNIPT", ci);
            xrTableCell31.Text = rm.GetString("labelRaportiNr", ci);

            xrTableCell25.Text = rm.GetString("labelLlojDokumenti", ci);
            xrTableCell26.Text = rm.GetString("labelRaportiNrDok", ci);
            xrTableCell27.Text = rm.GetString("labelRaportiDtDok", ci);
            xrTableCell28.Text = rm.GetString("labelPershkrimVeprimi", ci);
            xrTableCell30.Text = rm.GetString("labelRaportMonedheLlogarie", ci);
            xrTableCell37.Text = rm.GetString("labelRaportiFaturuar", ci);
            xrTableCell38.Text = rm.GetString("lblRaportiArketuar", ci);
            xrTableCell39.Text = rm.GetString("labelRaportiProgresivi", ci);
            xrTableCell45.Text = rm.GetString("labelRaportiGjendjaNeFillim", ci);
            xrTableCell49.Text = rm.GetString("labelRaportiTotali", ci);
        }

     
    }
}
