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
    public partial class Rap_ProcedimProdhimi : DevExpress.XtraReports.UI.XtraReport
    {

        private Hashtable skippedDetailBands;
    
        double sasia = 0;
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

        public Rap_ProcedimProdhimi()
        {
            InitializeComponent();
        }
        public Rap_ProcedimProdhimi(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

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


        public Rap_ProcedimProdhimi(CultureInfo ci, int idNdermarja, int idViti, int Idperdorues, XtraReport raport)
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
            parameter3.Value = raport.Parameters[5].Value;
            parameter5.Value = raport.Parameters[6].Value;
            parameter6.Value = raport.Parameters[8].Value;

        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel12.Text = rm.GetString("RaportProcedimProdhimiTitulli", ci);
            xrLabel5.Text = rm.GetString("labelRAportiDokKryesor", ci);
            xrLabel7.Text = rm.GetString("labelRaportiKonvertimet", ci);
            xrLabel4.Text = rm.GetString("labelRaportiDiferenca", ci);
            xrLabel13.Text = rm.GetString("labelRaportiNrDok", ci);
            xrLabel28.Text = rm.GetString("labelShitesi", ci);
            xrLabel14.Text = rm.GetString("labelRaportiDtDok", ci);
            xrLabel2.Text = rm.GetString("labelAfatKohor", ci);
            xrLabel10.Text = rm.GetString("labelRaportiSubjekti", ci);
            xrLabel11.Text = rm.GetString("labelFilterAvancuarMagazina", ci);
            xrLabel15.Text = rm.GetString("labelRaportiArtikull", ci);
            xrLabel27.Text = rm.GetString("labelGjendjaMagazines", ci);
            xrLabel16.Text = rm.GetString("labelSasia", ci);
            xrLabel1.Text = rm.GetString("MenuItemRaportProdhimi", ci);
            xrLabel6.Text = rm.GetString("labelRaportiDiferenca", ci);
            xrLabel29.Text = rm.GetString("lblRaportStatusi", ci);
            xrLabel42.Text = rm.GetString("labelLogoIMB", ci);
        }
    }
}