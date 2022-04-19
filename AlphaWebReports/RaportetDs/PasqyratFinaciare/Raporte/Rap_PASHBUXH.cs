using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.PasqyratFinaciare.Raporte
{
    public partial class Rap_PASHBUXH : DevExpress.XtraReports.UI.XtraReport, IUpdateDetailKPF
    {
		public Rap_PASHBUXH(){InitializeComponent();}
        int formatNumri = 0;
        bool hapurgjitha = false;
        bool gjendje = false;


        bool bold = false;
        public Rap_PASHBUXH(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_PASHBUXH(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            MonNder.Value = raport.Parameters[4].Value;
            xrLabel52.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel55.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            azhornimLabel.Text = raport.Parameters[3].Description;
            Azhornim.Value = raport.Parameters[3].Value;
            xrLabel105.Text = raport.Parameters[5].Description;
            parameter3.Value = raport.Parameters[5].Value;
            hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);
            if ((raport.Parameters["filterGjendja"].Value).ToString() == "Llogari me gjendje")
                gjendje = true;
            
            EmrateLabelave(ci); caktoFormatinENumrave();
            formatNumri = Convert.ToInt32(raport.Parameters["filterFormatNumri"].Value);
            parameterFormatNumri.Value = formatNumri;
            caktoFormatinENumrave();
        }
        private void caktoFormatinENumrave()
        {
            xrLabel11.DataBindings[0].FormatString = xrLabel43.DataBindings[0].FormatString = xrLabel59.DataBindings[0].FormatString = xrLabel60.DataBindings[0].FormatString = xrLabel65.DataBindings[0].FormatString = xrLabel66.DataBindings[0].FormatString =
            xrLabel45.DataBindings[0].FormatString = xrLabel46.DataBindings[0].FormatString = xrLabel39.DataBindings[0].FormatString = xrLabel40.DataBindings[0].FormatString = xrLabel36.DataBindings[0].FormatString
            = xrLabel37.DataBindings[0].FormatString = xrLabel29.DataBindings[0].FormatString = xrLabel30.DataBindings[0].FormatString = xrLabel26.DataBindings[0].FormatString = xrLabel27.DataBindings[0].FormatString = xrLabel13.DataBindings[0].FormatString = xrLabel42.DataBindings[0].FormatString =
            xrLabel101.DataBindings[0].FormatString =
            xrLabel100.DataBindings[0].FormatString =
             xrLabel96.DataBindings[0].FormatString =
             xrLabel95.DataBindings[0].FormatString =
             xrLabel91.DataBindings[0].FormatString =
             xrLabel90.DataBindings[0].FormatString =
             xrLabel85.DataBindings[0].FormatString =
             xrLabel84.DataBindings[0].FormatString =
         xrLabel50.DataBindings[0].FormatString =
         xrLabel51.DataBindings[0].FormatString =
         xrLabel72.DataBindings[0].FormatString =
         xrLabel73.DataBindings[0].FormatString =
         xrLabel78.DataBindings[0].FormatString =
         xrLabel89.DataBindings[0].FormatString =
         xrLabel82.DataBindings[0].FormatString =
         xrLabel83.DataBindings[0].FormatString =
             "{0:n" + formatNumri + "}";

            xrLabel59.Summary.FormatString = xrLabel60.Summary.FormatString = xrLabel65.Summary.FormatString = xrLabel66.Summary.FormatString =
            xrLabel45.Summary.FormatString = xrLabel46.Summary.FormatString = xrLabel39.Summary.FormatString = xrLabel40.Summary.FormatString = xrLabel36.Summary.FormatString
            = xrLabel37.Summary.FormatString = xrLabel29.Summary.FormatString = xrLabel30.Summary.FormatString = xrLabel26.Summary.FormatString = xrLabel27.Summary.FormatString = xrLabel13.Summary.FormatString = xrLabel42.Summary.FormatString =
            xrLabel101.Summary.FormatString = xrLabel100.Summary.FormatString = xrLabel96.Summary.FormatString = xrLabel95.Summary.FormatString =
            xrLabel91.Summary.FormatString =
            xrLabel90.Summary.FormatString =
            xrLabel85.Summary.FormatString =
            xrLabel84.Summary.FormatString =
            xrLabel50.Summary.FormatString =
            xrLabel51.Summary.FormatString =
            xrLabel72.Summary.FormatString =
            xrLabel73.Summary.FormatString =
            xrLabel78.Summary.FormatString =
            xrLabel89.Summary.FormatString =
            xrLabel82.Summary.FormatString =
            xrLabel83.Summary.FormatString
            = "{0:n" + formatNumri + "}";


            xrLabel11.XlsxFormatString = xrLabel43.XlsxFormatString = xrLabel59.XlsxFormatString = xrLabel60.XlsxFormatString = xrLabel65.XlsxFormatString = xrLabel66.XlsxFormatString =
            xrLabel45.XlsxFormatString = xrLabel46.XlsxFormatString = xrLabel39.XlsxFormatString = xrLabel40.XlsxFormatString = xrLabel36.XlsxFormatString
            = xrLabel37.XlsxFormatString = xrLabel29.XlsxFormatString = xrLabel30.XlsxFormatString = xrLabel26.XlsxFormatString = xrLabel27.XlsxFormatString = xrLabel13.XlsxFormatString = xrLabel42.XlsxFormatString =
                       xrLabel101.XlsxFormatString =
                       xrLabel100.XlsxFormatString =
                       xrLabel96.XlsxFormatString =
                       xrLabel95.XlsxFormatString =
                       xrLabel91.XlsxFormatString =
                       xrLabel90.XlsxFormatString = xrLabel85.XlsxFormatString = xrLabel84.XlsxFormatString =
                       xrLabel50.XlsxFormatString = xrLabel51.XlsxFormatString = xrLabel72.XlsxFormatString = xrLabel73.XlsxFormatString = xrLabel78.XlsxFormatString = xrLabel89.XlsxFormatString = xrLabel82.XlsxFormatString = xrLabel83.XlsxFormatString =
                        0.ToString("N" + formatNumri);
        }
        string[] shkronjevogel = { "", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t" };
        string[] shkronjemadhe = { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T" };

        int niv = 0;
        //int niv2 = 0;
        //int niv3 = 0;
        private void xrLabel32_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
            xrLabel32.Text = niv.ToString();
        }

        private void xrLabel4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
            xrLabel4.Text = niv.ToString();
        }

        private void xrLabel8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
            xrLabel8.Text = niv.ToString();
        }

        private void xrLabel16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
            xrLabel16.Text = niv.ToString();
        }

        private void xrLabel23_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
            xrLabel23.Text = niv.ToString();
        }
        

        private void xrLabel47_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //  if (GetCurrentColumnValue("NIVELI").ToString() == "1")
            niv++;
            xrLabel47.Text = niv.ToString();
        }

        private void xrLabel79_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
                niv++;
            xrLabel79.Text = niv.ToString();
        }
        
        private void xrLabel32_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel4_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel8_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel16_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {

            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel23_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }
        


        private void xrLabel47_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel79_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }
        

        private Hashtable skippedDetailBands;
        
        private Hashtable skippedDetailKPF;
        
        public Hashtable SkippedDetailBands
        {
            get
            {
                if (skippedDetailBands == null)
                    skippedDetailBands = new Hashtable();

                return skippedDetailBands;
            }
            set { skippedDetailBands = value; }
        }
        
        public Hashtable SkippedDetailKPF
        {
            get
            {
                if (skippedDetailKPF == null)
                    skippedDetailKPF = new Hashtable();

                return skippedDetailKPF;
            }
            set { skippedDetailKPF = value; }
        }

        public void UpdateDetailKPF(string detailID)
        {
            if (SkippedDetailKPF.Contains(detailID))
                SkippedDetailKPF[detailID] = !Convert.ToBoolean(SkippedDetailKPF[detailID]);
            else
                SkippedDetailKPF.Add(detailID, false);

            
        }
        
        public void UpdateDetail(string detailID)
        {
            if (SkippedDetailBands.Contains(detailID))
                SkippedDetailBands[detailID] = !Convert.ToBoolean(SkippedDetailBands[detailID]);
            else
                SkippedDetailBands.Add(detailID, false);

            
        }
        
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {


            string catid = "";

            if (GetCurrentColumnValue("kodikpf") != null && GetCurrentColumnValue("kodikpf").ToString() != null && GetCurrentColumnValue("kodikpf").ToString() != "")
                catid = GetCurrentColumnValue("kodikpf").ToString();
            string catid2 = "";

            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null && GetCurrentColumnValue("PERSHKRIMIZERIT").ToString() != null && GetCurrentColumnValue("PERSHKRIMIZERIT").ToString() != "")
                catid2 = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            if (SkippedDetailKPF.Contains(catid2))
            {
                if (Convert.ToBoolean(SkippedDetailKPF[catid2]) == true)
                {
                    e.Cancel = true;
                    return;
                }

            }

            if (SkippedDetailBands.Contains(catid))
                e.Cancel = Convert.ToBoolean(SkippedDetailBands[catid]);
            else
            {
                e.Cancel = !hapurgjitha; SkippedDetailBands.Add(catid, !hapurgjitha);
            }
        }

        private void lblKPF_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            string catid = "";

            if (GetCurrentColumnValue("kodikpf") != null && GetCurrentColumnValue("kodikpf").ToString() != null && GetCurrentColumnValue("kodikpf").ToString() != "")
                catid = GetCurrentColumnValue("kodikpf").ToString();

            if (GetCurrentColumnValue("NRLLOGARI") != null &&  GetCurrentColumnValue("NRLLOGARI").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJLLOG") != null && GetCurrentColumnValue("SHFAQBIJLLOG").ToString() == "False"))
            {
                label.Text = "";
            }
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";Detail;ardhura')";
                if (!SkippedDetailBands.Contains(catid))
                    if (hapurgjitha)
                        label.Text = "-";
                    else label.Text = "+";
                else if ((bool)SkippedDetailBands[catid] == false)
                    label.Text = "-";
                else
                    label.Text = "+";
            }
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string catid = "";

            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null && GetCurrentColumnValue("PERSHKRIMIZERIT").ToString() != null && GetCurrentColumnValue("PERSHKRIMIZERIT").ToString() != "")
                catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();

            if (SkippedDetailKPF.Contains(catid))
                e.Cancel = Convert.ToBoolean(SkippedDetailKPF[catid]);
            else
            {
                e.Cancel = !hapurgjitha; SkippedDetailKPF.Add(catid, !hapurgjitha);
            }
        }

        private void lblPrindi1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            string catid = "";

            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null && GetCurrentColumnValue("PERSHKRIMIZERIT").ToString() != null && GetCurrentColumnValue("PERSHKRIMIZERIT").ToString() != "")
                catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji") != null && GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ3") != null && GetCurrentColumnValue("SHFAQBIJ3").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;ardhura')";
                if (!SkippedDetailKPF.Contains(catid))
                    if (hapurgjitha)
                        label.Text = "-";
                    else label.Text = "+";
                else if ((bool)SkippedDetailKPF[catid] == false)
                    label.Text = "-";
                else
                    label.Text = "+";
            }
        }

        private void lblprindi2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            string catid = "";

            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null && GetCurrentColumnValue("PERSHKRIMIZERIT").ToString() != null && GetCurrentColumnValue("PERSHKRIMIZERIT").ToString() != "")
                catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji") != null && GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ4") != null && GetCurrentColumnValue("SHFAQBIJ4").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;ardhura')";
                if (!SkippedDetailKPF.Contains(catid))
                    if (hapurgjitha)
                        label.Text = "-";
                    else label.Text = "+";
                else if ((bool)SkippedDetailKPF[catid] == false)
                    label.Text = "-";
                else
                    label.Text = "+";
            }
        }

        private void lblprindi3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            string catid = "";

            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null && GetCurrentColumnValue("PERSHKRIMIZERIT").ToString() != null && GetCurrentColumnValue("PERSHKRIMIZERIT").ToString() != "")
                catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji") != null && GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ5") != null && GetCurrentColumnValue("SHFAQBIJ5").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;ardhura')";
                if (!SkippedDetailKPF.Contains(catid))
                    if (hapurgjitha)
                        label.Text = "-";
                    else label.Text = "+";
                else if ((bool)SkippedDetailKPF[catid] == false)
                    label.Text = "-";
                else
                    label.Text = "+";
            }
        }

        private void lblprindi4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            string catid = "";

            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null && GetCurrentColumnValue("PERSHKRIMIZERIT").ToString() != null && GetCurrentColumnValue("PERSHKRIMIZERIT").ToString() != "")
                catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji") != null && GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ6") != null && GetCurrentColumnValue("SHFAQBIJ6").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;ardhura')";
                if (!SkippedDetailKPF.Contains(catid))
                    if (hapurgjitha)
                        label.Text = "-";
                    else label.Text = "+";
                else if ((bool)SkippedDetailKPF[catid] == false)
                    label.Text = "-";
                else
                    label.Text = "+";
            }
        }

        private void lblprindi5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            string catid = "";

            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null && GetCurrentColumnValue("PERSHKRIMIZERIT").ToString() != null && GetCurrentColumnValue("PERSHKRIMIZERIT").ToString() != "")
                catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji") != null && GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ7") != null && GetCurrentColumnValue("SHFAQBIJ7").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;ardhura')";
                if (!SkippedDetailKPF.Contains(catid))
                    if (hapurgjitha)
                        label.Text = "-";
                    else label.Text = "+";
                else if ((bool)SkippedDetailKPF[catid] == false)
                    label.Text = "-";
                else
                    label.Text = "+";
            }
        }
        

        private void xrLabel61_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            string catid = "";

            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null && GetCurrentColumnValue("PERSHKRIMIZERIT").ToString() != null && GetCurrentColumnValue("PERSHKRIMIZERIT").ToString() != "")
                catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji") != null && GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ2") != null && GetCurrentColumnValue("SHFAQBIJ2").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;ardhura')";
                if (SkippedDetailKPF.Contains(catid) && (bool)SkippedDetailKPF[catid] == false)
                    label.Text = "-";
                else
                    label.Text = "+";
            }
        }

        private void xrLabel18_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            string catid = "";

            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null && GetCurrentColumnValue("PERSHKRIMIZERIT").ToString() != null && GetCurrentColumnValue("PERSHKRIMIZERIT").ToString() != "")
                catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji") != null && GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;ardhura')";
                if (SkippedDetailKPF.Contains(catid) && (bool)SkippedDetailKPF[catid] == false)
                    label.Text = "-";
                else
                    label.Text = "+";
            }
        }

        private void xrLabel56_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            niv++;
            xrLabel56.Text = niv.ToString();
        }

        private void xrLabel56_SummaryGetResult_1(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel62_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
            xrLabel62.Text = niv.ToString();
        }

        private void xrLabel62_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel67_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
            xrLabel67.Text = niv.ToString();
        }

        private void xrLabel67_SummaryGetResult_1(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel74_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
            xrLabel74.Text = niv.ToString();
        }

        private void xrLabel74_SummaryGetResult_1(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }
        private void xrLabel88_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel94_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel99_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel104_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel104_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
            xrLabel104.Text = niv.ToString();
        }

        private void xrLabel99_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
            xrLabel99.Text = niv.ToString();
        }

        private void xrLabel94_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
            xrLabel94.Text = niv.ToString();
        }

        private void xrLabel88_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
            xrLabel88.Text = niv.ToString();
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));


            xrLabel12.Text = rm.GetString("RaportTeArdhuratShpenzimetTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel9.Text = rm.GetString("labelRaportiEmertimi", ci);
            xrLabel7.Text = rm.GetString("labelLogoIMB", ci);
            //xrLabel20.Text = rm.GetString("labelRaportiShenime", ci);
            xrLabel21.Text = rm.GetString("labelRaportiUshtrimiMbyllur", ci);
            xrLabel22.Text = rm.GetString("labelRaportiUshtrimiParaardhes", ci);
            xrLabel80.Text = rm.GetString("labelRaportiFitimiNetoVitFinanciar", ci);
            xrLabel1.Text = rm.GetString("labelRaportiNr", ci);
            xrLabel70.Text = rm.GetString("filterMonedha", ci);
        }

        private void xrLabel6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
                XRLabel label = sender as XRLabel;

            if (GetCurrentColumnValue("NIVELI") != null && GetCurrentColumnValue("NIVELI").ToString() != null && GetCurrentColumnValue("NIVELI").ToString() != "")
            {
                if (Convert.ToInt32(GetCurrentColumnValue("NIVELI")) == 1 || Convert.ToInt32(GetCurrentColumnValue("NIVELI")) == 2 || Convert.ToInt32(GetCurrentColumnValue("NIVELI")) == 3)
                {
                    label.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    bold = true;

                }
            }
            else
            {
                label.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                bold = false;
            }

        }

        private void xrLabel46_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;

            if (bold)
            {
                label.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
            else
  
                label.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         

        }

        private void xrLabel110_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;

            if (bold)
            {
                label.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
            else

                label.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        }

        private void xrLabel45_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;

            if (bold)
            {
                label.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
            else

                label.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


        }

        private void ReportHeader_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv = 0;
        }

        private void GroupHeader8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (gjendje && Convert.ToInt32(GetCurrentColumnValue("gjend")) == 0)
               // GroupHeader8.Visible = false;

        }

        private void GroupHeader7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (gjendje && Convert.ToInt32(GetCurrentColumnValue("gjend")) == 0)
                //GroupHeader7.Visible = false;
        }

        private void GroupHeader6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            //if (gjendje && Convert.ToInt32(GetCurrentColumnValue("gjend")) == 0)
                //GroupHeader6.Visible = false;
        }
    }
}
