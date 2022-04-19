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
    public partial class Rap_ShlyerjeKlientiPeriudhaUnivers : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ShlyerjeKlientiPeriudhaUnivers(){InitializeComponent();} 

        public Rap_ShlyerjeKlientiPeriudhaUnivers(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_ShlyerjeKlientiPeriudhaUnivers(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
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


            xrLabel12.Text = rm.GetString("RaportShlyerjetKlienteveSipasPeriudhesTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel32.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell7.Text = rm.GetString("labelRaportTeKaluara", ci);
            xrTableCell9.Text = rm.GetString("labelRaportGjatePeriudhes", ci);
            xrTableCell10.Text = rm.GetString("labelRaportTotalShlyer", ci);
            xrTableCell8.Text = rm.GetString("labelRaportMbetur", ci);
            xrLabel10.Text = rm.GetString("labelRaportKlienti", ci);
            xrTableCell5.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell17.Text = rm.GetString("labelRaportDetyrimi", ci);
            xrTableCell16.Text = rm.GetString("labelRaportShlyerje", ci);
            xrTableCell11.Text = rm.GetString("labelRaportMbetje", ci);
            xrTableCell19.Text = rm.GetString("labelRaportDetyrimIRi", ci);
            xrTableCell18.Text = rm.GetString("labelRaportShlyerje", ci);
            xrTableCell12.Text = rm.GetString("labelRaportMbetje", ci);
            xrTableCell30.Text = rm.GetString("labelGjithsej", ci);


        }

    }
}
