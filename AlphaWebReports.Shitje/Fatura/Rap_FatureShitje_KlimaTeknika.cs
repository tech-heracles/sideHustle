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
    public partial class Rap_FatureShitje_KlimaTeknika : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureShitje_KlimaTeknika(){InitializeComponent();}
        
        double totalSkonto = 0;        
        private int counter = 0;
        List<object[]> artikujt = new List<object[]>();
        public Rap_FatureShitje_KlimaTeknika(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitje_KlimaTeknika(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }




        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel1.Text = rm.GetString("lblRaportFatureTitull", ci);
            xrLabel44.Text = rm.GetString("lblRaportNrPreventiv", ci);
            xrLabel8.Text = rm.GetString("lblRaportNumriSerial", ci);
            xrLabel7.Text = rm.GetString("labelRaportDateFature", ci);
            xrLabel47.Text = rm.GetString("labelRaportiShenime", ci);
            xrTableCell11.Text = rm.GetString("lblRaportMag", ci);
            xrLabel50.Text = rm.GetString("labelRaportiSubjekti", ci);
            xrLabel13.Text = rm.GetString("labelRaportAdresa", ci);
            xrLabel12.Text = rm.GetString("labelNIPT", ci);
            xrLabel14.Text = rm.GetString("labelRaportTelFax", ci);
            //xrLabel4.Text = rm.GetString("labelRaportSubjektBleres", ci);
            xrLabel2.Text = rm.GetString("labelRaportAdresa", ci);
            //xrLabel16.Text = rm.GetString("labelNIPT", ci);
            //xrLabel18.Text = rm.GetString("labelRaportTelFax", ci);
            xrLabel20.Text = rm.GetString("lblRaportDetyrimi", ci);
           // xrTableCell3.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell14.Text = rm.GetString("lblRaportNrkartel", ci);
            xrTableCell15.Text = rm.GetString("labelRaportiPershkrimi", ci);
          //  xrTableCell11.Text = rm.GetString("lblRaportSerialNo", ci);
           xrTableCell7.Text = rm.GetString("labelNjesia", ci);
            xrTableCell9.Text = rm.GetString("labelSasia", ci);
          xrTableCell10.Text = rm.GetString("labelCmimiMeTvsh", ci);
           // xrTableCell16.Text = rm.GetString("lblRaportDetajime", ci);
            xrTableCell8.Text = rm.GetString("labelRaportVlera", ci);
           xrLabel24.Text = rm.GetString("lblRaportTotalPreventiv", ci);
           xrLabel35.Text = rm.GetString("labelTotaliMeSkonto", ci);

            xrLabel22.Text = rm.GetString("labelBleresi", ci);
           xrLabel23.Text = rm.GetString("lblRaportTransportuesi", ci);
            xrLabel25.Text = rm.GetString("lblRaportKontrollori", ci);
             xrLabel26.Text = rm.GetString("lblRaportPreventivuesi", ci);
             xrLabel16.Text = rm.GetString("lblRaportGjithsejRreshta", ci);
             xrLabel18.Text = rm.GetString("lblRaportTotalSasi", ci);
             xrLabel61.Text = rm.GetString("lblKontaktiKlimateknika", ci);
          }

        private void xrTableCell6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (GetCurrentColumnValue("KODI") != null)
            //{
            //    counter++;
            //    xrTableCell6.Text = counter.ToString();
            //}
            //else xrTableCell6.Text = "";        
        }

        private void xrTableCell18_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("SASIA") != null && GetCurrentColumnValue("CMIMI") != null && GetCurrentColumnValue("ZBRITJEPERQ") != null)
            {
                totalSkonto += Double.Parse(GetCurrentColumnValue("SASIA").ToString()) * (Double.Parse(GetCurrentColumnValue("CMIMI").ToString()) * ( 1- Double.Parse(GetCurrentColumnValue("ZBRITJEPERQ").ToString()) / 100));
            }
        }

        private void xrLabel33_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel33.Text = totalSkonto.ToString();
        }

        private void xrLabel33_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //e.Result = totalSkonto;
            //e.Handled = true;
        }

        private void xrLabel33_SummaryReset(object sender, EventArgs e)
        {
            
        }

        private void xrLabel33_SummaryRowChanged(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// ne rastin kur vlera e detyrimit eshte negative duhet te shfaqet vlera e saj ne kllapa dhe pozitive
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xrLabel21_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if    (GetCurrentColumnValue("detyrimi") != null )
            { 
                if (float.Parse(GetCurrentColumnValue("detyrimi").ToString()) < 0)
            {
                String detyrimiformatuar = String.Format("{0:#,#.00}", GetCurrentColumnValue("detyrimi"));
                String detyrimi = '(' + ((float.Parse(detyrimiformatuar)) * (-1)).ToString() + ')';
                xrLabel21.Text = detyrimi;
            }


            }
        }

        private void xrTableCell1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KODI") != null)
                counter++;
            object afatGarancie = GetCurrentColumnValue("AfatGarancie");
            object llojGarancie = GetCurrentColumnValue("LlojGarancie");
            if (afatGarancie != null && afatGarancie != DBNull.Value && Convert.ToDecimal(afatGarancie) != 0 && llojGarancie != null && llojGarancie != DBNull.Value)
            {
                object[] obj = new object[4];
                obj[0] = GetCurrentColumnValue("PERSHKRIMI");
                object seriali = GetCurrentColumnValue("SHENIME");
                obj[1] = seriali != null ? seriali : "";
                obj[2] = afatGarancie;
                obj[3] = llojGarancie;
                artikujt.Add(obj);
            }
        }

        private void xrLabel37_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel37.Text = counter.ToString();
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (artikujt.Count == 0)
                ReportFooter.Visible = false;
        }

        private void xrTable5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int i = 0;
            if (artikujt.Count > 0)
            {
                xrTable5.Rows.Clear();
                foreach (object[] obj in artikujt)
                {
                    XRTableRow rr = new XRTableRow();
                    rr.HeightF = 25f;

                    XRTableCell pershkrimi = new XRTableCell();
                    pershkrimi.WidthF = 110f;
                    pershkrimi.Name = "pershkrimi" + i.ToString();
                    pershkrimi.Text = obj[0].ToString();
                    pershkrimi.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Left;
                    rr.Cells.Add(pershkrimi);

                    XRTableCell serial = new XRTableCell();
                    serial.WidthF = 80f;
                    serial.Name = "NrSerial" + i.ToString();
                    serial.Text = obj[1].ToString();
                    serial.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Left;
                    rr.Cells.Add(serial);

                    XRTableCell afatGarancie = new XRTableCell();
                    afatGarancie.WidthF = 10f;
                    afatGarancie.Name = "afatGarancie" + i.ToString();
                    afatGarancie.Text = String.Format("{0:#,#}", obj[2]);
                    afatGarancie.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Left;
                    rr.Cells.Add(afatGarancie);

                    XRTableCell llojGarancie = new XRTableCell();
                    llojGarancie.Name = "llojGarancie" + i.ToString();
                    llojGarancie.Text = obj[3].ToString();
                    llojGarancie.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right;
                    rr.Cells.Add(llojGarancie);

                    xrTable5.Rows.Insert(i, rr);
                    i++;
                }
            }
        }

        private void Rap_FatureShitje_KlimaTeknika_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);

        }
    }
}
