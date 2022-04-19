using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_KartelaEThjeshteEArtikullit : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_KartelaEThjeshteEArtikullit(){InitializeComponent();} 
        public Rap_KartelaEThjeshteEArtikullit(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_KartelaEThjeshteEArtikullit(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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
            parameter12.Value = raport.Parameters[13].Value;
            parameter14.Value = raport.Parameters[14].Value;
            parameter15.Value = raport.Parameters[15].Value;
            parameter13.Value = raport.Parameters[11].Value;
            DegaAdministrative.Value = raport.Parameters[12].Value;

        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
            System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel17.Text = rm.GetString("lblTitulliRaportKartelaThjeshteArtikullit", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel5.Text = rm.GetString("labelKartela", ci) + ":";
            xrLabel11.Text = rm.GetString("labelRaportiPershkrimi", ci) + ":";
            xrLabel7.Text = rm.GetString("filterKodbari", ci);
            xrLabel13.Text = rm.GetString("filterArkaBankaEmer", ci);
            xrLabel9.Text = rm.GetString("labelRaportMetodaKostos", ci);
            xrLabel15.Text = rm.GetString("labelFilterAvancuarGrupi", ci) + ":";
            xrLabel18.Text = rm.GetString("labelLlojDokumenti", ci);
            xrLabel19.Text = rm.GetString("labelRaportiNrDok", ci);
            xrLabel22.Text = rm.GetString("labelRaportiDtDok", ci);
            xrLabel23.Text = rm.GetString("labelFilterAvancuarMagazina", ci);
            xrLabel24.Text = rm.GetString("labelNjesia", ci);
            xrLabel35.Text = rm.GetString("labelRaportHyrje", ci);
            xrLabel26.Text = rm.GetString("labelRaportDalje", ci);
            xrLabel28.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
            xrLabel69.Text = rm.GetString("labelLogoIMB", ci);
        }
    }
}
