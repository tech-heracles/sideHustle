using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Resources;
using System.Globalization;


namespace AlphaWebReports.RaportetDs.KlientFurnitor
{
    public partial class Rap_Permbledhes_Klienteve : DevExpress.XtraReports.UI.XtraReport
    {
		public Rap_Permbledhes_Klienteve(){InitializeComponent();} 
        public Rap_Permbledhes_Klienteve(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi, report)
        {

        }
        public Rap_Permbledhes_Klienteve(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi, DevExpress.XtraReports.UI.XtraReport raport)
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
            //xrTableCell23.Text = rm.GetString("labelRaportTotal", ci);
            //xrLabel1.Text = rm.GetString("FiltratEmertimi", ci);
            Titulli.Text = rm.GetString("labelRegjistriPermbledhesiKlienteve", ci);
            //xrTableCell13.Text = rm.GetString("labelRaportiEmertimi", ci);
            //xrTableCell14.Text = rm.GetString("labelRaportXhiro", ci);
            //xrTableCell15.Text = rm.GetString("labelRaportXhiro", ci);
            //xrTableCell16.Text = rm.GetString("labelRaportXhiro", ci);
            //xrTableCell1.Text = rm.GetString("lblDiferenca", ci);
            //xrTableCell4.Text = rm.GetString("lblDiferenca", ci);
            //xrTableCell17.Text = rm.GetString("labelRaportiRritjes", ci);
            //xrTableCell2.Text = rm.GetString("labelRaportiRritjes", ci);
            //xrTableCell23.Text = rm.GetString("labelRaportiTotali", ci);
            //xrLabel20.Text = rm.GetString("labelLogoIMB", ci);
        }
    }
}
 
