using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_FleteGarancia : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FleteGarancia(){InitializeComponent();} 
        public Rap_FleteGarancia(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FleteGarancia(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel2.Text = rm.GetString("RaportFormularGarancieTitulli", ci);
            xrLabel1.Text = rm.GetString("labelNrSerial_i_Formularit", ci);
            xrLabel34.Text = rm.GetString("labelRaportData", ci);
            xrLabel4.Text = rm.GetString("labelShenimMbiProduktin", ci);
            xrLabel5.Text = rm.GetString("labelKohezgjatja_e_garanciseTitulli", ci);
            xrLabel7.Text = rm.GetString("labelData_e_mbarimit", ci);
            xrLabel10.Text = rm.GetString("labelKohezgjatjaGarancisePerProdukt", ci);
            xrLabel12.Text = rm.GetString("labelInformacion_i_klientit", ci);
            xrLabel14.Text = rm.GetString("labelDyqani", ci);
            xrLabel15.Text = rm.GetString("labelEmri_i_klientit", ci);
            xrLabel16.Text = rm.GetString("labelRaportProdukti", ci);
            xrLabel13.Text = rm.GetString("labelRaportKontakti", ci);
            xrLabel17.Text = rm.GetString("labelNrKontaktit", ci);
            xrLabel18.Text = rm.GetString("labelIMEI", ci);
            xrLabel19.Text = rm.GetString("labelCmimi", ci);
            xrLabel26.Text = rm.GetString("labelSiTePerfitoniGarancineTitulli", ci);
            xrLabel27.Text = rm.GetString("labelKriteretTeGarancise", ci);
            xrLabel28.Text = rm.GetString("labelMundesiPerfitimiTeGaranciseTitull", ci);
            xrLabel29.Text = rm.GetString("labelKriterePerPerfitiminEGarancise", ci);
            xrLabel30.Text = rm.GetString("labelFirmaEPerfaqesuesitTeShitjeve", ci);
            xrLabel31.Text = rm.GetString("labelFirmaEKlientit", ci);
              
        }        

    }
}
