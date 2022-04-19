using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Globalization;
using System.Resources;
using System.Reflection;
namespace AlphaWebReports.RaportetDs.Arka.Raporte
{
    public partial class RAP_ARKA_SIPAS_KATEGORISE : DevExpress.XtraReports.UI.XtraReport
    {
		public RAP_ARKA_SIPAS_KATEGORISE(){InitializeComponent();} 
       
        private CultureInfo ci;
        int idraporti = -1;

        public RAP_ARKA_SIPAS_KATEGORISE(AlphaWebReports.Common.ParametraRaporti param, XtraReport report):
            this(param.Ci, param.IdNdermarrje, param.IdViti, param.IdPerdoruesi,param.IdRaporti, report)
        {

        }
        public RAP_ARKA_SIPAS_KATEGORISE(CultureInfo ci, int idNdermarrje, int idViti, int idPerdoruesi,int idRaporti, DevExpress.XtraReports.UI.XtraReport raport)
        {
            this.ci = ci;
            InitializeComponent();
            EmrateLabelave(ci);
            parameter1.Value = raport.Parameters[0].Value;
            parameter2.Value = raport.Parameters[1].Value;
            parameter3.Value = raport.Parameters[2].Value;
            parameter4.Value = raport.Parameters[3].Value;
            parameter5.Value = raport.Parameters[5].Value;
            parameter6.Value = raport.Parameters[6].Value;
            parameter7.Value = raport.Parameters[7].Value;
            parameter8.Value = raport.Parameters[8].Value;
            parameter9.Value = raport.Parameters[9].Value;
            parameter10.Value = raport.Parameters[11].Value;
            parameter11.Value = raport.Parameters[12].Value;
            DegaAdministrative.Value = raport.Parameters[10].Value;
            parameter13.Value = raport.Parameters[4].Value;

            ResourceManager rm = new ResourceManager("Resources.Strings",
            System.Reflection.Assembly.Load("App_GlobalResources"));
            parameterrm.Value = rm.GetString("cmbboxItemFilterAvancArka", ci) + ":";
            parameterrm2.Value = rm.GetString("cmbboxItemFilterAvancBanka", ci) + ":";
            parameterrm1.Value = rm.GetString("labelRaportVlArketuar", ci);
            parameterrm4.Value = rm.GetString("labelRaportVlPaguar", ci);
            idraporti = idRaporti;
            parameterIdrap.Value = idRaporti;
           switch (idRaporti)
           {
               case 220:
                    xrLabel5.Text = rm.GetString("MenuItemArketimet", ci);
                   break;
               case 222:
                    xrLabel5.Text = rm.GetString("MenuItemPagesat", ci);
                   break;
               case 223:
                    xrLabel5.Text = rm.GetString("labelRaportDerdhje", ci);
                   break;
               case 224:
                    xrLabel5.Text = rm.GetString("labelRaportTerheqje", ci);
                   break;
               default: xrLabel5.Text = " "; break;

           }
        }
        /// <summary>
        /// Vendos emrat e labelave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>

        private void EmrateLabelave(CultureInfo ci)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings",
            System.Reflection.Assembly.Load("App_GlobalResources"));
            FiltratLabel.Text = rm.GetString("FiltratEmertimi", ci);
            xrTableCell1.Text = rm.GetString("labelLloj", ci);
            xrTableCell12.Text = rm.GetString("labelDateDok", ci);
            xrTableCell2.Text = rm.GetString("cmbItemRaportNumri", ci);
            xrTableCell10.Text = rm.GetString("labelKunderParti", ci);
            xrTableCell2.Text = rm.GetString("labelRaportiPershkrimi", ci);
            xrTableCell17.Text = rm.GetString("labelRaportVlArketuar", ci);
            xrTableCell5.Text = rm.GetString("labelFilterAvancuarMonedha", ci);
            xrLabel19.Text = rm.GetString("labelRaportiTotali", ci);
            xrLabel7.Text = rm.GetString("labelLogoIMB", ci);                
        }
    }
}
