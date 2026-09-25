using System;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;

namespace AlphaWebReports.RaportetDs.Magazina.Format_Printimi
{
    public partial class Rap_FormatStandart_ASH : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_FormatStandart_ASH(){InitializeComponent();} 


        public Rap_FormatStandart_ASH(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_FormatStandart_ASH(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
            var rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));


            ////xrTableCell9.Text = rm.GetString("labelCmimi", ci);
            ////xrTableCell8.Text = rm.GetString("labelZbritje", ci) + "%";
            ////xrTableCell6.Text = rm.GetString("labelVleraPaTVSH", ci);
            ////xrTableCell3.Text = rm.GetString("labelTVSH", ci);
            ////xrTableCell7.Text = rm.GetString("labelVleraMeTVSH", ci);


        }

    }
}
