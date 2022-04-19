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
    public partial class Rap_Format_Printimi_Magazina_Model7 : DevExpress.XtraReports.UI.XtraReport
    { 
		public Rap_Format_Printimi_Magazina_Model7(){InitializeComponent();} 
   
        public Rap_Format_Printimi_Magazina_Model7(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {
        }
        public Rap_Format_Printimi_Magazina_Model7(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }     
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel1.Text = rm.GetString("lblRaportFirma", ci);
            xrLabel3.Text = rm.GetString("lblRaportAdresa", ci);
            xrLabel16.Text = rm.GetString("lblRaportTo", ci);
            xrLabel7.Text = rm.GetString("lblRaportDate", ci);
            xrLabel6.Text = rm.GetString("lblRaportInvoice", ci);
            xrLabel18.Text = rm.GetString("lblRaportSite", ci);
            xrTableCell7.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell4.Text = rm.GetString("labelKartela", ci);
            xrTableCell5.Text = rm.GetString("labelRaportiEmertimi", ci);
            xrTableCell15.Text = rm.GetString("lblRaportSeriale", ci);
            xrTableCell6.Text = rm.GetString("labelNjesia", ci);
            xrTableCell1.Text = rm.GetString("labelSasia", ci);
            xrTableCell2.Text = rm.GetString("labelCmimi", ci);
            xrTableCell3.Text = rm.GetString("labelRaportVlefta", ci);
            xrLabel5.Text = rm.GetString("lblRaportTotali", ci);
            xrLabel12.Text = rm.GetString("lblRaportSignatureWh", ci);
            xrLabel23.Text = rm.GetString("lblRaportFirmaMagazinierit", ci);
            xrLabel24.Text = rm.GetString("lblRaportReceiverSingature", ci);
            xrLabel25.Text = rm.GetString("lblRaportNenshkrimiMarresit", ci);
            xrLabel28.Text = rm.GetString("lblRaportNoTrack", ci);
            xrLabel29.Text = rm.GetString("lblRaportTargaMakines", ci);
            xrLabel14.Text = rm.GetString("lblRaportPrintuarAlphaWeb", ci);
        }
    }
}
