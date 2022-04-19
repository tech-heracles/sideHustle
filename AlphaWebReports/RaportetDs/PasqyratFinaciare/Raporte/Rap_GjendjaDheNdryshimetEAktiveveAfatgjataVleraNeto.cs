using System;
using System.Collections;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.PasqyratFinaciare.Raporte
{
    public partial class Rap_GjendjaDheNdryshimetEAktiveveAfatgjataVleraNeto : DevExpress.XtraReports.UI.XtraReport, IUpdateDetailKPF
    {
        bool hapurgjitha = false;
        private int niv = 0;
        public Rap_GjendjaDheNdryshimetEAktiveveAfatgjataVleraNeto() { InitializeComponent(); }
        public Rap_GjendjaDheNdryshimetEAktiveveAfatgjataVleraNeto( ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {
        }
        public Rap_GjendjaDheNdryshimetEAktiveveAfatgjataVleraNeto(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            MonNder.Value = raport.Parameters[4].Value;
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            Azhornim.Value = raport.Parameters[3].Value;
            parameter3.Value = raport.Parameters[5].Value;
            hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);
            EmrateLabelave(ci);
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            var rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportiGjendjaDheNdryshimetAktiveveAfatgjataVleraNeto", ci);

            xrTableCell178.Text = rm.GetString("labelRaportiNr", ci) + " "+ rm.GetString("labelRaportRreshti", ci);
            xrTableCell179.Text = rm.GetString("filterNrReference", ci).Replace(":","");
            xrTableCell180.Text = rm.GetString("labelEmertimi", ci).ToUpper();
            xrTableCell188.Text = rm.GetString("labelRaportiKostoHistorike", ci);
            xrTableCell206.Text = rm.GetString("labelRaportiKostoHistorike", ci);
            xrTableCell217.Text = rm.GetString("labelRaportiKostoHistorike", ci);
            xrTableCell218.Text = rm.GetString("labelRaportiKostoHistorike", ci);
            xrTableCell208.Text = rm.GetString("labelRaportAmortizimiAkumuluar", ci);
            xrTableCell190.Text = rm.GetString("labelRaportAmortizimiAkumuluar", ci);
            xrTableCell189.Text = rm.GetString("MenuItemAmortizimi", ci);
            xrTableCell219.Text = rm.GetString("MenuItemAmortizimi", ci);
            xrTableCell207.Text = rm.GetString("labelRptTepricaNeto", ci);
            xrTableCell191.Text = rm.GetString("labelRptTepricaNeto", ci);
            xrTableCell182.Text = rm.GetString("lblRaportTepricaNeFillim", ci);
            xrTableCell181.Text = rm.GetString("lblRaportShtesatGjateVitit", ci);
            xrTableCell184.Text = rm.GetString("lblRapotiPakesimetGjateVitit", ci);
            xrTableCell183.Text = rm.GetString("lblRaportTepricaNeFund", ci);
        }

        private Hashtable skippedDetailBands;
        private Hashtable skippedDetailKPF;
        public Hashtable SkippedDetailBands
        {
            get
            {
                if (skippedDetailBands == null)
                {
                    skippedDetailBands = new Hashtable();
                }
                return skippedDetailBands;
            }
            set
            {
                skippedDetailBands = value;
            }
        }
        public Hashtable SkippedDetailKPF
        {
            get
            {
                if (skippedDetailKPF == null)
                {
                    skippedDetailKPF = new Hashtable();
                }
                return skippedDetailKPF;
            }
            set
            {
                skippedDetailKPF = value;
            }
        }
        public void UpdateDetailKPF(string detailID)
        {
            if (SkippedDetailKPF.Contains(detailID))
            {
                SkippedDetailKPF[detailID] = !Convert.ToBoolean(SkippedDetailKPF[detailID]);
            }
            else
            {
                SkippedDetailKPF.Add(detailID, false);
            }
            this.CreateDocument();
        }
        public void UpdateDetail(string detailID)
        {
            if (SkippedDetailBands.Contains(detailID))
            {
                SkippedDetailBands[detailID] = !Convert.ToBoolean(SkippedDetailBands[detailID]);
            }
            else
            {
                SkippedDetailBands.Add(detailID, false);
            }
            this.CreateDocument();
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("kodikpf") == null && GetCurrentColumnValue("PERSHKRIMIZERIT") == null)
                return;
            var catid = GetCurrentColumnValue("kodikpf").ToString();
            var catid2 = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
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
                e.Cancel = !hapurgjitha;
                SkippedDetailBands.Add(catid, !hapurgjitha);
            }
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") == null) return;
            var catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();

            if (SkippedDetailKPF.Contains(catid))
                e.Cancel = Convert.ToBoolean(SkippedDetailKPF[catid]);
            else
            {
                e.Cancel = !hapurgjitha;
                SkippedDetailKPF.Add(catid, !hapurgjitha);
            }
        }

        private void xrTableCell6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = sender as XRLabel;
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") == null) return;
            var catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = string.Empty;
            if (GetCurrentColumnValue("lloji").ToString() == string.Empty || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
                label.Text = string.Empty;
            else
            {
                label.NavigateUrl = "javascript:window.parent.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;GjendjaDheNdryshimetEAktiveveAfatgjataVleraNeto')";
                if (SkippedDetailKPF.Contains(catid) && (bool)SkippedDetailKPF[catid] == false)
                    label.Text = "-";
                else
                    label.Text = "+";
            }
        }

        private void xrTableCell2_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (var i = 0; i < e.CalculatedValues.Count; i++)
            {
                if (e.CalculatedValues[i].ToString().Contains("("))
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                else
                    shuma += double.Parse(e.CalculatedValues[i].ToString());
            }
            //if (shuma >= 0)
                e.Result = String.Format("{0:#,#.00}", shuma);
            //else
            //    e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            e.Handled = true;
        }

        private void xrTableCell15_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = sender as XRLabel;

            if (GetCurrentColumnValue("PERSHKRIMIZERIT") == null)
                return;
            var catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = string.Empty;
            if (GetCurrentColumnValue("lloji").ToString() == string.Empty || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
                label.Text = string.Empty;
            else
            {
                label.NavigateUrl = "javascript:window.parent.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;GjendjaDheNdryshimetEAktiveveAfatgjataVleraNeto')";
                if (SkippedDetailKPF.Contains(catid) && (bool)SkippedDetailKPF[catid] == false)
                    label.Text = "-";
                else
                    label.Text = "+";
            }
        }

        private void xrTableCell30_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = sender as XRLabel;
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") == null) return;
            var catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = string.Empty;
            if (GetCurrentColumnValue("lloji").ToString() == string.Empty || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
                label.Text = string.Empty;
            else
            {
                label.NavigateUrl = "javascript:window.parent.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;GjendjaDheNdryshimetEAktiveveAfatgjataVleraNeto')";
                if (SkippedDetailKPF.Contains(catid) && (bool)SkippedDetailKPF[catid] == false)
                    label.Text = "-";
                else
                    label.Text = "+";
            }
        }
        private void xrTableCell45_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = sender as XRLabel;
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") == null) return;
            var catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = string.Empty;
            if (GetCurrentColumnValue("lloji").ToString() == string.Empty || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
                label.Text = string.Empty;
            else
            {
                label.NavigateUrl = "javascript:window.parent.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;GjendjaDheNdryshimetEAktiveveAfatgjataVleraNeto')";
                if (SkippedDetailKPF.Contains(catid) && (bool)SkippedDetailKPF[catid] == false)
                    label.Text = "-";
                else
                    label.Text = "+";
            }
        }

        private void xrTableCell60_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = sender as XRLabel;
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") == null) return;
            var catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = string.Empty;
            if (GetCurrentColumnValue("lloji").ToString() == string.Empty || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
                label.Text = string.Empty;
            else
            {
                label.NavigateUrl = "javascript:window.parent.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;GjendjaDheNdryshimetEAktiveveAfatgjataVleraNeto')";
                if (SkippedDetailKPF.Contains(catid) && (bool)SkippedDetailKPF[catid] == false)
                    label.Text = "-";
                else
                    label.Text = "+";
            }
        }

        private void xrTableCell76_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = sender as XRLabel;
            if (GetCurrentColumnValue("kodikpf") == null) return;
            var catid = GetCurrentColumnValue("kodikpf").ToString();
            if (GetCurrentColumnValue("NRLLOGARI").ToString() == string.Empty || (GetCurrentColumnValue("SHFAQBIJLLOG") != null && GetCurrentColumnValue("SHFAQBIJLLOG").ToString() == "False"))
                label.Text = string.Empty;
            else
            {
                label.NavigateUrl = "javascript:window.parent.ASPxCallbackPanel1.PerformCallback('" + catid + ";Detail;GjendjaDheNdryshimetEAktiveveAfatgjataVleraNeto')";
                if (!SkippedDetailBands.Contains(catid))
                {
                    if (hapurgjitha)
                        label.Text = "-";
                    else
                        label.Text = "+";
                }
                else
                {
                    if ((bool)SkippedDetailBands[catid] == false)
                        label.Text = "-";
                    else
                        label.Text = "+";
                }
            }
        }
    }
}
