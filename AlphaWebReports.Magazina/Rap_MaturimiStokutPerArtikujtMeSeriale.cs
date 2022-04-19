using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Collections.Generic;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_MaturimiStokutPerArtikujtMeSeriale : DevExpress.XtraReports.UI.XtraReport,  IUpdateDetail
    {
        bool hapurgjitha = false;
        public Rap_MaturimiStokutPerArtikujtMeSeriale()
        {
            InitializeComponent();
        }
        public Rap_MaturimiStokutPerArtikujtMeSeriale(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_MaturimiStokutPerArtikujtMeSeriale(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);
        }
        Dictionary<string, bool> skippedDetailBands = new Dictionary<string, bool>();
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
        private void xrLabel28_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            XRLabel label = sender as XRLabel;
            if (GetCurrentColumnValue("KODARTIKULLI") != System.DBNull.Value && GetCurrentColumnValue("KODARTIKULLI") != null)
            {
                string kodi = GetCurrentColumnValue("KODARTIKULLI").ToString();
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + kodi + ";Detail;MaturimiStokutPerArtikujtMeSeriale')";
                if (!SkippedDetailBands.ContainsKey(kodi))
                    if (hapurgjitha)
                        label.Text = "-";
                    else label.Text = "+";
                else if ((bool)SkippedDetailBands[kodi] == false)
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
             if (GetCurrentColumnValue("KODARTIKULLI") != null)
            {
                string kodi = GetCurrentColumnValue("KODARTIKULLI").ToString();

                if (SkippedDetailBands.ContainsKey(kodi))
                    e.Cancel = Convert.ToBoolean(SkippedDetailBands[kodi]);
                else
                {
                    e.Cancel = !hapurgjitha;
                    SkippedDetailBands.Add(kodi, !hapurgjitha);
                }
             }
         }
        public void UpdateDetail(string kodi)
        {
            if (SkippedDetailBands.ContainsKey(kodi))
                SkippedDetailBands[kodi] = !Convert.ToBoolean(SkippedDetailBands[kodi]);
            else
                SkippedDetailBands.Add(kodi, false);
            
        }

 

    }
}
