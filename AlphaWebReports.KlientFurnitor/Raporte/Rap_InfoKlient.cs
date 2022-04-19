using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.KlientFurnitor.Raporte
{
    public partial class Rap_InfoKlient : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_InfoKlient(){InitializeComponent();} 
        public Rap_InfoKlient(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_InfoKlient(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
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
            xrTableCell12.Text = rm.GetString("labelKodi", ci);
            xrTableCell11.Text = rm.GetString("labelRaportiEmertimi", ci);
            xrTableCell13.Text = rm.GetString("labelRaportKontakti", ci);
            xrTableCell14.Text = rm.GetString("labelNIPT", ci);
            xrTableCell8.Text = rm.GetString("labelRaportAktiviteti", ci);
            xrTableCell16.Text = rm.GetString("labelRaportShteti", ci);
            xrTableCell17.Text = rm.GetString("labelQyteti", ci);
            xrTableCell18.Text = rm.GetString("labelRaportAdresa", ci);
            xrTableCell15.Text = rm.GetString("labelRaportTelefon", ci);
            xrTableCell20.Text = rm.GetString("lblRaportCel", ci);
            xrTableCell19.Text = rm.GetString("labelRaportFax", ci);
            xrTableCell9.Text = rm.GetString("labelRaportEmail", ci);
            xrTableCell22.Text = rm.GetString("labelGrupimKlientPare", ci);
            xrTableCell21.Text = rm.GetString("labelGrupimKlientDyte", ci);
            xrTableCell23.Text = rm.GetString("filterGrupimi3", ci);
            xrTableCell10.Text = rm.GetString("labelRaportLlogariBankare", ci);               
        }
    }
}
