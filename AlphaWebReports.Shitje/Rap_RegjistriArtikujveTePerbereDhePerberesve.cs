using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_RegjistriArtikujveTePerbereDhePerberesve : DevExpress.XtraReports.UI.XtraReport
    {
        public Rap_RegjistriArtikujveTePerbereDhePerberesve()
        {
            InitializeComponent();
        }
        public Rap_RegjistriArtikujveTePerbereDhePerberesve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_RegjistriArtikujveTePerbereDhePerberesve(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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
          ResourceManager  rm = new ResourceManager("Resources.Strings",
                     System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel17.Text = rm.GetString("RaportRegjistriArtikujveTePerbereDhePerberesve", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel1.Text = rm.GetString("labelRaportiNrDok", ci);
            xrLabel11.Text = rm.GetString("labelRaportiDtDok", ci);
            xrTableCell13.Text = rm.GetString("labelRaportKlient", ci);
            xrLabel5.Text = rm.GetString("labelKodi", ci);
            xrLabel18.Text = rm.GetString("labelRaportPershkrimi", ci);
            xrTableCell19.Text = rm.GetString("labelNjesia", ci);
            xrLabel6.Text = rm.GetString("labelRaportSasia", ci);
            xrLabel16.Text = rm.GetString("labelCmimi", ci);
            xrTableCell9.Text = rm.GetString("labelRaportVlefta", ci);
            xrLabel43.Text = rm.GetString("labelLogoIMB", ci);
        }
    }
}
