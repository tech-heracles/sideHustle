using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraReports.UI.PivotGrid;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_KataloguArtikujve : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_KataloguArtikujve()
        {
            InitializeComponent();
        }
        public Rap_KataloguArtikujve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_KataloguArtikujve(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, XtraReport report)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                         System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            
            Ndermarja.Value = report.Parameters[0].Value;
            DtDok.Value = report.Parameters[1].Value;
            Kartela.Value = report.Parameters[2].Value;
            PershkrimArt.Value = report.Parameters[3].Value;
            Grupim1.Value = report.Parameters[4].Value;
            Grupim2.Value = report.Parameters[5].Value;
            Grupim3.Value = report.Parameters[6].Value;
            Tac.Value = report.Parameters[7].Value;
            raporteUtil.shtoFiltra(filtraTable, null, report.Parameters);
 
        }


        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportTitulliKataloguArtikujve", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);

            labelKodi.Text = rm.GetString("labelKodi", ci);
            labelPershkrimi.Text = rm.GetString("labelRaportiEmertimi", ci);
            labelTAC.Text = rm.GetString("labelRaportiTac", ci);
            labelKosto.Text = rm.GetString("cmbCmimeArtikulliKosto", ci);
            labelRetailPrice.Text = rm.GetString("labelRaportRetailPrice", ci);
            labelMarketPrice.Text = rm.GetString("labelRaportMarketPrice", ci);
        }
    }
}
