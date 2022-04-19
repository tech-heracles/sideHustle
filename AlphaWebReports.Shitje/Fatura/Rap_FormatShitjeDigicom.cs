using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FormatShitjeDigicom : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatShitjeDigicom(){InitializeComponent();} 
        private ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
       
        public Rap_FormatShitjeDigicom(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatShitjeDigicom(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel2.Text = rm.GetString("lbltitullFatureShtijeDigicom", ci);
            xrLabel6.Text = rm.GetString("lblNrRegj", ci);
            xrLabel7.Text = rm.GetString("lblSerialiFatures", ci);
            xrLabel1.Text = rm.GetString("lblDataFatures", ci);
            xrLabel40.Text = rm.GetString("lblPeriudhaFaturimit", ci);
            xrLabel41.Text = rm.GetString("MenuItemMonedhat", ci);
            xrLabel42.Text = rm.GetString("labelKursi", ci);
            xrLabel9.Text = rm.GetString("lblAdrsDigicom", ci);
            xrLabel10.Text = rm.GetString("lblDigicomTel", ci);
            xrLabel56.Text = rm.GetString("lblKujdesiDigicom", ci);
            xrLabel59.Text = rm.GetString("niptDigicom", ci);
            xrLabel61.Text = rm.GetString("labelAdministrimiKodiFiskal", ci) + ":";
            xrLabel67.Text = rm.GetString("labelInformacion_i_klientit", ci);
            xrLabel66.Text = rm.GetString("label_Emri", ci);
            xrLabel65.Text = rm.GetString("lblRaportAdresa", ci);
            xrLabel64.Text = rm.GetString("lblNrkontakti", ci);
            xrLabel63.Text = rm.GetString("labelAdministrimiEmail", ci);
            xrLabel62.Text = rm.GetString("labelAdministrimiNIPT", ci);
            xrLabel69.Text = rm.GetString("labelAdministrimiKodiFiskal", ci)+":";

            xrTableCell4.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell5.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell7.Text = rm.GetString("lblNjesia", ci);
            xrTableCell9.Text = rm.GetString("lblRaportSasia", ci);
            xrTableCell10.Text = rm.GetString("lblcmimNjesi", ci);
            xrTableCell6.Text = rm.GetString("labelVleftameTVSH", ci);
            xrTableCell15.Text = rm.GetString("labelVleftapaTVSH", ci);
            xrTableCell16.Text = rm.GetString("lblVlTvsh", ci);

            xrLabel13.Text = rm.GetString("lblFurnizimeTëTatueshme", ci);
            xrLabel12.Text = rm.GetString("lblFurnizimeTëPaTatueshme", ci);
            xrLabel20.Text = rm.GetString("lblPagesaDigicom", ci);
            xrLabel29.Text= rm.GetString("lblPagesaDigicom2", ci);
            xrLabel15.Text = rm.GetString("lblVlEUR", ci);
            xrLabel14.Text = rm.GetString("lblvlLEK", ci);
            xrLabel16.Text = rm.GetString("labelVleraTVSH", ci);
            xrLabel3.Text = rm.GetString("lblTotaliPaguar", ci);
            xrLabel22.Text = rm.GetString("lblPErdorimiFatures", ci);
            xrLabel23.Text = rm.GetString("lblDigicom", ci);
            xrLabel25.Text = rm.GetString("lblDigicomFiber", ci);
            xrLabel26.Text = rm.GetString("lblDigicomAdrENG", ci);
            xrLabel27.Text = rm.GetString("lblTelFaxDigicom", ci);
            xrLabel28.Text = rm.GetString("lblWebsiteEmailDigicom", ci);
          
        }

    }
}
