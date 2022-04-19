using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Arka.Raporte
{
    public partial class RAP_ARKA_PAGESADEALER : DevExpress.XtraReports.UI.XtraReport
    {
		public RAP_ARKA_PAGESADEALER(){InitializeComponent();} 

        public RAP_ARKA_PAGESADEALER(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public RAP_ARKA_PAGESADEALER(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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
            parameter9.Value = raport.Parameters[8].Value;

        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));


            xrLabel12.Text = rm.GetString("RaportPagesaDealerTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell19.Text = rm.GetString("labelEmri_i_Dyqanit", ci);
            xrTableCell1.Text = rm.GetString("labelNrMandatArketimiMandatPagese", ci);
            xrTableCell2.Text = rm.GetString("labelRaportData", ci);
            xrTableCell5.Text = rm.GetString("filterRaportiShoqeria", ci);
            xrTableCell2.Text = rm.GetString("labelTotali_i_pagesave", ci);
            xrTableCell21.Text = rm.GetString("labelTotali_i_garancive", ci);
            xrTableCell3.Text = rm.GetString("KthimMPESA", ci);
            xrTableCell17.Text = rm.GetString("labelGaranciaRoaming", ci);
           // xrTableCell22.Text = rm.GetString("labelPageseRegjistrimi", ci);
            xrTableCell4.Text = rm.GetString("labelSaveAndGo", ci);
            xrTableCell5.Text = rm.GetString("labelNdryshimi_i_MSISDN_Post_Pay", ci);
            xrTableCell6.Text = rm.GetString("labelListeAnalitikePrePay", ci);
            xrTableCell7.Text = rm.GetString("labelPageseFature", ci);
            xrTableCell8.Text = rm.GetString("filterRaportiNrTel", ci);
            xrTableCell9.Text = rm.GetString("labelCustomerNumber", ci);
            xrLabel11.Text = rm.GetString("labelTotali_i_arketimeve", ci);
            xrTableCell27.Text = rm.GetString("labelGarancia", ci);
            xrTableCell32.Text = rm.GetString("labelVleraTeArketuaraNeLeke", ci);
            xrTableCell26.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell24.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell28.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel61.Text = rm.GetString("labelLogoIMB", ci);        

        }

    }
}
