using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Prodhimi.Raportet
{
    public partial class Rap_Prodhimi_Univers : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Prodhimi_Univers(){InitializeComponent();} 
        ResourceManager rm = new ResourceManager("Resources.Strings",
                         System.Reflection.Assembly.Load("App_GlobalResources"));
        public Rap_Prodhimi_Univers(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, report)
        { }
        public Rap_Prodhimi_Univers(CultureInfo ci, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            xrLabel57.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            //i vendoset pershkrim tjeter nga ai i filtrit
            xrLabel63.Text = rm.GetString("filterRaportiNrPorosie", ci); 
            parameter2.Value = raport.Parameters[1].Value;
            xrLabel52.Text = raport.Parameters[2].Description;
            parameter3.Value = raport.Parameters[2].Value;
            xrLabel46.Text = raport.Parameters[3].Description;
            parameter4.Value = raport.Parameters[3].Value;
            xrLabel60.Text = rm.GetString("filterRaportiNrProdhimi", ci);
            parameter5.Value = raport.Parameters[4].Value;
         }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
           

            xrLabel12.Text = rm.GetString("RaportProduktetSipasPorosiveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel7.Text = rm.GetString("labelNrPorosie", ci);
            xrLabel18.Text = rm.GetString("labelRaportData", ci);
            xrLabel17.Text = rm.GetString("comboItemBlerjeShitjeKlient", ci);
            xrLabel16.Text = rm.GetString("labelMenaxher", ci);
            xrLabel15.Text = rm.GetString("labelNrProdhimi", ci);
            xrLabel5.Text = rm.GetString("labelDtProdhimi", ci);
            xrLabel14.Text = rm.GetString("labelKodProd", ci);
            xrLabel2.Text = rm.GetString("labelEmertimProd", ci);
            xrLabel13.Text = rm.GetString("labelSasiProdukti", ci);
            xrLabel11.Text = rm.GetString("labelNjesia", ci);
            xrLabel1.Text = rm.GetString("labelKodiBM", ci);
            xrLabel10.Text = rm.GetString("labelPershkrimiBM", ci);
            xrLabel9.Text = rm.GetString("labelSasite", ci);
            xrLabel8.Text = rm.GetString("labelNjesia", ci);
            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
        }
    }
}
