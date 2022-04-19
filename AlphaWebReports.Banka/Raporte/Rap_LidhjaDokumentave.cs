using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Banka.Raporte
{
    public partial class Rap_LidhjaDokumentave : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_LidhjaDokumentave()
        {
            InitializeComponent();
        }
        ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

        public Rap_LidhjaDokumentave(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_LidhjaDokumentave(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[10].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
            parameter6.Value = raport.Parameters[5].Value;
            parameter7.Value = raport.Parameters[6].Value;
            parameter8.Value = raport.Parameters[7].Value;
            parameter9.Value = raport.Parameters[8].Value;
            parameter10.Value = raport.Parameters[9].Value;
            Monedha.Value = raport.Parameters["monedhaKF"].Value;
            MonNder.Value = raport.Parameters[11].Value;

   
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
            xrTableCell10.Text = rm.GetString("labelRaportVlera", ci);
            xrTableCell9.Text = rm.GetString("labelRaportLidhjeDokumentash", ci);
            xrTableCell16.Text = xrTableCell18.Text = rm.GetString("labelLlojDokumenti", ci);
            xrTableCell23.Text = xrTableCell15.Text =  Numer.Text = rm.GetString("labelRaportNumer", ci);
            xrTableCell19.Text = xrTableCell14.Text = xrTableCell17.Text = rm.GetString("labelRaportData", ci);
            xrTableCell12.Text = xrTableCell11.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell20.Text = rm.GetString("labelRaportVleraDokumentit", ci);
            xrTableCell21.Text = rm.GetString("labelRaportVleraMbetur", ci);
            xrTableCell22.Text = rm.GetString("labelRaportVleraLidhur", ci);
            xrTableCell13.Text = rm.GetString("labelRaportVleraMbeturPasLidhjes", ci);
            xrTableCell24.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell25.Text = rm.GetString("filterMonedha", ci);
 
        }

     
    }
}
