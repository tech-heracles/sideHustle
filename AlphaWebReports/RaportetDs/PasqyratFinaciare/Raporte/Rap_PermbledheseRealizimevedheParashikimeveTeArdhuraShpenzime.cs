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
    public partial class Rap_PermbledheseRealizimevedheParashikimeveTeArdhuraShpenzime : DevExpress.XtraReports.UI.XtraReport,IUpdateDetail
    {
        bool hapurgjitha = false;
        private string date;
        public Rap_PermbledheseRealizimevedheParashikimeveTeArdhuraShpenzime()
        {
            InitializeComponent();
        }
        public Rap_PermbledheseRealizimevedheParashikimeveTeArdhuraShpenzime(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {
        }
        public Rap_PermbledheseRealizimevedheParashikimeveTeArdhuraShpenzime(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

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
            if (GetCurrentColumnValue("IDKRYESOR") != null && GetCurrentColumnValue("IDKRYESOR").ToString() != null && GetCurrentColumnValue("IDKRYESOR").ToString() != "" && Convert.ToInt32(GetCurrentColumnValue("NIVELI")) == 1 && GetCurrentColumnValue("TOTALPOSHTE").ToString() != "2")
            {
                object objPrindi = GetCurrentColumnValue("IDKRYESOR");
                if (objPrindi != System.DBNull.Value && objPrindi != null)
                {
                    string prindi = objPrindi.ToString();
                    if (!SkippedDetailBands.Contains(prindi))
                        SkippedDetailBands.Add(prindi, !hapurgjitha);
                }
                e.Cancel = true;
            }
        }


        private void xrTableCell128_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            if (GetCurrentColumnValue("IDKRYESOR") != System.DBNull.Value && GetCurrentColumnValue("IDKRYESOR") != null)
            {
                string nderm = GetCurrentColumnValue("ID").ToString();
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + nderm + ";Detail;RaportiPermbledheseRealizimevedheParashikimeveTeArdhuraShpenzime')";
                if (!SkippedDetailBands.ContainsKey(nderm))
                    if (hapurgjitha)
                        label.Text = "-";
                    else label.Text = "+";
                else if ((bool)SkippedDetailBands[nderm] == false)
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

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("NIVELI") != null && GetCurrentColumnValue("NIVELI").ToString() != null && GetCurrentColumnValue("NIVELI").ToString() != "" && Convert.ToInt32(GetCurrentColumnValue("NIVELI")) != 1)
                e.Cancel = true;

            Detail.Visible = true;
            object objPrindi = GetCurrentColumnValue("IDKRYESOR");
            if (objPrindi != System.DBNull.Value && objPrindi != null)
            {
                string prindi = objPrindi.ToString();
                if (!SkippedDetailBands.Contains(prindi))
                    SkippedDetailBands.Add(prindi, !hapurgjitha);
            }
        }
    }
}


      
   
