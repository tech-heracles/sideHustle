using System;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    
    public partial class Rap_MarzhiShitjeveTollona : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_MarzhiShitjeveTollona(){InitializeComponent();} 


        public Rap_MarzhiShitjeveTollona(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):this(param.Ci, param.IdNdermarrje, param.IdViti, report){}
        public Rap_MarzhiShitjeveTollona(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {

            InitializeComponent();
            EmrateLabelave(ci);
            Ndermarrja.Value = raport.Parameters[0].Value;
            DtDok.Value = raport.Parameters[1].Value;
            Klient.Value = raport.Parameters[2].Value;
            Artikull.Value = raport.Parameters[3].Value;
            Qyteti.Value = raport.Parameters[4].Value;
            Grupim1.Value = raport.Parameters[5].Value;
            Grupim2.Value = raport.Parameters[6].Value;
            PikeShitjeFurnizim.Value = raport.Parameters[7].Value;
            LlojArtikulli.Value = raport.Parameters[8].Value;
            KlasaArtikulli.Value = raport.Parameters[9].Value;
            parameter1.Value = raport.Parameters[10].Value;
            parameter2.Value = raport.Parameters[11].Value;
            parameter3.Value = raport.Parameters[12].Value;
            parameter4.Value = raport.Parameters[14].Value;
            parameter5.Value = raport.Parameters[15].Value;
            parameter6.Value = raport.Parameters[16].Value;
            DegaAdministrative.Value = raport.Parameters[13].Value;
            parameter7.Value = raport.Parameters[17].Value;
            parameter8.Value = raport.Parameters["filterKodbari"].Value;
            parameter9.Value = raport.Parameters["filterAktivitetiKlient"].Value;


        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            var rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));


            titulliLabel.Text = rm.GetString("RaportMarzhiShitjeveTollonaTitull", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);

            labelKodi.Text = rm.GetString("labelKartela", ci);
            labelKodbari.Text = rm.GetString("labelEmertimiArtikullit", ci);
            //labelRaportiPershkrimi.Text = rm.GetString("labelRaportiPershkrimi", ci);
            labelNjesia.Text = rm.GetString("labelNjesia", ci);
            labelSasia.Text = rm.GetString("labelSasia", ci);
            labelCmimi.Text = rm.GetString("labelRaportCmimiFaturimit", ci);
            labelZbritjeAnalitike.Text = rm.GetString("labelRaportVleraFaturimit", ci);
            labelVleftapaTVSH.Text = rm.GetString("lblcmShitje", ci);
            labelZbritjaTotale.Text = rm.GetString("labelVleraShitjesMeZbritje", ci);
            labelTVSH.Text = rm.GetString("labelMarzhiBrutoMeZbritje", ci);
            xrTableCell9.Text = rm.GetString("labelMarzhiBrutoPerqindje", ci);
            labelVleftaMe_Tvsh.Text = rm.GetString("labelRaportFitimLiter", ci);

            labelRaportiTotali.Text = rm.GetString("labelRaportiTotali", ci);
            labelLogoIMB.Text = rm.GetString("labelLogoIMB", ci);
            
            //Report header
            //titulliLabelReportHeader.Text = rm.GetString("RaportArtikujTeShiturTitulli", ci);
            //xrLabel67.Text = rm.GetString("labelKodi", ci);
            //xrLabel75.Text = rm.GetString("labelRaportiPershkrimi", ci);
            //xrLabel74.Text = rm.GetString("labelNjesia", ci);
            //xrLabel73.Text = rm.GetString("labelSasia", ci);
            //xrLabel72.Text = rm.GetString("labelCmimi", ci);
            //xrLabel71.Text = rm.GetString("labelZbritjeAnalitike", ci);
            //xrLabel70.Text = rm.GetString("labelVleftapaTVSH", ci);
            //xrLabel66.Text = rm.GetString("labelZbritjaTotale", ci);
            //xrLabel69.Text = rm.GetString("labelTVSH", ci);
            //xrLabel76.Text = rm.GetString("labelVleftaMe_Tvsh", ci);
        }
    }
}
