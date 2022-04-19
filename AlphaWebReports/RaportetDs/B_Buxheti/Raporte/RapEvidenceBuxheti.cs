using System;
using System.Collections;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.B_Buxheti.Raporte
{
    public partial class RapEvidenceBuxheti : XtraReport, IUpdateDetail
    {
        public RapEvidenceBuxheti()
        {
            InitializeComponent();
        }

        public RapEvidenceBuxheti(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, report)
        {

        }
        bool hapurgjitha = false;
        public RapEvidenceBuxheti(CultureInfo ci, XtraReport raport)
        {
            InitializeComponent();
            hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);
            EmertimetELabelave(ci);
        }
        private void EmertimetELabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                         System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel13.Text = rm.GetString("lblRapEvidencaERealizimitTeBuxhetit", ci);
            xrTableCell29.Text = rm.GetString("lblRapBuxhetimiAnaliza", ci);
            xrTableCell30.Text = rm.GetString("labelEmertimi", ci);
            xrTableCell22.Text = rm.GetString("lblRapBuxhetiIKontraktuar", ci);//buxheti i kontraktuar
            xrTableCell26.Text = rm.GetString("lblRapBuxhetiIKontraktuar", ci);//buxheti i kontraktuar

            xrTableCell14.Text = rm.GetString("lblRapBuxhetimiPlan", ci);//plan 12 10 9
            xrTableCell12.Text = rm.GetString("lblRapBuxhetimiPlan", ci);//plan 12 10 9
            xrTableCell10.Text = rm.GetString("lblRapBuxhetimiPlan", ci);//plan 12 10 9
            xrTableCell9.Text = rm.GetString("lblRapBuxhetimiPlan", ci);//plan 12 10 9

            xrTableCell13.Text = rm.GetString("lblRapBuxhetimiFakt", ci);//fakt
            xrTableCell11.Text = rm.GetString("lblRapBuxhetimiFakt", ci);//fakt
            xrTableCell8.Text = rm.GetString("lblRapBuxhetimiFakt", ci);//fakt
            xrTableCell17.Text = rm.GetString("lblRapBuxhetimiFakt", ci);//fakt

            xrTableCell24.Text = rm.GetString("lblRapBuxhetimiBurimeTeTjera", ci);//Burime te tjera
            xrTableCell28.Text = rm.GetString("lblRapBuxhetimiBurimeTeTjera", ci);//Burime te tjera

            xrTableCell39.Text = rm.GetString("labelRaportiProgresivi", ci);//progresivi
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
            if (GetCurrentColumnValue("NIVELI") != null && GetCurrentColumnValue("NIVELI").ToString() != null && GetCurrentColumnValue("NIVELI").ToString() != "")
            {
                if (Convert.ToInt32(GetCurrentColumnValue("NIVELI")) == 1)
                    e.Cancel = true;

                if (GetCurrentColumnValue("PRINDI_MBI_BIJEN") != null && (Convert.ToInt32(GetCurrentColumnValue("NIVELI")) > 1))
                {
                    string PRINDI_MBI_BIJEN = GetCurrentColumnValue("PRINDI_MBI_BIJEN").ToString();
                    if (SkippedDetailBands.ContainsKey(PRINDI_MBI_BIJEN))
                        e.Cancel = Convert.ToBoolean(SkippedDetailBands[PRINDI_MBI_BIJEN]);
                    else

                    {
                        e.Cancel = !hapurgjitha;
                        SkippedDetailBands.Add(PRINDI_MBI_BIJEN, !hapurgjitha);
                    }
                }
            }
        }
        
        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("IDPRINDIFILLESTAR") == DBNull.Value || GetCurrentColumnValue("IDPRINDIFILLESTAR") == null)
                return;
            Detail.Visible = true;
            object objPrindi = GetCurrentColumnValue("IDPRINDIFILLESTAR");
            if (objPrindi != DBNull.Value && objPrindi != null)
            {
                string prindi = objPrindi.ToString();
                if (!SkippedDetailBands.Contains(prindi))
                {
                    SkippedDetailBands.Add(prindi, !hapurgjitha);
                }
            }

        }

        private void xrTableCell15_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            if (GetCurrentColumnValue("IDPRINDIFILLESTAR") != DBNull.Value && GetCurrentColumnValue("IDPRINDIFILLESTAR") != null)
            {
                string nrdok = GetCurrentColumnValue("PRINDI_MBI_BIJEN").ToString();
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + nrdok + ";Detail;RapEvidenceBuxheti')";
                label.Target = "_self";
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
        
        private void xrTableCell49_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            if (GetCurrentColumnValue("PRINDI_MBI_BIJEN") != DBNull.Value && GetCurrentColumnValue("PRINDI_MBI_BIJEN") != null)
            {
                string nrdok = GetCurrentColumnValue("IDBIJA").ToString();

                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + nrdok + ";Detail;RapEvidenceBuxheti')";
                label.Target = "_self";
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

   
      
