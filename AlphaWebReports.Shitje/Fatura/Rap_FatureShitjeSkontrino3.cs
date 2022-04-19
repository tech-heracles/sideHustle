using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.RAP_SHITJE.Fatura
{
    public partial class Rap_FatureShitjeSkontrino3 : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FatureShitjeSkontrino3(){InitializeComponent();} 


        public Rap_FatureShitjeSkontrino3(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FatureShitjeSkontrino3(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
           // xrLabel12.Text = rm.GetString("labelNIPT", ci);
           // xrLabel6.Text = rm.GetString("labelRaportNrFature", ci);
           // xrLabel8.Text = rm.GetString("labelRaportNrSerial", ci);
            xrTableCell5.Text = rm.GetString("labelRaportiArtikull", ci);
            xrTableCell9.Text = rm.GetString("labelSasia", ci);
            xrTableCell10.Text = rm.GetString("labelCmimi", ci);
            xrTableCell8.Text = rm.GetString("lblRaportiTotaliBankaveArkave", ci);
            xrLabel45.Text = rm.GetString("lblPagesaEur", ci) ;
            xrLabel47.Text = rm.GetString("lblPagesaLEk", ci);
            xrLabel26.Text = rm.GetString("lblRaportiTotaliBankaveArkave", ci) + ":";
            xrLabel3.Text = rm.GetString("labelFleteGarancieUpperCase", ci);
            //xrLabel49.Text = rm.GetString("labelRaportJuFalemnderit", ci);
            xrLabel2.Text = rm.GetString("lblbGarancia", ci);
            xrTableCell1.Text = rm.GetString("labelRaportKlienti", ci);
            xrTableCell11.Text = rm.GetString("labelDyqani", ci);
            xrTableCell7.Text = rm.GetString("labelRaportTelefon", ci);
            xrTableCell17.Text = rm.GetString("lblFatura", ci);
        xrTableCell19.Text = rm.GetString("labelNIPT", ci);
            xrTableCell21.Text = rm.GetString("labelRaportData", ci);

        }

    }
}
