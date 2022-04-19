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
    public partial class Rap_ShitjeArtikujsh : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ShitjeArtikujsh(){InitializeComponent();} 
        public Rap_ShitjeArtikujsh(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_ShitjeArtikujsh(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
            parameter7.Value = raport.Parameters[6].Value;
            parameter8.Value = raport.Parameters[7].Value;
            KlasaArtikulli.Value = raport.Parameters[10].Value;
            parameter9.Value = raport.Parameters[11].Value;
            parameter10.Value = raport.Parameters[12].Value;
            parameter11.Value = raport.Parameters[13].Value;
            parameter12.Value = raport.Parameters[15].Value;
            parameter13.Value = raport.Parameters[16].Value;
            parameter14.Value = raport.Parameters[17].Value;
            parameter15.Value = raport.Parameters[18].Value;
            parameter15.Value = raport.Parameters[19].Value;
            parameter15.Value = raport.Parameters[20].Value;
            xrPictureBox1.ImageUrl = @"/images/RaporteLogo.bmp";
                }
         /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {

            ResourceManager rm = new ResourceManager("Resources.Strings",
            System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel13.Text = rm.GetString("RaportiThjeshteArtikujveTeShiturTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel3.Text = rm.GetString("labelKodi", ci);
            xrLabel4.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel6.Text = rm.GetString("labelNjesia", ci);
            xrLabel20.Text = rm.GetString("labelSasia", ci);
            xrLabel7.Text = rm.GetString("labelCmimi", ci);
            xrLabel9.Text = rm.GetString("labelVleftapaTVSH", ci);
            xrLabel24.Text = rm.GetString("labelTVSH", ci);
            xrLabel25.Text = rm.GetString("labelShumaMeTvsh", ci);
            xrLabel22.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel11.Text = rm.GetString("labelLogoIMB", ci);
        }
    }
}

    
