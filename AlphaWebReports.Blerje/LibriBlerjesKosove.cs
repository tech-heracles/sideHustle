using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Blerje
{
    public partial class LibriBlerjesKosove : DevExpress.XtraReports.UI.XtraReport
    {
		public LibriBlerjesKosove(){InitializeComponent();} 
       
    
        public LibriBlerjesKosove(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti,  param.IdPerdoruesi, param.ScopeID, report)
        {

        }
        public LibriBlerjesKosove(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, string scopeID, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);

        }

       

        private void LibriBlerjes_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);
            xrLabel100.Text = parametraRaporti.NdermarrjePershkrimi;
            xrLabel101.Text = parametraRaporti.NdermarrjeNipt;
            xrLabel102.Text = parametraRaporti.KodiViti;


            var parameters = AlphaWebReports.raporteUtil.DeserializoParametrat(this.Extensions["filtrat"]);
            var filterDtDokFLD = parameters.Find(param => param.Emri == "filterDtDokFLD");

            if (filterDtDokFLD != null && GetCurrentColumnValue("PERSHKRIMI") != null)
       
                  xrLabel27.Text = GetCurrentColumnValue("PERSHKRIMI").ToString();
                  

        }
        double maxblerjeperjashtuar = 0;
        double shumatotale = 0;
   
   

     
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
           ResourceManager rm = new ResourceManager("Resources.Strings",
                     System.Reflection.Assembly.Load("App_GlobalResources"));
         
            xrLabel1.Text = rm.GetString("RaportLibriBlerjeveTitulli", ci);
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
            xrLabel13.Text = rm.GetString("labelShitesi", ci);
            xrLabel18.Text = rm.GetString("labelBlerje", ci);
             xrLabel10.Text = rm.GetString("labelNrFature", ci);
            xrLabel12.Text = rm.GetString("labelDataFormat", ci);
            xrLabel14.Text = rm.GetString("labelEmerTregtarPerson", ci);
            xrLabel15.Text = rm.GetString("labelRrethi", ci);
            xrLabel16.Text = rm.GetString("labelNrFiskal", ci);
            xrLabel17.Text = rm.GetString("labelTotalBlerjeve", ci);
            xrLabel19.Text = rm.GetString("labelBlerjetImportetpaTVSH", ci);
            xrLabel20.Text = rm.GetString("labelImpor", ci);
             xrLabel22.Text = rm.GetString("labelBlerjetTatueshmeVendore", ci);
             xrLabel25.Text = rm.GetString("labelVleraTatueshme", ci);
             xrLabel26.Text = rm.GetString("labelTVSH", ci);
            xrLabel29.Text = rm.GetString("labelVleraTatueshme", ci);
             xrLabel30.Text = rm.GetString("labelTVSH", ci);

            xrLabel70.Text = rm.GetString("labelShumaTotale", ci);
            xrLabel94.Text = rm.GetString("labelKutiaFormularitTeDeklarimitTePageses", ci);

            xrLabel86.Text = rm.GetString("labelKutia", ci) + " (15)";
            xrLabel87.Text = rm.GetString("labelKutia", ci) + " (16)";
            xrLabel85.Text = rm.GetString("labelKutia", ci) + " (17)";
            xrLabel88.Text = rm.GetString("labelKutia", ci) + " (20)";
            xrLabel92.Text = rm.GetString("labelKutia", ci) + " (21)";
           

            xrLabel96.Text = rm.GetString("labelshpjegim", ci);
            xrLabel97.Text = rm.GetString("labelEmerMbiemer", ci);
            xrLabel95.Text = rm.GetString("labelShpjegimPrintimi", ci);
            xrLabel98.Text = rm.GetString("labelShtimNrRreshtash", ci);
            xrLabel31.Text = rm.GetString("labelDorezoi", ci);
            xrLabel28.Text = rm.GetString("labelPranoi", ci);
        }

        private void xrTableCell1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);

            if ( GetCurrentColumnValue("IDSHITJEKOKA") != null && GetCurrentColumnValue("NRDOK") != null)
            {
                xrTableCell1.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_RegjistrimDokumentash.aspx?scopeID=" + parametraRaporti.ScopeID + "&newScopeId=True&shitje_blerje=blerje&id=" + GetCurrentColumnValue("IDSHITJEKOKA").ToString() + "&numer=" + GetCurrentColumnValue("NRDOK").ToString() + "&shtim_modifikim=modifikim')";
                xrTableCell1.Target = "_self";
            }  
        }

        private void xrTableCell10_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                shuma += double.Parse(e.CalculatedValues[i].ToString());
            e.Result = shuma;
            shumatotale += shuma;
            e.Handled = true;  
        }


        private void xrTableCell10_SummaryReset(object sender, EventArgs e)
        {
            maxblerjeperjashtuar = 0;
        }

        private void xrTableCell10_SummaryRowChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(GetCurrentColumnValue("BLERJEPERJASHTUAR")) > maxblerjeperjashtuar)
                maxblerjeperjashtuar = Convert.ToDouble(GetCurrentColumnValue("BLERJEPERJASHTUAR"));
        }
    }
}
