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
    public partial class Rap_SituacionIKlienteveSipasMuajve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_SituacionIKlienteveSipasMuajve(){InitializeComponent();} 
        public Rap_SituacionIKlienteveSipasMuajve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report)
        {
        }

        public Rap_SituacionIKlienteveSipasMuajve(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport report)
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
            xrLabel12.Text = rm.GetString("labelRaportiSituacioniKlientitSipasMuajveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel20.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell77.Text = rm.GetString("labelRaportEmertimiKlientit", ci);
            xrTableCell78.Text = rm.GetString("labelRaportiGjendjaFillimMuaji", ci);
            xrTableCell41.Text = rm.GetString("labelRaportiFaturaShitjeDebi", ci);
            xrTableCell42.Text = rm.GetString("labelRaportiLikuidimiKredi", ci);
            xrTableCell43.Text = rm.GetString("lblRaportiGjendjaFund", ci);
            xrTableCell44.Text = rm.GetString("labelRaportiFaturaShitjeDebi", ci);
            xrTableCell45.Text = rm.GetString("labelRaportiLikuidimiKredi", ci);
            xrTableCell46.Text = rm.GetString("lblRaportiGjendjaFund", ci);
            xrTableCell47.Text = rm.GetString("labelRaportiFaturaShitjeDebi", ci);
            xrTableCell48.Text = rm.GetString("labelRaportiLikuidimiKredi", ci);
            xrTableCell49.Text = rm.GetString("lblRaportiGjendjaFund", ci);
            xrTableCell50.Text = rm.GetString("labelRaportiFaturaShitjeDebi", ci);
            xrTableCell51.Text = rm.GetString("labelRaportiLikuidimiKredi", ci);
            xrTableCell52.Text = rm.GetString("lblRaportiGjendjaFund", ci);
            xrTableCell53.Text = rm.GetString("labelRaportiFaturaShitjeDebi", ci);
            xrTableCell54.Text = rm.GetString("labelRaportiLikuidimiKredi", ci);
            xrTableCell55.Text = rm.GetString("lblRaportiGjendjaFund", ci);
            xrTableCell56.Text = rm.GetString("labelRaportiFaturaShitjeDebi", ci);
            xrTableCell57.Text = rm.GetString("labelRaportiLikuidimiKredi", ci);
            xrTableCell58.Text = rm.GetString("lblRaportiGjendjaFund", ci);
            xrTableCell59.Text = rm.GetString("labelRaportiFaturaShitjeDebi", ci);
            xrTableCell60.Text = rm.GetString("labelRaportiLikuidimiKredi", ci);
            xrTableCell61.Text = rm.GetString("lblRaportiGjendjaFund", ci);
            xrTableCell62.Text = rm.GetString("labelRaportiFaturaShitjeDebi", ci);
            xrTableCell63.Text = rm.GetString("labelRaportiLikuidimiKredi", ci);
            xrTableCell64.Text = rm.GetString("lblRaportiGjendjaFund", ci);
            xrTableCell65.Text = rm.GetString("labelRaportiFaturaShitjeDebi", ci);
            xrTableCell66.Text = rm.GetString("labelRaportiLikuidimiKredi", ci);
            xrTableCell67.Text = rm.GetString("lblRaportiGjendjaFund", ci);
            xrTableCell68.Text = rm.GetString("labelRaportiFaturaShitjeDebi", ci);
            xrTableCell69.Text = rm.GetString("labelRaportiLikuidimiKredi", ci);
            xrTableCell70.Text = rm.GetString("lblRaportiGjendjaFund", ci);
            xrTableCell71.Text = rm.GetString("labelRaportiFaturaShitjeDebi", ci);
            xrTableCell72.Text = rm.GetString("labelRaportiLikuidimiKredi", ci);
            xrTableCell73.Text = rm.GetString("lblRaportiGjendjaFund", ci);
            xrTableCell74.Text = rm.GetString("labelRaportiFaturaShitjeDebi", ci);
            xrTableCell75.Text = rm.GetString("labelRaportiLikuidimiKredi", ci);
            xrTableCell76.Text = rm.GetString("lblRaportiGjendjaFund", ci);

            xrTableCell79.Text = rm.GetString("labelRaportJanar", ci);
            xrTableCell80.Text = rm.GetString("labelRaportShkurt", ci);
            xrTableCell81.Text = rm.GetString("labelRaportMars", ci);
            xrTableCell82.Text = rm.GetString("labelRaportPrill", ci);
            xrTableCell83.Text = rm.GetString("labelRaportMaj", ci);
            xrTableCell84.Text = rm.GetString("labelRaportQershor", ci);
            xrTableCell85.Text = rm.GetString("labelRaportKorrik", ci);
            xrTableCell86.Text = rm.GetString("labelRaportGusht", ci);
            xrTableCell87.Text = rm.GetString("labelRaportShtator", ci);
            xrTableCell88.Text = rm.GetString("labelRaportTetor", ci);
            xrTableCell89.Text = rm.GetString("labelRaportNentor", ci);
            xrTableCell90.Text = rm.GetString("labelRaportDhjetor", ci);

            xrTableCell91.Text = rm.GetString("labelRaportiTotali", ci);
        }
        

    }
}
