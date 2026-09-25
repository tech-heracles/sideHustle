using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Magazina.Format_Printimi
{
    public partial class Rap_FormatMagazineArteg : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatMagazineArteg(){InitializeComponent();} 

        public Rap_FormatMagazineArteg(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatMagazineArteg(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            xrLabel49.Text = rm.GetString("labelAdresaKlientiArteg", ci);
        }


    }
}
