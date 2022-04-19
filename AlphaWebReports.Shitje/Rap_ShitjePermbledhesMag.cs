using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_ShitjePermbledhesMag : DevExpress.XtraReports.UI.XtraReport
    {        
		public Rap_ShitjePermbledhesMag(){InitializeComponent();} 

        private string monedha;
        private bool ndryshuar = false;
        public Rap_ShitjePermbledhesMag(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.ScopeID,  report)
        {

        }
        
        public Rap_ShitjePermbledhesMag(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, string scopeID, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            xrLabel10.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel19.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            xrLabel28.Text = raport.Parameters[2].Description;
            parameter3.Value = raport.Parameters[2].Value;
            xrLabel12.Text = raport.Parameters[3].Description;
            parameter4.Value = raport.Parameters[3].Value;
            xrLabel20.Text = raport.Parameters[4].Description;
            parameter5.Value = raport.Parameters[4].Value;
            xrLabel21.Text = raport.Parameters[5].Description;
            parameter6.Value = raport.Parameters[5].Value;
            xrLabel15.Text = raport.Parameters[6].Description;
            parameter7.Value = raport.Parameters[6].Value;  
            xrLabel51.Text = raport.Parameters[7].Description;
            parameter8.Value = raport.Parameters[7].Value;  
            xrLabel53.Text = raport.Parameters[8].Description;
            parameter9.Value = raport.Parameters[8].Value;
            xrLabel55.Text = raport.Parameters[9].Description;
            parameter10.Value = raport.Parameters[9].Value;  
            xrLabel57.Text = raport.Parameters[10].Description;
            parameter11.Value = raport.Parameters[10].Value;  
            xrLabel59.Text = raport.Parameters[12].Description;
            parameter12.Value = raport.Parameters[12].Value;  
            xrLabel61.Text = raport.Parameters[13].Description;
            parameter13.Value = raport.Parameters[13].Value;  
            xrLabel64.Text = raport.Parameters[14].Description;
            parameter14.Value = raport.Parameters[14].Value;
            degaAdminLabel.Text = raport.Parameters[11].Description;
            DegaAdministrative.Value = raport.Parameters[11].Value;
            xrLabel65.Text = raport.Parameters[15].Description;
            parameter15.Value = raport.Parameters[15].Value;
            adrFaturimit.Text = raport.Parameters[20].Description;
            adresaFaturimit.Value = raport.Parameters[20].Value;
            adrKlientit.Text = raport.Parameters[16].Description;
            parameter16.Value = raport.Parameters[16].Value;
            xrLabel85.Text = raport.Parameters["filterKodbari"].Description;
            parameter17.Value = raport.Parameters["filterKodbari"].Value;
        }

        private void xrLabel7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);


            if ( GetCurrentColumnValue("IDSHITJEKOKA") != null && GetCurrentColumnValue("NRDOK") != null)
            {
                xrLabel7.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_RegjistrimDokumentash.aspx?scopeID=" + parametraRaporti.ScopeID + "&newScopeId=True&shitje_blerje=shitje&id=" + GetCurrentColumnValue("IDSHITJEKOKA").ToString() + "&numer=" + GetCurrentColumnValue("NRDOK").ToString() + "&shtim_modifikim=modifikim')";
                this.xrLabel7.ForeColor = Color.SteelBlue;
                xrLabel7.Target = "_self";

            }

            // cnt++;
            // xrLabel7.Text = cnt.ToString();
        }

        private void xrLabel36_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (ndryshuar == true)
            //    xrLabel36.Text = "";
            //else
            //    xrLabel36.Text = String.Format("{0:#,#.00}", shumaNenTotal);
        }

        private void xrLabel16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("MONEDHAKOD") != null)
            {
                if (ndryshuar == false)
                {
                    if (monedha == null || monedha == "")
                        monedha = GetCurrentColumnValue("MONEDHAKOD").ToString();
                    else
                    {
                        if (monedha == GetCurrentColumnValue("MONEDHAKOD").ToString())
                            ndryshuar = false;
                        else
                            ndryshuar = true;
                    }
                }
            }
        }

        private void xrLabel26_AfterPrint(object sender, EventArgs e)
        {            
            //if (GetCurrentColumnValue("NRDOK") != null)
            //{
            //    shumaMonBazeTVSH = shumaMonBazeTVSH + (Convert.ToDouble(GetCurrentColumnValue("TVSH").ToString()) * Convert.ToDouble(GetCurrentColumnValue("KURSI").ToString()));
            //}
        }

        private void xrLabel38_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (ndryshuar == true)
            //    xrLabel38.Text = "";
            //else
            //    xrLabel38.Text = String.Format("{0:#,#.00}", shumaTVSH);
        }

        private void xrLabel40_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (GetCurrentColumnValue("NRDOK") != null)
            //{
            //    xrLabel40.Text = String.Format("{0:#,#.00}", shumaMonBazeTVSH);
            //}
        }

        private void xrLabel37_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        { 
        //    if (ndryshuar == true)
        //        xrLabel37.Text = "";
        //    else
        //        xrLabel37.Text = String.Format("{0:#,#.00}", shumaZbritje);
        }

        private void xrLabel13_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //for (int i = 0; i < e.CalculatedValues.Count; i++)
            //{
            //    if (GetCurrentColumnValue("NRDOK") != null)
            //    {
            //        shumaNenTotal = shumaNenTotal + Convert.ToDouble(e.CalculatedValues[i]);
            //    }
            //}
        }

        private void xrLabel8_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //for (int i = 0; i < e.CalculatedValues.Count; i++)
            //{
            //    if (GetCurrentColumnValue("NRDOK") != null)
            //    {
            //        shumaZbritje = shumaZbritje + Convert.ToDouble(e.CalculatedValues[i]);
            //    }
            //}
        }

        private void xrLabel3_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //for (int i = 0; i < e.CalculatedValues.Count; i++)
            //{
            //    if (GetCurrentColumnValue("NRDOK") != null)
            //    {
            //        shumaTotal = shumaTotal + Convert.ToDouble(e.CalculatedValues[i]);
            //    }
            //}
        }

        private void xrLabel39_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (ndryshuar == true)
            //    xrLabel39.Text = "";
            //else
            //    xrLabel39.Text = String.Format("{0:#,#.00}", shumaTotal);
        }

        private void xrLabel2_AfterPrint(object sender, EventArgs e)
        {
            //if (GetCurrentColumnValue("NRDOK") != null)
            //{
            //    shumaTVSH = shumaTVSH + Convert.ToDouble(GetCurrentColumnValue("TVSH").ToString());
            //}
        }
        private void TotalLabels_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
            if (ndryshuar == true)
            {
                e.Text = "";
            }
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.PrintingSystem.Document.PageCount == 0)
            {
                e.Cancel = true;
                return;
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
           
            xrLabel17.Text = rm.GetString("RaportRegjistriPermbledhesShitjeveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel31.Text = rm.GetString("labelNrRendor", ci);
            xrLabel45.Text = rm.GetString("labelRaportiLloji", ci);
            xrLabel27.Text = rm.GetString("labelDokumenti", ci);
            xrLabel29.Text = rm.GetString("labelMonedhaFature", ci);
            xrLabel33.Text = rm.GetString("labelMonedhaBaze", ci);
            xrLabel1.Text = rm.GetString("labelRaportiNr", ci);
            xrLabel4.Text = rm.GetString("labelRaportiDtDok", ci);
            xrLabel9.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrLabel5.Text = rm.GetString("labelKursi", ci);
           
            xrLabel14.Text = rm.GetString("labelNentotal", ci);
            xrLabel22.Text = rm.GetString("labelZbritje", ci);
            xrLabel23.Text = rm.GetString("labelTVSH", ci);
            xrLabel24.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel35.Text = rm.GetString("labelTVSH", ci);
            xrLabel34.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel42.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel50.Text = rm.GetString("labelLogoIMB", ci);

            //report header
            xrLabel84.Text = rm.GetString("RaportRegjistriPermbledhesShitjeveTitulli", ci);
            xrLabel77.Text = rm.GetString("labelNrRendor", ci);
            xrLabel69.Text = rm.GetString("labelRaportiLloji", ci);
            xrLabel71.Text = rm.GetString("labelDokumenti", ci);
            xrLabel82.Text = rm.GetString("labelMonedhaFature", ci);
            xrLabel74.Text = rm.GetString("labelMonedhaBaze", ci);
            xrLabel78.Text = rm.GetString("labelRaportiNr", ci);
            xrLabel81.Text = rm.GetString("labelRaportiDtDok", ci);
            xrLabel76.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrLabel83.Text = rm.GetString("labelKursi", ci);
            xrLabel80.Text = rm.GetString("labelNentotal", ci);
            xrLabel72.Text = rm.GetString("labelZbritje", ci);
            xrLabel73.Text = rm.GetString("labelTVSH", ci);
            xrLabel70.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel79.Text = rm.GetString("labelTVSH", ci);
            xrLabel75.Text = rm.GetString("labelRaportiTotali", ci);
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {           
        }

        private void Rap_ShitjePermbledhesMag_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox2.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);

        }
    }
}
