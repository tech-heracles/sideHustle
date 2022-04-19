using System;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    
    public partial class Rap_ShitjetSipasSasiveKrahasuese : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ShitjetSipasSasiveKrahasuese(){InitializeComponent();} 

        
		public Rap_ShitjetSipasSasiveKrahasuese(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
		this(param.Ci,param.IdNdermarrje,param.IdPerdoruesi,param.GuidString,param.IdRaporti,param.IdGjuha,param.Vjen,param.IdViti,param.IdSubRaporti,report)
		{

		}

            public Rap_ShitjetSipasSasiveKrahasuese(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport) {
            InitializeComponent();
            EmrateLabelave(ci);

            

        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            var rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));


            titulliLabel.Text = rm.GetString("labelRaportShitjetSipasSasiveKrahasueseTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            
            //labelRaportiTotali.Text = rm.GetString("labelRaportiTotali", ci);
            labelLogoIMB.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell85.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell86.Text = rm.GetString("labelRaportJanar", ci);
            xrTableCell87.Text = rm.GetString("labelRaportJanar", ci);
            xrTableCell88.Text = rm.GetString("labelRaportShkurt", ci);
            xrTableCell89.Text = rm.GetString("labelRaportShkurt", ci);
            xrTableCell90.Text = rm.GetString("labelRaportMars", ci);
            xrTableCell91.Text = rm.GetString("labelRaportMars", ci);
            xrTableCell92.Text = rm.GetString("labelRaportPrill", ci);
            xrTableCell93.Text = rm.GetString("labelRaportPrill", ci);
            xrTableCell94.Text = rm.GetString("labelRaportMaj", ci);
            xrTableCell95.Text = rm.GetString("labelRaportMaj", ci);
            xrTableCell96.Text = rm.GetString("labelRaportQershor", ci);
            xrTableCell97.Text = rm.GetString("labelRaportQershor", ci);
            xrTableCell98.Text = rm.GetString("labelRaportKorrik", ci);
            xrTableCell99.Text = rm.GetString("labelRaportKorrik", ci);
            xrTableCell100.Text = rm.GetString("labelRaportGusht", ci);
            xrTableCell101.Text = rm.GetString("labelRaportGusht", ci);
            xrTableCell102.Text = rm.GetString("labelRaportShtator", ci);
            xrTableCell103.Text = rm.GetString("labelRaportShtator", ci);
            xrTableCell104.Text = rm.GetString("labelRaportTetor", ci);
            xrTableCell105.Text = rm.GetString("labelRaportTetor", ci);
            xrTableCell106.Text = rm.GetString("labelRaportNentor", ci);
            xrTableCell107.Text = rm.GetString("labelRaportNentor", ci);
            xrTableCell108.Text = rm.GetString("labelRaportDhjetor", ci);
            xrTableCell109.Text = rm.GetString("labelRaportDhjetor", ci);
        }
    }
}
