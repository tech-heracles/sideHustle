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
    public partial class Rap_LibriShitjeveKosove2016 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_LibriShitjeveKosove2016(){InitializeComponent();} 

        public Rap_LibriShitjeveKosove2016(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.ScopeID,  report)
        {

        }
        public Rap_LibriShitjeveKosove2016(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, string scopeID, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);



            string data = raport.Parameters["filterDtDok"].Value.ToString();
            string dtFillimi = data.Split('-')[0];
            string muaji = dtFillimi.Split('/')[1] + "/" + dtFillimi.Split('/')[2];
            xrLabel103.Text = muaji;

        }

        private void Rap_LibriShitjeveKosove2016_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);
            xrLabel100.Text = parametraRaporti.NdermarrjePershkrimi;
            xrLabel101.Text = parametraRaporti.NdermarrjeNipt;
            xrLabel102.Text = parametraRaporti.KodiViti;
        }

        private void xrTableCell3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);

            if ( GetCurrentColumnValue("IDSHITJEKOKA") != null && GetCurrentColumnValue("nrdok") != null)
            {
                xrTableCell3.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_RegjistrimDokumentash.aspx?scopeID=" + parametraRaporti.ScopeID + "&newScopeId=True&shitje_blerje=shitje&id=" + GetCurrentColumnValue("IDSHITJEKOKA").ToString() + "&numer=" + GetCurrentColumnValue("nrdok").ToString() + "&shtim_modifikim=modifikim')";
                xrTableCell3.Target = "_self";
            }
        }


        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel1.Text = rm.GetString("RaportLibriShitjeveTitulli", ci);

            xrLabel2.Text = rm.GetString("lblRaportKsTatimPaguesi", ci);
            xrLabel3.Text = rm.GetString("lblRaportKsNumriFiskal", ci);
            xrLabel4.Text = rm.GetString("labelViti", ci);
            xrLabel5.Text = rm.GetString("labelFilterKryesorMuaji", ci);

            xrLabel6.Text = rm.GetString("lblRaportKsShitjeFatura", ci);//Fatura
            xrLabel9.Text = rm.GetString("lblNr", ci);  //NR
            xrLabel12.Text = rm.GetString("lblData", ci);  //Data
            xrLabel7.Text = rm.GetString("labelRaportNumriFatures", ci);  //Nr i fatures


            xrLabel13.Text = rm.GetString("labelBleresi", ci);//shitesi
            xrLabel14.Text = rm.GetString("labelEmriBleresit", ci);//Emri i Shitesit
            xrLabel16.Text = rm.GetString("labelNrFiskal", ci); //Nr tvsh
            xrLabel34.Text = rm.GetString("lblRaportKsNipti", ci); //Nipti

            xrLabel18.Text = rm.GetString("lblRaportKsShitjeHead1", ci);//Blerjet dhe importete  liruara dhe me tvsh jo te zbritshme
            xrLabel19.Text = rm.GetString("lblRaportKsShitje0", ci);
            xrLabel17.Text = rm.GetString("lblRaportKsShitjeJashte0", ci);
            xrLabel10.Text = rm.GetString("lblRaportKsShitjeNegative0", ci);
            xrLabel11.Text = rm.GetString("lblRaportKsShitjeLiruara0", ci);
            xrLabel21.Text = rm.GetString("lblRaportKsShitjeTotal0", ci);
            xrLabel24.Text = rm.GetString("lblRaportKsShitjeExport", ci);

            xrLabel23.Text = rm.GetString("lblRaportKsShitjeHead2", ci); //Blerjet dhe Importet e tatushme me 18%, si dhe rregullimet e zbritjeve								
            xrLabel25.Text = rm.GetString("lblRaportKsShitjeTat18", ci);
            xrLabel26.Text = rm.GetString("lblRaportKsShitjeNegative18", ci);
            xrLabel30.Text = rm.GetString("lblRaportKsShitjeBorxhiKeq18", ci);
            xrLabel32.Text = rm.GetString("lblRaportKsShitjeRregullime18", ci);
            xrLabel33.Text = rm.GetString("lblRaportKsShitjeAutongarkese18", ci);
            xrLabel36.Text = rm.GetString("lblRaportKsShitjeTotal18", ci);
            

            xrLabel61.Text = rm.GetString("lblRaportKsShitjeHead3", ci); //Blerjet dhe Importet e tatushme me 8%, si dhe rregullimet e zbritjeve			
            xrLabel37.Text = rm.GetString("lblRaportKsShitjeTat18", ci);
            xrLabel43.Text = rm.GetString("lblRaportKsShitjeNegative18", ci);
            xrLabel45.Text = rm.GetString("lblRaportKsShitjeBorxhiKeq18", ci);
            xrLabel46.Text = rm.GetString("lblRaportKsShitjeRregullime18", ci);
            xrLabel47.Text = rm.GetString("lblRaportKsShitjeTvsh8", ci);
            xrLabel51.Text = rm.GetString("lblRaportKsShitjeTotalTvsh", ci);
        }
    }
}
