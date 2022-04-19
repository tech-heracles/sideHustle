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
    public partial class Rap_SituacionPermbledhesIKlienteveNivelRaportues : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_SituacionPermbledhesIKlienteveNivelRaportues(){InitializeComponent();} 
        public Rap_SituacionPermbledhesIKlienteveNivelRaportues(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.IdRaporti, param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.IdGjuha, report)
        {

        }
        public Rap_SituacionPermbledhesIKlienteveNivelRaportues(int idRaporti, CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, int idGjuha, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            EmrateLabelave(ci, rm);
        }



        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        /// <param name="rm"></param>

        private void EmrateLabelave(CultureInfo ci, ResourceManager rm)
        {
            xrLabel12.Text = rm.GetString("RaportiSituacioniPermbledhesKlientitNivelRaportues", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel20.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell13.Text = rm.GetString("lblNr", ci);
            xrTableCell34.Text = rm.GetString("labelRaportLevizjegjatePeriudhes", ci);
            xrTableCell30.Text = rm.GetString("labelRaportiSubjekti", ci);
            xrTableCell32.Text = rm.GetString("labelRaportNdermarje", ci);
            xrTableCell44.Text = rm.GetString("labelRaportiDebi", ci);
            xrTableCell41.Text = rm.GetString("labelRaportiKredi", ci);
            xrTableCell35.Text = rm.GetString("labelRaportMbetjaDebitore", ci);
            xrTableCell43.Text = rm.GetString("labelRaportiDebi", ci);
            xrTableCell40.Text = rm.GetString("labelRaportiKredi", ci);
            xrTableCell33.Text = rm.GetString("labelRaportGjendjaneFillimPeriudhes", ci);
            xrTableCell31.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrTableCell2.Text = rm.GetString("llogariaTab", ci)+ " ";
            xrTableCell12.Text = rm.GetString("labelRaportTotal", ci) + " ";
        }
    }
}
