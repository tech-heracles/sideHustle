using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Magazina.Format_Printimi
{
    public partial class Rap_Format_Zyrtar : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Format_Zyrtar(){InitializeComponent();} 
        
        public Rap_Format_Zyrtar(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi)
        {

        }
        public Rap_Format_Zyrtar(CultureInfo ci, int idNdermarrje, int idPerdoruesi)
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
     
            xrLabel10.Text = rm.GetString("labelRaportiNr", ci);
            xrLabel16.Text = rm.GetString("labelKodi", ci);
            xrLabel17.Text = rm.GetString("lblNjesi", ci);
            xrLabel18.Text = rm.GetString("lblSasi", ci);
            xrLabel19.Text = rm.GetString("lblCmim", ci);
            xrLabel20.Text = rm.GetString("cmbCmimeArtikulliVlere", ci);
            xrLabel2.Text = rm.GetString("labelFormatZyrtarTitulli", ci);
          

        }

   
    }
}
