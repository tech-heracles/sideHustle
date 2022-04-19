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
    public partial class Rap_Klienteteriaktivizuar : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Klienteteriaktivizuar(){InitializeComponent();} 
        int count = 0;

        public Rap_Klienteteriaktivizuar(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_Klienteteriaktivizuar(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            
        }
        private void EmrateLabelave(CultureInfo ci)
        {
            var rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel10.Text = rm.GetString("labelTitullRaportiKlienteteriktivizuar",ci);
            xrTableCell13.Text = rm.GetString("labelNrRendor", ci);
            xrTableCell14.Text = rm.GetString("cmbCmimeArtikulliCaptionKodi", ci);
            xrTableCell18.Text = rm.GetString("labelRaportAdresa", ci);
            xrTableCell15.Text = rm.GetString("labelRaportiEmertimi", ci);
            xrTableCell16.Text = rm.GetString("labelNIPT", ci);
            xrTableCell17.Text = rm.GetString("labelQyteti", ci);
            xrLabel33.Text = rm.GetString("labelLogoIMB", ci);
        }

    }
}
