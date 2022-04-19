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
    public partial class Rap_KartelaPunonjesve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_KartelaPunonjesve(){InitializeComponent();} 

        
        public Rap_KartelaPunonjesve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.ScopeID, report)
        {

        }
        public Rap_KartelaPunonjesve(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, string scopeID, DevExpress.XtraReports.UI.XtraReport raport)
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


            xrLabel12.Text = rm.GetString("RaportKartelaPunonjësveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel20.Text = rm.GetString("filterKodDep", ci);
            xrLabel21.Text = rm.GetString("filterKodNenDep", ci);
            xrLabel24.Text = rm.GetString("filterNrPersonalPunonjesi", ci);
            xrLabel41.Text = rm.GetString("labelRaportListepagesa", ci);
            xrLabel45.Text = rm.GetString("labelRaportPAGESA", ci);
            xrLabel47.Text = rm.GetString("labelRaportNDALESA", ci);
            xrLabel36.Text = rm.GetString("labelRaportiNr", ci);
            xrLabel37.Text = rm.GetString("labelFilterKryesorMuaji", ci);
            xrLabel38.Text = rm.GetString("labelRaportKomponentet", ci);
            xrLabel39.Text = rm.GetString("labelVlefta", ci);
            xrLabel40.Text = rm.GetString("labelRaportKomponentet", ci);
            xrLabel42.Text = rm.GetString("labelVlefta", ci);
            xrLabel43.Text = rm.GetString("labelRaportPagaNeto", ci);
            xrLabel7.Text = rm.GetString("labelRaportShumaPagesa", ci);
            xrLabel18.Text = rm.GetString("labelRaportiShuma", ci);
            xrLabel28.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel9.Text = rm.GetString("labelRaportShumaNdalesa", ci);
           




        }

        private void Rap_KartelaPunonjesve_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
        }
    }
}
