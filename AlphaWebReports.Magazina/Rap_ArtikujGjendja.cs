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
    public partial class Rap_ArtikujGjendja : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ArtikujGjendja(){InitializeComponent();} 
        public Rap_ArtikujGjendja(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_ArtikujGjendja(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
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
            if (raport.Parameters["grupoSipasGrupim1"].Value.ToString() == "Jo" || raport.Parameters["grupoSipasGrupim1"].Value.ToString() == "No")
                xrLabel13.Text = rm.GetString("labelKartela", ci);
            else
                xrLabel13.Text = rm.GetString("labelKodi", ci);

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
            xrLabel21.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel20.Text = rm.GetString("labelNjesia", ci);
            xrLabel18.Text = rm.GetString("labelRaportHyrje", ci);
            xrLabel17.Text = rm.GetString("LinkbtnLogOut", ci);
            xrLabel16.Text = rm.GetString("labelRaportGjendje", ci);
            xrLabel14.Text = rm.GetString("MenuItemRaportInventari", ci);
            xrLabel34.Text = rm.GetString("filterMagazina", ci) + ":";
            xrLabel37.Text = rm.GetString("labelLogoIMB", ci);
            
        }

    }
}
