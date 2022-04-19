using System;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_ShitjetSipasKartaveTeKlientit : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ShitjetSipasKartaveTeKlientit(){InitializeComponent();} 


        public Rap_ShitjetSipasKartaveTeKlientit(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.IdRaporti, param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_ShitjetSipasKartaveTeKlientit(int idRaporti, CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[1].Value;
            parameter2.Value = raport.Parameters[3].Value;
            parameter3.Value = raport.Parameters[4].Value;
            parameter4.Value = raport.Parameters[5].Value;
            parameter5.Value = raport.Parameters[6].Value;
            parameter6.Value = raport.Parameters[7].Value;
            parameter7.Value = raport.Parameters[9].Value;
          
        }


        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            var rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));


           
          //  FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);           
          //  xrLabel18.Text = rm.GetString("labelRaportiPershkrimi", ci);
          //  xrLabel19.Text = rm.GetString("labelNjesia", ci);
          //  xrLabel20.Text = rm.GetString("labelSasia", ci);
          // xrLabel22.Text = rm.GetString("labelRaportVlefta", ci);
          //  xrLabel12.Text = rm.GetString("labelLogoIMB", ci);
          //  xrLabel17.Text = rm.GetString("labelRaportKlienti", ci);
          ////  xrLabel13.Text = rm.GetString("labelRaportShitjetSipasKlienteve", ci);
          //  xrLabel21.Text = rm.GetString("cmbCmimeArtikulliCaptionKodi", ci);
          // // xrLabel76.Text = rm.GetString("labelRaportTotalKliente", ci);

        }

    }
    }

