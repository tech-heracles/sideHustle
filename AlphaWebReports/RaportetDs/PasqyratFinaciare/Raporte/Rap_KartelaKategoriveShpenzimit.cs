using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.PasqyratFinaciare.Raporte
{
    public partial class Rap_KartelaKategoriveShpenzimit : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_KartelaKategoriveShpenzimit(){InitializeComponent();}

        

        public Rap_KartelaKategoriveShpenzimit(ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        { }
        public Rap_KartelaKategoriveShpenzimit(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            xrLabel52.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel55.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            xrLabel57.Text = raport.Parameters[3].Description;
            parameter4.Value = raport.Parameters[3].Value;
         
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


            xrLabel12.Text = rm.GetString("TitullRaportiKartelaKategoriveShpenzimit", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel6.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel1.Text = rm.GetString("labelKodi", ci) + ":";
            xrLabel3.Text = rm.GetString("lblPrindi", ci);
            xrLabel5.Text = rm.GetString("labelBlerjeShitjeEmertimi", ci);
            xrLabel10.Text = rm.GetString("labelFilterKryesorMuaji", ci);
            xrLabel15.Text = rm.GetString("lblBuxhetiMiratuar", ci);
            xrLabel17.Text = rm.GetString("lblShumaENdryshimeve", ci);
            xrLabel18.Text = rm.GetString("lblBuxhetiProgresiv", ci);
            xrLabel14.Text = rm.GetString("labelRaportiShenime", ci);
             xrLabel54.Text = rm.GetString("labelRaportiTotali", ci);

        }

        private void Rap_KartelaKategoriveShpenzimit_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
        }
    }
}
