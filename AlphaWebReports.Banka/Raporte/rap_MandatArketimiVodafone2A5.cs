using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Banka.Raporte
{
    public partial class rap_MandatArketimiVodafone2A5 : DevExpress.XtraReports.UI.XtraReport
    {
		public rap_MandatArketimiVodafone2A5(){InitializeComponent();}
        private System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings",
                     System.Reflection.Assembly.Load("App_GlobalResources"));

        public rap_MandatArketimiVodafone2A5(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje)
        {

        }

        public rap_MandatArketimiVodafone2A5(CultureInfo ci, int idNdermarrje)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            parameterIdNderm.Value = idNdermarrje;
            parameterlabelRaportMAndatArketimi.Value = rm.GetString("labelRaportMAndatArketimi", ci);
            parameterlabelRaportMandatPagese.Value = rm.GetString("labelRaportMandatPagese", ci);
            parameterlabelRaportDerdhje.Value = rm.GetString("labelRaportDerdhje", ci);
            parameterlabelRaportTerheqje.Value = rm.GetString("labelRaportTerheqje", ci);
            parameterlabelRaportMarresi.Value = rm.GetString("labelRaportMarresi", ci);
            parameterlabelRaportArketoniNgaVodf.Value = rm.GetString("labelRaportArketoniNgaVodf", ci);
            parameterlabelRaportPaguaniVodf.Value = rm.GetString("labelRaportPaguaniVodf", ci);
            parameterlabelRaportKlienti.Value = rm.GetString("labelRaportKlienti", ci);
            parameterlabelRaportArketari.Value = rm.GetString("labelRaportArketari", ci);
            parameterlabelRaportiAnullimArketimi.Value = rm.GetString("labelRaportiAnullimArketimi", ci);
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
          ResourceManager rm = new ResourceManager("Resources.Strings",
                          System.Reflection.Assembly.Load("App_GlobalResources"));
            xrTableCell35.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell36.Text = rm.GetString("labelRaportData", ci);
            xrTableCell7.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell41.Text = rm.GetString("labelRaportArketoniNgaVodf", ci);
            xrTableCell37.Text = rm.GetString("labelRaportLeke", ci) ;
            xrTableCell34.Text = rm.GetString("labelRaportPerVodf", ci) ;
            xrTableCell19.Text = rm.GetString("labelRaportArketari", ci);
            xrTableCell23.Text = rm.GetString("labelRaportKlienti", ci);
            xrTableCell30.Text = rm.GetString("labelRaportProdhuarNgaIMB", ci);
            xrTableCell31.Text = rm.GetString("filterRaportIdNdermarje", ci);
            xrTableCell39.Text = rm.GetString("labelRaportBarcode", ci);
            xrTableCell32.Text = rm.GetString("labelRaportPrintuarMe", ci);
            xrTableCell9.Text = rm.GetString("labelRaportiShumaVodf", ci) ;


            xrTableCell44.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell46.Text = rm.GetString("labelRaportData", ci);
            xrTableCell48.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell53.Text = rm.GetString("labelRaportArketoniNgaVodf", ci);
            xrTableCell59.Text = rm.GetString("labelRaportLeke", ci);
            xrTableCell62.Text = rm.GetString("labelRaportPerVodf", ci);
            xrTableCell65.Text = rm.GetString("labelRaportArketari", ci);
            xrTableCell69.Text = rm.GetString("labelRaportKlienti", ci);
            xrTableCell77.Text = rm.GetString("labelRaportProdhuarNgaIMB", ci);
            xrTableCell78.Text = rm.GetString("filterRaportIdNdermarje", ci);
            xrTableCell80.Text = rm.GetString("labelRaportBarcode", ci);
            xrTableCell81.Text = rm.GetString("labelRaportPrintuarMe", ci);
            xrTableCell56.Text = rm.GetString("labelRaportiShumaVodf", ci);


        }

        

    }
}
