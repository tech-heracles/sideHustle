using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Collections.Generic;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
  
    public partial class RAP_BilanciEnergjetikPermbledhes : DevExpress.XtraReports.UI.XtraReport, IUpdateDetail
    {
		public RAP_BilanciEnergjetikPermbledhes(){InitializeComponent();} 
        private ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
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
        public RAP_BilanciEnergjetikPermbledhes(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report)
        {

        }
        public RAP_BilanciEnergjetikPermbledhes(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
            parameter6.Value = raport.Parameters[5].Value;
            parameter7.Value = raport.Parameters[6].Value;
            hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);
            EmrateLabelave(ci);           

        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));

        }

        public void UpdateDetail(string kodPrind)
        {
            if (SkippedDetailBands.ContainsKey(kodPrind))
                SkippedDetailBands[kodPrind] = !Convert.ToBoolean(SkippedDetailBands[kodPrind]);
            else
                SkippedDetailBands.Add(kodPrind, false);
            
        }

        private void xrTableCell17_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            if (GetCurrentColumnValue("muaji") != System.DBNull.Value && GetCurrentColumnValue("muaji") != null)
            {
                string muaji = GetCurrentColumnValue("muaji").ToString();
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + muaji + ";Detail;bilanciEnergjitikPermbledhes')";
                if (!SkippedDetailBands.ContainsKey(muaji))
                    if (hapurgjitha)
                        label.Text = "-";
                    else label.Text = "+";
                else if ((bool)SkippedDetailBands[muaji] == false)
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
            if (GetCurrentColumnValue("muaji") != null)
            {
                string kodPrind = GetCurrentColumnValue("muaji").ToString();
                if (SkippedDetailBands.ContainsKey(kodPrind))
                    e.Cancel = Convert.ToBoolean(SkippedDetailBands[kodPrind]);
                else
                {
                    e.Cancel = !hapurgjitha;
                    SkippedDetailBands.Add(kodPrind, !hapurgjitha);
                }
            }
        }
    }
}
