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
    public partial class LibriBlerjes : DevExpress.XtraReports.UI.XtraReport
    {
		public LibriBlerjes(){InitializeComponent();} 

        public LibriBlerjes(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }

        public LibriBlerjes(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi,
            DevExpress.XtraReports.UI.XtraReport raport)
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

            xrTableCell1.Text = rm.GetString("RaportLibriBlerjeveTitulli", ci);
            xrTableCell4.Text = rm.GetString("labelShoqeria", ci);
            xrTableCell7.Text = rm.GetString("labelNipti", ci);
            xrTableCell10.Text = rm.GetString("labelViti", ci);
            xrTableCell13.Text = rm.GetString("labelFilterKryesorMuaji", ci);
            xrTableCell2.Text = rm.GetString("labelPaVeprimtari", ci);
            xrTableCell17.Text = rm.GetString("labelKonfirmimTransaksioni", ci);
            xrTableCell20.Text = rm.GetString("labelFature", ci);
            xrTableCell36.Text = rm.GetString("labelNrFature", ci);
            xrTableCell37.Text = rm.GetString("labelFilterAvancuarNrSerial", ci);
            xrTableCell38.Text = rm.GetString("labelDataFormat", ci);
            xrTableCell23.Text = rm.GetString("labelShitesi", ci);
            xrTableCell39.Text = rm.GetString("labelEmerTregtarPerson", ci);
            xrTableCell40.Text = rm.GetString("labelRrethi", ci);
            xrTableCell41.Text = rm.GetString("labelNiptKodFermeri", ci);
            xrTableCell24.Text = rm.GetString("labelTotalBlerjeshMeTVSH", ci);
            xrTableCell35.Text = rm.GetString("labelBlerje", ci);
            xrTableCell43.Text = rm.GetString("labelTePerjashtuaraMeTVSH", ci);

            xrTableCell45.Text = rm.GetString("labelImporte", ci) + " 20%";
            xrTableCell47.Text = rm.GetString("labelImporte", ci) + " 10%";
            xrTableCell49.Text = rm.GetString("labelFurnitorVendas", ci) + " 20%";
            xrTableCell51.Text = rm.GetString("labelFurnitorVendas", ci) + " 10%";
            xrTableCell53.Text = rm.GetString("labelFermerVendas", ci);
            xrTableCell80.Text = rm.GetString("labelVleraTatueshme", ci);
            xrTableCell81.Text = rm.GetString("labelTVSH", ci);
            xrTableCell82.Text = rm.GetString("labelVleraTatueshme", ci);
            xrTableCell83.Text = rm.GetString("labelTVSH", ci);
            xrTableCell84.Text = rm.GetString("labelVleraTatueshme", ci);
            xrTableCell85.Text = rm.GetString("labelTVSH", ci);
            xrTableCell86.Text = rm.GetString("labelVleraTatueshme", ci);
            xrTableCell87.Text = rm.GetString("labelTVSH", ci);
            xrTableCell88.Text = rm.GetString("labelVleraTatueshme", ci);
            xrTableCell89.Text = rm.GetString("labelTVSH", ci);
            xrLabel70.Text = rm.GetString("labelShumaTotale", ci);
            xrLabel94.Text = rm.GetString("labelKutiaFormularitTeDeklarimitTePageses", ci);

            xrTableCell153.Text = rm.GetString("labelKutia", ci) + " (15)";
            xrTableCell154.Text = rm.GetString("labelKutia", ci) + " (16)";
            xrTableCell155.Text = rm.GetString("labelKutia", ci) + " (17)";
            xrTableCell156.Text = rm.GetString("labelKutia", ci) + " (18)";
            xrTableCell157.Text = rm.GetString("labelKutia", ci) + " (19)";
            xrTableCell158.Text = rm.GetString("labelKutia", ci) + " (20)";
            xrTableCell159.Text = rm.GetString("labelKutia", ci) + " (21)";
            xrTableCell160.Text = rm.GetString("labelKutia", ci) + " (22)";
            xrTableCell161.Text = rm.GetString("labelKutia", ci) + " (23)";
            xrTableCell162.Text = rm.GetString("labelKutia", ci) + " (24)";
            xrTableCell163.Text = rm.GetString("labelKutia", ci) + " (25)";
        
            xrLabel96.Text = rm.GetString("labelshpjegim", ci);
            xrLabel97.Text = rm.GetString("labelEmerMbiemer", ci);
            xrLabel95.Text = rm.GetString("labelShpjegimPrintimi", ci);
            xrLabel98.Text = rm.GetString("labelShtimNrRreshtash", ci);
        }
    }
}
