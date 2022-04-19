using System;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    
    public partial class Rap_ShitjaArtikujveSipasMagazines : XtraReport
    {
        public Rap_ShitjaArtikujveSipasMagazines(){InitializeComponent();} 
        public Rap_ShitjaArtikujveSipasMagazines(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, report) { }
        public Rap_ShitjaArtikujveSipasMagazines(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport) {
            InitializeComponent();
            EmrateLabelave(ci);
          

        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            var rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));
            titulliLabel.Text = rm.GetString("RaportArtikujTeShiturSipasMagazinaveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            labelLogoIMB.Text = rm.GetString("labelLogoIMB", ci);
        }

        
    }
}
