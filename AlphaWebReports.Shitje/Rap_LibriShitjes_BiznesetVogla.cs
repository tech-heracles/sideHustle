using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_LibriShitjes_BiznesetVogla : DevExpress.XtraReports.UI.XtraReport
    {

        public Rap_LibriShitjes_BiznesetVogla()
        {
            InitializeComponent();
        }
        public Rap_LibriShitjes_BiznesetVogla(ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_LibriShitjes_BiznesetVogla(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);

            parameter2.Value = raport.Parameters[1].Value; 

        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {

            ResourceManager rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));
           
            xrLabel17.Text = rm.GetString("RaportLibriShitjeveBizneseteVogla", ci);
            xrLabel37.Text = rm.GetString("labelShoqeria", ci);
            xrLabel35.Text = rm.GetString("labelNipti", ci);
            xrLabel34.Text = rm.GetString("labelViti", ci);
            xrLabel24.Text = rm.GetString("labelFilterKryesorMuaji", ci);
            xrLabel23.Text = rm.GetString("labelAdresaKompanise", ci);
            xrLabel20.Text = rm.GetString("labelNjesiOrganizative", ci);
            xrLabel16.Text = rm.GetString("labelFilterAvancuarLlojDok", ci);
            xrLabel15.Text = rm.GetString("labelPaVeprimtari", ci);
            xrLabel14.Text = rm.GetString("labelKonfirmimTransaksioni", ci);
            xrLabel9.Text = rm.GetString("labelFaturatKuponatShitjes", ci);
            xrLabel10.Text = rm.GetString("labelRaportiNrDok", ci);
            xrLabel11.Text = rm.GetString("labelRaportiDtDok", ci);
            xrLabel18.Text = rm.GetString("labelEmriBleresit", ci);
            xrLabel19.Text = rm.GetString("labelNrFiskal", ci);
            xrLabel21.Text = rm.GetString("labelQyteti", ci);
            xrLabel22.Text = rm.GetString("labelCmimTotalEuro", ci);
            xrLabel47.Text = rm.GetString("labelshpjegim", ci);
            xrLabel26.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel48.Text = rm.GetString("labelShpjegimPrintimObligativ", ci);
            xrLabel13.Text = rm.GetString("labelDorezoi", ci);
            xrLabel12.Text = rm.GetString("labelPranoi", ci);
            xrLabel1.Text = rm.GetString("labelNrRendor", ci);


        } 
    }
}
