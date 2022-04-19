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
    public partial class Rap_MagazinaregjistriAnalitikSipasAutorizimeve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_MagazinaregjistriAnalitikSipasAutorizimeve(){InitializeComponent();} 
        private double shumagrup1;
        private double shumagrup2;
        private double shumatotale;
        private bool enabled;

        public Rap_MagazinaregjistriAnalitikSipasAutorizimeve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_MagazinaregjistriAnalitikSipasAutorizimeve(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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
          ResourceManager  rm = new ResourceManager("Resources.Strings",
                     System.Reflection.Assembly.Load("App_GlobalResources"));

          xrLabel17.Text = rm.GetString("RaportRegjistriAnalitikMagazinesTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell7.Text = rm.GetString("labelFilterAvancuarLlojDok", ci);
            xrTableCell12.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell8.Text = rm.GetString("labelFilterAvancuarNrDok", ci);
            xrTableCell10.Text = rm.GetString("labelFilterKryesorDtDokumenti", ci);
            xrTableCell9.Text = rm.GetString("labelFilterAvancuarDtRegjistrimi", ci);
            xrTableCell11.Text = rm.GetString("labelKartela", ci);
            xrTableCell14.Text = rm.GetString("labelNjesia", ci);
            xrTableCell13.Text = rm.GetString("labelSasia", ci);
            xrTableCell16.Text = rm.GetString("labelVlefta", ci);
            xrTableCell15.Text = rm.GetString("labelCmimi", ci);
            xrLabel23.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel25.Text = rm.GetString("labelRaportTotaliMagazines", ci);
            xrLabel20.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
           xrLabel36.Text = rm.GetString("labelLogoIMB", ci);
           xrLabel28.Text = rm.GetString("labelRaportMagazina", ci);
            
        }
        
    }
}
