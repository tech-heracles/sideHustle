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
    public partial class Rap_HistorikRecepturat : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_HistorikRecepturat(){InitializeComponent();}

        

        public Rap_HistorikRecepturat(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.IdGjuha, param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_HistorikRecepturat(int idGjuha, CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
              xrLabel52.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel57.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            xrLabel63.Text = raport.Parameters[2].Description;
            parameter3.Value = raport.Parameters[2].Value;
            xrLabel46.Text = raport.Parameters[3].Description;
            parameter4.Value = raport.Parameters[3].Value;
            xrLabel60.Text = raport.Parameters[4].Description;
            parameter5.Value = raport.Parameters[4].Value;
            xrLabel64.Text = raport.Parameters[5].Description;
            parameter6.Value = raport.Parameters[5].Value;
            switch (idGjuha)
            {
                case 0: //shqip
                    parameter7.Value = 0;
                    break;
                case 1: //anglisht
                    parameter7.Value = 1;
                    break;
                default: break;
                    
            }

        }
        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
            
        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportHistorikuRecepturaveProduktitTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel38.Text = rm.GetString("labelRaportRecepturat", ci);
            xrLabel36.Text = rm.GetString("labelRaportArtikullProdhim", ci);
            xrLabel7.Text = rm.GetString("labelKodi", ci);
            xrLabel8.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel9.Text = rm.GetString("labelNjesia", ci);
            xrLabel10.Text = rm.GetString("labelRaportLLogariInventar", ci);
            xrLabel11.Text = rm.GetString("labelRaportSasiaNjesi", ci);
            xrLabel13.Text = rm.GetString("labelRaportHumbjeLigjore", ci) + "%";
            xrLabel14.Text = rm.GetString("labelKodi", ci);
            xrLabel15.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel16.Text = rm.GetString("labelNjesia", ci);
            xrLabel17.Text = rm.GetString("labelRaportLLogariInventar", ci);
            xrLabel18.Text = rm.GetString("labelRaportSasiaNjesi", ci);
            xrLabel19.Text = rm.GetString("labelRaportHumbjeLigjore", ci) + "%";
            xrLabel5.Text = rm.GetString("labelRaportData", ci);
            //xrLabel21.Text = rm.GetString("labelRaportiArtikull", ci);
            //xrLabel31.Text = rm.GetString("labelRaportAktiviteti", ci);
            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
        }
    }

    }

