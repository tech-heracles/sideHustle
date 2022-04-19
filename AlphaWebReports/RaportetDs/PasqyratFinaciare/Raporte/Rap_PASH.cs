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
    public partial class Rap_PASH : DevExpress.XtraReports.UI.XtraReport, IUpdateDetailKPF
    {
        int formatNumri = 0;
        bool hapurgjitha = false;
        
        private int rritshuma = 0;
        public Rap_PASH()
        {
            InitializeComponent();
        }
        public Rap_PASH(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_PASH(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            MonNder.Value = raport.Parameters[4].Value;
            xrLabel52.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel55.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            azhornimLabel.Text = raport.Parameters[3].Description;
            Azhornim.Value = raport.Parameters[3].Value;
            xrLabel18.Text = raport.Parameters[5].Description;
            parameter3.Value = raport.Parameters[5].Value;
            hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);
            EmrateLabelave(ci);
            formatNumri = Convert.ToInt32(raport.Parameters["filterFormatNumri"].Value);
            parameterFormatNumri.Value = formatNumri;
            caktoFormatinENumrave();
        }
        
    
    private void caktoFormatinENumrave()
    {
            xrTableCell43.DataBindings[0].FormatString = xrTableCell44.DataBindings[0].FormatString = xrTableCell40.DataBindings[0].FormatString = xrTableCell41.DataBindings[0].FormatString = xrTableCell5.DataBindings[0].FormatString = xrTableCell6.DataBindings[0].FormatString =
            xrTableCell11.DataBindings[0].FormatString = xrTableCell12.DataBindings[0].FormatString = xrTableCell17.DataBindings[0].FormatString = xrTableCell18.DataBindings[0].FormatString = xrTableCell23.DataBindings[0].FormatString
            = xrTableCell24.DataBindings[0].FormatString = xrTableCell30.DataBindings[0].FormatString = xrTableCell31.DataBindings[0].FormatString = xrTableCell50.DataBindings[0].FormatString = xrTableCell51.DataBindings[0].FormatString = xrTableCell53.DataBindings[0].FormatString = xrTableCell54.DataBindings[0].FormatString =
            xrTableCell68.DataBindings[0].FormatString =
            xrTableCell69.DataBindings[0].FormatString =
             xrTableCell73.DataBindings[0].FormatString =
             xrTableCell74.DataBindings[0].FormatString =
             xrTableCell78.DataBindings[0].FormatString =
             xrTableCell79.DataBindings[0].FormatString =
             xrTableCell58.DataBindings[0].FormatString =
             xrTableCell59.DataBindings[0].FormatString = "{0:n" + formatNumri + "}";

            xrTableCell40.Summary.FormatString = xrTableCell41.Summary.FormatString = xrTableCell5.Summary.FormatString = xrTableCell6.Summary.FormatString =
            xrTableCell11.Summary.FormatString = xrTableCell12.Summary.FormatString = xrTableCell17.Summary.FormatString = xrTableCell18.Summary.FormatString = xrTableCell23.Summary.FormatString
            = xrTableCell24.Summary.FormatString = xrTableCell30.Summary.FormatString = xrTableCell31.Summary.FormatString = xrTableCell50.Summary.FormatString = xrTableCell51.Summary.FormatString = xrTableCell53.Summary.FormatString = xrTableCell54.Summary.FormatString =
            xrTableCell68.Summary.FormatString = xrTableCell69.Summary.FormatString = xrTableCell73.Summary.FormatString = xrTableCell74.Summary.FormatString =
            xrTableCell78.Summary.FormatString =
            xrTableCell79.Summary.FormatString =
            xrTableCell58.Summary.FormatString =
            xrTableCell59.Summary.FormatString = "{0:n" + formatNumri + "}";


            xrTableCell43.XlsxFormatString = xrTableCell44.XlsxFormatString = xrTableCell40.XlsxFormatString = xrTableCell41.XlsxFormatString = xrTableCell5.XlsxFormatString = xrTableCell6.XlsxFormatString =
        xrTableCell11.XlsxFormatString = xrTableCell12.XlsxFormatString = xrTableCell17.XlsxFormatString = xrTableCell18.XlsxFormatString = xrTableCell23.XlsxFormatString
        = xrTableCell24.XlsxFormatString = xrTableCell30.XlsxFormatString = xrTableCell31.XlsxFormatString = xrTableCell50.XlsxFormatString = xrTableCell51.XlsxFormatString = xrTableCell53.XlsxFormatString = xrTableCell54.XlsxFormatString =
                   xrTableCell68.XlsxFormatString =
                   xrTableCell69.XlsxFormatString =
                   xrTableCell73.XlsxFormatString =
                   xrTableCell74.XlsxFormatString =
                   xrTableCell78.XlsxFormatString =
                   xrTableCell79.XlsxFormatString = xrTableCell58.XlsxFormatString = xrTableCell59.XlsxFormatString =
                    0.ToString("N" + formatNumri);
    }
        

    string[] shkronjevogel = { "", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t" };

        int niv = 0;
       
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
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
            
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportTeArdhuratShpenzimetTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell81.Text = rm.GetString("labelRaportiEmertimi", ci);
            xrLabel7.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell82.Text = rm.GetString("labelRaportiShenime", ci);
            xrTableCell84.Text = rm.GetString("labelRaportiVitiRaportues", ci);
            xrTableCell83.Text = rm.GetString("labelRaportiVitiParaardhes", ci);
            xrTableCell56.Text = rm.GetString("labelRaportiFitimiNetoVitFinanciar", ci);
            xrTableCell80.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell61.Text = rm.GetString("labelElementeTePasqyraveTeKonsoliduara", ci);
            xrLabel70.Text = rm.GetString("filterMonedha", ci);
        }

       

        private void xrTableCell37_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") == null)
                return;
            if (xrTableCell37.Text != "" && xrTableCell38.Text == " Te pacaktuara")
            {
                xrTableCell37.Text = (Convert.ToInt16(xrTableCell37.Text) + 1).ToString();
                rritshuma = 1;
            }
        }

        private void xrTableCell36_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") == null)
                return;
            XRTableCell label = sender as XRTableCell;
            string catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
                label.Text = "";
            else
            {
                label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;ardhura')";
                label.Target = "_self";
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

        private void xrTableCell1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") == null)
                return;
            XRTableCell label = sender as XRTableCell;
            string catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ2") != null && GetCurrentColumnValue("SHFAQBIJ2").ToString() == "False"))
                label.Text = "";
            else
            {
                label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;ardhura')";
                label.Target = "_self";
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

        
        private void xrTableCell7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") == null)
                return;
            XRTableCell label = sender as XRTableCell;
            string catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ3") != null && GetCurrentColumnValue("SHFAQBIJ3").ToString() == "False"))
                label.Text = "";
            else
            {
                label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;ardhura')";
                label.Target = "_self";
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
        

        private void xrTableCell13_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") == null)
                return;
            XRTableCell label = sender as XRTableCell;
            string catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ4") != null && GetCurrentColumnValue("SHFAQBIJ4").ToString() == "False"))
                label.Text = "";
            else
            {
                label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;ardhura')";
                label.Target = "_self";
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
        

        private void xrTableCell19_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") == null)
                return;
            XRTableCell label = sender as XRTableCell;
            string catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ5") != null && GetCurrentColumnValue("SHFAQBIJ5").ToString() == "False"))
                label.Text = "";
            else
            {
                label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;ardhura')";
                label.Target = "_self";
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

        private void xrTableCell26_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("kodikpf") == null)
                return;
            XRTableCell label = sender as XRTableCell;
            string catid = GetCurrentColumnValue("kodikpf").ToString();
            if (GetCurrentColumnValue("NRLLOGARI").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJLLOG") != null && GetCurrentColumnValue("SHFAQBIJLLOG").ToString() == "False"))
            {
                label.Text = "";
            }
            else
            {
                label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";Detail;ardhura')";
                label.Target = "_self";
                if (!SkippedDetailBands.Contains(catid))
                    if (hapurgjitha)
                        label.Text = "-";
                    else label.Text = "+";
                else if ((bool)SkippedDetailBands[catid] == false)
                    label.Text = "-";
                else
                    label.Text = "+";
            }
        }
        

        private void xrTableCell55_AfterPrint(object sender, EventArgs e)
        {
            int nr = 0;
            if (xrTableCell55.Text != "")
            {
                int.TryParse(xrTableCell55.Text, out nr);

            }
                xrTableCell55.Text = (nr+ 1 + rritshuma).ToString();
        }

        private void xrTableCell55_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
           
            e.Result = xrTableCell55.Text;
            e.Handled = true;
        }

       

        private void xrTableCell60_AfterPrint(object sender, EventArgs e)
        {           int nr = 0;
           
            if (xrTableCell60.Text != "")
            {
              
                int.TryParse(xrTableCell60.Text, out nr);

            }
                xrTableCell60.Text = (nr+ 2 + rritshuma).ToString();
        }

        private void xrTableCell60_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = xrTableCell60.Text;
            e.Handled = true;
           
        }

      
    }
}
