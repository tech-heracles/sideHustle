using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using DevExpress.XtraReports.Web;	
namespace AlphaWebReports.RaportetDs.ListPagesat.Raportet
{
    public partial class Rap_PasqyraSigurimeveTatimeveRaportuese : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_PasqyraSigurimeveTatimeveRaportuese(){InitializeComponent();} 
        int shifraPasPresjes = 0;
        CultureInfo ci;
        private ResourceManager rm = new ResourceManager("Resources.Strings",
                          System.Reflection.Assembly.Load("App_GlobalResources"));
        public Rap_PasqyraSigurimeveTatimeveRaportuese(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_PasqyraSigurimeveTatimeveRaportuese(CultureInfo ci, int idNdermarrje, int idViti, XtraReport report)
        {
           
            InitializeComponent();
            EmrateLabelave(ci);
            Monedha.Value = report.Parameters[1].Value;
            Ndermarja.Value = report.Parameters[0].Value;
            NdermarjaEmri.Text = report.Parameters[0].Value.ToString();
            Departamenti.Value = report.Parameters[4].Value;
            Nendepartamenti.Value = report.Parameters[5].Value;
            NrPunonjesit.Value = report.Parameters[3].Value;
            PeriudhaMuaji.Text = report.Parameters["filterMuaji"].Value.ToString();
            
        }

        
       
     
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>

        private void EmrateLabelave(CultureInfo ci)
        {

            xrLabel66.Text = rm.GetString("DeklarataListpgKontributetSigShoq", ci);
            xrLabel68.Text = "1)" + rm.GetString("labelAdministrimiNIPT", ci) ;
            xrLabel70.Text = "2)" + rm.GetString("labelRaportEmriTatimpaguesit", ci)+ ":";
            xrLabel71.Text = "3)" + rm.GetString("labelRaportPeriudhaTatimore", ci) + ":";
            xrLabel72.Text = rm.GetString("filterMuaji", ci);
            xrLabel69.Text = "4)" + rm.GetString("lblPeriodicitetiDeklarimit", ci) + ":";
            xrLabel65.Text = "5)" + rm.GetString("lblAdresaKryesore", ci) + ":";
            xrLabel81.Text = "6)" + rm.GetString("labelRaportVeprimtariaKryesore", ci) + ":";
            xrLabel82.Text = rm.GetString("labelRaportTregti", ci);
            xrLabel83.Text = "7)" + rm.GetString("labelRaportVeprimtariaDegesNjesise",ci) + ":";
            xrLabel73.Text = "8)";
            xrLabel104.Text = rm.GetString("lblPaVeprimtari", ci);
            xrLabel41.Text = rm.GetString("lblNr", ci);
            xrLabel37.Text ="9)"+ rm.GetString("lblNumërPersonal", ci);
            xrLabel39.Text = "10)" + rm.GetString("lblEmriMbiemri", ci);
            xrLabel40.Text = "11)" + rm.GetString("lblDetFunxProfPuna", ci);
            xrLabel30.Text = "12)" + rm.GetString("lblNrKatPerKontribute", ci);
            xrLabel15.Text = "13)" + rm.GetString("lblDiteKalendarikePaPunuar", ci);
            xrLabel16.Text = "14)" + rm.GetString("lblDiteKalendarikePunuar", ci);
            xrLabel34.Text = "15)" + rm.GetString("lblPagaBrutoLEK", ci);
            xrLabel35.Text = "16)" + rm.GetString("lblPagaBrutoPerSigShoq",ci);
            xrLabel17.Text = rm.GetString("lblKontrubutePerSigShoq", ci);
            xrLabel21.Text = rm.GetString("lblNgaKëto", ci)+ ":";
            xrLabel23.Text = rm.GetString("lblKontributeSupl", ci);
            xrLabel1.Text = "17)" + rm.GetString("lblPunedhenesi", ci);
            xrLabel18.Text = "18)" + rm.GetString("lblPunemarresi", ci);
            xrLabel24.Text = "19)" + rm.GetString("lblGjithsej", ci);
            xrLabel3.Text = "20)" + rm.GetString("lblPunedhenesi", ci);
            xrLabel2.Text = "21)" + rm.GetString("lblPunemarresi", ci);
            xrLabel4.Text = "22)" + rm.GetString("lblGjithsej2", ci);
            xrLabel49.Text = "23)" + rm.GetString("lblTotaliSigShoq", ci);
            xrLabel44.Text = "24)" + rm.GetString("lblPagaBrutoMbiKontributetSigShend", ci);
            xrLabel46.Text = "25)" + rm.GetString("lblKontributeSigShend", ci);
            xrLabel26.Text = "26)" + rm.GetString("lblTAP", ci);
            xrLabel27.Text = "27)" + " " + rm.GetString("lblshenimeUPPERCASE", ci);
            xrLabel5.Text = "28)" + rm.GetString("lblNdermarrjeBije", ci);
            xrLabel96.Text = "29)" + rm.GetString("lblDeklarojDhenatListepagese", ci);
            xrLabel97.Text = "30)" + rm.GetString("lblPunonjesDuhetPagKontributeShoq", ci);
            xrLabel29.Text = rm.GetString("lblpersona", ci);
            xrLabel32.Text = rm.GetString("lbllekë", ci);
            xrLabel33.Text = "31)" + rm.GetString("lblAdministratori", ci);
            xrLabel36.Text = "(" + rm.GetString("lblemermbiemer", ci) + ")";
            xrLabel38.Text = "32)" + rm.GetString("lblDeklaruesi", ci);
            xrLabel43.Text = rm.GetString("lblemermbiemernenshkrimi", ci);
            xrLabel80.Text = rm.GetString("labelRaportMujore", ci);
        }

        private void Rap_PasqyraSigurimeveTatimeveRaportuese_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);
            NdermarjeNipti.Text = parametraRaporti.NdermarrjeNipt;
            xrLabel67.Text = parametraRaporti.NdermarrjeVendi;
            PeriudhaViti.Text = rm.GetString("labelViti", ci) + " " + parametraRaporti.KodiViti;
        }
    }
}

