using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Collections.Generic;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.Prodhimi.Raportet
{
    public partial class Rap_KonsumiPermbledhesIBurimeveSipasSkedulimit : DevExpress.XtraReports.UI.XtraReport, IUpdateDetail
    {
		public Rap_KonsumiPermbledhesIBurimeveSipasSkedulimit(){InitializeComponent();} 

        

        Dictionary<string, bool> skippedDetailBands = new Dictionary<string, bool>();
        Dictionary<int, bool> idKokaPlanifikim = new Dictionary<int, bool>();
        bool hapurgjitha = false;
        public Dictionary<string, bool> SkippedDetailBands
        {
            get
            {
                if (skippedDetailBands == null)
                    skippedDetailBands = new Dictionary<string, bool>();
                return skippedDetailBands;
            }
            set { skippedDetailBands = value; }
        }
        public Rap_KonsumiPermbledhesIBurimeveSipasSkedulimit(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_KonsumiPermbledhesIBurimeveSipasSkedulimit(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            nrLlog.Text = raport.Parameters[7].Description;
            parameter1.Value = raport.Parameters[7].Value;
            ndermarrja.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            magazina.Text = raport.Parameters[3].Description;
            parameter4.Value = raport.Parameters[3].Value;
            dtDok.Text = raport.Parameters[4].Description;
            parameter7.Value = raport.Parameters[4].Value;
            dtRegj.Text = raport.Parameters[5].Description;
            parameter8.Value = raport.Parameters[5].Value;
            nrDok.Text = raport.Parameters[6].Description;
            parameter9.Value = raport.Parameters[6].Value;
            hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("lblTitullRaportKonsumiPermbledhesIBurSipasSked", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel36.Text = rm.GetString("lblLloji2", ci);
            xrLabel37.Text = rm.GetString("cmbCmimeArtikulliCaptionKodi", ci);
            xrLabel38.Text = rm.GetString("labelRaportiEmertimi", ci);
            xrLabel42.Text = rm.GetString("labelRaportData", ci);
            xrLabel39.Text = rm.GetString("labelNjesia", ci);
            xrLabel43.Text = rm.GetString("labelSasia", ci);
            xrLabel1.Text = rm.GetString("lblRptKostoPerNjesi", ci);
            xrLabel14.Text = rm.GetString("labelVlefta", ci);
            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel5.Text = rm.GetString("burimiTab", ci);
            xrLabel26.Text = rm.GetString("labelRaportKonsumi", ci);
        }

        private void xrLabel6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            if (GetCurrentColumnValue("KODI") != System.DBNull.Value && GetCurrentColumnValue("KODI") != null)
            {
                string kodi = GetCurrentColumnValue("KODI").ToString();
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + kodi + ";Detail;konsumPermbledhesBurimeshSkedulim')";
                if (!SkippedDetailBands.ContainsKey(kodi))
                    if (hapurgjitha)
                        label.Text = "-";
                    else label.Text = "+";
                else if ((bool)SkippedDetailBands[kodi] == false)
                    label.Text = "-";
                else
                    label.Text = "+";
            }
            else
            {
                label.Text = "";
            }
        }

        public void UpdateDetail(string kodi)
        {
            if (SkippedDetailBands.ContainsKey(kodi))
                SkippedDetailBands[kodi] = !Convert.ToBoolean(SkippedDetailBands[kodi]);
            else
                SkippedDetailBands.Add(kodi, false);
            
        }
        double sasiPlan, sasiPlan2, sasi, sasi2, kostoNjesi, kostoNjesi2, shumaVlefta; int nr, nr2;
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KODI") != null)
            {
                string kodi = GetCurrentColumnValue("KODI").ToString();
                if (SkippedDetailBands.ContainsKey(kodi))
                    e.Cancel = Convert.ToBoolean(SkippedDetailBands[kodi]);
                else
                {
                    e.Cancel = !hapurgjitha;
                    SkippedDetailBands.Add(kodi, !hapurgjitha);
                }
            }
            if (GetCurrentColumnValue("KostoPerNjesi") != DBNull.Value)
            {
                kostoNjesi += Convert.ToDouble(GetCurrentColumnValue("KostoPerNjesi"));
                nr++;
                kostoNjesi2 = kostoNjesi;
                nr2 = nr;

            }
            if (GetCurrentColumnValue("sasiplanshuma") != DBNull.Value)
                sasiPlan += Convert.ToDouble(GetCurrentColumnValue("sasiplanshuma"));
            if (GetCurrentColumnValue("SASIA") != DBNull.Value)
                sasi += Convert.ToDouble(GetCurrentColumnValue("SASIA"));
            sasiPlan2 = sasiPlan;
            sasi2 = sasi;
        }

        private void xrLabel4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        double sasiaPlan = 0;
        double sasia = 0;
        double rendimenti = 0;
        private void xrLabel18_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("SASIA") != DBNull.Value && GetCurrentColumnValue("sasiplanshuma") != DBNull.Value)
            {
                sasia += Convert.ToDouble(GetCurrentColumnValue("SASIA"));
                sasiaPlan += Convert.ToDouble(GetCurrentColumnValue("sasiplanshuma"));
            }
        }

        private void Rap_KonsumiPermbledhesIBurimeveSipasSkedulimit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);

        }

        private void xrLabel18_SummaryReset(object sender, EventArgs e)
        {
            sasiaPlan = 0;
            sasia = 0;
        }

        private void xrLabel18_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            rendimenti = sasia == 0 ? 0 : sasiaPlan / sasia;
            e.Result = String.Format("{0:#,0.000}", rendimenti);
            e.Handled = true;
        }

        private void xrLabel56_SummaryReset(object sender, EventArgs e)
        {
            sasiPlan = 0;
            sasi = 0;
            kostoNjesi = 0;
            nr = 0;
        }

        private void xrLabel56_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double rendimenti = sasi == 0 ? 0 : sasiPlan / sasi;
            double rezultati = sasiPlan * rendimenti * kostoNjesi / nr;
            e.Result = String.Format("{0:#,0.00}", rezultati);
            shumaVlefta += rezultati;
            e.Handled = true;
        }

        private void GroupHeader2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KODI") != null)
            {
                string kodi = GetCurrentColumnValue("KODI").ToString();
                if (SkippedDetailBands.ContainsKey(kodi))
                    e.Cancel = Convert.ToBoolean(SkippedDetailBands[kodi]);
                else
                {
                    e.Cancel = !hapurgjitha;
                    SkippedDetailBands.Add(kodi, !hapurgjitha);
                }
            }
        }

        private void xrLabel22_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = String.Format("{0:#,0.00}", shumaVlefta);
            e.Handled = true;
        }

        private void xrLabel22_SummaryReset(object sender, EventArgs e)
        {
            shumaVlefta = 0;
        }
    }
}
