using System.Globalization;
using System.Resources;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs
{
    public partial class Rap_FleteKontabel : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FleteKontabel(){InitializeComponent();} 
        public Rap_FleteKontabel(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci)
        {

        }
        public Rap_FleteKontabel(CultureInfo ci)
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
            ResourceManager rm = new ResourceManager("Resources.Strings",
                     System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel1.Text = rm.GetString("labelRaportiNr", ci);
            xrLabel4.Text = rm.GetString("labelRaportData", ci);
            xrLabel6.Text = rm.GetString("labelRaportiShenime", ci);
            xrTableCell5.Text = rm.GetString("LabelRaportLlogari", ci);
            xrTableCell6.Text = rm.GetString("labelRaportiEmertimi", ci);
            xrTableCell4.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrTableCell7.Text = rm.GetString("labelKursi", ci);
            xrTableCell1.Text = rm.GetString("labelRaportiVleftaDebi", ci);
            xrTableCell2.Text = rm.GetString("labelRaportiVleftaKredi", ci);
            xrTableCell8.Text = rm.GetString("labelVleftaMonDebi", ci);
            xrTableCell3.Text = rm.GetString("labelVleftaMonKredi", ci);
            xrLabel8.Text = rm.GetString("labelRaportiTotali", ci);
            

        }
    }
}
