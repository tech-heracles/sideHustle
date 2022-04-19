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
    public partial class Rap_KartelaPersonaleEPages : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_KartelaPersonaleEPages(){InitializeComponent();} 

        
        public Rap_KartelaPersonaleEPages(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.ScopeID, report)
        {

        }
        public Rap_KartelaPersonaleEPages(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, string scopeID, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
        }

        private void xrLabel5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);

            xrLabel5.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_ListPagesa.aspx?scopeID=" + parametraRaporti.ScopeID + "&newScopeId=True&shtim_modifikim=modifikim&id=" + GetCurrentColumnValue("IDKOKA") + "&numer=" + GetCurrentColumnValue("NRDOK") + "')";
                this.xrLabel5.ForeColor = System.Drawing.Color.SteelBlue;
                this.xrLabel5.Target = "_self";
            
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));


            xrLabel12.Text = rm.GetString("RaportKartelaPersonalePages", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel20.Text = rm.GetString("filterKodDep", ci);
            xrLabel21.Text = rm.GetString("filterKodNenDep", ci);
            xrLabel24.Text = rm.GetString("filterPunonjesi", ci);
            xrLabel26.Text = rm.GetString("filterNumerSigurimesh", ci);
            xrLabel44.Text = rm.GetString("filterDatelindja", ci);
            xrLabel37.Text = rm.GetString("labelFilterKryesorMuaji", ci);
            xrLabel45.Text = rm.GetString("labelRaportPagaBazeBruto", ci);
            xrLabel38.Text = rm.GetString("labelRaportDitePune", ci);
            xrLabel39.Text = rm.GetString("labelRaportPagaBrutoMuajit", ci);
            xrLabel47.Text = rm.GetString("labelRaportShtesa", ci);
            xrLabel40.Text = rm.GetString("labelRaportNdalesa2", ci);
            xrLabel42.Text = rm.GetString("labelRaportPagaGjithsej", ci);
            xrLabel43.Text = rm.GetString("labelRaportSigurimetPunonjesit", ci);
            xrLabel49.Text = rm.GetString("labelRaportSigurimetNdermarrjes", ci);
            xrLabel36.Text = rm.GetString("labelRaportTatimiPaga", ci);
            xrLabel41.Text = rm.GetString("labelRaportPaguar", ci);
            xrLabel18.Text = rm.GetString("labelRaportiTotalPunonjesi", ci);
            xrLabel28.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);

        }

        private void Rap_KartelaPunonjesve_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
        }
    }
}
