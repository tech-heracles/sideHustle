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
        

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
           ResourceManager rm = new ResourceManager("Resources.Strings",
                     System.Reflection.Assembly.Load("App_GlobalResources"));
           

        }
    }
}
