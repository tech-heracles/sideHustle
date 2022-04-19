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
    public partial class Rap_BilanciBuxh : DevExpress.XtraReports.UI.XtraReport, IUpdateDetailKPF
    {
		public Rap_BilanciBuxh(){InitializeComponent();} 
        bool hapurgjitha = false;
        bool gjendje = false;
        CultureInfo ci;
        int niv = 0;
        int nivTemp = 0;
        int formatNumri = 0;
        public Rap_BilanciBuxh(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, report)
        {

        }
        public Rap_BilanciBuxh(CultureInfo ci, DevExpress.XtraReports.UI.XtraReport raport)
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
            if ((raport.Parameters["filterGjendja"].Value).ToString() == "Llogari me gjendje")
                gjendje = true;
            formatNumri = Convert.ToInt32(raport.Parameters["filterFormatNumri"].Value);
            parameterFormatNumri.Value = formatNumri;
            caktoFormatinENumrave();
        }
        private void caktoFormatinENumrave()
        {
            xrLabel11.DataBindings[0].FormatString = xrLabel43.DataBindings[0].FormatString =
            xrLabel69.DataBindings[0].FormatString = xrLabel37.DataBindings[0].FormatString
            = xrLabel46.DataBindings[0].FormatString = xrLabel38.DataBindings[0].FormatString =
            xrLabel16.DataBindings[0].FormatString = xrLabel39.DataBindings[0].FormatString =
            xrLabel17.DataBindings[0].FormatString = xrLabel40.DataBindings[0].FormatString =
            xrLabel14.DataBindings[0].FormatString
            = xrLabel41.DataBindings[0].FormatString = xrLabel13.DataBindings[0].FormatString =
            xrLabel42.DataBindings[0].FormatString =
            xrLabel94.DataBindings[0].FormatString = xrLabel95.DataBindings[0].FormatString =
            xrLabel89.DataBindings[0].FormatString = xrLabel90.DataBindings[0].FormatString =
            xrLabel83.DataBindings[0].FormatString = xrLabel84.DataBindings[0].FormatString =
            xrLabel15.DataBindings[0].FormatString = xrLabel61.DataBindings[0].FormatString =
            xrLabel18.DataBindings[0].FormatString = xrLabel62.DataBindings[0].FormatString =
            xrLabel50.DataBindings[0].FormatString = xrLabel65.DataBindings[0].FormatString = xrLabel67.DataBindings[0].FormatString = xrLabel78.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";

            xrLabel69.Summary.FormatString = xrLabel37.Summary.FormatString =
            xrLabel46.Summary.FormatString = xrLabel38.Summary.FormatString =
            xrLabel16.Summary.FormatString = xrLabel39.Summary.FormatString =
            xrLabel17.Summary.FormatString = xrLabel40.Summary.FormatString = xrLabel14.Summary.FormatString
            = xrLabel41.Summary.FormatString = xrLabel13.Summary.FormatString = xrLabel42.Summary.FormatString = xrLabel94.Summary.FormatString = xrLabel95.Summary.FormatString = xrLabel89.Summary.FormatString =
            xrLabel90.Summary.FormatString = xrLabel83.Summary.FormatString = xrLabel84.Summary.FormatString = xrLabel15.Summary.FormatString = xrLabel61.Summary.FormatString = xrLabel18.Summary.FormatString = xrLabel62.Summary.FormatString =
            xrLabel50.Summary.FormatString = xrLabel65.Summary.FormatString = xrLabel67.Summary.FormatString = xrLabel78.Summary.FormatString = "{0:n" + formatNumri + "}";


            xrLabel11.XlsxFormatString = xrLabel43.XlsxFormatString = xrLabel69.XlsxFormatString = xrLabel37.XlsxFormatString =
            xrLabel46.XlsxFormatString = xrLabel38.XlsxFormatString =
            xrLabel16.XlsxFormatString = xrLabel39.XlsxFormatString
            = xrLabel17.XlsxFormatString = xrLabel40.XlsxFormatString
            = xrLabel14.XlsxFormatString = xrLabel41.XlsxFormatString
            = xrLabel13.XlsxFormatString  = xrLabel42.XlsxFormatString = xrLabel94.XlsxFormatString =
            xrLabel95.XlsxFormatString = xrLabel89.XlsxFormatString =
            xrLabel90.XlsxFormatString = xrLabel83.XlsxFormatString =
                       xrLabel84.XlsxFormatString =
                       xrLabel15.XlsxFormatString =
                       xrLabel61.XlsxFormatString =
                       xrLabel18.XlsxFormatString =
                       xrLabel62.XlsxFormatString =
                       xrLabel50.XlsxFormatString =
                       xrLabel65.XlsxFormatString = xrLabel67.XlsxFormatString = xrLabel78.XlsxFormatString = 0.ToString("N" + formatNumri);
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
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";Detail;bilanci;buxhetor')";
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
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;bilanci;buxhetor')";

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
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;bilanci;buxhetor')";
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
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;bilanci;buxhetor')";
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
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;bilanci;buxhetor')";
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
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;bilanci;buxhetor')";
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
            XRLabel label = sender as XRLabel;
            label.Text = xrLabel32.Text + "." + xrLabel23.Text;
        }

        private void LlogaritSummary(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = shuma.ToString("N" + formatNumri);
            else
                e.Result = "(" + (-shuma).ToString("N" + formatNumri) + ")";
            e.Handled = true;
        }
       
        

        private void xrLabel67_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {

            if (diferenca >= 0)
                e.Result = diferenca.ToString("N" + formatNumri);
            else
                e.Result = "(" + (-diferenca).ToString("N" + formatNumri) + ")";
            e.Handled = true;

        }

        private void xrLabel67_SummaryCalculated(object sender, TextFormatEventArgs e)
        {


        }

        double diferenca = 0;
        double diferenca1 = 0;
        private void xrLabel67_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("shenja") != null)
            {
                if (GetCurrentColumnValue("shenja").ToString() == "Pozitive")
                    diferenca += double.Parse(GetCurrentColumnValue("gjend").ToString());
                else diferenca -= double.Parse(GetCurrentColumnValue("gjend").ToString());
            }
        }

        private void xrLabel67_SummaryReset(object sender, EventArgs e)
        {
            diferenca = 0;
        }

        private void xrLabel11_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            double shuma = 0;

            if (label.Text.Contains("("))
                shuma = -double.Parse(label.Text.ToString().Replace('(', ' ').Replace(')', ' '));
            else shuma = double.Parse(label.Text.ToString());
            if (shuma >= 0)
                label.Text = shuma.ToString("N" + formatNumri);

            else label.Text = "(" + (-shuma).ToString("N" + formatNumri) + ")";
        }

        


    /// <summary>
    /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
    /// </summary>
    /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
    private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportBilanciTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel19.Text = rm.GetString("labelRaportiEmertimiLlogariveAktivitUpper", ci);
            xrLabel85.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel20.Text = rm.GetString("labelRaportiShenime", ci);
            xrLabel21.Text = rm.GetString("labelRaportiUshtrimiMbyllur", ci);
            xrLabel22.Text = rm.GetString("labelRaportiUshtrimiParaardhes", ci);
            xrLabel56.Text = rm.GetString("labelRaportiShuma", ci);
            xrLabel58.Text = rm.GetString("labelRaportiTotali", ci);
            //xrLabel63.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel66.Text = rm.GetString("labelRaportiDiferenca", ci);
            xrLabel70.Text = rm.GetString("filterMonedha", ci);
            xrLabel14.Text = rm.GetString("labelRaportiNrLlog", ci);
            xrLabel20.Text = rm.GetString("labelRaportiNrLower", ci);
        }

  
        private void xrLabel64_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (xrLabel64.Text == "Aktivet")
                xrLabel64.Text = "Totali " + rm.GetString("labelRaportiAktive", ci);
            else xrLabel64.Text = "Totali " + rm.GetString("labelRaportiKapitali", ci);
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
        }

        private void xrLabel44_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
            xrLabel44.Text = niv.ToString();
        }

        private void xrLabel44_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel45_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
            xrLabel45.Text = niv.ToString();
        }

        private void xrLabel45_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel47_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
            xrLabel47.Text = niv.ToString();
        }

        private void xrLabel47_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel48_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
            xrLabel48.Text = niv.ToString();
        }

        private void xrLabel48_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel49_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
            xrLabel49.Text = niv.ToString();
        }

        private void xrLabel49_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }


        private void xrLabel51_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel79_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
            xrLabel79.Text = niv.ToString();
        }

        private void xrLabel79_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel86_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
            xrLabel86.Text = niv.ToString();
        }

        private void xrLabel86_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel91_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv++;
            xrLabel91.Text = niv.ToString();
        }

        private void xrLabel91_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = niv;
            e.Handled = true;
        }

        private void xrLabel49_SummaryReset(object sender, EventArgs e)
        {

        }

      

        private void xrLabel33_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = nivTemp + 1;
            e.Handled = true;
        }

        private void xrLabel33_SummaryReset(object sender, EventArgs e)
        {
        
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
           // niv = 0;
        }

        private void xrLabel43_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            double shuma = 0;

            if (label.Text.Contains("("))
                shuma = -double.Parse(label.Text.ToString().Replace('(', ' ').Replace(')', ' '));
            else shuma = double.Parse(label.Text.ToString());
            if (shuma >= 0)
                label.Text = shuma.ToString("N" + formatNumri);

            else label.Text = "(" + (-shuma).ToString("N" + formatNumri) + ")";
        }
        
        private void xrLabel78_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (diferenca1 >= 0)
                e.Result = diferenca1.ToString("N" + formatNumri);
            else
                e.Result = "(" + (-diferenca1).ToString("N" + formatNumri) + ")";
            e.Handled = true;
        }

        private void xrLabel78_SummaryReset(object sender, EventArgs e)
        {
            diferenca1 = 0;
        }

        private void xrLabel78_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("shenja") != null)
            {
                if (GetCurrentColumnValue("shenja").ToString() == "Pozitive")
                    diferenca1 += double.Parse(GetCurrentColumnValue("gjend1").ToString());
                else diferenca1 -= double.Parse(GetCurrentColumnValue("gjend1").ToString());
            }
        }

        private void xrLabel24_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;

        }

        private void xrLabel1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;

            if (GetCurrentColumnValue("NIVELI") != null && GetCurrentColumnValue("NIVELI").ToString() != null && GetCurrentColumnValue("NIVELI").ToString() != "")
            {
                if (Convert.ToInt32(GetCurrentColumnValue("NIVELI")) == 1 || Convert.ToInt32(GetCurrentColumnValue("NIVELI")) == 2 || Convert.ToInt32(GetCurrentColumnValue("NIVELI")) == 3)
                {
                    label.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                }
            }
            else
                label.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            niv = 0;
        }
        private void GroupHeader4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (gjendje && Convert.ToInt32(GetCurrentColumnValue("gjend")) == 0)
            //    GroupHeader4.Visible = false;
        }
    }
}
