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
namespace AlphaWebReports.RaportetDs
{
    public partial class Rap_FatureShitjeMeArtikujDAST : DevExpress.XtraReports.UI.XtraReport
    { 
		public Rap_FatureShitjeMeArtikujDAST(){InitializeComponent();} 
      

       public Rap_FatureShitjeMeArtikujDAST(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitjeMeArtikujDAST(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
           
            InitializeComponent();
            EmrateLabelave(ci);
            parameterIdNderm.Value = idNdermarrje;
            parameterIdPerdoruesi.Value = idPerdoruesi;
            }


    /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel32.Text = rm.GetString("lblDurres2015", ci);
           

        }

    }
}
