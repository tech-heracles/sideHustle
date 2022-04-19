using System;
using System.Collections;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using AlphaWebReports.Common;


namespace AlphaWebReports.RaportetDs.PasqyratFinaciare.Raporte
{
    public partial class Rap_Gjendja_ndryshimet_aktiveve_te_qendrueshme : DevExpress.XtraReports.UI.XtraReport, IUpdateDetailKPF
    {
		public Rap_Gjendja_ndryshimet_aktiveve_te_qendrueshme(){InitializeComponent();} 
        bool hapurgjitha = false;
        bool gjendje = false;
        public Rap_Gjendja_ndryshimet_aktiveve_te_qendrueshme(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_Gjendja_ndryshimet_aktiveve_te_qendrueshme(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            MonNder.Value = raport.Parameters[4].Value;
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            Azhornim.Value = raport.Parameters[3].Value;
            parameter3.Value = raport.Parameters[5].Value;
            hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);
            if ((raport.Parameters["filterGjendja"].Value).ToString() == "Llogari me gjendje")
                gjendje = true;
            EmrateLabelave(ci);
        }
        private string[] shkronjevogel = { string.Empty, "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t" };
        private string[] shkronjemadhe = { string.Empty, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T" };

        private int niv = 0;

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
            {
                e.Cancel = Convert.ToBoolean(SkippedDetailBands[catid]);
            }
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
            {
                e.Cancel = Convert.ToBoolean(SkippedDetailKPF[catid]);
            }
            else
            {
                e.Cancel = !hapurgjitha;
                SkippedDetailKPF.Add(catid, !hapurgjitha);
            }
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            var rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportGjendjaNdryshimetAktiveveTitulli", ci);
        }

        private void xrTableCell6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = sender as XRLabel;
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") == null) return;
            var catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = string.Empty;
            if (GetCurrentColumnValue("lloji").ToString() == string.Empty || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
            {
                label.Text = string.Empty;
            }
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;gjendjeNdryshimeAktiveTeQendrueshem')";
                if (SkippedDetailKPF.Contains(catid) && (bool)SkippedDetailKPF[catid] == false)
                {
                    label.Text = "-";
                }
                else
                {
                    label.Text = "+";
                }
            }
        }

        private void xrTableCell2_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (var i = 0; i < e.CalculatedValues.Count; i++)
            {
                if (e.CalculatedValues[i].ToString().Contains("("))
                {
                    shuma -= double.Parse(e.CalculatedValues[i].ToString().Replace('(', ' ').Replace(')', ' '));
                }
                else
                {
                    shuma += double.Parse(e.CalculatedValues[i].ToString());
                }
            }
            if (shuma >= 0)
            {
                e.Result = String.Format("{0:#,#.00}", shuma);
            }
            else
            {
                e.Result = "(" + String.Format("{0:#,#.00}", -shuma) + ")";
            }
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
            {
                label.Text = string.Empty;
            }
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;gjendjeNdryshimeAktiveTeQendrueshem')";
                if (SkippedDetailKPF.Contains(catid) && (bool)SkippedDetailKPF[catid] == false)
                {
                    label.Text = "-";
                }
                else
                {
                    label.Text = "+";
                }
            }
        }

        private void xrTableCell30_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = sender as XRLabel;
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") == null) return;
            var catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = string.Empty;
            if (GetCurrentColumnValue("lloji").ToString() == string.Empty || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
            {
                label.Text = string.Empty;
            }
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;gjendjeNdryshimeAktiveTeQendrueshem')";
                if (SkippedDetailKPF.Contains(catid) && (bool)SkippedDetailKPF[catid] == false)
                {
                    label.Text = "-";
                }
                else
                {
                    label.Text = "+";
                }
            }
        }
        private void xrTableCell45_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = sender as XRLabel;
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") == null) return;
            var catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = string.Empty;
            if (GetCurrentColumnValue("lloji").ToString() == string.Empty || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
            {
                label.Text = string.Empty;
            }
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;gjendjeNdryshimeAktiveTeQendrueshem')";
                if (SkippedDetailKPF.Contains(catid) && (bool)SkippedDetailKPF[catid] == false)
                {
                    label.Text = "-";
                }
                else
                {
                    label.Text = "+";
                }
            }
        }

        private void xrTableCell60_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = sender as XRLabel;
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") == null) return;
            var catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = string.Empty;
            if (GetCurrentColumnValue("lloji").ToString() == string.Empty || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
            {
                label.Text = string.Empty;
            }
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;gjendjeNdryshimeAktiveTeQendrueshem')";
                if (SkippedDetailKPF.Contains(catid) && (bool)SkippedDetailKPF[catid] == false)
                {
                    label.Text = "-";
                }
                else
                {
                    label.Text = "+";
                }
            }
        }

        private void xrTableCell76_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = sender as XRLabel;

            if (GetCurrentColumnValue("kodikpf") == null) return;
            var catid = GetCurrentColumnValue("kodikpf").ToString();


            if (GetCurrentColumnValue("NRLLOGARI").ToString() == string.Empty || (GetCurrentColumnValue("SHFAQBIJLLOG") != null && GetCurrentColumnValue("SHFAQBIJLLOG").ToString() == "False"))
            {
                label.Text = string.Empty;
            }
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";Detail;gjendjeNdryshimeAktiveTeQendrueshem')";
                if (!SkippedDetailBands.Contains(catid))
                {
                    if (hapurgjitha)
                    {
                        label.Text = "-";
                    }
                    else
                    {
                        label.Text = "+";
                    }
                }
                else
                {
                    if ((bool)SkippedDetailBands[catid] == false)
                    {
                        label.Text = "-";
                    }
                    else
                    {
                        label.Text = "+";
                    }
                }
            }
        }
    }
}
