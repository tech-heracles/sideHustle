using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.Amortizimi
{
    public partial class Rap_TabelaAmortizimit : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_TabelaAmortizimit(){InitializeComponent();} 


        public Rap_TabelaAmortizimit(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        {

        }
        public Rap_TabelaAmortizimit(CultureInfo ci, int idNdermarrje, int idViti, DevExpress.XtraReports.UI.XtraReport raport)
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
            parameter8.Value = raport.Parameters[7].Value;
            parameter9.Value = raport.Parameters[8].Value;
            parameter10.Value = raport.Parameters[9].Value;
            parameter11.Value = raport.Parameters[10].Value;
            parameter12.Value = raport.Parameters[11].Value;
            parameter13.Value = raport.Parameters[12].Value;
            parameter14.Value = raport.Parameters[13].Value;
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

            xrLabel17.Text = rm.GetString("RaportTabelaAmortizimitTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel37.Text = rm.GetString("labelLlojDokumenti", ci);
            xrTableCell12.Text = rm.GetString("filterNrDokRezervime", ci);
            xrTableCell11.Text = rm.GetString("labelDateDok", ci);
            xrTableCell13.Text = rm.GetString("labelFilterAvancuarMagazina", ci);
            xrTableCell1.Text = rm.GetString("labelKodi", ci);
            xrTableCell14.Text = rm.GetString("filterSeriali", ci);
            xrTableCell9.Text = rm.GetString("lblRaportiDtAmortizimi", ci);
            xrTableCell16.Text = rm.GetString("lblRaportiDtMePare", ci);
            xrTableCell15.Text = rm.GetString("lblRaportiVleftaGjendje", ci);
            xrTableCell2.Text = rm.GetString("lblRaportiAmortAkumuluar", ci);
            xrTableCell10.Text = rm.GetString("lblRaportiAmortVjetor", ci);
            xrTableCell4.Text = rm.GetString("labelRaportVlefta", ci) + "+/-";
            xrTableCell18.Text = rm.GetString("lblRaportiHDAmortGjith", ci);
            xrTableCell17.Text = rm.GetString("lblRaportiHDAmortVjetor", ci);
            xrTableCell5.Text = rm.GetString("lblRaportiHDAmortShtese", ci);
            xrTableCell6.Text = rm.GetString("lblRaportiDite", ci);
            xrTableCell3.Text = rm.GetString("loginLoginPerdoruesi", ci);
            xrTableCell42.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel71.Text = rm.GetString("labelLogoIMB", ci);
          
        }
    }
}
