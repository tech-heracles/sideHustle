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
    public partial class Rap_LidhjaDokumentaveLidhes : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_LidhjaDokumentaveLidhes(){InitializeComponent();} 

        public Rap_LidhjaDokumentaveLidhes(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_LidhjaDokumentaveLidhes(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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
            xrLabel12.Text = rm.GetString("RaportiLidhjesDokumentaveSiapsDokQeUlDetyriminTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel4.Text = rm.GetString("labelRaportiDokumentaQeRrisinDetyrimet", ci);
            xrLabel5.Text = rm.GetString("labelRaportiDokumentaQeUlinDetyrimet", ci);
            xrLabel6.Text = rm.GetString("labelRaportVlera", ci);
            xrLabel7.Text = rm.GetString("labelRaportLidhjeDokumentash", ci);
            xrLabel10.Text = rm.GetString("labelLlojDokumenti", ci);
            xrLabel11.Text = rm.GetString("labelRaportNumer", ci);
            xrLabel18.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel8.Text = rm.GetString("labelLlojDokumenti", ci);
            xrLabel9.Text = rm.GetString("labelRaportNumer", ci);
            xrLabel13.Text = rm.GetString("labelRaportData", ci);
            xrLabel14.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel17.Text = rm.GetString("labelRaportVleraDokumentit", ci);
            xrLabel15.Text = rm.GetString("labelRaportVleraMbetur", ci);
            xrLabel16.Text = rm.GetString("labelRaportVleraLidhur", ci);
            xrLabel20.Text = rm.GetString("labelRaportVleraMbeturPasLidhjes", ci);
            xrLabel21.Text = rm.GetString("labelRaportNumer", ci);
            xrLabel22.Text = rm.GetString("labelRaportData", ci);
            xrLabel43.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel1.Text = rm.GetString("filterMonedha", ci);
        }
    }
}
