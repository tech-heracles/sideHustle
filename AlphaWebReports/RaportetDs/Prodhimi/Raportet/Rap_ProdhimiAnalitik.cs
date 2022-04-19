using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Prodhimi.Raportet
{
    public partial class Rap_ProdhimiAnalitik : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ProdhimiAnalitik(){InitializeComponent();}

        
        public Rap_ProdhimiAnalitik(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.ScopeID, report)
        {

        }

        public Rap_ProdhimiAnalitik(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, string scopeID, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            nrLlog.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            ndermarrja.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            magazina.Text = raport.Parameters[2].Description;
            parameter4.Value = raport.Parameters[2].Value;
            kodArtikulli.Text = raport.Parameters[3].Description;
            parameter5.Value = raport.Parameters[3].Value;
            dtDok.Text = raport.Parameters[4].Description;
            parameter7.Value = raport.Parameters[4].Value;
            dtRegj.Text = raport.Parameters[5].Description;
            parameter8.Value = raport.Parameters[5].Value;
            nrDok.Text = raport.Parameters[6].Description;
            parameter9.Value = raport.Parameters[6].Value;
            this.xrLabel21.Text = raport.Parameters[7].Description;
            parameter11.Value = raport.Parameters[7].Value;
            xrLabel22.Text = raport.Parameters[8].Description;
            parameter12.Value = raport.Parameters[8].Value;
            xrLabel24.Text = raport.Parameters[9].Description;
            parameter13.Value = raport.Parameters[9].Value;
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

            xrLabel12.Text = rm.GetString("RaportRegjistriAnalitikProdhimitTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel36.Text = rm.GetString("labelRaportiNrDok", ci);
            xrLabel37.Text = rm.GetString("labelRaportiDtDok", ci);
            xrLabel38.Text = rm.GetString("labelDtRegj", ci);
            xrLabel42.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel39.Text = rm.GetString("labelRaportiShenime", ci);
            xrLabel43.Text = rm.GetString("labelFilterAvancuarMagazina", ci);
            xrLabel40.Text = rm.GetString("labelKartela", ci);
            xrLabel1.Text = rm.GetString("labelNjesia", ci);
            xrLabel2.Text = rm.GetString("labelSasia", ci);
            xrLabel14.Text = rm.GetString("labelVlefta", ci);
            xrLabel13.Text = rm.GetString("labelCmimi", ci);
            xrLabel16.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel35.Text = rm.GetString("labelLogoIMB", ci);
        }

        private void xrLabel3_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);
            xrLabel3.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_Ekzekutim.aspx?scopeID=" +  parametraRaporti.ScopeID + "&newScopeId=True&shtim_modifikim=modifikim&id=" + GetCurrentColumnValue("IDKOKA") + "&numer=" + GetCurrentColumnValue("NRDOK") + "')";
                this.xrLabel3.ForeColor = System.Drawing.Color.SteelBlue;
                xrLabel3.Target = "_self";
     
        }

        private void Rap_ProdhimiAnalitik_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
        }
    }
}
