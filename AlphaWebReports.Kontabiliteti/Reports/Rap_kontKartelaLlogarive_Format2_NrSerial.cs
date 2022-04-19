using System;
using System.Globalization;
using System.Resources;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs
{
    public partial class Rap_kontKartelaLlogarive_Format2_NrSerial : DevExpress.XtraReports.UI.XtraReport
    {
        double vleraProgresive = 0;
        double vleraProgresive2 = 0;
        double vleraProgresivGjithsej = 0;
        double vleraProgresivGjithsej2 = 0;
        double gjendjamepare = 0;
        double shumadebi = 0;
        double shumakredi = 0;
        double shumakredimonllog = 0;
        double shumadebimonllog = 0;
        double shumadebigjithsej = 0;
        double shumakredigjithsej = 0;
        double shumadebimonlloggjithsej = 0;
        double shumakredimonlloggjithsej = 0;
        int cnt = 0;
      
      

        public Rap_kontKartelaLlogarive_Format2_NrSerial(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_kontKartelaLlogarive_Format2_NrSerial(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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
            parameter8.Value = raport.Parameters[7].Value;
            Azhornim.Value = raport.Parameters[8].Value;
            parameter9.Value = raport.Parameters[9].Value;
            parameterIdNderm.Value = idNdermarrje;
            
            vendosFormatNumrash(xrTableCell45, xrTableCell47, xrTableCell49, xrTableCell50, xrTableCell13, xrTableCell12, xrTableCell5, xrTableCell3, xrTableCell60, xrTableCell61, xrTableCell68, xrTableCell62, xrTableCell48,
                xrTableCell52, xrTableCell70, xrTableCell54, xrTableCell65, xrTableCell66, xrTableCell69, xrTableCell67, xrTableCell55, xrTableCell56,KMSHTotali,VleraMeZbritjeTotali);
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


            xrLabel17.Text = rm.GetString("RaportKartelaLlogariveTitulliformat2", ci);
            xrLabel2.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell27.Text = rm.GetString("labelNrRef", ci);
            xrTableCell29.Text = rm.GetString("FilterDateRegjistrimi", ci);
            xrTableCell28.Text = rm.GetString("labelRaportiLloji", ci);
            xrTableCell30.Text = rm.GetString("labelRaportiNrDok", ci);
            xrTableCell32.Text = rm.GetString("labelRaportiDtDok", ci);
            xrTableCell24.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell34.Text = rm.GetString("labelRaportiKredi", ci);
            xrTableCell36.Text = rm.GetString("labelRaportiDebi", ci);
            xrTableCell25.Text = rm.GetString("labelRaportiGjendMonBaze", ci);
            xrTableCell26.Text = rm.GetString("labelRaportiGjendMonLlog", ci);
            xrTableCell37.Text = rm.GetString("labelRaportiDebi", ci);
            xrTableCell35.Text = rm.GetString("labelRaportiKredi", ci);           
            xrTableCell31.Text = rm.GetString("labelRaportNrSerial", ci);
        }

           
    }
}