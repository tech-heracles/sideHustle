using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.QendraKosto.Raportet
{
    public partial class Rap_Permbledhese_Rezultati_i_QK : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_Permbledhese_Rezultati_i_QK(){InitializeComponent();} 
    
        public Rap_Permbledhese_Rezultati_i_QK(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, report)
        {

        }
        public Rap_Permbledhese_Rezultati_i_QK(CultureInfo ci, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            xrLabel1.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel3.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            nivel1.Value = 1;
            grupimQK.Value = Convert.ToBoolean(raport.Parameters["filterGrupimQK"].Value);
            EmrateLabelave(ci);
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel13.Text = rm.GetString("lblTitulliRapPermbledheseRezultatiQK", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel11.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrLabel22.Text = rm.GetString("cmbCmimeArtikulliCaptionKodi", ci);
            xrLabel21.Text = rm.GetString("cmbCmimeArtikulliCaptionPershkrimi", ci);
            xrLabel17.Text = rm.GetString("lblRaportQendraEKostos", ci);
            xrLabel20.Text = rm.GetString("lblRaportTeArdhura", ci);
            xrLabel19.Text = rm.GetString("labelRaportShpenzime", ci);
            xrLabel18.Text = rm.GetString("lblRezultati", ci);
            xrLabel16.Text = rm.GetString("lblRaportDiferencaMeShpenzimetNe", ci);
            xrLabel23.Text = rm.GetString("buxhetiTab", ci);
            xrLabel24.Text = rm.GetString("cmbCmimeArtikulliVlere", ci);
            xrLabel45.Text = rm.GetString("lblRaportVleraERealizuar", ci);
            xrLabel40.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel44.Text = rm.GetString("labelLogoIMB", ci);
            //report header
            xrLabel12.Text = rm.GetString("lblTitulliRapPermbledheseRezultatiQK", ci);
            xrLabel50.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrLabel36.Text = rm.GetString("cmbCmimeArtikulliCaptionKodi", ci);
            xrLabel37.Text = rm.GetString("cmbCmimeArtikulliCaptionPershkrimi", ci);
            xrLabel48.Text = rm.GetString("lblRaportQendraEKostos", ci);
            xrLabel38.Text = rm.GetString("lblRaportTeArdhura", ci);
            xrLabel39.Text = rm.GetString("labelRaportShpenzime", ci);
            xrLabel47.Text = rm.GetString("lblRezultati", ci);
            xrLabel49.Text = rm.GetString("lblRaportDiferencaMeRezultatinNe", ci);
            xrLabel56.Text = rm.GetString("lblRaportDiferencaMeRezultatinNe", ci);
            xrLabel35.Text = rm.GetString("buxhetiTab", ci);
            xrLabel34.Text = rm.GetString("cmbCmimeArtikulliVlere", ci);
            xrLabel14.Text = rm.GetString("lblRaportVleraERealizuar", ci);
            xrLabel51.Text = rm.GetString("lblRaportDiferencaMeShpenzimetNe", ci);
            xrLabel61.Text = rm.GetString("lblNivel", ci);
            xrLabel59.Text = rm.GetString("lblNivel", ci);
        }
       

    }
}
