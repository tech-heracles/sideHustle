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
    public partial class Rap_FormularPagesesMbiTeArdhuratNgaPunesimi : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormularPagesesMbiTeArdhuratNgaPunesimi(){InitializeComponent();} 

        public Rap_FormularPagesesMbiTeArdhuratNgaPunesimi(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_FormularPagesesMbiTeArdhuratNgaPunesimi(CultureInfo ci, int idNdermaje, int idViti, XtraReport report)
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
            xrLabel1.Text = rm.GetString("RaportFormularitDeklarimitPagTatimitArdhTitulli", ci);

            xrLabel2.Text = rm.GetString("labelRaportNrVendosjesDok", ci);
            xrLabel3.Text =rm.GetString("labelRaportPerdorimZyrtar", ci);
            xrLabel4.Text = "(2)" +  rm.GetString("labelRaportPeriudhaTatimore", ci);
            xrLabel5.Text = "(1)" + rm.GetString("labelFilterAvancuarNrSerial", ci) + ":";
            xrLabel9.Text = "(3)" + rm.GetString("labelRaportNrIdentifikuesPersonitTatueshem", ci) + ":";
            xrLabel10.Text = "(4)" + rm.GetString("labelRaportEmriTregtarPersonitTat", ci) + ":";
            xrLabel11.Text = "(5)" + rm.GetString("labelRaportEmerMbiemerPersFizik", ci) + ":";
            xrLabel12.Text = "(6)" + rm.GetString("labelRaportAdresa", ci) + ":";
            xrLabel13.Text = "(7)" + rm.GetString("labelRaportNrTel", ci) + ":";
            xrCheckBox1.Text = rm.GetString("labelPaVeprimtari", ci);
            xrLabel8.Text = "8)" + rm.GetString("labelRaportDeklarimPermbledhes", ci);
            xrTableCell1.Text = "9)" + rm.GetString("labelRaportTeArdhuratTatueshme", ci);
            xrTableCell4.Text = "10)" + rm.GetString("labelRaportTatimiPerTuPaguar", ci);
            xrTableCell7.Text = "11)" + rm.GetString("labelRaportNrPersonaMbajturTatimi", ci);
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
          
        }

        private void Rap_FormularPagesesMbiTeArdhuratNgaPunesimi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
