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
    public partial class Rap_ShitjePermbledhes : DevExpress.XtraReports.UI.XtraReport
    {    
		public Rap_ShitjePermbledhes(){InitializeComponent();} 
     

        public Rap_ShitjePermbledhes(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_ShitjePermbledhes(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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
            parameter9.Value = raport.Parameters[8].Value;
            parameter10.Value = raport.Parameters[9].Value;  
            parameter11.Value = raport.Parameters[10].Value;  
            parameter12.Value = raport.Parameters[12].Value;  
            parameter13.Value = raport.Parameters[13].Value;  
            parameter14.Value = raport.Parameters[14].Value;
            DegaAdministrative.Value = raport.Parameters[11].Value;
            parameter15.Value = raport.Parameters[15].Value;
            adresaFaturimit.Value = raport.Parameters[20].Value;
            parameter16.Value = raport.Parameters[16].Value;
            parameter17.Value = raport.Parameters["filterKodbari"].Value;
 
        }
    
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {

            ResourceManager rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));

            RaportRegjistriPermbledhesShitjeveR.Text = rm.GetString("RaportRegjistriPermbledhesShitjeveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            NumerRendor.Text = rm.GetString("labelNrRendor", ci);
            Lloji.Text = rm.GetString("labelRaportiLloji", ci);
            Dokumenti.Text = rm.GetString("labelDokumenti", ci);
            monedheFature.Text = rm.GetString("labelMonedhaFature", ci);
            VlereMonedheBaze.Text = rm.GetString("labelMonedhaBaze", ci);
            Nr.Text = rm.GetString("labelRaportiNr", ci);
            DtDok.Text = rm.GetString("labelRaportiDtDok", ci);
            Monedhe.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            Kursi.Text = rm.GetString("labelKursi", ci);
            Nentotal.Text = rm.GetString("labelNentotal", ci);
            Zbritje.Text = rm.GetString("labelZbritje", ci);
            TVSH.Text = TVSH1.Text = rm.GetString("labelTVSH", ci);
            Total.Text = Total2.Text = TOTALI.Text = rm.GetString("labelRaportiTotali", ci);
            labelLogo.Text = rm.GetString("labelLogoIMB", ci);
            Furnitor1.Text = rm.GetString("cmbCmimeArtikulliCaptionKodi", ci);
            Furnitor2.Text = rm.GetString("labelRaportEmertimi", ci);
            
        }
    }
}
