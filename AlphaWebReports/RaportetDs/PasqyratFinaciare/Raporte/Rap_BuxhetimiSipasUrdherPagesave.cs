using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Collections.Generic;
using System.Data;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.PasqyratFinaciare.Raporte
{
    public partial class Rap_BuxhetimiSipasUrdherPagesave : DevExpress.XtraReports.UI.XtraReport, IUpdateDetail
    {
        bool hapurgjitha = false;
        private int idNderm;
        private double buxheti1Shuma;
        private double shumaNdyshimeveShuma;
        private double buxheti2Shuma;
        private double gjendjaShuma;
        private double TVSHShuma;
        private double DiferencaShuma;
        private double RealizimiShuma;
        public Rap_BuxhetimiSipasUrdherPagesave(ParametraRaporti param, XtraReport report):this(param.Ci,param.IdNdermarrje, param.IdViti, report) { }
        public Rap_BuxhetimiSipasUrdherPagesave(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            parameter1.Value = raport.Parameters[6].Value;
            parameter2.Value = raport.Parameters["filterMuaji"].Value;
            hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);
            EmrateLabelave(ci);
            idNderm = idNdermarrje;

        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("lblRaportiBuxhetimitSipasUP", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel7.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel1.Text = rm.GetString("labelRaportiNr", ci);
            xrLabel107.Text = rm.GetString("lblBuxhetiMiratuar", ci);
            xrLabel19.Text = rm.GetString("labelRaportiEmertimi", ci);
            xrLabel20.Text = rm.GetString("lblShumaENdryshimeve", ci);
            xrLabel21.Text = rm.GetString("lblBuxhetiProgresiv", ci);
            xrLabel22.Text = rm.GetString("lblLikujduarGjateVitit", ci);
            xrLabel16.Text = rm.GetString("lblDiferenca", ci);
            xrLabel2.Text = rm.GetString("lblRealizimiNePerqindje", ci);
        }
        private Hashtable skippedDetailBands;
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

        public void UpdateDetail(string detailID)
        {
            if (SkippedDetailBands.Contains(detailID))
                SkippedDetailBands[detailID] = !Convert.ToBoolean(SkippedDetailBands[detailID]);
            else
                SkippedDetailBands.Add(detailID, false);

        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            if (GetCurrentColumnValue("IDPRINDIFILLESTAR") == System.DBNull.Value || GetCurrentColumnValue("IDPRINDIFILLESTAR") == null)
                return;
            if (Convert.ToInt32(GetCurrentColumnValue("NIVELKATEGORIE")) == 1)
                e.Cancel = true;
            Detail.Visible = true;
            object objPrindi = GetCurrentColumnValue("IDPRINDIFILLESTAR");
            if (objPrindi != System.DBNull.Value && objPrindi != null)
            {
                string prindi = objPrindi.ToString();
                if (!SkippedDetailBands.Contains(prindi))
                {
                    SkippedDetailBands.Add(prindi, !hapurgjitha);
                }
            }

        }
    
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("NIVELKATEGORIE") != null && GetCurrentColumnValue("NIVELKATEGORIE").ToString() != null && GetCurrentColumnValue("NIVELKATEGORIE").ToString() != "")
            {
            
                if (GetCurrentColumnValue("PRINDI_MBI_BIJEN") != null && (Convert.ToInt32(GetCurrentColumnValue("NIVELKATEGORIE")) > 1))
                {

                    string IDPRIND = GetCurrentColumnValue("PRINDI_MBI_BIJEN").ToString();
                    if (SkippedDetailBands.ContainsKey(IDPRIND))
                        e.Cancel = Convert.ToBoolean(SkippedDetailBands[IDPRIND]);
                    else
                    {
                        e.Cancel = !hapurgjitha;
                        SkippedDetailBands.Add(IDPRIND, !hapurgjitha);
                    }
                }
            }
        }

        private void xrTableCell19_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell label = sender as XRTableCell;
            if (GetCurrentColumnValue("IDPRINDIFILLESTAR") != System.DBNull.Value && GetCurrentColumnValue("IDPRINDIFILLESTAR") != null)
            {
                string nrdok = GetCurrentColumnValue("PRINDI_MBI_BIJEN").ToString();
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + nrdok + ";Detail;Rap_BuxhetimiSipasUrdherPagesave')";
                if (!SkippedDetailBands.ContainsKey(nrdok))
                    if (hapurgjitha || (GetCurrentColumnValue("GJETHE").ToString() == "Jo"))
                        label.Text = "-";
                    else if ((GetCurrentColumnValue("GJETHE").ToString() == "Po"))
                        label.Text = " ";
                    else
                        label.Text = "+";
                else if ((bool)SkippedDetailBands[nrdok] == false)
                
                label.Text = "-";
          
            else
            {
              Detail.Visible = false;
                label.Text = "+";
            }
            }
            else
            {
                label.Text = "";
            }
        }


        private void xrTableCell20_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            if (GetCurrentColumnValue("PRINDI_MBI_BIJEN") != System.DBNull.Value && GetCurrentColumnValue("PRINDI_MBI_BIJEN") != null)
            {
                string nrdok = GetCurrentColumnValue("ID").ToString();
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + nrdok + ";Detail;Rap_BuxhetimiSipasUrdherPagesave')";
                if (!SkippedDetailBands.ContainsKey(nrdok))
                    if (hapurgjitha && (GetCurrentColumnValue("GJETHE").ToString() == "Jo"))
                        label.Text = "-";
                    else if (hapurgjitha && (GetCurrentColumnValue("GJETHE").ToString() == "Po"))
                        label.Text = "";
                    else if (GetCurrentColumnValue("GJETHE").ToString() == "Po")
                        label.Text = "";
                    else label.Text = "+";
                else if ((bool)SkippedDetailBands[nrdok] == false)
                {
                    if ((GetCurrentColumnValue("GJETHE").ToString() == "Po") && (Convert.ToInt32(GetCurrentColumnValue("NIVELKATEGORIE")) == 1))
                    { label.Text = ""; return; }
                    label.Text = "-";

                }
                else
                {
                    if ((GetCurrentColumnValue("GJETHE").ToString() == "Po") && (Convert.ToInt32(GetCurrentColumnValue("NIVELKATEGORIE")) == 1))
                    {
                        label.Text = ""; return;
                    }
                    label.Text = "+";
                }
            }
            else
            {
                label.Text = "";
            }
        }
    
        private void xrLabel31_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("NIVELKATEGORIE") != null && GetCurrentColumnValue("NIVELKATEGORIE") != DBNull.Value && Convert.ToInt32(GetCurrentColumnValue("NIVELKATEGORIE")) == 1)
            {

                buxheti1Shuma += Convert.ToDouble(GetCurrentColumnValue("Buxheti1"));
                shumaNdyshimeveShuma += Convert.ToDouble(GetCurrentColumnValue("ShumaNdryshimeve"));
                buxheti2Shuma += Convert.ToDouble(GetCurrentColumnValue("Buxheti2"));
                gjendjaShuma += Convert.ToDouble(GetCurrentColumnValue("gjendja"));
                TVSHShuma += Convert.ToDouble(GetCurrentColumnValue("TVSH"));
                DiferencaShuma += Convert.ToDouble(GetCurrentColumnValue("Diferenca"));
                RealizimiShuma += Convert.ToDouble(GetCurrentColumnValue("RealizimiNePerqindje"));
            }
        }
        private string ktheRezultat(double shume)
        {
            double sum = Math.Round(shume, 2);
            if (sum < 0)
                return "(" + Math.Abs(sum) + ")";
            else
                return sum.ToString();

        }

        private void xrLabel31_SummaryReset(object sender, EventArgs e)
        {
            buxheti1Shuma = 0.0;
            shumaNdyshimeveShuma = 0.0;
            buxheti2Shuma = 0.0;
            gjendjaShuma = 0.0;
            TVSHShuma = 0.0;
            DiferencaShuma = 0.0;
            RealizimiShuma = 0.0;
        }
        private void xrLabel31_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = ktheRezultat(buxheti1Shuma);
            e.Handled = true;
        }
        private void xrLabel49_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = ktheRezultat(shumaNdyshimeveShuma);
            e.Handled = true;
        }

        private void xrLabel47_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = ktheRezultat(buxheti2Shuma);
            e.Handled = true;
        }

        private void xrLabel46_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = ktheRezultat(gjendjaShuma);
            e.Handled = true;
        }

        private void xrLabel9_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = ktheRezultat(TVSHShuma);
            e.Handled = true;
        }

        private void xrLabel5_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = ktheRezultat(DiferencaShuma);
            e.Handled = true;
        }

        private void xrLabel4_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = ktheRezultat(RealizimiShuma);
            e.Handled = true;
        }
    }

}
