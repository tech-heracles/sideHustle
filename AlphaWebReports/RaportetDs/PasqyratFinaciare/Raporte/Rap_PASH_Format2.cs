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
    public partial class Rap_PASH_Format2 : DevExpress.XtraReports.UI.XtraReport, IUpdateDetailKPF
    {
        bool hapurgjitha = false;
        private int rritshuma = 0;
        int formatNumri = 0;
        public Rap_PASH_Format2()
        {
           InitializeComponent();
        }
        public Rap_PASH_Format2(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_PASH_Format2(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            MonNder.Value = raport.Parameters["Azhornim"].Value;
            //xrLabel52.Text = raport.Parameters["filterDtDok"].Description;
            parameter1.Value = raport.Parameters["filterDtDok"].Value;
            //xrLabel55.Text = raport.Parameters["IdRaport"].Description;
            parameter2.Value = raport.Parameters["IdRaport"].Value;
            parameter4.Value = raport.Parameters["filterDtDok2"].Value;
            parameter5.Value = raport.Parameters["filterDtDokKrahasuesMbarim"].Value;
            //azhornimLabel.Text = raport.Parameters["idpasqyrafinaciarekoka"].Description;
            Azhornim.Value = raport.Parameters["idpasqyrafinaciarekoka"].Value;
            //xrLabel18.Text = raport.Parameters["filterDtDokKrahasues"].Description;
            parameter3.Value = raport.Parameters["filterDtDokKrahasues"].Value;
            hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);           
            EmrateLabelave(ci);
            formatNumri = Convert.ToInt32(raport.Parameters["filterFormatNumri"].Value);
            parameterFormatNumri.Value = formatNumri;
            parameterIdNderm.Value = idNdermarrje;
            caktoFormatinENumrave();
        }  
        
        public void caktoFormatinENumrave()
        {
                xrTableCell40.XlsxFormatString = xrTableCell5.XlsxFormatString = xrTableCell11.XlsxFormatString = xrTableCell17.XlsxFormatString = xrTableCell23.XlsxFormatString
                = xrTableCell30.XlsxFormatString = xrTableCell43.XlsxFormatString = xrTableCell50.XlsxFormatString = xrTableCell53.XlsxFormatString = xrTableCell68.XlsxFormatString
                = xrTableCell73.XlsxFormatString = xrTableCell78.XlsxFormatString = xrTableCell59.XlsxFormatString = xrTableCell41.XlsxFormatString = xrTableCell6.XlsxFormatString
                = xrTableCell12.XlsxFormatString = xrTableCell18.XlsxFormatString = xrTableCell24.XlsxFormatString = xrTableCell31.XlsxFormatString = xrTableCell44.XlsxFormatString
                = xrTableCell51.XlsxFormatString = xrTableCell54.XlsxFormatString = xrTableCell69.XlsxFormatString = xrTableCell74.XlsxFormatString = xrTableCell79.XlsxFormatString
                = xrTableCell58.XlsxFormatString = 0.ToString("N" + formatNumri);

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
        

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            // xrLabel12.Text = rm.GetString("RaportTeArdhuratShpenzimetTitulli", ci);
            //FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel3.Text = rm.GetString("labelRaportiEmertimi", ci);
            //   xrLabel7.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel4.Text = rm.GetString("labelRaportiVitiRaportues", ci);
            xrLabel5.Text = rm.GetString("labelRaportiVitiParaardhes", ci);
            xrTableCell56.Text = rm.GetString("labelRaportiFitimiNetoVitFinanciar", ci);
            xrLabel1.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell61.Text = rm.GetString("labelElementeTePasqyraveTeKonsoliduara", ci);
            //xrLabel70.Text = rm.GetString("filterMonedha", ci);
        }

       

        private void xrTableCell37_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //xrLabel32_BeforePrint_1
            if (xrTableCell37.Text != "" && xrTableCell38.Text == " Te pacaktuara")
            {
                xrTableCell37.Text = (Convert.ToInt16(xrTableCell37.Text) + 1).ToString();
                rritshuma = 1;
            }
        }

        private void xrTableCell36_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //lblPrindi1_BeforePrint
            XRTableCell label = sender as XRTableCell;
            string catid = String.Empty;
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") == null)
                return;
            else
                catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();

            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;teardhuraKesh')";
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
            //lblprindi2_BeforePrint
            XRTableCell label = sender as XRTableCell;
            string catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ2") != null && GetCurrentColumnValue("SHFAQBIJ2").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;teardhuraKesh')";
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
            //lblprindi3_BeforePrint
            XRTableCell label = sender as XRTableCell;
            string catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ3") != null && GetCurrentColumnValue("SHFAQBIJ3").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;teardhuraKesh')";
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
            //lblprindi4_BeforePrint
            XRTableCell label = sender as XRTableCell;
            string catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ4") != null && GetCurrentColumnValue("SHFAQBIJ4").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;teardhuraKesh')";
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
            //lblprindi5_BeforePrint
            XRTableCell label = sender as XRTableCell;
            string catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ5") != null && GetCurrentColumnValue("SHFAQBIJ5").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;teardhuraKesh')";
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
            //lblKPF_BeforePrint
            XRTableCell label = sender as XRTableCell;
            string catid = GetCurrentColumnValue("kodikpf").ToString();
            if (GetCurrentColumnValue("NRLLOGARI").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJLLOG") != null && GetCurrentColumnValue("SHFAQBIJLLOG").ToString() == "False"))
            {
                label.Text = "";
            }
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";Detail;teardhuraKesh')";
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

        private void xrLabel7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel7.Text = "Treguesit financiarë të " + GetCurrentColumnValue("PERSHKRIMNDERMARRJE").ToString() + " - " + parameter4.Value.ToString();
        }

    }
}
