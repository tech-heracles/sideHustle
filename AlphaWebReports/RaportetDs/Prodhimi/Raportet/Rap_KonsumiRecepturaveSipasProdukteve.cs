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
    public partial class Rap_KonsumiRecepturaveSipasProdukteve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_KonsumiRecepturaveSipasProdukteve(){InitializeComponent();}
        
        int cnt = 0;
        public Rap_KonsumiRecepturaveSipasProdukteve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.IdGjuha, param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_KonsumiRecepturaveSipasProdukteve(int idGjuha,CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));

            switch (idGjuha) {
                case 0: gjuha.Value =0;
                    break;
                case 1: gjuha.Value =1;
                    break;
            
            }
            noLlogari.Text = raport.Parameters[0].Description;
            parameter2.Value = raport.Parameters[0].Value;
            ndermarrja.Text = raport.Parameters[1].Description;
            parameter4.Value = raport.Parameters[1].Value;
            furnitorArt.Text = raport.Parameters[2].Description;
            parameter5.Value = raport.Parameters[2].Value;
            dateDok.Text = raport.Parameters[3].Description;
            parameter6.Value = raport.Parameters[3].Value;
            dateRegj.Text = raport.Parameters[4].Description;
            parameter7.Value = raport.Parameters[4].Value;
            numerDok.Text = raport.Parameters[5].Description;
            parameter8.Value = raport.Parameters[5].Value; 
            this.xrLabel22.Text = raport.Parameters[8].Description;
            parameter11.Value = raport.Parameters[8].Value;
            xrLabel26.Text = raport.Parameters[9].Description;
            parameter12.Value = raport.Parameters[9].Value;
            xrLabel23.Text = raport.Parameters[10].Description;
            parameter13.Value = raport.Parameters[10].Value;
            switch (idGjuha){
                case 0:
            if(raport.Parameters[6].Value.ToString() == "1")
                parameter3.Value = "Artikull";
            else if (raport.Parameters[6].Value.ToString() == "2")
                parameter3.Value = "Burim";
            else parameter3.Value = "";
            break;
                case 1:
            if (raport.Parameters[6].Value.ToString() == "1")
                parameter3.Value = "Item";
            else if (raport.Parameters[6].Value.ToString() == "2")
                parameter3.Value = "Source";
            else parameter3.Value = "";
                
            break;

        }
            llojiArtBurim.Text = raport.Parameters[6].Description;
            kodArtBurim.Text = rm.GetString("labelKodi", ci) + ":";
            parameter1.Value = raport.Parameters[7].Value;            
        }

        private void xrLabel3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (GetCurrentColumnValue("lloji") != null)
            {
                cnt++;
                xrLabel3.Text = cnt.ToString();
            }
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
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportKonsumiRecepturaveSipasProdukteveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel40.Text = rm.GetString("labelKodi", ci);
            xrLabel16.Text = rm.GetString("labelKodi", ci);
            xrLabel8.Text = rm.GetString("labelRaportiNr", ci);
            xrLabel5.Text = rm.GetString("labelRaportiLloji", ci);
            xrLabel1.Text = rm.GetString("labelNjesia", ci);
            xrLabel2.Text = rm.GetString("labelSasia", ci);
             xrLabel17.Text = rm.GetString("labelRaportiEmertimi", ci);
            xrLabel42.Text = rm.GetString("labelRaportiEmertimi", ci);
            xrLabel15.Text = rm.GetString("labelRaportLlogari", ci);
            xrLabel18.Text = rm.GetString("labelRaportHumbjeLigjore", ci);
            xrLabel14.Text = rm.GetString("labelVlefta", ci);
            xrLabel9.Text = rm.GetString("labelRaportReceptura", ci);
            xrLabel10.Text = rm.GetString("labelRaportKonsumi", ci);
            xrLabel11.Text = rm.GetString("labelRaportProdukti", ci);
            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
        }

        private void Rap_KonsumiRecepturaveSipasProdukteve_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
            cnt = 0;
        }
    }
}
