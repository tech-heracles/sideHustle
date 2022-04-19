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
    public partial class Rap_MagazinaHyrjeVodafone : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_MagazinaHyrjeVodafone(){InitializeComponent();} 


        public Rap_MagazinaHyrjeVodafone(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }

        public Rap_MagazinaHyrjeVodafone(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {   InitializeComponent();
            EmrateLabelave(ci);
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
            parameter6.Value = raport.Parameters[5].Value;
            parameterIdNderm.Value = idNdermarrje;
        }


          /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
          ResourceManager  rm = new ResourceManager("Resources.Strings",
                     System.Reflection.Assembly.Load("App_GlobalResources"));

          xrLabel35.Text = rm.GetString("RaportiHyrjeVodafoneTitulli", ci);
          FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
          xrLabel2.Text = rm.GetString("labelDyqani", ci);
          xrLabel3.Text = rm.GetString("lblAdresaDyqanit", ci);
          xrTableCell10.Text = rm.GetString("labelVeprimi", ci);
            xrTableCell12.Text = rm.GetString("labelDataVeprimit", ci);
            xrTableCell8.Text = rm.GetString("labelPerfaqesuesi_i_shitjes", ci);
            xrTableCell7.Text = rm.GetString("labelKategoriaProduktit", ci);
            xrTableCell9.Text = rm.GetString("labelNenkategoria", ci);
            xrTableCell11.Text = rm.GetString("labelRaportProdukti", ci);
            xrTableCell14.Text = rm.GetString("labelSasia", ci);
            xrTableCell13.Text = rm.GetString("labelCmimiDealer", ci);
            xrTableCell15.Text = rm.GetString("labelVlefta", ci);
          xrLabel36.Text = rm.GetString("labelLogoIMB", ci);
        }
        
    }
}
