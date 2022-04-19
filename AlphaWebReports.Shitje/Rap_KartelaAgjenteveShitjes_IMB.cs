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
    public partial class Rap_KartelaAgjenteveShitjes_IMB : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_KartelaAgjenteveShitjes_IMB(){InitializeComponent();} 
      
        public Rap_KartelaAgjenteveShitjes_IMB(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_KartelaAgjenteveShitjes_IMB(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
            parameter6.Value = raport.Parameters[5].Value;
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
            xrLabel3.Text = rm.GetString("labelRaportKlienti", ci);
            xrLabel14.Text = rm.GetString("filterNrDokRezervime", ci);
            xrLabel22.Text = rm.GetString(" labelRaportiDtDok", ci);
            xrLabel4.Text = rm.GetString("labelRaportiArtikull", ci);
            xrLabel5.Text = rm.GetString("cmbItemBlerjeShitjeperqindje", ci);
            xrLabel6.Text = rm.GetString("labelVleraKomisionit", ci);
            xrLabel2.Text = rm.GetString("labelRaportiTotali", ci) + ":";
            xrLabel50.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel22.Text = rm.GetString("labelRaportiDtDok", ci);         
        }

        private void xrLabel15_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("paguar") != null)
            {
                if (GetCurrentColumnValue("paguar").ToString() == "pjeserisht")
                    xrLabel15.BackColor = System.Drawing.Color.Orange;
                else if (GetCurrentColumnValue("paguar").ToString() == "jo")
                    xrLabel15.BackColor = System.Drawing.Color.LightGray;
                else if (GetCurrentColumnValue("paguar").ToString() == "po")
                    xrLabel15.BackColor = System.Drawing.Color.LightGreen;
            }
        }
    }
}
