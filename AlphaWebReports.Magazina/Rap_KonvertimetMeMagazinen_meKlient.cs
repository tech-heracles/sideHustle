using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_KonvertimetMeMagazinen_meKlient : DevExpress.XtraReports.UI.XtraReport
    {

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

        public Rap_KonvertimetMeMagazinen_meKlient()
        {
            InitializeComponent();
        }

        public Rap_KonvertimetMeMagazinen_meKlient(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_KonvertimetMeMagazinen_meKlient(CultureInfo ci, int idNdermarja, int idViti, int Idperdorues, XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            ResourceManager rm = new ResourceManager("Resources.Strings",
                           System.Reflection.Assembly.Load("App_GlobalResources"));
            Ndermarja.Value = raport.Parameters[0].Value;
            LlojDok.Value = raport.Parameters[1].Value;
            Kartela.Value = raport.Parameters[3].Value;
            DtDok.Value = raport.Parameters[4].Value;
            parameter4.Value = raport.Parameters[2].Value;
            parameter5.Value = raport.Parameters[5].Value;

        }
        private void xrLabel32_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            //Kontrollojme nese artikulli eshte konvertuar ose jo ne dok te tjere
            if (GetCurrentColumnValue("IDKOKAM") != System.DBNull.Value && GetCurrentColumnValue("IDKOKAM") != null)
            {

                string kokaID = GetCurrentColumnValue("IDKOKAF").ToString();
                string artID = GetCurrentColumnValue("IDARTIKULLI").ToString();
                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + kokaID + ";" + artID + ";126')";

                if (SkippedDetailBands.Contains(kokaID + ";" + artID) && (bool)SkippedDetailBands[kokaID + ";" + artID] == false)
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
                else e.Cancel = true;
            }
        }
        private void xrLabel31_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //Kontrollojme nese raporti ka ose jo te dhena
            if (GetCurrentColumnValue("Ngjyra") != null)
            {
                string ngjyra = GetCurrentColumnValue("Ngjyra").ToString();
                switch (ngjyra)
                {
                    case "gjelber":
                        xrLabel31.BackColor = Color.Green;
                        break;
                    case "verdhe":
                        xrLabel31.BackColor = Color.Orange;
                        break;
                    case "gri":
                        xrLabel31.BackColor = Color.Gray;
                        break;
                    case "kuqe":
                        xrLabel31.BackColor = Color.Red;
                        break;
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
            xrLabel12.Text = rm.GetString("RaportKonvertimetEMagazinesTitulli", ci);
            xrLabel5.Text = rm.GetString("labelRAportiDokKryesor", ci);
            xrLabel7.Text = rm.GetString("labelRaportiKonvertimet", ci);
            xrLabel4.Text = rm.GetString("labelRaportiDiferenca", ci);
            xrLabel9.Text = rm.GetString("labelLlojDokumenti", ci);
            xrLabel10.Text = rm.GetString("labelRaportiSubjekti", ci);
            xrLabel11.Text = rm.GetString("lblRaportMagazina", ci);
            xrLabel13.Text = rm.GetString("labelRaportiNrDok", ci);
            xrLabel14.Text = rm.GetString("labelRaportiDtDok", ci);
            xrLabel15.Text = rm.GetString("labelRaportiArtikull", ci);
            xrLabel16.Text = rm.GetString("labelSasia", ci);
            xrLabel1.Text = rm.GetString("labelSasiaKonvertuar", ci);
            xrLabel6.Text = rm.GetString("labelRaportiDiferenca", ci);
            xrLabel29.Text = rm.GetString("labelRaportKonvertuar", ci);
            xrLabel42.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel26.Text = rm.GetString("lblKodiKlient", ci);
        }

       
    }
}