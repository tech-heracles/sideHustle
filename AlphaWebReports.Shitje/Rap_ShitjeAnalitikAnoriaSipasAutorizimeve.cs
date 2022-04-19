using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_ShitjeAnalitikAnoriaSipasAutorizimeve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ShitjeAnalitikAnoriaSipasAutorizimeve(){InitializeComponent();}

        
        public Rap_ShitjeAnalitikAnoriaSipasAutorizimeve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.ScopeID,  report)
        {

        }

        public Rap_ShitjeAnalitikAnoriaSipasAutorizimeve(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, string scopeID, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            xrLabel54.Text = raport.Parameters[0].Description;
            parameter1.Value = raport.Parameters[0].Value;
            xrLabel55.Text = raport.Parameters[1].Description;
            parameter2.Value = raport.Parameters[1].Value;
            xrLabel56.Text = raport.Parameters[2].Description;
            parameter3.Value = raport.Parameters[2].Value;
            xrLabel57.Text = raport.Parameters[3].Description;
            parameter4.Value = raport.Parameters[3].Value;
            xrLabel58.Text = raport.Parameters[4].Description;
            parameter5.Value = raport.Parameters[4].Value;
            xrLabel59.Text = raport.Parameters[5].Description;
            parameter6.Value = raport.Parameters[5].Value;
            xrLabel60.Text = raport.Parameters[6].Description;
            parameter7.Value = raport.Parameters[6].Value;
            xrLabel67.Text = raport.Parameters[7].Description;
            parameter8.Value = raport.Parameters[7].Value;
            xrLabel69.Text = raport.Parameters[8].Description;
            parameter9.Value = raport.Parameters[8].Value;
            xrLabel74.Text = raport.Parameters[9].Description;
            parameter10.Value = raport.Parameters[9].Value;
            xrLabel76.Text = raport.Parameters[10].Description;
            parameter11.Value = raport.Parameters[10].Value;
            xrLabel78.Text = raport.Parameters[11].Description;
            parameter12.Value = raport.Parameters[11].Value; 
            xrLabel80.Text = raport.Parameters[13].Description;
            parameter13.Value = raport.Parameters[13].Value; 
            xrLabel82.Text = raport.Parameters[14].Description;
            parameter14.Value = raport.Parameters[14].Value;  
            xrLabel85.Text = raport.Parameters[15].Description;
            parameter15.Value = raport.Parameters[15].Value;
            degaAdminLabel.Text = raport.Parameters[12].Description;
            DegaAdministrative.Value = raport.Parameters[12].Value;
            xrLabel87.Text = raport.Parameters[16].Description;
            parameter16.Value = raport.Parameters[16].Value;
        }

        private void xrLabel11_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);

            if (GetCurrentColumnValue("IDSHITJEKOKA") != null && GetCurrentColumnValue("NRDOK") != null)
            {
                xrLabel11.NavigateUrl = "javascript:window.myFaqeCelje.hapFaqeNeTabTeRi('Shto_RegjistrimDokumentash.aspx?scopeID=" + parametraRaporti.ScopeID + "&newScopeId=True&shitje_blerje=shitje&id=" + GetCurrentColumnValue("IDSHITJEKOKA").ToString() + "&numer=" + GetCurrentColumnValue("NRDOK").ToString() + "&shtim_modifikim=modifikim')";
                this.xrLabel11.ForeColor = System.Drawing.Color.SteelBlue;
                xrLabel11.Target = "_self";
            }
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
          ResourceManager  rm = new ResourceManager("Resources.Strings",
                     System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel17.Text = rm.GetString("RaportRegjistriAnalitikShitjeveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel10.Text = rm.GetString("labelDokumentArtikulli", ci);
            xrLabel3.Text = rm.GetString("labelKodi", ci);
            xrLabel4.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel7.Text = rm.GetString("labelNjesia", ci);
            xrLabel8.Text = rm.GetString("labelSasia", ci);
            xrLabel13.Text = rm.GetString("labelVlefta", ci);
            //xrLabel9.Text = rm.GetString("labelCmimi", ci);
            xrLabel14.Text = rm.GetString("labelGjithsej", ci);
            xrLabel15.Text = rm.GetString("labelZbritjeAnalitike", ci);
            xrLabel23.Text = rm.GetString("labelVleftapaTVSH", ci);
            xrLabel19.Text = rm.GetString("labelZbritjaTotale", ci);
            xrLabel24.Text = rm.GetString("labelVleftameZbritje", ci);
            xrLabel21.Text = rm.GetString("labelMonLlogari", ci);
            xrLabel22.Text = rm.GetString("labelMonBaze", ci);
            xrLabel44.Text = rm.GetString("labelShumaPaTvsh", ci);
            xrLabel45.Text = rm.GetString("labelTVSH", ci);
            xrLabel46.Text = rm.GetString("labelShumaMeTvsh", ci);
            xrLabel66.Text = rm.GetString("labelShumaPaTvsh", ci);
            xrLabel64.Text = rm.GetString("labelTVSH", ci);
            xrLabel65.Text = rm.GetString("labelShumaMeTvsh", ci);

            xrLabel43.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel89.Text = rm.GetString("labelRaportGjatesi", ci);
            xrLabel88.Text = rm.GetString("labelRaportGjeresi", ci);

        }

        private void Rap_ShitjeAnalitikAnoriaSipasAutorizimeve_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);

        }
    }
}
