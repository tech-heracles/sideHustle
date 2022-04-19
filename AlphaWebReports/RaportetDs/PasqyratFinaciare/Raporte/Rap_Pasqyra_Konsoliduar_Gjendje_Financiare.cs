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
    public partial class Rap_Pasqyra_Konsoliduar_Gjendje_Financiare : DevExpress.XtraReports.UI.XtraReport, IUpdateDetailKPF
    {
		public Rap_Pasqyra_Konsoliduar_Gjendje_Financiare(){InitializeComponent();} 
        bool hapurgjitha = false;
        CultureInfo ci;
        int niv = 0;
        //int niv2 = 0;
        //int niv3 = 0;
        int nivTemp = 0;
        string vlera;
        string vlera1;
        public Rap_Pasqyra_Konsoliduar_Gjendje_Financiare(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, report)
        {

        }
        public Rap_Pasqyra_Konsoliduar_Gjendje_Financiare(CultureInfo ci, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            this.ci = ci;
            MonNder.Value = raport.Parameters[4].Value;
            xrLabel52.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel55.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            azhornimLabel.Text = raport.Parameters[3].Description;
            Azhornim.Value = raport.Parameters[3].Value;
            xrLabel96.Text = raport.Parameters[5].Description;
            parameter3.Value = raport.Parameters[5].Value;
            hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);
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
            string catid = GetCurrentColumnValue("kodikpf") == null ? "" : GetCurrentColumnValue("kodikpf").ToString();
            string catid2 = GetCurrentColumnValue("PERSHKRIMIZERIT") == null ? "" : GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();

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
            if (GetCurrentColumnValue("kodikpf") != null) catid = GetCurrentColumnValue("kodikpf").ToString();


            if (GetCurrentColumnValue("NRLLOGARI") == null || GetCurrentColumnValue("NRLLOGARI").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJLLOG") != null && GetCurrentColumnValue("SHFAQBIJLLOG").ToString() == "False"))
            {
                label.Text = "";
            }

            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";Detail;pasqyreKonsoliduarGjendjeFinanciare;buxhetor')";
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

        private void GroupHeader7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string catid = "";
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null) catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();

            if (SkippedDetailKPF.Contains(catid))
                e.Cancel = Convert.ToBoolean(SkippedDetailKPF[catid]);
            else
            {
                e.Cancel = !hapurgjitha; SkippedDetailKPF.Add(catid, !hapurgjitha);
            }
        }

        private void lblprindi5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            string catid = "";
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null) catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji") == null || GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ5") != null && GetCurrentColumnValue("SHFAQBIJ5").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;pasqyreKonsoliduarGjendjeFinanciare;buxhetor')";

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

        private void lblPrindi4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            string catid = "";
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null) catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji") == null || GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ4") != null && GetCurrentColumnValue("SHFAQBIJ4").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;pasqyreKonsoliduarGjendjeFinanciare;buxhetor')";
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

        private void lblPrindi3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            string catid = "";
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null) catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji") == null || GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ3") != null && GetCurrentColumnValue("SHFAQBIJ3").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;pasqyreKonsoliduarGjendjeFinanciare;buxhetor')";
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

        private void lblPrindi2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            string catid = "";
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null) catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji") == null || GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ2") != null && GetCurrentColumnValue("SHFAQBIJ2").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;pasqyreKonsoliduarGjendjeFinanciare;buxhetor')";
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

        private void lblPrindi1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;pasqyreKonsoliduarGjendjeFinanciare;buxhetor')";
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

        private void xrLabel57_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //string niv = "";
            //if (niveli1 > 0)
            //    niv = romake[niveli1 - 1];
            //else niv = "I";
            XRLabel label = sender as XRLabel;
            label.Text = xrLabel32.Text + "." + xrLabel23.Text;
        }

        private void xrLabel13_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrLabel14_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrLabel16_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrLabel15_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrLabel18_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrLabel50_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //niv = 0;
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

        private void xrLabel67_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {

            if (diferenca >= 0)
                e.Result = String.Format("{0:#,#.00}", diferenca);
            else e.Result = "(" + String.Format("{0:#,#.00}", -diferenca) + ")";
            e.Handled = true;

        }

        private void xrLabel67_SummaryCalculated(object sender, TextFormatEventArgs e)
        {


        }

        double diferenca = 0;
        double diferenca1 = 0;
        private void xrLabel67_SummaryRowChanged(object sender, EventArgs e)
        { String zeri="";
            if (GetCurrentColumnValue("shenja") != null )
            { if (GetCurrentColumnValue("prindi1")!=null)
                 zeri = GetCurrentColumnValue("prindi1").ToString();
          if (!zeri.Equals("Nga kjo ; rezult. e mbartura e te ushtrimit (+ -)"))
          {
                if (GetCurrentColumnValue("shenja").ToString() == "Pozitive")
                    diferenca += double.Parse(GetCurrentColumnValue("gjend").ToString());
                else diferenca -= double.Parse(GetCurrentColumnValue("gjend").ToString());
          } 
            
           }
        }

        private void xrLabel67_SummaryReset(object sender, EventArgs e)
        {
            diferenca = 0;
            diferenca1 = 0;
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

        private void xrLabel83_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrLabel89_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrLabel94_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportPasqyreKonsolidGjendjeFinancTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel19.Text = rm.GetString("labelRaportiEmertimi", ci);
            xrLabel85.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel20.Text = rm.GetString("labelRaportiShenime", ci);
            xrLabel21.Text = rm.GetString("labelRaportiUshtrimiMbyllur", ci);
            xrLabel22.Text = rm.GetString("labelRaportiUshtrimiParaardhes", ci);
            xrLabel56.Text = rm.GetString("labelRaportiShuma", ci);
            xrLabel58.Text = rm.GetString("labelRaportiTotali", ci);
            //xrLabel63.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel74.Text = rm.GetString("lblRaportAktivetNeto", ci);
            xrLabel70.Text = rm.GetString("filterMonedha", ci);
            xrLabel14.Text = rm.GetString("labelRaportiNrLlog", ci);
            xrLabel20.Text = rm.GetString("labelRaportiNrLower", ci);
            xrLabel30.Text = rm.GetString("lblRaportRezMbartura", ci);
            xrLabel68.Text = rm.GetString("lblRaportPerfaqesuarFondiKonsoliduar", ci);
          
        }

        private void xrLabel2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv = 0;
            //ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            //if (xrLabel2.Text == "Aktivet")
            //    xrLabel2.Text = rm.GetString("labelRaportiAktive", ci);
            //else xrLabel2.Text = rm.GetString("labelRaportiKapitali", ci);
        }

        private void xrLabel64_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (xrLabel64.Text == "Aktivet")
                xrLabel64.Text = "Totali " + rm.GetString("labelRaportiAktive", ci);
            else xrLabel64.Text = "Totali " + rm.GetString("labelRaportiPasivetTotal", ci);
        }

        private void xrLabel59_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                          System.Reflection.Assembly.Load("App_GlobalResources"));
            if (xrLabel59.Text == "Aktive te pacaktuara")
                xrLabel59.Text = rm.GetString("labelRaportiAktiveTePacaktuara", ci);
            else if (xrLabel59.Text == "Pasive te pacaktuara") xrLabel59.Text = rm.GetString("labelRaportiKapitaliTePacaktuara", ci);

        }

        private void xrLabel6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            if (xrLabel6.Text == "Aktive te pacaktuara")
                xrLabel6.Text = rm.GetString("labelRaportiAktiveTePacaktuara", ci);
            else if (xrLabel6.Text == "Pasive te pacaktuara") xrLabel6.Text = rm.GetString("labelRaportiKapitaliTePacaktuara", ci);
            //if (xrLabel6.Text == "Nga kjo ; Rezult. e mbartura e te ushtrimi")
            //{
            //    xrLabel16.Text = "";
            //    xrLabel69.Text = "";
            //    xrLabel37.Text = "";
             
            //}
        }

        private void xrLabel44_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
        }

        private void xrLabel44_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel45_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
        }

        private void xrLabel45_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel47_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
        }

        private void xrLabel47_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel48_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
        }

        private void xrLabel48_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel49_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
        }

        private void xrLabel49_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel51_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrLabel51_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel79_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrLabel79_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel86_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrLabel86_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel91_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrLabel91_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        //private void xrLabel66_SummaryReset(object sender, EventArgs e)
        //{
        //    //niv = 0;
        //    niv2 = 0;
        //    niv3 = 0;
        //}

        //private void xrLabel63_SummaryReset(object sender, EventArgs e)
        //{
        //    //niv = 0;
        //}

        private void xrLabel49_SummaryReset(object sender, EventArgs e)
        {

        }

        //private void xrLabel63_AfterPrint(object sender, EventArgs e)
        //{
        //    //niv = 0;
        //}

        //private void xrLabel33_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{

        //}

        private void xrLabel33_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = nivTemp + 1;
            e.Handled = true;
        }

        private void xrLabel33_SummaryReset(object sender, EventArgs e)
        {
            //niv = 0;
        }

        private void xrLabel33_AfterPrint(object sender, EventArgs e)
        {
            // niv++;
        }

        private void xrLabel33_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // niv++;
            if (niv > 0)
                nivTemp = niv;
        }

        private void xrLabel63_AfterPrint_1(object sender, EventArgs e)
        {
            niv = 0;
        }

        private void xrPageBreak1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrLabel46_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
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

        private void xrLabel34_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           // xrLabel34.Text = vlera;
        }

        private void xrLabel69_AfterPrint(object sender, EventArgs e)
        {
          
        }

        private void xrLabel69_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }

        private void xrLabel89_AfterPrint(object sender, EventArgs e)
        {
            //if (xrLabel6.Text == "Nga kjo ; Rezult. e mbartura e te ushtrimi")
            //{
            //    vlera = xrLabel69.Text;
            //}
        }

        private void xrLabel89_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (xrLabel6.Text == "Nga kjo ; Rezult. e mbartura e te ushtrimi")
            //{
            //    vlera = xrLabel69.Text;
            //}

        }

        private void xrLabel67_AfterPrint(object sender, EventArgs e)
        {
         
        }

        private void xrLabel34_AfterPrint(object sender, EventArgs e)
        {
            //vlera1 = xrLabel34.Text;
            //float diferenca=Co
            //xrLabel72.Text = String.Format("{0:#,#.00}", shuma);
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrLabel72_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {

            if (diferenca1 >= 0)
                e.Result = String.Format("{0:#,#.00}", diferenca1);
            else e.Result = "(" + String.Format("{0:#,#.00}", -diferenca1) + ")";
            e.Handled = true;

        }

        private void xrLabel72_SummaryRowChanged(object sender, EventArgs e)
        {
           
            if (GetCurrentColumnValue("shenja") != null)
            {               
                    if (GetCurrentColumnValue("shenja").ToString() == "Pozitive")
                        diferenca1 += double.Parse(GetCurrentColumnValue("gjend").ToString());
                    else diferenca1 -= double.Parse(GetCurrentColumnValue("gjend").ToString());                

            }
        }

        private void ReportFooter_AfterPrint(object sender, EventArgs e)
        {
            niv = 0;
        }

        private void xrLabel76_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
            xrLabel76.Text = niv.ToString();
        }

        private void xrLabel35_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
            xrLabel35.Text = niv.ToString();
        }

        private void xrLabel66_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
            xrLabel66.Text = niv.ToString();

        }

        private void xrLabel76_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel35_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel66_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }
    }
}
