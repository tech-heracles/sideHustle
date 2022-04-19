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
  
    public partial class RAP_Porosive : DevExpress.XtraReports.UI.XtraReport
    {
		public RAP_Porosive(){InitializeComponent();} 
        private ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
        public RAP_Porosive(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report)
        {

        }
        public RAP_Porosive(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();       
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;       
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
            parameter6.Value = raport.Parameters[5].Value;
            parameter7.Value = raport.Parameters[6].Value;
            EmrateLabelave(ci);
            parameter9.Value = raport.Parameters[8].Value;          
  
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

            xrLabel1.Text = rm.GetString("RaportIPorosiveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel3.Text = rm.GetString("labelNR_USH", ci);
            xrLabel20.Text = rm.GetString("labelNR_FSH", ci);
            xrLabel19.Text = rm.GetString("labelDataPorosise", ci);
            xrLabel46.Text = rm.GetString("labelAfatiKohor", ci);
            xrLabel17.Text = rm.GetString("labelKLIENTI", ci);
            xrLabel16.Text = rm.GetString("labelPERSHKRIMI", ci);
            xrLabel13.Text = rm.GetString("label_SASIA", ci);
            xrLabel11.Text = rm.GetString("label_CMIMI", ci);
            xrLabel10.Text = rm.GetString("labelVlera_Pa_Tvsh", ci);
            xrLabel9.Text = rm.GetString("labelTVSH", ci);
            xrLabel8.Text = rm.GetString("labelVLERA_ME_TVSH", ci);
            xrLabel7.Text = rm.GetString("labelPERFUNDUAR", ci);
            xrLabel6.Text = rm.GetString("labelRaportFaturuarUpperCase", ci);
            xrLabel5.Text = rm.GetString("labelArketuarUpperCase", ci);
            xrLabel4.Text = rm.GetString("labelKodi", ci);
            xrLabel21.Text = rm.GetString("labelShenimeUpperCase", ci);
            xrLabel22.Text = rm.GetString("labelShitesUpperCase", ci);
            xrLabel24.Text = rm.GetString("labelTOTAL_USH", ci);
            TotaliLabel.Text = rm.GetString("labelTotaliUpperCase", ci);
            xrLabel45.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel14.Text = rm.GetString("labelKodi", ci);
            xrLabel29.Text = rm.GetString("labelRaportiPRODHUAR", ci);
        }

      

    }
}
