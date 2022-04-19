using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.KlientFurnitor.Raporte
{
    public partial class Rap_SituacionIKlienteve_MeGrupime : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_SituacionIKlienteve_MeGrupime(){InitializeComponent();} 

       public Rap_SituacionIKlienteve_MeGrupime(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, report)
        {

        }
        public Rap_SituacionIKlienteve_MeGrupime(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            
            InitializeComponent();
            EmrateLabelave(ci);
        }
       
      
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportSituacioniKlientit_MeGrupime_Titulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel20.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel54.Text = rm.GetString("labelRaportiTotaliRaportit", ci);
            xrTableCell24.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel10.Text = rm.GetString("labelNrRendor", ci);
            xrLabel1.Text = rm.GetString("labelKodi", ci);
            xrLabel2.Text = rm.GetString("labelRaportEmertimiKlientit", ci);
            xrLabel3.Text = rm.GetString("labelRaportiNrLlogari", ci);
            xrLabel5.Text = rm.GetString("labelRaportShumaDebi", ci);
            xrLabel4.Text = rm.GetString("labelRaportShumaKredi", ci);
            xrLabel6.Text = rm.GetString("labelRaportDetyrimi", ci);
            xrLabel7.Text = rm.GetString("labelRaportLimiti", ci);
            xrLabel8.Text = rm.GetString("labelRaportPesha", ci);
            xrLabel33.Text = rm.GetString("lblRaportDetyrimIMeparshem", ci);
        }



    }
}
