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
    public partial class Rap_ArtikujGjendjaSipasAutorizimeve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ArtikujGjendjaSipasAutorizimeve(){InitializeComponent();} 
        public Rap_ArtikujGjendjaSipasAutorizimeve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_ArtikujGjendjaSipasAutorizimeve(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                           System.Reflection.Assembly.Load("App_GlobalResources"));

            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
            parameter6.Value = raport.Parameters[5].Value;
            parameter7.Value = raport.Parameters[6].Value;
            parameter8.Value = raport.Parameters[11].Value;
            parameter9.Value = raport.Parameters[12].Value;
            parameter10.Value = raport.Parameters[7].Value;
           degaAdministrative.Value = raport.Parameters[8].Value;
           parameter11.Value = raport.Parameters[9].Value;
            parameterIdNderm.Value = idNdermarrje;
        }


        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));


            xrLabel12.Text = rm.GetString("RaportGjendjaEArtikujveTitulli", ci);
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
