using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.B_Buxheti.Raporte
{
    public partial class RapRialokimidheEkzekutimiBuxhetitQeveritar : DevExpress.XtraReports.UI.XtraReport, IUpdateDetail
    {
        public RapRialokimidheEkzekutimiBuxhetitQeveritar()
        {
            InitializeComponent();  
        }
        public RapRialokimidheEkzekutimiBuxhetitQeveritar(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, report)
        {

        }
        bool hapurgjitha = false;
        public RapRialokimidheEkzekutimiBuxhetitQeveritar(CultureInfo ci, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel2.Text = rm.GetString("RaportiRialokimidheEkzekutimiBuxhetitQeveritar", ci);
            hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);
            //xrLabel2.Text = rm.GetString("VeprimeteKlientitperdatatmetefundit", ci);

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
            
                if (GetCurrentColumnValue("NIVELI") != null && GetCurrentColumnValue("NIVELI").ToString() != null && GetCurrentColumnValue("NIVELI").ToString() != "")
                    if (Convert.ToInt32(GetCurrentColumnValue("NIVELI")) != 1)
                        e.Cancel = true;
            
                if (GetCurrentColumnValue("MEMA") == System.DBNull.Value || GetCurrentColumnValue("MEMA") == null)
                return;
            Detail.Visible = true;
            object objPrindi = GetCurrentColumnValue("MEMA");
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
            if (GetCurrentColumnValue("NIVELI") != null && GetCurrentColumnValue("NIVELI").ToString() != null && GetCurrentColumnValue("NIVELI").ToString() != "")
            {
                if (Convert.ToInt32(GetCurrentColumnValue("NIVELI")) == 1)
                    e.Cancel = true;

                if (GetCurrentColumnValue("PRINDI_MBI_BIJEN") != null && (Convert.ToInt32(GetCurrentColumnValue("NIVELI")) > 1))
                {
                    string IDQKPRIND = GetCurrentColumnValue("PRINDI_MBI_BIJEN").ToString();
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
        //shtuar befori per plusin e pare
        private void xrTableCell23_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell label = sender as XRTableCell;
            if (GetCurrentColumnValue("MEMA") != System.DBNull.Value && GetCurrentColumnValue("MEMA") != null)
            {
                string nrdok = GetCurrentColumnValue("PRINDI_MBI_BIJEN").ToString();
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + nrdok + ";Detail;RialokimidheEkzekutimiBuxhetitQeveritar')";
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
        //+ ne detail
        private void xrTableCell27_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRTableCell label = sender as XRTableCell;
            if (GetCurrentColumnValue("PRINDI_MBI_BIJEN") != System.DBNull.Value && GetCurrentColumnValue("PRINDI_MBI_BIJEN") != null)
            {
                string nrdok = GetCurrentColumnValue("IDBIJA").ToString();
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + nrdok + ";Detail;RialokimidheEkzekutimiBuxhetitQeveritar')";
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

    }
}
