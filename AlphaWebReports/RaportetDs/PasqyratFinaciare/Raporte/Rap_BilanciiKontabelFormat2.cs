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
    public partial class Rap_BilanciiKontabelFormat2 : DevExpress.XtraReports.UI.XtraReport, IUpdateDetailKPF
    {
        int formatNumri = 0;
        public Rap_BilanciiKontabelFormat2()
        {
            InitializeComponent();
        }
        bool hapurgjitha = false;
        CultureInfo ci;
        ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
        public Rap_BilanciiKontabelFormat2(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci , param.IdNdermarrje, report) 
        {

        }
        public Rap_BilanciiKontabelFormat2(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport) // int idNdermarrje,
        {
            InitializeComponent();
            this.ci = ci;
            MonNder.Value = raport.Parameters[4].Value;
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            Azhornim.Value = raport.Parameters[3].Value;
            hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);
            parameter3.Value = raport.Parameters[5].Value;
            parameterRapAktivePacaktuar.Value = rm.GetString("labelRaportiAktiveTePacaktuara", ci);
            parameterRapKapitaliPacakt.Value = rm.GetString("labelRaportiKapitaliTePacaktuara", ci);
            parameterRapAktive.Value = rm.GetString("labelRaportiAktive", ci);
            parameterRapKapitali.Value = rm.GetString("labelRaportiKapitaliFormat2", ci);
            parameterRapKapitaliDheDetyrimet.Value = rm.GetString("labelRaportiKapitaliDheDetyrimeFormat2", ci);
            parameterRapDetyrimet.Value = rm.GetString("labelRaportiDetyrimetFormat2", ci);
            formatNumri = Convert.ToInt32(raport.Parameters["filterFormatNumri"].Value);
            parameterFormatNumri.Value = formatNumri;
            EmrateLabelave(ci);
            caktoFormatinENumrave();
        }
        private void caktoFormatinENumrave()
        {
            xrTableCell74.XlsxFormatString = xrTableCell75.XlsxFormatString = xrTableCell40.XlsxFormatString = xrTableCell41.XlsxFormatString
                    = xrTableCell33.XlsxFormatString = xrTableCell34.XlsxFormatString = xrTableCell7.XlsxFormatString = xrTableCell3.XlsxFormatString
                    = xrTableCell27.XlsxFormatString = xrTableCell19.XlsxFormatString = xrTableCell20.XlsxFormatString = xrTableCell12.XlsxFormatString = xrTableCell10.XlsxFormatString
                    = xrTableCell26.XlsxFormatString = xrTableCell59.XlsxFormatString
                = xrTableCell60.XlsxFormatString
                    = xrTableCell62.XlsxFormatString
                     = xrTableCell63.XlsxFormatString = xrTableCell67.XlsxFormatString = xrTableCell68.XlsxFormatString = xrTableCell48.XlsxFormatString
                      = xrTableCell45.XlsxFormatString = xrTableCell54.XlsxFormatString = xrTableCell55.XlsxFormatString = xrTableCell87.XlsxFormatString
                       = xrTableCell88.XlsxFormatString = xrTableCell82.XlsxFormatString = xrTableCell83.XlsxFormatString 
                    = 0.ToString("N" + formatNumri);

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
        
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string catid = GetCurrentColumnValue("kodikpf") == null ? "" : GetCurrentColumnValue("kodikpf").ToString();
            string catid2 = GetCurrentColumnValue("PERSHKRIMIZERIT") == null ? "" : GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();

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
        
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel12.Text = rm.GetString("RaportBilanciFormat2Titulli", ci);
          
            xrLabel21.Text = rm.GetString("labelRaportiVitiRaportues", ci);
            xrLabel22.Text = rm.GetString("labelRaportiVitiParaardhes", ci);
            //xrLabel56.Text = rm.GetString("labelRaportiShuma", ci);
            xrTableCell43.Text = rm.GetString("labelRaportiShuma", ci);
            //xrLabel58.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell50.Text = rm.GetString("labelRaportiTotali", ci);
            //xrLabel63.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell84.Text = rm.GetString("labelRaportiTotali", ci);
            //xrLabel66.Text = rm.GetString("labelRaportiDiferenca", ci);
            xrTableCell69.Text = rm.GetString("labelRaportiDiferenca", ci);
       }
        private void xrTableCell2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            XRTableCell label = sender as XRTableCell;
            string catid = "";
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null) catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji") == null || GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ4") != null && GetCurrentColumnValue("SHFAQBIJ4").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;bilanci;joBuxhetor')";
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
        private void xrTableCell15_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //lblKPF_BeforePrint
            XRTableCell label = sender as XRTableCell;
            string catid = "";
            if (GetCurrentColumnValue("kodikpf") != null) catid = GetCurrentColumnValue("kodikpf").ToString();


            if (GetCurrentColumnValue("NRLLOGARI") == null || GetCurrentColumnValue("NRLLOGARI").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJLLOG") != null && GetCurrentColumnValue("SHFAQBIJLLOG").ToString() == "False"))
            {
                label.Text = "";
            }

            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";Detail;bilanci;joBuxhetor')";
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
        private void xrTableCell22_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //lblprindi5_BeforePrint
            XRTableCell label = sender as XRTableCell;
            string catid = "";
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null) catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji") == null || GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ5") != null && GetCurrentColumnValue("SHFAQBIJ5").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;bilanci;joBuxhetor')";

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

        private void xrTableCell36_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //lblPrindi2_BeforePrint
            XRTableCell label = sender as XRTableCell;
            string catid = "";
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null) catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji") == null || GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ2") != null && GetCurrentColumnValue("SHFAQBIJ2").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;bilanci;joBuxhetor')";
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

        private void xrTableCell70_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //lblPrindi1_BeforePrint
            XRTableCell label = sender as XRTableCell;
            string catid = "";
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null)
                catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji") == null || GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;bilanci;joBuxhetor')";
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
        private void xrTableCell29_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //lblPrindi3_BeforePrint
            XRTableCell label = sender as XRTableCell;
            string catid = "";
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null) catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = "";
            if (GetCurrentColumnValue("lloji") == null || GetCurrentColumnValue("lloji").ToString() == "" || (GetCurrentColumnValue("SHFAQBIJ3") != null && GetCurrentColumnValue("SHFAQBIJ3").ToString() == "False"))
                label.Text = "";
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;bilanci;joBuxhetor')";
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
        private void GroupHeader7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string catid = "";
            if (GetCurrentColumnValue("PERSHKRIMIZERIT") != null) catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();

            if (SkippedDetailKPF.Contains(catid))
                e.Cancel = Convert.ToBoolean(SkippedDetailKPF[catid]);
            else
            {
                e.Cancel = !hapurgjitha; SkippedDetailKPF.Add(catid, !hapurgjitha);
            }
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
    }
}
