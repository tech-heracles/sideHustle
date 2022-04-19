using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace PlatinumWeb.RaportetDs.Magazina
{
    public partial class Rap_GjendjaProdukteveAgreguar : DevExpress.XtraReports.UI.XtraReport
    {
        DbCore.DbAdmin.clsNdermarrje nderm;

        public Rap_GjendjaProdukteveAgreguar()
        {
            InitializeComponent();
        }

        public Rap_GjendjaProdukteveAgreguar(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            xrLabel54.Text = raport.Parameters["IdNdermarje"].Description;
            IdNdermarje.Value = raport.Parameters["IdNdermarje"].Value;
            xrLabel55.Text = raport.Parameters["filterKartela"].Description;
            filterKartela.Value = raport.Parameters["filterKartela"].Value;
            xrLabel56.Text = raport.Parameters["filterMagazina"].Description;
            filterMagazina.Value = raport.Parameters["filterMagazina"].Value;
            xrLabel57.Text = raport.Parameters["filterDtDok"].Description;
            filterDtDok.Value = raport.Parameters["filterDtDok"].Value;
            xrLabel58.Text = raport.Parameters["filterKompania"].Description;
            filterKompania.Value = raport.Parameters["filterKompania"].Value;
            nderm = new DbCore.DbAdmin.clsNdermarrje(idNdermarrje);
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrPictureBox1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Image", nderm, "ndermarrjeLogo")});
        }


    }
}
