using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Magazina.Format_Printimi
{
    public partial class Rap_Format_Printimi_FD_Vodafone : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Format_Printimi_FD_Vodafone(){InitializeComponent();} 
        public Rap_Format_Printimi_FD_Vodafone(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_Format_Printimi_FD_Vodafone(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            
            xrLabel1.Text = rm.GetString("filterRaportIdNdermarje", ci);
            xrLabel3.Text = rm.GetString("labelFilterAvancuarMagazina", ci) + ":";
            xrLabel6.Text = rm.GetString("lbelRaportiNumriIDokumentit", ci) + ":";
            xrLabel7.Text = rm.GetString("labelDataeDokumentit", ci);
            xrLabel4.Text = rm.GetString("labelRaportiNr", ci);
            xrLabel5.Text = rm.GetString("lblKodiArt", ci);
            xrLabel8.Text = rm.GetString("filterRaportPershkrimiArtikullit", ci);
            xrLabel11.Text = rm.GetString("labelNjesiaMatjes", ci);
            xrLabel12.Text = rm.GetString("labelSasia", ci);
            xrLabel13.Text = rm.GetString("labelCmimi", ci);
            xrLabel14.Text = rm.GetString("labelVlefta", ci);
            xrLabel23.Text = rm.GetString("labelRaportiTotali", ci);
        }
    }
}
