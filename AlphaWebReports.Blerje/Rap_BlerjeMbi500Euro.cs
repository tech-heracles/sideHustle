using System;
using System.Globalization;
using System.Resources;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs.Blerje
{
    public partial class Rap_BlerjeMbi500Euro : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_BlerjeMbi500Euro(){InitializeComponent();} 
      
        public Rap_BlerjeMbi500Euro(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_BlerjeMbi500Euro(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);

         //   Param1Desc.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
        //    Param2Desc.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
        //    Param3Desc.Text = raport.Parameters[2].Description;
            parameter3.Value = raport.Parameters[2].Value;
         //   Param4Desc.Text = raport.Parameters[3].Description;
            parameter4.Value = raport.Parameters[3].Value;
          //  Param5Desc.Text = raport.Parameters[4].Description;
            parameter5.Value = raport.Parameters[4].Value;
         //   Param6Desc.Text = raport.Parameters[5].Description;
            parameter6.Value = raport.Parameters[5].Value;
          //  Param7Desc.Text = raport.Parameters[6].Description;
            parameter7.Value = raport.Parameters[6].Value;
            //degaAdminLabel.Text = raport.Parameters[7].Description;
            DegaAdministrative.Value = raport.Parameters[7].Value;
        }

      
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            var rm = new ResourceManager("Resources.Strings",
                     System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel17.Text = rm.GetString("RaportBlerjeMeVlereMbi500EuroTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);

            xrLabel3.Text = rm.GetString("labelEmriFurnizuesit", ci);
            xrLabel18.Text = rm.GetString("labelNrFiskalFurnizues", ci);
            xrLabel19.Text = rm.GetString("labelNivelTVSHFurnizuesi", ci);
            xrLabel4.Text = rm.GetString("labelAdresaFurnizuesit", ci);
            xrLabel21.Text = rm.GetString("labelKodiNACE", ci);
            xrLabel22.Text = rm.GetString("labelTotaliIFurnizimeve", ci);
            xrLabel23.Text = rm.GetString("labelVleraFurnizimeve", ci);
            xrLabel32.Text = rm.GetString("labelVleraFurnizimeveTeLiruaraNgaTVSH", ci);
            xrLabel24.Text = rm.GetString("labelQirammarjePatundshmerise", ci);
            xrLabel34.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel26.Text = rm.GetString("labelRaportiTotali", ci);
        }
    }
}
