using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;


namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_ShitjeLikujdime : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ShitjeLikujdime(){InitializeComponent();} 

  
        public Rap_ShitjeLikujdime(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_ShitjeLikujdime(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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
            parameter8.Value = raport.Parameters[8].Value;
            parameter16.Value = raport.Parameters[9].Value;                    
            parameter9.Value = raport.Parameters[10].Value;
            parameter15.Value = raport.Parameters[11].Value;
            parameter10.Value = raport.Parameters[12].Value;
            parameter11.Value = raport.Parameters[13].Value;
            parameter12.Value = raport.Parameters[14].Value;
            parameter13.Value = raport.Parameters[15].Value;
            parameter14.Value = raport.Parameters[16].Value;
            parameter22.Value = raport.Parameters["filterAgjentShitje"].Value;
            Monedha.Value = raport.Parameters[7].Value;
        }
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                     System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel1.Text = rm.GetString("RaportShitjeLikujdimeTitulli", ci);
            xrLabel31.Text = rm.GetString("labelKrijuesi", ci);
            xrLabel39.Text = rm.GetString("filterNrDokRezervime", ci);
            xrLabel40.Text = rm.GetString("labelRaportData", ci);
            xrLabel42.Text = rm.GetString("labelRaportKlienti", ci);
            xrLabel43.Text = rm.GetString("labelRaportiVleraTotale", ci);           
            xrLabel47.Text = rm.GetString("labelRaportLikuiduar", ci);
            xrLabel48.Text = rm.GetString("labelRaportMbetje", ci);
             xrLabel77.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel78.Text = rm.GetString("labelLogoIMB", ci);
        }
    }
}
