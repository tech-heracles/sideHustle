using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Collections.Generic;
namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_GjendjaKerkesaRecepturavePerProdhim_Total : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_GjendjaKerkesaRecepturavePerProdhim_Total(){InitializeComponent();} 
        
        public Rap_GjendjaKerkesaRecepturavePerProdhim_Total(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_GjendjaKerkesaRecepturavePerProdhim_Total(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
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

            xrLabel12.Text = rm.GetString("RaportTitullGjendjaRecepturaveTotal", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel18.Text = rm.GetString("labelKodi", ci);
            xrTableCell9.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell10.Text = rm.GetString("labelNjesia", ci);
            xrTableCell11.Text = rm.GetString("lblSasiaKerkuar", ci);
            xrTableCell12.Text = rm.GetString("lblGjendjaNeMagazine", ci);
            xrTableCell13.Text = rm.GetString("lblGjendjeMinimum", ci);
            xrTableCell14.Text = rm.GetString("lblSasiPorosiProdhuar", ci);
            xrTableCell15.Text = rm.GetString("lblKoheProdhuar", ci);
            xrTableCell8.Text = rm.GetString("labelFurnitori", ci);
            xrLabel28.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel31.Text = rm.GetString("lblKohaTotMin", ci);

        }
    }
}
