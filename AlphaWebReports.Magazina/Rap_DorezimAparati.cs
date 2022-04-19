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
    public partial class Rap_DorezimAparati : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_DorezimAparati(){InitializeComponent();} 
        public Rap_DorezimAparati(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_DorezimAparati(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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

            xrLabel2.Text = rm.GetString("RaportFormaDorezimitTeAparatitTitulli", ci);
            xrLabel14.Text = rm.GetString("labelAparatiIRiparuar", ci);
            xrLabel1.Text = rm.GetString("labelAparatIZevendesuar", ci);
            xrLabel3.Text = rm.GetString("labelDefektiJashteKushteveTeGarancise", ci);

            xrLabel9.Text = rm.GetString("label_IMEI_i_ri", ci);
            xrLabel10.Text = rm.GetString("labelDataZevendesimit", ci);
            xrLabel11.Text = rm.GetString("labelFilterAvancuarTipi", ci);

          
            xrLabel26.Text = rm.GetString("labelLexoniShenimetDhePlotesojiniPerkatesisht", ci) + ":";
            xrLabel7.Text = rm.GetString("labelAparatiCelularPersonal", ci) + ":";
            xrLabel8.Text = rm.GetString("labelVertetojSeVodafone", ci);
          
            xrLabel18.Text = rm.GetString("labelFirmaEKlientit", ci);
            xrLabel19.Text = rm.GetString("labelPranuarNga", ci);
            
        }

    }
}
