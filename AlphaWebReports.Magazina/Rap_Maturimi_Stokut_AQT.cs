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
    public partial class Rap_Maturimi_Stokut_AQT : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Maturimi_Stokut_AQT(){InitializeComponent();} 
        public Rap_Maturimi_Stokut_AQT(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, report)
        {

        }
        public Rap_Maturimi_Stokut_AQT(CultureInfo ci, XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            DtDok.Value = raport.Parameters[4].Value;
            Magazina.Value = raport.Parameters[2].Value;
            Kartela.Value = raport.Parameters[3].Value;
            Grupim1.Value = raport.Parameters[13].Value;
            Grupim2.Value = raport.Parameters[14].Value;
            FurnitorArtikulli.Value = raport.Parameters[15].Value;
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel12.Text = rm.GetString("lblTitulliRaportMaturimiStokutAQT", ci);
            xrTableCell15.Text = rm.GetString("lblRaportFurnitori", ci);
            xrTableCell10.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell11.Text = rm.GetString("labelRaportDataBlerjes", ci);
            xrTableCell17.Text = rm.GetString("lblRaportSasia", ci);
            xrTableCell9.Text = rm.GetString("lblRaportAsetKodiArt", ci);
            xrTableCell12.Text = rm.GetString("labelDiteQendrimi", ci);
            xrTableCell13.Text = rm.GetString("lblRaportKodiMag", ci);
            xrTableCell14.Text = rm.GetString("lblRaportEmertimiMag", ci);
            xrLabel42.Text = rm.GetString("labelLogoIMB", ci);

        }

    }
}
