using System;
using System.Globalization;
using System.Resources;
using DevExpress.XtraReports.UI;


namespace AlphaWebReports.RaportetDs
{
    public partial class Rap_kontKartelaLlogarive : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_kontKartelaLlogarive(){InitializeComponent();} 
     
        public Rap_kontKartelaLlogarive(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi,param.ScopeID, report)
        {

        }
      public Rap_kontKartelaLlogarive(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi,string scopeID, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci); 
        }
        
        private void EmrateLabelave(CultureInfo ci)
        {
           ResourceManager rm = new ResourceManager("Resources.Strings",
                     System.Reflection.Assembly.Load("App_GlobalResources"));
           

            xrLabel17.Text = rm.GetString("RaportKartelaLlogariveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell25.Text = rm.GetString("labelNrRef", ci);
            xrTableCell27.Text = rm.GetString("FilterDateRegjistrimi", ci);
            xrTableCell26.Text = rm.GetString("labelRaportiLloji", ci);
            xrTableCell28.Text = rm.GetString("labelRaportiNrDok", ci);
            xrTableCell29.Text = rm.GetString("labelRaportiDtDok", ci);
            xrTableCell30.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell39.Text = rm.GetString("labelRaportiKredi", ci);
            xrTableCell41.Text = rm.GetString("labelRaportiDebi", ci);
            xrTableCell31.Text = rm.GetString("labelRaportiGjendMonBaze", ci);
            xrTableCell32.Text = rm.GetString("labelRaportiGjendMonLlog", ci);
            xrTableCell42.Text = rm.GetString("labelRaportiDebi", ci);
            xrTableCell40.Text = rm.GetString("labelRaportiKredi", ci);
            xrTableCell46.Text = rm.GetString("labelRaportiGjendjaMePare", ci);
            xrTableCell61.Text = rm.GetString("lblLevizja", ci);
            xrTableCell79.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
            xrTableCell66.Text = rm.GetString("lblLevizjaGjithsej", ci);
            xrTableCell74.Text = rm.GetString("labelRaportiGjendjaGjithsej", ci);
            xrLabel64.Text = rm.GetString("labelLogoIMB", ci);
            
            
        }

       
    }
}
