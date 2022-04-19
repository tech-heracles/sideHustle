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
    public partial class Rap_MagazinaMaturimiStokut : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_MagazinaMaturimiStokut(){InitializeComponent();} 

        public Rap_MagazinaMaturimiStokut(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_MagazinaMaturimiStokut(CultureInfo ci, int idNdermarja, int idViti, int Idperdorues, XtraReport raport)
        { InitializeComponent();
        EmrateLabelave(ci);
            Ndermarja.Value = raport.Parameters[0].Value;
            DtDok.Value = raport.Parameters[1].Value;
            Magazina.Value = raport.Parameters[2].Value;
            Kartela.Value = raport.Parameters[3].Value;
            string data = raport.Parameters[1].Value.ToString();
            string strdtDok2 = data.Split('-')[1];
            DtDok2.Value = strdtDok2;
            Grupim1.Value = raport.Parameters[10].Value;
            Grupim2.Value = raport.Parameters[11].Value;
            FurnitorArt.Value = raport.Parameters[12].Value;
            DegaAdministrative.Value = raport.Parameters[13].Value;
            this.parameter1.Value = raport.Parameters[14].Value;
            parameter2.Value = raport.Parameters[15].Value;
            parameter3.Value = raport.Parameters[16].Value;
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));

            xrLabel12.Text = rm.GetString("RaportMaturimiStokutTitulli", ci);
            xrLabel1.Text = rm.GetString("filterNrPersonalPunonjesi", ci);
            xrLabel3.Text = rm.GetString("labelRaportiPershkrimi", ci) + ":";
            xrLabel23.Text = rm.GetString("filterArkaBankaEmer", ci);
            xrTableCell7.Text = rm.GetString("labelDokumenti", ci);
            xrTableCell13.Text = rm.GetString("labelRaportGjendje", ci);
            xrTableCell8.Text = rm.GetString("labelRaportDiteQendrimi", ci);
            xrLabel8.Text = rm.GetString("labelRaportSasiaSipasDiteqendrimit", ci);

            xrTableCell15.Text = rm.GetString("labelRaportiNr", ci);
            xrTableCell14.Text = rm.GetString("labelRaportData", ci);
            xrTableCell10.Text = rm.GetString("labelRaportiLloji", ci);
            xrTableCell17.Text = rm.GetString("labelSasia", ci);
            xrTableCell18.Text = rm.GetString("labelRaportVlera", ci);
            xrTableCell16.Text = rm.GetString("labelCmimi", ci);
            xrLabel33.Text = rm.GetString("labelRaportGjendjaNeDt", ci);
            xrLabel42.Text = rm.GetString("labelLogoIMB", ci);

        }
        
    }
}
