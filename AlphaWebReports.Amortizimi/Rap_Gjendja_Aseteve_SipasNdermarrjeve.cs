using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.Amortizimi
{
    public partial class Rap_Gjendja_Aseteve_SipasNdermarrjeve : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_Gjendja_Aseteve_SipasNdermarrjeve(ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdViti, report){ }
        public Rap_Gjendja_Aseteve_SipasNdermarrjeve() { InitializeComponent(); }
        public Rap_Gjendja_Aseteve_SipasNdermarrjeve(CultureInfo ci, int IdNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)

        {
           
            InitializeComponent();
            parameter1.Value = raport.Parameters["filterpershkrimArt"].Value;
            parameter2.Value = raport.Parameters["filterDtDok1"].Value;
            parameter3.Value = raport.Parameters["filterDtDok2"].Value;
            parameter4.Value = raport.Parameters["filterkodifikimartP"].Value;
            parameterIdNderm.Value = IdNdermarrje;
            String grup1 = Convert.ToString(raport.Parameters["filterkodifikimartD"].Value);
            parameter7.Value = raport.Parameters[6].Value;
            parameter6.Value = raport.Parameters["filterDtDok"].Value.ToString().Split('-')[1];
            parameter8.Value = raport.Parameters[8].Value;
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
            xrLabel17.Text = rm.GetString("labelRaportGjendjaAseteveNdermarrjeTitull", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel37.Text = rm.GetString("labelKodi", ci);
            xrLabel23.Text = rm.GetString("labelRaportPershkrimi", ci);
            xrLabel29.Text = rm.GetString("filterSeriali", ci);
            xrLabel28.Text = rm.GetString("labelFooterNdermarrja", ci);
            xrLabel36.Text = rm.GetString("labelPershkrimiNd", ci);
          //  xrLabel30.Text = rm.GetString("labelRaportStatusMagazina", ci);
            xrLabel27.Text = rm.GetString("labelRaportGrupi", ci);
            xrLabel25.Text = rm.GetString("labelRaportDataBlerjes", ci);
            xrLabel31.Text = rm.GetString("labelRaportDtFillimAmort", ci);
            xrLabel24.Text = rm.GetString("labelRaportNorma", ci);
            xrLabel33.Text = rm.GetString("labelRaportGjendjeFillestare", ci);
            xrLabel40.Text = rm.GetString("labelRaportShtesaViti", ci);
            xrLabel12.Text = rm.GetString("lblRaportTotalGjendje", ci);
            xrLabel14.Text = rm.GetString("labelRaportAmortAkum", ci) + ":";
            xrLabel15.Text = rm.GetString("labelRaportShtesaVitiAm", ci);
            xrLabel10.Text = rm.GetString("labelRaportTotalAmort", ci);
            xrLabel16.Text = rm.GetString("labelRaportVlMbeturFill", ci);
            xrLabel18.Text = rm.GetString("labelRaportVlMbetur", ci);
            xrLabel71.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel32.Text = rm.GetString("labelRaportGrupiCategory", ci) + ":";
        }
        
    }
}
