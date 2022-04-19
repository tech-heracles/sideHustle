using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Resources;
using DevExpress.XtraReports.UI;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatEksporti_Redis_1 : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_FormatEksporti_Redis_1() { InitializeComponent(); }
        public Rap_FormatEksporti_Redis_1(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatEksporti_Redis_1(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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

            xrLabel1.Text = rm.GetString("FatureRedisEksportTitull", ci);
            xrLabel53.Text = rm.GetString("lblInvoiceNo", ci);
            xrLabel54.Text = rm.GetString("lblInvoiceDate", ci);
            xrLabel55.Text = rm.GetString("lblInvoiceSerial", ci);
            xrLabel7.Text = rm.GetString("lblSoldTo", ci);
            xrLabel52.Text = rm.GetString("lblIncoTerms", ci);
            xrLabel11.Text = rm.GetString("lblDeliveredTo", ci);
            xrLabel8.Text = rm.GetString("lblCustomer", ci);
            xrLabel10.Text = rm.GetString("lblCustomer", ci);
            xrLabel13.Text = rm.GetString("lblAddress", ci);
            xrLabel5.Text = rm.GetString("lblAddress", ci);
            xrLabel6.Text = rm.GetString("labelRaportTel", ci);
            xrLabel9.Text = rm.GetString("labelRaportTel", ci);
            xrLabel46.Text = rm.GetString("labelRaportFax", ci);
            xrLabel49.Text = rm.GetString("labelRaportFax", ci);
            xrLabel47.Text = rm.GetString("lblInvoiceVatNo", ci);
            xrLabel50.Text = rm.GetString("lblInvoiceVatNo", ci);
            xrLabel12.Text = rm.GetString("lblInvoiceVatNo", ci);
            xrTableCell5.Text = rm.GetString("labelDescription", ci);
            xrTableCell9.Text = rm.GetString("lblTariffCode", ci);
            xrTableCell6.Text = rm.GetString("lblRaportPrice", ci);
            xrTableCell4.Text = rm.GetString("lblTotalValue", ci);
            label4.Text = rm.GetString("lblPaymentDetails", ci);
            label5.Text = rm.GetString("lblPaymentDetails", ci);
            label6.Text = rm.GetString("lblAuthorizedBy", ci);
            label3.Text = rm.GetString("Redis_detail1", ci);
            label2.Text = rm.GetString("Redis_detail2", ci);
        }

    }
}
