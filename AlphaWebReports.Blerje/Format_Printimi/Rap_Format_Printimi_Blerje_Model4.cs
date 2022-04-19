using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.Magazina.Format_Printimi
{
    public partial class Rap_Format_Printimi_Blerje_Model4 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Format_Printimi_Blerje_Model4(){InitializeComponent();}      
        public Rap_Format_Printimi_Blerje_Model4(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_Format_Printimi_Blerje_Model4(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
           
        }
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel2.Text = rm.GetString("lblRaportPurchaseInvoice", ci);
            xrLabel3.Text = rm.GetString("lblRaportAddress", ci);
            xrLabel16.Text = rm.GetString("lblRaportSupplier", ci);
            xrLabel18.Text = rm.GetString("lblRaportCurrency", ci);
            xrLabel1.Text = rm.GetString("lblRaportDelivered", ci);
            xrLabel7.Text = rm.GetString("lblRaportDate", ci);
            xrLabel6.Text = rm.GetString("lblRaportInvoiceNo", ci);
            xrLabel14.Text = rm.GetString("lblRaportPONo", ci);
            xrTableCell7.Text = rm.GetString("lblRaportNo", ci);
            xrTableCell4.Text = rm.GetString("lblRaportItemCode", ci);
            xrTableCell5.Text = rm.GetString("labelDescription", ci);
            xrTableCell6.Text = rm.GetString("lblRaportEA", ci);
            xrTableCell1.Text = rm.GetString("lblRaportQtu", ci);
            xrTableCell2.Text = rm.GetString("lblRaportPrice", ci);
            xrTableCell3.Text = rm.GetString("lblRaportValue", ci);
            xrLabel5.Text = rm.GetString("labelRaportTotal", ci);
            xrLabel12.Text = rm.GetString("lblRaportSignatureWh", ci);
            xrLabel23.Text = rm.GetString("lblRaportFirmaMagazinierit", ci);
            xrLabel24.Text = rm.GetString("lblRaportReceiverSingature", ci);
            xrLabel25.Text = rm.GetString("lblRaportNenshkrimiMarresit", ci);
            xrLabel28.Text = rm.GetString("lblRaportNoTrack", ci);
            xrLabel29.Text = rm.GetString("lblRaportTargaMakines", ci);
            xrLabel32.Text = rm.GetString("lblRaportPrintuarAlphaWeb", ci);
        }
    }
}
