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
    public partial class Rap_Analiza_Ardhurave_Shpenzimeve : DevExpress.XtraReports.UI.XtraReport, IUpdateDetailKPF

    {
        public Rap_Analiza_Ardhurave_Shpenzimeve(){InitializeComponent();} 
        bool hapurgjitha = false;
        private int rritshuma = 0;
        //public Rap_PASH()
        //    {
        //    InitializeComponent();
        //    }
        public Rap_Analiza_Ardhurave_Shpenzimeve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_Analiza_Ardhurave_Shpenzimeve(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            MonNder.Value = raport.Parameters[5].Value;
            xrLabel70.Text = raport.Parameters[5].Description;
            xrLabel52.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel55.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            azhornimLabel.Text = raport.Parameters[3].Description;
            Azhornim.Value = raport.Parameters[3].Value;
            xrLabel18.Text = raport.Parameters[4].Description;
            parameter3.Value = raport.Parameters[4].Value;
            hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);

            //xrPictureBox1.ImageUrl = @"/images/RaporteLogo.bmp";
            EmrateLabelave(ci);
        }
        string[] shkronjevogel = { "", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t" };

        int niv = 0;
     

      
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
            string catid = GetCurrentColumnValue("kodikpf").ToString();
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

        private void lblKPF_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            string catid = GetCurrentColumnValue("kodikpf").ToString();


            if (GetCurrentColumnValue("NRLLOGARI").ToString() == ""||( GetCurrentColumnValue("SHFAQBIJLLOG")!=null && GetCurrentColumnValue("SHFAQBIJLLOG").ToString()=="False"))
            {
                label.Text = "";
            }
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";Detail;analizeArdhuraShpenzime')";
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
            string catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();

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
            string catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;analizeArdhuraShpenzime')";
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
            string catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ2") != null && GetCurrentColumnValue("SHFAQBIJ2").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;analizeArdhuraShpenzime')";
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
            string catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ3") != null && GetCurrentColumnValue("SHFAQBIJ3").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;analizeArdhuraShpenzime')";
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
            string catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ4") != null && GetCurrentColumnValue("SHFAQBIJ4").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;analizeArdhuraShpenzime')";
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
            string catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ5") != null && GetCurrentColumnValue("SHFAQBIJ5").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;analizeArdhuraShpenzime')";
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

   

        private void xrLabel13_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
              
                    if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel26_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel29_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel36_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel39_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel45_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel74_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel67_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel64_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel59_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel50_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel82_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel11_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double shuma = 0;
            XRLabel label = sender as XRLabel;
            if (label.Text.Contains("("))
                shuma = -double.Parse(label.Text.ToString().Replace('(', ' ').Replace(')', ' '));
            else shuma = double.Parse(label.Text.ToString());
            if (shuma >= 0)
                label.Text = String.Format("{0:#,#.00}", shuma);
            else label.Text = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
        }

       

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("lblTitulliAnalizaETeArdhuraveShpenzimeve", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            //xrTableCell81.Text = rm.GetString("labelRaportiEmertimi", ci);
            xrLabel7.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell81.Text = rm.GetString("lblTreguesit", ci);
            //xrTableCell84.Text = rm.GetString("labelRaportiVitiRaportues", ci);
            //xrTableCell83.Text = rm.GetString("labelRaportiVitiParaardhes", ci);
            //xrTableCell56.Text = rm.GetString("labelbuxhetorardhura", ci);
            xrTableCell85.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell42.Text = rm.GetString("labelbuxhetorrezultatfunksionimit", ci);
            //xrTableCell77.Text = rm.GetString("labelbuxhetorgrante", ci);
            //xrLabel70.Text = rm.GetString("filterMonedha", ci);
        }

       

        //private void xrLabel79_AfterPrint(object sender, EventArgs e)
        //{
        //    if (xrLabel79.Text != "")
        //        xrLabel79.Text = (Convert.ToInt16(xrLabel79.Text) + 1+rritshuma).ToString();
        //}

        //private void xrLabel84_AfterPrint(object sender, EventArgs e)
        //{
        //    if (xrLabel84.Text != "")
        //        xrLabel84.Text = (Convert.ToInt16(xrLabel84.Text) + 2+rritshuma).ToString();
        //}

      

        //private void xrLabel32_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    if (xrLabel32.Text != "" && xrTableCell38.Text == " Te pacaktuara")
        //    {
        //        xrLabel32.Text = (Convert.ToInt16(xrLabel32.Text) + 1).ToString();
        //        rritshuma = 1;
        //    }
        //}

        private void xrTableCell40_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //xrLabel45_SummaryGetResult
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            gjend1 = shuma;
            e.Handled = true;
        }

        
        private void xrTableCell36_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //lblPrindi1_BeforePrint
            XRTableCell label = sender as XRTableCell;
            string catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;analizeArdhuraShpenzime')";
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

        private void xrTableCell1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //lblprindi2_BeforePrint
            XRTableCell label = sender as XRTableCell;
            string catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ2") != null && GetCurrentColumnValue("SHFAQBIJ2").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;analizeArdhuraShpenzime')";
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

        private void xrTableCell5_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //xrLabel39_SummaryGetResult
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrTableCell7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //lblprindi3_BeforePrint
            XRTableCell label = sender as XRTableCell;
            string catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ3") != null && GetCurrentColumnValue("SHFAQBIJ3").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;analizeArdhuraShpenzime')";
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

        private void xrTableCell11_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //xrLabel36_SummaryGetResult
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrTableCell13_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //lblprindi4_BeforePrint
            XRTableCell label = sender as XRTableCell;
            string catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ4") != null && GetCurrentColumnValue("SHFAQBIJ4").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;analizeArdhuraShpenzime')";
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

        private void xrTableCell17_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //xrLabel29_SummaryGetResult
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrTableCell23_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //xrLabel26_SummaryGetResult
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrTableCell19_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //lblprindi5_BeforePrint
            XRTableCell label = sender as XRTableCell;
            string catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ5") != null && GetCurrentColumnValue("SHFAQBIJ5").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;analizeArdhuraShpenzime')";
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

        private void xrTableCell26_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //lblKPF_BeforePrint
            XRTableCell label = sender as XRTableCell;
            string catid = GetCurrentColumnValue("kodikpf").ToString();
            if (GetCurrentColumnValue("NRLLOGARI").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJLLOG") != null && GetCurrentColumnValue("SHFAQBIJLLOG").ToString() == "False"))
            {
                label.Text = "";
            }
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";Detail;analizeArdhuraShpenzime')";
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

        private void xrTableCell30_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //xrLabel13_SummaryGetResult
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrTableCell43_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //xrLabel11_BeforePrint
            double shuma = 0;
            XRTableCell label = sender as XRTableCell;
            if (label.Text.Contains("("))
                shuma = -double.Parse(label.Text.ToString().Replace('(', ' ').Replace(')', ' '));
            else shuma = double.Parse(label.Text.ToString());
            if (shuma >= 0)
                label.Text = String.Format("{0:#,#.00}", shuma);
            else label.Text = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
        }

        private void xrTableCell50_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //xrLabel74_SummaryGetResult
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrTableCell55_AfterPrint(object sender, EventArgs e)
        {
            //int nr = 0;
            ////xrLabel79_AfterPrint
            //if (xrTableCell55.Text != "")
            //{
            //    int.TryParse(xrTableCell55.Text, out nr);

            //}
            //    xrTableCell55.Text = (nr+ 1 + rritshuma).ToString();
        }


        double shumaCalcField1 = 0;
        double shumaCalcField2 = 0;
        double shumaCalcField3 = 0;
        private void xrTableCell59_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            ndryshimiAbsolut1 = shumaCalcField1 - shumaCalcField1Para;
            e.Result = ndryshimiAbsolut1;
            e.Handled = true;
            
        }

     

     

        private void xrTableCell68_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
          
        }

        private void xrTableCell73_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //xrLabel59_SummaryGetResult
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrTableCell78_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //xrLabel50_SummaryGetResult
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrTableCell4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
        }

        private void xrTableCell4_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrTableCell10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
        }

        private void xrTableCell16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
        }

        private void xrTableCell22_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
        }

        private void xrTableCell29_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
        }

        private void xrTableCell10_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrTableCell16_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrTableCell22_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrTableCell29_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void ReportFooter_AfterPrint(object sender, EventArgs e)
        {
            niv = 0;
        }

       
        private void xrTableCell49_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrTableCell82_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
            {
                e.Result = String.Format("{0:#,#.00}", shuma);
                shumaCalcField3 = shuma;
            }
            else
            {
                e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
                shumaCalcField3 = Convert.ToDouble(-shuma);
            }
            e.Handled = true;
        }

        private void xrTableCell53_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        //private void xrTableCell25_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    niv++;
        //    xrTableCell25.Text = niv.ToString();
        //}

        //private void xrTableCell57_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    niv++;
        //    xrTableCell57.Text = niv.ToString();
        //}

        //private void xrTableCell67_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    niv++;
        //    xrTableCell67.Text = niv.ToString();
        //}

        

        private void xrTableCell49_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else
                e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            shumaCalcField2 = shuma;
            e.Handled = true;
        }
        double shumaCalcField1Para = 0;
        double shumaCalcField2Para = 0;
        double shumaCalcField3Para = 0;
        private void xrTableCell58_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else
                e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            shumaCalcField1Para = shuma;
            e.Handled = true;
        }

        private void xrTableCell52_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else
                e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            shumaCalcField2Para = shuma;
            e.Handled = true;
        }

        private void xrTableCell86_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else
                e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            shumaCalcField3Para = shuma;
            e.Handled = true;
        }

        double ndryshimiAbsolut1 = 0;
        double ndryshimiAbsolut2 = 0;
        double ndryshimiAbsolut3 = 0;
        private void xrTableCell4_SummaryGetResult_1(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else
                e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            shumaCalcField1 = shuma;
            e.Handled = true;
        }

        private void xrTableCell10_SummaryGetResult_1(object sender, SummaryGetResultEventArgs e)
        {
            ndryshimiAbsolut2 = shumaCalcField2 - shumaCalcField2Para;
            if (ndryshimiAbsolut2 >= 0)
                e.Result = String.Format("{0:#,#.00}", ndryshimiAbsolut2); 
            else
                e.Result = "(" + String.Format("{0:#,#.00}", -ndryshimiAbsolut2) + ")"; 
            e.Handled = true;
        }

        private void xrTableCell22_SummaryGetResult_1(object sender, SummaryGetResultEventArgs e)
        {
            ndryshimiAbsolut3 = shumaCalcField3 - shumaCalcField3Para;
            if (ndryshimiAbsolut3 >= 0)
                e.Result = String.Format("{0:#,#.00}", ndryshimiAbsolut3);
            else
                e.Result = "(" + String.Format("{0:#,#.00}", -ndryshimiAbsolut3) + ")"; 
            e.Handled = true;
        }

        private void xrTableCell80_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
           double ndryshimePerqindje = shumaCalcField1Para == 0 ? 0 : (ndryshimiAbsolut1 / shumaCalcField1Para) * 100;
           if (ndryshimePerqindje >= 0)
               e.Result = String.Format("{0:#,#.00}", ndryshimePerqindje);
            else
               e.Result = "(" + String.Format("{0:#,#.00}", -ndryshimePerqindje) + ")"; 
            e.Handled = true;
        }

        private void xrTableCell60_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double ndryshimePerqindje = shumaCalcField2Para == 0 ? 0 : (ndryshimiAbsolut2 / shumaCalcField2Para) * 100;
            if (ndryshimePerqindje >= 0)
                e.Result = String.Format("{0:#,#.00}", ndryshimePerqindje);
            else
                e.Result = "(" + String.Format("{0:#,#.00}", -ndryshimePerqindje) + ")"; 
            e.Handled = true;
        }

        private void xrTableCell29_SummaryGetResult_1(object sender, SummaryGetResultEventArgs e)
        {
            double ndryshimePerqindje = shumaCalcField3Para == 0 ? 0 : (ndryshimiAbsolut3 / shumaCalcField3Para) * 100;
            if (ndryshimePerqindje >= 0)
                e.Result = String.Format("{0:#,#.00}", ndryshimePerqindje);
            else
                e.Result = "(" + String.Format("{0:#,#.00}", -ndryshimePerqindje) + ")"; 
            e.Handled = true;
        }

        private void xrTableCell101_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }
        double gjend1 = 0;
        double gjend2 = 0;
        double gjend3 = 0;
        double gjend4 = 0;
        double gjend5 = 0;
        double gjend6 = 0;
        double gjend7 = 0;
        double gjend8 = 0;
        double gjend9 = 0;
        double gjend10 = 0;
        double gjend11 = 0;
        double gjend12 = 0;
        double gjend13 = 0;
        double gjend14 = 0;
        double gjendVitiPara1 = 0;
        double gjendVitiPara2 = 0;
        double gjendVitiPara3 = 0;
        double gjendVitiPara4 = 0;
        double gjendVitiPara5 = 0;
        double gjendVitiPara6 = 0;
        double gjendVitiPara7 = 0;
        double gjendVitiPara8 = 0;
        double gjendVitiPara9 = 0;
        double gjendVitiPara10 = 0;
        double gjendVitiPara11 = 0;
        double gjendVitiPara12 = 0;
        double gjendVitiPara13 = 0;
        double gjendVitiPara14 = 0;
        double ndryshimAbsolut1 = 0;
        double ndryshimAbsolut2 = 0;
        double ndryshimAbsolut3 = 0;
        double ndryshimAbsolut4 = 0;
        double ndryshimAbsolut5 = 0;
        double ndryshimAbsolut6 = 0;
        double ndryshimAbsolut7 = 0;
        double ndryshimAbsolut8 = 0;
        double ndryshimAbsolut9 = 0;
        double ndryshimAbsolut10 = 0;
        double ndryshimAbsolut11 = 0;
        double ndryshimAbsolut12 = 0;
        double ndryshimAbsolut13 = 0;
        double ndryshimAbsolut14 = 0;

        private void xrTableCell59_AfterPrint(object sender, EventArgs e)
        {
            double shuma = shumaCalcField1Para - shumaCalcField1;
            if (shuma >= 0)
                xrTableCell59.Text = String.Format("{0:#,#.00}", shuma);
            else
                xrTableCell59.Text = "(" + String.Format("{0:#,#.00}", -shuma) + ")"; 
        }

        private void xrTableCell63_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            //if (e.Value.ToString().Contains("("))
            //    gjend1 = -double.Parse(e.Value.ToString().Replace('(', ' ').Replace(')', ' '));
            //else gjend1 = Convert.ToDouble(e.Value);
        }

        private void xrTableCell64_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            //if (e.Value.ToString().Contains("("))
            //    gjendVitiPara1 = -double.Parse(e.Value.ToString().Replace('(', ' ').Replace(')', ' '));
            //else gjendVitiPara1 = Convert.ToDouble(e.Value);
        }

        private void xrTableCell89_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
            //XRTableCell label = sender as XRTableCell;
            //label.Text = Convert.ToString(ndryshimAbsolut1 = gjend1 - gjendVitiPara1);
            //xrTableCell89.Text = Convert.ToString(Convert.ToDouble(xrTableCell63.Summary.GetResult()) - Convert.ToDouble(xrTableCell64.Summary.GetResult()));
        //e.Handled = true;
        }

        private void xrTableCell90_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //XRTableCell label = sender as XRTableCell;
            //label.Text = Convert.ToString(gjendVitiPara1 == 0 ? 0 : ndryshimAbsolut1 /gjendVitiPara1);
            String vlera1, vlera2;
            double ndryshAbs1, gjendPara;
            vlera1 = Convert.ToString(xrTableCell89.Summary.GetResult());
            vlera2 = Convert.ToString(xrTableCell41.Summary.GetResult());
            if (vlera1.Contains("("))
                ndryshAbs1 = -double.Parse(vlera1.Replace('(', ' ').Replace(')', ' '));
            else ndryshAbs1 = double.Parse(vlera1);
            if (vlera2.Contains("("))
                gjendPara = -double.Parse(vlera2.Replace('(', ' ').Replace(')', ' '));
            else gjendPara = double.Parse(vlera2);
            double ndryshimiPerqindje = gjendPara == 0 ? 0 : (ndryshAbs1 / gjendPara) * 100;
            if (ndryshimiPerqindje >= 0)
                e.Result = String.Format("{0:#,#.00}", ndryshimiPerqindje);
            else
                e.Result = "(" + String.Format("{0:#,#.00}", -ndryshimiPerqindje) + ")";
            e.Handled = true;
        }

        private void xrTableCell89_AfterPrint(object sender, EventArgs e)
        {

        }

        private void xrTableCell41_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            gjendVitiPara1 = shuma;
            e.Handled = true;
        }

        private void xrTableCell6_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrTableCell12_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrTableCell18_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrTableCell24_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrTableCell31_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrTableCell44_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            double shuma = 0;
            XRTableCell label = sender as XRTableCell;
            if (label.Text.Contains("("))
                shuma = -double.Parse(label.Text.ToString().Replace('(', ' ').Replace(')', ' '));
            else shuma = double.Parse(label.Text.ToString());
            if (shuma >= 0)
                label.Text = String.Format("{0:#,#.00}", shuma);
            else label.Text = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
        }

        private void xrTableCell51_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrTableCell54_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrTableCell69_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrTableCell74_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrTableCell79_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrTableCell63_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double ndryshimeAbsolut = gjend1 - gjendVitiPara1;
            if (ndryshimeAbsolut >= 0)
                e.Result = String.Format("{0:#,#.00}", ndryshimeAbsolut);
            else
                e.Result = "(" + String.Format("{0:#,#.00}", -ndryshimeAbsolut) + ")";
            e.Handled = true;
        }

        private void xrTableCell89_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           // xrTableCell89.Text = Convert.ToString(Convert.ToDouble(xrTableCell63.Summary.GetResult()) - Convert.ToDouble(xrTableCell64.Summary.GetResult()));
        }

        private void xrTableCell91_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
            //object ndryshimiAbs = GetCurrentColumnValue("ndryshimiAbsolut");
            //if (ndryshimiAbs != null && ndryshimiAbs != DBNull.Value)
            //{
            //    if (Convert.ToDouble(ndryshimiAbs) >= 0)
            //        xrTableCell91.Text = String.Format("{0:#,#.00}", ndryshimiAbs);
            //    else
            //        xrTableCell91.Text = "(" + String.Format("{0:#,#.00}", -Convert.ToDouble(ndryshimiAbs)) + ")";
            //}
            //String vlera1, vlera2;
            //double shuma1, shuma2;
            //vlera1 = Convert.ToString(xrTableCell40.Summary.GetResult());
            //vlera2 = Convert.ToString(xrTableCell41.Summary.GetResult());
            //if (vlera1.Contains("(")) 
            //    shuma1 = -double.Parse(vlera1.Replace('(', ' ').Replace(')', ' '));
            //else shuma1 = double.Parse(vlera1);
            //if (vlera2.Contains("("))
            //    shuma2 = -double.Parse(vlera2.Replace('(', ' ').Replace(')', ' '));
            //else shuma2 = double.Parse(vlera2);
            //double ndryshimeAbsolut = shuma1 - shuma2;
            //if (ndryshimeAbsolut >= 0)
            //    e.Result = String.Format("{0:#,#.00}", ndryshimeAbsolut);
            //else
            //    e.Result = "(" + String.Format("{0:#,#.00}", -ndryshimeAbsolut) + ")";
            //e.Handled = true;
            //e.Result = Convert.ToDouble(xrTableCell40.Summary.GetResult()) - Convert.ToDouble(xrTableCell41.Summary.GetResult());
        }

        private void xrTableCell93_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {

        }

        private void xrTableCell93_SummaryGetResult_1(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
            //object ndryshimiAbs = GetCurrentColumnValue("ndryshimiAbsolut");
            //if (ndryshimiAbs != null && ndryshimiAbs != DBNull.Value)
            //{
            //    if (Convert.ToDouble(ndryshimiAbs) >= 0)
            //        xrTableCell93.Text = String.Format("{0:#,#.00}", ndryshimiAbs);
            //    else
            //        xrTableCell93.Text = "(" + String.Format("{0:#,#.00}", -Convert.ToDouble(ndryshimiAbs)) + ")";
            //}
            //double shuma = 0;
            //for (int i = 0; i < e.CalculatedValues.Count; i++)
            //    if (e.CalculatedValues[i].ToString().Contains("("))
            //        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
            //    else shuma += double.Parse(e.CalculatedValues[i].ToString());
            //if (shuma >= 0)
            //    e.Result = String.Format("{0:#,#.00}", shuma);
            //else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            //e.Handled = true;
        }

        private void xrTableCell94_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double ndryshimePerq = gjenVitPara2 == 0 ? 0 : (ndrAbs2 / gjenVitPara2) * 100;
            if (ndryshimePerq >= 0)
                e.Result = String.Format("{0:#,#.00}", ndryshimePerq);
            else
                e.Result = "(" + String.Format("{0:#,#.00}", -ndryshimePerq) + ")";
            e.Handled = true;
        }

        private void xrTableCell16_SummaryGetResult_1(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        double ndrAbs1, gjenVitPara1,ndrAbs2, gjenVitPara2,ndrAbs3, gjenVitPara3,ndrAbs4, gjenVitPara4,ndrAbs5, gjenVitPara5,ndrAbs6, gjenVitPara6,ndrAbs7, gjenVitPara7,ndrAbs8, gjenVitPara8,ndrAbs9, gjenVitPara9,ndrAbs10, gjenVitPara10,ndrAbs11, gjenVitPara11,ndrAbs12, gjenVitPara12;
        private void xrTableCell92_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double ndryshimePerq = gjenVitPara1 == 0 ? 0 : (ndrAbs1 / gjenVitPara1) * 100;
            if (ndryshimePerq >= 0)
                e.Result = String.Format("{0:#,#.00}", ndryshimePerq);
            else
                e.Result = "(" + String.Format("{0:#,#.00}", -ndryshimePerq) + ")";
            e.Handled = true;
        }

        private void xrTableCell92_SummaryReset(object sender, EventArgs e)
        {
            ndrAbs1 = 0;
            gjenVitPara1 = 0;
        }
   //if (GetCurrentColumnValue("ndryshimiAbsolut") != null && GetCurrentColumnValue("gjendVitiParaardhes") != null)
            //{
                //vlera1 = Convert.ToString(xrTableCell41.Summary.GetResult());
                //vlera2 = Convert.ToString(xrTableCell91.Summary.GetResult());
                //if (vlera1.Contains("("))
                //    gjenVitPara1 = -double.Parse(vlera1.Replace('(', ' ').Replace(')', ' '));
                //else gjenVitPara1 = double.Parse(vlera1);
                //if (vlera2.Contains("("))
                //    ndrAbs = -double.Parse(vlera2.Replace('(', ' ').Replace(')', ' '));
                //else ndrAbs = double.Parse(vlera2);
                //shumandrAbs1 += ndrAbs;
                //shumagjenVitiPara1 += gjenVitPara1;
           // }
//            Iif([shenja] == 'Pozitive',
//Iif([gjendVitiParaardhes] >= 0,[gjendVitiParaardhes]  ,'('+ -[gjendVitiParaardhes]+')' ),
// Iif([gjendVitiParaardhes] > 0,'('+[gjendVitiParaardhes]+')'  , -[gjendVitiParaardhes] ))
        //if (GetCurrentColumnValue("ndryshimiAbsolut") != null && GetCurrentColumnValue("gjendVitiParaardhesMeShenje") != null)
        //    {
        //        object shenja = GetCurrentColumnValue("shenja");
        //        double gjendja = Convert.ToDouble(GetCurrentColumnValue("gjendVitiParaardhesMeShenje"));
        //        double ndryshimiAbs = Convert.ToDouble(GetCurrentColumnValue("ndryshimiAbsolut"));
        //        if (shenja != null && shenja != DBNull.Value && shenja.ToString() == "Pozitive")
        //            gjenVitPara1 += gjendja;
        //        else
        //            gjenVitPara1 += -gjendja;
        //        if (shenja != null && shenja != DBNull.Value && shenja.ToString() == "Pozitive")
        //            ndrAbs += ndryshimiAbs;
        //        else
        //            ndrAbs += -ndryshimiAbs;
        //    }
        private void xrTableCell92_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("ndryshimiAbsolut") != null && GetCurrentColumnValue("gjendVitiParaardhesMeShenje") != null)
            {
                gjenVitPara1 += Convert.ToDouble(GetCurrentColumnValue("gjendVitiParaardhesMeShenje"));
                ndrAbs1 += Convert.ToDouble(GetCurrentColumnValue("ndryshimiAbsolut"));
            }
        }

        private void xrTableCell91_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrTableCell93_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrTableCell95_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrTableCell97_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrTableCell99_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrTableCell103_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrTableCell105_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrTableCell107_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrTableCell109_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrTableCell111_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i] != null)
                {
                    if (e.CalculatedValues[i].ToString().Contains("("))
                        shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                    else shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrTableCell94_SummaryReset(object sender, EventArgs e)
        {
            ndrAbs2 = 0;
            gjenVitPara2 = 0;
        }

        private void xrTableCell94_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("ndryshimiAbsolut") != null && GetCurrentColumnValue("gjendVitiParaardhesMeShenje") != null)
            {
                gjenVitPara2 += Convert.ToDouble(GetCurrentColumnValue("gjendVitiParaardhesMeShenje"));
                ndrAbs2 += Convert.ToDouble(GetCurrentColumnValue("ndryshimiAbsolut"));
            }
        }

        private void xrTableCell113_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("ndryshimiAbsolut") != null && GetCurrentColumnValue("gjendVitiParaardhesMeShenje") != null)
            {
                gjenVitPara3 += Convert.ToDouble(GetCurrentColumnValue("gjendVitiParaardhesMeShenje"));
                ndrAbs3 += Convert.ToDouble(GetCurrentColumnValue("ndryshimiAbsolut"));
            }
        }

        private void xrTableCell96_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("ndryshimiAbsolut") != null && GetCurrentColumnValue("gjendVitiParaardhesMeShenje") != null)
            {
                gjenVitPara4 += Convert.ToDouble(GetCurrentColumnValue("gjendVitiParaardhesMeShenje"));
                ndrAbs4 += Convert.ToDouble(GetCurrentColumnValue("ndryshimiAbsolut"));
            }
        }

        private void xrTableCell98_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("ndryshimiAbsolut") != null && GetCurrentColumnValue("gjendVitiParaardhesMeShenje") != null)
            {
                gjenVitPara5 += Convert.ToDouble(GetCurrentColumnValue("gjendVitiParaardhesMeShenje"));
                ndrAbs5 += Convert.ToDouble(GetCurrentColumnValue("ndryshimiAbsolut"));
            }
        }

        private void xrTableCell100_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("ndryshimiAbsolut") != null && GetCurrentColumnValue("gjendVitiParaardhesMeShenje") != null)
            {
                gjenVitPara6 += Convert.ToDouble(GetCurrentColumnValue("gjendVitiParaardhesMeShenje"));
                ndrAbs6 += Convert.ToDouble(GetCurrentColumnValue("ndryshimiAbsolut"));
            }
        }

        private void xrTableCell102_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("ndryshimiAbsolut") != null && GetCurrentColumnValue("gjendVitiParaardhesMeShenje") != null)
            {
                gjenVitPara7 += Convert.ToDouble(GetCurrentColumnValue("gjendVitiParaardhesMeShenje"));
                ndrAbs7 += Convert.ToDouble(GetCurrentColumnValue("ndryshimiAbsolut"));
            }
        }

        private void xrTableCell104_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("ndryshimiAbsolut") != null && GetCurrentColumnValue("gjendVitiParaardhesMeShenje") != null)
            {
                gjenVitPara8 += Convert.ToDouble(GetCurrentColumnValue("gjendVitiParaardhesMeShenje"));
                ndrAbs8 += Convert.ToDouble(GetCurrentColumnValue("ndryshimiAbsolut"));
            }
        }

        private void xrTableCell106_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("ndryshimiAbsolut") != null && GetCurrentColumnValue("gjendVitiParaardhesMeShenje") != null)
            {
                gjenVitPara9 += Convert.ToDouble(GetCurrentColumnValue("gjendVitiParaardhesMeShenje"));
                ndrAbs9 += Convert.ToDouble(GetCurrentColumnValue("ndryshimiAbsolut"));
            }
        }

        private void xrTableCell108_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("ndryshimiAbsolut") != null && GetCurrentColumnValue("gjendVitiParaardhesMeShenje") != null)
            {
                gjenVitPara10 += Convert.ToDouble(GetCurrentColumnValue("gjendVitiParaardhesMeShenje"));
                ndrAbs10 += Convert.ToDouble(GetCurrentColumnValue("ndryshimiAbsolut"));
            }
        }

        private void xrTableCell110_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("ndryshimiAbsolut") != null && GetCurrentColumnValue("gjendVitiParaardhesMeShenje") != null)
            {
                gjenVitPara11 += Convert.ToDouble(GetCurrentColumnValue("gjendVitiParaardhesMeShenje"));
                ndrAbs11 += Convert.ToDouble(GetCurrentColumnValue("ndryshimiAbsolut"));
            }
        }

        private void xrTableCell112_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("ndryshimiAbsolut") != null && GetCurrentColumnValue("gjendVitiParaardhesMeShenje") != null)
            {
                gjenVitPara12 += Convert.ToDouble(GetCurrentColumnValue("gjendVitiParaardhesMeShenje"));
                ndrAbs12 += Convert.ToDouble(GetCurrentColumnValue("ndryshimiAbsolut"));
            }
        }

        private void xrTableCell113_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double ndryshimePerq = gjenVitPara3 == 0 ? 0 : (ndrAbs3 / gjenVitPara3) * 100;
            if (ndryshimePerq >= 0)
                e.Result = String.Format("{0:#,#.00}", ndryshimePerq);
            else
                e.Result = "(" + String.Format("{0:#,#.00}", -ndryshimePerq) + ")";
            e.Handled = true;
        }

        private void xrTableCell96_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double ndryshimePerq = gjenVitPara4 == 0 ? 0 : (ndrAbs4 / gjenVitPara4) * 100;
            if (ndryshimePerq >= 0)
                e.Result = String.Format("{0:#,#.00}", ndryshimePerq);
            else
                e.Result = "(" + String.Format("{0:#,#.00}", -ndryshimePerq) + ")";
            e.Handled = true;
        }

        private void xrTableCell98_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double ndryshimePerq = gjenVitPara5 == 0 ? 0 : (ndrAbs5 / gjenVitPara5) * 100;
            if (ndryshimePerq >= 0)
                e.Result = String.Format("{0:#,#.00}", ndryshimePerq);
            else
                e.Result = "(" + String.Format("{0:#,#.00}", -ndryshimePerq) + ")";
            e.Handled = true;
        }

        private void xrTableCell100_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double ndryshimePerq = gjenVitPara6 == 0 ? 0 : ndrAbs6 / gjenVitPara6;
            if (ndryshimePerq >= 0)
                e.Result = String.Format("{0:#,#.00}", ndryshimePerq);
            else
                e.Result = "(" + String.Format("{0:#,#.00}", -ndryshimePerq) + ")";
            e.Handled = true;
        }

        private void xrTableCell102_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double ndryshimePerq = gjenVitPara7 == 0 ? 0 : (ndrAbs7 / gjenVitPara7) * 100;
            if (ndryshimePerq >= 0)
                e.Result = String.Format("{0:#,#.00}", ndryshimePerq);
            else
                e.Result = "(" + String.Format("{0:#,#.00}", -ndryshimePerq) + ")";
            e.Handled = true;
        }

        private void xrTableCell104_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double ndryshimePerq = gjenVitPara8 == 0 ? 0 : (ndrAbs8 / gjenVitPara8) * 100;
            if (ndryshimePerq >= 0)
                e.Result = String.Format("{0:#,#.00}", ndryshimePerq);
            else
                e.Result = "(" + String.Format("{0:#,#.00}", -ndryshimePerq) + ")";
            e.Handled = true;
        }

        private void xrTableCell106_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double ndryshimePerq = gjenVitPara9 == 0 ? 0 : (ndrAbs9 / gjenVitPara9) * 100;
            if (ndryshimePerq >= 0)
                e.Result = String.Format("{0:#,#.00}", ndryshimePerq);
            else
                e.Result = "(" + String.Format("{0:#,#.00}", -ndryshimePerq) + ")";
            e.Handled = true;
        }

        private void xrTableCell108_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double ndryshimePerq = gjenVitPara10 == 0 ? 0 : (ndrAbs10 / gjenVitPara10) * 100;
            if (ndryshimePerq >= 0)
                e.Result = String.Format("{0:#,#.00}", ndryshimePerq);
            else
                e.Result = "(" + String.Format("{0:#,#.00}", -ndryshimePerq) + ")";
            e.Handled = true;
        }

        private void xrTableCell110_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double ndryshimePerq = gjenVitPara11 == 0 ? 0 : (ndrAbs11 / gjenVitPara11) * 100;
            if (ndryshimePerq >= 0)
                e.Result = String.Format("{0:#,#.00}", ndryshimePerq);
            else
                e.Result = "(" + String.Format("{0:#,#.00}", -ndryshimePerq) + ")";
            e.Handled = true;
        }

        private void xrTableCell112_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double ndryshimePerq = gjenVitPara12 == 0 ? 0 : (ndrAbs12 / gjenVitPara12) * 100;
            if (ndryshimePerq >= 0)
                e.Result = String.Format("{0:#,#.00}", ndryshimePerq);
            else
                e.Result = "(" + String.Format("{0:#,#.00}", -ndryshimePerq) + ")";
            e.Handled = true;
        }

        private void xrTableCell113_SummaryReset(object sender, EventArgs e)
        {
            ndrAbs3 = 0;
            gjenVitPara3 = 0;
        }

        private void xrTableCell96_SummaryReset(object sender, EventArgs e)
        {
            ndrAbs4= 0;
            gjenVitPara4 = 0;
        }

        private void xrTableCell98_SummaryReset(object sender, EventArgs e)
        {
            ndrAbs5= 0;
            gjenVitPara5 = 0;
        }

        private void xrTableCell100_SummaryReset(object sender, EventArgs e)
        {
            ndrAbs6 = 0;
            gjenVitPara6 = 0;
        }

        private void xrTableCell102_SummaryReset(object sender, EventArgs e)
        {
            ndrAbs7 = 0;
            gjenVitPara7 = 0;
        }

        private void xrTableCell104_SummaryReset(object sender, EventArgs e)
        {
            ndrAbs8 = 0;
            gjenVitPara8 = 0;
        }

        private void xrTableCell106_SummaryReset(object sender, EventArgs e)
        {
            ndrAbs9 = 0;
            gjenVitPara9 = 0;
        }

        private void xrTableCell108_SummaryReset(object sender, EventArgs e)
        {
            ndrAbs10 = 0;
            gjenVitPara10 = 0;
        }

        private void xrTableCell110_SummaryReset(object sender, EventArgs e)
        {
            ndrAbs11 = 0;
            gjenVitPara11 = 0;
        }

        private void xrTableCell112_SummaryReset(object sender, EventArgs e)
        {
            ndrAbs12 = 0;
            gjenVitPara12 = 0;
        }
    }
}
