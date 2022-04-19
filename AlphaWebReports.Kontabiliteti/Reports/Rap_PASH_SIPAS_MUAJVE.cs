using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.Raporte
{
    public partial class Rap_PASH_SIPAS_MUAJVE : DevExpress.XtraReports.UI.XtraReport, IUpdateDetailKPF
    {
        bool hapurgjitha = false;
        private int rritshuma = 0;
        private Hashtable skippedDetailBands;
        private Hashtable skippedDetailKPF;
        private int viti;
    

        

        public Rap_PASH_SIPAS_MUAJVE()
        {
            InitializeComponent();
        }
        public Rap_PASH_SIPAS_MUAJVE(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_PASH_SIPAS_MUAJVE(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
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

            viti = Convert.ToInt16(raport.Parameters["filterDtDok"].Value.ToString().Substring(19));


            EmrateLabelave(ci);
        }
        string[] shkronjevogel = { "", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t" };

        int niv = 0;


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
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
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
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;ardhura_shpenzime_sipas_muajve')";
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
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;ardhura_shpenzime_sipas_muajve')";
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
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;ardhura_shpenzime_sipas_muajve')";
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
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;ardhura_shpenzime_sipas_muajve')";
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
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;ardhura_shpenzime_sipas_muajve')";
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
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";Detail;ardhura_shpenzime_sipas_muajve')";
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
            //xrLabel79_AfterPrint
            if (xrTableCell55.Text != "")
            {
                int.TryParse(xrTableCell55.Text, out nr);

            }
            xrTableCell55.Text = (nr + 1 + rritshuma).ToString();
        }

        private void xrTableCell55_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //xrLabel79_SummaryGetResult
            e.Result = xrTableCell55.Text;
            //e.Result = niv;
            e.Handled = true;
        }



        private void xrTableCell60_AfterPrint(object sender, EventArgs e)
        {
            int nr = 0;
            //xrLabel84_AfterPrint
            if (xrTableCell60.Text != "")
            {

                int.TryParse(xrTableCell60.Text, out nr);

            }
            xrTableCell60.Text = (nr + 2 + rritshuma).ToString();
        }

        private void xrTableCell60_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //xrLabel84_SummaryGetResult
            e.Result = xrTableCell60.Text;
            //e.Result = niv;
            e.Handled = true;
            //  niv++;
            //e.Result = niv + 1;
            //e.Handled = true;
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportPASHSipasMuajveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell81.Text = rm.GetString("labelRaportiEmertimi", ci);
            xrLabel7.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell82.Text = rm.GetString("labelRaportiShenime", ci);
            xrTableCell84.Text = rm.GetString("labelRaportJanar", ci);
            xrTableCell85.Text = rm.GetString("labelRaportShkurt", ci);
            xrTableCell87.Text = rm.GetString("labelRaportMars", ci);
            xrTableCell86.Text = rm.GetString("labelRaportPrill", ci);
            xrTableCell89.Text = rm.GetString("labelRaportMaj", ci);
            xrTableCell88.Text = rm.GetString("labelRaportQershor", ci);
            xrTableCell92.Text = rm.GetString("labelRaportKorrik", ci);
            xrTableCell90.Text = rm.GetString("labelRaportGusht", ci);
            xrTableCell91.Text = rm.GetString("labelRaportShtator", ci);
            xrTableCell94.Text = rm.GetString("labelRaportTetor", ci);
            xrTableCell93.Text = rm.GetString("labelRaportNentor", ci);
            xrTableCell83.Text = rm.GetString("labelRaportDhjetor", ci);
            xrTableCell234.Text = rm.GetString("labelRaportMaxhinaliteti", ci);
            xrTableCell56.Text = rm.GetString("labelRaportiFitimiNetoVitFinanciar", ci);
            xrTableCell80.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell61.Text = rm.GetString("labelElementeTePasqyraveTeKonsoliduara", ci);
            xrLabel70.Text = rm.GetString("filterMonedha", ci);
            xrTableCell84.Text = rm.GetString("labelRaportJanar", ci) + " " + viti.ToString();
            xrTableCell85.Text = rm.GetString("labelRaportShkurt", ci) + " " + viti.ToString();
            xrTableCell87.Text = rm.GetString("labelRaportMars", ci) + " " + viti.ToString();
            xrTableCell86.Text = rm.GetString("labelRaportPrill", ci) + " " + viti.ToString();
            xrTableCell89.Text = rm.GetString("labelRaportMaj", ci) + " " + viti.ToString();
            xrTableCell88.Text = rm.GetString("labelRaportQershor", ci) + " " + viti.ToString();
            xrTableCell92.Text = rm.GetString("labelRaportKorrik", ci) + " " + viti.ToString();
            xrTableCell90.Text = rm.GetString("labelRaportGusht", ci) + " " + viti.ToString();
            xrTableCell91.Text = rm.GetString("labelRaportShtator", ci) + " " + viti.ToString();
            xrTableCell94.Text = rm.GetString("labelRaportTetor", ci) + " " + viti.ToString();
            xrTableCell93.Text = rm.GetString("labelRaportNentor", ci) + " " + viti.ToString();
            xrTableCell83.Text = rm.GetString("labelRaportDhjetor", ci) + " " + viti.ToString();
            xrTableCell250.Text = rm.GetString("labelRaportJanar", ci) + " " + (viti - 1).ToString();
            xrTableCell266.Text = rm.GetString("labelRaportShkurt", ci) + " " + (viti - 1).ToString();
            xrTableCell282.Text = rm.GetString("labelRaportMars", ci) + " " + (viti - 1).ToString();
            xrTableCell298.Text = rm.GetString("labelRaportPrill", ci) + " " + (viti - 1).ToString();
            xrTableCell314.Text = rm.GetString("labelRaportMaj", ci) + " " + (viti - 1).ToString();
            xrTableCell330.Text = rm.GetString("labelRaportQershor", ci) + " " + (viti - 1).ToString();
            xrTableCell346.Text = rm.GetString("labelRaportKorrik", ci) + " " + (viti - 1).ToString();
            xrTableCell362.Text = rm.GetString("labelRaportGusht", ci) + " " + (viti - 1).ToString();
            xrTableCell378.Text = rm.GetString("labelRaportShtator", ci) + " " + (viti - 1).ToString();
            xrTableCell394.Text = rm.GetString("labelRaportTetor", ci) + " " + (viti - 1).ToString();
            xrTableCell410.Text = rm.GetString("labelRaportNentor", ci) + " " + (viti - 1).ToString();
            xrTableCell426.Text = rm.GetString("labelRaportDhjetor", ci) + " " + (viti - 1).ToString();

        }
    }
}
