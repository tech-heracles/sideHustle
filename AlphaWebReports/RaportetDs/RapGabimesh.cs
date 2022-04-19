
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace AlphaWebReports.RaportetDs
{
    public partial class RapGabimesh : XtraReport
    {
        public RapGabimesh() { InitializeComponent(); }
        readonly string _vjen = "";

        public RapGabimesh (AlphaWebReports.Common.ParametraRaporti param, XtraReport report) :
            this(param.Ci, param.Vjen)
        {

        }

        public RapGabimesh(CultureInfo ci, string vjen)
        {
            _vjen = vjen;
            InitializeComponent();
            EmrateLabelave(ci);
        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        private void EmrateLabelave(CultureInfo ci)
        {
          
            ResourceManager rm = new ResourceManager("Resources.Strings",
                System.Reflection.Assembly.Load("App_GlobalResources"));

            if (_vjen == "serialeTePerdoruraNeDokDraft")
            {
                xrLabel12.Text = rm.GetString("RaportiGabimeveTeImportitTitulli", ci);
                xrLabel2.Text = rm.GetString("labelRaportGabimi", ci);
                xrLabel1.Text = rm.GetString("labelDateDokNrDok", ci);
                xrLabel3.Text = rm.GetString("labelLlojDokumenti", ci);
                xrLabel9.Text = rm.GetString("labelRaportUGjeten", ci);
                xrLabel8.Text = rm.GetString("labelRaportGabime", ci);
                xrLabel32.Text = rm.GetString("labelLogoIMB", ci);
                return;
            }
            if (_vjen == "krahasimi")
            {
                xrLabel12.Text = rm.GetString("RaportiGabimeve", ci);
                xrLabel2.Text = rm.GetString("labelRaportSasia", ci);
            }
            if (_vjen == "Sukses")
            {
                xrLabel12.Text = rm.GetString("raportiIImportimeve", ci);
                xrLabel2.Text = rm.GetString("labelRaportPershkrimi", ci);
                xrLabel8.Text = rm.GetString("labelRaportiRekorde", ci);
            }
            else
            {
                xrLabel12.Text = rm.GetString("RaportiGabimeveTeImportitTitulli", ci);
                xrLabel2.Text = rm.GetString("labelRaportGabimi", ci);
            }

            xrLabel1.Text = rm.GetString("labelKodi", ci);
            xrLabel3.Text = rm.GetString("labelRaportRreshti", ci);
            xrLabel9.Text = rm.GetString("labelRaportUGjeten", ci);
            xrLabel8.Text = rm.GetString("labelRaportGabime", ci);
            xrLabel32.Text = rm.GetString("labelLogoIMB", ci);
        }
    }
}
