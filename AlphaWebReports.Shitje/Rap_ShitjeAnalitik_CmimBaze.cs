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
    public partial class Rap_ShitjeAnalitik_CmimBaze : XtraReport
    {
        public Rap_ShitjeAnalitik_CmimBaze()
        {
            InitializeComponent();

        }
        int shifraPasPresjes = 0;
        public Rap_ShitjeAnalitik_CmimBaze(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {
           
        }
        public Rap_ShitjeAnalitik_CmimBaze(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);

           
        }


      
      
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
          ResourceManager  rm = new ResourceManager("Resources.Strings",
                     System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel17.Text = rm.GetString("RaportRegjistriAnalitikShitjeveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel10.Text = rm.GetString("labelDokumentArtikulli", ci);
            xrLabel3.Text = rm.GetString("labelKodi", ci);
            xrLabel4.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel7.Text = rm.GetString("labelNjesia", ci);
            xrLabel8.Text = rm.GetString("labelSasia", ci);
            xrLabel13.Text = rm.GetString("labelVlefta", ci);
            xrLabel14.Text = rm.GetString("labelGjithsej", ci);
            xrLabel15.Text = rm.GetString("labelZbritjeAnalitike", ci) +" %";
            xrLabel23.Text = rm.GetString("labelVleftapaTVSH", ci);
            xrLabel19.Text = rm.GetString("labelZbritjaTotale", ci) + " %";
            xrLabel24.Text = rm.GetString("labelVleftameZbritje", ci);
            xrLabel21.Text = rm.GetString("labelMonLlogari", ci);
            xrLabel22.Text = rm.GetString("labelMonBaze", ci);
            xrLabel44.Text = rm.GetString("labelShumaPaTvsh", ci);
            xrLabel45.Text = rm.GetString("labelTVSH", ci);
            xrLabel46.Text = rm.GetString("labelShumaMeTvsh", ci);
            xrLabel66.Text = rm.GetString("labelShumaPaTvsh", ci);
            xrLabel64.Text = rm.GetString("labelTVSH", ci);
            xrLabel65.Text = rm.GetString("labelShumaMeTvsh", ci);
            xrLabel90.Text = rm.GetString("labelFilterAvancuarPerdorues", ci);
            xrLabel94.Text = rm.GetString("filterRaportPershkrimFature", ci);
            xrLabel1.Text = rm.GetString("labelNr", ci);
            xrLabel2.Text = rm.GetString("label_Klienti",ci);
            xrLabel71.Text = rm.GetString("label_Emri", ci);
            xrLabel5.Text = rm.GetString("labelDateDokumenti", ci);
            xrLabel6.Text = rm.GetString("filterMonedha", ci);
            xrLabel43.Text = rm.GetString("labelLogoIMB", ci);
            
            //report header
            //xrLabel109.Text = rm.GetString("RaportRegjistriAnalitikShitjeveTitulli", ci);
            //xrLabel123.Text = rm.GetString("labelDokumentArtikulli", ci);
            //xrLabel120.Text = rm.GetString("labelKodi", ci);
            //xrLabel121.Text = rm.GetString("labelRaportiPershkrimi", ci);
            //xrLabel110.Text = rm.GetString("labelNjesia", ci);
            //xrLabel111.Text = rm.GetString("labelSasia", ci);
            //xrLabel117.Text = rm.GetString("labelVlefta", ci);
            //xrLabel113.Text = rm.GetString("labelCmimi", ci);
            //xrLabel122.Text = rm.GetString("labelGjithsej", ci);
            //xrLabel116.Text = rm.GetString("labelZbritjeAnalitike", ci);
            //xrLabel118.Text = rm.GetString("labelVleftapaTVSH", ci);
            //xrLabel112.Text = rm.GetString("labelZbritjaTotale", ci);
            //xrLabel119.Text = rm.GetString("labelVleftameZbritje", ci);
            //xrLabel114.Text = rm.GetString("labelMonLlogari", ci);
            //xrLabel115.Text = rm.GetString("labelMonBaze", ci);
            //xrLabel124.Text = rm.GetString("labelFilterAvancuarPerdorues", ci);
            //xrLabel125.Text = rm.GetString("filterRaportPershkrimFature", ci);
            //xrLabel128.Text = rm.GetString("labelRaportiCmimiBaze", ci);
        }

       
      
    }
}
