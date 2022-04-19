using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_GjendjaPermbledhurArtSipasNdermarrjeve : DevExpress.XtraReports.UI.XtraReport, IUpdateDetail
    {
		public Rap_GjendjaPermbledhurArtSipasNdermarrjeve(){InitializeComponent();} 
        string windowWidth = "";

        bool hapurgjitha = false;
        public Rap_GjendjaPermbledhurArtSipasNdermarrjeve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.IdRaporti, param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.IdGjuha, report)
        {

        }
        public Rap_GjendjaPermbledhurArtSipasNdermarrjeve(int idRaporti, CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, int idGjuha, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[2].Value;
            parameter3.Value = raport.Parameters[3].Value;
            parameter4.Value = raport.Parameters[4].Value;
            parameter5.Value = raport.Parameters[7].Value;
            parameter7.Value = raport.Parameters[6].Value;
            parameter6.Value = raport.Parameters[10].Value;
            parameter9.Value = raport.Parameters[1].Value;
            windowWidth = Convert.ToString(raport.Parameters[6].Value);
            hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);
 
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel17.Text = rm.GetString("RaportGjendjaPermbledhurArtikujveSipasNdermTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel1.Text = rm.GetString("labelKartela", ci);
            xrLabel26.Text = rm.GetString("koloneLoginNdermarrjeNdermarrja", ci);
            xrLabel2.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel3.Text = rm.GetString("labelNjesia", ci);
            xrLabel4.Text = rm.GetString("labelRaportGjendjaeMbartur", ci);
            xrLabel5.Text = rm.GetString("labelRaportHyrje", ci);
            xrLabel6.Text = rm.GetString("labelRaportDalje", ci);
            xrLabel7.Text = rm.GetString("labelRaportGjendje", ci);
            xrLabel8.Text = rm.GetString("labelRaportKosto", ci);
            xrLabel9.Text = rm.GetString("labelVlefta", ci);
            xrLabel23.Text = rm.GetString("labelRaportiTotali", ci) + ":";
            xrLabel32.Text = rm.GetString("labelLogoIMB", ci);
        }

        private Hashtable skippedDetailBands;
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
        public void UpdateDetail(string detailID)
        {
            if (SkippedDetailBands.Contains(detailID))
                SkippedDetailBands[detailID] = !Convert.ToBoolean(SkippedDetailBands[detailID]);
            else
                SkippedDetailBands.Add(detailID, false);

            
        }
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KODARTIKULLI") != null)
            {
                string kodArt = GetCurrentColumnValue("KODARTIKULLI").ToString() + GetCurrentColumnValue("eshteArtILidhur");
                if (SkippedDetailBands.ContainsKey(kodArt))
                    e.Cancel = Convert.ToBoolean(SkippedDetailBands[kodArt]);
                else
                {
                    e.Cancel = !hapurgjitha;
                    SkippedDetailBands.Add(kodArt, !hapurgjitha);
                }
            }
        }

        private void xrLabel43_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;

            if (GetCurrentColumnValue("KODARTIKULLI") != System.DBNull.Value && GetCurrentColumnValue("KODARTIKULLI") != null)
            {
                //if (GetCurrentColumnValue("pershkrimNdermBije") == System.DBNull.Value || GetCurrentColumnValue("pershkrimNdermBije") == null)
                //    label.Text = "";
                //else
                //{
                string kodArt = GetCurrentColumnValue("KODARTIKULLI").ToString() + GetCurrentColumnValue("eshteArtILidhur");
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + kodArt + ";Detail;gjendjepermbledhurArtikujshNdermarje')";
                if (!SkippedDetailBands.ContainsKey(kodArt))
                    if (hapurgjitha)
                        label.Text = "-";
                    else label.Text = "+";
                else if ((bool)SkippedDetailBands[kodArt] == false)
                    label.Text = "-";
                else
                    label.Text = "+";
                // }
            }
            else
            {
                label.Text = "";
            }
        }

    }
}
