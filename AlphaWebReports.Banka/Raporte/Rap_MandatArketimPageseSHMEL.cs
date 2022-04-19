using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Banka.Raporte
{
    public partial class Rap_MandatArketimPageseSHMEL : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_MandatArketimPageseSHMEL(){InitializeComponent();}

        private ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
        private CultureInfo ci;

        public Rap_MandatArketimPageseSHMEL(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje)
        {

        }
        public Rap_MandatArketimPageseSHMEL(CultureInfo ci, int idNdermarrje)
        {
            this.ci = ci;
            InitializeComponent();
            EmrateLabelave(ci);
        }


        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            xrLabel2.Text = rm.GetString("labelRaportUrdhesPagese", ci);
            xrLabel3.Text = rm.GetString("labelPageseFurnitori", ci);
            xrTableCell1.Text = rm.GetString("labelRaportiFurnitori", ci);
            xrTableCell3.Text = rm.GetString("labelRaportNrLlogarise", ci) + ":";
            xrTableCell5.Text = rm.GetString("labelBlerjeShitjePeriudha", ci);
            xrTableCell7.Text = rm.GetString("labelAfatiLikujdim", ci) + ":";
            xrTableCell9.Text = rm.GetString("labelRaportiNrUpperCase", ci);
            xrTableCell10.Text = rm.GetString("labelNrFatures", ci);
            xrTableCell11.Text = rm.GetString("labelDtFatureUpperCase", ci);
            xrTableCell12.Text = rm.GetString("labelPershkrimFatureUpperCase", ci);
            xrTableCell13.Text = rm.GetString("labelDestinacioni", ci);
            xrTableCell14.Text = rm.GetString("labelVleraUpperCase", ci);
            xrLabel4.Text = rm.GetString("labelRaportTotaluUppercase", ci);
            xrLabel6.Text = rm.GetString("labelShmelExpress", ci);
            xrLabel7.Text = rm.GetString("labelRaportPresident", ci) + ":";
            xrLabel8.Text = rm.GetString("labelRaportDrejtoriPergj", ci) + ":";
            xrLabel9.Text = rm.GetString("labelRaportFinancier", ci) + ":";
            xrLabel13.Text = rm.GetString("labelRaportDt", ci);
        }
        
    }
}
