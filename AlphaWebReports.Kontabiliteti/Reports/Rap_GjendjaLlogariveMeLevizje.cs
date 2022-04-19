using System.Globalization;
using System.Resources;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs
{
    public partial class Rap_GjendjaLlogariveMeLevizje : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_GjendjaLlogariveMeLevizje()
        {
            InitializeComponent();
        }

        
        public Rap_GjendjaLlogariveMeLevizje(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_GjendjaLlogariveMeLevizje(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
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
            param8Label.Text = raport.Parameters[7].Description;
            parameter8.Value = raport.Parameters[7].Value;
            param9Label.Text = raport.Parameters[8].Description;
            parameter9.Value = raport.Parameters[8].Value;
            xrLabel21.Text = raport.Parameters[9].Description;
            parameter10.Value = raport.Parameters[9].Value;
            EmrateLabelave(ci);

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
           
            xrLabel17.Text = rm.GetString("RaportLevizjaLlogariveDKTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel8.Text = rm.GetString("labelRaportiNrLlogari", ci);
            xrLabel32.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel9.Text = rm.GetString("filterRaportEmerLlogarie", ci);
            xrLabel10.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
             xrLabel7.Text = rm.GetString("labelRaportiKredi", ci);
            xrLabel28.Text = rm.GetString("labelRaportiDebi", ci);
            xrLabel15.Text = rm.GetString("labelRaportiDebi", ci);
            xrLabel14.Text = rm.GetString("labelRaportiKredi", ci);
            xrLabel4.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
           xrLabel19.Text = rm.GetString("labelRaportiTotali", ci);
           xrLabel6.Text = rm.GetString("filterRaportLevizje", ci);
           
           





        }

        private void Rap_GjendjaLlogariveMeLevizje_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);

        }
    }
}
