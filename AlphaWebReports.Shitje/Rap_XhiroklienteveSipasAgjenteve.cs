using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Resources;
using System.Globalization;


namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_XhiroklienteveSipasAgjenteve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_XhiroklienteveSipasAgjenteve(){InitializeComponent();} 
        ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
        CultureInfo ci;
        double shumaxhiro1, shumaxhiro2, shumaxhiro3 = 0;
        double dif1, dif2, total1, total2 = 0;
        public Rap_XhiroklienteveSipasAgjenteve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_XhiroklienteveSipasAgjenteve(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            lblFilter1.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel6.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            xrLabel5.Text = raport.Parameters[2].Description;
            parameter3.Value = raport.Parameters[2].Value;
            xrLabel7.Text = raport.Parameters[3].Description;
            parameter4.Value = raport.Parameters[3].Value;
            //  monedha = Convert.ToBoolean(raport.Parameters[2].Value);
            xrLabel1.Text = raport.Parameters[4].Description;
            parameter5.Value = raport.Parameters[4].Value;
            xrLabel11.Text = raport.Parameters[5].Description;
            parameter6.Value = raport.Parameters[5].Value;
            xrLabel13.Text = raport.Parameters[6].Description;
            parameter7.Value = raport.Parameters[6].Value;
            xrLabel17.Text = raport.Parameters[7].Description;
            parameter8.Value = raport.Parameters[7].Value;
            xrLabel15.Text = raport.Parameters[8].Description;
            parameter9.Value = raport.Parameters[8].Value;
            xrLabel19.Text = raport.Parameters[9].Description;
            parameter10.Value = raport.Parameters[9].Value;
            xrLabel22.Text = raport.Parameters[10].Description;
            parameter11.Value = raport.Parameters[10].Value;
            xrLabel24.Text = raport.Parameters[11].Description;
            parameter12.Value = raport.Parameters[11].Value;
            xrLabel26.Text = raport.Parameters[12].Description;
            parameter13.Value = raport.Parameters[12].Value;
            xrLabel28.Text = raport.Parameters[13].Description;
            parameter14.Value = raport.Parameters[13].Value;
            xrLabel30.Text = raport.Parameters[14].Description;
            parameter15.Value = raport.Parameters[14].Value;
            xrLabel32.Text = raport.Parameters[15].Description;
            parameter16.Value = raport.Parameters[15].Value;
            xrLabel38.Text = raport.Parameters[16].Description;
            parameter17.Value = raport.Parameters[16].Value;
            xrLabel34.Text = raport.Parameters[17].Description;
            parameter18.Value = raport.Parameters[17].Value;
            xrLabel40.Text = raport.Parameters[18].Description;
            parameter19.Value = raport.Parameters[18].Value;
            xrLabel36.Text = raport.Parameters[19].Description;
            parameter20.Value = raport.Parameters[19].Value;
            xrLabel42.Text = raport.Parameters[20].Description;
            parameter21.Value = raport.Parameters[20].Value;
            this.ci = ci;
            EmrateLabelave(ci);
           // this.PrintingSystem.ExportOptions.Xlsx.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Value;

        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrTableCell23.Text = rm.GetString("labelRaportTotal", ci);
            xrLabel1.Text = rm.GetString("FiltratEmertimi", ci);
            Titulli.Text = rm.GetString("RaportXhiroklienteveSipasAgjenteveTitulli", ci);
            xrTableCell13.Text = rm.GetString("labelRaportiEmertimi", ci);
            xrTableCell14.Text = rm.GetString("labelRaportXhiro", ci);
            xrTableCell15.Text = rm.GetString("labelRaportXhiro", ci);
            xrTableCell16.Text = rm.GetString("labelRaportXhiro", ci);
            xrTableCell1.Text = rm.GetString("lblDiferenca", ci);
            xrTableCell4.Text = rm.GetString("lblDiferenca", ci);
            xrTableCell17.Text = rm.GetString("labelRaportiRritjes", ci);
            xrTableCell2.Text = rm.GetString("labelRaportiRritjes", ci);
            xrTableCell23.Text = rm.GetString("labelRaportiTotali", ci);
            //xrLabel20.Text = rm.GetString("labelLogoIMB", ci);
        }

        private void xrTableCell14_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
            if (GetCurrentColumnValue("muajaktual") != null)
            {
                switch (GetCurrentColumnValue("muajaktual").ToString())
                {
                    case "1":
                        xrTableCell14.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportJanar", ci)
                            + " " + GetCurrentColumnValue("vitiparaardhes").ToString();

                        break;
                    case "2":
                        xrTableCell14.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportShkurt", ci)
                              + " " + GetCurrentColumnValue("vitiparaardhes").ToString();
                        break;
                    case "3":
                        xrTableCell14.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportMars", ci)
                              + " " + GetCurrentColumnValue("vitiparaardhes").ToString();
                        break;
                    case "4":
                        xrTableCell14.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportPrill", ci)
                              + " " + GetCurrentColumnValue("vitiparaardhes").ToString();
                        break;
                    case "5":
                        xrTableCell14.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportMaj", ci)
                              + " " + GetCurrentColumnValue("vitiparaardhes").ToString();
                        break;
                    case "6":
                        xrTableCell14.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportQershor", ci)
                              + " " + GetCurrentColumnValue("vitiparaardhes").ToString();
                        break;
                    case "7":
                        xrTableCell14.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportKorrik", ci)
                              + " " + GetCurrentColumnValue("vitiparaardhes").ToString();
                        break;
                    case "8":
                        xrTableCell14.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportGusht", ci)
                              + " " + GetCurrentColumnValue("vitiparaardhes").ToString();
                        break;
                    case "9":
                        xrTableCell14.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportShtator", ci)
                              + " " + GetCurrentColumnValue("vitiparaardhes").ToString();
                        break;
                    case "10":
                        xrTableCell14.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportTetor", ci) + " " + GetCurrentColumnValue("vitiparaardhes").ToString();
                        break;
                    case "11":
                        xrTableCell14.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportNentor", ci) + " " + GetCurrentColumnValue("vitiparaardhes").ToString();
                        break;
                    case "12":
                        xrTableCell14.Text = rm.GetString("labelRaportXhiro", ci) + rm.GetString("labelRaportDhjetor", ci) + " " + GetCurrentColumnValue("vitiparaardhes").ToString();
                        break;
                    default:
                        break;
                }
            }
        }

        private void xrTableCell15_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("muajparaardhes") != null)
            {
                switch (GetCurrentColumnValue("muajparaardhes").ToString())
                {
                    case "1":
                        xrTableCell15.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportJanar", ci) + " " + GetCurrentColumnValue("vitiaktual").ToString();
                        break;
                    case "2":
                        xrTableCell15.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportShkurt", ci) + " " + GetCurrentColumnValue("vitiaktual").ToString();
                        break;
                    case "3":
                        xrTableCell15.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportMars", ci) + " " + GetCurrentColumnValue("vitiaktual").ToString();
                        break;
                    case "4":
                        xrTableCell15.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportPrill", ci) + " " + GetCurrentColumnValue("vitiaktual").ToString();
                        break;
                    case "5":
                        xrTableCell15.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportMaj", ci) + " " + GetCurrentColumnValue("vitiaktual").ToString();
                        break;
                    case "6":
                        xrTableCell15.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportQershor", ci) + " " + GetCurrentColumnValue("vitiaktual").ToString();
                        break;
                    case "7":
                        xrTableCell15.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportKorrik", ci) + " " + GetCurrentColumnValue("vitiaktual").ToString();
                        break;
                    case "8":
                        xrTableCell15.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportGusht", ci) + " " + GetCurrentColumnValue("vitiaktual").ToString();
                        break;
                    case "9":
                        xrTableCell15.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportShtator", ci) + " " + GetCurrentColumnValue("vitiaktual").ToString();
                        break;
                    case "10":
                        xrTableCell15.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportTetor", ci) + " " + GetCurrentColumnValue("vitiaktual").ToString();
                        break;
                    case "11":
                        xrTableCell15.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportNentor", ci) + " " + GetCurrentColumnValue("vitiaktual").ToString();
                        break;
                    case "12":
                        xrTableCell15.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportDhjetor", ci) + " " + GetCurrentColumnValue("vitiaktual").ToString();
                        break;
                    default:
                        break;
                }
            }
        }

        private void xrTableCell16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("muajaktual") != null)
            {
                switch (GetCurrentColumnValue("muajaktual").ToString())
                {
                    case "1":
                        xrTableCell16.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportJanar", ci) + " " + GetCurrentColumnValue("vitiaktual").ToString();
                        break;
                    case "2":
                        xrTableCell16.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportShkurt", ci) + " " + GetCurrentColumnValue("vitiaktual").ToString();
                        break;
                    case "3":
                        xrTableCell16.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportMars", ci) + " " + GetCurrentColumnValue("vitiaktual").ToString();
                        break;
                    case "4":
                        xrTableCell16.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportPrill", ci) + " " + GetCurrentColumnValue("vitiaktual").ToString();
                        break;
                    case "5":
                        xrTableCell16.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportMaj", ci) + " " + GetCurrentColumnValue("vitiaktual").ToString();
                        break;
                    case "6":
                        xrTableCell16.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportQershor", ci) + " " + GetCurrentColumnValue("vitiaktual").ToString();
                        break;
                    case "7":
                        xrTableCell16.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportKorrik", ci) + " " + GetCurrentColumnValue("vitiaktual").ToString();
                        break;
                    case "8":
                        xrTableCell16.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportGusht", ci) + " " + GetCurrentColumnValue("vitiaktual").ToString();
                        break;
                    case "9":
                        xrTableCell16.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportShtator", ci) + " " + GetCurrentColumnValue("vitiaktual").ToString();
                        break;
                    case "10":
                        xrTableCell16.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportTetor", ci) + " " + GetCurrentColumnValue("vitiaktual").ToString();
                        break;
                    case "11":
                        xrTableCell16.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportNentor", ci) + " " + GetCurrentColumnValue("vitiaktual").ToString();
                        break;
                    case "12":
                        xrTableCell16.Text = rm.GetString("labelRaportXhiro", ci) + " " + rm.GetString("labelRaportDhjetor", ci) + " " + GetCurrentColumnValue("vitiaktual").ToString();
                        break;
                    default:
                        break;
                }
            }
        }

        private void xrTableCell1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCell1.Text = "Diferenca " + xrTableCell16.Text.ToString().Substring(5) + " - " + xrTableCell14.Text.ToString().Substring(5);
        }

        private void xrTableCell4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCell4.Text = "Diferenca " + xrTableCell16.Text.ToString().Substring(5) + " - " + xrTableCell15.Text.ToString().Substring(5);

        }

              private void xrTableCell2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCell2.Text = "% e Ndryshimit " + xrTableCell16.Text.ToString().Substring(5) + " - " + xrTableCell14.Text.ToString().Substring(5);
        }

        private void xrTableCell17_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCell17.Text = "% e Ndryshimit " + xrTableCell16.Text.ToString().Substring(5) + " - " + xrTableCell15.Text.ToString().Substring(5);
        }


        private void xrTableCell8_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = String.Format("{0:#,#.00}",  shumaxhiro2);
            e.Handled=true;
            total2 += shumaxhiro2;
        }

        private void xrTableCell7_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result  = String.Format("{0:#,#.00}", shumaxhiro1);
           e.Handled = true;
           total1 += shumaxhiro1;
           xrTableCell7 .XlsxFormatString= 0.ToString("N" + 0);
        }

        private void xrTableCell9_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = String.Format("{0:#,#.00}",  shumaxhiro3);
               
            e.Handled = true;
            //if ((shumaxhiro1 == 0) && (shumaxhiro2 == 0) && (shumaxhiro3 == 0))
            //{
            //    xrTableCell6.Visible = false;

            //}
        }

        private void xrTableCell7_SummaryReset(object sender, EventArgs e)
        {
           shumaxhiro1 = 0;
        }

        private void xrTableCell8_SummaryReset(object sender, EventArgs e)
        {
           shumaxhiro2 = 0;
        
        }

        private void xrTableCell9_SummaryReset(object sender, EventArgs e)
        {
            shumaxhiro3 = 0;
        }

        private void xrTableCell7_SummaryRowChanged(object sender, EventArgs e)
        {
            if (this.GetCurrentColumnValue("DETYRIMIMONBAZEViti") != null)
                shumaxhiro1 += Convert.ToDouble(this.GetCurrentColumnValue("DETYRIMIMONBAZEViti"));
        }

        private void xrTableCell8_SummaryRowChanged(object sender, EventArgs e)
        {
            if (this.GetCurrentColumnValue("DETYRIMIMeparshem") != null)
                shumaxhiro2 += Convert.ToDouble(this.GetCurrentColumnValue("DETYRIMIMeparshem"));
        }

        private void xrTableCell9_SummaryRowChanged(object sender, EventArgs e)
        {
            if (this.GetCurrentColumnValue("DETYRIMI") != null)
                shumaxhiro3 += Convert.ToDouble(this.GetCurrentColumnValue("DETYRIMI"));

        }


        private void xrTableCell10_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = String.Format("{0:#,#.00}",Convert.ToDouble( shumaxhiro3-shumaxhiro1));
         //   xrTableCell10.Text = String.Format("{0:#,#.00}", shumaxhiro3 - shumaxhiro1);
            e.Handled = true;
            xrTableCell10.XlsxFormatString=String.Format("{0:#,#.00}",Convert.ToDouble( shumaxhiro3-shumaxhiro1));
            dif1 += shumaxhiro3 - shumaxhiro1;
        }

      

        private void xrTableCell11_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = String.Format("{0:#,#.00}", shumaxhiro3 - shumaxhiro2);
            
            e.Handled = true;
            dif2 += shumaxhiro3 - shumaxhiro2;

        }

     

        private void xrTableCell26_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCell26.Text = String.Format("{0:#,#.00}", dif1);
        }

        private void xrTableCell27_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrTableCell27.Text = String.Format("{0:#,#.00}", dif2);
        }

        private void xrTableCell30_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (total2 != 0)
            {
                if ((total2 < 0) && (dif2> 0))
                    xrTableCell30.Text = "";
                else
                    xrTableCell30.Text =
                        //((shumaxhiro3 - shumaxhiro1) / shumaxhiro1).ToString();
            String.Format("{0:#,#.00}", (dif2 / total2) * 100);
            }
            else xrTableCell30.Text = "";
        }

        private void xrTableCell31_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (total1 != 0)
            {
                if ((total1 < 0) && (dif1 > 0))
                    xrTableCell31.Text = "";
                else
                    xrTableCell31.Text =
                        //((shumaxhiro3 - shumaxhiro1) / shumaxhiro1).ToString();
            String.Format("{0:#,#.00}", (dif1 / total1) * 100);
            }
            else xrTableCell31.Text = "";
        }

       

        private void xrTableCell29_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (shumaxhiro1 != 0)
            {
                if ((shumaxhiro1 < 0) && (shumaxhiro3 - shumaxhiro1) > 0)
                    e.Result = "";
                else
                    e.Result =
            //((shumaxhiro3 - shumaxhiro1) / shumaxhiro1).ToString();
            String.Format("{0:#,#.00}", ((shumaxhiro3 - shumaxhiro1) / shumaxhiro1) * 100);
            }
            else e.Result = "";
            e.Handled = true;

            //if (shumaxhiro2 != 0)
            //{
            //    if ((shumaxhiro2 < 0) && (shumaxhiro3 - shumaxhiro2) > 0)
            //        e.Result = "";
            //    else
            //        e.Result = String.Format("{0:#,#.00}", ((shumaxhiro3 - shumaxhiro2) / shumaxhiro2) * 100);
            //}
            //else e.Result = "";

            //e.Handled = true;
        }

        private void xrTableCell12_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (shumaxhiro2 != 0)
            {
                if ((shumaxhiro2 < 0) && (shumaxhiro3 - shumaxhiro2) > 0)
                    e.Result = "";
                else
                    e.Result = String.Format("{0:#,#.00}", ((shumaxhiro3 - shumaxhiro2) / shumaxhiro2) * 100);
            }
            else e.Result = "";

            e.Handled = true;          
        }


    }
}
 
