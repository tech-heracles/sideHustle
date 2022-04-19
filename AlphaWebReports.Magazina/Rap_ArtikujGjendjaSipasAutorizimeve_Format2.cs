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
    public partial class Rap_ArtikujGjendjaSipasAutorizimeve_Format2 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ArtikujGjendjaSipasAutorizimeve_Format2(){InitializeComponent();} 
        public Rap_ArtikujGjendjaSipasAutorizimeve_Format2(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_ArtikujGjendjaSipasAutorizimeve_Format2(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
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
            
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell7.Text = rm.GetString("labelKartela", ci);
            xrTableCell8.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell9.Text = rm.GetString("labelNjesia", ci);
            xrTableCell10.Text = rm.GetString("labelRaportHyrje", ci);
            xrTableCell11.Text = rm.GetString("LinkbtnLogOut", ci);
            xrTableCell12.Text = rm.GetString("labelRaportGjendje", ci);
            xrTableCell14.Text = rm.GetString("MenuItemRaportInventari", ci);
            xrLabel34.Text = rm.GetString("filterMagazina", ci) + ":";
            xrLabel37.Text = rm.GetString("labelLogoIMB", ci);

        }

    }
}
