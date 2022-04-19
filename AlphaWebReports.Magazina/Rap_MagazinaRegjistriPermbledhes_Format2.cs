using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_MagazinaRegjistriPermbledhes_Format2 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_MagazinaRegjistriPermbledhes_Format2(){InitializeComponent();} 
        public Rap_MagazinaRegjistriPermbledhes_Format2(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }

        public Rap_MagazinaRegjistriPermbledhes_Format2(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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
            parameter9.Value = raport.Parameters[8].Value;     
            parameter10.Value = raport.Parameters[9].Value;
            parameter11.Value = raport.Parameters[11].Value;
            parameter12.Value = raport.Parameters[12].Value;
            parameter13.Value = raport.Parameters[13].Value;
            DegaAdministrative.Value = raport.Parameters[10].Value;
            parameter14.Value = raport.Parameters[14].Value;
            parameter15.Value = raport.Parameters[15].Value;
            parameter16.Value = raport.Parameters[16].Value;
            parameter17.Value = raport.Parameters[17].Value;
       
        }


        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel17.Text = rm.GetString("RaportRegjstriPermbledhesMagazinesTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel15.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel20.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
            xrLabel32.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel4.Text = rm.GetString("labelRaportMagazina", ci);
            xrLabel46.Text = rm.GetString("labelRaportiLloji", ci);
            xrLabel47.Text = rm.GetString("labelRaportNrDokument", ci);
            xrLabel49.Text = rm.GetString("labelRaportDtDokument", ci);
            xrLabel45.Text = rm.GetString("labelRaportDtRegjistrim", ci);
            xrLabel41.Text = rm.GetString("labelPershkrimVeprimi", ci);
            xrLabel40.Text = rm.GetString("labelVlefta", ci);
        }
    }
}
