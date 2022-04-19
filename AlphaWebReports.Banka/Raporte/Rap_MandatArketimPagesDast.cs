using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Data;
using System.Collections.Generic;
namespace AlphaWebReports.RaportetDs.Banka.Raporte
{
    public partial class Rap_MandatArketimPagesDast : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_MandatArketimPagesDast(){InitializeComponent();} 

        public Rap_MandatArketimPagesDast(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje)
        {

        }

        public Rap_MandatArketimPagesDast(CultureInfo ci, int idNdermarrje)
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
            xrTableCell35.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell36.Text = rm.GetString("labelRaportData", ci);
            xrTableCell7.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrTableCell10.Text = rm.GetString("labelShumaTotale", ci);
            xrTableCell37.Text = rm.GetString("labelRaportNeFjale", ci);
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
            xrTableCell73.Text = rm.GetString("labelRaportFinancieri", ci);
            xrTableCell75.Text = rm.GetString("labelRaportArketari", ci);
            xrTableCell83.Text = rm.GetString("labelRaportProdhuarNgaIMB", ci);
            xrTableCell84.Text = rm.GetString("filterRaportIdNdermarje", ci);
            xrTableCell87.Text = rm.GetString("labelRaportPrintuarMe", ci);
        }
    }
}
