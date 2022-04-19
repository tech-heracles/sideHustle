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
    public partial class LibriBlerjes2015_Raportuese : DevExpress.XtraReports.UI.XtraReport
    {
       
        double maxblerjeperjashtuar = 0;
        double shumatotale = 0;
        
        public LibriBlerjes2015_Raportuese()         
        {
                 InitializeComponent();

        }
        public LibriBlerjes2015_Raportuese(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {
            

        }
        public LibriBlerjes2015_Raportuese(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi,
            DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);


            // Modifikimi i fushes Muaji
            string data = raport.Parameters["filterDtDok"].Value.ToString();
            string dtFillimi = data.Split('-')[0];   
            string muaji = dtFillimi.Split('/')[1];
            xrLabel103.Text = muaji.TrimStart('0');

        }
        


        //private void xrLabel53_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
            
        //    if (enabled && GetCurrentColumnValue("IDSHITJEKOKA") != null && GetCurrentColumnValue("NRDOK") != null)
        //    {
        //        xrLabel53.NavigateUrl = "javascript:window.parent. myFaqeCelje.kontrolloTeDrejta('Shto_RegjistrimDokumentash.aspx?shitje_blerje=blerje&id=" + GetCurrentColumnValue("IDSHITJEKOKA").ToString() + "&numer=" + GetCurrentColumnValue("NRDOK").ToString() + "&shtim_modifikim=modifikim')";
        //    }                        
        //}
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
           ResourceManager rm = new ResourceManager("Resources.Strings",
                     System.Reflection.Assembly.Load("App_GlobalResources"));
           
            //xrLabel1.Text = rm.GetString("RaportLibriBlerjeveTitulli", ci);
            //xrLabel2.Text = rm.GetString("labelShoqeria", ci);
            //xrLabel3.Text = rm.GetString("labelNipti", ci);
            //xrLabel4.Text = rm.GetString("labelViti", ci);
            //xrLabel5.Text = rm.GetString("labelFilterKryesorMuaji", ci);
            //xrLabel6.Text = rm.GetString("labelPaVeprimtari", ci);
            //xrLabel8.Text = rm.GetString("labelKonfirmimTransaksioni", ci);
            //xrLabel9.Text = rm.GetString("labelFature", ci);
            //xrLabel10.Text = rm.GetString("labelNrFature", ci);
            //xrLabel11.Text = rm.GetString("labelFilterAvancuarNrSerial", ci);
            //xrLabel12.Text = rm.GetString("labelDataFormat", ci);
            //xrLabel13.Text = rm.GetString("labelShitesi", ci);
            //xrLabel14.Text = rm.GetString("labelEmerTregtarPerson", ci);
            //xrLabel15.Text = rm.GetString("labelRrethi", ci);
            //xrLabel16.Text = rm.GetString("labelNiptKodFermeri", ci);
            //xrLabel17.Text = rm.GetString("labelTotalBlerjeshMeTVSH", ci);
            //xrLabel18.Text = rm.GetString("labelBlerje", ci);
            //xrLabel19.Text = rm.GetString("labelTePerjashtuaraMeTVSH", ci);

            //xrLabel20.Text = rm.GetString("labelImporte", ci) + " 20%";
            //xrLabel21.Text = rm.GetString("labelImporte", ci) + " 10%";
            //xrLabel22.Text = rm.GetString("labelFurnitorVendas", ci) + " 20%";
            //xrLabel23.Text = rm.GetString("labelFurnitorVendas", ci) + " 10%";
            //xrLabel24.Text = rm.GetString("labelFermerVendas", ci);
            //xrLabel25.Text = rm.GetString("labelVleraTatueshme", ci);
            //xrLabel26.Text = rm.GetString("labelTVSH", ci);
            //xrLabel27.Text = rm.GetString("labelVleraTatueshme", ci);
            //xrLabel28.Text = rm.GetString("labelTVSH", ci);
            //xrLabel29.Text = rm.GetString("labelVleraTatueshme", ci);
            //xrLabel30.Text = rm.GetString("labelTVSH", ci);
            //xrLabel31.Text = rm.GetString("labelVleraTatueshme", ci);
            //xrLabel32.Text = rm.GetString("labelTVSH", ci);
            //xrLabel33.Text = rm.GetString("labelVleraTatueshme", ci);
            //xrLabel34.Text = rm.GetString("labelTVSH", ci);
            //xrLabel70.Text = rm.GetString("labelShumaTotale", ci);
            //xrLabel94.Text = rm.GetString("labelKutiaFormularitTeDeklarimitTePageses", ci);

            //xrLabel86.Text = rm.GetString("labelKutia", ci) + " (15)";
            //xrLabel87.Text = rm.GetString("labelKutia", ci) + " (16)";
            //xrLabel85.Text = rm.GetString("labelKutia", ci) + " (17)";
            //xrLabel83.Text = rm.GetString("labelKutia", ci) + " (18)";
            //xrLabel84.Text = rm.GetString("labelKutia", ci) + " (19)";
            //xrLabel88.Text = rm.GetString("labelKutia", ci) + " (20)";
            //xrLabel92.Text = rm.GetString("labelKutia", ci) + " (21)";
            //xrLabel93.Text = rm.GetString("labelKutia", ci) + " (22)";
            //xrLabel91.Text = rm.GetString("labelKutia", ci) + " (23)";
            //xrLabel89.Text = rm.GetString("labelKutia", ci) + " (24)";
            //xrLabel90.Text = rm.GetString("labelKutia", ci) + " (25)";
        
            //xrLabel96.Text = rm.GetString("labelshpjegim", ci);
            //xrLabel97.Text = rm.GetString("labelEmerMbiemer", ci);
            //xrLabel95.Text = rm.GetString("labelShpjegimPrintimi", ci);
            //xrLabel98.Text = rm.GetString("labelShtimNrRreshtash", ci);
            //xrLabel15.Text = rm.GetString("labelRrethi", ci);
            //xrLabel16.Text = rm.GetString("labelNiptKodFermeri", ci);
            //xrLabel17.Text = rm.GetString("labelTotalBlerjeshMeTVSH", ci);
            //xrLabel18.Text = rm.GetString("labelBlerje", ci);
            //xrLabel19.Text = rm.GetString("labelTePerjashtuaraMeTVSH", ci);
        }
    }
}
