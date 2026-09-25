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
    public partial class LibriBlerjesPaMerge : DevExpress.XtraReports.UI.XtraReport
    {
		public LibriBlerjesPaMerge(){InitializeComponent();} 
       

        double maxblerjeperjashtuar = 0;
        double shumatotale = 0;

        public LibriBlerjesPaMerge(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public LibriBlerjesPaMerge(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi,
            DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);

        }

        private void LibriBlerjes_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);
            xrLabel100.Text = parametraRaporti.NdermarrjePershkrimi;
            xrLabel101.Text = parametraRaporti.NdermarrjeNipt;
            xrLabel102.Text = parametraRaporti.KodiViti;
        }
       
        private void xrLabel58_SummaryReset(object sender, EventArgs e)
            {
            maxblerjeperjashtuar = 0;
            }

        private void xrLabel58_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
            {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
               shuma += double.Parse(e.CalculatedValues[i].ToString());
            e.Result = shuma;//+maxblerjeperjashtuar;
            shumatotale += shuma;// +maxblerjeperjashtuar;
            e.Handled = true;  
            }

        private void xrLabel58_SummaryRowChanged(object sender, EventArgs e)
            {
               if (Convert.ToDouble(GetCurrentColumnValue("BLERJEPERJASHTUAR")) > maxblerjeperjashtuar)
                maxblerjeperjashtuar = Convert.ToDouble(GetCurrentColumnValue("BLERJEPERJASHTUAR"));
            }

        private void xrLabel71_SummaryReset(object sender, EventArgs e)
            {
            shumatotale = 0;
            }

        private void xrLabel71_SummaryRowChanged(object sender, EventArgs e)
            {

            }

        private void xrLabel71_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
            {
            e.Result = shumatotale;
            e.Handled = true;
            }

        private void xrLabel53_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
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


        }
    }
}
