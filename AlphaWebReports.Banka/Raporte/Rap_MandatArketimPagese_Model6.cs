using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Collections.Generic;
using System.Data;
namespace AlphaWebReports.RaportetDs.Banka.Raporte
{
    public partial class Rap_MandatArketimPagese_Model6 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_MandatArketimPagese_Model6(){InitializeComponent();} 
        public Rap_MandatArketimPagese_Model6(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje)
        {

        }

        public Rap_MandatArketimPagese_Model6(CultureInfo ci, int idNdermarrje)
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
            xrTableCell46.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell36.Text = rm.GetString("labelRaportData", ci);
            xrTableCell48.Text = rm.GetString("labelRaportData", ci);
            xrTableCell7.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrTableCell50.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrTableCell10.Text = rm.GetString("labelShumaTotale", ci);
            xrTableCell55.Text = rm.GetString("labelShumaTotale", ci);
            xrTableCell37.Text = rm.GetString("labelRaportNeFjale", ci);
            xrTableCell58.Text = rm.GetString("labelRaportNeFjale", ci);
            xrTableCell19.Text = rm.GetString("labelRaportFinancieri", ci);
            xrTableCell61.Text = rm.GetString("labelRaportFinancieri", ci);
            xrTableCell23.Text = rm.GetString("labelRaportArketari", ci);
            xrTableCell63.Text = rm.GetString("labelRaportArketari", ci);
            xrTableCell30.Text = rm.GetString("labelRaportProdhuarNgaIMB", ci);
            xrTableCell73.Text = rm.GetString("labelRaportProdhuarNgaIMB", ci);
            xrTableCell32.Text = rm.GetString("labelRaportPrintuarMe", ci);
            xrTableCell76.Text = rm.GetString("labelRaportPrintuarMe", ci);
            xrTableCell9.Text = rm.GetString("labelRaportArketoniNga", ci) + ':';
            xrTableCell53.Text = rm.GetString("labelRaportArketoniNga", ci) + ':';
        }

    }
}
