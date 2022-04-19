using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureShitjeMeArtikujManilla : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureShitjeMeArtikujManilla(){InitializeComponent();} 

        public Rap_FatureShitjeMeArtikujManilla(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitjeMeArtikujManilla(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel10.Text = rm.GetString("labelNrF", ci);
            xrLabel56.Text = rm.GetString("labelDatf", ci);
            xrTableCell17.Text = rm.GetString("labelRaportiNrKartele", ci).ToUpper();
            xrTableCell5.Text = rm.GetString("labelRaportiPershkrimi", ci).ToUpper();
            xrTableCell7.Text = rm.GetString("labelNjesia", ci).ToUpper();
            xrTableCell9.Text = rm.GetString("labelRaportSasia", ci).ToUpper();
            xrTableCell10.Text = rm.GetString("label_CMIMI", ci).ToUpper();
            xrTableCell11.Text = rm.GetString("labelVleftaMe_Tvsh", ci).ToUpper();
            xrLabel1.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel5.Text = rm.GetString("RaportOferteTitulli", ci);

        }

    }
}
