using System;
using System.Globalization;
using System.Resources;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs.KlientFurnitor.Raporte
{
    public partial class Rap_Vjetersia_Detyrimeve_Klienteve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Vjetersia_Detyrimeve_Klienteve(){InitializeComponent();} 
        public Rap_Vjetersia_Detyrimeve_Klienteve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_Vjetersia_Detyrimeve_Klienteve(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
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
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            //xrLabel35.Text = rm.GetString("labelKodi", ci);
            //xrLabel36.Text = rm.GetString("labelRaportiEmertimi", ci);
            //xrLabel37.Text = rm.GetString("labelRaportKontakti", ci);
            //xrLabel38.Text = rm.GetString("labelNIPT", ci);
            //xrLabel39.Text = rm.GetString("labelRaportAktiviteti", ci);
            //xrLabel28.Text = rm.GetString("labelRaportShteti", ci);
            //xrLabel29.Text = rm.GetString("labelQyteti", ci);
            //interval1.Text = rm.GetString("labelRaportAdresa", ci);
            //interval2.Text = rm.GetString("labelRaportTelefon", ci);
            //interval3.Text = rm.GetString("lblRaportCel", ci);
            //interval4.Text = rm.GetString("labelRaportFax", ci);
            //interval5.Text = rm.GetString("labelRaportEmail", ci);
        }
     
    }
}
