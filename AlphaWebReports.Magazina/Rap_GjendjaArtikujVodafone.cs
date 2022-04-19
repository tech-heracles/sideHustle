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
    public partial class Rap_GjendjaArtikujVodafone : DevExpress.XtraReports.UI.XtraReport
    {

        public Rap_GjendjaArtikujVodafone()
        {
            InitializeComponent();
        }
        public Rap_GjendjaArtikujVodafone(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report)
        {

        }

        public Rap_GjendjaArtikujVodafone(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
            parameter6.Value = raport.Parameters[5].Value;
            parameter7.Value = raport.Parameters[6].Value;
            parameter8.Value = raport.Parameters[7].Value;
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel35.Text = rm.GetString("RaportiGjendjeArtikujshVodafoneTitulli", ci);
            xrTableCell8.Text = rm.GetString("labelKartela", ci);
            xrTableCell9.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell10.Text = rm.GetString("labelKategoria", ci);
            xrTableCell11.Text = rm.GetString("labelNenkategoria", ci);
            xrTableCell12.Text = rm.GetString("labelRaportGjendjaeMeparshme", ci);
            xrTableCell13.Text = rm.GetString("labelSasiHyrje", ci);
            xrTableCell14.Text = rm.GetString("labelRaportSasiaDalje", ci);
            xrTableCell15.Text = rm.GetString("labelPorosiVFOne", ci);
            xrTableCell17.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
            xrTableCell6.Text = rm.GetString("labelDealeri", ci) + ":";
            xrTableCell7.Text = rm.GetString("labelDyqani", ci) + ":";
            xrLabel36.Text = rm.GetString("labelLogoIMB", ci) + ":";
            
        }


    }
}
