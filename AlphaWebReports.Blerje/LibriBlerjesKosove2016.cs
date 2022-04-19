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
    public partial class LibriBlerjesKosove2016 : DevExpress.XtraReports.UI.XtraReport
    {
		public LibriBlerjesKosove2016(){InitializeComponent();} 
        public LibriBlerjesKosove2016(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public LibriBlerjesKosove2016(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            string data = raport.Parameters["filterDtDok"].Value.ToString();
            string dtFillimi = data.Split('-')[0];
            string muaji = dtFillimi.Split('/')[1] + "/" + dtFillimi.Split('/')[2];
            xrLabel103.Text = muaji;
        }

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel1.Text = rm.GetString("RaportLibriBlerjeveTitulli", ci);

            //xrLabel2.Text = rm.GetString("labelShoqeria", ci);
            //xrLabel3.Text = rm.GetString("labelNipti", ci);
            xrLabel4.Text = rm.GetString("labelViti", ci);
            xrLabel5.Text = rm.GetString("labelFilterKryesorMuaji", ci);
            
            xrLabel6.Text = rm.GetString("lblRaportKsShitjeFatura", ci);//Fature
            xrLabel9.Text = rm.GetString("lblNr", ci);  //NR
            xrLabel12.Text = rm.GetString("lblData", ci);  //Data
            xrLabel7.Text = rm.GetString("labelRaportNumriFatures", ci);  //Nr i fatures

            xrLabel13.Text = rm.GetString("labelShitesi", ci);//shitesi
            xrLabel14.Text = rm.GetString("labelEmriShitesit", ci);//Emri i Shitesit
            xrLabel16.Text = rm.GetString("labelNrFiskal", ci); //Nr tvsh

            
            xrLabel18.Text = rm.GetString("lblRaportKsHead1", ci);//Blerjet dhe importete  liruara dhe me tvsh jo te zbritshme
            xrLabel19.Text = rm.GetString("lblRaportKsBleImpPaTvsh", ci);
            xrLabel17.Text = rm.GetString("lblRaportKsBleImpInvestivePaTvsh", ci);
            xrLabel10.Text = rm.GetString("lblRaportKsBleImpPaTvshZb", ci);
            xrLabel11.Text = rm.GetString("lblRaportKsBleImpIntensivePaTvshZb", ci);

            xrLabel23.Text = rm.GetString("lblRaportKsHead2", ci); //Blerjet dhe Importet e tatushme me 18%, si dhe rregullimet e zbritjeve								
            xrLabel21.Text = rm.GetString("lblRaportKsImp", ci);
            xrLabel24.Text = rm.GetString("lblRaportKsImpInvestive", ci);
            xrLabel25.Text = rm.GetString("lblRaportKsBleVendore", ci);
            xrLabel26.Text = rm.GetString("lblRaportKsBleInvestiveVendore", ci);
            xrLabel30.Text = rm.GetString("lblRaportKsBleNoteDebitore", ci);
            xrLabel32.Text = rm.GetString("lblRaportKsBorxhiKeq", ci);
            xrLabel33.Text = rm.GetString("lblRaportKsRregullime", ci);
            xrLabel36.Text = rm.GetString("lblRaportKsDrejtKreditimi", ci);
            xrLabel37.Text = rm.GetString("lblRaportKsTVSH18", ci);

            xrLabel61.Text = rm.GetString("lblRaportKsHead3", ci); //Blerjet dhe Importet e tatushme me 8%, si dhe rregullimet e zbritjeve			
            xrLabel43.Text = rm.GetString("lblRaportKsImp", ci);
            xrLabel45.Text = rm.GetString("lblRaportKsImpInvestive", ci);
            xrLabel46.Text = rm.GetString("lblRaportKsBleVendore", ci);
            xrLabel47.Text = rm.GetString("lblRaportKsBleInvestiveVendore", ci);
            xrLabel51.Text = rm.GetString("lblRaportKsBleFermere", ci);
            xrLabel53.Text = rm.GetString("lblRaportKsBleNoteDebitore", ci);
            xrLabel54.Text = rm.GetString("lblRaportKsBorxhiKeq", ci);
            xrLabel55.Text = rm.GetString("lblRaportKsRregullime", ci);
            xrLabel56.Text = rm.GetString("lblRaportKsTvsh8", ci);
            xrLabel62.Text = rm.GetString("lblRaportKsTotalTvsh", ci);
            
        }
    }
}
