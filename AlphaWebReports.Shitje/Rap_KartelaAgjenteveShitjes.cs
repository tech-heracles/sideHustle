using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_KartelaAgjenteveShitjes : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_KartelaAgjenteveShitjes(){InitializeComponent();} 
     
        public Rap_KartelaAgjenteveShitjes(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_KartelaAgjenteveShitjes(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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

            xrLabel17.Text = rm.GetString("RaportKartelaEAgjenteveTeShitjesTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel1.Text = rm.GetString("labelAgjenti", ci) + ":";
            xrLabel24.Text = rm.GetString("labelNrRendor", ci);
            xrLabel3.Text = rm.GetString("labelDtRegjistrimi", ci);
            xrLabel23.Text = rm.GetString("labelLlojDokumenti", ci);
            xrLabel14.Text = rm.GetString("filterNrDokRezervime", ci);
            xrLabel22.Text = rm.GetString(" labelRaportiDtDok", ci);
            xrLabel4.Text = rm.GetString("labelVleraPaTVSH", ci);
            xrLabel5.Text = rm.GetString("cmbItemBlerjeShitjeperqindje", ci);
            xrLabel6.Text = rm.GetString("labelVleraKomisionit", ci);
            xrLabel2.Text = rm.GetString("labelRaportiTotali", ci) + ":";
            xrLabel50.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel22.Text = rm.GetString("labelRaportiDtDok", ci);
        } 
    }
}
