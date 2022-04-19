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
    public partial class Rap_FatureShitjeMeArtikujUniversReklamaMePermasaPaGaranci : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureShitjeMeArtikujUniversReklamaMePermasaPaGaranci(){InitializeComponent();} 

        public Rap_FatureShitjeMeArtikujUniversReklamaMePermasaPaGaranci(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitjeMeArtikujUniversReklamaMePermasaPaGaranci(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            //xrLabel25.Text = rm.GetString("labelRaportTel", ci);
            //xrLabel47.Text = rm.GetString("lblRaportAdreseDregimi", ci);
            //xrLabel48.Text = rm.GetString("lblRaportAdreseFaturimi", ci);
            //xrLabel6.Text = rm.GetString("labelRaportNrFature", ci);
            //xrLabel14.Text = rm.GetString("labelRaportReferenca", ci);
            //xrLabel7.Text = rm.GetString("labelRaportDateFature", ci);
            //xrLabel12.Text = rm.GetString("lblRaportAgjenti", ci);
            //xrLabel13.Text = rm.GetString("lblRaportKushtePagese", ci);
            //xrTableCell4.Text = rm.GetString("filterRaportPershkrimi", ci);
            //xrTableCell13.Text = rm.GetString("labelNjesia", ci);
            //xrTableCell14.Text = rm.GetString("labelRaportGjatesi", ci);
            //xrTableCell24.Text = rm.GetString("labelRaportGjeresi", ci);
            //xrTableCell28.Text = rm.GetString("lblRaportSasiPermase", ci);
            //xrTableCell5.Text = rm.GetString("labelSasia", ci);
            //xrTableCell9.Text = rm.GetString("labelCmimi", ci);
            //xrTableCell8.Text = rm.GetString("lblRaportZbritjaperqindje", ci);
            //xrTableCell6.Text = rm.GetString("labelVleraPaTVSH", ci);
            //xrLabel20.Text = rm.GetString("labelNentotal", ci);
            //xrLabel21.Text = rm.GetString("labelZbritje", ci);
            //xrLabel15.Text = rm.GetString("labelTVSH", ci);
            //xrTableCell17.Text = rm.GetString("labelKodi", ci);
            //xrTableCell18.Text = rm.GetString("filterRaportPershkrimi", ci);
            //xrTableCell19.Text = rm.GetString("lblRaportGarancia", ci);
        }

 
       

   
     

     

 

       
    }
}
