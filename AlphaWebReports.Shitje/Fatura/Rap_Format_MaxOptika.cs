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
    public partial class Rap_Format_MaxOptika : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Format_MaxOptika(){InitializeComponent();} 
       
        public Rap_Format_MaxOptika(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_Format_MaxOptika(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel1.Text = rm.GetString("TitullRaportiFatureShitje", ci);
            xrLabel66.Text = rm.GetString("labelNumriIKlientit", ci) + ":";
            xrLabel70.Text = rm.GetString("labelNIPTiKlientit", ci) + ":";
            xrLabel69.Text = rm.GetString("labelRaportNumriFatures", ci) + ":";
            xrLabel62.Text = rm.GetString("labelRaportiDateFaturimi", ci);
            xrLabel16.Text = rm.GetString("labelNipti", ci) + ":";
            xrLabel63.Text = rm.GetString("labelRaportDateMaturmi", ci) + ":";
            xrLabel83.Text = rm.GetString("labelDateDokumenti", ci) + ":";
            xrLabel14.Text = rm.GetString("labelRaportiCelular", ci) + ":";
            xrLabel4.Text = rm.GetString("labelFatureEmail", ci);
            xrTableCell17.Text = rm.GetString("lblNr", ci) + ".";
            xrTableCell5.Text = rm.GetString("cmbCmimeArtikulliCaptionPershkrimi", ci);
            xrTableCell9.Text = rm.GetString("labelRaportSasia", ci);
            xrTableCell7.Text = rm.GetString("labelRaportiNjesiMatje", ci);
            xrTableCell10.Text = rm.GetString("labelCmimi", ci); 
            xrTableCell3.Text = rm.GetString("labelRaportiZbritjaPerqindje", ci);
            xrTableCell6.Text = rm.GetString("labelRaportiVatIdentifier", ci);
            xrTableCell8.Text = rm.GetString("labelVleraMeTVSH", ci);
            xrTableCell38.Text = rm.GetString("labelVleraPaTVSH", ci); 
            xrLabel11.Text = rm.GetString("labelRaportiTVSHSpecifikime", ci);
            xrTableCell4.Text = rm.GetString("labelRaportiTatimiAplikuar", ci);
            xrTableCell19.Text = rm.GetString("labelRaportiVatIdentifier", ci) + " %";
            xrTableCell20.Text = rm.GetString("labelRaportiLineAmount", ci);
            xrTableCell21.Text = rm.GetString("labelRaportiTotaliPaTvshMeZbritje", ci);
            xrTableCell22.Text = rm.GetString("labelRaportiShumaEZbritjes", ci);
            xrTableCell23.Text = rm.GetString("labelRaportiBzaETatueshme", ci);
            xrTableCell24.Text = rm.GetString("labelRaportiShumaETatimit", ci);
            xrTableCell40.Text = rm.GetString("labelRaportTotal", ci);
            xrTableCell52.Text = rm.GetString("labelRaportTotal", ci);
            xrTableCell47.Text = rm.GetString("lblTotLek", ci);

        }

       
        
    }
}
