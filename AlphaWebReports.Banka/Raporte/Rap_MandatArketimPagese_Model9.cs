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
using DevExpress.XtraPrinting;

namespace AlphaWebReports.RaportetDs.Banka.Raporte
{
    public partial class Rap_MandatArketimPagese_Model9 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_MandatArketimPagese_Model9(){InitializeComponent();}
        private ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
        private CultureInfo ci;


        public Rap_MandatArketimPagese_Model9(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje)
        {

        }
        public Rap_MandatArketimPagese_Model9(CultureInfo ci, int idNdermarrje)
        {
            this.ci = ci;
            InitializeComponent();
            EmrateLabelave(ci);
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
            xrTableCell108.Text = rm.GetString("labelRaportFinancieri", ci);
            xrTableCell20.Text = rm.GetString("labelRaportFinancieri", ci);
            xrTableCell67.Text = rm.GetString("labelRaportFinancieri", ci);
            xrTableCell107.Text = rm.GetString("labelRaportArketari", ci);
            xrTableCell66.Text = rm.GetString("labelRaportArketari", ci);
            xrTableCell19.Text = rm.GetString("labelRaportArketari", ci);
            xrTableCell30.Text = rm.GetString("labelRaportProdhuarNgaIMB", ci);
            xrTableCell32.Text = rm.GetString("labelRaportPrintuarMe", ci) + ": ";
            xrTableCell122.Text = rm.GetString("labelRaportPrintuarMe", ci) + ": ";
            xrTableCell81.Text = rm.GetString("labelRaportPrintuarMe", ci) + ": ";
        }
        
    }
    }

