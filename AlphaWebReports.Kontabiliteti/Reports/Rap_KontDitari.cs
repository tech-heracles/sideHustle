using System;
using System.Globalization;
using System.Resources;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs
{
    public partial class Rap_KontDitari : DevExpress.XtraReports.UI.XtraReport
    {
        double shumadebi = 0;
        double shumakredi = 0;
        
        public Rap_KontDitari()
        {
            InitializeComponent();
        }

        public Rap_KontDitari(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi,param.ScopeID, report)
        {
            
        }
        public Rap_KontDitari(CultureInfo ci,int idNdermarrje, int idViti, int idPerdoruesi,string scopeID, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
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
            xrLabel30.Text = raport.Parameters[7].Description;
            parameter8.Value = raport.Parameters[7].Value;
            azhornimLabel.Text = raport.Parameters[8].Description;
            Azhornim.Value = raport.Parameters[8].Value;
            xrLabel33.Text = raport.Parameters[9].Description;
            parameter9.Value = raport.Parameters[9].Value;
            EmrateLabelave(ci);
           
        }

        private void xrLabel20_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (shumadebi - shumakredi > 0)
                e.Result = (shumadebi - shumakredi).ToString("#,#.00");
            else
                e.Result = "";
            e.Handled = true;
        }
        private void xrLabel29_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (shumadebi - shumakredi < 0)
                e.Result = (shumakredi-shumadebi).ToString("#,#.00");
            else
                e.Result = "";
            e.Handled = true;
        }

        private void xrLabel25_AfterPrint(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("NRLLOGARI") != null)
                shumadebi = Convert.ToDouble(xrLabel25.Summary.GetResult());
        }

        private void xrLabel26_AfterPrint_1(object sender, EventArgs e)
        {
            if (GetCurrentColumnValue("NRLLOGARI") != null)
                shumakredi = Convert.ToDouble(xrLabel26.Summary.GetResult());
        }

        private void xrLabel20_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (shumadebi - shumakredi >=0)
            //    xrLabel20.Text = String.Format("{0:#,#.00}", shumadebi - shumakredi);
            //else
            //    xrLabel20.Text = String.Format("{0:#,#.00}", 0);
        }

        private void xrLabel29_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (shumadebi - shumakredi <=0)
            //    xrLabel29.Text = String.Format("{0:#,#.00}", shumakredi - shumadebi);
            //else
            //    xrLabel29.Text = String.Format("{0:#,#.00}", 0);
        }

        private void xrLabel5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("KODKONFIGAMBJENTE") != null && GetCurrentColumnValue("KODKONFIGAMBJENTE").ToString() == "NKM")
            {
                xrLabel5.NavigateUrl = "";
                this.xrLabel5.ForeColor = System.Drawing.Color.Black;
                return;
            }

            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);

            xrLabel5.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_FleteKontabel.aspx?scopeID=" + parametraRaporti.ScopeID + "&newScopeId=True&shtim_modifikim=modifikim&id=" + GetCurrentColumnValue("IDKOKAFLETEKONTABEL") + "&numur=" + GetCurrentColumnValue("NRKOKAFLETEKONTABEL") + "')";
               this.xrLabel5.ForeColor = System.Drawing.Color.SteelBlue;
                xrLabel5.Target = "_self";

            
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }
      
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        
        private void EmrateLabelave(CultureInfo ci)
        {
            
          ResourceManager  rm = new ResourceManager("Resources.Strings",
                     System.Reflection.Assembly.Load("App_GlobalResources"));

          xrLabel12.Text = rm.GetString("RaportDitariIKontabilitetitTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel10.Text = rm.GetString("labelNrRef", ci);
            xrLabel11.Text = rm.GetString("FilterDateRegjistrimi", ci);
            xrLabel19.Text = rm.GetString("labelRaportiLloji", ci);
            xrLabel18.Text = rm.GetString("labelRaportiNrDok", ci);
            xrLabel17.Text = rm.GetString("labelRaportiDtDok", ci);
            xrLabel16.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel15.Text = rm.GetString("labelRaportiNrLlogari", ci);
            xrLabel14.Text = rm.GetString("labelRaportiVleftaDebi", ci);
            xrLabel13.Text = rm.GetString("labelRaportiVleftaKredi", ci);
            xrLabel23.Text = rm.GetString("labelRaportiTotaliVeprimeve", ci);
            xrLabel24.Text = rm.GetString("labelRaportiDiferenca", ci);
            xrLabel32.Text = rm.GetString("labelLogoIMB", ci);
        }

        private void Rap_KontDitari_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);

        }
    }
}
