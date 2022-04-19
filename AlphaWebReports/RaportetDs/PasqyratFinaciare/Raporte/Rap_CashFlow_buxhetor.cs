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
    public partial class Rap_CashFlow_buxhetor : DevExpress.XtraReports.UI.XtraReport, IUpdateDetailKPF
    {
        public Rap_CashFlow_buxhetor() { InitializeComponent(); }
        bool hapurgjitha = false;
        int count = 0;
        int nivkodi = 0;

        string[] romake = { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII", "XIII", "XIV", "XV", "XVI", "XVII", "XVIII", "XIX", "XX" };
        string[] romakevogel = { "", "i", "ii", "iii", "iv", "v", "vi", "vii", "viii", "ix", "x", "xi", "xii", "xiii", "xiv", "xv", "xvi", "xvii", "xviii", "xix", "xx" };
        string[] shkronje = { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T" };
        string[] shkronjevogel = { "", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t" };


        CultureInfo ci;
        
        public Rap_CashFlow_buxhetor( ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_CashFlow_buxhetor(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            this.ci = ci;
            MonNder.Value = raport.Parameters[4].Value;
            xrLabel70.Text = raport.Parameters[4].Description;
            xrLabel52.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel55.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            azhornimLabel.Text = raport.Parameters[3].Description;
            Azhornim.Value = raport.Parameters[3].Value;
            xrLabel105.Text = raport.Parameters[5].Description;
            parameter3.Value = raport.Parameters[5].Value;
            hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);

            EmrateLabelave(ci);
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

            this.CreateDocument();
        }
        public void UpdateDetail(string detailID)
        {
            if (SkippedDetailBands.Contains(detailID))
                SkippedDetailBands[detailID] = !Convert.ToBoolean(SkippedDetailBands[detailID]);
            else
                SkippedDetailBands.Add(detailID, false);

            this.CreateDocument();
        }
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            if (GetCurrentColumnValue("kodikpf") == null && GetCurrentColumnValue("PERSHKRIMIZERIT") == null)
                return;
            string catid = GetCurrentColumnValue("kodikpf").ToString();
            string catid2 = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
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

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            bool pse = hapurgjitha;
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") == null)
                return;
            string catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();

            if (SkippedDetailKPF.Contains(catid))
                e.Cancel = Convert.ToBoolean(SkippedDetailKPF[catid]);
            else
            {
                e.Cancel = !hapurgjitha; SkippedDetailKPF.Add(catid, !hapurgjitha);
            }
        }
        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image  = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
            count = 0;
            nivkodi = 0;
            
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel85.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel12.Text = rm.GetString("RaportCashFlowTitulli", ci);
        }


        private void xrTableCell2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                                  System.Reflection.Assembly.Load("App_GlobalResources"));
            if (xrTableCell2.Text == "Fluksi i parave nga veprimtarite e shfrytezimit")
                xrTableCell2.Text = "VEPRIMTARITE E SHFRYTEZIMIT";
            else if (xrTableCell2.Text == "Fluksi i parave nga veprimtarite investuese")
                xrTableCell2.Text = "VEPRIMTARITE E INVESTIMEVE";
            else xrTableCell2.Text = "TRANSFERTA E TE TJERA";
        }


        private void xrTableCell6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            //string catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            string catid = "";
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null)
                catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji") == null || GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
                label.Text = "";
            else
            {
                label.NavigateUrl = "javascript:window.parent.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;cashFlow')";
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

        private void xrTableCell12_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            string catid = "";
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null)
                catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji") == null || GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ2") != null && GetCurrentColumnValue("SHFAQBIJ2").ToString() == "False"))
                label.Text = "";
            else
            {
                label.NavigateUrl = "javascript:window.parent.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;cashFlow')";
                if (!SkippedDetailKPF.Contains(catid))
                    if (hapurgjitha)
                    {
                        label.Text = "-";
           
                    }
                    else label.Text = "+";
                else if ((bool)SkippedDetailKPF[catid] == false)
                    label.Text = "-";
                else
                    label.Text = "+";
            }
        }


        private void xrTableCell18_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            string catid = "";
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null) catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji") == null || GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ3") != null && GetCurrentColumnValue("SHFAQBIJ3").ToString() == "False"))
                label.Text = "";
            else
            {
                label.NavigateUrl = "javascript:window.parent.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;cashFlow')";
                if (!SkippedDetailKPF.Contains(catid))
                    if (hapurgjitha)
                    {
                        label.Text = "-";
                  
                    }
                    else label.Text = "+";
                else if ((bool)SkippedDetailKPF[catid] == false)
                    label.Text = "-";
                else
                    label.Text = "+";
            }
        }



        private void xrTableCell24_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            string catid = "";
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null) catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji") == null || GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ4") != null && GetCurrentColumnValue("SHFAQBIJ4").ToString() == "False"))
                label.Text = "";
            else
            {
                label.NavigateUrl = "javascript:window.parent.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;cashFlow')";
                if (!SkippedDetailKPF.Contains(catid))
                    if (hapurgjitha)
                    {
                        label.Text = "-";
           
                    }
                    else label.Text = "+";
                else if ((bool)SkippedDetailKPF[catid] == false)
                    label.Text = "-";
                else
                    label.Text = "+";
            }
        }


        private void xrTableCell30_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            string catid = "";
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null)
                catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji") == null || GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ5") != null && GetCurrentColumnValue("SHFAQBIJ5").ToString() == "False"))
                label.Text = "";
            else
            {
                label.NavigateUrl = "javascript:window.parent.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;cashFlow')";
                if (!SkippedDetailKPF.Contains(catid))
                    if (hapurgjitha)
                    {
                        label.Text = "-";
       
                    }
                    else label.Text = "+";
                else if ((bool)SkippedDetailKPF[catid] == false)
                    label.Text = "-";
                else
                    label.Text = "+";
            }
        }


        private void xrTableCell36_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KODIKPF") != null)
            {
                XRLabel label = sender as XRLabel;
                string catid = GetCurrentColumnValue("KODIKPF").ToString();
                if (GetCurrentColumnValue("NRLLOGARI").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJLLOG") != null && GetCurrentColumnValue("SHFAQBIJLLOG").ToString() == "False"))
                    label.Text = "";
                else
                {
                    label.NavigateUrl = "javascript:window.parent.ASPxCallbackPanel1.PerformCallback('" + catid + ";Detail;cashFlow')";
                    if (!SkippedDetailBands.Contains(catid))
                        if (hapurgjitha)
                        {
                            label.Text = "-";
                       
                        }
                        else label.Text = "+";
                    else if ((bool)SkippedDetailBands[catid] == false)
                        label.Text = "-";
                    else
                        label.Text = "+";
                }
            }
        }



        private void xrTableCell27_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            count++;
            xrTableCell27.Text = count.ToString();
        }

        private void xrTableCell27_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = count;
            e.Handled = true;
        }

        private void xrTableCell27_SummaryReset(object sender, EventArgs e)
        {
        }

        private void xrTableCell3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            count++;
            xrTableCell3.Text = count.ToString();
        }

        private void xrTableCell3_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = count;
            e.Handled = true;
        }

        private void xrTableCell3_SummaryReset(object sender, EventArgs e)
        {
        }

        private void xrTableCell8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            count++;
            xrTableCell8.Text = count.ToString();
        }

        private void xrTableCell8_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = count;
            e.Handled = true;
        }

        private void xrTableCell8_SummaryReset(object sender, EventArgs e)
        {
        }

        private void xrTableCell15_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            count++;
            xrTableCell15.Text = count.ToString();
        }

        private void xrTableCell15_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = count;
            e.Handled = true;
        }

        private void xrTableCell15_SummaryReset(object sender, EventArgs e)
        {
        }


        private void xrTableCell21_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            count++;
            xrTableCell21.Text = count.ToString();
        }

        private void xrTableCell21_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = count;
            e.Handled = true;
        }


        
        private void xrTableCell33_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            count++;
            xrTableCell33.Text = count.ToString();
        }

        private void xrTableCell33_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = count;
            e.Handled = true;
        }

   

        private void xrTableCell54_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            count++;
            xrTableCell54.Text = count.ToString();
        }

        private void xrTableCell54_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = count;
            e.Handled = true;
        }

    


        private void xrTableCell55_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            count++;
            xrTableCell55.Text = count.ToString();
        }

        private void xrTableCell55_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = count;
            e.Handled = true;
        }

    

        private void xrTableCell64_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            count++;
            xrTableCell64.Text = count.ToString();
        }

        private void xrTableCell64_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = count;
            e.Handled = true;
        }

 
        private void xrTableCell74_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            count++;
            xrTableCell74.Text = count.ToString();
        }

        private void xrTableCell74_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = count;
            e.Handled = true;
        }

       
        private void xrTableCell69_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            count++;
            xrTableCell69.Text = count.ToString();
        }

        private void xrTableCell69_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = count;
            e.Handled = true;
        }

      

        private void xrTableCell85_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            count++;
            xrTableCell85.Text = count.ToString();
        }

        private void xrTableCell85_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = count;
            e.Handled = true;
        }

   

        private void xrTableCell95_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            count++;
            xrTableCell95.Text = count.ToString();
        }

        private void xrTableCell95_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = count;
            e.Handled = true;
        }



        private void xrTableCell87_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            count++;
            xrTableCell87.Text = count.ToString();
        }

        private void xrTableCell87_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = count;
            e.Handled = true;
        }


        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void xrTableCell100_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            nivkodi++;
            xrTableCell100.Text = romake[nivkodi];
        }

        private void xrTableCell100_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            e.Result = nivkodi;
            e.Handled = true;

        }

        private void xrTableCell100_SummaryReset(object sender, System.EventArgs e)
        {
           // nivkodi = 0;

        }

        private void xrTableCell4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            nivkodi++;
            xrTableCell4 .Text= romake[nivkodi];

        }

        private void xrTableCell4_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = nivkodi;
            e.Handled = true;
        }

        private void xrTableCell5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            nivkodi++;
            xrTableCell5.Text = romake[nivkodi];
        }

        private void xrTableCell5_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = nivkodi;
            e.Handled = true;
        }

        private void xrTableCell39_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            nivkodi++;
            xrTableCell39.Text = romake[nivkodi];
        }

        private void xrTableCell39_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = nivkodi;
            e.Handled = true;
        }
    }
}
