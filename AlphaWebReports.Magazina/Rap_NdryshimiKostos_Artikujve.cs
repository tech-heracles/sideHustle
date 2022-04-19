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


           // xrLabel17.Text = rm.GetString("lblRapHyrjetKosto", ci);
            xrLabel89.Text = rm.GetString("RapNdryshimiKostosArtikuj", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);

            //  xrLabel5.Text = rm.GetString("labelKartela", ci) + ":";
            //   xrLabel11.Text = rm.GetString("labelRaportiPershkrimi", ci) + ":";
            //  xrLabel7.Text = rm.GetString("filterKodbari", ci);
            ////  xrLabel13.Text = rm.GetString("filterArkaBankaEmer", ci);
            // xrLabel9.Text = rm.GetString("labelRaportMetodaKostos", ci);
            //  xrLabel15.Text = rm.GetString("labelFilterAvancuarGrupi", ci) + ":";
            xrTableCell6.Text = rm.GetString("labelKartela", ci);

         //   xrLabel19.Text = rm.GetString("labelRaportiNrDok", ci);
            xrTableCell11.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell12.Text = rm.GetString("labelNjesia", ci);
            //xrLabel5.Text = rm.GetString("labelRaportSasia", ci);
            //xrLabel6.Text = rm.GetString("labelCmimi", ci);
           // xrLabel7.Text = rm.GetString("cmbCmimeArtikulliVlere", ci);
            //xrLabel9.Text = rm.GetString("labelRaportSasia", ci);
          //  xrLabel10.Text = rm.GetString("cmbCmimeArtikulliKosto", ci);
            //xrLabel11.Text = rm.GetString("cmbCmimeArtikulliVlere", ci);
            xrTableCell19.Text = rm.GetString("xrtableSasiPara", ci);
            xrTableCell20.Text = rm.GetString("xrtableCmimiPara", ci);
            xrTableCell21.Text = rm.GetString("xrtableHyrjeFundit", ci);
            xrTableCell22.Text = rm.GetString("xrtableKostoHyrjeFundit", ci);
            xrTableCell23.Text = rm.GetString("xrtableNdryshimiNeVlere", ci);
            xrTableCell24.Text = rm.GetString("xrtableNdryshimiNePerqindje", ci);
            xrTableCell25.Text = rm.GetString("xrtableDataFundit", ci);

            
            //xrLabel21.Text = rm.GetString("labelCmimi", ci);
            //xrLabel20.Text = rm.GetString("labelRaportVleraHyrje", ci);
            //xrLabel26.Text = rm.GetString("labelRaportDalje", ci);
            //xrLabel25.Text = rm.GetString("labelCmimi", ci);
            //xrLabel27.Text = rm.GetString("labelRaportVleraDalje", ci);
            //xrLabel28.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
            //xrLabel34.Text = rm.GetString("labelRaportVlera", ci);
            xrLabel69.Text = rm.GetString("labelLogoIMB", ci);
            
        }

      

       
    }
}
