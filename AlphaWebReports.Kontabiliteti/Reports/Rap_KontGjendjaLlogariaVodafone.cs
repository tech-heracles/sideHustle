using System;
using System.Globalization;
using System.Resources;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs
{
    public partial class Rap_KontGjendjaLlogariaVodafone : DevExpress.XtraReports.UI.XtraReport
    {
        double debimonbaze = 0.0;
        double kredimonbaze = 0.0;
        private static string formatstring = "{0:#,#.00}";
        string windowWidth = "";
        

        public Rap_KontGjendjaLlogariaVodafone(){
            InitializeComponent();
        }
        public Rap_KontGjendjaLlogariaVodafone(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen, param.IdViti, param.IdSubRaporti, param.ScopeID, report)
        {

        }
        public Rap_KontGjendjaLlogariaVodafone(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, string scopeID, DevExpress.XtraReports.UI.XtraReport raport)
        {
           
            InitializeComponent();
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
            xrLabel18.Text = raport.Parameters[7].Description;
            parameter8.Value = raport.Parameters[7].Value;
            azhornimLabel.Text = raport.Parameters[8].Description;
            Azhornim.Value = raport.Parameters[8].Value;
            xrLabel25.Text = raport.Parameters[10].Description;
            parameter9.Value = raport.Parameters[10].Value;
            windowWidth = Convert.ToString((object)raport.Parameters[9].Value);
            EmrateLabelave(ci);
        }
        
        private void xrLabel14_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            debimonbaze = Convert.ToDouble(xrLabel14.Summary.GetResult());
        }

        private void xrLabel16_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            kredimonbaze = Convert.ToDouble(xrLabel16.Summary.GetResult());
        }

        private void xrLabel36_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (debimonbaze-kredimonbaze>=0)
                xrLabel36.Text = String.Format("{0:#,#.00}", debimonbaze - kredimonbaze);
            else
                xrLabel36.Text = String.Format("{0:#,#.00}", 0);
        }

        private void xrLabel37_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (debimonbaze - kredimonbaze <= 0)
                xrLabel37.Text = String.Format("{0:#,#.00}", kredimonbaze - debimonbaze);
            else
                xrLabel37.Text = String.Format("{0:#,#.00}", 0);
        }

        private void xrLabel4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var parametraRaporti = AlphaWebReports.raporteUtil.DeserializoParametraPerKonstruktorRaporti(this.Extensions["parametraRaporti"]);
            xrLabel4.NavigateUrl = "javascript:window.parent.myFaqeCelje.hapFaqeNeTabTeRi('RaportiShpejte.aspx?scopeID=" + parametraRaporti.ScopeID + "&guidString=" + parametraRaporti.GuidString + "&idraporti=" + parametraRaporti.IdSubRaporti + "&filterNumerLlogarie=" + GetCurrentColumnValue("NRLLOGARI") + "&printo=0&Sesioni=true')";
                xrLabel4.Target = "_self";
           
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
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel17.Text = rm.GetString("RaportLevizjeteLlogariveTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel8.Text = rm.GetString("labelRaportiNrLlogari", ci);
            xrLabel27.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel9.Text = rm.GetString("filterRaportEmerLlogarie", ci);
            xrLabel10.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrLabel15.Text = rm.GetString("labelRaportiGjendMonBaze", ci);
            xrLabel2.Text = rm.GetString("labelRaportiGjendMonLlog", ci);
            xrLabel12.Text = rm.GetString("labelRaportiKredi", ci);
            xrLabel11.Text = rm.GetString("labelRaportiDebi", ci);
            xrLabel3.Text = rm.GetString("labelRaportiDebi", ci);
            xrLabel1.Text = rm.GetString("labelRaportiKredi", ci);
            xrLabel19.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel38.Text = rm.GetString("filterRaportLevizje", ci);
        }

        private void Rap_KontGjendjaLlogariaVodafone_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
        }
    }
}
