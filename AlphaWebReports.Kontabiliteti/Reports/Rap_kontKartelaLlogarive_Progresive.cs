using System;
using System.Globalization;
using System.Resources;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs
{
    public partial class Rap_kontKartelaLlogarive_Format2_Progresive : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_kontKartelaLlogarive_Format2_Progresive(){InitializeComponent();} 



        public Rap_kontKartelaLlogarive_Format2_Progresive(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_kontKartelaLlogarive_Format2_Progresive(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            xrLabel54.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel55.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            xrLabel56.Text = raport.Parameters[2].Description;
            parameter3.Value = raport.Parameters[2].Value;
            xrLabel57.Text = raport.Parameters[3].Description;
            parameter4.Value = raport.Parameters[3].Value;
            xrLabel58.Text = raport.Parameters[4].Description;
            parameter5.Value = raport.Parameters[4].Value;
            xrLabel59.Text = raport.Parameters[5].Description;
            parameter6.Value = raport.Parameters[5].Value;
            xrLabel60.Text = raport.Parameters[6].Description;
            parameter7.Value = raport.Parameters[6].Value;
            xrLabel42.Text = raport.Parameters[7].Description;
            parameter8.Value = raport.Parameters[7].Value;
            azhornimLabel.Text = raport.Parameters[8].Description;
            Azhornim.Value = raport.Parameters[8].Value;
            xrLabel69.Text = raport.Parameters[9].Description;
            parameter9.Value = raport.Parameters[9].Value;

            vendosFormatNumrash( xrLabel36, xrLabel37, xrLabel39, xrLabel40, xrLabel67, xrLabel66, xrLabel34, xrLabel33, xrLabel90, xrLabel91);
        }

        private void vendosFormatNumrash(params XRLabel[] labels )
        {
            foreach (XRLabel label in labels)
            {
                label.XlsxFormatString = "#,##0.00";
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


            xrLabel17.Text = rm.GetString("RaportKartelaLlogariveProgresiveTitulli", ci);
            xrLabel23.Text = rm.GetString("RaportKartelaLlogariveProgresiveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel18.Text = rm.GetString("labelNrRef", ci);
            xrLabel11.Text = rm.GetString("FilterDateRegjistrimi", ci);
            xrLabel20.Text = rm.GetString("labelRaportiLloji", ci);
            xrLabel14.Text = rm.GetString("labelRaportiNrDok", ci);
            xrLabel21.Text = rm.GetString("labelRaportiDtDok", ci);
            xrLabel22.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel12.Text = rm.GetString("labelRaportiKredi", ci);
            xrLabel13.Text = rm.GetString("labelRaportiDebi", ci);
            xrLabel15.Text = rm.GetString("labelRaportiGjendMonBaze", ci);
            xrLabel10.Text = rm.GetString("labelRaportiGjendMonLlog", ci);
            xrLabel19.Text = rm.GetString("labelRaportiDebi", ci);
            xrLabel24.Text = rm.GetString("labelRaportiKredi", ci);
            xrLabel51.Text = rm.GetString("labelRaportiGjendjaMePare", ci);
            xrLabel28.Text = rm.GetString("lblLevizja", ci);
            xrLabel38.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
            xrLabel35.Text = rm.GetString("lblLevizjaGjithsej", ci);
            xrLabel65.Text = rm.GetString("labelRaportiGjendjaGjithsej", ci);
            xrLabel64.Text = rm.GetString("labelLogoIMB", ci);

            //report header
            xrLabel71.Text = rm.GetString("labelNrRef", ci);
            xrLabel80.Text = rm.GetString("FilterDateRegjistrimi", ci);
            xrLabel82.Text = rm.GetString("labelRaportiLloji", ci);
            xrLabel72.Text = rm.GetString("labelRaportiNrDok", ci);
            xrLabel76.Text = rm.GetString("labelRaportiDtDok", ci);
            xrLabel77.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel79.Text = rm.GetString("labelRaportiKredi", ci);
            xrLabel73.Text = rm.GetString("labelRaportiDebi", ci);
            xrLabel78.Text = rm.GetString("labelRaportiGjendMonBaze", ci);
            xrLabel81.Text = rm.GetString("labelRaportiGjendMonLlog", ci);
            xrLabel74.Text = rm.GetString("labelRaportiDebi", ci);
            xrLabel75.Text = rm.GetString("labelRaportiKredi", ci);
            xrLabel89.Text = rm.GetString("labelRaportiShuma", ci);
            xrLabel1.Text = rm.GetString("labelRaportiGjendProgresiveMonBaze", ci);
            xrLabel5.Text = rm.GetString("labelRaportiGjendProgresiveMonBaze", ci);
            xrLabel2.Text = rm.GetString("labelRaportiDebi", ci);
            xrLabel3.Text = rm.GetString("labelRaportiKredi", ci);
            xrLabel7.Text = rm.GetString("labelRaportiDebi", ci);
            xrLabel8.Text = rm.GetString("labelRaportiKredi", ci);

        }

    
    }
}
