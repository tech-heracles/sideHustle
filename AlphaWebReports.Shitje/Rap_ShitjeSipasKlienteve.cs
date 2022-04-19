using System;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_ShitjeSipasKlienteve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ShitjeSipasKlienteve(){InitializeComponent();} 

        public Rap_ShitjeSipasKlienteve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.IdRaporti, param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_ShitjeSipasKlienteve(int idRaporti, CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            EmrateLabelave(ci);
            xrLabel54.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel55.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            xrLabel56.Text = raport.Parameters[2].Description;
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
            parameter17.Value = raport.Parameters[20].Value;
            parameter18.Value = raport.Parameters[21].Value;
            adresaFaturimit.Value = raport.Parameters[27].Value;
            parameter20.Value = raport.Parameters["filterAktivitetiKlient"].Value;
            Monedha.Value = raport.Parameters["monedhaKF"].Value;
           // if (Monedha.Value.ToString() == "False")
              //  xrLabel66.Text = rm.GetString("labelRaportJo", ci);
           // else xrLabel66.Text = rm.GetString("labelRaportPo", ci);
        //    xrLabel65.Text = rm.GetString("labelFilterAvancuarMonedheKF", ci);

        

        }


        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            var rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));


           
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);           
            xrLabel18.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel19.Text = rm.GetString("labelNjesia", ci);
            xrLabel20.Text = rm.GetString("labelSasia", ci);
           xrLabel22.Text = rm.GetString("labelRaportVlefta", ci);
            xrLabel12.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel17.Text = rm.GetString("labelRaportKlienti", ci);
            xrLabel13.Text = rm.GetString("labelRaportShitjetSipasKlienteve", ci);
            xrLabel21.Text = rm.GetString("cmbCmimeArtikulliCaptionKodi", ci);
        }   
    }
}
