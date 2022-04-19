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
    public partial class Rap_CashFlowQK : DevExpress.XtraReports.UI.XtraReport, IUpdateDetailKPF
    {
		public Rap_CashFlowQK(){InitializeComponent();} 

        bool hapurgjitha = false;
        

        CultureInfo ci;
        public Rap_CashFlowQK(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje,param.IdViti, report)
        {

        }
        public Rap_CashFlowQK(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            this.ci = ci;
            MonNder.Value = raport.Parameters[3].Value;
            xrLabel70.Text = raport.Parameters[3].Description;
            xrLabel52.Text = raport.Parameters[5].Description;
            parameter1.Value = raport.Parameters[5].Value;
            xrLabel55.Text = raport.Parameters[0].Description;
            parameter2.Value = raport.Parameters[0].Value;
            xrLabel114.Text = raport.Parameters[4].Description;
            parameter4.Value = raport.Parameters[4].Value;
            azhronimLabel.Text = raport.Parameters[3].Description;
            Azhornim.Value = raport.Parameters[3].Value;
            xrLabel112.Text = raport.Parameters[6].Description;
            parameter3.Value = raport.Parameters[6].Value;
            hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);

            EmrateLabelave(ci);

        }


        string[] romake = { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII", "XIII", "XIV", "XV", "XVI", "XVII", "XVIII", "XIX", "XX" };
        string[] romakevogel = { "", "i", "ii", "iii", "iv", "v", "vi", "vii", "viii", "ix", "x", "xi", "xii", "xiii", "xiv", "xv", "xvi", "xvii", "xviii", "xix", "xx" };
        string[] shkronje = { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T" };
        string[] shkronjevogel = { "", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t" };
        int niv = 0;
        int niv2 = 0;
        int niv3 = 0;
        int niv4 = 0;
        int niv5 = 0;
        int niv6 = 0;

        private void xrLabel32_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrLabel7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrLabel14_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrLabel24_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrLabel31_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrLabel51_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {


            niv6++;
        }

        private void xrLabel32_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel7_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = shkronjevogel[niv2];
            e.Handled = true;
        }

        private void xrLabel14_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = romake[niv3];
            e.Handled = true;
        }

        private void xrLabel24_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = romakevogel[niv4];
            e.Handled = true;
        }

        private void xrLabel31_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = shkronje[niv5];
            e.Handled = true;
        }

        private void xrLabel51_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = shkronje[niv6];
            e.Handled = true;

        }

        private void xrLabel51_SummaryReset(object sender, EventArgs e)
        {
            niv = 0;
        }

        private void xrLabel32_SummaryReset(object sender, EventArgs e)
        {
            niv2 = 0;
        }

        private void xrLabel7_SummaryReset(object sender, EventArgs e)
        {
            niv3 = 0;
        }

        private void xrLabel14_SummaryReset(object sender, EventArgs e)
        {
            niv4 = 0;
        }

        private void xrLabel24_SummaryReset(object sender, EventArgs e)
        {
            niv5 = 0;
        }

        private void xrLabel63_SummaryReset(object sender, EventArgs e)
        {
            niv6 = 0;
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
            if (GetCurrentColumnValue("KODIKPF") != null)
            {
                string catid = GetCurrentColumnValue("KODIKPF").ToString();
                string catid2 = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
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
        }

        private void lblKPF_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KODIKPF") != null)
            {
                XRLabel label = sender as XRLabel;
                string catid = GetCurrentColumnValue("KODIKPF").ToString();


                if (GetCurrentColumnValue("NRLLOGARI").ToString() == "")
                {
                    label.Text = "";
                }
                else
                {
                    label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";Detail;cashFlowQendraKosto')";
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
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null)
            {
                string catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();

                if (SkippedDetailKPF.Contains(catid))
                    e.Cancel = Convert.ToBoolean(SkippedDetailKPF[catid]);
                else
                {
                    e.Cancel = !hapurgjitha; SkippedDetailKPF.Add(catid, !hapurgjitha);
                }
            }
        }

        private void xrLabel33_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null)
            {
                XRLabel label = sender as XRLabel;
                string catid = "";
                if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null) 
                    catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
                label.NavigateUrl = "";
                if (GetCurrentColumnValue("lloji") == null || GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ4") != null && GetCurrentColumnValue("SHFAQBIJ4").ToString() == "False")) 
                    label.Text = "";
                else
                {
                    label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;cashFlowQendraKosto')";
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
        }

        private void xrLabel25_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null)
            {
                XRLabel label = sender as XRLabel;
                string catid = "";
                if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null) 
                    catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
                label.NavigateUrl = "";
                if (GetCurrentColumnValue("lloji") == null || GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ4") != null && GetCurrentColumnValue("SHFAQBIJ4").ToString() == "False"))
                    label.Text = "";
                else
                {
                    label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;cashFlowQendraKosto')";
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
        }

        private void xrLabel15_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null)
            {
                XRLabel label = sender as XRLabel;
                string catid = "";
                if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null) 
                    catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
                label.NavigateUrl = "";
                if (GetCurrentColumnValue("lloji") == null || GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ3") != null && GetCurrentColumnValue("SHFAQBIJ3").ToString() == "False"))
                    label.Text = "";
                else
                {
                    label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;cashFlowQendraKosto')";
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
        }

        private void xrLabel8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null)
            {
                XRLabel label = sender as XRLabel;
                string catid = "";
                if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null)
                    catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
                label.NavigateUrl = "";
                if (GetCurrentColumnValue("lloji") == null || GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ2") != null && GetCurrentColumnValue("SHFAQBIJ2").ToString() == "False"))
                    label.Text = "";
                else
                {
                    label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;cashFlowQendraKosto')";
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
        }

        private void lblPrindi1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null)
            {
                XRLabel label = sender as XRLabel;
                string catid = "";
                if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null)
                    catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
                label.NavigateUrl = "";
                if (GetCurrentColumnValue("lloji") == null || GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
                    label.Text = "";
                else
                {
                    label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;cashFlowQendraKosto')";
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
        }

        private void xrLabel42_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel10_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel17_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel28_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel35_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel69_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel59_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel63_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (xrLabel72.Text != "")
            {
                if (xrLabel72.Text.Contains("("))
                    shuma += double.Parse(xrLabel72.Text.Replace('(', ' ').Replace(')', ' '));
                else shuma -= double.Parse(xrLabel72.Text);
            }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel78_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel48_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double shuma = 0;
            XRLabel label = sender as XRLabel;
            if (label.Text != "")
            {
                if (label.Text.Contains("("))
                    shuma = -double.Parse(label.Text.ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma = double.Parse(label.Text.ToString());
                if (shuma >= 0)
                    label.Text = String.Format("{0:#,#.00}", shuma);
                else label.Text = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            }
        }

        private void xrLabel72_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double shuma = 0;
            XRLabel label = sender as XRLabel;
            if (label.Text != "")
            {
                if (label.Text.Contains("("))
                    shuma = -double.Parse(label.Text.ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma = double.Parse(label.Text.ToString());
                if (shuma >= 0)
                    label.Text = String.Format("{0:#,#.00}", shuma);
                else label.Text = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            }
        }

        private void xrLabel90_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel95_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel100_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel105_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel110_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
            
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel12.Text = rm.GetString("RaportCashFlowTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel19.Text = rm.GetString("labelRaportiEmertimi", ci);
            xrLabel1.Text = rm.GetString("labelRaportiNr", ci);
            xrLabel85.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel20.Text = rm.GetString("labelRaportiShenime", ci);
            xrLabel21.Text = rm.GetString("labelRaportiVitiRaportues", ci);
            xrLabel22.Text = rm.GetString("labelRaportiVitiParaardhes", ci);
            xrLabel56.Text = rm.GetString("labelRaportiShuma", ci);
            xrLabel61.Text = rm.GetString("labelRaportiRritjaMjeteveMonetare", ci);
            xrLabel80.Text = rm.GetString("labelRaportiMjeteMonetareFundPeriudhe", ci);
            //xrLabel70.Text = rm.GetString("filterMonedha");



        }
        private void xrLabel57_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                                 System.Reflection.Assembly.Load("App_GlobalResources"));
            if (xrLabel57.Text == "(Para neto nga veprimtarite e shfrytezimit)")
                xrLabel57.Text = rm.GetString("parashfytezimi", ci);
            else if (xrLabel57.Text == "(Para neto nga veprimtarite investuese)")
                xrLabel57.Text = rm.GetString("parainvenstuese", ci);
            else if (xrLabel57.Text == "(Para neto nga te pacaktuara)")
                xrLabel57.Text = rm.GetString("parapacaktuara", ci);
            else xrLabel57.Text = rm.GetString("paraaktiviteti", ci);
            //'(Para neto nga aktiviteti financiar)') ) )
        }

        private void xrLabel2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));
            if (xrLabel2.Text == "Fluksi i parave nga veprimtarite e shfrytezimit")
                xrLabel2.Text = rm.GetString("fluksshfrytezimi", ci);
            else if (xrLabel2.Text == "Fluksi i parave nga veprimtarite investuese")
                xrLabel2.Text = rm.GetString("fluksinvestues", ci);
            else if (xrLabel2.Text == "Te pacaktuara")
                xrLabel2.Text = rm.GetString("flukstepacaktuara", ci);
            else xrLabel2.Text = rm.GetString("fluksfinanciar1", ci);
            //   'Fluksi i parave nga aktiviteti financiar'

        }
    }
}
