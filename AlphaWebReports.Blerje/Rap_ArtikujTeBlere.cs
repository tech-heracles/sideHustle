using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.Blerje
{
    public partial class Rap_ArtikujTeBlere : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ArtikujTeBlere(){InitializeComponent();} 

        public Rap_ArtikujTeBlere(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen, param.IdViti, param.IdSubRaporti, report)
        {

        }

        public Rap_ArtikujTeBlere(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
        {         
            InitializeComponent();
            EmrateLabelave(ci);         
            parameter1.Value = raport.Parameters[8].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter6.Value = raport.Parameters[5].Value;
            parameter7.Value = raport.Parameters[6].Value;
            parameter8.Value = raport.Parameters[7].Value;
            KlasaArtikulli.Value = raport.Parameters[10].Value;
            parameter9.Value = raport.Parameters[11].Value;
            parameter10.Value = raport.Parameters[12].Value;
            parameter11.Value = raport.Parameters[13].Value;
            parameter12.Value = raport.Parameters[15].Value;
            parameter13.Value = raport.Parameters[16].Value;
            parameter14.Value = raport.Parameters[17].Value;
            DegaAdministrative.Value = raport.Parameters[14].Value;
            parameter15.Value = raport.Parameters[18].Value;
            parameter16.Value = raport.Parameters[19].Value;
            parameter17.Value = raport.Parameters[9].Value;

           
        }


        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel13.Text = rm.GetString("RaportArtikujTeBlereTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel17.Text = rm.GetString("labelKodi", ci);
            xrLabel18.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel19.Text = rm.GetString("labelNjesia", ci);
            xrLabel20.Text = rm.GetString("labelSasia", ci);
            xrLabel21.Text = rm.GetString("labelCmimi", ci);
            xrLabel22.Text = rm.GetString("labelZbritjeAnalitike", ci);
            xrLabel23.Text = rm.GetString("labelVleftapaTVSH", ci);
            xrLabel32.Text = rm.GetString("labelZbritjaTotale", ci);
            xrLabel24.Text = rm.GetString("labelTVSH", ci);
            xrLabel25.Text = rm.GetString("labelVleftaMe_Tvsh", ci);
            xrLabel16.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell7.Text = rm.GetString("labelFilterAvancuarKodbari", ci);
            xrLabel12.Text = rm.GetString("labelLogoIMB", ci);


        }
    }
}
