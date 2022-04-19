using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Collections.Generic;
using System.Reflection;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_MarzhiShitjeve_SipasMagazinave : DevExpress.XtraReports.UI.XtraReport, IUpdateDetail
    {
		public Rap_MarzhiShitjeve_SipasMagazinave(){InitializeComponent();} 
        Dictionary<string, bool> skippedDetailBands = new Dictionary<string, bool>();
        bool hapurgjitha = false;
        double vleramezbritje = 0.0;
        double marzhimezbritje = 0.0;
        private double marzhiBrutoMeZbritje = 0.0;
        private double vleraShitjesMeZbritje = 0.0;
        

        public Dictionary<string, bool> SkippedDetailBands
        {
            get
            {
                if (skippedDetailBands == null)
                    skippedDetailBands = new Dictionary<string, bool>();
                return skippedDetailBands;
            }
            set { skippedDetailBands = value; }
        }
        public Rap_MarzhiShitjeve_SipasMagazinave(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_MarzhiShitjeve_SipasMagazinave(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            Ndermarrja.Value = raport.Parameters[0].Value;
            DtDok.Value = raport.Parameters[1].Value;
            Klient.Value = raport.Parameters[2].Value;
            Artikull.Value = raport.Parameters[3].Value;
            Qyteti.Value = raport.Parameters[4].Value;
            Grupim1.Value = raport.Parameters[5].Value;
            Grupim2.Value = raport.Parameters[6].Value;
            PikeShitjeFurnizim.Value = raport.Parameters[7].Value;
            LlojArtikulli.Value = raport.Parameters[8].Value;
            KlasaArtikulli.Value = raport.Parameters[9].Value;
            parameter1.Value = raport.Parameters[10].Value;
            parameter2.Value = raport.Parameters[11].Value;
            parameter3.Value = raport.Parameters[12].Value;
            parameter4.Value = raport.Parameters[14].Value;
            parameter5.Value = raport.Parameters[15].Value;
            parameter6.Value = raport.Parameters[16].Value;
            DegaAdministrative.Value = raport.Parameters[13].Value;
            parameter7.Value=raport.Parameters[17].Value;
        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {

            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            MarzhiShitjeveLabel.Text = rm.GetString("lblRaportTitulliMarzhiShitjeveSipasMag", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel9.Text = rm.GetString("labelKodi", ci);
            xrLabel11.Text = rm.GetString("labelRaportMagazina", ci);
            xrLabel12.Text = rm.GetString("labelKMSH", ci);
            xrLabel13.Text = rm.GetString("labelVleraShitjesMeZbritje", ci);
            xrLabel14.Text = rm.GetString("labelMarzhiBrutoMeZbritje", ci);
            xrLabel15.Text = rm.GetString("labelMarzhiBrutoPerqindje", ci);
            TotaliLabel.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel32.Text = rm.GetString("labelLogoIMB", ci);
        }
        private void xrLabel28_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            if (GetCurrentColumnValue("kodimag") != System.DBNull.Value && GetCurrentColumnValue("kodimag") != null)
            {
                string kodimag = GetCurrentColumnValue("kodimag").ToString();
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + kodimag + ";Detail;marzhiShitjeveMagazina')";
                if (!SkippedDetailBands.ContainsKey(kodimag))
                    if (hapurgjitha)
                        label.Text = "-";
                    else label.Text = "+";
                else if ((bool)SkippedDetailBands[kodimag] == false)
                    label.Text = "-";
                else
                    label.Text = "+";
            }
            else
            {
                label.Text = "";
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("kodimag") != null)
            {
                string kodimag = GetCurrentColumnValue("kodimag").ToString();
                if (SkippedDetailBands.ContainsKey(kodimag))
                    e.Cancel = Convert.ToBoolean(SkippedDetailBands[kodimag]);
                else
                {
                    e.Cancel = !hapurgjitha;
                    SkippedDetailBands.Add(kodimag, !hapurgjitha);
                }
            }
        }

        public void UpdateDetail(string kodimag)
        {
            if (SkippedDetailBands.ContainsKey(kodimag))
                SkippedDetailBands[kodimag] = !Convert.ToBoolean(SkippedDetailBands[kodimag]);
            else
                SkippedDetailBands.Add(kodimag, false);
            
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            

        }

        private void xrLabel6_AfterPrint(object sender, EventArgs e)
        {
            double vl;
            if (xrLabel6.Text == "")
            {
                vl = 0;
            }
            else
            {
                vl = Convert.ToDouble(xrLabel6.Text);
            }
            marzhiBrutoMeZbritje = marzhiBrutoMeZbritje + vl;

        }

        private void xrLabel5_AfterPrint(object sender, EventArgs e)
        {
            double vl;
            if (xrLabel5.Text == "")
            {
                vl = 0;
            }
            else
            {
                vl = Convert.ToDouble(xrLabel5.Text);
            }
            vleraShitjesMeZbritje = vleraShitjesMeZbritje + vl;

        }

        private void xrLabel7_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (vleramezbritje == 0.0)
            { marzhimezbritje = 0.0; }
            else
            {
                e.Result = String.Format("{0:#,#.00}", (marzhimezbritje / vleramezbritje) * 100);
            }
            e.Handled = true;

        }

        private void xrLabel7_SummaryReset(object sender, EventArgs e)
        {
            vleramezbritje = 0.0;
            marzhimezbritje = 0.0;


        }

        private void xrLabel7_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("MARZHIMEZBRITJE") != null)
            {
                marzhimezbritje += Convert.ToDouble(GetCurrentColumnValue("MARZHIMEZBRITJE"));

            }
            if (GetCurrentColumnValue("VLERAMEZBRITJE") != null)
            {
                vleramezbritje += Convert.ToDouble(GetCurrentColumnValue("VLERAMEZBRITJE"));
            }


        }

        private void MarzhiPerqindjeTotali_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (vleraShitjesMeZbritje == 0.0)
            { marzhiBrutoMeZbritje = 0.0; }
            e.Result = String.Format("{0:#,#.00}", (marzhiBrutoMeZbritje / vleraShitjesMeZbritje) * 100);
            e.Handled = true;


        }

        private void MarzhiPerqindjeTotali_SummaryReset(object sender, EventArgs e)
        {
            vleraShitjesMeZbritje = 0.0;
            marzhiBrutoMeZbritje = 0.0;


        }

        private void MarzhiPerqindjeTotali_SummaryRowChanged(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("MARZHIMEZBRITJE") != null)
            {
                marzhiBrutoMeZbritje += Convert.ToDouble(GetCurrentColumnValue("MARZHIMEZBRITJE"));

            }
            if (GetCurrentColumnValue("VLERAMEZBRITJE") != null)
            { vleraShitjesMeZbritje += Convert.ToDouble(GetCurrentColumnValue("VLERAMEZBRITJE")); }

        }

        private void Rap_MarzhiShitjeve_SipasMagazinave_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);

        }
    }
}
