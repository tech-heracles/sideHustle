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
    public partial class Rap_VeprimeTeAnulluaraVodafone : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_VeprimeTeAnulluaraVodafone(){InitializeComponent();} 
       
 

        public Rap_VeprimeTeAnulluaraVodafone(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_VeprimeTeAnulluaraVodafone(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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
          ResourceManager  rm = new ResourceManager("Resources.Strings",
                     System.Reflection.Assembly.Load("App_GlobalResources"));

          xrLabel35.Text = rm.GetString("RaportVeprimeTeAnulluaraTitulli", ci);
          FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell7.Text = rm.GetString("labelDealeri", ci);
            xrTableCell8.Text = rm.GetString("labelDyqani", ci);
            xrTableCell9.Text = rm.GetString("labelVeprimi", ci);
            xrTableCell10.Text = rm.GetString("labelRaportData", ci);
            xrTableCell11.Text = rm.GetString("labelPerfaqesuesi_i_shitjes", ci);
            xrTableCell12.Text = rm.GetString("labelKategoriaProduktit", ci);
            xrTableCell13.Text = rm.GetString("labelNenkategoria", ci);
            xrTableCell15.Text = rm.GetString("labelRaportProdukti", ci);
            xrTableCell14.Text = rm.GetString("labelSasia", ci);
          xrLabel36.Text = rm.GetString("labelLogoIMB", ci);
        }

    }
}
