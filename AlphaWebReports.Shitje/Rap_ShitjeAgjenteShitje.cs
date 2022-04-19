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
    public partial class Rap_ShitjeAgjenteShitje : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ShitjeAgjenteShitje(){InitializeComponent();} 
       
        int count = 1;
        double vleraShiturTotal;
        int nrFaturashTotal, nrRreshtashTotal;
        public Rap_ShitjeAgjenteShitje(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_ShitjeAgjenteShitje(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            
            parameter1.Value = raport.Parameters["IdNdermarje"].Value;

            parameter4.Value = raport.Parameters["filterDtDok"].Value;
            parameter5.Value = raport.Parameters["filterDegeAdministrative"].Value;
            parameter7.Value = raport.Parameters["filterDtRegj"].Value;
            parameter10.Value = raport.Parameters["filterAgjentShitje"].Value;
            DegaAdministrative.Value = raport.Parameters["filterKlientFurnitor"].Value;
            parameter11.Value = raport.Parameters["filterkodifikimartP"].Value;
            parameter13.Value = raport.Parameters["filterkodifikimartD"].Value; 
            parameter15.Value = raport.Parameters["filterQyteti"].Value;


            parameter16.Value = raport.Parameters["filterGrupimDokP"].Value;
            parameter18.Value = raport.Parameters["filterGrupimDokD"].Value;
            PershkrimDetajimArt.Value = raport.Parameters["filterGrupimDokT"].Value;
            parameter21.Value = raport.Parameters[21].Value;
            

            count = 1;
            vleraShiturTotal = 0;
            nrFaturashTotal = 0;
            nrRreshtashTotal = 0;
        }

     
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
          ResourceManager  rm = new ResourceManager("Resources.Strings",
                     System.Reflection.Assembly.Load("App_GlobalResources"));


            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            NrRendKoka.Text = rm.GetString("labelNrRendor", ci);
            KodiKoka.Text = rm.GetString("labelKodi", ci);
            EmertimiKoka.Text = rm.GetString("labelRaportEmertimi", ci);
            VlShNetoKoka.Text = rm.GetString("lblVlerashiturNeto", ci);
            ZbritjeKoka.Text = rm.GetString("labelZbritje", ci);
            NrFatKoka.Text = rm.GetString("lblNrFaturash", ci);
            NrKlientKoka.Text = rm.GetString("labelNrKlientesh", ci);
            KostoPlanKoka.Text = rm.GetString("labelKostoPlan", ci);
            VlMesShFatKoka.Text = rm.GetString("lblVlMesFature", ci);
            NrTotRrKoka.Text = rm.GetString("lblNrTotRreshtash", ci);
            NrGrArtKoka.Text = rm.GetString("lblNrGrupeArt", ci);
            NrMesRrFatKoka.Text = rm.GetString("lblNrMesRreshtashFat", ci);
            PeshaAgjent.Text = rm.GetString("lblPeshaAgjentit", ci);
             //report header
            xrLabel109.Text = rm.GetString("TitullRaportiShitjeAgjenteShitje", ci);

            //xrLabel54.Text = rm.GetString("labelFooterNdermarrja", ci);
            //xrLabel57.Text = rm.GetString("filterDtDok", ci);
            //xrLabel60.Text = rm.GetString("filterDtRegj", ci);
            //xrLabel74.Text = rm.GetString("filterRaportiAgjentetShitjes", ci);
            //degaAdminLabel.Text = rm.GetString("filterFurnitor", ci);
            //xrLabel85.Text = rm.GetString("filterQyteti", ci);
            //xrLabel76.Text = rm.GetString("filterGrupimPare", ci);
            //xrLabel80.Text = rm.GetString("filterGrupimDyte", ci);
            //xrLabel87.Text = rm.GetString("filterGrupimDokP", ci);
            //xrLabel92.Text = rm.GetString("filterGrupimDokD", ci);
            //xrLabel103.Text = rm.GetString("filterGrupimDokt", ci);
           
            //xrLabel24.Text = rm.GetString("filterDegeAdministrative", ci);

        }
    }
}
