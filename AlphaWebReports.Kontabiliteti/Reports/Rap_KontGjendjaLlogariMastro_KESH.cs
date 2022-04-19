using System;
using System.Globalization;
using System.Resources;
using DevExpress.XtraReports.UI;

namespace AlphaWebReports.RaportetDs
{
    public partial class Rap_KontGjendjaLlogariMastro_KESH : DevExpress.XtraReports.UI.XtraReport
    {
        String monedheLlogarie = "";
        public Rap_KontGjendjaLlogariMastro_KESH(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdPerdoruesi, param.GuidString, param.IdRaporti, param.IdGjuha, param.Vjen,param.IdViti, param.IdSubRaporti, report)
        {

        }
    
    
        public Rap_KontGjendjaLlogariMastro_KESH(){
            InitializeComponent();
        }
        public Rap_KontGjendjaLlogariMastro_KESH(CultureInfo ci, int idNdermarrje, int idPerdoruesi, String guidString, int idRaporti, int idGjuha, String vjen, int idViti, int idSubRaporti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            monedheLlogarie = (raport.Parameters["monedheLl"].Value).ToString();
            InitializeComponent();
            EmrateLabelave(ci);
            
            
           
   
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
            xrLabel17.Text = rm.GetString("TitullRaportBILANCIVËRTETUES", ci);
            xrLabel1.Text = rm.GetString("NenTitullRaportiTrialBalance", ci);
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrLabel8.Text = rm.GetString("labelRaportiNrLlogari", ci);
            //xrLabel33.Text = rm.GetString("labelLogoIMB", ci);
            xrLabel9.Text = rm.GetString("filterRaportEmerLlogarie", ci);
            xrLabel10.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrLabel26.Text = rm.GetString("labelRaportiKredi", ci);
            xrLabel27.Text = rm.GetString("labelRaportiDebi", ci);
            xrLabel7.Text = rm.GetString("labelRaportiKredi", ci);
            xrLabel28.Text = rm.GetString("labelRaportiDebi", ci);
            xrLabel12.Text = rm.GetString("labelRaportiKredi", ci);
            xrLabel11.Text = rm.GetString("labelRaportiDebi", ci); 
            if(monedheLlogarie == "Po" ){
                 xrLabel15.Text = rm.GetString("labelRaportiGjendMonBaze", ci);
                 xrLabel25.Text = rm.GetString("labelRaportiGjendFillestareMonBaze", ci);
            }else{
                 xrLabel15.Text = rm.GetString("labelGjendjaAktuale", ci);
                 xrLabel25.Text = rm.GetString("labelGjendjaFillestare", ci);
            
            }
            xrLabel20.Text = rm.GetString("filterRaportLevizje", ci);
            xrLabel19.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel6.Text = rm.GetString("labelNdryshimiGjendjes", ci);
        }

      
    }
}
