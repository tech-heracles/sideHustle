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
    public partial class Rap_ArtikujShiturDegeAdm_neTon : DevExpress.XtraReports.UI.XtraReport, IUpdateDetail
    {
		public Rap_ArtikujShiturDegeAdm_neTon(){InitializeComponent();} 
        
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
      
        public Rap_ArtikujShiturDegeAdm_neTon(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.IdRaporti,param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        { }
        public Rap_ArtikujShiturDegeAdm_neTon(int idRaporti, CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter6.Value = raport.Parameters[5].Value;
            parameter7.Value = raport.Parameters[6].Value;
            parameter8.Value = raport.Parameters[7].Value;
            KlasaArtikulli.Value = raport.Parameters[10].Value;
            parameter9.Value = raport.Parameters[11].Value;
            parameter10.Value = raport.Parameters[12].Value;
            parameter11.Value = raport.Parameters[13].Value;
            parameter12.Value = raport.Parameters[15].Value;
            parameter13.Value = raport.Parameters[16].Value;
            parameter14.Value = raport.Parameters[17].Value;
            DegaAdministrative.Value = raport.Parameters[14].Value;
            parameter15.Value = raport.Parameters[18].Value;
            parameter16.Value = raport.Parameters[19].Value;
            parameter17.Value = raport.Parameters[20].Value;
            parameter18.Value = raport.Parameters[21].Value;
            adresaFaturimit.Value = raport.Parameters[27].Value;

            hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);



        }

        private decimal shuma;
        private decimal sasia;


        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));


            xrLabel13.Text = rm.GetString("RaportArtikujTeShiturDegeAdmTon", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel17.Text = rm.GetString("labelKodi", ci);
            xrLabel18.Text = rm.GetString("labelRaportiPershkrimi", ci);
           // xrLabel19.Text = rm.GetString("labelNjesia", ci);
            xrLabel20.Text = rm.GetString("labelSasia", ci);
           // xrLabel21.Text = rm.GetString("labelCmimi", ci);
           // xrLabel22.Text = rm.GetString("labelZbritjeAnalitike", ci);
           // xrLabel23.Text = rm.GetString("labelVleftapaTVSH", ci);
           // xrLabel32.Text = rm.GetString("labelZbritjaTotale", ci);
           // xrLabel24.Text = rm.GetString("labelTVSH", ci);
            xrLabel25.Text = rm.GetString("labelVleftameTVSH", ci);
            xrLabel16.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel12.Text = rm.GetString("labelLogoIMB", ci);
        }

        private void xrLabel67_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;

            if (this.GetCurrentColumnValue("dega") != null)
            {
                string dega = GetCurrentColumnValue("dega").ToString();
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + dega + ";GroupHeader1;artikujShiturDegeAdm')";
                if (!SkippedDetailBands.ContainsKey(dega))
                    if (hapurgjitha)
                        label.Text = "-";
                    else label.Text = "+";
                else if ((bool)SkippedDetailBands[dega] == false)
                    label.Text = "-";
                else
                    label.Text = "+";
            }
            
        }

        public void UpdateDetail(string dega)
        {
            if (SkippedDetailBands.ContainsKey(dega))
                SkippedDetailBands[dega] = !Convert.ToBoolean(SkippedDetailBands[dega]);
            else
                SkippedDetailBands.Add(dega, false);
            
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("dega") != null)
            {
                string dega = GetCurrentColumnValue("dega").ToString();
                if (SkippedDetailBands.ContainsKey(dega))
                    e.Cancel = Convert.ToBoolean(SkippedDetailBands[dega]);
                else
                {
                    e.Cancel = !hapurgjitha;
                    SkippedDetailBands.Add(dega, !hapurgjitha);
                }
            }
        }

    }
}
