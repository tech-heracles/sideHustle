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
    public partial class Rap_ArtikujteshiturDateSkadenceDheSeri : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ArtikujteshiturDateSkadenceDheSeri(){InitializeComponent();} 
        public Rap_ArtikujteshiturDateSkadenceDheSeri(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_ArtikujteshiturDateSkadenceDheSeri(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
            parameter6.Value = raport.Parameters[5].Value;
            parameter7.Value = raport.Parameters[6].Value;
            parameter8.Value = raport.Parameters[7].Value;
            parameter9.Value = raport.Parameters[9].Value;
            parameter10.Value = raport.Parameters[10].Value;
            parameter11.Value = raport.Parameters[11].Value;
            parameter12.Value = raport.Parameters[13].Value;
            parameter13.Value = raport.Parameters[14].Value;
            parameter14.Value = raport.Parameters[15].Value;
            parameter16.Value = raport.Parameters[12].Value;
            parameter17.Value = raport.Parameters[16].Value;
            parameter18.Value = raport.Parameters[17].Value;
     
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));


            xrLabel13.Text = rm.GetString("labelArtikujteshiturDateSkadenceSeri", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel17.Text = rm.GetString("labelKodi", ci);
            xrLabel18.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel19.Text = rm.GetString("labelNjesia", ci);
            xrLabel4.Text = rm.GetString("labelDetajim1", ci);
            xrLabel10.Text = rm.GetString("labelDetajim2", ci);

            xrLabel20.Text = rm.GetString("labelSasia", ci);
            xrLabel21.Text = rm.GetString("labelCmimi", ci);
            xrLabel22.Text = rm.GetString("labelZbritjeAnalitike", ci);
            xrLabel23.Text = rm.GetString("labelVleraPaTVSH", ci);
            xrLabel32.Text = rm.GetString("labelZbritjaTotale", ci);
            xrLabel24.Text = rm.GetString("labelTVSH", ci);
            xrLabel25.Text = rm.GetString("labelVleraMeTVSH", ci);
            xrLabel64.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel65.Text = rm.GetString("labelLogoIMB", ci);

        }
    }
}
