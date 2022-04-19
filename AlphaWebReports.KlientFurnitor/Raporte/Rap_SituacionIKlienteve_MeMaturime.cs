using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.KlientFurnitor
{
    public partial class Rap_SituacionIKlienteve_MeMaturime : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_SituacionIKlienteve_MeMaturime(){InitializeComponent();} 

        public Rap_SituacionIKlienteve_MeMaturime(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_SituacionIKlienteve_MeMaturime(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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

            ResourceManager rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));
           

            //xrLabel17.Text = rm.GetString("RaportKartelaArtikullitTitulli", ci);
            //FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);         
            //xrLabel10.Text = rm.GetString("labelRaportKlienti", ci);
            //xrLabel14.Text = rm.GetString("labelRaportiNrDok", ci);
            //xrLabel22.Text = rm.GetString("labelRaportiDtDok", ci);
            //xrLabel23.Text = rm.GetString("labelLlojDokumenti", ci);
            //xrLabel24.Text = rm.GetString("labelNjesia", ci);
            //xrLabel35.Text = rm.GetString("labelSasia", ci);
            //xrLabel3.Text = rm.GetString("labelCmimi", ci);
            //xrLabel4.Text = rm.GetString("labelVleraPaTVSH", ci);
            //xrLabel5.Text = rm.GetString("labelTVSH", ci);
            //xrLabel6.Text = rm.GetString("labelVleraMeTVSH", ci);
            //xrLabel34.Text = rm.GetString("labelProgresiviSasi", ci);
            //xrLabel31.Text = rm.GetString("labelRaportiTotali", ci) + ":";
            //xrLabel38.Text = rm.GetString("labelTotaliArtikullit", ci);
            //xrLabel69.Text = rm.GetString("labelLogoIMB", ci);

        }
    }
}
