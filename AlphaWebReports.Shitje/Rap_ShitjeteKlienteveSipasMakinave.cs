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
    public partial class Rap_ShitjeteKlienteveSipasMakinave : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ShitjeteKlienteveSipasMakinave(){InitializeComponent();} 

      
        public Rap_ShitjeteKlienteveSipasMakinave(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_ShitjeteKlienteveSipasMakinave(CultureInfo ci, int idNdermarrje, int idViti,int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value =raport.Parameters[4].Value;
            parameter6.Value = raport.Parameters[5].Value;
            parameter7.Value = raport.Parameters[6].Value;
            parameter8.Value = raport.Parameters[7].Value;
            parameter9.Value = raport.Parameters[8].Value;
            parameter10.Value = raport.Parameters[9].Value;
            DegaAdministrative.Value = raport.Parameters[10].Value;
            parameter11.Value = raport.Parameters[11].Value;
            parameter12.Value = raport.Parameters[12].Value;
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
           ResourceManager rm = new ResourceManager("Resources.Strings",
                     System.Reflection.Assembly.Load("App_GlobalResources"));


           xrLabel13.Text = rm.GetString("RaportShitjetEKlienteveSipasMakinaveTitulli", ci);
           FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
           xrLabel17.Text = rm.GetString("labelKodi", ci);
           xrLabel18.Text = rm.GetString("labelEmerKlienti", ci);
           xrLabel19.Text = rm.GetString("labelRaportTarga", ci);
           xrLabel20.Text = rm.GetString("labelRaportModeli", ci);
           xrLabel23.Text = rm.GetString("labelVleftapaTVSH", ci);
           xrLabel25.Text = rm.GetString("labelVleftameTVSH", ci);
           xrLabel16.Text = rm.GetString("labelRaportiTotali", ci);
           xrLabel12.Text = rm.GetString("labelLogoIMB", ci);
        }

        private void xrLabel16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
    }
}
