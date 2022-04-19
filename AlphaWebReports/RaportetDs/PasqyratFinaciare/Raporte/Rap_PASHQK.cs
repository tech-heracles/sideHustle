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
    public partial class Rap_PASHQK : DevExpress.XtraReports.UI.XtraReport, IUpdateDetailKPF
    {
		public Rap_PASHQK(){InitializeComponent();} 
        bool hapurgjitha = false;
        private int rritshuma = 0;
        //public Rap_PASH()
        //    {
        //    InitializeComponent();
        //    }
        public Rap_PASHQK(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_PASHQK(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            MonNder.Value = raport.Parameters["monedheNdermarje"].Value;
            xrLabel52.Text = raport.Parameters["filterDtDokQenderKosto"].Description;
            parameter1.Value = raport.Parameters["filterDtDokQenderKosto"].Value;
            xrLabel55.Text = raport.Parameters[0].Description;
            parameter2.Value = raport.Parameters[0].Value; 
            xrLabel90.Text = raport.Parameters[4].Description;
            parameter4.Value = raport.Parameters[4].Value;
            azhornimLabel.Text = raport.Parameters["Azhornim"].Description;
            Azhornim.Value = raport.Parameters["Azhornim"].Value;
            xrLabel18.Text = raport.Parameters[6].Description;
            parameter3.Value = raport.Parameters[6].Value;
            hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);
            EmrateLabelave(ci);
        }
        string[] shkronjevogel = { "", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t" };

        int niv = 0;
        int niv2 = 0;
        int niv3 = 0;
        private void xrLabel32_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("NIVELI").ToString() == "1")
                if (GetCurrentColumnValue("PRINDI").ToString().Contains("(a-b)"))
                    niv2++;
                else if (GetCurrentColumnValue("PRINDI").ToString().Contains("(a�d)"))
                    niv3++;
                else niv++;

        }

        private void xrLabel4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("NIVELI").ToString() == "2")
                if (GetCurrentColumnValue("PRINDI").ToString().Contains("(a-b)"))
                    niv2++;
                else if (GetCurrentColumnValue("PRINDI").ToString().Contains("(a�d)"))
                    niv3++;
                else niv++;
        }

        private void xrLabel8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("NIVELI").ToString() == "3")
                if (GetCurrentColumnValue("PRINDI").ToString().Contains("(a-b)"))
                    niv2++;
                else if (GetCurrentColumnValue("PRINDI").ToString().Contains("(a�d)"))
                    niv3++;
                else niv++;
        }

        private void xrLabel16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("NIVELI").ToString() == "4")
                if (GetCurrentColumnValue("PRINDI").ToString().Contains("(a-b)"))
                    niv2++;
                else if (GetCurrentColumnValue("PRINDI").ToString().Contains("(a�d)"))
                    niv3++;
                else niv++;
        }

        private void xrLabel23_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("NIVELI").ToString() == "5")
                if (GetCurrentColumnValue("PRINDI").ToString().Contains("(a-b)"))
                    niv2++;
                else if (GetCurrentColumnValue("PRINDI").ToString().Contains("(a�d)"))
                    niv3++;
                else niv++;
        }

        private void xrLabel73_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //  if (GetCurrentColumnValue("NIVELI").ToString() == "5")
            niv++;
        }

        private void xrLabel66_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //    if (GetCurrentColumnValue("NIVELI").ToString() == "4")
            niv++;
        }

        private void xrLabel61_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //     if (GetCurrentColumnValue("NIVELI").ToString() == "3")
            niv++;
        }

        private void xrLabel56_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // if (GetCurrentColumnValue("NIVELI").ToString() == "2")
            niv++;
        }

        private void xrLabel47_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //   if (GetCurrentColumnValue("NIVELI").ToString() == "1")
            niv++;
        }

        private void xrLabel79_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
        }

        private void xrLabel84_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrLabel32_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue("PRINDI").ToString().Contains("(a-b)"))
                e.Result = shkronjevogel[niv2];
            else if (GetCurrentColumnValue("PRINDI").ToString().Contains("(a�d)"))
                e.Result = shkronjevogel[niv3];
            else
                e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel4_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue("PRINDI").ToString().Contains("(a-b)"))
                e.Result = shkronjevogel[niv2];
            else if (GetCurrentColumnValue("PRINDI").ToString().Contains("(a�d)"))
                e.Result = shkronjevogel[niv3];
            else
                e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel8_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue("PRINDI").ToString().Contains("(a-b)"))
                e.Result = shkronjevogel[niv2];
            else if (GetCurrentColumnValue("PRINDI").ToString().Contains("(a�d)"))
                e.Result = shkronjevogel[niv3];
            else
                e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel16_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue("PRINDI").ToString().Contains("(a-b)"))
                e.Result = shkronjevogel[niv2];
            else if (GetCurrentColumnValue("PRINDI").ToString().Contains("(a�d)"))
                e.Result = shkronjevogel[niv3];
            else
                e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel23_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue("PRINDI").ToString().Contains("(a-b)"))
                e.Result = shkronjevogel[niv2];
            else if (GetCurrentColumnValue("PRINDI").ToString().Contains("(a�d)"))
                e.Result = shkronjevogel[niv3];
            else
                e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel73_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel66_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel61_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel56_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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
            e.Result = xrLabel79.Text;
            //e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel84_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = xrLabel84.Text;
            //e.Result = niv;
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
            object kodiKpf = GetCurrentColumnValue("kodikpf");
            if (kodiKpf == null || kodiKpf == DBNull.Value)
                return;
            object pershkrimi = GetCurrentColumnValue("PERSHKRIMIZERIT");
            if (pershkrimi == null || pershkrimi == DBNull.Value)
                return;
            string catid = kodiKpf.ToString();
            string catid2 = pershkrimi.ToString();
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
            object kodiKpf = GetCurrentColumnValue("kodikpf");
            if (kodiKpf == null || kodiKpf == DBNull.Value)
                return;
            string catid = kodiKpf.ToString();

            if (GetCurrentColumnValue("NRLLOGARI").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJLLOG") != null && GetCurrentColumnValue("SHFAQBIJLLOG").ToString() == "False"))
            {
                label.Text = "";
            }
            else
            {
                label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";Detail;teArdhuraShpenzimeQendraKosto')";
                label.Target = "_self";
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
            object pershkrimi = GetCurrentColumnValue("PERSHKRIMIZERIT");
            if (pershkrimi == null || pershkrimi == DBNull.Value)
                return;
            string catid = pershkrimi.ToString();

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
            object pershkrimi = GetCurrentColumnValue("PERSHKRIMIZERIT");
            if (pershkrimi == null || pershkrimi == DBNull.Value)
                return;
            string catid = pershkrimi.ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
                label.Text = "";
            else
            {
                label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;teArdhuraShpenzimeQendraKosto')";
                label.Target = "_self";
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
            object pershkrimi = GetCurrentColumnValue("PERSHKRIMIZERIT");
            if (pershkrimi == null || pershkrimi == DBNull.Value)
                return;
            string catid = pershkrimi.ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ2") != null && GetCurrentColumnValue("SHFAQBIJ2").ToString() == "False"))
                label.Text = "";
            else
            {
                label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;teArdhuraShpenzimeQendraKosto')";
                label.Target = "_self";
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
            object pershkrimi = GetCurrentColumnValue("PERSHKRIMIZERIT");
            if (pershkrimi == null || pershkrimi == DBNull.Value)
                return;
            string catid = pershkrimi.ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ3") != null && GetCurrentColumnValue("SHFAQBIJ3").ToString() == "False"))
                label.Text = "";
            else
            {
                label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;teArdhuraShpenzimeQendraKosto')";
                label.Target = "_self";
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
            object pershkrimi = GetCurrentColumnValue("PERSHKRIMIZERIT");
            if (pershkrimi == null || pershkrimi == DBNull.Value)
                return;
            string catid = pershkrimi.ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ4") != null && GetCurrentColumnValue("SHFAQBIJ4").ToString() == "False"))
                label.Text = "";
            else
            {
                label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;teArdhuraShpenzimeQendraKosto')";
                label.Target = "_self";
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
            object pershkrimi = GetCurrentColumnValue("PERSHKRIMIZERIT");
            if (pershkrimi == null || pershkrimi == DBNull.Value)
                return;
            string catid = pershkrimi.ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ5") != null && GetCurrentColumnValue("SHFAQBIJ5").ToString() == "False"))
                label.Text = "";
            else
            {
                label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;teArdhuraShpenzimeQendraKosto')";
                label.Target = "_self";
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

        private void xrLabel84_SummaryReset(object sender, EventArgs e)
        {
           

        }

        private void xrLabel79_SummaryReset(object sender, EventArgs e)
        {
            //niv = 0;
            //niv2 = 0;
            //niv3 = 0;
        }

        private void xrLabel13_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:n0}", shuma);
            else e.Result = "(" + String.Format("{0:n0}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel42_SummaryGetResult(object sender, SummaryGetResultEventArgs e) {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else
                    shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:n0}", shuma);
            else
                e.Result = "(" + String.Format("{0:n0}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel26_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:n0}", shuma);
            else e.Result = "(" + String.Format("{0:n0}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel27_SummaryGetResult(object sender, SummaryGetResultEventArgs e) {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else
                    shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:n0}", shuma);
            else
                e.Result = "(" + String.Format("{0:n0}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel29_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:n0}", shuma);
            else e.Result = "(" + String.Format("{0:n0}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel30_SummaryGetResult(object sender, SummaryGetResultEventArgs e) {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else
                    shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:n0}", shuma);
            else
                e.Result = "(" + String.Format("{0:n0}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel36_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:n0}", shuma);
            else e.Result = "(" + String.Format("{0:n0}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel37_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:n0}", shuma);
            else e.Result = "(" + String.Format("{0:n0}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel39_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:n0}", shuma);
            else e.Result = "(" + String.Format("{0:n0}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel40_SummaryGetResult(object sender, SummaryGetResultEventArgs e) {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else
                    shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:n0}", shuma);
            else
                e.Result = "(" + String.Format("{0:n0}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel45_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:n0}", shuma);
            else e.Result = "(" + String.Format("{0:n0}", -shuma) + ")";
            e.Handled = true;
        }
        private void xrLabel46_SummaryGetResult(object sender, SummaryGetResultEventArgs e) {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else
                    shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:n0}", shuma);
            else
                e.Result = "(" + String.Format("{0:n0}", -shuma) + ")";
            e.Handled = true;
        }
        private void xrLabel74_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:n0}", shuma);
            else e.Result = "(" + String.Format("{0:n0}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel78_SummaryGetResult(object sender, SummaryGetResultEventArgs e) {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else
                    shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:n0}", shuma);
            else
                e.Result = "(" + String.Format("{0:n0}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel67_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:n0}", shuma);
            else e.Result = "(" + String.Format("{0:n0}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel72_SummaryGetResult(object sender, SummaryGetResultEventArgs e) {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else
                    shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:n0}", shuma);
            else
                e.Result = "(" + String.Format("{0:n0}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel64_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:n0}", shuma);
            else e.Result = "(" + String.Format("{0:n0}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel65_SummaryGetResult(object sender, SummaryGetResultEventArgs e) {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else
                    shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:n0}", shuma);
            else
                e.Result = "(" + String.Format("{0:n0}", -shuma) + ")";
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
                e.Result = String.Format("{0:n0}", shuma);
            else e.Result = "(" + String.Format("{0:n0}", -shuma) + ")";
            e.Handled = true;
        }
        private void xrLabel60_SummaryGetResult(object sender, SummaryGetResultEventArgs e) {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else
                    shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:n0}", shuma);
            else
                e.Result = "(" + String.Format("{0:n0}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel50_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:n0}", shuma);
            else e.Result = "(" + String.Format("{0:n0}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel51_SummaryGetResult(object sender, SummaryGetResultEventArgs e) {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else
                    shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:n0}", shuma);
            else
                e.Result = "(" + String.Format("{0:n0}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel82_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:n0}", shuma);
            else e.Result = "(" + String.Format("{0:n0}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel83_SummaryGetResult(object sender, SummaryGetResultEventArgs e) {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else
                    shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = String.Format("{0:n0}", shuma);
            else
                e.Result = "(" + String.Format("{0:n0}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel11_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double shuma = 0;
            XRLabel label = sender as XRLabel;
            if (string.IsNullOrEmpty(label.Text))
                return;
            if (label.Text.Contains("("))
                shuma = -double.Parse(label.Text.ToString().Replace('(', ' ').Replace(')', ' '));
            else shuma = double.Parse(label.Text.ToString());
            if (shuma >= 0)
                label.Text = String.Format("{0:n0}", shuma);
            else label.Text = "(" + String.Format("{0:n0}", -shuma) + ")";
        }

        private void xrLabel43_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) {
            double shuma = 0;
            XRLabel label = sender as XRLabel;
            if (string.IsNullOrEmpty(label.Text))
                return;
            if (label.Text.Contains("("))
                shuma = -double.Parse(label.Text.ToString().Replace('(', ' ').Replace(')', ' '));
            else
                shuma = double.Parse(label.Text.ToString());
            if (shuma >= 0)
                label.Text = String.Format("{0:n0}", shuma);
            else
                label.Text = "(" + String.Format("{0:n0}", -shuma) + ")";
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
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
            xrLabel19.Text = rm.GetString("labelRaportiEmertimi", ci);
            xrLabel7.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel20.Text = rm.GetString("labelRaportiShenime", ci);
            xrLabel21.Text = rm.GetString("labelRaportiVitiRaportues", ci);
            xrLabel22.Text = rm.GetString("labelRaportiVitiParaardhes", ci);
            xrLabel80.Text = rm.GetString("labelRaportiFitimiNetoVitFinanciar", ci);
            xrLabel1.Text = rm.GetString("labelRaportiNr", ci);
            xrLabel85.Text = rm.GetString("labelElementeTePasqyraveTeKonsoliduara", ci);
            xrLabel70.Text = rm.GetString("filterMonedha", ci);
        }

        private void xrLabel79_AfterPrint(object sender, EventArgs e)
        {
            if (xrLabel79.Text != "")
                xrLabel79.Text = (Convert.ToInt16(xrLabel79.Text) + 1 + rritshuma).ToString();
        }

        private void xrLabel84_AfterPrint(object sender, EventArgs e)
        {
            if (xrLabel84.Text != "")
                xrLabel84.Text = (Convert.ToInt16(xrLabel84.Text) + 2 + rritshuma).ToString();
        }

        private void xrLabel32_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (xrLabel32.Text != "" && xrLabel6.Text == " Te pacaktuara")
            {
                xrLabel32.Text = (Convert.ToInt16(xrLabel32.Text) + 1).ToString();
                rritshuma = 1;
            }
        }
    }
}
