using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatShitje_RedisProforma : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatShitje_RedisProforma(){InitializeComponent();} 
public Rap_FormatShitje_RedisProforma(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):this(param.Ci,param.IdNdermarrje, param.IdPerdoruesi){}
        public Rap_FormatShitje_RedisProforma(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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


            xrLabel14.Text = rm.GetString("lblTelephone", ci);
            xrLabel12.Text = rm.GetString("lblNipt", ci);
            xrLabel47.Text = rm.GetString("lblNipt", ci);
            xrLabel33.Text = rm.GetString("lblEmail", ci);
            xrLabel8.Text = rm.GetString("RaportlblCustomer", ci);
            xrLabel13.Text = rm.GetString("lblRaportAddress", ci);
            xrLabel2.Text = rm.GetString("lblRaportDate", ci);
            xrLabel5.Text = rm.GetString("lblQuatation", ci);
            xrLabel24.Text = rm.GetString("labelRaportTotal", ci);
            xrLabel10.Text = rm.GetString("labelRaportTotal", ci);
            xrLabel11.Text = rm.GetString("lblTotalNet", ci);
            xrLabel15.Text = rm.GetString("lblTotalNet", ci);
            xrLabel34.Text = rm.GetString("lblRate", ci);
            xrLabel37.Text = rm.GetString("lblRate", ci);
            xrTableCell5.Text = rm.GetString("lblNo", ci);
            xrTableCell13.Text = rm.GetString("lblCode", ci);
            xrTableCell10.Text = rm.GetString("labelDescription", ci);
            xrTableCell8.Text = rm.GetString("labelQuantity", ci);
            xrTableCell11.Text = rm.GetString("lblRaportPrice", ci);
            xrTableCell6.Text = rm.GetString("lblRaportZbritjeTotale", ci);
            xrTableCell1.Text = rm.GetString("lblAmountWithoutVAT", ci);
            xrTableCell7.Text = rm.GetString("lblVAT", ci);
            xrTableCell4.Text = rm.GetString("lblAmountWithVAT", ci);

        }

    }
}
