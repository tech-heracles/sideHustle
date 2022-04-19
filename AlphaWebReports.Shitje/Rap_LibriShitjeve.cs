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
    public partial class Rap_LibriShitjeve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_LibriShitjeve(){InitializeComponent();} 
       

        public Rap_LibriShitjeve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.ScopeID, report)
        {

        }
        public Rap_LibriShitjeve(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, string scopeID,
            DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);

        }

        private void Rap_LibriShitjeve_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);
            xrLabel100.Text = parametraRaporti.NdermarrjePershkrimi;
            xrLabel101.Text = parametraRaporti.NdermarrjeNipt;            
            xrLabel102.Text = parametraRaporti.KodiViti;
        }

        private void xrLabel53_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);

            if (GetCurrentColumnValue("IDSHITJEKOKA") != null && GetCurrentColumnValue("NRDOK") != null)
            {
                xrLabel53.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_RegjistrimDokumentash.aspx?scopeID=" + parametraRaporti.ScopeID + "&newScopeId=True&shitje_blerje=shitje&id=" + GetCurrentColumnValue("IDSHITJEKOKA").ToString() + "&numer=" + GetCurrentColumnValue("NRDOK").ToString() + "&shtim_modifikim=modifikim')";
                xrLabel53.Target = "_self";
            }  
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
            xrLabel2.Text = rm.GetString("labelShoqeria", ci);
            xrLabel3.Text = rm.GetString("labelNipti", ci);
            xrLabel4.Text = rm.GetString("labelViti", ci);
            xrLabel5.Text = rm.GetString("labelFilterKryesorMuaji", ci);
            xrLabel6.Text = rm.GetString("labelPaVeprimtari", ci);
            xrLabel8.Text = rm.GetString("labelKonfirmimTransaksioni", ci);
            xrLabel9.Text = rm.GetString("labelFature", ci);
            xrLabel10.Text = rm.GetString("labelNrFature", ci);
            xrLabel11.Text = rm.GetString("labelFilterAvancuarNrSerial", ci);
            xrLabel12.Text = rm.GetString("labelDataFormat", ci);
            xrLabel13.Text = rm.GetString("labelBleresi", ci);
            xrLabel14.Text = rm.GetString("labelEmerTregtarPerson", ci);
            xrLabel15.Text = rm.GetString("labelRrethi", ci);
            xrLabel16.Text = rm.GetString("labelNIPT", ci);
            xrLabel17.Text = rm.GetString("labelTotalShitjesh", ci);
            xrLabel19.Text = rm.GetString("labelShitjetePerjashtuara", ci);
            xrLabel20.Text = rm.GetString("labelExporteFurnizime", ci) ;
            xrLabel22.Text = rm.GetString("labelShitjeMeShkalle", ci) + " 20%";
            xrLabel23.Text = rm.GetString("labelShitjeMeShkalle", ci) + " 10%";
            xrLabel29.Text = rm.GetString("labelVleraTatueshme", ci);
            xrLabel30.Text = rm.GetString("labelTVSH", ci);
            xrLabel31.Text = rm.GetString("labelVleraTatueshme", ci);
            xrLabel32.Text = rm.GetString("labelTVSH", ci);
            xrLabel70.Text = rm.GetString("labelShumaTotale", ci);
            xrLabel94.Text = rm.GetString("labelKutiaFormularitTeDeklarimitTePageses", ci);

            xrLabel86.Text = rm.GetString("labelKutia", ci) + " (9)";
            xrLabel87.Text = rm.GetString("labelKutia", ci) + " (10)";
            xrLabel85.Text = rm.GetString("labelKutia", ci) + " (11)";
            xrLabel88.Text = rm.GetString("labelKutia", ci) + " (12)";
            xrLabel91.Text = rm.GetString("labelKutia", ci) + " (13)";
            xrLabel90.Text = rm.GetString("labelKutia", ci) + " (14)";

            xrLabel96.Text = rm.GetString("labelshpjegim", ci);
            xrLabel97.Text = rm.GetString("labelEmerMbiemer", ci);
            xrLabel95.Text = rm.GetString("labelShpjegimPrintimi", ci);
            xrLabel98.Text = rm.GetString("labelShtimNrRreshtash", ci);
           
        }
    }
}
