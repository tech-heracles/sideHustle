using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.ListPagesat.Raportet
{
    public partial class Rap_PasqyraSigSuplementare : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_PasqyraSigSuplementare(){InitializeComponent();} 
        int shifraPasPresjes = 0;
        public Rap_PasqyraSigSuplementare(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_PasqyraSigSuplementare(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, XtraReport report)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
        }
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel20.Text = rm.GetString("RaportiTilullPasqyraSigurimeveSuplementar", ci);
            xrTableCell17.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell18.Text = rm.GetString("lblEmeriMbiemeri", ci);
            xrTableCell19.Text = rm.GetString("labelRaportDetyra", ci);
            xrTableCell20.Text = rm.GetString("labelRaportGrupi", ci);
            xrTableCell21.Text = rm.GetString("labelRaportiGerma", ci);
            xrTableCell22.Text = rm.GetString("labelRaportiPaga", ci);
            xrTableCell23.Text = rm.GetString("lblsimbolperq", ci);
            xrTableCell24.Text = rm.GetString("labelRaportiKontributi", ci);
            xrTableCell1.Text = rm.GetString("lblRaportTotali",ci);
        }
      
    }
}
