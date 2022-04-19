using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.KlientFurnitor.Raporte
{
    public partial class Rap_SituacionIKlienteve_Ekspozim : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_SituacionIKlienteve_Ekspozim(){InitializeComponent();} 

        public Rap_SituacionIKlienteve_Ekspozim(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, report)
        {

        }
        public Rap_SituacionIKlienteve_Ekspozim(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
           
        }

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportSituacioniKlientitEkspozimTitulli", ci);
            xrLabel1.Text = rm.GetString("FiltratEmertimi", ci);
           
            xrLabel20.Text = rm.GetString("labelLogoIMB", ci);

            //header
            xrTableCell23.Text = rm.GetString("labelRaportNrRendor", ci);
            xrTableCell24.Text = rm.GetString("labelRaportGrupi", ci);
            xrTableCell26.Text = rm.GetString("label_KODI", ci);
            xrTableCell27.Text = rm.GetString("label_KODI", ci) + " 2";
            xrTableCell25.Text = rm.GetString("labelRaportEmertimiKlientit", ci);
            xrTableCell28.Text = rm.GetString("labelNrLlogarie", ci);
            xrTableCell29.Text = rm.GetString("lblRaportDetyrimIMeparshem", ci);
            xrTableCell30.Text = rm.GetString("labelRaportShumaDebi", ci);
            xrTableCell31.Text = rm.GetString("labelRaportShumaKredi", ci);
            xrTableCell32.Text = rm.GetString("labelRaportDetyrimi", ci);
            xrTableCell33.Text = rm.GetString("labelRaportLimitiParalajmerues", ci);
            xrTableCell35.Text = rm.GetString("labelRaportiDiferenca", ci);
            xrTableCell34.Text = rm.GetString("labelRaportiShenja", ci);
            xrTableCell14.Text = rm.GetString("labelRaportiTotali", ci);

        }
     
    }
}
