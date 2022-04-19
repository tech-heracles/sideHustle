using System;
using System.Globalization;
using System.Resources;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs
{
    public partial class Rap_KontGjendjaLlogariMastro : DevExpress.XtraReports.UI.XtraReport
    {
        String monedheLlogarie = "";
        public Rap_KontGjendjaLlogariMastro(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, report)
        {

        }
        public Rap_KontGjendjaLlogariMastro() {
            InitializeComponent();
        }
        public Rap_KontGjendjaLlogariMastro(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[4].Value;
            parameter6.Value = raport.Parameters[5].Value;
            parameter7.Value = raport.Parameters[6].Value;
            parameter8.Value = raport.Parameters[7].Value;
            Azhornim.Value = raport.Parameters[8].Value;
            parameter9.Value = raport.Parameters[10].Value;
            monedheLlogarie = (raport.Parameters["monedheLl"].Value).ToString();
           
        }

        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            xrLabel17.Text = rm.GetString("RaportBilanciVertetuesTitulli", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell31.Text = rm.GetString("labelRaportiNrLlogari", ci);
            xrLabel33.Text = rm.GetString("labelLogoIMB", ci);
            xrTableCell32.Text = rm.GetString("filterRaportEmerLlogarie", ci);
            xrTableCell33.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrTableCell35.Text = rm.GetString("labelRaportiKredi", ci);
            xrTableCell34.Text = rm.GetString("labelRaportiDebi", ci);
            xrTableCell37.Text = rm.GetString("labelRaportiKredi", ci);
            xrTableCell36.Text = rm.GetString("labelRaportiDebi", ci);
            xrTableCell40.Text = rm.GetString("labelRaportiKredi", ci);
            xrTableCell39.Text = rm.GetString("labelRaportiDebi", ci);
            if(monedheLlogarie == "Po" ){
                xrTableCell61.Text = rm.GetString("labelRaportiGjendMonBaze", ci);
                xrTableCell59.Text = rm.GetString("labelRaportiGjendFillestareMonBaze", ci);
            }else{
                xrTableCell61.Text = rm.GetString("labelRaportiGjendje", ci);
                xrTableCell59.Text = rm.GetString("labelRaportiGjendjeFillestare", ci);
            
            }
            xrTableCell38.Text = rm.GetString("filterRaportLevizje", ci);
            xrTableCell44.Text = rm.GetString("labelRaportiTotali", ci);
            xrTableCell60.Text = rm.GetString("filterRaportLevizje", ci);
        }
    }
}
