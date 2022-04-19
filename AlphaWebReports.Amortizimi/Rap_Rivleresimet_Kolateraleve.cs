using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.Amortizimi
{
    public partial class Rap_Rivleresimet_Kolateraleve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Rivleresimet_Kolateraleve(){InitializeComponent();} 
     
        public Rap_Rivleresimet_Kolateraleve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) : this(param.Ci, param.IdNdermarrje, param.IdViti, report) { }
        public Rap_Rivleresimet_Kolateraleve(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
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
        
            xrLabel17.Text = rm.GetString("labelRivleresimetKolateraleve", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell1.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell15.Text = rm.GetString("labelDateFillimiAmortizimi", ci);
            xrTableCell3.Text = rm.GetString("labelKodiAsetit", ci);
            xrTableCell5.Text = rm.GetString("labelEmriDebitorit", ci);
            xrTableCell6.Text = rm.GetString("labelVendimiRegjistrimit", ci);
            xrTableCell8.Text = rm.GetString("labelLlojiPrones", ci);
            xrTableCell10.Text = rm.GetString("MenuItemMonedhat", ci);
            xrTableCell11.Text = rm.GetString("lblwriteoff", ci);
            xrTableCell4.Text = rm.GetString("filterSeriali", ci);
            xrTableCell7.Text = rm.GetString("dataRegLibratKontabel", ci);
            xrTableCell14.Text = rm.GetString("labelJetegjatesiaMbetur", ci);
            xrTableCell9.Text = rm.GetString("lblVleraRegj", ci);
            xrTableCell13.Text = rm.GetString("lbljetegjatesiaAmort", ci);
            xrTableCell18.Text = rm.GetString("labelRaportAmortAkumuluar", ci);
            xrTableCell19.Text = rm.GetString("labelVleraMbetur", ci);
            xrTableCell16.Text = rm.GetString("labelVleraMbeturVitiPara", ci);
            xrTableCell17.Text = rm.GetString("labelVleraMbeturMuajiPara", ci);
            xrTableCell21.Text = rm.GetString("labelRaportAmortizimiVjetor", ci);
            xrTableCell2.Text = rm.GetString("labelRaportPershkrimi", ci);
            xrLabel71.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell12.Text = rm.GetString("lblKomenteTeTjera", ci);
            xrTableCell20.Text = rm.GetString("labelRaportAmortizimiMujor", ci);
            
        }

  
    }
}
