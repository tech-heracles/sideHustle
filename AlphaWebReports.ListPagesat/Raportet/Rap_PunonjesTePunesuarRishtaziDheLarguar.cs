using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.ListPagesat.Raportet
{
    public partial class Rap_PunonjesTePunesuarRishtaziDheLarguar : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_PunonjesTePunesuarRishtaziDheLarguar(){InitializeComponent();} 
        private CultureInfo ci;
        private ResourceManager rm = new ResourceManager("Resources.Strings",
                          System.Reflection.Assembly.Load("App_GlobalResources"));
        public Rap_PunonjesTePunesuarRishtaziDheLarguar(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_PunonjesTePunesuarRishtaziDheLarguar(CultureInfo ci, int idNdermarrje, int idViti, XtraReport report)
        {
           
            this.ci = ci;
            InitializeComponent();
            EmrateLabelave(ci);
            string data = report.Parameters["filterDtDok"].Value.ToString();
            string dtFillimi = data.Split('-')[0];
            dataFillimLabel.Text = dtFillimi;
            string muaji = dtFillimi.Split('/')[1];
            switch (muaji)
            {
                case "01":
                    PeriudhaMuaji.Text = "Janar";
                    break;
                case "02":
                    PeriudhaMuaji.Text =  "Shkurt";
                    break;
                case "03":
                    PeriudhaMuaji.Text = "Mars";
                    break;
                case "04":
                    PeriudhaMuaji.Text = "Prill";
                    break;
                case "05":
                    PeriudhaMuaji.Text = "Maj";
                    break;
                case "06":
                    PeriudhaMuaji.Text = "Qershor";
                    break;
                case "07":
                    PeriudhaMuaji.Text = "Korrik";
                    break;
                case "08":
                    PeriudhaMuaji.Text = "Gusht";
                    break;
                case "09":
                    PeriudhaMuaji.Text = "Shtator";
                    break;
                case "10":
                    PeriudhaMuaji.Text = "Tetor";
                    break;
                case "11":
                    PeriudhaMuaji.Text = "Nentor";
                    break;
                case "12":
                    PeriudhaMuaji.Text = "Dhjetor";
                    break;
                default:
                    break;
            }
        }
     
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>

        private void EmrateLabelave(CultureInfo ci)
        {

            xrLabel47.Text = rm.GetString("RaportPasqyraTePunuarRishtziTeLarguarTitulli", ci);
            xrLabel9.Text = rm.GetString("labelRaportiNr", ci) + ":";
            xrLabel48.Text = rm.GetString("labelDeklarimi", ci) + " E-SIG 027";
            xrLabel52.Text = "1)" + rm.GetString("labelNipti", ci) + ":";
            xrLabel70.Text = "2)" + rm.GetString("labelRaportEmriTatimpaguesit", ci) + ":";
            xrLabel71.Text = "3)" + rm.GetString("labelPeriudhaTatimoreViti", ci) + ":";
            xrLabel72.Text = "4)" + rm.GetString("labelFilterKryesorMuaji", ci) + ":";
            xrLabel81.Text = "5)" + rm.GetString("labelRaportVeprimtariaKryesore", ci) + ":";
            xrLabel83.Text = "6)" + rm.GetString("labelRaportVeprimtariaDegesNjesise", ci) + ":";
            xrLabel73.Text = "7)" + rm.GetString("labelRrethi", ci) + ":";
            xrLabel82.Text = rm.GetString("labelRaportTregti", ci) + ":";

            xrLabel23.Text = rm.GetString("labelRaportPersonatQeFillojnePune", ci);
            xrLabel25.Text = "1)" + rm.GetString("labelRaportiNr", ci);
            xrLabel49.Text = "2)" + rm.GetString("labelNumriPersonal", ci);
            xrLabel26.Text = "3)" + rm.GetString("labelEmerMbiemer", ci);
            xrLabel27.Text = "4)" + rm.GetString("labelShtetesia", ci);
            xrLabel28.Text = "5)" + rm.GetString("labelRaportiDetyraFunksioniProfesioni", ci);
            xrLabel29.Text = "6)" + rm.GetString("labelNumriKatTePunonjesvePerKontributet", ci);
            xrLabel30.Text = "7)" + rm.GetString("labelRaportDataFillimitPunes", ci);

            xrLabel24.Text = rm.GetString("labelRaportPersonatLarguarNgaPuna", ci);
            xrLabel31.Text = "1)" + rm.GetString("labelRaportiNr", ci);
            xrLabel32.Text = "2)" + rm.GetString("labelNumriPersonal", ci);
            xrLabel33.Text = "3)" + rm.GetString("labelEmerMbiemer", ci);
            xrLabel34.Text = "4)" + rm.GetString("labelShtetesia", ci);
            xrLabel35.Text = "5)" + rm.GetString("labelRaportiDetyraFunksioniProfesioni", ci);
            xrLabel36.Text = "6)" + rm.GetString("labelNumriKatTePunonjesvePerKontributet", ci);
            xrLabel53.Text = "7)" + rm.GetString("labelRaportDataLargimitPuna", ci);

            xrLabel2.Text = rm.GetString("labelRaportDeklarim", ci) + ":";
            xrLabel3.Text = "19)" + rm.GetString("labelRaportDukeFilluarNgaData", ci);
            xrLabel4.Text = rm.GetString("labelRaportPunonjesPaguajneKontribute", ci) + ":";
            xrLabel8.Text = rm.GetString("labelRaportPerfaqPersonitJuridik", ci);
            xrLabel1.Text = rm.GetString("labelDataEKonfirmimit", ci) + ":";
            xrLabel5.Text = rm.GetString("labelData_e_printimit", ci);


        }

        private void Rap_PunonjesTePunesuarRishtaziDheLarguar_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);
            Rrethi.Text = parametraRaporti.NdermarrjeQytetiPershkrimi;
            PeriudhaViti.Text = rm.GetString("labelViti", ci) + parametraRaporti.KodiViti;
            NdermarjaEmri.Text = parametraRaporti.NdermarrjeKodi;
        }
    }
}
