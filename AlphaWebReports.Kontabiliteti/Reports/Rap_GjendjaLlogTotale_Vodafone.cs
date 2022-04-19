using System;
using System.Collections;
using System.Globalization;
using System.Resources;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs
{
    public partial class Rap_GjendjaLlogTotale_Vodafone : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_GjendjaLlogTotale_Vodafone() { InitializeComponent(); }
        private static string formatstring = "{0:#,#.00}";

        double debimonbaze = 0.0;
        double kredimonbaze = 0.0;
        double rezJoSintetikeDebi = 0;
        double rezJoSintetikeKredi = 0;


        double levizjaDebitore = 0;
        double gjendjedebiparaDebitore = 0;
        double gjendjeKrediParaDebitore = 0;

        double levizjaKreditore = 0;
        double gjendjedebiparaKreditore = 0;
        double gjendjeKrediParaKreditore = 0;

        double levizjaNeGrup = 0;
        double gjendjedebiparaNeGrup = 0;
        double gjendjeKrediParaNeGrup = 0;
        string kodi = "";
        double shumaDebi = 0;
        double shumaKredi = 0;


        public Rap_GjendjaLlogTotale_Vodafone(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, report)
        {

        }
        public Rap_GjendjaLlogTotale_Vodafone(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
        {

            InitializeComponent();
            EmrateLabelave(ci);
            if ((raport.Parameters["filterLlogariSintetike"].Value).ToString() == "Po")
            {
                this.sintetike.Value = true;
            }
            else
            {
                this.sintetike.Value = false;
            }
         

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
            xrLabel47.Text = rm.GetString("lblLlogariSAPRaportGjendjeLlogarive", ci);
        }

        private void xrLabel31_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
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

        private void xrLabel32_SummaryGetResult_1(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
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
                    shumaKredi += shuma;
                e.Result = shuma;
            }
            else e.Result = 0.00;
            e.Handled = true;
        }


        private void xrLabel21_AfterPrint(object sender, EventArgs e)
        {
            shumaDebi += Convert.ToDouble(xrLabel21.Text);
        }

        private void xrLabel22_AfterPrint(object sender, EventArgs e)
        {
            shumaKredi += Convert.ToDouble(xrLabel22.Text);
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
        private void xrLabel16_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            e.Result = shumaKredi;
            e.Handled = true;
        }


        private void xrLabel14_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            debimonbaze = Convert.ToDouble(xrLabel14.Summary.GetResult());
        }


        private void xrLabel16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            kredimonbaze = Convert.ToDouble(xrLabel16.Summary.GetResult());
        }

        private void xrLabel14_SummaryGetResult(object sender, DevExpress.XtraReports.UI.SummaryGetResultEventArgs e)
        {
            e.Result = shumaDebi;
            e.Handled = true;
        }

        private void xrLabel14_SummaryReset(object sender, System.EventArgs e)
        {
            levizjaDebitore = 0;
            gjendjedebiparaDebitore = 0;
            gjendjeKrediParaDebitore = 0;
            levizjaKreditore = 0;
            gjendjedebiparaKreditore = 0;
            gjendjeKrediParaKreditore = 0;
            rezJoSintetikeDebi = 0;
            rezJoSintetikeKredi = 0;
            shumaDebi = 0;
            shumaKredi = 0;
            kodi = "";
        }

        private void xrLabel14_SummaryRowChanged(object sender, System.EventArgs e)
        {

            double levizjaDebiCurrentRow, levizjaKrediCurrentRow, debiParaCurrentRow, krediParaCurrentRow;
            double.TryParse(GetCurrentColumnValue("levizjadebitore").ToString(), out levizjaDebiCurrentRow);
            double.TryParse(GetCurrentColumnValue("levizjakreditore").ToString(), out levizjaKrediCurrentRow);
            double.TryParse(GetCurrentColumnValue("gjendjedebipara").ToString(), out debiParaCurrentRow);
            double.TryParse(GetCurrentColumnValue("gjedjekredipara").ToString(), out krediParaCurrentRow);

            double gjendje = debiParaCurrentRow + levizjaDebiCurrentRow - krediParaCurrentRow - levizjaKrediCurrentRow;

            string kodiIRi = GetCurrentColumnValue("kodikpf").ToString();
            if (kodi != kodiIRi)
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
        private void resetVlera(string kodi)
        {
            this.kodi = kodi;
            levizjaNeGrup = 0;
            gjendjedebiparaNeGrup = 0;
            gjendjeKrediParaNeGrup = 0;

        }
        private void updateVleraPerGrupin()
        {
            if ((levizjaNeGrup + gjendjedebiparaNeGrup - gjendjeKrediParaNeGrup) >= 0)
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
        

    }
}
