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
    public partial class Rap_MarzhiShitjeve_SipasMagazinaveFormat2 : DevExpress.XtraReports.UI.XtraReport
        {
		public Rap_MarzhiShitjeve_SipasMagazinaveFormat2(){InitializeComponent();} 
        public Rap_MarzhiShitjeve_SipasMagazinaveFormat2(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_MarzhiShitjeve_SipasMagazinaveFormat2(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
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

        }


        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {

            ResourceManager rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));


            MarzhiShitjeveLabel.Text = rm.GetString("lblRaportTitulliMarzhiShitjeveSipasMag", ci) + " (Format 2)";
            xrLabel43.Text = rm.GetString("FiltratEmertimi", ci);
            TotaliLabel.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel32.Text = rm.GetString("labelLogoIMB", ci);
            KartelaKokaR.Text =  rm.GetString("labelKartela", ci);
            EmertimiArtikulliKokaR.Text =  rm.GetString("labelEmertimiArtikullit", ci);
            NjesiaKokaR.Text = rm.GetString("labelNjesia", ci);
            SasiaKokaR.Text =rm.GetString("labelSasia", ci);
            KostoNjesiaKokaR.Text= rm.GetString("labelKostoNjesi", ci);
            KMSHKokaR.Text =  rm.GetString("labelKMSH", ci);
            CmimiShitjesKokaR.Text = rm.GetString("labelCmimShitje", ci);
            VleraShitjesMeZbritjeKokaR.Text = rm.GetString("labelVleraShitjesMeZbritje", ci);
            MarzhiBrutoMeZbritjeKokaR.Text =  rm.GetString("labelMarzhiBrutoMeZbritje", ci);
            MarzhiBrutoKokaR.Text =  rm.GetString("labelMarzhiBrutoPerqindje", ci);
            MagazinaKokaR.Text =  rm.GetString("labelRaportMagazina", ci);
            
        }
        
    }
}
