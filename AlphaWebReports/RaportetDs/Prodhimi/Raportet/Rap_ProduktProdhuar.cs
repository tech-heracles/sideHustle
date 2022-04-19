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
    public partial class Rap_ProduktProdhuar : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ProduktProdhuar(){InitializeComponent();}
        
        public Rap_ProduktProdhuar(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_ProduktProdhuar(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);

            xrLabel52.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel57.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            xrLabel63.Text = raport.Parameters[2].Description;
            parameter3.Value = raport.Parameters[2].Value;
            xrLabel46.Text = raport.Parameters[3].Description;
            parameter4.Value = raport.Parameters[3].Value;
            xrLabel60.Text = raport.Parameters[4].Description;
            parameter5.Value = raport.Parameters[4].Value;

            xrLabel64.Text = raport.Parameters[5].Description;
            parameter6.Value = raport.Parameters[5].Value;


            xrLabel48.Text = raport.Parameters[6].Description;
            parameter7.Value = raport.Parameters[6].Value;   
            xrLabel10.Text = raport.Parameters[7].Description;
            parameter8.Value = raport.Parameters[7].Value;
            xrLabel13.Text = raport.Parameters[8].Description;
            parameter9.Value = raport.Parameters[8].Value; 
            xrLabel15.Text = raport.Parameters[9].Description;
            parameter10.Value = raport.Parameters[9].Value; 
            xrLabel19.Text = raport.Parameters[10].Description;
            parameter11.Value = raport.Parameters[10].Value; 
            xrLabel18.Text = raport.Parameters[11].Description;
            parameter12.Value = raport.Parameters[11].Value; 
            xrLabel24.Text = raport.Parameters[12].Description;
            parameter13.Value = raport.Parameters[12].Value;

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
            xrLabel12.Text = rm.GetString("RaportProduktProdhuarTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel36.Text = rm.GetString("labelKodi", ci);
            xrLabel37.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel38.Text = rm.GetString("labelNjesia", ci);
            xrLabel40.Text = rm.GetString("labelSasia", ci);
            xrLabel39.Text = rm.GetString("labelRaportLLogariInventar", ci);
            xrLabel42.Text = rm.GetString("labelRaportKosto", ci);
            xrLabel1.Text = rm.GetString("labelVlefta", ci);
            xrLabel23.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
        }

        private void Rap_ProduktProdhuar_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);

        }
    }
}
