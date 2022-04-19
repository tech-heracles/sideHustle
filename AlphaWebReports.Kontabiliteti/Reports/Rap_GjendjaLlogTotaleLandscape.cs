using System;
using System.Collections;
using System.Globalization;
using System.Resources;
using AlphaWebReports.Common;
using DevExpress.XtraReports.UI;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs
{
    public partial class Rap_GjendjaLlogTotaleLandscape : DevExpress.XtraReports.UI.XtraReport,  IUpdateDetail
    {
		public Rap_GjendjaLlogTotaleLandscape(){InitializeComponent();} 
       
        double debimonbaze = 0.0;
        double kredimonbaze = 0.0;
        double grandtotdebi = 0.0;
        double grandtotkredi = 0.0;
        private static string formatstring = "{0:#,#.00}";
        string windowWidth = "";
        

        public Rap_GjendjaLlogTotaleLandscape(ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti,param.ScopeID, report)
        {

        }
        public Rap_GjendjaLlogTotaleLandscape(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti,string scopeID, DevExpress.XtraReports.UI.XtraReport raport)
        {
          
            InitializeComponent();
            EmrateLabelave(ci);
            xrLabel54.Text = "Deri me:";// raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value.ToString().Split('-')[1];
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
            xrLabel7.DataBindings[0].FormatString = formatstring;
            xrLabel18.Text = raport.Parameters[7].Description;
            parameter8.Value = raport.Parameters[7].Value;
            azhornimLabel.Text = raport.Parameters[8].Description;
            Azhornim.Value = raport.Parameters[8].Value;
            xrLabel25.Text = raport.Parameters[10].Description;
            parameter9.Value = raport.Parameters[10].Value;
            xrLabel45.Text = raport.Parameters["filterLlogariSintetike"].Description;

            if ((raport.Parameters["filterLlogariSintetike"].Value).ToString() == "Po")
            {
                xrLabel46.Text = "Po";
                this.sintetike.Value = true;
            }
            else
            {
                xrLabel46.Text = "Jo";
                this.sintetike.Value = false;
            }
            windowWidth = Convert.ToString((object) raport.Parameters["windowWidth"].Value);
        }

        private void xrLabel14_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
                debimonbaze = Convert.ToDouble(xrLabel14.Summary.GetResult());
        }

        private void xrLabel16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            kredimonbaze = Convert.ToDouble(xrLabel16.Summary.GetResult());
        }

        private void xrLabel36_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (debimonbaze - kredimonbaze >= 0)
                xrLabel36.Text = (debimonbaze - kredimonbaze).ToString("N2");
            else
                xrLabel36.Text = 0.ToString("N2");
        }

        private void xrLabel37_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (debimonbaze - kredimonbaze <= 0)
                xrLabel37.Text = (kredimonbaze - debimonbaze).ToString("N2");
            else
                xrLabel37.Text = 0.ToString("N2");
        }

        private void xrLabel4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);

            xrLabel4.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('RaportiShpejte.aspx?scopeID=" + parametraRaporti.ScopeID + "&guidString=" + parametraRaporti.GuidString + "&idraporti=" + parametraRaporti.IdSubRaporti + "&filterNumerLlogarie=" + GetCurrentColumnValue("NRLLOGARI") + "&printo=0&Sesioni=true')";
                xrLabel4.Target = "_self";
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
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel17.Text = rm.GetString("RaportGjendjaLlogariveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel15.Text = rm.GetString("labelRaportiGjendMonBaze", ci);
            xrLabel2.Text = rm.GetString("labelRaportiGjendMonLlog", ci);
            xrLabel12.Text = rm.GetString("labelRaportiKredi", ci);
            xrLabel3.Text = rm.GetString("labelRaportiDebi", ci);
            xrLabel1.Text = rm.GetString("labelRaportiKredi", ci);
            xrLabel11.Text = rm.GetString("labelRaportiDebi", ci);
            xrLabel8.Text = rm.GetString("labelRaportiNrLlogari", ci);
            xrLabel27.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel9.Text = rm.GetString("filterRaportEmerLlogarie", ci);
            xrLabel10.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrLabel19.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel38.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
        }

        private void xrLabel5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //lblKPF_BeforePrint
            XRLabel label = sender as XRLabel;
            string catid = "";
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);

            if (GetCurrentColumnValue("NRLLOGARI") != null) catid = GetCurrentColumnValue("NRLLOGARI").ToString();


            if (GetCurrentColumnValue("NDERMARJEKODI") == null || GetCurrentColumnValue("NDERMARJEKODI").ToString() == "" || GetCurrentColumnValue("NDERMARJEKODI").ToString() == parametraRaporti.NdermarrjePershkrimi)
            {
                label.Text = "";
            }

            else
            {


                label.Target = "_self"; label.NavigateUrl = "javascript:window.ASPxCallbackPanel1.PerformCallback('" + catid + ";Detail;gjendjaLlogarive;H')";
                if (!SkippedDetailBands.Contains(catid))
                    label.Text = "+";
                else if ((bool)SkippedDetailBands[catid] == false)
                    label.Text = "-";
                else
                    label.Text = "+";
            }

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
            string catid = GetCurrentColumnValue("NRLLOGARI") == null ? "" : GetCurrentColumnValue("NRLLOGARI").ToString();



            if (SkippedDetailBands.Contains(catid))
                e.Cancel = Convert.ToBoolean(SkippedDetailBands[catid]);
            else
            {
                e.Cancel = true; SkippedDetailBands.Add(catid, true);
            }
        }
        double shumaDebi = 0;
        double shumaKredi = 0;
        private void xrLabel31_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            string catid = "";
            if (GetCurrentColumnValue("NRLLOGARI") != null)
                catid = GetCurrentColumnValue("NRLLOGARI").ToString();
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
            {
                string shumaFormatuar = (Convert.ToDouble(e.CalculatedValues[i])).ToString("N2");
                shuma += double.Parse(shumaFormatuar);
            }
            if (shuma >= 0)
            {
                if (!((bool)SkippedDetailBands[catid] == false))
                    shumaDebi += shuma;
                e.Result = shuma;
            }
            else
                e.Result = 0.00;
            e.Handled = true;
        }

        private void xrLabel32_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            string catid = "";
            if (GetCurrentColumnValue("NRLLOGARI") != null)
                catid = GetCurrentColumnValue("NRLLOGARI").ToString();
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
            {
                string shumaFormatuar = (Convert.ToDouble(e.CalculatedValues[i])).ToString("N2");
                shuma += double.Parse(shumaFormatuar);
            }
            if (shuma >= 0)
            {
                if(!((bool)SkippedDetailBands[catid] == false))
                    shumaKredi += shuma;
                e.Result = shuma;
            }
            else e.Result = 0.00;
            e.Handled = true;
        }

        private void xrLabel28_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {

            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
            {
                string shumaFormatuar = (Convert.ToDouble(e.CalculatedValues[i])).ToString("N2");
                shuma += double.Parse(shumaFormatuar);
            }
            if (shuma >= 0)
                e.Result = shuma;
            else
                e.Result = 0.00;
            e.Handled = true;
        }

        private void xrLabel34_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {

            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
            {
                string shumaFormatuar = (Convert.ToDouble(e.CalculatedValues[i])).ToString("N2");
                shuma += double.Parse(shumaFormatuar);
            }
            if (shuma >= 0)
                e.Result = shuma;
            else e.Result = 0.00;
            e.Handled = true;
        }

        private void xrLabel41_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
            {
                string shumaFormatuar = (Convert.ToDouble(e.CalculatedValues[i])).ToString("N2");
                shuma += double.Parse(shumaFormatuar);
            }
            if (shuma >= 0)
                e.Result = shuma;
            else e.Result = 0.00;
            e.Handled = true;
        }

        private void xrLabel42_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {

            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
            {
                string shumaFormatuar = (Convert.ToDouble(e.CalculatedValues[i])).ToString("N2");
                shuma += double.Parse(shumaFormatuar);
            }
            if (shuma >= 0)
                e.Result = shuma;
            else e.Result = 0.00;
            e.Handled = true;
        }

        private void xrLabel43_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
            {
                string shumaFormatuar = (Convert.ToDouble(e.CalculatedValues[i])).ToString("N2");
                shuma += double.Parse(shumaFormatuar);
            }
            if (shuma >= 0)
                e.Result = shuma;
            else e.Result = 0.00;
            e.Handled = true;
        }

        private void xrLabel44_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            double shuma = 0;
            for (int i = 0; i < e.CalculatedValues.Count; i++)
            {
                string shumaFormatuar = (Convert.ToDouble(e.CalculatedValues[i])).ToString("N2");
                shuma += double.Parse(shumaFormatuar);
            }
            if (shuma >= 0)
                e.Result = shuma;
            else e.Result = 0.00;
            e.Handled = true;
        }

        double levizjaDebitore = 0;
        double gjendjedebiparaDebitore = 0;
        double gjendjeKrediParaDebitore = 0;

        double levizjaKreditore = 0;
        double gjendjedebiparaKreditore = 0;
        double gjendjeKrediParaKreditore = 0;

        double levizjaNeGrup = 0;
        double gjendjedebiparaNeGrup = 0;
        double gjendjeKrediParaNeGrup = 0;
        
       

        double rezJoSintetikeDebi = 0;
        double rezJoSintetikeKredi = 0;
        string kodi = "";

        private void xrLabel14_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            
                e.Result = shumaDebi;
                
            e.Handled = true;

        }

        private void xrLabel14_SummaryReset(object sender, EventArgs e)
        {
            levizjaDebitore = 0;
            gjendjedebiparaDebitore = 0;
            gjendjeKrediParaDebitore = 0;
            levizjaKreditore = 0;
            gjendjedebiparaKreditore = 0;
            gjendjeKrediParaKreditore = 0;
            rezJoSintetikeDebi = 0;
            rezJoSintetikeKredi = 0;
            shumaKredi = 0;
            shumaDebi = 0;
            kodi = "";
        }
        private void resetVlera(string kodi)
        {
            this.kodi = kodi;
            levizjaNeGrup = 0;
            gjendjedebiparaNeGrup = 0;
            gjendjeKrediParaNeGrup = 0;

           
        }
        private void updateVleraPerGrupin()
        {
            if((levizjaNeGrup + gjendjedebiparaNeGrup - gjendjeKrediParaNeGrup) >= 0)
            {
                levizjaDebitore += levizjaNeGrup;
                gjendjedebiparaDebitore += gjendjedebiparaNeGrup;
                gjendjeKrediParaDebitore += gjendjeKrediParaNeGrup;

            }
            else
            {
                levizjaKreditore -= levizjaNeGrup;
                gjendjedebiparaKreditore -= gjendjedebiparaNeGrup;
                gjendjeKrediParaKreditore -= gjendjeKrediParaNeGrup;

            }
        }
        private void xrLabel14_SummaryRowChanged(object sender, EventArgs e)
        {
            double levizjaDebiCurrentRow, levizjaKrediCurrentRow, debiParaCurrentRow, krediParaCurrentRow;
            double.TryParse(GetCurrentColumnValue("levizjadebitore").ToString(), out levizjaDebiCurrentRow);
            double.TryParse(GetCurrentColumnValue("levizjakreditore").ToString(), out levizjaKrediCurrentRow);
            double.TryParse(GetCurrentColumnValue("gjendjedebipara").ToString(), out debiParaCurrentRow);
            double.TryParse(GetCurrentColumnValue("gjedjekredipara").ToString(), out krediParaCurrentRow);

            double gjendje = debiParaCurrentRow + levizjaDebiCurrentRow - krediParaCurrentRow - levizjaKrediCurrentRow;

            string kodiIRi = GetCurrentColumnValue("kodikpf").ToString();
            if(kodi != kodiIRi)
            {
                updateVleraPerGrupin();
                resetVlera(kodiIRi);
            }
            levizjaNeGrup += Math.Abs(levizjaDebiCurrentRow - levizjaKrediCurrentRow);
            gjendjedebiparaNeGrup += debiParaCurrentRow;
            gjendjeKrediParaNeGrup += krediParaCurrentRow;
            
            rezJoSintetikeDebi += (gjendje >= 0 ? gjendje : 0);
            rezJoSintetikeKredi += (gjendje <= 0 ? -(gjendje) : 0);
            

        }

        private void xrLabel16_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
                e.Result = shumaKredi;
                e.Handled = true;

        }
        private void xrLabel33_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);
            xrLabel33.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Raporti.aspx?scopeID=" + parametraRaporti.ScopeID + "&guidString=" + parametraRaporti.GuidString + "&idraporti=" + parametraRaporti.IdSubRaporti + "&filterNumerLlogarie=" + GetCurrentColumnValue("NRLLOGARI") + "&printo=0&Sesioni=true&VjenNgaSubraporti=true')";
                xrLabel33.Target = "_self";
        
        }
        private void xrLabel21_AfterPrint(object sender, EventArgs e)
        {
            shumaDebi += Convert.ToDouble(xrLabel21.Text);
        }

        private void xrLabel22_AfterPrint(object sender, EventArgs e)
        {
            shumaKredi += Convert.ToDouble(xrLabel22.Text);
        }

        private void Rap_GjendjaLlogTotaleLandscape_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);

        }
    }
}
