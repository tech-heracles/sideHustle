using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Banka.Raporte
{
    public partial class Rap_PermbledheseDetajuar_UrdherPagesave : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_PermbledheseDetajuar_UrdherPagesave(){InitializeComponent();} 


        public Rap_PermbledheseDetajuar_UrdherPagesave(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_PermbledheseDetajuar_UrdherPagesave(CultureInfo ci, int idNdermarje, int idViti, int idPerdoruesi, XtraReport report)
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
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("RaportPermbledheseDetajuarUrdherPagesaveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell7.Text = rm.GetString("labelRaportiDtDok", ci);
            xrTableCell10.Text = rm.GetString("labelRaportiNrDok", ci);
            xrTableCell8.Text = rm.GetString("labelFilterAvancuarGrupi", ci);
            xrTableCell9.Text = rm.GetString("labelRaportKapitulli", ci);
            xrTableCell11.Text = rm.GetString("labelRaportKodProgrami", ci);
            xrTableCell12.Text = rm.GetString("labelFilterAvancuarLlogEkonomike", ci);
            xrTableCell13.Text = rm.GetString("labelFilterAvancuarNenllogEkonomike", ci);
            xrTableCell14.Text = rm.GetString("labelRaportKodProj", ci);
            xrTableCell15.Text = rm.GetString("labelRaportiShuma", ci);
            xrTableCell16.Text = rm.GetString("labelRaportObjektiShpenzimit", ci);
            xrTableCell17.Text = rm.GetString("labelRaportKreditoriPerfitues", ci);
            xrTableCell18.Text = rm.GetString("labelRaportTotaliSipasLllogEkonomike", ci);
            xrTableCell19.Text = rm.GetString("labelRaportTotaliSipasKoditProgramit", ci);
            xrTableCell21.Text = rm.GetString("labelRaporttTotaliSipasKapitullit", ci);
          
            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
        }

    }
}
