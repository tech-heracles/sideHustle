using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_NdryshimiKostos_Artikujve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_NdryshimiKostos_Artikujve(){InitializeComponent();} 
   
        public Rap_NdryshimiKostos_Artikujve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_NdryshimiKostos_Artikujve(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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


            xrLabel89.Text = rm.GetString("RapNdryshimiKostosArtikuj", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);

            ////  xrLabel13.Text = rm.GetString("filterArkaBankaEmer", ci);
            xrTableCell6.Text = rm.GetString("labelKartela", ci);

            xrTableCell11.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell12.Text = rm.GetString("labelNjesia", ci);
            xrTableCell19.Text = rm.GetString("xrtableSasiPara", ci);
            xrTableCell20.Text = rm.GetString("xrtableCmimiPara", ci);
            xrTableCell21.Text = rm.GetString("xrtableHyrjeFundit", ci);
            xrTableCell22.Text = rm.GetString("xrtableKostoHyrjeFundit", ci);
            xrTableCell23.Text = rm.GetString("xrtableNdryshimiNeVlere", ci);
            xrTableCell24.Text = rm.GetString("xrtableNdryshimiNePerqindje", ci);
            xrTableCell25.Text = rm.GetString("xrtableDataFundit", ci);

            
            xrLabel69.Text = rm.GetString("labelLogoIMB", ci);
            
        }

      
    }
}
