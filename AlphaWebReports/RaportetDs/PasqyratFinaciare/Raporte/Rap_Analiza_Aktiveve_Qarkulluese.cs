using System;
using System.Collections;
using System.Drawing.Printing;
using System.Globalization;
using System.Reflection;
using System.Resources;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs.PasqyratFinaciare.Raporte
{
    public partial class Rap_Analiza_Aktiveve_Qarkulluese : XtraReport
    {
		public Rap_Analiza_Aktiveve_Qarkulluese(){InitializeComponent();} 
        private readonly CultureInfo ci;

        private double diferenca;
        private double diferenca1;
        private readonly bool hapurgjitha;
        private double ndrAbs1;
        private double gjenVitPara1;
        private double ndrAbs2;
        private double gjenVitPara2;
        private double ndrAbs3;
        private double gjenVitPara3;
        private double ndrAbs4;
        private double gjenVitPara4;
        private double ndrAbs5;
        private double gjenVitPara5;
        private double ndrAbs6;
        private double gjenVitPara6;
        private double ndrAbs7;
        private double gjenVitPara7;
        private double ndrAbs8;
        private double gjenVitPara8;
        private double ndrAbs9;
        private double gjenVitPara9;
        private double ndrAbs10;
        private double gjenVitPara10;
        private double ndrAbs11;
        private double gjenVitPara11;
        private double ndrAbs12;
        private double gjenVitPara12;
        private int niv;
        //int niv2 = 0;
        //int niv3 = 0;
        private int nivTemp;
        private double sh1;
        private double sh2;
        private double sh3;
        private double sh4;
        private double sh5;
        private double sh6;
        private double sh7;
        private double sh8;
        private double sh9;
        private double sh10;
        private double total1;
        private double total2;
        private double total;
        private double shuma;
        private double shuma1;
        private double shuma2;

        private Hashtable skippedDetailBands;

        private Hashtable skippedDetailKPF;
        private string vlera;
        private string vlera1;

        public Rap_Analiza_Aktiveve_Qarkulluese(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, report)
        {

        }
        public Rap_Analiza_Aktiveve_Qarkulluese(CultureInfo ci, XtraReport raport)
        {
            InitializeComponent();
            //  EmrateLabelave(ci);
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
            hapurgjitha = false;
        }

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

            CreateDocument();
        }

        public void UpdateDetail(string detailID)
        {
            if (SkippedDetailBands.Contains(detailID))
                SkippedDetailBands[detailID] = !Convert.ToBoolean(SkippedDetailBands[detailID]);
            else
                SkippedDetailBands.Add(detailID, false);

            CreateDocument();
        }

        private void Detail_BeforePrint(object sender, PrintEventArgs e)
        {
            var catid = GetCurrentColumnValue("kodikpf") == null ? "" : GetCurrentColumnValue("kodikpf").ToString();
            var catid2 = GetCurrentColumnValue("PERSHKRIMIZERIT") == null
                ? ""
                : GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();

            if (SkippedDetailKPF.Contains(catid2))
            {
                if (Convert.ToBoolean(SkippedDetailKPF[catid2]))
                {
                    e.Cancel = true;
                    return;
                }
            }

            if (SkippedDetailBands.Contains(catid))
                e.Cancel = Convert.ToBoolean(SkippedDetailBands[catid]);
            else
            {
                e.Cancel = !hapurgjitha;
                SkippedDetailBands.Add(catid, !hapurgjitha);
            }
            e.Cancel = false;
            SkippedDetailBands.Add(catid, !hapurgjitha);
        }

        private void lblKPF_BeforePrint(object sender, PrintEventArgs e)
        {
            //XRLabel label = sender as XRLabel;
            //string catid = "";
            //if (GetCurrentColumnValue("kodikpf") != null) catid = GetCurrentColumnValue("kodikpf").ToString();


            //if (GetCurrentColumnValue("NRLLOGARI") == null || GetCurrentColumnValue("NRLLOGARI").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJLLOG") != null && GetCurrentColumnValue("SHFAQBIJLLOG").ToString() == "False"))
            //{
            //    label.Text = "";
            //}

            //else
            //{
            //    label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";Detail;235;buxhetor')";
            //    if (!SkippedDetailBands.Contains(catid))
            //        if (hapurgjitha)
            //            label.Text = "-";
            //        else label.Text = "+";
            //    else if ((bool)SkippedDetailBands[catid] == false)
            //        label.Text = "-";
            //    else
            //        label.Text = "+";
            //}
        }

        private void GroupHeader7_BeforePrint(object sender, PrintEventArgs e)
        {
            var catid = "";
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null)
                catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();

            if (SkippedDetailKPF.Contains(catid))
                e.Cancel = Convert.ToBoolean(SkippedDetailKPF[catid]);
            else
            {
                e.Cancel = !hapurgjitha;
                SkippedDetailKPF.Add(catid, !hapurgjitha);
            }
        }

        private void lblprindi5_BeforePrint(object sender, PrintEventArgs e)
        {
            //XRLabel label = sender as XRLabel;
            //string catid = "";
            //if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null) catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            //label.NavigateUrl = "";
            //if (GetCurrentColumnValue("lloji") == null || GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ5") != null && GetCurrentColumnValue("SHFAQBIJ5").ToString() == "False"))
            //    label.Text = "";
            //else
            //{
            //    label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;235;buxhetor')";

            //    if (!SkippedDetailKPF.Contains(catid))
            //        if (hapurgjitha)
            //            label.Text = "-";
            //        else label.Text = "+";
            //    else if ((bool)SkippedDetailKPF[catid] == false)
            //        label.Text = "-";
            //    else
            //        label.Text = "+";
            //}
        }

        private void lblPrindi4_BeforePrint(object sender, PrintEventArgs e)
        {
            //XRLabel label = sender as XRLabel;
            //string catid = "";
            //if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null) catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            //label.NavigateUrl = "";
            //if (GetCurrentColumnValue("lloji") == null || GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ4") != null && GetCurrentColumnValue("SHFAQBIJ4").ToString() == "False"))
            //    label.Text = "";
            //else
            //{
            //    label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;235;buxhetor')";
            //    if (!SkippedDetailKPF.Contains(catid))
            //        if (hapurgjitha)
            //            label.Text = "-";
            //        else label.Text = "+";
            //    else if ((bool)SkippedDetailKPF[catid] == false)
            //        label.Text = "-";
            //    else
            //        label.Text = "+";
            //}
        }

        private void lblPrindi3_BeforePrint(object sender, PrintEventArgs e)
        {
            //XRLabel label = sender as XRLabel;
            //string catid = "";
            //if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null) catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            //label.NavigateUrl = "";
            //if (GetCurrentColumnValue("lloji") == null || GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ3") != null && GetCurrentColumnValue("SHFAQBIJ3").ToString() == "False"))
            //    label.Text = "";
            //else
            //{
            //    label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;235;buxhetor')";
            //    if (!SkippedDetailKPF.Contains(catid))
            //        if (hapurgjitha)
            //            label.Text = "-";
            //        else label.Text = "+";
            //    else if ((bool)SkippedDetailKPF[catid] == false)
            //        label.Text = "-";
            //    else
            //        label.Text = "+";
            //}
        }

        private void lblPrindi2_BeforePrint(object sender, PrintEventArgs e)
        {
            //XRLabel label = sender as XRLabel;
            //string catid = "";
            //if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null) catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            //label.NavigateUrl = "";
            //if (GetCurrentColumnValue("lloji") == null || GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ2") != null && GetCurrentColumnValue("SHFAQBIJ2").ToString() == "False"))
            //    label.Text = "";
            //else
            //{
            //    label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;235;buxhetor')";
            //    if (!SkippedDetailKPF.Contains(catid))
            //        if (hapurgjitha)
            //            label.Text = "-";
            //        else label.Text = "+";
            //    else if ((bool)SkippedDetailKPF[catid] == false)
            //        label.Text = "-";
            //    else
            //        label.Text = "+";
            //}
        }

        private void lblPrindi1_BeforePrint(object sender, PrintEventArgs e)
        {
            //XRLabel label = sender as XRLabel;
            //string catid = "";
            //if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null)
            //    catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            //label.NavigateUrl = "";
            //if (GetCurrentColumnValue("lloji") == null || GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
            //    label.Text = "";
            //else
            //{
            //    label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;235;buxhetor')";
            //    if (!SkippedDetailKPF.Contains(catid))
            //        if (hapurgjitha)
            //            label.Text = "-";
            //        else label.Text = "+";
            //    else if ((bool)SkippedDetailKPF[catid] == false)
            //        label.Text = "-";
            //    else
            //        label.Text = "+";
            //}
        }

        private void xrLabel57_BeforePrint(object sender, PrintEventArgs e)
        {
            //string niv = "";
            //if (niveli1 > 0)
            //    niv = romake[niveli1 - 1];
            //else niv = "I";
            //XRLabel label = sender as XRLabel;
            //label.Text = xrLabel32.Text + "." + xrLabel23.Text;
        }

        private void xrLabel13_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (var i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = string.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + string.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel14_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (var i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = string.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + string.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel17_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (var i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = string.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + string.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel16_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (var i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = string.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + string.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel15_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (var i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = string.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + string.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel18_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (var i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = string.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + string.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel50_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //niv = 0;
            double shuma = 0;
            for (var i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = string.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + string.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel67_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (diferenca >= 0)
                e.Result = string.Format("{0:#,#.00}", diferenca);
            else e.Result = "(" + string.Format("{0:#,#.00}", -diferenca) + ")";
            e.Handled = true;
        }

        private void xrLabel67_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
        }

        private void xrLabel67_SummaryRowChanged(object sender, EventArgs e)
        {
            var zeri = "";
            if (GetCurrentColumnValue("shenja") != null)
            {
                if (GetCurrentColumnValue("prindi1") != null)
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

        private void xrLabel11_BeforePrint(object sender, PrintEventArgs e)
        {
            double shuma = 0;
            var label = sender as XRLabel;
            if (label.Text.Contains("("))
                shuma = -double.Parse(label.Text.Replace('(', ' ').Replace(')', ' '));
            else shuma = double.Parse(label.Text);
            if (shuma >= 0)
                label.Text = string.Format("{0:#,#.00}", shuma);
            else label.Text = "(" + string.Format("{0:#,#.00}", -shuma) + ")";
        }

        private void xrLabel83_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (var i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = string.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + string.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel89_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (var i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = string.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + string.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel94_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (var i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = string.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + string.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        /// <summary>
        ///     Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            var rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportPasqyreKonsolidGjendjeFinancTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel19.Text = rm.GetString("labelRaportiEmertimi", ci);
            xrLabel85.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel20.Text = rm.GetString("labelRaportiShenime", ci);
            xrLabel21.Text = rm.GetString("labelRaportiUshtrimiMbyllur", ci);
            xrLabel22.Text = rm.GetString("labelRaportiUshtrimiParaardhes", ci);
            // xrLabel56.Text = rm.GetString("labelRaportiShuma", ci);
            //  xrLabel58.Text = rm.GetString("labelRaportiTotali", ci);
            //xrLabel63.Text = rm.GetString("labelRaportiTotali", ci);
            //  xrLabel74.Text = rm.GetString("lblRaportAktivetNeto", ci);
            xrLabel70.Text = rm.GetString("filterMonedha", ci);
            xrLabel14.Text = rm.GetString("labelRaportiNrLlog", ci);
            xrLabel20.Text = rm.GetString("labelRaportiNrLower", ci);
            //  xrLabel30.Text = rm.GetString("lblRaportRezMbartura", ci);
            // xrLabel68.Text = rm.GetString("lblRaportPerfaqesuarFondiKonsoliduar", ci);
        }

        private void xrLabel2_BeforePrint(object sender, PrintEventArgs e)
        {
            niv = 0;
            //ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            //if (xrLabel2.Text == "Aktivet")
            //    xrLabel2.Text = rm.GetString("labelRaportiAktive", ci);
            //else xrLabel2.Text = rm.GetString("labelRaportiKapitali", ci);
        }

        private void xrLabel64_BeforePrint(object sender, PrintEventArgs e)
        {
            //ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            //if (xrLabel64.Text == "Aktivet")
            //    xrLabel64.Text = "Totali " + rm.GetString("labelRaportiAktive", ci);
            //else xrLabel64.Text = "Totali " + rm.GetString("labelRaportiPasivetTotal", ci);
        }

        private void xrLabel59_BeforePrint(object sender, PrintEventArgs e)
        {
            //ResourceManager rm = new ResourceManager("Resources.Strings",
            //              System.Reflection.Assembly.Load("App_GlobalResources"));
            //if (xrLabel59.Text == "Aktive te pacaktuara")
            //    xrLabel59.Text = rm.GetString("labelRaportiAktiveTePacaktuara", ci);
            //else if (xrLabel59.Text == "Pasive te pacaktuara") xrLabel59.Text = rm.GetString("labelRaportiKapitaliTePacaktuara", ci);
        }

        private void xrLabel6_BeforePrint(object sender, PrintEventArgs e)
        {
            var rm = new ResourceManager("Resources.Strings",
                Assembly.Load("App_GlobalResources"));
            if (xrLabel6.Text == "Aktive te pacaktuara")
                xrLabel6.Text = rm.GetString("labelRaportiAktiveTePacaktuara", ci);
            else if (xrLabel6.Text == "Pasive te pacaktuara")
                xrLabel6.Text = rm.GetString("labelRaportiKapitaliTePacaktuara", ci);
            //if (xrLabel6.Text == "Nga kjo ; Rezult. e mbartura e te ushtrimi")
            //{
            //    xrLabel16.Text = "";
            //    xrLabel69.Text = "";
            //    xrLabel37.Text = "";

            //}
        }

        private void xrLabel44_BeforePrint(object sender, PrintEventArgs e)
        {
            niv++;
        }

        private void xrLabel44_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel45_BeforePrint(object sender, PrintEventArgs e)
        {
            niv++;
        }

        private void xrLabel45_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel47_BeforePrint(object sender, PrintEventArgs e)
        {
            niv++;
        }

        private void xrLabel47_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel48_BeforePrint(object sender, PrintEventArgs e)
        {
            niv++;
        }

        private void xrLabel48_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel49_BeforePrint(object sender, PrintEventArgs e)
        {
            niv++;
        }

        private void xrLabel49_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel51_BeforePrint(object sender, PrintEventArgs e)
        {
        }

        private void xrLabel51_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel79_BeforePrint(object sender, PrintEventArgs e)
        {
        }

        private void xrLabel79_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel86_BeforePrint(object sender, PrintEventArgs e)
        {
        }

        private void xrLabel86_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel91_BeforePrint(object sender, PrintEventArgs e)
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

        private void xrLabel33_BeforePrint_1(object sender, PrintEventArgs e)
        {
            // niv++;
            if (niv > 0)
                nivTemp = niv;
        }

        private void xrLabel63_AfterPrint_1(object sender, EventArgs e)
        {
            niv = 0;
        }

        private void xrPageBreak1_BeforePrint(object sender, PrintEventArgs e)
        {
        }

        private void xrLabel46_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (var i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = string.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + string.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel69_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (var i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = string.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + string.Format("{0:#,#.00}", -shuma) + ")";

            e.Handled = true;
        }

        private void xrLabel34_BeforePrint(object sender, PrintEventArgs e)
        {
            // xrLabel34.Text = vlera;
        }

        private void xrLabel69_AfterPrint(object sender, EventArgs e)
        {
            // double vlera= Convert.ToDouble(xrLabel69.Summary.GetResult());
            //  double vlera = shuma1 - shuma2;
            // xrLabel9.Text = String.Format("{0:#,#.00}", vlera);
        }

        private void xrLabel69_BeforePrint(object sender, PrintEventArgs e)
        {
        }

        private void xrLabel89_AfterPrint(object sender, EventArgs e)
        {
            //if (xrLabel6.Text == "Nga kjo ; Rezult. e mbartura e te ushtrimi")
            //{
            //    vlera = xrLabel69.Text;
            //}
        }

        private void xrLabel89_BeforePrint(object sender, PrintEventArgs e)
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

        private void ReportFooter_BeforePrint(object sender, PrintEventArgs e)
        {
        }

        private void xrLabel72_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (diferenca1 >= 0)
                e.Result = string.Format("{0:#,#.00}", diferenca1);
            else e.Result = "(" + string.Format("{0:#,#.00}", -diferenca1) + ")";
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

        private void xrLabel76_BeforePrint(object sender, PrintEventArgs e)
        {
            //niv++;
            //xrLabel76.Text = niv.ToString();
        }


        private void xrLabel66_BeforePrint(object sender, PrintEventArgs e)
        {
            //niv++;
            //xrLabel66.Text = niv.ToString();
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

        private void xrLabel37_BeforePrint(object sender, PrintEventArgs e)
        {
        }

        private void xrLabel66_SummaryReset(object sender, EventArgs e)
        {
        }

        private void GroupHeader6_BeforePrint(object sender, PrintEventArgs e)
        {
            //shuma2 = 0; shuma1 = 0;
        }

        private void xrLabel66_SummaryGetResult_1(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (var i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = string.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + string.Format("{0:#,#.00}", -shuma) + ")";

            e.Handled = true;
        }

        private void xrLabel9_BeforePrint(object sender, PrintEventArgs e)
        {
        }

        private void xrLabel9_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
        }

        private void xrLabel69_SummaryReset(object sender, EventArgs e)
        {
        }

        private void xrLabel69_SummaryRowChanged(object sender, EventArgs e)
        {
            double shuma = 0;
            if (xrLabel69.Text != "")
                shuma += Convert.ToDouble(xrLabel69.Text);
        }

        private void xrLabel69_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            if (e.Value.ToString().Contains("("))
                shuma1 = -double.Parse(e.Value.ToString().Replace('(', ' ').Replace(')', ' '));
            else shuma1 = double.Parse(e.Value.ToString());
        }

        private void xrLabel66_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            if (e.Value.ToString().Contains("("))
                shuma2 = -double.Parse(e.Value.ToString().Replace('(', ' ').Replace(')', ' '));
            else shuma2 = double.Parse(e.Value.ToString());
        }

        private void xrLabel9_AfterPrint(object sender, EventArgs e)
        {
            shuma1 = 0;
            shuma2 = 0;
        }

        private void xrLabel66_AfterPrint(object sender, EventArgs e)
        {
        }

        private void GroupHeader6_AfterPrint(object sender, EventArgs e)
        {
            // double shuma = shuma1 - shuma2;
            //  xrLabel9.Text = shuma.ToString();
            shuma1 = 0;
            shuma2 = 0;
        }

        private void xrLabel100_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //double shuma = shuma1 - shuma2;
            //if (shuma >= 0)
            //{
            //    e.Result = String.Format("{0:#,#.00}", shuma);
            //    xrLabel100.Text = String.Format("{0:#,#.00}", shuma);
            //}
            //else
            //{
            //    e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            //    xrLabel100.Text = "(" + String.Format("{0:#,#.00}", shuma) + ")";
            //}

            //e.Handled = true;
            double shuma = 0;
            for (var i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = string.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + string.Format("{0:#,#.00}", -shuma) + ")";

            e.Handled = true;
        }

        private void xrLabel101_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (var i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = string.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + string.Format("{0:#,#.00}", -shuma) + ")";

            e.Handled = true;
        }

        private void xrLabel14_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            if (e.Value.ToString().Contains("("))
                sh1 = -double.Parse(e.Value.ToString().Replace('(', ' ').Replace(')', ' '));
            else sh1 = double.Parse(e.Value.ToString());

            total1 += sh1;
        }

        private void xrLabel68_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            if (e.Value.ToString().Contains("("))
                sh2 = -double.Parse(e.Value.ToString().Replace('(', ' ').Replace(')', ' '));
            else sh2 = double.Parse(e.Value.ToString());
            total2 += sh2;
        }

        private void xrLabel17_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            if (e.Value.ToString().Contains("("))
                sh3 = -double.Parse(e.Value.ToString().Replace('(', ' ').Replace(')', ' '));
            else sh3 = double.Parse(e.Value.ToString());

            total1 += sh3;
        }

        private void xrLabel72_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            if (e.Value.ToString().Contains("("))
                sh4 = -double.Parse(e.Value.ToString().Replace('(', ' ').Replace(')', ' '));
            else sh4 = double.Parse(e.Value.ToString());
            total2 += sh4;
        }

        private void xrLabel16_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            if (e.Value.ToString().Contains("("))
                sh5 = -double.Parse(e.Value.ToString().Replace('(', ' ').Replace(')', ' '));
            else sh5 = double.Parse(e.Value.ToString());

            total1 += sh5;
        }

        private void xrLabel77_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            if (e.Value.ToString().Contains("("))
                sh6 = -double.Parse(e.Value.ToString().Replace('(', ' ').Replace(')', ' '));
            else sh6 = double.Parse(e.Value.ToString());
            total2 += sh6;
        }

        private void xrLabel46_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            if (e.Value.ToString().Contains("("))
                sh7 = -double.Parse(e.Value.ToString().Replace('(', ' ').Replace(')', ' '));
            else sh7 = double.Parse(e.Value.ToString());

            total1 += sh7;
        }

        private void xrLabel79_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            if (e.Value.ToString().Contains("("))
                sh8 = -double.Parse(e.Value.ToString().Replace('(', ' ').Replace(')', ' '));
            else sh8 = double.Parse(e.Value.ToString());
            total2 += sh8;
        }

        private void xrLabel13_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            if (e.Value.ToString().Contains("("))
                sh9 = -double.Parse(e.Value.ToString().Replace('(', ' ').Replace(')', ' '));
            else sh9 = double.Parse(e.Value.ToString());

            total1 += sh9;
        }

        private void xrLabel81_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            if (e.Value.ToString().Contains("("))
                sh10 = -double.Parse(e.Value.ToString().Replace('(', ' ').Replace(')', ' '));
            else sh10 = double.Parse(e.Value.ToString());
            total2 += sh10;
        }

        private void xrLabel11_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            //if (e.Value.ToString().Contains("(")) shuma1 = -double.Parse(e.Value.ToString().Replace('(', ' ').Replace(')', ' '));
            //else shuma1 = double.Parse(e.Value.ToString());
        }

        private void xrLabel82_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            //if (e.Value.ToString().Contains("(")) shuma2 = -double.Parse(e.Value.ToString().Replace('(', ' ').Replace(')', ' '));
            //else shuma2 = double.Parse(e.Value.ToString());
        }


        private void xrLabel102_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (var i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = string.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + string.Format("{0:#,#.00}", -shuma) + ")";

            e.Handled = true;
        }

        private void xrLabel103_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (var i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = string.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + string.Format("{0:#,#.00}", -shuma) + ")";

            e.Handled = true;
        }

        private void xrLabel104_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
        }

        private void xrLabel105_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (var i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = string.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + string.Format("{0:#,#.00}", -shuma) + ")";

            e.Handled = true;
        }

        private void xrLabel106_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //double shuma = shuma1 - shuma2;
            //if (shuma >= 0)
            //{
            //    e.Result = String.Format("{0:#,#.00}", shuma);
            //    xrLabel106.Text = String.Format("{0:#,#.00}", shuma);
            //}
            //else
            //{
            //    e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            //    xrLabel106.Text = "(" + String.Format("{0:#,#.00}", shuma) + ")";
            //}
            //e.Handled = true;
        }


        private void xrLabel200_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (var i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = string.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + string.Format("{0:#,#.00}", -shuma) + ")";

            e.Handled = true;
        }


        private void xrLabel202_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            var ndryshimePerq = gjenVitPara5 == 0 ? 0 : ndrAbs5/gjenVitPara5;

            if (ndryshimePerq >= 0)
                e.Result = string.Format("{0:#,#.00}", ndryshimePerq*100) + " %";
            else
                e.Result = string.Format("{0:#,#.00}", ndryshimePerq*100) + " %";
            e.Handled = true;
        }


        private void xrLabel203_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            var ndryshimePerq = gjenVitPara2 == 0 ? 0 : ndrAbs2/gjenVitPara2;

            if (ndryshimePerq >= 0)
                e.Result = string.Format("{0:#,#.00}", ndryshimePerq*100) + " %";
            else
                e.Result = string.Format("{0:#,#.00}", ndryshimePerq*100) + " %";
            e.Handled = true;
        }

        private void xrLabel204_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
        }

        private void xrLabel205_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
        }

        private void xrLabel206_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
        }

        private void xrLabel40_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
        }

        private void xrLabel301_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
        }

        private void xrLabel99_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
        }

        private void xrLabel201_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            var ndryshimePerq = gjenVitPara4 == 0 ? 0 : ndrAbs4/gjenVitPara4;

            if (ndryshimePerq >= 0)
                e.Result = string.Format("{0:#,#.00}", ndryshimePerq*100) + " %";
            else
                e.Result = string.Format("{0:#,#.00}", ndryshimePerq*100) + " %";
            e.Handled = true;
        }

        private void xrLabel204_SummaryGetResult_1(object sender, SummaryGetResultEventArgs e)
        {
            var ndryshimePerq = gjenVitPara3 == 0 ? 0 : ndrAbs2/gjenVitPara3;

            if (ndryshimePerq >= 0)
                e.Result = string.Format("{0:#,#.00}", ndryshimePerq*100) + " %";
            else
                e.Result = string.Format("{0:#,#.00}", ndryshimePerq*100) + " %";
            e.Handled = true;
        }

        private void xrLabel205_SummaryGetResult_1(object sender, SummaryGetResultEventArgs e)
        {
            var ndryshimePerq = gjenVitPara6 == 0 ? 0 : ndrAbs6/gjenVitPara6;

            if (ndryshimePerq >= 0)
                e.Result = string.Format("{0:#,#.00}", ndryshimePerq*100) + " %";
            else
                e.Result = string.Format("{0:#,#.00}", ndryshimePerq*100) + " %";
            e.Handled = true;
        }

        private void xrLabel500_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            var ndryshimePerq = gjenVitPara1 == 0 ? 0 : ndrAbs1/gjenVitPara1;
            e.Result = string.Format("{0:#,#.00}", ndryshimePerq*100) + " %";
            e.Handled = true;
        }

        private void xrLabel79_SummaryGetResult_1(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (var i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = string.Format("{0:#,#.00}", shuma);
            else e.Result = "(" + string.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrLabel500_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("gjend0") != null && GetCurrentColumnValue("gjend1") != null)
            {
                if (GetCurrentColumnValue("shenja").ToString() == "Pozitive")
                {
                    gjenVitPara1 += Convert.ToDouble(GetCurrentColumnValue("gjend1"));
                    ndrAbs1 += Convert.ToDouble(GetCurrentColumnValue("gjend0"));
                }
                else
                {
                    gjenVitPara1 -= Convert.ToDouble(GetCurrentColumnValue("gjend1"));
                    ndrAbs1 -= Convert.ToDouble(GetCurrentColumnValue("gjend0"));
                }
            }
        }

        private void xrLabel500_SummaryReset(object sender, EventArgs e)
        {
            gjenVitPara1 = 0;
            ndrAbs1 = 0;
        }

        private void xrLabel202_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("gjend0") != null && GetCurrentColumnValue("gjend1") != null)
            {
                gjenVitPara5 += Convert.ToDouble(GetCurrentColumnValue("gjend1"));
                ndrAbs5 += Convert.ToDouble(GetCurrentColumnValue("gjend0"));
            }
        }

        private void xrLabel202_SummaryReset(object sender, EventArgs e)
        {
            ndrAbs5 = 0;
            gjenVitPara5 = 0;
        }

        private void xrLabel203_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("gjend0") != null && GetCurrentColumnValue("gjend1") != null)
            {
                gjenVitPara2 += Convert.ToDouble(GetCurrentColumnValue("gjend1"));
                ndrAbs2 += Convert.ToDouble(GetCurrentColumnValue("gjend0"));
            }
        }

        private void xrLabel203_SummaryReset(object sender, EventArgs e)
        {
            ndrAbs2 = 0;
            gjenVitPara2 = 0;
        }

        private void xrLabel204_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("gjend0") != null && GetCurrentColumnValue("gjend1") != null)
            {
                gjenVitPara3 += Convert.ToDouble(GetCurrentColumnValue("gjend1"));
                ndrAbs3 += Convert.ToDouble(GetCurrentColumnValue("gjend0"));
            }
        }

        private void xrLabel204_SummaryReset(object sender, EventArgs e)
        {
            ndrAbs3 = 0;
            gjenVitPara3 = 0;
        }

        private void xrLabel201_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("gjend0") != null && GetCurrentColumnValue("gjend1") != null)
            {
                gjenVitPara4 += Convert.ToDouble(GetCurrentColumnValue("gjend1"));
                ndrAbs4 += Convert.ToDouble(GetCurrentColumnValue("gjend0"));
            }
        }

        private void xrLabel201_SummaryReset(object sender, EventArgs e)
        {
            ndrAbs4 = 0;
            gjenVitPara4 = 0;
        }

        private void xrLabel205_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("gjend0") != null && GetCurrentColumnValue("gjend1") != null)
            {
                gjenVitPara6 += Convert.ToDouble(GetCurrentColumnValue("gjend1"));
                ndrAbs6 += Convert.ToDouble(GetCurrentColumnValue("gjend0"));
            }
        }

        private void xrLabel205_SummaryReset(object sender, EventArgs e)
        {
            ndrAbs6 = 0;
            gjenVitPara6 = 0;
        }

        private void xrLabel34_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (total1 >= 0)
            {
                e.Result = string.Format("{0:#,#.00}", total1);
                xrLabel34.Text = string.Format("{0:#,#.00}", total1);
            }
            else
            {
                e.Result = "(" + string.Format("{0:#,#.00}", -total1) + ")";
                xrLabel34.Text = "(" + string.Format("{0:#,#.00}", total1) + ")";
            }
            e.Handled = true;
        }

        private void xrLabel36_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (total2 >= 0)
            {
                e.Result = string.Format("{0:#,#.00}", total2);
                xrLabel36.Text = string.Format("{0:#,#.00}", total2);
            }
            else
            {
                e.Result = "(" + string.Format("{0:#,#.00}", -total2) + ")";
                xrLabel36.Text = "(" + string.Format("{0:#,#.00}", total2) + ")";
            }
            e.Handled = true;
        }

        private void xrLabel57_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            var perqindjetotal = total/total2;
            e.Result = string.Format("{0:#,#.00}", perqindjetotal*100) + " %";
            //  String.Format("{0:P}", perqindjetotal);
            xrLabel57.Text = string.Format("{0:#,#.00}", perqindjetotal*100) + " %";
            e.Handled = true;
        }


        private void xrLabel2_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
        }

        private void xrLabel58_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            total = total1 - total2;

            if (total >= 0)
            {
                e.Result = string.Format("{0:#,#.00}", total);
                xrLabel58.Text = string.Format("{0:#,#.00}", total);
            }
            else
            {
                e.Result = "(" + string.Format("{0:#,#.00}", -total) + ")";
                xrLabel58.Text = "(" + string.Format("{0:#,#.00}", total) + ")";
            }
            e.Handled = true;
        }
    }
}
