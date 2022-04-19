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
    public partial class Rap_ShitjePermbledhesMeEmertimKlienti_Zbritje : DevExpress.XtraReports.UI.XtraReport
    {        
		public Rap_ShitjePermbledhesMeEmertimKlienti_Zbritje(){InitializeComponent();} 
        public Rap_ShitjePermbledhesMeEmertimKlienti_Zbritje(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_ShitjePermbledhesMeEmertimKlienti_Zbritje(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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
            NrRend.Text = rm.GetString("labelNrRendor", ci);
            Lloji.Text = rm.GetString("labelRaportiLloji", ci);
            Dokumenti.Text = rm.GetString("labelDokumenti", ci);
            ZbritjeTotale.Text = MonedheFature.Text = rm.GetString("labelMonedhaFature", ci);
            VleraNeMonedheBaze.Text = rm.GetString("labelMonedhaBaze", ci);
            Nr.Text = rm.GetString("labelRaportiNr", ci);
            DtDok.Text = rm.GetString("labelRaportiDtDok", ci);
            Monedhe.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            Kursi.Text = rm.GetString("labelKursi", ci);
            Nentotal.Text = ZbritjeAnalitike.Text =  rm.GetString("labelNentotal", ci);
            Total1.Text = xrLabel42.Text = Total.Text = rm.GetString("labelRaportiTotali", ci);
            TVSH1.Text = TVSH.Text = rm.GetString("labelTVSH", ci);
            xrLabel50.Text = rm.GetString("labelLogoIMB", ci);
            ZbritjeAnalitike.Text = rm.GetString("MenuItemZbritjeAnalitike", ci);
            ZbritjeTotale.Text = rm.GetString("lblZbritjeTotale", ci);
            ZbritjeGjithsejNePerqindje.Text = rm.GetString("lblZbrGjithsej", ci);
            ZbritjeGjithsejNeVlere.Text = rm.GetString("lblZbrVler", ci);

        }

    }
}
