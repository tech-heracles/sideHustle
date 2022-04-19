using AlphaWebReports.Common;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.PasqyratFinaciare.Raporte
{
    public partial class Rap_Pasqyra_Levizjeve_AQT : DevExpress.XtraReports.UI.XtraReport, IUpdateDetailKPF
    {
		public Rap_Pasqyra_Levizjeve_AQT(){InitializeComponent();} 
        private bool hapurgjitha = false;
        
        public Rap_Pasqyra_Levizjeve_AQT(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_Pasqyra_Levizjeve_AQT(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            MonNder.Value = raport.Parameters[4].Value;
            xrLabel52.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel55.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            azhornimLabel.Text = raport.Parameters[3].Description;
            Azhornim.Value = raport.Parameters[3].Value;
            xrLabel105.Text = raport.Parameters[5].Description;
            parameter3.Value = raport.Parameters[5].Value;
            EmrateLabelave(ci);
        }

        private string[] shkronjevogel = { string.Empty, "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t" };
        private string[] shkronjemadhe = { string.Empty, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T" };

      
        
        private Hashtable skippedDetailBands;
        private Hashtable skippedDetailKPF;
        public Hashtable SkippedDetailBands
        {
            get
            {
                if (skippedDetailBands == null)
                {
                    skippedDetailBands = new Hashtable();
                }
                return skippedDetailBands;
            }
            set
            {
                skippedDetailBands = value;
            }
        }
        public Hashtable SkippedDetailKPF
        {
            get
            {
                if (skippedDetailKPF == null)
                {
                    skippedDetailKPF = new Hashtable();
                }
                return skippedDetailKPF;
            }
            set
            {
                skippedDetailKPF = value;
            }
        }

        public void UpdateDetailKPF(string detailID)
        {
            if (SkippedDetailKPF.Contains(detailID))
            {
                SkippedDetailKPF[detailID] = !Convert.ToBoolean(SkippedDetailKPF[detailID]);
            }
            else
            {
                SkippedDetailKPF.Add(detailID, false);
            }
            
        }
        public void UpdateDetail(string detailID)
        {
            if (SkippedDetailBands.Contains(detailID))
            {
                SkippedDetailBands[detailID] = !Convert.ToBoolean(SkippedDetailBands[detailID]);
            }
            else
            {
                SkippedDetailBands.Add(detailID, false);
            }
            
        }
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var catid = GetCurrentColumnValue("kodikpf").ToString();
            var catid2 = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            if (SkippedDetailKPF.Contains(catid2))
            {
                if (Convert.ToBoolean(SkippedDetailKPF[catid2]) == true)
                {
                    e.Cancel = true;
                    return;
                }
            }

            if (SkippedDetailBands.Contains(catid))
            {
                e.Cancel = Convert.ToBoolean(SkippedDetailBands[catid]);
            }
            else
            {
                e.Cancel = !hapurgjitha;
                SkippedDetailBands.Add(catid, !hapurgjitha);
            }
        }
        

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();

            if (SkippedDetailKPF.Contains(catid))
            {
                e.Cancel = Convert.ToBoolean(SkippedDetailKPF[catid]);
            }
            else
            {
                e.Cancel = !hapurgjitha;
                SkippedDetailKPF.Add(catid, !hapurgjitha);
            }
        }
        

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            var rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));


            // xrLabel12.Text = rm.GetString("RaportGjendjaNdryshimetAktiveveTitulli", ci);
        }


        private void xrTableCell6_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = sender as XRLabel;
            var catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = string.Empty;
            if (GetCurrentColumnValue("lloji").ToString() == string.Empty || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
            {
                label.Text = string.Empty;
            }
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;pasqyraLevizjeveAQT')";
                if (SkippedDetailKPF.Contains(catid) && (bool)SkippedDetailKPF[catid] == false)
                {
                    label.Text = "-";
                }
                else
                {
                    label.Text = "+";
                }
            }
        }      

        private void xrTableCell15_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = sender as XRLabel;
            var catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = string.Empty;
            if (GetCurrentColumnValue("lloji").ToString() == string.Empty || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
            {
                label.Text = string.Empty;
            }
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;pasqyraLevizjeveAQT')";
                if (SkippedDetailKPF.Contains(catid) && (bool)SkippedDetailKPF[catid] == false)
                {
                    label.Text = "-";
                }
                else
                {
                    label.Text = "+";
                }
            }
        }

        private void xrTableCell30_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = sender as XRLabel;
            var catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = string.Empty;
            if (GetCurrentColumnValue("lloji").ToString() == string.Empty || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
            {
                label.Text = string.Empty;
            }
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;pasqyraLevizjeveAQT')";
                if (SkippedDetailKPF.Contains(catid) && (bool)SkippedDetailKPF[catid] == false)
                {
                    label.Text = "-";
                }
                else
                {
                    label.Text = "+";
                }
            }
        }      

        private void xrTableCell45_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = sender as XRLabel;
            var catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = string.Empty;
            if (GetCurrentColumnValue("lloji").ToString() == string.Empty || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
            {
                label.Text = string.Empty;
            }
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;pasqyraLevizjeveAQT')";
                if (SkippedDetailKPF.Contains(catid) && (bool)SkippedDetailKPF[catid] == false)
                {
                    label.Text = "-";
                }
                else
                {
                    label.Text = "+";
                }
            }
        }   

        private void xrTableCell60_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = sender as XRLabel;
            var catid = GetCurrentColumnValue("PERSHKRIMIZERIT").ToString();
            label.NavigateUrl = string.Empty;
            if (GetCurrentColumnValue("lloji").ToString() == string.Empty || (GetCurrentColumnValue("SHFAQBIJ1") != null && GetCurrentColumnValue("SHFAQBIJ1").ToString() == "False"))
            {
                label.Text = string.Empty;
            }
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";KPF;pasqyraLevizjeveAQT')";
                if (SkippedDetailKPF.Contains(catid) && (bool)SkippedDetailKPF[catid] == false)
                {
                    label.Text = "-";
                }
                else
                {
                    label.Text = "+";
                }
            }
        }   

      

        private void xrTableCell76_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var label = sender as XRLabel;
            var catid = GetCurrentColumnValue("kodikpf").ToString();


            if (GetCurrentColumnValue("NRLLOGARI").ToString() == string.Empty || (GetCurrentColumnValue("SHFAQBIJLLOG") != null && GetCurrentColumnValue("SHFAQBIJLLOG").ToString() == "False"))
            {
                label.Text = string.Empty;
            }
            else
            {
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";Detail;pasqyraLevizjeveAQT')";
                if (!SkippedDetailBands.Contains(catid))
                {
                    if (hapurgjitha)
                    {
                        label.Text = "-";
                    }
                    else
                    {
                        label.Text = "+";
                    }
                }
                else
                {
                    if ((bool)SkippedDetailBands[catid] == false)
                    {
                        label.Text = "-";
                    }
                    else
                    {
                        label.Text = "+";
                    }
                }
            }
        }

        
        }
    }

