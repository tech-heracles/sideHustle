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
    public partial class Rap_SellOut : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_SellOut(){InitializeComponent();} 
        public Rap_SellOut(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report)
        {

        }
        public Rap_SellOut(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
           // dtDokLabel.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
          //  KartelaLabel.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
           // CmimiLabel.Text = raport.Parameters[2].Description;
            parameter3.Value = raport.Parameters[2].Value;
           /// xrLabel1.Text = raport.Parameters[3].Description;
            parameter4.Value = raport.Parameters[3].Value;
         //   xrLabel3.Text = raport.Parameters[4].Description;
            parameter5.Value = raport.Parameters[4].Value;
          //  xrLabel2.Text = raport.Parameters[5].Description;
            parameter6.Value = raport.Parameters[5].Value;
           
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {

            ResourceManager rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel17.Text = rm.GetString("RaportiSellOutTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell4.Text = rm.GetString("labelStatus", ci);
            xrTableCell5.Text = rm.GetString("labelSalesdate", ci);
            xrTableCell6.Text = rm.GetString("labelDistri", ci);
            xrTableCell11.Text = rm.GetString("labelDistriName", ci);
            xrTableCell7.Text = rm.GetString("labelDealerVat", ci);
            xrTableCell9.Text = rm.GetString("labelDealerName", ci);
            xrTableCell10.Text = rm.GetString("labelDealerStreet", ci);
            xrTableCell8.Text = rm.GetString("labelDealerCity", ci);
            xrTableCell1.Text = rm.GetString("labelDealerZIP", ci);
            xrTableCell2.Text = rm.GetString("labelDealerCountry", ci);
            xrTableCell3.Text = rm.GetString("labelDealerEmail", ci);
            xrTableCell12.Text = rm.GetString("labelDealerPhone", ci);
            xrTableCell13.Text = rm.GetString("labelDealerType", ci);
            xrTableCell14.Text = rm.GetString("labelPNAcer", ci);
            xrTableCell15.Text = rm.GetString("labelDescription", ci);
            xrTableCell16.Text = rm.GetString("labelUnitsSold", ci);
            xrTableCell17.Text = rm.GetString("labelSellingPrice", ci);
            xrTableCell18.Text = rm.GetString("labelCurrencyid", ci);
            xrTableCell19.Text = rm.GetString("labelSN", ci);
            xrTableCell20.Text = rm.GetString("labelInvoiceNumber", ci);
            xrTableCell21.Text = rm.GetString("labelDealerCode", ci); 
            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);

        }

    }
}
