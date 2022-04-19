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
    public partial class Rap_FormularSigShoqShend : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormularSigShoqShend(){InitializeComponent();} 

        public Rap_FormularSigShoqShend(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_FormularSigShoqShend(CultureInfo ci, int idNdermaje, int idViti, XtraReport report)
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
            xrLabel1.Text = rm.GetString("RaportFormulariDeklarimitSigShoqShendTitulli", ci);

            xrLabel2.Text = rm.GetString("labelRaportNrVendosjesDok", ci);
            xrLabel3.Text =rm.GetString("labelRaportPerdorimZyrtar", ci);
            xrLabel4.Text = "(2)" +  rm.GetString("labelRaportPeriudhaTatimore", ci);
           // xrLabel5.Text = "(1)" + rm.GetString("labelFilterAvancuarNrSerial", ci) + ":";
            xrLabel9.Text = rm.GetString("labelRaportNrIdentifikuesPersonitTatueshem", ci) + ":";
            xrLabel10.Text = rm.GetString("labelRaportEmriTregtarPersonitTat", ci) + ":";
            xrLabel11.Text = rm.GetString("labelRaportEmerMbiemerPersFizik", ci) + ":";
            xrLabel12.Text = rm.GetString("labelRaportAdresa", ci) + ":";
            xrLabel13.Text =  rm.GetString("labelRaportNrTel", ci) + ":";
            xrCheckBox1.Text = rm.GetString("labelPaVeprimtari", ci);
            xrLabel8.Text = rm.GetString("labelRaportDeklarimPermbledhes", ci);
            xrTableCell1.Text = rm.GetString("labelRaportTotaliPagavePagKontributet", ci);
            xrTableCell4.Text = rm.GetString("labelRaportKontributeNgaPunedhenesi", ci);
            xrTableCell7.Text = rm.GetString("labelRaportKontributeNgaPunemarresi", ci);
            xrTableCell17.Text = rm.GetString("labelRaportKontributetShteseSigShoqSuplement", ci);
            xrTableCell20.Text =  rm.GetString("labelRaportKontributSigShoqGjithsej", ci) + " (10+11+12)";
            xrTableCell23.Text =  rm.GetString("labelRaportKontributSigShend", ci);
            xrTableCell26.Text =  rm.GetString("labelRaportTotaliPerTuPaguar", ci) + " (13+14)";
            xrTableCell32.Text = rm.GetString("labelRaportNrGjithsejPersPaguajneKontributet", ci);
            xrTableCell29.Text =  rm.GetString("labelRaportNrGjithsejPersPaguajneKontributetPagMin", ci);
            xrTableCell10.Text = "(1)"+ rm.GetString("labelFilterAvancuarNrSerial", ci) + ":";
            
            xrLabel18.Text = rm.GetString("labelRaportDataFirmaPersonitTatueshem", ci);
            xrLabel19.Text = rm.GetString("labelRaportDeklarojInfoPlote", ci);
            xrLabel20.Text = rm.GetString("labelRaportPAGESA", ci);
            xrLabel21.Text = rm.GetString("labelRaportPerdorimZyrtar", ci);
            xrLabel22.Text = rm.GetString("labelRaportShumaEPaguar", ci) + ":";
            xrCheckBox2.Text = rm.GetString("checkboxRaportLeke", ci);
            xrCheckBox3.Text = rm.GetString("checkboxRaportXhirim", ci);
            xrCheckBox4.Text =  rm.GetString("checkboxRaportCek", ci);
            xrCheckBox5.Text = rm.GetString("labelRaportTeTjera", ci);
            xrLabel24.Text = rm.GetString("labelRaportDataVulaBankes", ci);
            xrLabel25.Text = rm.GetString("labelRaportOrigjinaliKopja", ci);
            xrLabel30.Text = rm.GetString("labelRaportInfIPloteOseNdryshuar", ci);
          
        }

        private void Rap_FormularSigShoqShend_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);
            var parameters = AlphaWebReports.raporteUtil.DeserializoParametrat(this.Extensions["filtrat"]);
            var filterMuaj = parameters.Find(param => param.Emri == "filterMuaji");
            PeriudhaTatimore.Text = filterMuaj.Vlera.ToString() + " " + parametraRaporti.KodiViti;
            Nipti.Text = parametraRaporti.NdermarrjeNipt;
            EmriTregtar.Text = parametraRaporti.NdermarrjeKodi;
            Adresa.Text = parametraRaporti.NdermarrjeVendi;
            NrTel.Text = parametraRaporti.NdermarrjeTel;

        }
    }
}
