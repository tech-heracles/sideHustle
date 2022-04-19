using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Arka.Raporte
{
    public partial class Rap_LidhjaDokumentave : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_LidhjaDokumentave(){InitializeComponent();} 


      
        public Rap_LidhjaDokumentave(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_LidhjaDokumentave(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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


            xrLabel12.Text = rm.GetString("RaportiLidhjesDokumentaveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell7.Text = rm.GetString("labelRaportiDokumentaQeRrisinDetyrimet", ci);
            xrTableCell8.Text = rm.GetString("labelRaportiDokumentaQeUlinDetyrimet", ci);
            xrTableCell9.Text = rm.GetString("labelRaportVlera", ci);
            xrTableCell10.Text = rm.GetString("labelRaportLidhjeDokumentash", ci);
            xrTableCell16.Text = rm.GetString("labelLlojDokumenti", ci);
            xrTableCell15.Text = rm.GetString("labelRaportNumer", ci);
            xrTableCell17.Text = rm.GetString("labelRaportData", ci);
            xrTableCell11.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell19.Text = rm.GetString("labelLlojDokumenti", ci);
            xrTableCell18.Text = rm.GetString("labelRaportNumer", ci);
            xrTableCell20.Text = rm.GetString("labelRaportData", ci);
            xrTableCell12.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell22.Text = rm.GetString("labelRaportVleraDokumentit", ci);
            xrTableCell21.Text = rm.GetString("labelRaportVleraMbetur", ci);
            xrTableCell23.Text = rm.GetString("labelRaportVleraLidhur", ci);
            xrTableCell13.Text = rm.GetString("labelRaportVleraMbeturPasLidhjes", ci);
            xrTableCell24.Text = rm.GetString("labelRaportNumer", ci);
            xrTableCell14.Text = rm.GetString("labelRaportData", ci);
            xrLabel43.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel1.Text = rm.GetString("filterMonedha", ci);
        }
    }
}
