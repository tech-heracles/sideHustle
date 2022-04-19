using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
using AlphaWebReports.Common;

namespace AlphaWebReports.RaportetDs.PasqyratFinaciare.Raporte
{
        public partial class Rap_Buxhetimi : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Buxhetimi(){InitializeComponent();} 
        public Rap_Buxhetimi(ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, param.IdViti, report)
        { }
        public Rap_Buxhetimi(CultureInfo ci, int idNdermarrje, int idViti, XtraReport raport)
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
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel12.Text = rm.GetString("lblTitulliRaportiBuxhetimit", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel7.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel1.Text = rm.GetString("labelRaportiNr", ci);
            xrLabel107.Text = rm.GetString("lblBuxhetiMiratuar", ci);
            xrLabel19.Text = rm.GetString("labelRaportiEmertimi", ci);
            xrLabel20.Text = rm.GetString("lblShumaENdryshimeve", ci);
            xrLabel21.Text = rm.GetString("lblBuxhetiProgresiv", ci);
            xrLabel22.Text = rm.GetString("lblLikujduarGjateVitit", ci);
            xrLabel16.Text = rm.GetString("lblDiferenca", ci);
            xrLabel2.Text = rm.GetString("lblRealizimiNePerqindje", ci);
        }


    
    }
}
