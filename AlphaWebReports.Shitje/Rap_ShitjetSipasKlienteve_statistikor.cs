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
    public partial class Rap_ShitjetSipasKlienteve_statistikor : DevExpress.XtraReports.UI.XtraReport
    {  
		public Rap_ShitjetSipasKlienteve_statistikor(){InitializeComponent();} 
        private string monedha;
        private bool ndryshuar = false;
     
        public Rap_ShitjetSipasKlienteve_statistikor(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_ShitjetSipasKlienteve_statistikor(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters["IdNdermarje"].Value;
            parameter2.Value = raport.Parameters["filterDtDok"].Value;
            parameter3.Value = raport.Parameters["filterDtRegj"].Value;
            parameter4.Value = raport.Parameters["filterNumerLlogarie"].Value;
            parameter5.Value = raport.Parameters["filterKlientFurnitor"].Value;
            parameter6.Value = raport.Parameters["filterQyteti"].Value;
            parameter7.Value = raport.Parameters["filterAdreseKlienti"].Value;
            parameter8.Value = raport.Parameters["filterLlojDokumenti"].Value;
            parameter9.Value = raport.Parameters["filterDegeAdministrative"].Value;
            parameter10.Value = raport.Parameters["filterPikeshitjeFurnizmi"].Value;
            parameter11.Value = raport.Parameters["filterAgjentShitje"].Value;
            parameter12.Value = raport.Parameters["filterGrupimPKF"].Value;
            parameter13.Value = raport.Parameters["filterGrupimDKF"].Value;
            parameter14.Value = raport.Parameters["filterGrupimTKF"].Value;
            parameter15.Value = raport.Parameters["filterkodifikimartP"].Value;
            parameter16.Value = raport.Parameters["filterkodifikimartD"].Value;
            parameter17.Value = raport.Parameters["filterGrupimDokP"].Value;
            parameter18.Value = raport.Parameters["filterGrupimDokD"].Value;
            parameter19.Value = raport.Parameters["filterGrupimDokT"].Value;
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
            System.Reflection.Assembly.Load("App_GlobalResources"));          
            xrLabel70.Text = rm.GetString("FiltratEmertimi", ci);
            //xrLabel20.Text = rm.GetString("lblTitulliRap", ci);
            NrRendKoka.Text = rm.GetString("lblNrrend", ci);
            KodiKoka.Text = rm.GetString("lblKodi", ci);
            EmertimiKoka.Text = rm.GetString("labelRaportiEmertimi", ci);
            QytetiKoka.Text = rm.GetString("labelQyteti", ci);
            AdresaKoka.Text = rm.GetString("labelRaportAdresa", ci);
            VleraShiturNetoKoka.Text = rm.GetString("lblVlerashiturNeto", ci);
            NrFaturashKoka.Text = rm.GetString("lblNrFaturash", ci);
            VleraMesFaturKoka.Text = rm.GetString("lblVlMesFature", ci);
            NrTotRreshtKoka.Text = rm.GetString("lblNrTotRreshtash", ci);
            NrGrupArtikujKoka.Text = rm.GetString("lblNrGrupeArt", ci);
            NrMesRreshtaFatureKoka.Text = rm.GetString("lblNrMesRreshtashFat", ci);
            PeshaKlientPerqKoka.Text = rm.GetString("lblPeshaKlientit", ci);
            xrLabel42.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel40.Text = "-";

            //xrLabel109.Text = rm.GetString("FiltratEmertimi", ci);
            //xrLabel120.Text = rm.GetString("lblTitulliRap", ci);
            //xrLabel109.Text = rm.GetString("lblNrrend", ci);
            //xrLabel117.Text = rm.GetString("lblKodi", ci);
            //xrLabel119.Text = rm.GetString("labelRaportiEmertimi", ci);
            //xrLabel118.Text = rm.GetString("labelQyteti", ci);
            //xrLabel116.Text = rm.GetString("labelRaportAdresa", ci);
            //xrLabel115.Text = rm.GetString("lblVlerashiturNeto", ci);
            //xrLabel114.Text = rm.GetString("lblNrFaturash", ci);
            //xrLabel113.Text = rm.GetString("lblVlMesFature", ci);
            //xrLabel112.Text = rm.GetString("lblNrTotRreshtash", ci);
            //xrLabel111.Text = rm.GetString("lblNrGrupeArt", ci);
            //xrLabel110.Text = rm.GetString("lblNrMesRreshtashFat", ci);
            //xrLabel17.Text = rm.GetString("lblPeshaKlientit", ci);
           
        }

    }
}
