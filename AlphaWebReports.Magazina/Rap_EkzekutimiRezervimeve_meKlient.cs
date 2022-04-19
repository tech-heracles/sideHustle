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
    public partial class Rap_EkzekutimiRezervimeve_meKlient : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_EkzekutimiRezervimeve_meKlient(){InitializeComponent();} 
        public Rap_EkzekutimiRezervimeve_meKlient(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, param.IdGjuha, report)
        {

        }
        public Rap_EkzekutimiRezervimeve_meKlient(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, int idGjuha, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            InitializeComponent();
            EmrateLabelave(ci);
            switch (idGjuha)
            {
                case 0: //shqip
                    gjuha.Value = 0;
                    break;
                case 1: //anglisht
                    gjuha.Value = 1;
                    break;
                default: break;
            }
            parameterIdNderm.Value = raport.Parameters[3].Value;
            parameter2.Value = raport.Parameters[0].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[4].Value;
            parameter5.Value = raport.Parameters[6].Value;
            parameter6.Value = raport.Parameters[10].Value;
            parameter7.Value = raport.Parameters[11].Value;
            parameter8.Value = raport.Parameters[9].Value;
            parameter9.Value = raport.Parameters[7].Value;
            parameter10.Value = raport.Parameters[8].Value;
            parameter12.Value = raport.Parameters[1].Value;
  
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("lblRaportEkzekutimRezervim", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel22.Text = rm.GetString("lblRaportEkzekutimet", ci);
            xrLabel23.Text = rm.GetString("lblRaportDiferencat", ci);
            xrLabel13.Text = rm.GetString("labelLlojDokumenti", ci);
            xrLabel42.Text = rm.GetString("labelRaportiSubjekti", ci);
            xrLabel1.Text = rm.GetString("labelRaportiNrDok", ci);
            xrLabel2.Text = rm.GetString("labelRaportiDtDok", ci);
            xrLabel5.Text = rm.GetString("labelKartela", ci);
            xrLabel21.Text = rm.GetString("labelRaportiPershkrimi", ci);
            kokaTable.Text = rm.GetString("lblRaportDokHyrjeRezervim", ci);
            xrLabel20.Text = rm.GetString("labelNjesia", ci);
            xrLabel10.Text = rm.GetString("lblRaportSasiaRezervuar", ci);
            xrLabel19.Text = rm.GetString("lblRaportMagazina", ci);
            xrLabel18.Text = rm.GetString("labelSasia", ci);
            xrLabel17.Text = rm.GetString("lblRaportMagazina", ci);
            xrLabel16.Text = rm.GetString("labelSasia", ci);
            xrLabel15.Text = rm.GetString("lblRaportStatusi", ci);
            xrLabel37.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel40.Text = rm.GetString("labelRaportEmertimiKlientit", ci);

        }
    }
}
