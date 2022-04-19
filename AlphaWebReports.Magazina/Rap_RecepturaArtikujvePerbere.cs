using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_RecepturaArtikujvePerbere : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_RecepturaArtikujvePerbere(){InitializeComponent();} 
public Rap_RecepturaArtikujvePerbere(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):this(param.Ci,param.IdNdermarrje, report){}
        public Rap_RecepturaArtikujvePerbere(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
        }
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",System.Reflection.Assembly.Load("App_GlobalResources"));


            xrLabel1.Text = rm.GetString("RaportRecepturaArtikujvePerbere", ci);

            xrTableCell1.Text = rm.GetString("lblRapRecArtikujPerberes", ci);

            xrLabel3.Text = rm.GetString("labelRaportiLloji", ci);
            xrLabel4.Text = rm.GetString("labelKodi", ci);
            xrLabel4.Text = rm.GetString("lblEmertimi", ci);
            xrLabel4.Text = rm.GetString("lblNjesia", ci);
            xrLabel4.Text = rm.GetString("lblKoef", ci);
            xrLabel4.Text = rm.GetString("labelRaportKosto", ci);

        }

    }
}
