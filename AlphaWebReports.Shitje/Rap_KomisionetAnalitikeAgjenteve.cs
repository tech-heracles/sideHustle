using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;


namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_KomisionetAnalitikeAgjenteve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_KomisionetAnalitikeAgjenteve(){InitializeComponent();} 
    
        String agjenti = "";
        public Rap_KomisionetAnalitikeAgjenteve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_KomisionetAnalitikeAgjenteve(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {

            ResourceManager rm = new ResourceManager("Resources.Strings",
                    System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value; 
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
            parameter6.Value = raport.Parameters[5].Value;
            if (raport.Parameters[4].Value.ToString() != "")
            {
                agjenti = raport.Parameters[4].Value.ToString();
            }     
        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {

            ResourceManager rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel17.Text = rm.GetString("RaportKomisionetAnalitikeSipasAgjenteveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel24.Text = rm.GetString("labelRaportiNr", ci);
            xrLabel2.Text = rm.GetString("labelRaportData", ci);
            xrLabel4.Text = rm.GetString("labelKartela", ci);
            xrLabel5.Text = rm.GetString("labelRaportKlienti", ci);
            xrLabel6.Text = rm.GetString("labelRaportiPaguar", ci);
            xrLabel7.Text = rm.GetString("labelVleratTotalePaTvsh", ci);
            xrLabel8.Text = "% " + rm.GetString("labelPerqindjaEkompanise", ci);
            xrLabel9.Text = rm.GetString("labelAgjent", ci) + " 1";
            xrLabel10.Text = "% \n" + rm.GetString("labelAgjent", ci) + " 1";
            xrLabel11.Text = rm.GetString("labelAgjent", ci) + " 2";
         //   xrLabel12.Text = "% \n" + rm.GetString("labelAgjent", ci) + " 2";
            xrLabel13.Text = rm.GetString("labelAgjent", ci) + " 3";
            xrLabel14.Text = "% \n" + rm.GetString("labelAgjent", ci) + " 3";
            xrLabel28.Text = rm.GetString("labelTotali", ci);
            xrLabel34.Text = rm.GetString("labelTotali_i_agjentit", ci);
            xrLabel38.Text = rm.GetString("labelRaportiPershkrimiFatures", ci);
            xrLabel39.Text = rm.GetString("labelRaportiShenime", ci);
        }

        private void xrLabel16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
    }
}
