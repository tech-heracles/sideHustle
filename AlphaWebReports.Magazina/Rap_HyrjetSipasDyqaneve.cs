using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_HyrjetSipasDyqaneve : DevExpress.XtraReports.UI.XtraReport
    {

        public Rap_HyrjetSipasDyqaneve()
        {
            InitializeComponent();
        }
        
        public Rap_HyrjetSipasDyqaneve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report)
            :this(param.Ci, param.IdNdermarrje,param.IdViti,param.IdPerdoruesi,report) { }

        public Rap_HyrjetSipasDyqaneve(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            xrLabel54.Text = raport.Parameters[0].Description;
            xrLabel55.Text = raport.Parameters[1].Description;
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
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

            //xrLabel17.Text = rm.GetString("RaportRegjistriAnalitikMagazinesTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell3.Text = rm.GetString("lblRaportiNumerDokumenti", ci);
            xrTableCell1.Text = rm.GetString("lblRaportiNumerSerial", ci);
            xrTableCell4.Text = rm.GetString("labelDateDokumenti", ci);
            xrTableCell5.Text = rm.GetString("lblRaportiKodArtikulli", ci);
            xrTableCell2.Text = rm.GetString("lblRaportMagazina", ci);
            xrTableCell7.Text = rm.GetString("labelSasia", ci);
            xrTableCell6.Text = rm.GetString("labelCmimi", ci);
            xrTableCell13.Text = rm.GetString("labelVleftapaTVSH", ci);
            xrTableCell12.Text = rm.GetString("labelShitesi", ci);
            xrLabel36.Text = rm.GetString("labelLogoIMB", ci);
        }

        private void Rap_HyrjetSipasDyqaneve_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);
        }
    }
}
