using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_PorosiDealer : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_PorosiDealer(){InitializeComponent();} 
       
        public Rap_PorosiDealer(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_PorosiDealer(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {   InitializeComponent();
        EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
            parameter6.Value = raport.Parameters[5].Value;

        }

          /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
          ResourceManager  rm = new ResourceManager("Resources.Strings",
                     System.Reflection.Assembly.Load("App_GlobalResources"));

          RaportPorosiDealer.Text = rm.GetString("RaportPorosiDealerTitulli", ci);
          FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
          DealerName.Text = rm.GetString("labelDealerName", ci);
          ShopName.Text = rm.GetString("labelShopName", ci);
          ShopCode.Text = rm.GetString("labelShopCode", ci);
          Produkti.Text = rm.GetString("labelRaportProdukti", ci);
          Quantity.Text = rm.GetString("labelQuantity", ci);
          Date.Text = rm.GetString("labelDate", ci);
          labelLogo.Text = rm.GetString("labelLogoIMB", ci);
          
        }

            
     
    }
}
