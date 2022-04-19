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
    public partial class Rap_KontrolliPorosive : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_KontrolliPorosive()
        {
            InitializeComponent();
        }
        public Rap_KontrolliPorosive(AlphaWebReports.Common.ParametraRaporti param, XtraReport report)
            :this(param.Ci, param.IdNdermarrje,param.IdViti,param.IdPerdoruesi, report) { }
        
        public Rap_KontrolliPorosive(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            xrLabel52.Text = raport.Parameters[4].Description;
            parameter1.Value = raport.Parameters[4].Value;
            xrLabel55.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            xrLabel5.Text = raport.Parameters[2].Description;
            parameter3.Value = raport.Parameters[2].Value;
            xrLabel6.Text = raport.Parameters[3].Description;
            parameter4.Value = raport.Parameters[3].Value;
           
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
            
        }


        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel18.Text = rm.GetString("labelKartela", ci);
            xrLabel19.Text = rm.GetString("labelRaportPershkrimi", ci);
            xrLabel22.Text = rm.GetString("labelKategoria", ci);
            xrLabel23.Text = rm.GetString("labelRaportNenKategoria", ci);
            xrLabel69.Text = rm.GetString("labelLogoIMB", ci);
        }

        private void Rap_KontrolliPorosive_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
        }
    }
}
