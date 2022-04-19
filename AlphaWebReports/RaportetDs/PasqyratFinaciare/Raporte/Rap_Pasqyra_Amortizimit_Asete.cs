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
    public partial class Rap_Pasqyra_Amortizimit_Asete : DevExpress.XtraReports.UI.XtraReport, IUpdateDetailKPF
    {
		public Rap_Pasqyra_Amortizimit_Asete(){InitializeComponent();} 
        bool hapurgjitha = false;
        public Rap_Pasqyra_Amortizimit_Asete(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_Pasqyra_Amortizimit_Asete(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            xrLabel52.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel55.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            azhornimLabel.Text = raport.Parameters[3].Description;
            Azhornim.Value = raport.Parameters[3].Value;
            xrLabel105.Text = raport.Parameters[5].Description;
            parameter3.Value = raport.Parameters[5].Value;
            hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);
            EmrateLabelave(ci);
        }

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

       
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));


            xrLabel12.Text = rm.GetString("RaportiPasqyraEAmortizimitTeAseteve", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell146.Text = rm.GetString("lblRaportiMarredhenieBrendshmeDEK", ci);
            xrTableCell239.Text = rm.GetString("lblRaportiGjendjaRealeCeljesVititUshtrimor", ci);
            xrTableCell240.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell241.Text = rm.GetString("labelRaportiNrLlog", ci);
            xrTableCell242.Text = rm.GetString("labelRaportiEmertimi", ci);
            xrTableCell243.Text = rm.GetString("lblRaportGjendjaCeljeVitUshtrimor", ci);
            xrTableCell248.Text = rm.GetString("labelRaportiShtesagjateushtrimit", ci);
            xrTableCell249.Text = xrTableCell256.Text = rm.GetString("labelGjithsej", ci);
            xrTableCell255.Text = rm.GetString("labelRaportiPakesimeGjateUshtrimit", ci);
            xrTableCell257.Text = rm.GetString("lblRaportiGjendjaMbylljeVititUshtrimor", ci);
            xrTableCell259.Text = rm.GetString("lblRaportiSistemimeBeraFillimViti", ci);
            xrTableCell290.Text = rm.GetString("labelRaportiShtesaNgaKapitali", ci);
            xrTableCell291.Text = rm.GetString("lblRaportiAmortizimiVitit", ci);
            xrTableCell292.Text = rm.GetString("lblRaportiShtesaMarrBrendshmeNjesiTjerallog181", ci);
            xrTableCell294.Text = rm.GetString("lblRaportiShtesaNgaMagazina", ci);
            xrTableCell296.Text = rm.GetString("labelShitje", ci);
            xrTableCell297.Text = rm.GetString("labelRaportiJashtePerdorimi", ci);
            xrTableCell298.Text = rm.GetString("labelRaportiKthimeNeMagazine", ci);
            xrTableCell299.Text = rm.GetString("lblRaportiMarredhenieBrendshmeNjesiTjera", ci);
            xrTableCell300.Text = rm.GetString("labelRaportiPakesimeNgaKapitali", ci);
            xrTableCell301.Text = rm.GetString("lblRaportiUrdherCdemtimi", ci);
            xrTableCell306.Text = rm.GetString("lblRaportiTeKrijuara201X", ci);
            xrTableCell320.Text = rm.GetString("lblRaportiShtesaMarrBrendshmeDEKllog18", ci);
            xrLabel7.Text = rm.GetString("labelLogoIMB", ci);
        }

        private void xrTableCell6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            string catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;pasqyraAmortizimitAsete')";
                if (SkippedDetailKPF.Contains(catid) && (bool)SkippedDetailKPF[catid] == false)
                    label.Text = "-";
                else
                    label.Text = "+";
            }
        }

        private void xrTableCell75_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
        }

        private void xrTableCell75_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrTableCell9_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell10_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        

        private void xrTableCell13_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell11_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell5_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell4_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell3_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell29_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell28_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell27_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell26_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell24_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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



        private void xrTableCell20_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell19_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell16_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrTableCell16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
            //if (niv != 12)
            //    niv++;
            //else niv += 3;
        }

        private void xrTableCell15_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            string catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;pasqyraAmortizimitAsete')";
                if (SkippedDetailKPF.Contains(catid) && (bool)SkippedDetailKPF[catid] == false)
                    label.Text = "-";
                else
                    label.Text = "+";
            }
        }

        private void xrTableCell30_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            string catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            //string catid = GetCurrentColumnValue("KodZeri").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;pasqyraAmortizimitAsete')";
                if (!SkippedDetailKPF.Contains(catid))
                    label.Text = (hapurgjitha) ? "-" : "+";
                else label.Text = (!(bool)SkippedDetailKPF[catid]) ? "-" : "+";
            }
        }

        private void xrTableCell31_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue("KodZeri").ToString() != "230")                
            e.Result = niv;
            else e.Result = "4/1";
            e.Handled = true;
        }

        private void xrTableCell31_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        { if (GetCurrentColumnValue("KodZeri").ToString() != "230")
            niv++;
        }

        private void xrTableCell34_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell35_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell38_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell39_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell41_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell42_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell43_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell44_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell45_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            string catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;pasqyraAmortizimitAsete')";
                if (!SkippedDetailKPF.Contains(catid))
                    label.Text = (hapurgjitha) ? "-" : "+";
                else label.Text = (!(bool)SkippedDetailKPF[catid]) ? "-" : "+";
            }
        }

        private void xrTableCell46_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrTableCell46_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
        }

        private void xrTableCell49_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell50_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell53_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell54_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell56_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell57_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell58_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell59_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell60_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            string catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;pasqyraAmortizimitAsete')";
                if (!SkippedDetailKPF.Contains(catid))
                    label.Text = (hapurgjitha) ? "-" : "+";
                else label.Text = (!(bool)SkippedDetailKPF[catid]) ? "-" : "+";
            }
        }

        private void xrTableCell61_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrTableCell61_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
        }

        private void xrTableCell64_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell65_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell68_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell69_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell71_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell72_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell73_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell74_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
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

        private void xrTableCell76_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            string catid = GetCurrentColumnValue("kodikpf").ToString();
            if (GetCurrentColumnValue("NRLLOGARI").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJLLOG") != null && GetCurrentColumnValue("SHFAQBIJLLOG").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";Detail;pasqyraAmortizimitAsete')";
                if (!SkippedDetailBands.Contains(catid))
                    label.Text = (hapurgjitha) ? "-" : "+";
                else label.Text = (!(bool)SkippedDetailBands[catid]) ? "-" : "+";
            }
        }

        private void xrTableCell103_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

        private void xrTableCell102_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

        private void xrTableCell101_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

        private void xrTableCell100_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

        private void xrTableCell98_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

        private void xrTableCell97_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

        private void xrTableCell94_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

        private void xrTableCell93_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

        private void xrTableCell104_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrTableCell104_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
        }

        private void xrTableCell107_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell108_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell111_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell112_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell114_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell115_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell116_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell117_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell118_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrTableCell118_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
        }

        private void xrTableCell121_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell122_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell125_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell126_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell128_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell129_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell130_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell131_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell132_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
        }

        private void xrTableCell132_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrTableCell135_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell136_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell139_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell140_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell142_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell143_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell144_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell145_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell147_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrTableCell147_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
            //if (niv != 12)
            //    niv++;
            //else niv += 3;
        }

        private void xrTableCell150_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell151_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell154_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell155_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell157_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell158_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell159_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell160_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell162_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
            xrTableCell162.Text = niv.ToString();
        }

        private void xrTableCell162_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //e.Result = niv;
            //e.Handled = true;
        }

        private void xrTableCell165_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell166_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell169_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell170_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell172_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell173_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell174_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell175_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell162_AfterPrint(object sender, EventArgs e)
        {
            niv = 0;
        }

        private void xrTableCell14_SummaryGetResult_1(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell21_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell22_SummaryGetResult_1(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell36_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell37_SummaryGetResult_1(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell52_SummaryGetResult_1(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell66_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell67_SummaryGetResult_1(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell82_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell83_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell51_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

        private void xrTableCell189_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell305_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell1_SummaryGetResult_1(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell21_SummaryGetResult_1(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell2_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrTableCell23_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void Rap_Pasqyra_Amortizimit_Asete_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("MonedheNdermarrjes") != null)
                xrLabel71.Text = GetCurrentColumnValue("MonedheNdermarrjes").ToString();
            else xrLabel71.Text = "";
        }
    }
}
