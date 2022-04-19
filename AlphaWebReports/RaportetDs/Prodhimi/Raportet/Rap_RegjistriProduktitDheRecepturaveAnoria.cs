using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Prodhimi.Raportet
{
    public partial class Rap_RegjistriProduktitDheRecepturaveAnoria : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_RegjistriProduktitDheRecepturaveAnoria(){InitializeComponent();}

        
        public Rap_RegjistriProduktitDheRecepturaveAnoria(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.IdGjuha, param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_RegjistriProduktitDheRecepturaveAnoria(int idGjuha, CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            nrLlog.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            ndermarrja.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            magazina.Text = raport.Parameters[2].Description;
            parameter3.Value = raport.Parameters[2].Value;
            kodArtikulli.Text = raport.Parameters[3].Description;
            parameter4.Value = raport.Parameters[3].Value;
            njesiArt.Text = raport.Parameters[4].Description;
            parameter5.Value = raport.Parameters[4].Value;
            dtDok.Text = raport.Parameters[5].Description;
            parameter6.Value = raport.Parameters[5].Value;
            dtRegj.Text = raport.Parameters[6].Description;
            parameter7.Value = raport.Parameters[6].Value;
            nrDok.Text = raport.Parameters[7].Description;
            parameter8.Value = raport.Parameters[7].Value;
             this.xrLabel9.Text = raport.Parameters[8].Description;
            parameter11.Value = raport.Parameters[8].Value;
            xrLabel10.Text = raport.Parameters[9].Description;
            parameter12.Value = raport.Parameters[9].Value;
            xrLabel15.Text = raport.Parameters[10].Description;
            parameter13.Value = raport.Parameters[10].Value;

        }


        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("ArtProdhim") != null && GetCurrentColumnValue("ArtProdhim").ToString() == "1")
            {
                llojiLabel.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
                kodiLabel.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
                pershkrimiLabel.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
                njesiaLabel.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
                sasiaPlanLabel.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
                sasiAktLabel.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
                diferencaLabel.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
                cmimiLabel.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
                vleftaLabel.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            }
            else
            {
                llojiLabel.Font = new System.Drawing.Font("Times New Roman", 10F);
                kodiLabel.Font = new System.Drawing.Font("Times New Roman", 10F);
                pershkrimiLabel.Font = new System.Drawing.Font("Times New Roman", 10);
                njesiaLabel.Font = new System.Drawing.Font("Times New Roman", 10F);
                sasiaPlanLabel.Font = new System.Drawing.Font("Times New Roman", 10F);
                sasiAktLabel.Font = new System.Drawing.Font("Times New Roman", 10F);
                diferencaLabel.Font = new System.Drawing.Font("Times New Roman", 10F);
                cmimiLabel.Font = new System.Drawing.Font("Times New Roman", 10F);
                vleftaLabel.Font = new System.Drawing.Font("Times New Roman", 10F);
            }
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportRegjistriProduktitRecepturaveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel36.Text = rm.GetString("labelRaportiNrDok", ci);
            xrLabel37.Text = rm.GetString("labelRaportiDtDok", ci);
            xrLabel5.Text = rm.GetString("labelRaportiLloji", ci);
            xrLabel40.Text = rm.GetString("labelKodi", ci);
            xrLabel1.Text = rm.GetString("labelNjesia", ci);
            xrLabel14.Text = rm.GetString("labelVlefta", ci);
            xrLabel13.Text = rm.GetString("labelCmimi", ci);
            xrLabel42.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel6.Text = rm.GetString("labelRaportSasiaAktuale", ci);
            xrLabel2.Text = rm.GetString("labelRaportSasiaPlan", ci);
            xrLabel19.Text = rm.GetString("labelRaportiDiferenca", ci);
           xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
           xrLabel17.Text = rm.GetString("labelRaportGjatesi", ci);
           xrLabel18.Text = rm.GetString("labelRaportGjeresi", ci);
        }

        private void Rap_RegjistriProduktitDheRecepturaveAnoria_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
        }
    }
}
