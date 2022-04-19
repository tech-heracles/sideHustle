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
    public partial class Rap_ListpagesaPermbledheseFormat3 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ListpagesaPermbledheseFormat3(){InitializeComponent();} 
        
   
     
        public Rap_ListpagesaPermbledheseFormat3(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.ScopeID, report)
        {

        }
        public Rap_ListpagesaPermbledheseFormat3(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, string scopeID, XtraReport report)
        {
            InitializeComponent();
            EmrateLabelave(ci);
           
        }
        
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));


            xrLabel12.Text = rm.GetString("RaportPërmbledhëseListëpagesaveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell7.Text = rm.GetString("filterKodDep", ci);
            xrTableCell9.Text = rm.GetString("filterKodNenDep", ci);
            xrTableCell11.Text = rm.GetString("labelNrRendor", ci);
            xrTableCell12.Text = rm.GetString("labelRaportNrListepagese", ci);
            xrTableCell13.Text = rm.GetString("labelRaportPagaMeShtesat", ci);
            xrTableCell14.Text = rm.GetString("labelRaportNdalesatNePage", ci);
            xrTableCell15.Text = rm.GetString("labelRaportPagaNeto", ci);
            xrTableCell27.Text = rm.GetString("labelRaportPune", ci);
            xrTableCell26.Text = rm.GetString("labelFilterKryesorRaporti", ci);
            xrTableCell28.Text = rm.GetString("labelRaportTeTjera", ci);
            xrTableCell18.Text = rm.GetString("labelRaportiShuma", ci);
            xrTableCell34.Text = rm.GetString("labelRaportTatime", ci);
            xrTableCell32.Text = rm.GetString("labelRaportSigurime", ci);
            xrTableCell33.Text = rm.GetString("labelRaportTeTjera", ci);
            xrTableCell19.Text = rm.GetString("labelRaportiShuma", ci);
            xrTableCell49.Text = rm.GetString("labelRaportTotaliNendepartament", ci);
            xrTableCell59.Text = rm.GetString("labelRaportTotalDepartament", ci);
            xrTableCell62.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel81.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell79.Text = rm.GetString("labelRaportiTatimPageSek", ci);




        }
    }
}
