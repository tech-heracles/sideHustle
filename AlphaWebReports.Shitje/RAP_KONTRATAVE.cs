using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Collections.Generic;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class RAP_KONTRATAVE : DevExpress.XtraReports.UI.XtraReport
    {
		public RAP_KONTRATAVE(){InitializeComponent();} 
        
     

        public RAP_KONTRATAVE(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public RAP_KONTRATAVE(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
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
            parameter15.Value = raport.Parameters[16].Value;
            parameter16.Value = raport.Parameters[15].Value;


        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {

            ResourceManager rm = new ResourceManager("Resources.Strings",
                        System.Reflection.Assembly.Load("App_GlobalResources"));
            
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel26.Text = rm.GetString("labelKodiKlientitUpperCase", ci);
            xrLabel34.Text = rm.GetString("labelKLIENTI", ci);
            xrLabel27.Text = rm.GetString("labelQytetiUpperCase", ci);
            xrLabel46.Text = rm.GetString("labelTelUpperCase", ci);
            xrLabel33.Text = rm.GetString("labelEmailUpperCase", ci);
            xrLabel32.Text = rm.GetString("labelKartelaUpperCase", ci);
            xrLabel35.Text = rm.GetString("label_SASIA", ci);
            xrLabel30.Text = rm.GetString("labelVleraUpperCase", ci);
            xrLabel29.Text = rm.GetString("labelLlojDokumentiUpperCase", ci);
            xrLabel28.Text = rm.GetString("labelDtFillimiUpperCase", ci);
            xrLabel31.Text = rm.GetString("labelDtMbarimiUpperCase", ci);
            xrLabel37.Text = rm.GetString("labelLogoIMB", ci);
        }
    }
}
