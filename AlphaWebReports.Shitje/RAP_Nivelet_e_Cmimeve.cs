using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class RAP_Nivelet_e_Cmimeve : DevExpress.XtraReports.UI.XtraReport
    {
		public RAP_Nivelet_e_Cmimeve(){InitializeComponent();} 




        public RAP_Nivelet_e_Cmimeve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }

        public RAP_Nivelet_e_Cmimeve(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            xrTableCell23.Text = DateTime.Now.ToString("dd'/'MM'/'yyyy");
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                      System.Reflection.Assembly.Load("App_GlobalResources"));


            xrLabel2.Text = rm.GetString("RaportLista_e_CmimeveTitulli", ci);
            xrTableCell17.Text = rm.GetString("RaportLista_e_CmimeveTitulli", ci);
            xrTableCell18.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrTableCell19.Text = rm.GetString("labelData_e_printimit", ci);
            xrLabel6.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrLabel13.Text = rm.GetString("labelNjesia", ci);
            xrLabel7.Text = rm.GetString("labelCmimi", ci);
            xrLabel25.Text = rm.GetString("labelTelefon", ci);
            

        }
      

    }
}
