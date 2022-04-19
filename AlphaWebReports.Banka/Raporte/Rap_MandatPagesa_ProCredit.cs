using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.Banka.Raporte
{
    public partial class Rap_MandatPagesa_ProCredit : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_MandatPagesa_ProCredit(){InitializeComponent();} 
         private ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
        private CultureInfo ci;

        public Rap_MandatPagesa_ProCredit(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje)
        {

        }
        public Rap_MandatPagesa_ProCredit(CultureInfo ci, int idNdermarrje)
        {
            this.ci = ci;
            InitializeComponent();
            EmrateLabelave(ci);
           
            
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {

            xrLabel1.Text = rm.GetString("labelUrdherPageseUpperCase", ci);
            xrTableCell1.Text = rm.GetString("labelUrdheruesi", ci);
            xrTableCell5.Text = rm.GetString("labelNIPT", ci);
            xrTableCell3.Text = rm.GetString("labelRaportiShuma", ci);
            xrTableCell7.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrTableCell9.Text = rm.GetString("labelPerfituesi", ci);
            xrTableCell11.Text = rm.GetString("labelRaportAdresa", ci) + ":";
            xrTableCell15.Text = rm.GetString("labelRaportSwiftCode", ci);
            xrTableCell17.Text = rm.GetString("cmbboxItemFilterAvancBanka", ci);
            xrTableCell19.Text = rm.GetString("labelRaportData", ci);
            xrLabel4.Text = ", " + rm.GetString("labelSerial", ci);
            xrLabel6.Text = ", " + rm.GetString("labelNrFat", ci);
            xrLabel15.Text = rm.GetString("labelAutorizoiUpperCase", ci);
            xrTableCell18.Text = rm.GetString("labelRaportKontrolloi", ci);
            xrLabel17.Text = rm.GetString("labelPregatiti", ci);
            xrLabel16.Text = rm.GetString("labelMarresiNeDorezim", ci) + ":";

            xrLabel19.Text = rm.GetString("labelUrdherPageseUpperCase", ci) + " 1";
            xrTableCell23.Text = rm.GetString("labelUrdheruesi", ci);
            xrTableCell25.Text = rm.GetString("labelNIPT", ci);
            xrTableCell27.Text = rm.GetString("labelRaportiShuma", ci);
            xrTableCell29.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrTableCell31.Text = rm.GetString("labelPerfituesi", ci);
            xrTableCell37.Text = rm.GetString("labelNrLlogarie", ci);
            xrTableCell39.Text = rm.GetString("cmbboxItemFilterAvancBanka", ci);
            xrTableCell41.Text = rm.GetString("labelRaportData", ci);


            xrLabel20.Text = rm.GetString("labelUrdherPageseUpperCase", ci) + " 2";
            xrLabel28.Text = rm.GetString("labelShpenzimi", ci);
            xrLabel34.Text = rm.GetString("labelTatim", ci) + " 10%";
            xrLabel26.Text = rm.GetString("labelAutorizuesiUpperCase", ci);
            xrLabel44.Text = rm.GetString("labelPregatiti", ci);
            xrLabel45.Text = rm.GetString("labelMarresiNeDorezim", ci) + ":";
            xrLabel14.Text = rm.GetString("labelMoriNeDorezPerEkzekPagese", ci);
            
        }
     

    }
}
