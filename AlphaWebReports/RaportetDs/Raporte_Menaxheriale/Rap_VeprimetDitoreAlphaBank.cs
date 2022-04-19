using System.Globalization;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs.Raporte_Menaxheriale
{
    public partial class Rap_VeprimetDitoreAlphaBank : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_VeprimetDitoreAlphaBank(){InitializeComponent();}
        
        public Rap_VeprimetDitoreAlphaBank(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_VeprimetDitoreAlphaBank(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
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

        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void Rap_VeprimetDitoreAlphaBank_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.Image = AlphaWebReports.raporteUtil.MerrLogoNdermarrje(this.Extensions["ndermarrjeLogo"]);

        }

    }
}
