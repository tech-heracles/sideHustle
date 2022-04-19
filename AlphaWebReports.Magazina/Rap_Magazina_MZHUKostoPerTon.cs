using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using DevExpress.XtraCharts;
using System.Data;

namespace AlphaWebReports.RaportetDs.Magazina
{
    public partial class Rap_Magazina_MZHUKostoPerTon : DevExpress.XtraReports.UI.XtraReport
    {
        private int gjuha; CultureInfo ci;
        public Rap_Magazina_MZHUKostoPerTon(){ InitializeComponent(); }
        public Rap_Magazina_MZHUKostoPerTon(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report)
        {

        }
        public Rap_Magazina_MZHUKostoPerTon( CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
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

            xrLabel2.Text = rm.GetString("lblMZHUrezultat", ci);
            xrLabel4.Text = rm.GetString("lblMZHUkostoPaTurizem", ci);
            xrLabel6.Text = rm.GetString("lblMZHUBashkia", ci);
            xrLabel8.Text = rm.GetString("lblMZHUKostoTotale", ci);
            xrLabel1.Text = rm.GetString("lblMZHUkoka", ci);
            xrTableCell3.Text = rm.GetString("lblMZHUTotali", ci);

        }

    }
}
