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
        //string nrdok = "";
        //string kodi = "";
        //double sasia = 0;
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

            //xrLabel12.Text = rm.GetString("RaportIPorosiveTitulli", ci);
            //FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            //xrLabel3.Text = rm.GetString("labelNR_USH", ci);
            //xrLabel20.Text = rm.GetString("labelNR_FSH", ci);
            //xrLabel19.Text = rm.GetString("labelDataPorosise", ci);
            //xrLabel46.Text = rm.GetString("labelAfatiKohor", ci);
            //xrLabel17.Text = rm.GetString("labelKLIENTI", ci);
            //xrLabel16.Text = rm.GetString("labelPERSHKRIMI", ci);
            //xrLabel15.Text = rm.GetString("label_KODI", ci);
            //xrLabel13.Text = rm.GetString("label_SASIA", ci);
            //xrLabel11.Text = rm.GetString("label_CMIMI", ci);
            //xrLabel10.Text = rm.GetString("labelVlera_Pa_Tvsh", ci);
            //xrLabel9.Text = rm.GetString("labelTVSH", ci);
            //xrLabel8.Text = rm.GetString("labelVLERA_ME_TVSH", ci);
            //xrLabel7.Text = rm.GetString("labelPERFUNDUAR", ci);
            //xrLabel6.Text = rm.GetString("labelRaportFaturuarUpperCase", ci);
            //xrLabel5.Text = rm.GetString("labelArketuarUpperCase", ci);
            //xrLabel4.Text = rm.GetString("labelStatusiUpperCase", ci);
            //xrLabel21.Text = rm.GetString("labelShenimeUpperCase", ci);
            //xrLabel22.Text = rm.GetString("labelShitesUpperCase", ci);
            //xrLabel24.Text = rm.GetString("labelTOTAL_USH", ci);
            //TotaliLabel.Text = rm.GetString("labelTotaliUpperCase", ci);
            //xrLabel45.Text = rm.GetString("labelLogoIMB", ci);
            //xrLabel14.Text = rm.GetString("labelKodi", ci);
            //xrLabel29.Text = rm.GetString("labelRaportiPRODHUAR", ci);
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
