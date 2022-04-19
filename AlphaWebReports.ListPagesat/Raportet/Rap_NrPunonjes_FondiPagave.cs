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
    public partial class Rap_NrPunonjes_FondiPagave : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_NrPunonjes_FondiPagave(){InitializeComponent();} 
        private CultureInfo ci;
        private ResourceManager rm = new ResourceManager("Resources.Strings",
                          System.Reflection.Assembly.Load("App_GlobalResources"));
        int cnt = 0;
        //int nrmespunonjesish = 0;
        //int nrpunonjeslarg = 0;
        public Rap_NrPunonjes_FondiPagave(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report)
        {

        }
        public Rap_NrPunonjes_FondiPagave(CultureInfo ci, int idNdermarrje, XtraReport report)
        {           
            this.ci = ci;
            InitializeComponent();
            EmrateLabelave(ci);
          
        }

      

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>

        private void EmrateLabelave(CultureInfo ci)
        {

            xrLabel47.Text = rm.GetString("RaportNumriPunonjsveFondPagaveTitulli", ci);
            xrLabel25.Text = rm.GetString("labelRaportiNr", ci);
            xrLabel26.Text = rm.GetString("labelRaportKATEGORITE", ci);
            xrLabel27.Text = rm.GetString("labelRaportNrMesPunonjes", ci);
            xrLabel23.Text = rm.GetString("labelRaportNrPunonjesNdryshuar", ci);
            xrLabel28.Text = rm.GetString("labelRaportPranuarRinj", ci);
            xrLabel29.Text = rm.GetString("labelRaportlarguar", ci);
            xrLabel30.Text = rm.GetString("labelRaportGjendjeFundVitiUshtrimor", ci);
            xrLabel24.Text = rm.GetString("labelRaportFondiPagaveKont", ci);
            xrLabel32.Text = rm.GetString("labelRaportFondiPagaveGjithsej", ci);
            xrLabel33.Text = rm.GetString("labelRaportShperbSuplem", ci);
            xrLabel34.Text = rm.GetString("labelRaportNdihmaMenjeher", ci);
            xrLabel35.Text = rm.GetString("labelRaportSigShendShoq", ci);
            xrLabel36.Text = rm.GetString("labelRaportShperblimeTjera", ci);
            xrLabel53.Text =  rm.GetString("labelRaportTatimiMbiArdhura", ci);
            xrTableCell13.Text = rm.GetString("labelRaportNrPunonjGjithsej", ci);
          
        }

       

        private void xrTableCell1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("NR") != null)
            {
                cnt++;
                xrTableCell1.Text = cnt.ToString();

            }
        }


       
    }
}
