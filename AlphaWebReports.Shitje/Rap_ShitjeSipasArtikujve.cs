using System;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_ShitjeSipasArtikujve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ShitjeSipasArtikujve(){InitializeComponent();} 
     
        public Rap_ShitjeSipasArtikujve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_ShitjeSipasArtikujve(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
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
            parameter17.Value = raport.Parameters[20].Value;
            parameter18.Value = raport.Parameters[21].Value;
            adresaFaturimit.Value = raport.Parameters[27].Value;
            Monedha.Value = raport.Parameters["monedhaKF"].Value;
        ///    if (Monedha.Value.ToString() == "False")
              //  xrLabel9.Text =rm.GetString("labelRaportJo", ci);
           // else xrLabel9.Text = rm.GetString("labelRaportPo", ci);
         //   xrLabel10.Text = rm.GetString("labelFilterAvancuarMonedheKF", ci);
            //xrLabel65.Text = raport.Parameters[28].Description;
            //parameter19.Value = raport.Parameters[28].Value;

   
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
            xrLabel21.Text = rm.GetString("labelCmimi", ci);
            xrLabel12.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel17.Text = rm.GetString("labelRaportKlienti", ci);
            xrLabel13.Text = rm.GetString("labelRaportiShitjetSipasArtikujve", ci);
            xrTableCell3.Text = rm.GetString("labelRaportVlera", ci);
            xrLabel1.Text = rm.GetString("labelRaportPikaShitjes", ci);
            xrLabel5.Text = rm.GetString("labelRaportiShuma", ci);
          //  xrLabel76.Text = rm.GetString("labelRaportTotalKliente", ci);
        }

    }
}
