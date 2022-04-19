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
    public partial class Rap_ShitjeAnalitikAutoElite : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ShitjeAnalitikAutoElite(){InitializeComponent();} 
      
       
        public Rap_ShitjeAnalitikAutoElite(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_ShitjeAnalitikAutoElite(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
            parameter6.Value = raport.Parameters[5].Value;
            parameter7.Value = raport.Parameters[6].Value;
            parameter8.Value = raport.Parameters[7].Value;
            parameter9.Value = raport.Parameters[8].Value;
            parameter10.Value = raport.Parameters[9].Value;
            parameter11.Value = raport.Parameters[10].Value;
            parameter12.Value = raport.Parameters[11].Value; 
            parameter13.Value = raport.Parameters[13].Value; 
            parameter14.Value = raport.Parameters[14].Value;  
            parameter15.Value = raport.Parameters[15].Value;
            DegaAdministrative.Value = raport.Parameters[12].Value;
            parameter16.Value = raport.Parameters[16].Value;
            parameter17.Value = raport.Parameters[17].Value;
            parameter18.Value = raport.Parameters[18].Value;
            parameter19.Value = raport.Parameters[19].Value;
            parameter20.Value = raport.Parameters[20].Value;
            parameter21.Value = raport.Parameters[21].Value;
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
            xrLabel1.Text = rm.GetString("lblNr",  ci) + ":";
            xrLabel2.Text = rm.GetString("lblKlienti" , ci) + ":";
            xrLabel19.Text = rm.GetString("labelZbritjaTotale", ci) + " %";
            xrLabel15.Text = rm.GetString("labelZbritjeAnalitike", ci) + " %";
            xrLabel20.Text = rm.GetString("labelVleftameZbritje", ci);
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
            xrLabel43.Text = rm.GetString("labelLogoIMB", ci);
        }
    }
}
