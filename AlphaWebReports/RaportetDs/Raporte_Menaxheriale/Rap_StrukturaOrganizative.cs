using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using AlphaWebReports.Common;


namespace AlphaWebReports.RaportetDs.Raporte_Menaxheriale
{
    public partial class Rap_StrukturaOrganizative : DevExpress.XtraReports.UI.XtraReport, IUpdateDetail
    {
        public Rap_StrukturaOrganizative()
        {
            InitializeComponent();  
        }
        public Rap_StrukturaOrganizative(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, report)
        {

        }
        bool hapurgjitha = false;
        public Rap_StrukturaOrganizative(CultureInfo ci, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel2.Text = rm.GetString("RapStrukturaOrganizative", ci);
            hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);
            xrTableCell5.Text = rm.GetString("labelKodi", ci);
            xrTableCell6.Text = rm.GetString("labelRaportPershkrimi", ci);
            xrTableCell7.Text = rm.GetString("labelRaportEmail", ci);
            xrTableCell8.Text = rm.GetString("lblNivel", ci);

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

        private void GroupHeader2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("NIVELSTRUKTURE") != null && GetCurrentColumnValue("NIVELSTRUKTURE").ToString() != null && GetCurrentColumnValue("NIVELSTRUKTURE").ToString() != "" && Convert.ToInt32(GetCurrentColumnValue("NIVELSTRUKTURE")) != 1)
                    e.Cancel = true;
                       
            Detail.Visible = true;
            object objPrindi = GetCurrentColumnValue("PrindiNIVEL1");
            if (objPrindi != System.DBNull.Value && objPrindi != null)
            {
                string prindi = objPrindi.ToString();
                if (!SkippedDetailBands.Contains(prindi))
                    SkippedDetailBands.Add(prindi, !hapurgjitha);
            }
            xrTable4.BackColor = Color.FromArgb(91, 155, 213);
        }
        
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("NIVELSTRUKTURE") != null && GetCurrentColumnValue("NIVELSTRUKTURE").ToString() != null && GetCurrentColumnValue("NIVELSTRUKTURE").ToString() != "" && Convert.ToInt32(GetCurrentColumnValue("NIVELSTRUKTURE")) == 1)
                e.Cancel = true;

            if (GetCurrentColumnValue("PrindiNIVEL2") != null && (Convert.ToInt32(GetCurrentColumnValue("NIVELSTRUKTURE")) > 1))
            {
                if (Convert.ToInt32(GetCurrentColumnValue("NIVELSTRUKTURE")) == 2)
                {
                    xrTable2.BackColor = Color.FromArgb(189, 215, 238);
                    xrTable2.ForeColor = Color.Black;
                }
                else
                {
                    xrTable2.BackColor = Color.FromArgb(252, 228, 214);
                    xrTable2.ForeColor = Color.Black;
                }
                string ndermPrindi = GetCurrentColumnValue("PrindiNIVEL2").ToString();
                if (SkippedDetailBands.ContainsKey(ndermPrindi))
                    e.Cancel = Convert.ToBoolean(SkippedDetailBands[ndermPrindi]);
                else
                {
                    e.Cancel = !hapurgjitha;
                    SkippedDetailBands.Add(ndermPrindi, !hapurgjitha);
                }
            }
        }

        private void xrTableCell9_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            if (GetCurrentColumnValue("PrindiNIVEL1") != System.DBNull.Value && GetCurrentColumnValue("PrindiNIVEL1") != null)
            {
                string nderm = GetCurrentColumnValue("PrindiNIVEL2").ToString();
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + nderm + ";Detail;StrukturaOrganizative')";
                if (!SkippedDetailBands.ContainsKey(nderm))
                    if (hapurgjitha || (GetCurrentColumnValue("KaBija").ToString() == "PO"))
                        label.Text = "-";
                    else if ((GetCurrentColumnValue("KaBija").ToString() == "JO"))
                    { label.Text = " "; }
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
                
        private void xrTableCell27_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            if (GetCurrentColumnValue("PrindiNIVEL2") != System.DBNull.Value && GetCurrentColumnValue("PrindiNIVEL2") != null)
            {
                string nderm = GetCurrentColumnValue("IDNDERMARRJE").ToString();

                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + nderm + ";Detail;StrukturaOrganizative')";
                if (!SkippedDetailBands.ContainsKey(nderm))
                    if (hapurgjitha && (GetCurrentColumnValue("KaBija").ToString() == "PO"))
                        label.Text = "-";
                    else if (hapurgjitha && (GetCurrentColumnValue("KaBija").ToString() == "JO"))
                        label.Text = "";
                    else if (GetCurrentColumnValue("KaBija").ToString() == "JO")
                        label.Text = "";
                    else label.Text = "+";
                else if ((bool)SkippedDetailBands[nderm] == false)
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
