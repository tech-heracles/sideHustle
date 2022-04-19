using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Blerje

{
    public partial class Rap_ShpenzimeJoTeZbritshme : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_ShpenzimeJoTeZbritshme(){InitializeComponent();} 
    

        public Rap_ShpenzimeJoTeZbritshme(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_ShpenzimeJoTeZbritshme(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
            parameter6.Value = raport.Parameters[5].Value;
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
           ResourceManager rm = new ResourceManager("Resources.Strings",
                     System.Reflection.Assembly.Load("App_GlobalResources"));

           xrLabel17.Text = rm.GetString("RaportShpenzimeJoTeZbritshmeTitulli", ci);
            xrLabel37.Text = rm.GetString("FiltratEmertimi", ci);
             xrLabel5.Text = rm.GetString("labelKursi", ci);
            xrLabel32.Text = rm.GetString("filterNrDokRezervime", ci);
            xrLabel3.Text = rm.GetString("labelRaportiShenime", ci);
            xrLabel4.Text = rm.GetString("lblRaportDtValute", ci);
            xrLabel9.Text = rm.GetString("labelRaportMon", ci);
             xrLabel33.Text = rm.GetString("labelRaportiEmertimi", ci);
             xrLabel10.Text = rm.GetString("labelFurnitori", ci);
            xrLabel23.Text = rm.GetString("labelRaportLlogari", ci);
            xrLabel34.Text = rm.GetString("labelRaportVleraNeMB", ci);
              xrLabel35.Text = rm.GetString("labelRaportPershkrimi", ci);
             xrLabel24.Text = rm.GetString("labelRaportVlera", ci);
             xrLabel31.Text = rm.GetString("labelLogoIMB", ci);
             xrLabel39.Text = rm.GetString("labelRaportiTotali", ci);
             xrLabel6.Text = rm.GetString("filterDega", ci);
             xrLabel14.Text = rm.GetString("lblDtFature", ci);  
            

        }
    }
}
