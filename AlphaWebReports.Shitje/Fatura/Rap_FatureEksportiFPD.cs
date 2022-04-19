using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureEksportiFPD : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureEksportiFPD(){InitializeComponent();} 

        
        public Rap_FatureEksportiFPD(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureEksportiFPD(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            if (ci.Name == "sq-AL")     //pershkrimet duhet te dalin gjithmon ne anglisht
            {
                ci = new CultureInfo("en-US");
            }
            EmrateLabelave(ci);
        }

        private void EmrateLabelave(CultureInfo ci) 
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
           
   
            xrLabel6.Text = rm.GetString("labelNriSerise", ci);

            xrLabel72.Text = rm.GetString("Galina_TiranaBank", ci);

            xrLabel10.Text = rm.GetString("lblskonto", ci);
            xrLabel12.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
           // xrLabel15.Text = rm.GetString("lblMenyraPageses", ci) + ":";
            xrLabel15.Text = rm.GetString("lblMenyraPagese", ci) + ":";
            xrLabel17.Text = rm.GetString("lblRaportiDifferentNotes", ci) + ":";
            xrLabel18.Text = rm.GetString("lblInvoiceDate", ci);
            xrLabel19.Text = rm.GetString("labelInvoiceNumber", ci);
            xrLabel20.Text = rm.GetString("ReportToolbarLabelPage", ci);
            xrLabel60.Text = rm.GetString("lblRaportiBillTo", ci);
            xrLabel83.Text = rm.GetString("labelTransport", ci);
            xrLabel53.Text = "1) " + rm.GetString("lblRaportiBleresiBuyer", ci);
            xrLabel52.Text = "2) " + rm.GetString("lblKompaniaTransportit", ci);
            xrLabel51.Text = "3) " + rm.GetString("lblShitesi", ci);
            xrLabel86.Text = rm.GetString("lblRaportiNumberPlate", ci);
            xrLabel57.Text = rm.GetString("label_Emri", ci);
            xrLabel27.Text = rm.GetString("lblRaportAddress", ci);
            xrLabel29.Text = rm.GetString("labelRaportTel", ci) +". "+ rm.GetString("lblNo", ci) + ":";
            xrLabel31.Text = rm.GetString("lblInvoiceVatNo", ci) + ":";

            xrLabel33.Text = rm.GetString("lblKompaniaTransportit", ci);
            xrLabel76.Text = rm.GetString("lblInvoiceVatNo", ci) + ":";
            xrLabel80.Text = rm.GetString("lblRaportiBoxNO", ci) + ":";
            xrLabel78.Text = rm.GetString("lblRaportiNetWeight", ci) + ":";
            xrLabel22.Text = rm.GetString("lblRaportiGrossWeight", ci) + ":";

            xrLabel84.Text = rm.GetString("lblRaportiTransportDate", ci);
            xrTableCell18.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell2.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell3.Text = rm.GetString("lblNjesiaa", ci);
            xrTableCell4.Text = rm.GetString("lblRaportSasia", ci);
            xrTableCell5.Text = rm.GetString("labelRaportiCmimet", ci)+" (Euro)";
            xrTableCell6.Text = rm.GetString("labelRaportiShuma", ci);
            xrTableCell7.Text = xrTableCell30.Text = rm.GetString("lblVAT", ci);
            xrTableCell8.Text = xrTableCell31.Text = rm.GetString("lblVleraTotale", ci);
            xrTableCell17.Text = rm.GetString("lblDiscauntForReplacementProducts", ci);
            xrTableCell36.Text = rm.GetString("lblRaportiAmountToBePaid", ci)+" Eur";
            xrTableCell40.Text = rm.GetString("labelRaportiTotali", ci) + " on EURO";
            xrTableCell35.Text = rm.GetString("labelRaportiTotali", ci) + " on Lek";
            xrTableCell45.Text = rm.GetString("lblFurnizimeTÎTatueshme1", ci);
            xrTableCell50.Text = rm.GetString("lblFurnizimeTÎPaTatueshme1", ci);
            xrLabel55.Text = xrLabel54.Text = rm.GetString("lblRaportEmerMbiemerNenshkrimiVule", ci);
            xrLabel50.Text = rm.GetString("lblRaportEmerMbiemerNenshkrimiVule", ci);
            xrLabel55.Text = xrLabel54.Text = rm.GetString("lblRaportEmerMbiemerNenshkrimiVule1", ci);
            xrLabel50.Text = rm.GetString("lblRaportEmerMbiemerNenshkrimiVule1", ci);
            xrLabel49.Text = rm.GetString("labelRaportLlogariBankare", ci);
            xrLabel69.Text = rm.GetString("lblSocieteGeneraleAlbaniaUPPER", ci);
            xrLabel71.Text = rm.GetString("lblIBANfpd", ci);
            xrLabel72.Text = rm.GetString("lblSWIFTCODEFPD", ci);
            xrTableCell1.Text = rm.GetString("lblRaportiExchange", ci);
            xrTableCell29.Text = rm.GetString("labelRaportiVleraTotale", ci);
        }
    }
}
