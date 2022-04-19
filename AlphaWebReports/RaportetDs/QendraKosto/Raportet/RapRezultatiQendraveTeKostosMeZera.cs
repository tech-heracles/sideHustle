using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Collections.Generic;
using AlphaWebReports.Common;


namespace AlphaWebReports.RaportetDs.QendraKosto.Raportet
{
    public partial class RapRezultatiQendraveTeKostosMeZera : DevExpress.XtraReports.UI.XtraReport, IUpdateDetail
    {
        public RapRezultatiQendraveTeKostosMeZera()
        {
            InitializeComponent();
        }

        public RapRezultatiQendraveTeKostosMeZera(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, report)
        {

        }
        bool hapurgjitha = false;
        public RapRezultatiQendraveTeKostosMeZera(CultureInfo ci, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);

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


        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("NIVELI")!=null && GetCurrentColumnValue("NIVELI").ToString() != null && GetCurrentColumnValue("NIVELI").ToString() != "")
            {
                if (Convert.ToInt32(GetCurrentColumnValue("NIVELI")) == 1)
                    e.Cancel = true;

                if (GetCurrentColumnValue("IDQKPRIND") != null && (Convert.ToInt32(GetCurrentColumnValue("NIVELI")) > 1))
                {
                    string IDQKPRIND = GetCurrentColumnValue("IDQKPRIND").ToString();
                    if (SkippedDetailBands.ContainsKey(IDQKPRIND))
                        e.Cancel = Convert.ToBoolean(SkippedDetailBands[IDQKPRIND]);
                    else

                    {
                        e.Cancel = !hapurgjitha;
                        SkippedDetailBands.Add(IDQKPRIND, !hapurgjitha);
                    }

                }

            }


        }






        private void xrTableCell10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            XRLabel label = sender as XRLabel;
            if (GetCurrentColumnValue("IDQKPRIND") != System.DBNull.Value && GetCurrentColumnValue("IDQKPRIND") != null)
            {
                string nrdok = GetCurrentColumnValue("IDBIJE").ToString();

                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + nrdok + ";Detail;RezultatiQendraveTeKostosMeZera')";
                if (!SkippedDetailBands.ContainsKey(nrdok))
                    if (hapurgjitha && (GetCurrentColumnValue("GJETHE").ToString() == "JO"))
                        label.Text = "-";
                    else if (hapurgjitha && (GetCurrentColumnValue("GJETHE").ToString() == "PO"))
                        label.Text = "";
                    else if (GetCurrentColumnValue("GJETHE").ToString() == "PO")
                        label.Text = "";
                    else label.Text = "+";
                else if ((bool)SkippedDetailBands[nrdok] == false)
                    label.Text = "-";
                else
                    label.Text = "+";
            }
            else
            {
                label.Text = "";
            }
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("IDQENDRAKOSTO") == System.DBNull.Value || GetCurrentColumnValue("IDQENDRAKOSTO") == null)
                return;
            Detail.Visible = true;
            object objPrindi = GetCurrentColumnValue("IDQENDRAKOSTO");
            if (objPrindi != System.DBNull.Value && objPrindi != null)
            {
                string prindi = objPrindi.ToString();
                if (!SkippedDetailBands.Contains(prindi))
                {
                    SkippedDetailBands.Add(prindi, !hapurgjitha);
                }
            }

        }

        private void xrTableCell12_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            if (GetCurrentColumnValue("IDQENDRAKOSTO") != System.DBNull.Value && GetCurrentColumnValue("IDQENDRAKOSTO") != null)
            {
                string nrdok = GetCurrentColumnValue("IDQKPRIND").ToString();
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + nrdok + ";Detail;RezultatiQendraveTeKostosMeZera')";
                if (!SkippedDetailBands.ContainsKey(nrdok))
                    if (hapurgjitha || (GetCurrentColumnValue("GJETHE").ToString() == "JO"))
                        label.Text = "-";
                    else if ((GetCurrentColumnValue("GJETHE").ToString() == "PO"))
                    { label.Text = " "; }
                    else label.Text = "+";
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

        private void xrLabel4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           //string kod = xrTableCell16.Text.ToString();
           //XRLabel cell = sender as XRLabel;
           // cell.Text = kod;
        }

        private void xrLabel4_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
          
            XRLabel cell = sender as XRLabel;
            e.Result = kod; e.Handled = true;
        }
        

        private double shuma;
        private double shumaMonQk;
        private bool monedhaNjejte = true;
        private string monedha = null;
        double[] totali = new double[] {0, 0, 0, 0, 0, 0};
        bool printed = false; string kod;


        private void xrLabel5_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
           // if (!printed)
                e.Result = ktheRezultat(shuma);
           // else
               // e.Result = (sender as XRLabel).Text;
            e.Handled = true;
        }

        private void xrLabel5_SummaryReset(object sender, EventArgs e)
        {
            shuma = 0;
            shumaMonQk = 0;
        }

        private void xrLabel5_SummaryRowChanged(object sender, EventArgs e)
        {
            if(GetCurrentColumnValue("NIVELI")!=null && GetCurrentColumnValue("NIVELI") != DBNull.Value && Convert.ToInt32(GetCurrentColumnValue("NIVELI")) ==1) //&& !printed)
            {
                kod = GetCurrentColumnValue("kodi").ToString();
                shuma = Convert.ToDouble(GetCurrentColumnValue("teArdhuraMonBaze")) - Convert.ToDouble(GetCurrentColumnValue("shpenzimeMonBaze"));
                shumaMonQk = Convert.ToDouble(GetCurrentColumnValue("teArdhuraMonQk")) - Convert.ToDouble(GetCurrentColumnValue("shpenzimeMonQk"));
                totali[0] += Convert.ToDouble(GetCurrentColumnValue("teArdhuraMonBaze"));
                totali[1] += Convert.ToDouble(GetCurrentColumnValue("shpenzimeMonBaze"));
                totali[2] += Convert.ToDouble(GetCurrentColumnValue("teArdhuraMonQk"));
                totali[3] += Convert.ToDouble(GetCurrentColumnValue("shpenzimeMonQk"));
                totali[4] += shuma;
                totali[5] += shumaMonQk;
            }
            
        }

        private void xrTableCell33_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            joMonedhaNjejta(sender);
        }

        private void xrTableCell34_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            joMonedhaNjejta(sender);
        }

        private void xrLabel6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            joMonedhaNjejta(sender);
        }
        private void joMonedhaNjejta(object sender)
        {
            XRLabel cell = sender as XRLabel;
            if (!monedhaNjejte)
                cell.Text = "";
        }

        private void xrTableCell47_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = ktheRezultat(totali[0]);
            e.Handled = true;
        }

        private void xrTableCell48_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = ktheRezultat(totali[1]);
            e.Handled = true;
        }

        private void xrTableCell33_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (monedhaNjejte)
                e.Result = ktheRezultat(totali[2]);
            else
                e.Result = "";
            e.Handled = true;
        }
        private void xrTableCell34_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (monedhaNjejte)
                e.Result = ktheRezultat(totali[3]);
            else
                e.Result = "";
            e.Handled = true;
        }
        private void xrLabel2_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = ktheRezultat(totali[4]);
            printed = true;
            e.Handled = true;
        }

        private void xrLabel6_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (monedhaNjejte)
                e.Result = ktheRezultat(totali[5]);
            else
                e.Result = "";
            e.Handled = true;
        }

        private void xrLabel7_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = ktheRezultat(shumaMonQk);
            e.Handled = true;
        }

        private string ktheRezultat(double shume)
        {
            double sum = Math.Round(shume, 2);
            if (sum < 0)
                return "(" + Math.Abs(sum) + ")";
            else
                return sum.ToString();

        }

        private void xrTableCell32_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (monedhaNjejte && GetCurrentColumnValue("MONEDHAKOD") != DBNull.Value && GetCurrentColumnValue("MONEDHAKOD")!=null)
                if (monedha == null)
                    monedha = GetCurrentColumnValue("MONEDHAKOD").ToString();
                else if (GetCurrentColumnValue("MONEDHAKOD").ToString() != monedha)
                    monedhaNjejte = false;
        }

        private void xrTableCell47_SummaryReset(object sender, EventArgs e)
        {
            totali = new double[] { 0, 0, 0, 0, 0, 0 };
        }

      
    }

    }

   
      
