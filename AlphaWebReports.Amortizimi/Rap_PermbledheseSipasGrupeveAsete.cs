using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.Amortizimi
{
    public partial class Rap_PermbledheseSipasGrupeveAsete : DevExpress.XtraReports.UI.XtraReport, IUpdateDetail
    {
		public Rap_PermbledheseSipasGrupeveAsete(){InitializeComponent();}
        
        CultureInfo ci;
        ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
        Dictionary<string, bool> skippedDetailBands = new Dictionary<string, bool>();
        bool hapurgjitha = false;
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

        public Rap_PermbledheseSipasGrupeveAsete(ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }

        public Rap_PermbledheseSipasGrupeveAsete(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            StatusMag.Text = raport.Parameters[7].Description;
            parameter1.Value = raport.Parameters[7].Value;
            magazina.Text = raport.Parameters[1].Description + ":";
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            DegaAdmin.Text = raport.Parameters[2].Description;
            parameter4.Value = raport.Parameters[2].Value;
            GrupArt1.Text = raport.Parameters[3].Description;
            parameter5.Value = raport.Parameters[3].Value;
            standarti.Text = raport.Parameters[5].Description;
            parameter7.Value = raport.Parameters[5].Value;
            DateDok.Text = rm.GetString("labelRaportDeriMe", ci);
            parameter6.Value = raport.Parameters["filterDtDok"].Value.ToString().Split('-')[1];
            hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);
            EmrateLabelave(ci);
            this.ci = ci;
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            xrLabel17.Text = rm.GetString("labelRaportTitullPermbledheseSipasGrupeve", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel23.Text = rm.GetString("labelRaportGrupi", ci);
            xrLabel31.Text = rm.GetString("labelRaportAmortizimiAkumuluar", ci);
            xrLabel24.Text = rm.GetString("labelRapVlefta", ci);
            xrLabel33.Text = rm.GetString("labelRaportAmortizimiMujor", ci);
            xrLabel40.Text = rm.GetString("labelRaportVleftaMbetur", ci);
            xrLabel62.Text = rm.GetString("labelRaportiTotali", ci) + ":";
            xrLabel28.Text = rm.GetString("labelRaportPershkrimi", ci);
            xrLabel71.Text = rm.GetString("labelLogoIMB", ci);
        }

        private void xrLabel6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            if (GetCurrentColumnValue("kodPrind") != System.DBNull.Value && GetCurrentColumnValue("kodPrind") != null)
            {
                string kodPrind = GetCurrentColumnValue("kodPrind").ToString();
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + kodPrind + ";Detail;permbledheseSipasGrupeve')";
                if (!SkippedDetailBands.ContainsKey(kodPrind))
                    if (hapurgjitha)
                        label.Text = "-";
                    else label.Text = "+";
                else if ((bool)SkippedDetailBands[kodPrind] == false)
                    label.Text = "-";
                else
                    label.Text = "+";
            }
            else
            {
                label.Text = "";
            }
        }

        public void UpdateDetail(string kodPrind)
        {
            if (SkippedDetailBands.ContainsKey(kodPrind))
                SkippedDetailBands[kodPrind] = !Convert.ToBoolean(SkippedDetailBands[kodPrind]);
            else
                SkippedDetailBands.Add(kodPrind, false);
            
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("kodPrind") != null)
            {
                string kodPrind = GetCurrentColumnValue("kodPrind").ToString();
                if (SkippedDetailBands.ContainsKey(kodPrind))
                    e.Cancel = Convert.ToBoolean(SkippedDetailBands[kodPrind]);
                else
                {
                    e.Cancel = !hapurgjitha; 
                    SkippedDetailBands.Add(kodPrind, !hapurgjitha);
                }
            }
        }

        private void Rap_PermbledheseSipasGrupeveAsete_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
        }
    }
}
