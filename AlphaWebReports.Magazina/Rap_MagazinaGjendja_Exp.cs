using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Collections.Generic;
namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_MagazinaGjendja_Exp : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_MagazinaGjendja_Exp(){InitializeComponent();} 

        public Rap_MagazinaGjendja_Exp(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, report)
        {

        }
        ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
        public Rap_MagazinaGjendja_Exp(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
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
            xrLabel12.Text = rm.GetString("TitullRaportiGjendjaMagazinesExp", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell8.Text = rm.GetString("labelKartela", ci);
            xrTableCell9.Text = rm.GetString("labelFilterAvancuarKodbari", ci);
            xrTableCell13.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell17.Text = rm.GetString("labelRaportSasiHyrje", ci);
            xrTableCell19.Text = rm.GetString("labelRaportSasiaDalje", ci);
            xrTableCell25.Text = rm.GetString("labelVlefta", ci);
            xrTableCell21.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
            xrTableCell23.Text = rm.GetString("labelRaportKosto", ci);
            xrTableCell30.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell29.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell27.Text = rm.GetString("filterMagazina", ci);
            xrLabel37.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell15.Text = rm.GetString("labelRaportGjendjaeMeparshme", ci);
            xrTableCell7.Text = rm.GetString("labelRaportikodimagazines", ci);
        }

    }


}
