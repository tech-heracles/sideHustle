using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Resources;
using System.Globalization;

namespace AlphaWebReports.RaportetDs.RAP_SHITJE
{
    public partial class Rap_ShitjeAnalitike_Vodafone : DevExpress.XtraReports.UI.XtraReport
    {

        public Rap_ShitjeAnalitike_Vodafone()
        {
            InitializeComponent();
        }
        public Rap_ShitjeAnalitike_Vodafone(AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.IdNdermarrje, report)
        {

        }
        public Rap_ShitjeAnalitike_Vodafone(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
                       System.Reflection.Assembly.Load("App_GlobalResources"));


            xrLabel35.Text = rm.GetString("RaportiShitjeAnalitikeVodafoneTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell1.Text = rm.GetString("labelDealeri", ci);
            xrTableCell2.Text = rm.GetString("labelDyqani", ci);
            xrTableCell3.Text = rm.GetString("labelRaportAdresa", ci);
            xrTableCell7.Text = rm.GetString("labelVeprimi", ci);
            xrTableCell9.Text = rm.GetString("labelDataVeprimit", ci);
            xrTableCell11.Text = rm.GetString("labelPerfaqesuesi_i_shitjes", ci);
            xrTableCell13.Text = rm.GetString("labelKategoria", ci);
            xrTableCell15.Text = rm.GetString("labelNenkategoria", ci);
            xrTableCell17.Text = rm.GetString("labelRaportProdukti", ci);
            xrTableCell25.Text = rm.GetString("labelSasia", ci);
            xrTableCell33.Text = rm.GetString("labelCmimiDealer", ci);
            xrTableCell27.Text = rm.GetString("labelCmimiRetail", ci);
            xrTableCell29.Text = rm.GetString("labelRaportiVleraTotale", ci);
            xrLabel33.Text = rm.GetString("labelTotaliPerDyqanin", ci);
            xrLabel65.Text = rm.GetString("labelTotaliPerDealerin", ci);
            xrLabel37.Text = rm.GetString("labelTotali", ci);
            xrLabel36.Text = rm.GetString("labelLogoIMB", ci);

        }
    }
}
