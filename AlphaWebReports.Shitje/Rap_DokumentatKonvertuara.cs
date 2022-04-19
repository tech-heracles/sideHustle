using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_DokumentatKonvertuara : DevExpress.XtraReports.UI.XtraReport, IUpdateDetailMeDyId
    {

        private Hashtable skippedDetailBands;
        double sasia = 0; double sasiaF = 0;
        bool hapurgjitha = false;
        double sasiakonvertuar = 0;
        double sasif = 0;
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

        public Rap_DokumentatKonvertuara()
        {
            InitializeComponent();
        }
        public Rap_DokumentatKonvertuara(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti,param.IdPerdoruesi, report)
        { }
        public Rap_DokumentatKonvertuara(CultureInfo ci, int idNdermarja, int idViti, int Idperdorues, XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            Ndermarja.Value = raport.Parameters[0].Value;
            LlojDok.Value = raport.Parameters[1].Value;
            Monedha.Value = raport.Parameters[2].Value;
            NivelDok.Value = raport.Parameters[3].Value;
            KlientFurnitori.Value = raport.Parameters[4].Value;
            Kartela.Value = raport.Parameters[5].Value;
            Grupim1.Value = raport.Parameters[6].Value;
            Grupim2.Value = raport.Parameters[7].Value;
            NrDok.Value = raport.Parameters[8].Value;
            DtDok.Value = raport.Parameters[9].Value;
            parameter1.Value = raport.Parameters[10].Value;
            parameter2.Value = raport.Parameters[11].Value;
            parameter3.Value = raport.Parameters[12].Value;
            parameter4.Value = raport.Parameters[13].Value;
            parameter5.Value = raport.Parameters[15].Value;
            parameter6.Value = raport.Parameters[16].Value;
            parameter7.Value = raport.Parameters[18].Value;
            hapurgjitha = Convert.ToBoolean(raport.Parameters["Detajuar"].Value);
            parameter9.Value = raport.Parameters["monedhaKF"].Value;
        }

        private void xrLabel32_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;         
            if (GetCurrentColumnValue("IDKOKAM") != System.DBNull.Value && GetCurrentColumnValue("IDKOKAM") != null)
            {

                string kokaID = GetCurrentColumnValue("IDKOKAF").ToString();
                string artID = GetCurrentColumnValue("IDARTIKULLI").ToString();
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + kokaID + ";" + artID + ";dokumentaKonvertuar')";

                if (!SkippedDetailBands.Contains(kokaID + ";" + artID))
                    if (hapurgjitha)
                        label.Text = "-";
                    else label.Text = "+";
                else if ((bool)SkippedDetailBands[kokaID + ";" + artID] == false)
                    label.Text = "-";
                else
                    label.Text = "+";
            }
            else
            {
                label.Text = "";
            }
        }

        public void UpdateDetail(string kokaID, string artID)
        {
            if (SkippedDetailBands.Contains(kokaID + ";" + artID))
                SkippedDetailBands[kokaID + ";" + artID] = !Convert.ToBoolean(SkippedDetailBands[kokaID + ";" + artID]);
            else
                SkippedDetailBands.Add(kokaID + ";" + artID, false);

            
        }

        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("IDKOKAF") != null)
            {
                string kokaID = GetCurrentColumnValue("IDKOKAF").ToString();
                string artID = GetCurrentColumnValue("IDARTIKULLI").ToString();
                if (SkippedDetailBands.Contains(kokaID + ";" + artID))
                    e.Cancel = Convert.ToBoolean(SkippedDetailBands[kokaID + ";" + artID]);
                else
                {
                    e.Cancel = !hapurgjitha; SkippedDetailBands.Add(kokaID + ";" + artID, !hapurgjitha);
                }
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
            xrLabel12.Text = rm.GetString("RaportDokumentateKonvertuarTitulli", ci);
            xrLabel5.Text = rm.GetString("labelRAportiDokKryesor", ci);
            xrLabel7.Text = rm.GetString("labelRaportiKonvertimet", ci);
            xrLabel4.Text = rm.GetString("labelRaportiDiferenca", ci);
            xrLabel9.Text = rm.GetString("labelLlojDokumenti", ci);
            xrLabel10.Text = rm.GetString("labelRaportiSubjekti", ci);
            xrLabel11.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrLabel13.Text = rm.GetString("labelRaportiNrDok", ci);
            xrLabel14.Text = rm.GetString("labelRaportiDtDok", ci);
            xrLabel15.Text = rm.GetString("labelRaportiArtikull", ci);
            xrLabel16.Text = rm.GetString("labelSasia", ci);
            interval1.Text = rm.GetString("labelCmimi", ci);
            interval2.Text = rm.GetString("labelRaportVlera", ci);
            xrLabel1.Text = rm.GetString("labelSasia", ci);
            xrLabel3.Text = rm.GetString("labelCmimi", ci);
            xrLabel2.Text = rm.GetString("labelRaportVlera", ci);
            xrLabel6.Text = rm.GetString("labelSasia", ci);
            xrLabel17.Text = rm.GetString("labelCmimi", ci);
            xrLabel8.Text = rm.GetString("labelRaportVlera", ci);
            xrLabel29.Text = rm.GetString("labelRaportKonvertuar", ci);
            xrLabel42.Text = rm.GetString("labelLogoIMB", ci);
        }
    }
}
