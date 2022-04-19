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
    public partial class rap_MandatArketimPagesFastech : DevExpress.XtraReports.UI.XtraReport
    {
		public rap_MandatArketimPagesFastech(){InitializeComponent();} 
       
        private ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
        private CultureInfo ci;

        public rap_MandatArketimPagesFastech(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje)
        {

        }
        public rap_MandatArketimPagesFastech(CultureInfo ci, int idNdermarrje)
        {
            this.ci = ci;
            InitializeComponent();
            EmrateLabelave(ci);
            parameterIdNderm.Value = idNdermarrje;
            parameterlabelRaportMAndatArketimi.Value = rm.GetString("labelRaportMAndatArketimi", ci);
            parameterlabelRaportMandatPagese.Value = rm.GetString("labelRaportMandatPagese", ci);
            parameterlabelRaportDerdhje.Value = rm.GetString("labelRaportDerdhje", ci);
            parameterlabelRaportTerheqje.Value = rm.GetString("labelRaportTerheqje", ci);
            parameterlabelRaportArketoniNga.Value = rm.GetString("labelRaportArketoniNga", ci) + ":";
            parameterlabelRAportUPaguaPer.Value = rm.GetString("labelRAportUPaguaPer", ci) + ":";
            parameterlabelRaportUrdherXhirimKlienti.Value = rm.GetString("labelRaportUrdherXhirimKlienti", ci);
            parameterlabelRaportUrdherXhirimYne.Value = rm.GetString("labelRaportUrdherXhirimYne", ci);
            parameterlabelRaportDhenesi.Value = rm.GetString("labelRaportDhenesi", ci);
            parameterlabelRaportMarresi.Value = rm.GetString("labelRaportMarresi", ci);
        }
    /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            xrTableCell35.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell36.Text = rm.GetString("labelRaportData", ci);
            xrTableCell7.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrTableCell10.Text = rm.GetString("labelShumaTotale", ci);
            xrTableCell37.Text = rm.GetString("labelRaportNeFjale", ci);
            xrTableCell34.Text = rm.GetString("labelRaportPer", ci);
            xrTableCell15.Text = rm.GetString("labelRaportiShuma", ci) + ":";
            //xrTableCell41.Text = rm.GetString("labelRaportiSubjekti", ci); 
            xrTableCell19.Text = rm.GetString("labelRaportFinancieri", ci);
            xrTableCell23.Text = rm.GetString("labelRaportArketari", ci);
            xrTableCell30.Text = rm.GetString("labelRaportProdhuarNgaIMB", ci);
            xrTableCell31.Text = rm.GetString("filterRaportIdNdermarje", ci);
            xrTableCell32.Text = rm.GetString("labelRaportPrintuarMe", ci);
            xrTableCell49.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell51.Text = rm.GetString("labelRaportData", ci);
            xrTableCell53.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrTableCell57.Text = rm.GetString("labelShumaTotale", ci);
            xrTableCell60.Text = rm.GetString("labelRaportNeFjale", ci);
            xrTableCell63.Text = rm.GetString("labelRaportPer", ci);
            xrTableCell65.Text = rm.GetString("labelRaportiShuma", ci) + ":";
            //xrTableCell68.Text = rm.GetString("labelRaportiSubjekti", ci); 
            xrTableCell73.Text = rm.GetString("labelRaportFinancieri", ci);
            xrTableCell75.Text = rm.GetString("labelRaportArketari", ci);
            xrTableCell83.Text = rm.GetString("labelRaportProdhuarNgaIMB", ci);
            xrTableCell84.Text = rm.GetString("filterRaportIdNdermarje", ci);
            xrTableCell87.Text = rm.GetString("labelRaportPrintuarMe", ci);
        }

     

    }
}
