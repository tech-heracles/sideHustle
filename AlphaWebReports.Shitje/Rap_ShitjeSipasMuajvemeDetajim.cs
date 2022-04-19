using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_ShitjeSipasMuajvemeDetajim : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ShitjeSipasMuajvemeDetajim(){InitializeComponent();} 
        double shumavlera = 0;
        double shumashpenzime=0;
       
        public Rap_ShitjeSipasMuajvemeDetajim(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.IdRaporti, param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_ShitjeSipasMuajvemeDetajim(int idRaporti, CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
            parameter6.Value = raport.Parameters[5].Value;
            switch (ci.ToString())
            {
                case "sq-AL": //shqip
                    parameter7.Value = 0;
                    break;
                case "en_US": //anglisht
                    parameter7.Value = 1;
                    break;
                default: break;
            }

        }

        private decimal shuma;
        private decimal sasia;

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));


            xrLabel13.Text = rm.GetString("RaportShitjetSipasMuajveDetajimTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel15.Text = rm.GetString("labelKodi", ci) + ":";
            xrLabel29.Text = rm.GetString("labelBlerjeShitjeEmertimi", ci);
            Muaji.Text = rm.GetString("labelFilterKryesorMuaji", ci);
            SasiShitur.Text = rm.GetString("labelRaportSasiaShitur", ci);
            VleraShitur.Text = rm.GetString("labelRaportVleraShitur", ci);
            Shpenzime.Text = rm.GetString("labelRaportShpenzime", ci);
            ShpenzimePerq.Text = rm.GetString("labelRaportShpenzimePerq", ci);
            xrLabel16.Text = rm.GetString("labelRaportiTotali", ci);
             xrLabel12.Text = rm.GetString("labelLogoIMB", ci);
        }

    }
}
