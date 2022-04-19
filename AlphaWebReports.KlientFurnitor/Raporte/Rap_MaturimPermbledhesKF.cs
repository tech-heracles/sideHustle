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
    public partial class Rap_MaturimPermbledhesKF : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_MaturimPermbledhesKF()
        {
            InitializeComponent();

        }

        public Rap_MaturimPermbledhesKF(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_MaturimPermbledhesKF(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
       
            Int1.Value = Int64.Parse(raport.Parameters["txtInterval1"].Value.ToString());
            Int2.Value = Int64.Parse(raport.Parameters["txtInterval2"].Value.ToString());
            Int3.Value = Int64.Parse(raport.Parameters["txtInterval3"].Value.ToString());
            Int4.Value = Int64.Parse(raport.Parameters["txtInterval4"].Value.ToString());
            Int5.Value = Int64.Parse(raport.Parameters["txtInterval5"].Value.ToString());
            Int6.Value = Int64.Parse(raport.Parameters["txtInterval6"].Value.ToString());
 
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));


            xrLabel12.Text = rm.GetString("RaportMaturimiPermbledhesKlientFurnitorTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel62.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell1.Text = rm.GetString("labelKodKlientFurnitor", ci);
            xrTableCell2.Text = rm.GetString("labelRaportiEmertimi", ci);
            xrTableCell3.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrTableCell7.Text = rm.GetString("labelRaportPagesaFundit", ci);
            xrTableCell8.Text = rm.GetString("labelRaportData", ci);
            xrTableCell10.Text = rm.GetString("labelRaportNumer", ci);
            xrTableCell12.Text = rm.GetString("labelRaportVlera", ci);
            xrTableCell11.Text = rm.GetString("labelRaportiFaturuar", ci);
            xrTableCell14.Text = rm.GetString("labelRaportiPaguar", ci);
            xrTableCell16.Text = rm.GetString("labelRaportGjendje", ci);
            xrTableCell18.Text = rm.GetString("labelRaportiFaturuar", ci);
            xrTableCell15.Text = rm.GetString("labelRaportiPaguar", ci);
            xrTableCell20.Text = rm.GetString("labelRaportGjendje", ci);
            xrTableCell19.Text = rm.GetString("labelRaportiFaturuar", ci);
            xrTableCell22.Text = rm.GetString("labelRaportiPaguar", ci);
            xrTableCell24.Text = rm.GetString("labelRaportGjendje", ci);
            xrTableCell26.Text = rm.GetString("labelRaportiFaturuar", ci);
            xrTableCell25.Text = rm.GetString("labelRaportiPaguar", ci);
            xrTableCell28.Text = rm.GetString("labelRaportGjendje", ci);
            xrTableCell27.Text = rm.GetString("labelRaportiFaturuar", ci);
            xrTableCell30.Text = rm.GetString("labelRaportiPaguar", ci);
            xrTableCell32.Text = rm.GetString("labelRaportGjendje", ci);
            xrTableCell34.Text = rm.GetString("labelRaportiFaturuar", ci);
            xrTableCell33.Text = rm.GetString("labelRaportiPaguar", ci);
            xrTableCell36.Text = rm.GetString("labelRaportGjendje", ci);
            xrTableCell29.Text = rm.GetString("labelRaportiBalanca", ci);


        }
    }
}
