using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;


namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_ShitjeDitoreSipasAutorizimeve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ShitjeDitoreSipasAutorizimeve(){InitializeComponent();} 

  
        public Rap_ShitjeDitoreSipasAutorizimeve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_ShitjeDitoreSipasAutorizimeve(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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

            parameter8.Value = raport.Parameters[7].Value;

            parameter9.Value = raport.Parameters[10].Value;

            parameter10.Value = raport.Parameters[12].Value;

            parameter11.Value = raport.Parameters[13].Value;

            parameter12.Value = raport.Parameters[14].Value;

            parameter13.Value = raport.Parameters[15].Value;

            parameter14.Value = raport.Parameters[16].Value;

            parameter15.Value = raport.Parameters[17].Value;

            parameter16.Value = raport.Parameters[18].Value;

            parameter17.Value = raport.Parameters[19].Value;
                        
            Monedha.Value = raport.Parameters[11].Value;
        }


        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                     System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel1.Text = rm.GetString("RaportShitjetDitoreTitulli",ci);
            //xrLabel12.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel40.Text = rm.GetString("labelRaportData", ci);
            xrLabel42.Text = rm.GetString("labelRaportKlienti", ci);
            xrLabel43.Text = rm.GetString("labelRaportiVleraTotale", ci);
            xrLabel44.Text = rm.GetString("labelRaportiVlerePatatueshme", ci);
            xrLabel45.Text = rm.GetString("labelRaportiVlereTatueshme", ci);
            xrLabel46.Text = rm.GetString("labelTVSH", ci);
            xrLabel47.Text = rm.GetString("labelRaportLikuiduar", ci);
            xrLabel48.Text = rm.GetString("labelRaportMbetje", ci);
            xrLabel49.Text = rm.GetString("labelRaportiNrFatTatimore", ci);
            xrLabel77.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel61.Text = rm.GetString("labelRaportiTeArdhuraPaTVSH", ci);
            xrLabel64.Text = rm.GetString("labelRaportiProgresiMujor", ci);
            xrLabel66.Text = rm.GetString("labelRaportiLikuidimeDitore", ci);
            xrLabel68.Text = rm.GetString("labelRaportProgresiLikuidime", ci);
            xrLabel70.Text = rm.GetString("labelRaportMbetjaProgresive", ci);
            xrLabel78.Text = rm.GetString("labelLogoIMB", ci);


        }

    }
}
