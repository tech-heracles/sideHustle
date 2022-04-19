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
    public partial class Rap_MaturimAnalitikKF : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_MaturimAnalitikKF()
        {
            InitializeComponent();
        }
 
        public Rap_MaturimAnalitikKF(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.IdGjuha, param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_MaturimAnalitikKF(int idGjuha,CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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


            xrLabel12.Text = rm.GetString("RaportMaturimiAnalitikKlientFurnitorTitulli", ci);

            xrTableCell7.Text = rm.GetString("filterMonedha", ci);
            xrTableCell9.Text = rm.GetString("labelLlojDokumenti", ci);
            xrTableCell12.Text = rm.GetString("labelRaportNumer", ci);
            xrTableCell13.Text = rm.GetString("labelRaportData", ci);
            xrTableCell14.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell15.Text = rm.GetString("labelRaportVlera", ci);
            xrTableCell16.Text = rm.GetString("labelRaportDiteMaturimi", ci);
            xrTableCell17.Text = rm.GetString("labelRaportDtMaturimi", ci);
           xrLabel34.Text = rm.GetString("labelRaportiBalanca", ci);                     
        }


    }
}
