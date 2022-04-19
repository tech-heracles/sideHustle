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
    public partial class LibriBlerjesKosove2015 : DevExpress.XtraReports.UI.XtraReport
    {
		public LibriBlerjesKosove2015(){InitializeComponent();} 
       

        public LibriBlerjesKosove2015(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public LibriBlerjesKosove2015(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);


            if (raport.Parameters[9].Value != null)  
            {
               

                if (raport.Parameters["filterDegeAdministrative"].Value!="")
                {
                    xrLabel69.Text = raport.Parameters["filterDegeAdministrative"].Description + " " + raport.Parameters["filterDegeAdministrative"].Value;
                    xrLabel69.Visible = true;
                }


                if (raport.Parameters["filterDtDok"].Value != "")
                {
                    xrLabel68.Text = raport.Parameters["filterDtDok"].Description + " " + raport.Parameters["filterDtDok"].Value;
                    xrLabel68.Visible = true;
                }
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
            xrLabel20.Text = rm.GetString("labelImporte", ci) + " 18%";
            xrLabel22.Text = rm.GetString("labelNgaFurnitorvendasmeshkalle8", ci);
            xrLabel25.Text = rm.GetString("labelVleraTatueshme", ci);
            xrLabel26.Text = rm.GetString("labelTVSH", ci);
            xrLabel29.Text = rm.GetString("labelVleraTatueshme", ci);
            xrLabel30.Text = rm.GetString("labelTVSH", ci);
            xrLabel45.Text = rm.GetString("labelImporte", ci) + " 8%";
            xrLabel70.Text = rm.GetString("labelShumaTotale", ci);
            xrLabel94.Text = rm.GetString("labelKutiaFormularitTeDeklarimitTePageses", ci);
            xrLabel52.Text = rm.GetString("labelNgaFurnitorvendasmeshkalle18", ci);
            xrLabel86.Text = rm.GetString("labelKutia", ci) + " (15)";
            xrLabel87.Text = rm.GetString("labelKutia", ci) + " (16)";
            xrLabel85.Text = rm.GetString("labelKutia", ci) + " (17)";
            xrLabel59.Text = rm.GetString("labelKutia", ci) + " (18)";
            xrLabel60.Text = rm.GetString("labelKutia", ci) + " (19)";
            xrLabel62.Text = rm.GetString("labelKutia", ci) + " (20)";
            xrLabel61.Text = rm.GetString("labelKutia", ci) + " (21)";
            xrLabel88.Text = rm.GetString("labelKutia", ci) + " (22)";
            xrLabel92.Text = rm.GetString("labelKutia", ci) + " (23)";
            xrLabel96.Text = rm.GetString("labelshpjegim", ci);
            xrLabel97.Text = rm.GetString("labelEmerMbiemer", ci);
            xrLabel95.Text = rm.GetString("labelShpjegimPrintimi", ci);
            xrLabel98.Text = rm.GetString("labelShtimNrRreshtash", ci);
            xrLabel31.Text = rm.GetString("labelDorezoi", ci);
            xrLabel28.Text = rm.GetString("labelPranoi", ci);
        }
    }
}
