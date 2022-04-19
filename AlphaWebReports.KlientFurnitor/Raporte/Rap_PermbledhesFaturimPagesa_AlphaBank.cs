using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.KlientFurnitor.Raporte
    {
    public partial class Rap_PermbledhesFaturimPagesa_AlphaBank : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_PermbledhesFaturimPagesa_AlphaBank(){InitializeComponent();} 

        CultureInfo ci;
      
        public Rap_PermbledhesFaturimPagesa_AlphaBank(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.IdGjuha, param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_PermbledhesFaturimPagesa_AlphaBank(int idGjuha, CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {
            this.ci = ci;
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


            xrLabel12.Text = rm.GetString("RaportPermbledhesFaturimePagesaTitullli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel32.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell7.Text = rm.GetString("filterMonedha", ci);
            xrTableCell12.Text = rm.GetString("labelLlojDokumenti", ci);
            xrTableCell13.Text = rm.GetString("labelRaportNumer", ci);
            xrTableCell9.Text = rm.GetString("lblRaportDtValute", ci);
            xrTableCell15.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell16.Text = rm.GetString("labelRaportVleraDokumentit", ci);
            xrTableCell17.Text = rm.GetString("labelRaportVleraLidhur", ci);
            xrTableCell18.Text = rm.GetString("labelRaportVleraMbetur", ci);
            xrTableCell19.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell29.Text = rm.GetString("labelRaportTotaliFatuimePagesa", ci);
        }

     
    }
    }
