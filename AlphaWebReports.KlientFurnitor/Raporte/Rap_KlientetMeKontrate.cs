using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.KlientFurnitor
{
    public partial class Rap_KlientetMeKontrate : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_KlientetMeKontrate(){InitializeComponent();} 

        public Rap_KlientetMeKontrate(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, report)
        {

        }

        public Rap_KlientetMeKontrate(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
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

            xrLabel12.Text = rm.GetString("RaportKlientetMeKontrateTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell21.Text = rm.GetString("labelKodi", ci);
            xrTableCell22.Text = rm.GetString("labelRaportKlienti", ci);
            xrTableCell23.Text = rm.GetString("labelQyteti", ci);
            xrTableCell24.Text = rm.GetString("labelRaportTel", ci);
            xrTableCell31.Text = rm.GetString("labelDtMbarimi", ci);
            xrTableCell27.Text = rm.GetString("labelKartela", ci);
            xrTableCell26.Text = rm.GetString("labelAdministrimiKontakt", ci);
            xrTableCell28.Text = rm.GetString("labelPaFature", ci);
            xrTableCell29.Text = rm.GetString("labelPaPagese", ci);
            xrTableCell30.Text = rm.GetString("labelRaportiLloji", ci);
            xrLabel24.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell32.Text = rm.GetString("lblRaportiAgjentet", ci);
            xrTableCell33.Text = rm.GetString("labelFilterAvancuarGrupim2KF", ci);
            xrTableCell25.Text = rm.GetString("filterAktivitetiKlient", ci);
            xrTableCell34.Text = rm.GetString("labelRaportiShenime", ci); 

        }
    }
}
