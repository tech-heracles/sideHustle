using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.QendraKosto.Raportet
{
    public partial class Rap_QendraKosto_NdryshimetBuxhetore : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_QendraKosto_NdryshimetBuxhetore(){InitializeComponent();} 
        private double currentBudget_1;
        private double newBudget_1;
        private double previousBudget_2;
        private double currentBudget_2;
        private string currentCode, newCode, newEmertimi, newMuaj, currentMuaj;
        private double ndryshimiNeVlere;

        public Rap_QendraKosto_NdryshimetBuxhetore(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, report)
        {

        }
        public Rap_QendraKosto_NdryshimetBuxhetore(CultureInfo ci, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            xrLabel1.Text = raport.Parameters["filterMuaji"].Description;
            parameter1.Value = raport.Parameters["filterMuaji"].Value;
            xrLabel3.Text = raport.Parameters["filterQK"].Description;
            parameter2.Value = raport.Parameters["filterQK"].Value;
            EmrateLabelave(ci);
            currentCode = String.Empty;
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel13.Text = rm.GetString("TitullRaportiNdryshimeBuxhetore", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel36.Text = rm.GetString("labelRaportNr", ci);
            xrLabel37.Text = rm.GetString("labelRaportiEmertimi", ci);
            xrLabel50.Text = rm.GetString("labelFilterKryesorMuaji", ci);
            xrLabel38.Text = rm.GetString("lblBuxhetiFillestarIMiratuar", ci);
            xrLabel39.Text = rm.GetString("lblNdryshimetNeVlere", ci);

            xrLabel47.Text = rm.GetString("lblNdryshimetNePerqindje", ci);
            xrLabel35.Text = rm.GetString("lblBuxhetiProgresiv", ci);
            xrLabel34.Text = rm.GetString("filterRaportShenimeShitje", ci);
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KODI") == null)
                return;
            newCode = GetCurrentColumnValue("KODI").ToString();
            newEmertimi = GetCurrentColumnValue("PERSHKRIMI").ToString();
            newMuaj = GetCurrentColumnValue("MUAJ").ToString();
            newBudget_1 = Convert.ToDouble(GetCurrentColumnValue("BUXHETI_1").ToString());
            currentBudget_2 = Convert.ToDouble(GetCurrentColumnValue("BUXHETI_2").ToString());

            bool qender_E_re = (currentCode == String.Empty || currentCode != newCode);

            if (qender_E_re)
                switchBorder(true, xrLabel12, xrLabel26, xrLabel27, xrLabel28, xrLabel29, xrLabel30, xrLabel31, xrLabel33, xrLabel46);
            else
                switchBorder(false, xrLabel12, xrLabel26, xrLabel27, xrLabel28, xrLabel29, xrLabel30, xrLabel31, xrLabel33, xrLabel46);

            if (qender_E_re || currentMuaj != newMuaj)
            {
                currentCode = newCode;
                previousBudget_2 = currentBudget_2;
                currentBudget_1 = newBudget_1;
                currentMuaj = newMuaj; 
                vendosVlerat(currentCode, newEmertimi, newMuaj, currentBudget_1, currentBudget_2 - currentBudget_1,(currentBudget_1 == 0 || currentBudget_2 == 0) ?  0 : (currentBudget_2 - currentBudget_1) * 100 / currentBudget_1);
            }else
            {   
                ndryshimiNeVlere = currentBudget_2 - previousBudget_2;
                if (ndryshimiNeVlere != 0)
                {
                    vendosVlerat("", "", "", currentBudget_1, ndryshimiNeVlere, (previousBudget_2 == 0) ? 0 : ndryshimiNeVlere * 100 / currentBudget_1);
                    previousBudget_2 = currentBudget_2;
                }else
                {
                    e.Cancel = true;
                }
            }

            
        }
        
       
        /// <param name="labels"></param>
        /// <param name="set">true per te shtuar borderin dhe false per tja hequr</param>
        private void switchBorder(bool set,params XRLabel[] labels)
        {
            foreach(XRLabel label in labels)
            {
                label.Borders = (set) ? DevExpress.XtraPrinting.BorderSide.Top : DevExpress.XtraPrinting.BorderSide.None;
            }
        }

        private void vendosVlerat(string kodi, string emertimi, string muaji, double buxheti_1, double ndryshimiVlere, double ndryshimiPerqindje)
        {
            xrLabel26.Text = kodi;
            xrLabel27.Text = emertimi;
            xrLabel46.Text = muaji;
            xrLabel28.Text = String.Format("{0:N2}", buxheti_1);
            xrLabel29.Text = String.Format("{0:N2}", ndryshimiVlere);
            xrLabel30.Text = String.Format("{0:N2}", ndryshimiPerqindje);
        }
    }
}
