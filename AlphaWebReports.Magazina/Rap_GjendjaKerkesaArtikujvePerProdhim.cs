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

namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_GjendjaKerkesaArtikujvePerProdhim : DevExpress.XtraReports.UI.XtraReport, IUpdateDetail
    {
		public Rap_GjendjaKerkesaArtikujvePerProdhim(){InitializeComponent();} 
        Dictionary<string, bool> skippedDetailBands = new Dictionary<string, bool>();
        bool hapurgjitha = false;
        private decimal koha = 0;
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
        public Rap_GjendjaKerkesaArtikujvePerProdhim(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_GjendjaKerkesaArtikujvePerProdhim(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);    
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
            parameter6.Value = raport.Parameters[5].Value;
            parameter7.Value = raport.Parameters[6].Value;
            parameter7.Value = raport.Parameters[7].Value;
            hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);
        }     
        private void xrLabel18_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KODARTIKULLI") != null)
            {
                string kodPrind = GetCurrentColumnValue("KODARTIKULLI").ToString();
                if (SkippedDetailBands.ContainsKey(kodPrind))
                    e.Cancel = Convert.ToBoolean(SkippedDetailBands[kodPrind]);
                else
                {
                    e.Cancel = !hapurgjitha;
                    SkippedDetailBands.Add(kodPrind, !hapurgjitha);
                }
            }
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("lblRaportTitullGjendjaKerkesaArtProdhim", ci);
            xrTableCell13.Text = rm.GetString("labelKodi", ci);
            xrTableCell14.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell15.Text = rm.GetString("labelNjesia", ci);
            xrTableCell16.Text = rm.GetString("lblSasiPorositur", ci);
            xrTableCell18.Text = rm.GetString("lblGjendjaNeMagazine", ci);
            xrTableCell20.Text = rm.GetString("lblGjendjeMinimum", ci);
            xrTableCell22.Text = rm.GetString("lblSasiPorosiProdhuar", ci);
            xrTableCell24.Text = rm.GetString("lblKoheProdhuarMin", ci);
            xrLabel1.Text = rm.GetString("labelRaportRecepturat", ci);
            xrLabel18.Text = rm.GetString("labelKodi", ci);
            xrLabel13.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel14.Text = rm.GetString("labelNjesia", ci);
            xrLabel16.Text = rm.GetString("lblSasiaKerkuar", ci);
            xrLabel15.Text = rm.GetString("lblGjendjaNeMagazine", ci);
            xrLabel21.Text = rm.GetString("lblGjendjeMinimum", ci);
            xrLabel22.Text = rm.GetString("lblSasiPorosiProdhuar", ci);
            xrLabel19.Text = rm.GetString("lblKoheProdhuar", ci);
            xrLabel20.Text = rm.GetString("labelFurnitori", ci);
            xrLabel28.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel31.Text = rm.GetString("lblKohaTotMin", ci);
     
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KODARTIKULLI") != null)
            {
                string kodPrind = GetCurrentColumnValue("KODARTIKULLI").ToString();
                if (SkippedDetailBands.ContainsKey(kodPrind))
                    e.Cancel = Convert.ToBoolean(SkippedDetailBands[kodPrind]);
                else
                {
                    e.Cancel = !hapurgjitha;
                    SkippedDetailBands.Add(kodPrind, !hapurgjitha);
                }
            }
        }
        private void xrLabel32_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            if (GetCurrentColumnValue("KODARTIKULLI") != System.DBNull.Value && GetCurrentColumnValue("KODARTIKULLI") != null)
            {
                string kodPrind = GetCurrentColumnValue("KODARTIKULLI").ToString();
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + kodPrind + ";GroupHeader;gjendjeKerkeseArtikujshPerProdhim')";
                if (!SkippedDetailBands.ContainsKey(kodPrind))
                    if (hapurgjitha)
                        label.Text = "-";                           
                    else                  
                        label.Text = "+";
                    
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

        private void GroupHeader2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KODARTIKULLI") != null)
            {
                string kodPrind = GetCurrentColumnValue("KODARTIKULLI").ToString();
                if (SkippedDetailBands.ContainsKey(kodPrind))
                    e.Cancel = Convert.ToBoolean(SkippedDetailBands[kodPrind]);
                else
                {
                    e.Cancel = !hapurgjitha;
                    SkippedDetailBands.Add(kodPrind, !hapurgjitha);
                }
            }
        }
        private void xrLabel13_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KODARTIKULLI") != null)
            {
                string kodPrind = GetCurrentColumnValue("KODARTIKULLI").ToString();
                if (SkippedDetailBands.ContainsKey(kodPrind))
                    e.Cancel = Convert.ToBoolean(SkippedDetailBands[kodPrind]);
                else
                {
                    e.Cancel = !hapurgjitha;
                    SkippedDetailBands.Add(kodPrind, !hapurgjitha);
                }
            }
        }

        private void xrLabel14_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KODARTIKULLI") != null)
            {
                string kodPrind = GetCurrentColumnValue("KODARTIKULLI").ToString();
                if (SkippedDetailBands.ContainsKey(kodPrind))
                    e.Cancel = Convert.ToBoolean(SkippedDetailBands[kodPrind]);
                else
                {
                    e.Cancel = !hapurgjitha;
                    SkippedDetailBands.Add(kodPrind, !hapurgjitha);
                }
            }
        }

        private void xrLabel16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KODARTIKULLI") != null)
            {
                string kodPrind = GetCurrentColumnValue("KODARTIKULLI").ToString();
                if (SkippedDetailBands.ContainsKey(kodPrind))
                    e.Cancel = Convert.ToBoolean(SkippedDetailBands[kodPrind]);
                else
                {
                    e.Cancel = !hapurgjitha;
                    SkippedDetailBands.Add(kodPrind, !hapurgjitha);
                }
            }
        }

        private void xrLabel15_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KODARTIKULLI") != null)
            {
                string kodPrind = GetCurrentColumnValue("KODARTIKULLI").ToString();
                if (SkippedDetailBands.ContainsKey(kodPrind))
                    e.Cancel = Convert.ToBoolean(SkippedDetailBands[kodPrind]);
                else
                {
                    e.Cancel = !hapurgjitha;
                    SkippedDetailBands.Add(kodPrind, !hapurgjitha);
                }
            }
        }

        private void xrLabel21_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KODARTIKULLI") != null)
            {
                string kodPrind = GetCurrentColumnValue("KODARTIKULLI").ToString();
                if (SkippedDetailBands.ContainsKey(kodPrind))
                    e.Cancel = Convert.ToBoolean(SkippedDetailBands[kodPrind]);
                else
                {
                    e.Cancel = !hapurgjitha;
                    SkippedDetailBands.Add(kodPrind, !hapurgjitha);
                }
            }
        }

        private void xrLabel22_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KODARTIKULLI") != null)
            {
                string kodPrind = GetCurrentColumnValue("KODARTIKULLI").ToString();
                if (SkippedDetailBands.ContainsKey(kodPrind))
                    e.Cancel = Convert.ToBoolean(SkippedDetailBands[kodPrind]);
                else
                {
                    e.Cancel = !hapurgjitha;
                    SkippedDetailBands.Add(kodPrind, !hapurgjitha);
                }
            }
        }

        private void xrLabel19_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KODARTIKULLI") != null)
            {
                string kodPrind = GetCurrentColumnValue("KODARTIKULLI").ToString();
                if (SkippedDetailBands.ContainsKey(kodPrind))
                    e.Cancel = Convert.ToBoolean(SkippedDetailBands[kodPrind]);
                else
                {
                    e.Cancel = !hapurgjitha;
                    SkippedDetailBands.Add(kodPrind, !hapurgjitha);
                }
            }
        }

        private void xrLabel20_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KODARTIKULLI") != null)
            {
                string kodPrind = GetCurrentColumnValue("KODARTIKULLI").ToString();
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
