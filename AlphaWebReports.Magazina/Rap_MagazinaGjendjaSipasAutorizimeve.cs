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
    public partial class Rap_MagazinaGjendjaSipasAutorizimeve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_MagazinaGjendjaSipasAutorizimeve(){InitializeComponent();} 
  
        public Rap_MagazinaGjendjaSipasAutorizimeve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.IdRaporti, param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.IdGjuha, report)
        {

        }
        public Rap_MagazinaGjendjaSipasAutorizimeve(int idRaporti, CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, int idGjuha, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
     

        }

      

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell11.Text = rm.GetString("labelRaportikodimagazines", ci);
            xrTableCell7.Text = rm.GetString("labelKartela", ci);
            xrTableCell8.Text = rm.GetString("labelFilterAvancuarKodbari", ci);
            xrTableCell9.Text = rm.GetString("labelRaportPershkrimi", ci);
            xrTableCell10.Text = rm.GetString("labelRaportGjendjaeMeparshme", ci);
            xrTableCell19.Text = rm.GetString("labelRaportSasiHyrje", ci);
            xrTableCell17.Text = rm.GetString("labelRaportSasiaDalje", ci);
            xrTableCell21.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
            xrTableCell27.Text = rm.GetString("labelRaportKosto", ci);
            xrTableCell29.Text = rm.GetString("labelVlefta", ci);
            xrLabel77.Text = rm.GetString("labelLogoIMB", ci);
        }

    }
}
