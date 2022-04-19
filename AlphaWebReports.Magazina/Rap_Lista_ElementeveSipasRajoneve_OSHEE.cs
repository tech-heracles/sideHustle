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
    public partial class Rap_Lista_ElementeveSipasRajoneve_OSHEE : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Lista_ElementeveSipasRajoneve_OSHEE(){InitializeComponent();} 
        public Rap_Lista_ElementeveSipasRajoneve_OSHEE(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, report)
        {

        }
        public Rap_Lista_ElementeveSipasRajoneve_OSHEE(CultureInfo ci, int idNdermarrje, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            parameter2.Value = raport.Parameters[1].Value;

            
            parameter1.Value = raport.Parameters["filterDtDok"].Value;
            
            parameter2.Value = raport.Parameters["filterElementeWEBGIS"].Value;
            
            parameter3.Value = raport.Parameters["filterStatusi"].Value;
            
            parameter4.Value = raport.Parameters["filterDegeAdministrative"].Value;
            
            parameter5.Value = raport.Parameters["filterLlojElementesh"].Value;
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            lblTitullRpt.Text = rm.GetString("lblListaRajoneve", ci);
            lblGrupSipasNJQV.Text = rm.GetString("lblrajoni", ci);
            xrTableCell14.Text = rm.GetString("lblKodrajoni", ci);
            xrTableCell15.Text = rm.GetString("lblEmerRajoni", ci);
            xrTableCell16.Text = rm.GetString("lblLayerelem", ci);
            xrTableCell17.Text = rm.GetString("cmbCmimeArtikulliCaptionKodi", ci);
            xrTableCell18.Text = rm.GetString("cmbCmimeArtikulliCaptionPershkrimi", ci);
            xrTableCell19.Text = rm.GetString("labelFilterAvancuarStatusi", ci);
            xrTableCell20.Text = rm.GetString("lblGjatSasi", ci);

        }
    }
}
