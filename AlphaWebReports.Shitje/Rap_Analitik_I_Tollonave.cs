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
    public partial class Rap_Analitik_I_Tollonave : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Analitik_I_Tollonave(){InitializeComponent();}
        
        public Rap_Analitik_I_Tollonave(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) : this(param.IdRaporti,param.IdGjuha, param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report) { }
        public Rap_Analitik_I_Tollonave(int idRaporti, int idGjuha, CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            DtDokLabel.Text = raport.Parameters[1].Description;
            DtDok.Value = raport.Parameters[1].Value;
            KlientLabel.Text = raport.Parameters[2].Description;
            Klient.Value = raport.Parameters[2].Value;
            ArtikullLabel.Text = raport.Parameters[3].Description;
            Artikull.Value = raport.Parameters[3].Value;
            Grupim2Label.Text = raport.Parameters[0].Description;
            Grupim2.Value = raport.Parameters[0].Value;
            Grupim1Label.Text = raport.Parameters[4].Description;
            Grupim1.Value = raport.Parameters[4].Value;
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
            MarzhiShitjeveLabel.Text = rm.GetString("RaportMarzhiShitjeveSipasKlienteveTitulli", ci);
            
        }

        private void Rap_Analitik_I_Tollonave_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);

        }
    }
}
