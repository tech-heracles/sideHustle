using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_LibriShitjeveKosove : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_LibriShitjeveKosove(){InitializeComponent();} 
        public Rap_LibriShitjeveKosove(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_LibriShitjeveKosove(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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

            xrLabel1.Text = rm.GetString("RaportLibriShitjeveTitulli", ci);
            xrLabel23.Text = rm.GetString("labelAdresaKompanise", ci);
            xrLabel21.Text = rm.GetString("labelNjesiOrganizative", ci);
            xrLabel24.Text = rm.GetString("labelFilterAvancuarLlojDok", ci);
            xrLabel6.Text = rm.GetString("labelPaVeprimtari", ci);
            xrLabel8.Text = rm.GetString("labelKonfirmimTransaksioni", ci);
            xrLabel2.Text = rm.GetString("labelShoqeria", ci);
            xrLabel3.Text = rm.GetString("labelNipti", ci);
            xrLabel4.Text = rm.GetString("labelViti", ci);
            xrLabel5.Text = rm.GetString("labelFilterKryesorMuaji", ci);
            xrLabel9.Text = rm.GetString("labelFature", ci);
            xrLabel13.Text = rm.GetString("labelBleresi", ci);
            xrLabel22.Text = rm.GetString("labelShitje", ci);
            xrLabel10.Text = rm.GetString("labelNrFature", ci);
            xrLabel12.Text = rm.GetString("labelDataFormat", ci);
            xrLabel14.Text = rm.GetString("labelEmerTregtarPerson", ci);
            xrLabel15.Text = rm.GetString("labelRrethi", ci);
            xrLabel16.Text = rm.GetString("labelNrFiskal", ci);
            xrLabel17.Text = rm.GetString("labelTotalShitjesh", ci);
            xrLabel19.Text = rm.GetString("labelShitjetePerjashtuara", ci);
            xrLabel20.Text = rm.GetString("labelExporteFurnizime", ci);
            xrLabel28.Text = rm.GetString("labelAdministrimiNrTvsh", ci);
            xrLabel29.Text = rm.GetString("labelVleraTatueshme", ci);
            xrLabel30.Text = rm.GetString("labelTVSH", ci);

            xrLabel70.Text = rm.GetString("labelShumaTotale", ci);
            xrLabel94.Text = rm.GetString("labelKutiaFormularitTeDeklarimitTePageses", ci);

            xrLabel86.Text = rm.GetString("labelKutia", ci) + " (9)";
            xrLabel87.Text = rm.GetString("labelKutia", ci) + " (10)";
            xrLabel85.Text = rm.GetString("labelKutia", ci) + " (11)";
            xrLabel88.Text = rm.GetString("labelKutia", ci) + " (12)";
            
            xrLabel96.Text = rm.GetString("labelshpjegim", ci);
            xrLabel97.Text = rm.GetString("labelEmerMbiemer", ci);
            xrLabel95.Text = rm.GetString("labelShpjegimPrintimi", ci);
            xrLabel98.Text = rm.GetString("labelShtimNrRreshtash", ci);
            xrLabel18.Text = rm.GetString("labelDorezoi", ci);
            xrLabel11.Text = rm.GetString("labelPranoi", ci);
        }
    }
}
