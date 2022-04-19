using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_PPOR_ArtikujReceptura : DevExpress.XtraReports.UI.XtraReport
    {
       
        public Rap_PPOR_ArtikujReceptura()
        {
            InitializeComponent();
        }
        public Rap_PPOR_ArtikujReceptura(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_PPOR_ArtikujReceptura(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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

            xrLabel1.Text = rm.GetString("lblKlienti", ci);
            xrLabel2.Text = rm.GetString("labelRaportNrDokumenti", ci);
            xrLabel3.Text = rm.GetString("lblDateDokumenti", ci);
            xrTableCell1.Text = rm.GetString("lblNr", ci);
            xrTableCell2.Text = rm.GetString("labelKodi", ci);
            xrTableCell3.Text = rm.GetString("labelRaportPershkrimi", ci);
            xrTableCell4.Text = rm.GetString("lblNjesia", ci);
            xrTableCell5.Text = rm.GetString("lblRaportSasia", ci);
            xrTableCell6.Text = rm.GetString("lblRaportGjendja", ci);
            xrTableCell7.Text = rm.GetString("lblDiferenca", ci);
            xrTableCell8.Text = rm.GetString("lblShenimeOp1", ci);
        }

    }


}
