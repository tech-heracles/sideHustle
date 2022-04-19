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
    public partial class LibriBlerjesSunPetrolium : DevExpress.XtraReports.UI.XtraReport
    {
		public LibriBlerjesSunPetrolium(){InitializeComponent();} 

        public LibriBlerjesSunPetrolium(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public LibriBlerjesSunPetrolium(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi,
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

            xrTableCell16.Text = rm.GetString("labelPaVeprimtari", ci);
            xrTableCell18.Text = rm.GetString("labelKonfirmimTransaksioni", ci);

            xrTableCell20.Text = rm.GetString("labelFature", ci);
            xrTableCell37.Text = rm.GetString("labelNrFature", ci);
            xrTableCell38.Text = rm.GetString("labelFilterAvancuarNrSerial", ci);
            xrTableCell39.Text = rm.GetString("labelDataFormat", ci);

            xrTableCell23.Text = rm.GetString("labelShitesi", ci);
            xrTableCell40.Text = rm.GetString("labelEmerTregtarPerson", ci);
            xrTableCell41.Text = rm.GetString("labelRrethi", ci);
            xrTableCell42.Text = rm.GetString("labelNiptKodFermeri", ci);
            xrTableCell24.Text = rm.GetString("labelTotalBlerjeshMeTVSH", ci);

            xrTableCell36.Text = rm.GetString("labelBlerje", ci);
            xrTableCell44.Text = rm.GetString("labelTePerjashtuaraMeTVSH", ci);

            xrTableCell46.Text = rm.GetString("labelImporte", ci) + " 20%";
            xrTableCell48.Text = rm.GetString("labelImporte", ci) + " 10%";
            xrTableCell50.Text = rm.GetString("labelFurnitorVendas", ci) + " 20%";
            xrTableCell52.Text = rm.GetString("labelFurnitorVendas", ci) + " 10%";
            xrTableCell54.Text = rm.GetString("labelFermerVendas", ci);

            xrTableCell83.Text = rm.GetString("labelVleraTatueshme", ci);
            xrTableCell84.Text = rm.GetString("labelTVSH", ci);
            xrTableCell85.Text = rm.GetString("labelVleraTatueshme", ci);
            xrTableCell86.Text = rm.GetString("labelTVSH", ci);
            xrTableCell87.Text = rm.GetString("labelVleraTatueshme", ci);
            xrTableCell88.Text = rm.GetString("labelTVSH", ci);
            xrTableCell89.Text = rm.GetString("labelVleraTatueshme", ci);
            xrTableCell90.Text = rm.GetString("labelTVSH", ci);
            xrTableCell91.Text = rm.GetString("labelVleraTatueshme", ci);
            xrTableCell92.Text = rm.GetString("labelTVSH", ci);

            xrTableCell53.Text = rm.GetString("labelShumaTotale", ci);
            xrTableCell160.Text = rm.GetString("labelKutiaFormularitTeDeklarimitTePageses", ci);

            xrTableCell161.Text = rm.GetString("labelKutia", ci) + " (15)";
            xrTableCell162.Text = rm.GetString("labelKutia", ci) + " (16)";
            xrTableCell163.Text = rm.GetString("labelKutia", ci) + " (17)";
            xrTableCell164.Text = rm.GetString("labelKutia", ci) + " (18)";
            xrTableCell165.Text = rm.GetString("labelKutia", ci) + " (19)";
            xrTableCell166.Text = rm.GetString("labelKutia", ci) + " (20)";
            xrTableCell167.Text = rm.GetString("labelKutia", ci) + " (21)";
            xrTableCell168.Text = rm.GetString("labelKutia", ci) + " (22)";
            xrTableCell169.Text = rm.GetString("labelKutia", ci) + " (23)";
            xrTableCell170.Text = rm.GetString("labelKutia", ci) + " (24)";
            xrTableCell171.Text = rm.GetString("labelKutia", ci) + " (25)";
            xrTableCell172.Text = rm.GetString("labelKutia", ci) + " (26)";

            xrLabel96.Text = rm.GetString("labelshpjegim", ci);
            xrLabel97.Text = rm.GetString("labelEmerMbiemer", ci);
            xrLabel95.Text = rm.GetString("labelShpjegimPrintimi", ci);
            xrLabel98.Text = rm.GetString("labelShtimNrRreshtash", ci);
        }
    }
}
