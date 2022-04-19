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
    public partial class Rap_PasqyraPozicionFinanciar : DevExpress.XtraReports.UI.XtraReport, IUpdateDetailKPF
    {
        public Rap_PasqyraPozicionFinanciar()
        {
            InitializeComponent();
        }

        bool hapurgjitha = false;
        CultureInfo ci;
        public Rap_PasqyraPozicionFinanciar(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, report)
        {

        }
        int shifraPasPresjes = 0;
        public Rap_PasqyraPozicionFinanciar(CultureInfo ci, DevExpress.XtraReports.UI.XtraReport raport)
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
            shifraPasPresjes = Convert.ToInt32(raport.Parameters["filterFormatNumri"].Value);
            caktoFormatinENumrave();
        }
    private void caktoFormatinENumrave()
    {
            xrTableCell10.DataBindings[0].FormatString = xrTableCell12.DataBindings[0].FormatString = xrTableCell19.DataBindings[0].FormatString = xrTableCell26.DataBindings[0].FormatString = xrTableCell33.DataBindings[0].FormatString = xrTableCell48.DataBindings[0].FormatString =
   xrTableCell54.DataBindings[0].FormatString = xrTableCell59.DataBindings[0].FormatString = xrTableCell62.DataBindings[0].FormatString = xrTableCell67.DataBindings[0].FormatString = xrTableCell82.DataBindings[0].FormatString
   = xrTableCell87.DataBindings[0].FormatString = xrTableCell34.DataBindings[0].FormatString = xrTableCell3.DataBindings[0].FormatString = xrTableCell27.DataBindings[0].FormatString = xrTableCell20.DataBindings[0].FormatString = xrTableCell60.DataBindings[0].FormatString = xrTableCell63.DataBindings[0].FormatString =
   xrTableCell68.DataBindings[0].FormatString =
   xrTableCell45.DataBindings[0].FormatString =
   xrTableCell55.DataBindings[0].FormatString =
   xrTableCell88.DataBindings[0].FormatString =
   xrTableCell83.DataBindings[0].FormatString =
   xrTableCell74.DataBindings[0].FormatString =
   xrTableCell75.DataBindings[0].FormatString =
   xrTableCell40.DataBindings[0].FormatString =
   xrTableCell7.DataBindings[0].FormatString =
   xrTableCell3.DataBindings[0].FormatString =
   xrTableCell41.DataBindings[0].FormatString = "{0:n" + shifraPasPresjes + "}";

xrTableCell19.Summary.FormatString=xrTableCell26.Summary.FormatString=xrTableCell33.Summary.FormatString=xrTableCell48.Summary.FormatString=
xrTableCell54.Summary.FormatString=xrTableCell59.Summary.FormatString=xrTableCell62.Summary.FormatString=xrTableCell67.Summary.FormatString=xrTableCell82.Summary.FormatString
=xrTableCell87.Summary.FormatString=xrTableCell34.Summary.FormatString=xrTableCell3.Summary.FormatString=xrTableCell27.Summary.FormatString=xrTableCell20.Summary.FormatString=xrTableCell60.Summary.FormatString = xrTableCell63.Summary.FormatString =
xrTableCell68.Summary.FormatString =
xrTableCell45.Summary.FormatString =
xrTableCell55.Summary.FormatString =
xrTableCell88.Summary.FormatString =
xrTableCell83.Summary.FormatString =
xrTableCell74.Summary.FormatString =
xrTableCell75.Summary.FormatString =
xrTableCell40.Summary.FormatString =
xrTableCell7.Summary.FormatString =
xrTableCell3.Summary.FormatString =
xrTableCell41.Summary.FormatString = "{0:n" + shifraPasPresjes + "}";


            xrTableCell10.XlsxFormatString = xrTableCell12.XlsxFormatString = xrTableCell19.XlsxFormatString = xrTableCell26.XlsxFormatString = xrTableCell33.XlsxFormatString = xrTableCell48.XlsxFormatString =
           xrTableCell54.XlsxFormatString = xrTableCell59.XlsxFormatString = xrTableCell62.XlsxFormatString = xrTableCell67.XlsxFormatString = xrTableCell82.XlsxFormatString
           = xrTableCell87.XlsxFormatString = xrTableCell34.XlsxFormatString = xrTableCell3.XlsxFormatString = xrTableCell27.XlsxFormatString = xrTableCell20.XlsxFormatString = xrTableCell60.XlsxFormatString = xrTableCell63.XlsxFormatString =
           xrTableCell68.XlsxFormatString =
           xrTableCell45.XlsxFormatString =
           xrTableCell55.XlsxFormatString =
           xrTableCell88.XlsxFormatString =
           xrTableCell83.XlsxFormatString =
           xrTableCell74.XlsxFormatString =
           xrTableCell75.XlsxFormatString =
           xrTableCell40.XlsxFormatString =
           xrTableCell7.XlsxFormatString =
           xrTableCell3.XlsxFormatString =
           xrTableCell41.XlsxFormatString = 0.ToString("N" + shifraPasPresjes);
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


        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel12.Text = rm.GetString("labelPasqyraPozicionFinanciar", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel19.Text = rm.GetString("labelRaportiEmertimi", ci);
            xrLabel85.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel20.Text = rm.GetString("labelRaportiShenime", ci);
            xrLabel21.Text = rm.GetString("labelRaportiVitiRaportues", ci);
            xrLabel22.Text = rm.GetString("labelRaportiVitiParaardhes", ci);
            xrTableCell43.Text = rm.GetString("labelRaportiShuma", ci);
            xrTableCell50.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell84.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell69.Text = rm.GetString("labelRaportiDiferenca", ci);
            xrLabel70.Text = rm.GetString("filterMonedha", ci);
        }

        private void xrTableCell2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //lblPrindi4_BeforePrint Group header 3
            XRTableCell label = sender as XRTableCell;
            string catid = "";
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null) catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji") == null || GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ4") != null && GetCurrentColumnValue("SHFAQBIJ4").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;bilanciOjf;joBuxhetor')";
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

        private void xrTableCell7_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            LlogaritTotal(e);
        }

        private void xrTableCell12_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            double shuma = 0;

            if (label.Text == "")
                return;
            if (label.Text.Contains("("))
                shuma = -double.Parse(label.Text.ToString().Replace('(', ' ').Replace(')', ' '));
            else shuma = double.Parse(label.Text.ToString());
            if (shuma >= 0)
                label.Text = shuma.ToString("N" + shifraPasPresjes);

            else label.Text = "(" + (-shuma).ToString("N" + shifraPasPresjes) + ")";
        }

        private void xrTableCell15_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //lblKPF_BeforePrint
            XRTableCell label = sender as XRTableCell;
            string catid = "";
            if (GetCurrentColumnValue("kodikpf") != null) catid = GetCurrentColumnValue("kodikpf").ToString();


            if (GetCurrentColumnValue("NRLLOGARI") == null || GetCurrentColumnValue("NRLLOGARI").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJLLOG") != null && GetCurrentColumnValue("SHFAQBIJLLOG").ToString() == "False"))
            {
                label.Text = "";
            }

            else
            {


                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";Detail;bilanciOjf;joBuxhetor')";
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

        private void xrTableCell19_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            LlogaritTotal(e);
        }

        private void xrTableCell22_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //lblprindi5_BeforePrint
            XRTableCell label = sender as XRTableCell;
            string catid = "";
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null) catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji") == null || GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ5") != null && GetCurrentColumnValue("SHFAQBIJ5").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;bilanciOjf;joBuxhetor')";

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

        private void xrTableCell26_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            LlogaritTotal(e);
        }

        private void xrTableCell33_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            LlogaritTotal(e);
        }

        private void xrTableCell36_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //lblPrindi2_BeforePrint
            XRTableCell label = sender as XRTableCell;
            string catid = "";
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null) catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji") == null || GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ2") != null && GetCurrentColumnValue("SHFAQBIJ2").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;bilanciOjf;joBuxhetor')";
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

        private void xrTableCell46_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //xrLabel57_BeforePrint
            //string niv = "";
            //if (niveli1 > 0)
            //    niv = romake[niveli1 - 1];
            //else niv = "I";
            XRTableCell label = sender as XRTableCell;
            if (xrTableCell71.Text != "" && xrTableCell37.Text != "")
                label.Text = xrTableCell71.Text + "." + xrTableCell37.Text;
        }

        private void xrTableCell48_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            LlogaritTotal(e);
        }

        private void xrTableCell51_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //xrLabel59_BeforePrint
            ResourceManager rm = new ResourceManager("Resources.Strings",
                          System.Reflection.Assembly.Load("App_GlobalResources"));
            if (xrTableCell51.Text == "Aktive te pacaktuara")
                xrTableCell51.Text = rm.GetString("labelRaportiAktiveTePacaktuara", ci);
            else if (xrTableCell51.Text == "Pasive te pacaktuara")
                xrTableCell51.Text = rm.GetString("labelRaportiKapitaliTePacaktuara", ci);
        }

        private void xrTableCell54_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            LlogaritTotal(e);
        }

        private void xrTableCell59_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            LlogaritTotal(e);
        }

        private void xrTableCell62_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            LlogaritTotal(e);
        }
        private void xrTableCell67_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            LlogaritTotal(e);
        }

        private void xrTableCell70_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //lblPrindi1_BeforePrint
            XRTableCell label = sender as XRTableCell;
            string catid = "";
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null)
                catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji") == null || GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;bilanciOjf;joBuxhetor')";
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

        private void xrTableCell72_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //xrLabel6_BeforePrint
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            if (xrTableCell72.Text == "Aktive te pacaktuara")
                xrTableCell72.Text = rm.GetString("labelRaportiAktiveTePacaktuara", ci);
            else if (xrTableCell72.Text == "Pasive te pacaktuara")
                xrTableCell72.Text = rm.GetString("labelRaportiKapitaliTePacaktuara", ci);
        }

        private void xrTableCell77_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //xrLabel2_BeforePrint
            ResourceManager rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));
            if (xrTableCell77.Text == "Aktivet")
                xrTableCell77.Text = rm.GetString("labelRaportiAktive", ci);
            else xrTableCell77.Text = rm.GetString("labelRaportiAktiveNeto", ci);
        }

        private void xrTableCell82_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            //xrLabel67_SummaryCalculated
        }

        private void xrTableCell82_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //xrLabel67_SummaryGetResult
            if (diferenca >= 0)
                e.Result = diferenca.ToString("N" + shifraPasPresjes);
            else
                e.Result = "(" + (-diferenca).ToString("N" + shifraPasPresjes) + ")";
            e.Handled = true;
        }

        private void xrTableCell82_SummaryReset(object sender, EventArgs e)
        {
            //xrLabel67_SummaryReset
            diferenca = 0;
        }
        double diferenca = 0;
        double diferenca1 = 0;
        private void xrTableCell82_SummaryRowChanged(object sender, EventArgs e)
        {
            //xrLabel67_SummaryRowChanged
            if (GetCurrentColumnValue("shenja") != null)
            {
                if (GetCurrentColumnValue("shenja").ToString() == "Pozitive")
                    diferenca += double.Parse(GetCurrentColumnValue("gjend").ToString());
                else diferenca -= double.Parse(GetCurrentColumnValue("gjend").ToString());
            }
        }

        private void xrTableCell85_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //xrLabel64_BeforePrint
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));
            if (xrTableCell85.Text == "Aktivet")
                xrTableCell85.Text = rm.GetString("labelRaportiAktive", ci);
            else
                xrTableCell85.Text = rm.GetString("labelRaportiAktiveNeto", ci);
        }

        private void xrTableCell87_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            LlogaritTotal(e);
        }

        private void xrTableCell29_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //lblPrindi3_BeforePrint
            XRTableCell label = sender as XRTableCell;
            string catid = "";
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null) catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji") == null || GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ3") != null && GetCurrentColumnValue("SHFAQBIJ3").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;bilanciOjf;joBuxhetor')";
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

        private void xrTableCell34_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            LlogaritTotal(e);
        }

        private void xrTableCell3_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            LlogaritTotal(e);
        }

        private void xrTableCell27_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            LlogaritTotal(e);
        }

        private void xrTableCell20_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            LlogaritTotal(e);
        }

        private void xrTableCell10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            XRLabel label = sender as XRLabel;
            double shuma = 0;
            if (label.Text == "")
                return;
            if (label.Text.Contains("("))
                shuma = -double.Parse(label.Text.ToString().Replace('(', ' ').Replace(')', ' '));
            else shuma = double.Parse(label.Text.ToString());
            if (shuma >= 0)
                label.Text = shuma.ToString("N" + shifraPasPresjes);

            else label.Text = "(" + (-shuma).ToString("N" + shifraPasPresjes) + ")";

        }

        private void xrTableCell60_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            LlogaritTotal(e);
        }

        private void xrTableCell63_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            LlogaritTotal(e);
        }

        private void xrTableCell68_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            LlogaritTotal(e);
        }

        private void xrTableCell45_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            LlogaritTotal(e);

        }

        private void xrTableCell55_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            LlogaritTotal(e);
        }

        private void xrTableCell88_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            LlogaritTotal(e);
        }

        private void xrTableCell83_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (diferenca1 >= 0)
                e.Result = diferenca1.ToString("N" + shifraPasPresjes);
            else
                e.Result = "(" + (-diferenca1).ToString("N" + shifraPasPresjes) + ")";

            e.Handled = true;
        }

        private void xrTableCell83_SummaryReset(object sender, EventArgs e)
        {
            diferenca1 = 0;
        }

        private void xrTableCell83_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("shenja") != null)
            {
                if (GetCurrentColumnValue("shenja").ToString() == "Pozitive")
                    diferenca1 += double.Parse(GetCurrentColumnValue("gjend1").ToString());
                else diferenca1 -= double.Parse(GetCurrentColumnValue("gjend1").ToString());
            }
        }


        private void ShfaqTotalinLart_Prind1_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue("gjenerototal1") == null)
                return;
            if (GetCurrentColumnValue("gjenerototal1").ToString() != "1")
            {
                e.Result = string.Empty;
                e.Handled = true;
            }
            else
                LlogaritTotal(e);

        }
        private void ShfaqTotalinLart_Prind2_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (GetCurrentColumnValue("gjenerototal2") == null)
                return;
            if (GetCurrentColumnValue("gjenerototal2").ToString() != "1")
            {
                e.Result = string.Empty;
                e.Handled = true;
            }
            else
                LlogaritTotal(e);

        }
        private void LlogaritTotal(SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else shuma += double.Parse(e.CalculatedValues[i].ToString());
            if (shuma >= 0)
                e.Result = shuma.ToString("N" + shifraPasPresjes);
            else
                e.Result = "(" + (-shuma).ToString("N" + shifraPasPresjes) + ")";

            e.Handled = true;
        }
    }


}