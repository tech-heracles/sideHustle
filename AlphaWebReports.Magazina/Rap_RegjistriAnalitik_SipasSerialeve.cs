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
    public partial class Rap_RegjistriAnalitik_SipasSerialeve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_RegjistriAnalitik_SipasSerialeve(){InitializeComponent();}
        public Rap_RegjistriAnalitik_SipasSerialeve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_RegjistriAnalitik_SipasSerialeve(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
        {   InitializeComponent();
            EmrateLabelave(ci);

        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
          ResourceManager  rm = new ResourceManager("Resources.Strings",
                     System.Reflection.Assembly.Load("App_GlobalResources"));

          xrLabel17.Text = rm.GetString("RaportRegjistriAnalitikSipasSerialeveTitulli", ci);
          FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell1.Text = rm.GetString("labelFilterAvancuarLlojDok", ci);
            xrTableCell6.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell2.Text = rm.GetString("labelFilterAvancuarNrDok", ci);
            xrTableCell3.Text = rm.GetString("labelFilterKryesorDtDokumenti", ci);
            xrTableCell4.Text = rm.GetString("labelFilterAvancuarDtRegjistrimi", ci);
            xrTableCell5.Text = rm.GetString("labelKartela", ci);
            xrTableCell10.Text = rm.GetString("labelNjesia", ci);
            xrTableCell9.Text = rm.GetString("labelSasia", ci);
            xrTableCell7.Text = rm.GetString("labelVlefta", ci);
            xrTableCell8.Text = rm.GetString("labelCmimi", ci);
          xrLabel23.Text = rm.GetString("labelRaportiTotali", ci);
          xrLabel25.Text = rm.GetString("labelRaportTotaliMagazines", ci);
          xrLabel20.Text = rm.GetString("labelFilterAvancuarGjendja", ci);
          xrLabel36.Text = rm.GetString("labelLogoIMB", ci);
          xrLabel28.Text = rm.GetString("labelRaportMagazina", ci);
            
        }

    }
}
