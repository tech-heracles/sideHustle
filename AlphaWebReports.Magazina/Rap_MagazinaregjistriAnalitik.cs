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
    public partial class Rap_MagazinaregjistriAnalitik : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_MagazinaregjistriAnalitik(){InitializeComponent();} 
        public Rap_MagazinaregjistriAnalitik(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_MagazinaregjistriAnalitik(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {   InitializeComponent();
        EmrateLabelave(ci);
        }


        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
          ResourceManager  rm = new ResourceManager("Resources.Strings",
                     System.Reflection.Assembly.Load("App_GlobalResources"));

          xrLabel17.Text = rm.GetString("RaportRegjistriAnalitikMagazinesTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel23.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel25.Text = rm.GetString("labelRaportVleftaMagazines", ci);
            xrLabel20.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
           xrLabel36.Text = rm.GetString("labelLogoIMB", ci);
           xrLabel28.Text = rm.GetString("labelRaportMagazina", ci);
           xrLabel31.Text = rm.GetString("lblRaportTotali", ci);
            xrTableCell8.Text = rm.GetString("labelSasiHyrje", ci);
           xrLabel82.Text = rm.GetString("labelRaportSasiaDalje", ci);
           xrLabel78.Text = rm.GetString("labelFilterAvancuarLlojDok", ci);
           xrLabel76.Text = rm.GetString("labelRaportiPershkrimi", ci);
           xrLabel69.Text = rm.GetString("labelFilterAvancuarNrDok", ci);
           xrLabel73.Text = rm.GetString("labelFilterKryesorDtDokumenti", ci);
           xrLabel72.Text = rm.GetString("labelFilterAvancuarDtRegjistrimi", ci);
           xrLabel71.Text = rm.GetString("labelKartela", ci);
           xrLabel77.Text = rm.GetString("labelNjesia", ci);
           xrLabel74.Text = rm.GetString("labelSasia", ci);
           xrLabel70.Text = rm.GetString("labelVlefta", ci);
           xrLabel75.Text = rm.GetString("labelCmimi", ci);
            
        }
     
    }
}
